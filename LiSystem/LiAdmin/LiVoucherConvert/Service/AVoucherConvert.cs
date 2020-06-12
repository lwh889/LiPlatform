using LiModel.Basic;
using LiModel.LiConvert;
using LiVoucherConvert.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Service
{
    public abstract class AVoucherConvert
    {
        StringBuilder _reverseSql = new StringBuilder();
        /// <summary>
        /// 反写SQL
        /// </summary>
        public StringBuilder reverseSql {  get { return _reverseSql; } }

        List<TableModel> _tableModelList;
        /// <summary>
        /// 所有表信息
        /// </summary>
        public List<TableModel> tableModelList { set { _tableModelList = value; } get { return _tableModelList; } }

        public string _oriVoucherType;
        /// <summary>
        /// 原单据类型
        /// </summary>
        public string oriVoucherType { set { _oriVoucherType = value; } get { return _oriVoucherType; } }

        public bool _isPrefix = true;
        /// <summary>
        /// 转换数据是否有前缀
        /// </summary>
        public bool isPrefix { set { _isPrefix = value; } get { return _isPrefix; } }

        public List<DataRow> _convertData;
        /// <summary>
        /// 转换数据
        /// </summary>
        public List<DataRow> convertData { set { _convertData = value; } get { return _convertData; } }

        public LiConvertHeadModel _liConvertHead;
        /// <summary>
        /// 转换规则
        /// </summary>
        public LiConvertHeadModel liConvertHead
        {
            set {
                _liConvertHead = value;
                isPrefix = _liConvertHead.convertSourceType == ConvertSourceTypeModel.Excel ? false : true;
            }
            get { return _liConvertHead; }
        }

        public abstract LiReponseModel pushVoucher();
    }
}
