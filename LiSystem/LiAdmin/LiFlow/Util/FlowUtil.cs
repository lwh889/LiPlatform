using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using LiCommon.Util;
using LiFlow.Enums;
using LiFlow.Model;
using LiHttp.GetEntity;
using LiHttp.RequestParam;
using LiModel.Form;
using LiFlow.Element;
using LiModel.Basic;
using LiModel.LiEnum;
using LiHttp.Enum;
using LiContexts;

namespace LiFlow.Util
{
    public class FlowUtil
    {
        /// <summary>
        /// 替换占位符
        /// </summary>
        /// <param name="messageStr"></param>
        /// <param name="formDataDict"></param>
        /// <returns></returns>
        public static string replaceMessagePlaceholder(string messageStr, Dictionary<string, object> formDataDict)
        {
            List<string> placeholders = RegexUtil.CollectString(@"\{(.*?)\}", messageStr);

            foreach (string placeholder in placeholders)
            {
                string[] fieldNames = placeholder.Split('.');
                if (fieldNames.Length <= 1) continue;

                messageStr = messageStr.Replace(string.Format("{{{0}}}", placeholder), Convert.ToString(formDataDict[fieldNames[1]]));
            }
            return messageStr;
        }

        public static LiVoucherFlowModel getCurrentFlow(string entityKey, string voucherId)
        {
            //1、获取当前正在走的流程
            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("entityKey", entityKey);
            whereDict.Add("voucherId", voucherId);
            whereDict.Add("flowStatus", FlowStatus.RUN);
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow,LiContext.SystemCode).getEntitySingle<LiVoucherFlowModel>(whereDict);

        }

        public static LiVersionFlowNodeModel getNextStepFlow(string entityKey, string voucherId, out LiVoucherFlowStepModel liVoucherFlowStepModel, out LiVoucherFlowModel liVoucherFlowTemp, out LiVersionFlowModel liVersionFlowModel)
        {
            liVoucherFlowStepModel = null;
            liVersionFlowModel = null;
            liVoucherFlowTemp = getCurrentFlow(entityKey, voucherId);

            if (liVoucherFlowTemp == null || liVoucherFlowTemp.flowStatus == null)
            {
                return null;
            }
            //2、获取当前流程的最新一步
            liVoucherFlowStepModel = liVoucherFlowTemp.datas.LastOrDefault();
            liVersionFlowModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).getEntitySingle<LiVersionFlowModel>(liVoucherFlowTemp.flowVersionId, "id");
            //3、获取下一步审批流节点
            LiVersionFlowNodeModel liVersionFlowNodeModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlowNode, LiContext.SystemCode).getEntitySingle<LiVersionFlowNodeModel>(liVoucherFlowStepModel.flowVersionNextStepNodeId, "id");

            return liVersionFlowNodeModel;
        }

        /// <summary>
        /// 执行流程
        /// </summary>
        public static bool execFlow(string content, string entityKey, string voucherId, string voucherCode, string mainTableName, FormModel formModel,LiVersionFlowNodeModel liVersionFlowNodeModel, LiVoucherFlowStepModel liVoucherFlowStepModel, LiVoucherFlowModel liVoucherFlowTemp, LiVersionFlowModel liVersionFlowModel, Dictionary<string, object> formDataDict, out string resultContent)
        {
            resultContent = string.Empty;

            Dictionary<string, object> whereDict = new Dictionary<string, object>();

            switch (liVersionFlowNodeModel.flowNodeType)
            {
                //如果是节点类型
                case FlowNodeType.NODEELEMENT:
                    //4、判断当前用户是否当前审批人
                    LiVersionFlowUserModel liVersionFlowUserModel = liVersionFlowNodeModel.users.Where(m => m.userCode == LiContexts.LiContext.userInfo.userCode).FirstOrDefault();
                    if (liVersionFlowUserModel != null)
                    {
                        //记录当前节点

                        LiVoucherFlowStepModel liVoucherFlowStep = new LiVoucherFlowStepModel();
                        liVoucherFlowStep.flowSeq = liVoucherFlowStepModel.flowSeq + 1;
                        liVoucherFlowStep.flowStatus = FlowStatus.FINISH;
                        liVoucherFlowStep.flowUserCode = LiContexts.LiContext.userInfo.userCode;
                        liVoucherFlowStep.flowUserName = LiContexts.LiContext.userInfo.userName;
                        liVoucherFlowStep.flowContent = content;
                        liVoucherFlowStep.flowApprovalType = ApprovalType.Agree;
                        liVoucherFlowStep.flowDate = DateTime.Now;
                        liVoucherFlowStep.flowVersionNodeId = liVersionFlowNodeModel.id;
                        liVoucherFlowTemp.datas.Add(liVoucherFlowStep);

                        //5、如果是，则获取再一个审批节点判断走向
                        LiVersionFlowConnectorModel liVersionFlowConnectorModel = liVersionFlowNodeModel.connectors[0];
                        whereDict.Clear();
                        whereDict.Add("flowId", liVersionFlowNodeModel.flowId);
                        whereDict.Add("flowNodeCode", liVersionFlowConnectorModel.flowNodeCodeTo);
                        liVersionFlowNodeModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlowNode, LiContext.SystemCode).getEntitySingle<LiVersionFlowNodeModel>(whereDict);

                        //如果是分支，则判断所有条件
                        if (liVersionFlowNodeModel.flowNodeType == FlowNodeType.CONDITIONELEMENT)
                        {
                            foreach (LiVersionFlowConnectorModel liVersionFlowConnectorModelTemp in liVersionFlowNodeModel.connectors)
                            {
                                //6、判断分支条件是否满足
                                string queryWhereStr = CommonUtil.getFlowWhereStr(liVersionFlowConnectorModelTemp.conditions, true);
                                queryWhereStr += string.Format(" and Li{0}_{1} = ''{2}'' ", mainTableName, formModel.keyFieldName, voucherId);

                                Dictionary<string, object> paramDict = new Dictionary<string, object>();
                                paramDict.Clear();
                                paramDict.Add("childTableName", "");
                                paramDict.Add("entityKey", entityKey);
                                paramDict.Add("whereSql", queryWhereStr);
                                paramDict.Add("orderBySql", "");

                                //7、如果满足，则生成流程步骤
                                DataTable dt = LiContexts.LiContext.getHttpEntity("sp_QueryList").execProcedure_DataTable( paramDict);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //记录分支
                                    liVoucherFlowStep = new LiVoucherFlowStepModel();
                                    liVoucherFlowStep.flowSeq = liVoucherFlowStep.flowSeq + 1;
                                    liVoucherFlowStep.flowStatus = FlowStatus.FINISH;
                                    liVoucherFlowStep.flowUserCode = LiContexts.LiContext.userInfo.userCode;
                                    liVoucherFlowStep.flowUserName = LiContexts.LiContext.userInfo.userName;
                                    liVoucherFlowStep.flowContent = content;
                                    liVoucherFlowStep.flowApprovalType = ApprovalType.Agree;
                                    liVoucherFlowStep.flowDate = DateTime.Now;
                                    liVoucherFlowStep.flowVersionNodeId = liVersionFlowNodeModel.id;

                                    //8、获取分支下一步的节点
                                    whereDict.Clear();
                                    whereDict.Add("flowId", liVersionFlowNodeModel.flowId);
                                    whereDict.Add("flowNodeCode", liVersionFlowConnectorModelTemp.flowNodeCodeTo);
                                    liVersionFlowNodeModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlowNode, LiContext.SystemCode).getEntitySingle<LiVersionFlowNodeModel>(whereDict);

                                    liVoucherFlowStep.flowVersionNextStepNodeId = liVersionFlowNodeModel.id;

                                    liVoucherFlowTemp.datas.Add(liVoucherFlowStep);
                                    LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).updateEntity(liVoucherFlowTemp);
                                    if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                                    {
                                        List<MessageModel> messageList = new List<MessageModel>();
                                        foreach (LiVersionFlowUserModel liVersionFlowUserModelTemp in liVersionFlowNodeModel.users)
                                        {

                                            MessageModel messageModel = new MessageModel();
                                            messageModel.messageType = MessageType.Flow;
                                            messageModel.messageContent = replaceMessagePlaceholder(liVersionFlowNodeModel.flowNodeInformation, formDataDict); 
                                            messageModel.messageDate = DateTime.Now;
                                            messageModel.flowVersionId = liVersionFlowModel.id;
                                            messageModel.flowVersionNumber = liVersionFlowModel.flowVersionNumber;
                                            messageModel.flowCode = liVersionFlowModel.flowCode;
                                            messageModel.flowName = liVersionFlowModel.flowName;
                                            messageModel.entityKey = liVersionFlowModel.entityKey;
                                            messageModel.entityName = liVersionFlowModel.entityName;
                                            messageModel.voucherId = Convert.ToString(voucherId);
                                            messageModel.voucherCode = Convert.ToString(voucherCode);
                                            messageModel.userCode = liVersionFlowUserModelTemp.userCode;

                                            messageList.Add(messageModel);
                                        }

                                        LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).batchNewEntity(messageList);

                                        if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).bSuccess)
                                        {
                                            resultContent = "已审核！消息已送！";
                                        }
                                        else
                                        {
                                            resultContent = "已审核！";
                                        }

                                        return true;
                                    }
                                    else
                                    {
                                        resultContent = "审核失败！";

                                        return false;
                                    }
                                }
                                else
                                {
                                    //循环所有分支节点
                                    resultContent = "当前流程异常！";
                                }
                            }
                        }
                        else if (liVersionFlowNodeModel.flowNodeType == FlowNodeType.ENDELEMENT)
                        {
                            liVoucherFlowTemp.flowStatus = FlowStatus.FINISH;
                            liVoucherFlowTemp.flowEndDate = DateTime.Now;

                            liVoucherFlowStep = new LiVoucherFlowStepModel();
                            liVoucherFlowStep.flowSeq = liVoucherFlowStepModel.flowSeq + 1;
                            liVoucherFlowStep.flowStatus = FlowStatus.FINISH;
                            liVoucherFlowStep.flowUserCode = LiContexts.LiContext.userInfo.userCode;
                            liVoucherFlowStep.flowUserName = LiContexts.LiContext.userInfo.userName;
                            liVoucherFlowStep.flowContent = content;
                            liVoucherFlowStep.flowApprovalType = ApprovalType.Agree;
                            liVoucherFlowStep.flowDate = DateTime.Now;
                            liVoucherFlowStep.flowVersionNodeId = liVersionFlowNodeModel.id;
                            liVoucherFlowTemp.datas.Add(liVoucherFlowStep);
                            //完成审批流

                            LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).updateEntity(liVoucherFlowTemp);
                            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                            {
                                resultContent = "已审核";

                                return true;
                            }
                            else
                            {
                                resultContent = "审核失败！";

                                return false;
                            }
                        }
                        else
                        {
                            resultContent = "当前流程异常！";

                            return false;
                        }
                    }
                    else
                    {
                        resultContent = "不是当前审批节点的用户！";

                        return false;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// 撤销流程
        /// </summary>
        public static bool revokeFlow(string content, string revokeType, string entityKey, string voucherId, LiVoucherFlowStepModel liVoucherFlowStepModel, out string resultContent)
        {
            resultContent = "";

            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).getQueryParamModel_ShowAllColumn();

            QueryComplexWhereModel queryComplexWhereModel = QueryComplexWhereModel.AND();
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("entityKey", entityKey));
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("voucherId", voucherId));
            QueryComplexWhereModel queryComplexWhereModelTemp = QueryComplexWhereModel.OR();
            queryComplexWhereModelTemp.wheres.Add(QueryComplexWhereModel.OR("flowStatus", FlowStatus.RUN));
            queryComplexWhereModelTemp.wheres.Add(QueryComplexWhereModel.OR("flowStatus", FlowStatus.HANGUP));
            queryComplexWhereModel.wheres.Add(queryComplexWhereModelTemp);

            paramModel.complexWheres = queryComplexWhereModel;

            LiVoucherFlowModel liVoucherFlowTemp = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).getEntitySingle<LiVoucherFlowModel>(paramModel);

            switch (revokeType)
            {
                case RevokeType.UnSubmit:
                    if (liVoucherFlowTemp != null && liVoucherFlowTemp.flowStatus == FlowStatus.RUN && liVoucherFlowTemp.datas.Count == 1)
                    {
                        liVoucherFlowTemp.flowStatus = FlowStatus.REVOKE;
                        liVoucherFlowTemp.flowEndDate = DateTime.Now;


                        LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).updateEntity(liVoucherFlowTemp);
                        if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                        {
                            resultContent = "已撤销";
                            return true;
                        }
                    }
                    else
                    {
                        resultContent = "流程正在行动中...";
                        return false;
                    }
                    break;
                case RevokeType.Stop:
                case RevokeType.Revoke:
                    if (liVoucherFlowTemp != null && liVoucherFlowTemp.flowStatus != FlowStatus.FINISH && liVoucherFlowTemp.flowStatus != FlowStatus.REVOKE)
                    {
                        liVoucherFlowTemp.flowStatus = revokeType;
                        liVoucherFlowTemp.flowEndDate = DateTime.Now;


                        LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).updateEntity(liVoucherFlowTemp);
                        if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                        {
                            resultContent = "已撤销！";
                            return true;
                        }
                        else
                        {
                            resultContent = "撤销失败！";
                            return false;
                        }
                    }
                    break;
                case RevokeType.Disagree:
                    
                    liVoucherFlowTemp.flowStatus = FlowStatus.REVOKE;
                    liVoucherFlowTemp.flowEndDate = DateTime.Now;

                    LiVoucherFlowStepModel liVoucherFlowStep = new LiVoucherFlowStepModel();
                    liVoucherFlowStep.flowSeq = liVoucherFlowStepModel.flowSeq + 1;
                    liVoucherFlowStep.flowStatus = FlowStatus.FINISH;
                    liVoucherFlowStep.flowUserCode = LiContexts.LiContext.userInfo.userCode;
                    liVoucherFlowStep.flowUserName = LiContexts.LiContext.userInfo.userName;
                    liVoucherFlowStep.flowContent = content;
                    liVoucherFlowStep.flowApprovalType = ApprovalType.Disagree;
                    liVoucherFlowStep.flowDate = DateTime.Now;


                    liVoucherFlowTemp.datas.Add(liVoucherFlowStep);
                    LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).updateEntity(liVoucherFlowTemp);
                    if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                    {
                        resultContent = "已撤销";
                        return true;
                    }
                    else
                    {
                        resultContent = "撤销失败！";
                        return false;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// 开始流程
        /// </summary>
        public static bool startFlow(string entityKey, string voucherId, string voucherCode, Dictionary<string, object> formDataDict, out string resultContent)
        {
            resultContent = "";

            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("entityKey", entityKey);
            whereDict.Add("voucherId", voucherId);
            whereDict.Add("flowStatus", FlowStatus.RUN);

            LiVoucherFlowModel liVoucherFlowTemp = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).getEntitySingle<LiVoucherFlowModel>(whereDict);
            if (liVoucherFlowTemp == null)
            {

                List<LiVersionFlowModel> launchVersionFlowList = FlowUtil.launchFlow(entityKey);
                if (launchVersionFlowList.Count > 0)
                {
                    foreach (LiVersionFlowModel liVersionFlowModel in launchVersionFlowList)
                    {
                        LiVoucherFlowModel liVoucherFlow = new LiVoucherFlowModel();
                        liVoucherFlow.flowVersionNumber = liVersionFlowModel.flowVersionNumber;
                        liVoucherFlow.flowVersionId = liVersionFlowModel.id;
                        liVoucherFlow.flowCode = liVersionFlowModel.flowCode;
                        liVoucherFlow.flowName = liVersionFlowModel.flowName;
                        liVoucherFlow.entityKey = liVersionFlowModel.entityKey;
                        liVoucherFlow.entityName = liVersionFlowModel.entityName;
                        liVoucherFlow.voucherId = Convert.ToString(voucherId);
                        liVoucherFlow.voucherCode = Convert.ToString(voucherCode);
                        liVoucherFlow.flowStatus = "正在运行";
                        liVoucherFlow.flowTitle = string.Format("单据编号【{0}】需要审核", voucherCode);
                        liVoucherFlow.flowStartDate = DateTime.Now;
                        liVoucherFlow.datas = new List<LiVoucherFlowStepModel>();

                        //第一步
                        LiVersionFlowNodeModel liVersionFlowNodeModel = liVersionFlowModel.nodes.Where(m => m.flowNodeType == FlowNodeType.STARTELEMENT).FirstOrDefault();

                        LiVoucherFlowStepModel liVoucherFlowStep = new LiVoucherFlowStepModel();
                        liVoucherFlowStep.flowSeq = 1;
                        liVoucherFlowStep.flowStatus = FlowStatus.FINISH;
                        liVoucherFlowStep.flowUserCode = LiContexts.LiContext.userInfo.userCode;
                        liVoucherFlowStep.flowUserName = LiContexts.LiContext.userInfo.userName;
                        liVoucherFlowStep.flowContent = liVersionFlowNodeModel.flowNodeName;
                        liVoucherFlowStep.flowDate = DateTime.Now;
                        liVoucherFlowStep.flowVersionNodeId = liVersionFlowNodeModel.id;

                        LiVersionFlowConnectorModel liVersionFlowConnectorModel = liVersionFlowNodeModel.connectors[0];
                        //第二步
                        liVersionFlowNodeModel = liVersionFlowModel.nodes.Where(m => m.flowNodeCode == liVersionFlowConnectorModel.flowNodeCodeTo).FirstOrDefault();
                        liVoucherFlowStep.flowVersionNextStepNodeId = liVersionFlowNodeModel.id;

                        liVoucherFlow.datas.Add(liVoucherFlowStep);

                        LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).newEntity(liVoucherFlow);
                        if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).bSuccess)
                        {
                            List<MessageModel> messageList = new List<MessageModel>();
                            foreach (LiVersionFlowUserModel liVersionFlowUserModelTemp in liVersionFlowNodeModel.users)
                            {
                                
                                MessageModel messageModel = new MessageModel();
                                messageModel.messageType = MessageType.Flow;
                                messageModel.messageContent = replaceMessagePlaceholder(liVersionFlowNodeModel.flowNodeInformation, formDataDict);
                                messageModel.messageDate = DateTime.Now;
                                messageModel.flowVersionId = liVersionFlowModel.id;
                                messageModel.flowVersionNumber = liVersionFlowModel.flowVersionNumber;
                                messageModel.flowCode = liVersionFlowModel.flowCode;
                                messageModel.flowName = liVersionFlowModel.flowName;
                                messageModel.entityKey = liVersionFlowModel.entityKey;
                                messageModel.entityName = liVersionFlowModel.entityName;
                                messageModel.voucherId = Convert.ToString(voucherId);
                                messageModel.voucherCode = Convert.ToString(voucherCode);
                                messageModel.userCode = liVersionFlowUserModelTemp.userCode;

                                messageList.Add(messageModel);
                            }

                            LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).batchNewEntity(messageList);
                            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).bSuccess)
                            {
                                resultContent = "已发起流程！消息已发送";
                            }
                            else
                            {
                                resultContent = "已发起流程！消息发送失败";
                            }

                            return true;
                        }


                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                resultContent = "流程已发起！";
                return false;
            }

            return false;
        }

        /// <summary>
        /// 获取发起流程
        /// </summary>
        /// <returns></returns>
        public static List<LiVersionFlowModel> launchFlow(string entityKey)
        {
            List<LiVersionFlowModel> launchVersionFlowList = new List<LiVersionFlowModel>();

            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("openStatus", "开启");
            whereDict.Add("entityKey", entityKey);

            List<LiVersionFlowModel> lists = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).getEntityList<LiVersionFlowModel>(whereDict);

            foreach (LiVersionFlowModel liVersionFlowModel in lists)
            {
                //不能限制用户来获取流程
                LiVersionFlowNodeModel liVersionFlowNodeModel = liVersionFlowModel.nodes.Where(m => m.flowNodeType == "StartElement").FirstOrDefault();
                if (liVersionFlowNodeModel != null)
                {
                    LiVersionFlowConnectorModel liVersionFlowConnectorModel = liVersionFlowNodeModel.connectors[0];
                    if (liVersionFlowConnectorModel.conditions.Count <= 0)
                    {
                        launchVersionFlowList.Add(liVersionFlowModel);
                    }
                    else
                    {
                        string queryWhereStr = CommonUtil.getFlowWhereStr(liVersionFlowConnectorModel.conditions, true);

                        Dictionary<string, object> paramDict = new Dictionary<string, object>();
                        paramDict.Clear();
                        paramDict.Add("childTableName", "");
                        paramDict.Add("entityKey", entityKey);
                        paramDict.Add("whereSql", queryWhereStr);
                        paramDict.Add("orderBySql", "");

                        DataTable dt = LiContexts.LiContext.getHttpEntity("sp_QueryList").execProcedure_DataTable( paramDict);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            launchVersionFlowList.Add(liVersionFlowModel);
                        }
                    }

                }
            }

            return launchVersionFlowList;
        }


        public static ShapeBase NewShape(string shapeTypeStr, UI.FlowDesignControl graphControl1)
        {
            ShapeTypes shapeType = ShapeTypes.Node;
            switch (shapeTypeStr)
            {
                case "StartElement":
                    shapeType = ShapeTypes.Start;
                    break;
                case "EndElement":
                    shapeType = ShapeTypes.End;
                    break;
                case "NodeElement":
                    shapeType = ShapeTypes.Node;
                    break;
                case "ConditionElement":
                    shapeType = ShapeTypes.Condition;
                    break;
            }
            return graphControl1.NewShape(shapeType);
        }
    }
}
