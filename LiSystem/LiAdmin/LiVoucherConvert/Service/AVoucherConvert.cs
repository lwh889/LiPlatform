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
        /// <summary>
        /// 转换数据是否有前缀
        /// </summary>
        public bool _isPrefix;
        public bool isPrefix { set { _isPrefix = value; } get { return _isPrefix; } }

        /// <summary>
        /// 转换数据
        /// </summary>
        public List<DataRow> _convertData;
        public List<DataRow> convertData { set { _convertData = value; } get { return _convertData; } }

        /// <summary>
        /// 转换规则
        /// </summary>
        public LiConvertHeadModel _liConvertHead;
        public LiConvertHeadModel liConvertHead { set { _liConvertHead = value; }get { return _liConvertHead; } }

        public abstract LiReponseModel pushVoucher();
    }
}
