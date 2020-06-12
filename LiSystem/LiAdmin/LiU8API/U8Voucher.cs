//声明命名空间
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

//需要添加以下命名空间
using UFIDA.U8.MomServiceCommon;
using UFIDA.U8.U8MOMAPIFramework;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8APIFramework.Meta;
using UFIDA.U8.U8APIFramework.Parameter;
using MSXML2;
using LiU8API.Model;
using System.Linq;
using LiU8API.LiEnum;
using LiU8API.Util;

namespace LiU8API
{
    public class U8Voucher
    {
        private U8Login.clsLogin u8Login;
        private U8EnvContext envContext;
        private LiU8VoucherModel liU8VoucherModel;
        private LiU8OperationModel liU8OperationModel;
        private string operationType;

        private BusinessObject domHead;
        private BusinessObject domBody;
        private string domHeadFieldName = "domHead";
        private string domBodyFieldName = "domBody";

        private string IDFieldName = "";
        private string CodeFieldName = "";

        private U8ApiBroker broker;

        public U8Voucher(string operationType, LiU8VoucherModel liU8VoucherModel)
        {
            //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
            //如果当前环境中有login对象则可以省去第一步
            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = "(default)@999";
            String sYear = "2015";
            String sUserID = "demo";
            String sPassword = "DEMO";
            String sDate = "2015-05-21";
            String sServer = "localhost";
            String sSerial = "";
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                Marshal.FinalReleaseComObject(u8Login);
                return;
            }

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            this.u8Login = u8Login;
            this.liU8VoucherModel = liU8VoucherModel;
            this.operationType = operationType;
            liU8OperationModel = this.liU8VoucherModel.operations.Where(m => m.operationCode == operationType).FirstOrDefault();
            initParam();
        }

        public U8Voucher(string operationType, LiU8VoucherModel liU8VoucherModel, U8Login.clsLogin u8Login )
        {
            //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
            //如果当前环境中有login对象则可以省去第一步

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            this.u8Login = u8Login;
            this.liU8VoucherModel = liU8VoucherModel;
            this.operationType = operationType;
            liU8OperationModel = this.liU8VoucherModel.operations.Where(m => m.operationCode == operationType).FirstOrDefault();
            initParam();
        }

        private void initParam()
        {
            //获取表单的数据区域
            LiU8ParamModel liU8ParamModel = liU8OperationModel.paramModels.Where(m => m.paramBoType == "1").FirstOrDefault();
            if(liU8ParamModel != null)
            {
                domHeadFieldName = liU8ParamModel.paramName;
            }
            liU8ParamModel = liU8OperationModel.paramModels.Where(m => m.paramBoType == "0").FirstOrDefault();
            if (liU8ParamModel != null)
            {
                domBodyFieldName = liU8ParamModel.paramName;
            }

            //设置上下文参数
            SetApiContext("VoucherType", Convert.ToInt32(liU8VoucherModel.voucherType));
            if(liU8OperationModel.contexts != null)
            {
                foreach (LiU8EnvContextModel liU8EnvContext in liU8OperationModel.contexts)
                {
                    SetApiContext(liU8EnvContext.contextName, DataTypeUtil.convertType(liU8EnvContext.contextType, liU8EnvContext.contextDefaultValue));

                }
            }

            
            //第三步：设置API地址标识(Url)
            //当前API：新增或修改的地址标识为：U8API/SaleOrder/Save
            U8ApiAddress myApiAddress = new U8ApiAddress(string.Format("U8API/{0}/{1}", liU8VoucherModel.code, liU8OperationModel.operationSymbol));
            //第四步：构造APIBroker
            broker = new U8ApiBroker(myApiAddress, envContext);

            //设置Broker参数
            List<LiU8ParamModel> liU8Params = liU8OperationModel.paramModels.Where(m => string.IsNullOrWhiteSpace(m.paramBoType)).ToList();
            foreach(LiU8ParamModel liU8Param in liU8Params)
            {
                //判断ID的字段名
                if (liU8Param.parambID)
                {
                    IDFieldName = liU8Param.paramName;
                }
                //判断Code的字段名
                if (liU8Param.parambCode)
                {
                    CodeFieldName = liU8Param.paramName;
                }

                if (liU8Param.paramDirection == "in" || liU8Param.paramDirection == "inout")
                {
                    if(liU8Param.paramType == "MSXML2.IXMLDOMDocument2")
                    {
                        broker.AssignNormalValue(liU8Param.paramName, DataTypeUtil.convertType(liU8Param.paramType, liU8Param.paramDefaultValue) as MSXML2.IXMLDOMDocument2);
                    }
                    else
                    {
                        broker.AssignNormalValue(liU8Param.paramName, DataTypeUtil.convertType(liU8Param.paramType, liU8Param.paramDefaultValue));
                    }
                }
                else
                {
                    broker.AssignNormalValue(liU8Param.paramName, DataTypeUtil.getDefaultValue(liU8Param.paramType));
                }
            }


        }

        /// <summary>
        /// 初始化表
        /// </summary>
        /// <param name="iRow"></param>
        public void initDom(int iRow)
        {
            initDomHead();
            initDomBody(iRow);
        }
        /// <summary>
        /// 初始化表头
        /// </summary>
        public void initDomHead()
        {
            if(domHead == null)
            {
                domHead = broker.GetBoParam(domHeadFieldName);
                domHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            }

        }

        /// <summary>
        /// 初始化表体
        /// </summary>
        /// <param name="iRow">一共有多少行</param>
        public void initDomBody(int iRow)
        {
            if (domBody == null)
            {
                domBody = broker.GetBoParam(domBodyFieldName);
                domBody.RowCount = iRow; //设置BO对象(表头)行数，只能为一行
                //domBody
                //MSXML2.DOMDocument SourceDom = domBody as MSXML2.DOMDocument;
                //IXMLDOMNodeList ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
                //foreach (IXMLDOMElement body in ndbodylist)
                //{
                //    if (body.attributes.getNamedItem("cfactorycode") == null)
                //        //  '若没有当前元素，就增加当前元素
                //        body.setAttribute("cfactorycode", "");
                //}
            }
        }

        /// <summary>
        /// 设置表头值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public void setDomHeadValue(string fieldName, object fieldValue)
        {
            domHead[0][fieldName] = fieldValue; //主关键字段，int类型
        }

        /// <summary>
        /// 设置表体值
        /// </summary>
        /// <param name="iRow">行号 ，从0开始</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public void setDomBodyValue(int iRow, string fieldName, object fieldValue)
        {
            domBody[iRow][fieldName] = fieldValue; //主关键字段，int类型
        }

        /// <summary>
        /// 设置上下文参数
        /// </summary>
        /// <param name="key">参数Key</param>
        /// <param name="value">参数值</param>
        public void SetApiContext(string key, object value)
        {
            //设置上下文参数
            envContext.SetApiContext(key, value); //上下文数据类型：int，含义：单据类型：12

        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns>返回U8APIReponse</returns>
        public U8APIReponse commit()
        {
            U8APIReponse u8APIReponse = new U8APIReponse() { bSuccess = false };
            if (!broker.Invoke())
            {
                //错误处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        Console.WriteLine("系统异常：" + sysEx.Message);
                        u8APIReponse.resultContent = "系统异常：" + sysEx.Message;
                        u8APIReponse.apiEx = apiEx;
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        Console.WriteLine("API异常：" + bizEx.Message);
                        u8APIReponse.resultContent = "API异常：" + bizEx.Message;
                        u8APIReponse.apiEx = apiEx;
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        Console.WriteLine("异常原因：" + exReason);
                        u8APIReponse.resultContent = "异常原因：" + exReason;
                    }

                    return u8APIReponse;
                }
                return u8APIReponse;
            }

            u8APIReponse.bSuccess = true;
            //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示成功为空串
            u8APIReponse.resultContent = broker.GetReturnValue() as System.String;

            if (!string.IsNullOrEmpty(IDFieldName))
            {
                //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
                u8APIReponse.voucherID = broker.GetResult(IDFieldName) as string;
            }

            if (!string.IsNullOrEmpty(CodeFieldName))
            {
                //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
                u8APIReponse.voucherCode = broker.GetResult(CodeFieldName) as string;
            }

            //获取普通INOUT参数xmlmsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            //this.errMsgRet = broker.GetResult("xmlmsg") as System.String;

            return u8APIReponse;

        }

        ~U8Voucher() {
            if(broker != null)
            {
                broker.Release();
            }
        }
    }
}
