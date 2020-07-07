using LiCommon.LiException;
using LiHttp;
using LiModel.Basic;
using LiModel.LiConvert;
using LiModel.LiEnum;
using LiModel.LiTable;
using LiVoucherConvert.Model;
using LiVoucherConvert.Service;
using LiVoucherConvert.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert
{
    public class LiVoucherConvertUtil
    {
        /// <summary>
        /// 单据转换
        /// </summary>
        public static VoucherConvertContext voucherConvertContext = new VoucherConvertContext();

        /// <summary>
        /// 获取单据转换
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static AVoucherConvert getVoucherConvert(string convertType, string convertCode)
        {
            string keyName = string.Format("{0}_{1}", convertType, convertCode);
            AVoucherConvert voucherConvert = voucherConvertContext.get(keyName);
            if (voucherConvert == null)
            {
                switch (convertType)
                {
                    case ConvertDestTypeModel.System:
                        voucherConvert = new LiSystemConvert();
                        voucherConvertContext.put(keyName, voucherConvert);
                        break;
                    case ConvertDestTypeModel.U8:
                        voucherConvert = new U8VoucherConvert();
                        voucherConvertContext.put(keyName, voucherConvert);
                        break;
                }
            }

            return voucherConvert;
        }

        /// <summary>
        /// 下推单据
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static LiReponseModel pushVoucher(string convertType, string convertCode)
        {
            AVoucherConvert voucherConvert = getVoucherConvert(convertType, convertCode);
            if (voucherConvert == null) return LiReponseModel.getInstance();
            return voucherConvert.pushVoucher();
        }

        /// <summary>
        /// 反写数据
        /// </summary>
        /// <param name="liConvertHeadModel"></param>
        /// <param name="sourceTableModel"></param>
        public static void reverseData( string IDValue, LiConvertHeadModel liConvertHeadModel, List<TableModel> sourceTables, bool bDelete = false)
        {
            string sqlStr = string.Empty;
            string sourceIDField = string.Empty;
            string sourcePrimaryKeyField = string.Empty;
            LiConvertBodyModel liConvertBodyID = liConvertHeadModel.datas.Where(m => m.bCumulativeRelationID == true).FirstOrDefault();
            TableModel headTableModel = sourceTables.Where(m => m.entityOrder == EntityOrderType.Master).FirstOrDefault();
            TableModel sourceTableModel = sourceTables.Where(m => m.tableName == liConvertBodyID.convertDestType).FirstOrDefault();
            //外键
            if (sourceTableModel.entityOrder == EntityOrder.MASTER)
            {
                sourceIDField = sourceTableModel.keyName;
            }
            else
            {
                ColumnModel column = sourceTableModel.datas.Where(m => m.foreignKey == true).FirstOrDefault();
                sourceIDField = column.columnName;
            }
            //主键
            sourcePrimaryKeyField = sourceTableModel.keyName;

            switch (liConvertHeadModel.convertRelation)
            {
                case ConvertRelation.PUSHCUMULATIVE:
                    LiConvertBodyModel liConvertBodyQty = liConvertHeadModel.datas.Where(m => m.bCumulativeRelationQty == true).FirstOrDefault();
                    if (!bDelete)
                    {
                        sqlStr = string.Format(" update {0}.dbo.{1} set {2}= ISNULL({2},0) + A.{3} from ( select SUM(ISNULL({3},0)) {3},{7} from {4}.dbo.{5} where {8}=''{9}'' group by {7}  ) A where {0}.dbo.{1}.{6} = A.{7} "
                                            , liConvertHeadModel.convertCumulativeDatabaseName, liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeField
                                                       , liConvertBodyQty.convertDestField, sourceTableModel.dataBaseName, sourceTableModel.tableName
                                                       , liConvertHeadModel.convertCumulativeIDField, liConvertBodyID.convertDestField, sourceIDField, IDValue);
                    }
                    else
                    {
                        sqlStr = string.Format(" update {0}.dbo.{1} set {2}=  A.{3} from ( select SUM(ISNULL({3},0)) {3},{7} from {4}.dbo.{5} where {10}=''{9}'' group by {7}  ) A where {0}.dbo.{1}.{6} = A.{7} "
                                            , liConvertHeadModel.convertCumulativeDatabaseName, liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeField
                                                       , liConvertBodyQty.convertDestField, sourceTableModel.dataBaseName, sourceTableModel.tableName
                                                       , liConvertHeadModel.convertCumulativeIDField, liConvertBodyID.convertDestField, sourceIDField
                                                       , liConvertHeadModel.convertCode, sourceTableModel.entityOrder == EntityOrderType.Master ? "hConvertCode" : "bConvertCode");

                        sqlStr += string.Format("  update {0}.dbo.{1} set {2}= NULL where {6} not in (select {7} from {4}.dbo.{5} where {10}=''{9}'' )  ",
                                                   liConvertHeadModel.convertCumulativeDatabaseName, liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertRelation == ConvertRelation.ONE ? liConvertHeadModel.convertCumulativeTextField : liConvertHeadModel.convertCumulativeField
                                                   , sourceTableModel.keyName, sourceTableModel.dataBaseName, sourceTableModel.tableName
                                                   , liConvertHeadModel.convertCumulativeIDField, liConvertBodyID.convertDestField, sourcePrimaryKeyField
                                                   , liConvertHeadModel.convertCode, sourceTableModel.entityOrder == EntityOrderType.Master ? "hConvertCode" : "bConvertCode");
                    }
                    break;
                case ConvertRelation.ONE:
                    if (!bDelete)
                    {
                        sqlStr = string.Format(" update {0}.dbo.{1} set {2}= A.{3} from ( select {3},{7} from {4}.dbo.{5} where {8}=''{9}'') A  where {0}.dbo.{1}.{6} = A.{7}  ",
                                                   liConvertHeadModel.convertCumulativeDatabaseName, liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeTextField
                                                   , sourceTableModel.keyName, sourceTableModel.dataBaseName, sourceTableModel.tableName
                                                   , liConvertHeadModel.convertCumulativeIDField, liConvertBodyID.convertDestField, sourceIDField, IDValue);
                    }
                    else
                    {
                        sqlStr = string.Format("  update {0}.dbo.{1} set {2}= NULL where {6} not in (select {7} from {4}.dbo.{5} where {8} in (select {2} from {0}.dbo.{1} where {2} is not null) )  ",
                                                   liConvertHeadModel.convertCumulativeDatabaseName, liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertRelation == ConvertRelation.ONE ? liConvertHeadModel.convertCumulativeTextField : liConvertHeadModel.convertCumulativeField
                                                   , sourceTableModel.keyName, sourceTableModel.dataBaseName, sourceTableModel.tableName
                                                   , liConvertHeadModel.convertCumulativeIDField, liConvertBodyID.convertDestField, sourcePrimaryKeyField
                                                   , IDValue);
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(sqlStr))
            {
                Dictionary<string, object> paramDict = new Dictionary<string, object>();
                paramDict.Add("execSql", sqlStr);

                LiHttpUtil.getHttpEntity("sp_ExecSql").execProcedureNoResult(paramDict);
            }
        }

        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <param name="liConvertHeadModel"></param>
        /// <param name="tableModelList"></param>
        /// <returns></returns>
        public static string getFiliterSQL(LiConvertHeadModel liConvertHeadModel, List<TableModel> tableModelList)
        {
            string filiterSqlStr = string.Empty;
            if (liConvertHeadModel.bSourceTableFiliter)
            {
                LiConvertBodyModel liConvertBodyID = liConvertHeadModel.datas.Where(m => m.bCumulativeRelationID == true).FirstOrDefault();
                if (liConvertBodyID == null) throw new LiVoucherConvertException("累计关联ID字段为空");
                TableModel sourceTableModel = tableModelList.Where(m => m.tableName == liConvertBodyID.convertDestType).FirstOrDefault();

                switch (liConvertHeadModel.convertRelation)
                {
                    case ConvertRelation.PUSHCUMULATIVE:
                        filiterSqlStr = string.Format(" and ISNULL(Li{0}_{1},0) > ISNULL(Li{2}_{3},0) "
                                , liConvertHeadModel.convertPushTableName, liConvertHeadModel.convertPushField
                                , liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeField);
                        break;
                    case ConvertRelation.ONE:
                        filiterSqlStr = string.Format(" and ISNULL(Li{0}_{1},'''') = '''' ", liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeTextField);
                        break;
                }
            }
            return filiterSqlStr;
        }
    }
}
