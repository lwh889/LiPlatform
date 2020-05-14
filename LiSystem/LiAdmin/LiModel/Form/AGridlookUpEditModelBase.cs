using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.Interface;

namespace LiModel.Form
{
    public abstract class AGridlookUpEditModelBase 
    {
        /// <summary>
        /// 数据源
        /// </summary>
        /// 
        [JsonIgnore]  
        private object dataSource { set; get; }

        private List<string> _searchColumns = new List<string>();
        /// <summary>
        /// 搜索列
        /// </summary>
        /// 
        [JsonIgnore] 
        public List<string> SearchColumns { set { _searchColumns = value; } get { return _searchColumns;} }

        protected List<string> _displayColumns = new List<string>();
        /// <summary>
        /// 显示列
        /// </summary>
        /// 
        [JsonIgnore] 
        public List<string> DisplayColumns { set { _displayColumns = value; } get { return _displayColumns; } }

        private Dictionary<string, string> _dictModelDesc = new Dictionary<string, string>();
        /// <summary>
        /// 列名映射
        /// </summary>
        /// 
        [JsonIgnore] 
        public Dictionary<string, string> DictModelDesc { set { _dictModelDesc = value; } get { return _dictModelDesc; } }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public abstract TEntity getInstance<TEntity>() where TEntity : new();

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual TEntity getDataSource<TEntity>() where TEntity:new()
        {
            return (TEntity)dataSource;
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="dataSource"></param>
        public void setDataSource(object dataSource)
        {
            this.dataSource= dataSource;
        }


        public virtual List<string> getSearchColumns()
        {
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public virtual List<string> getDisplayColumns()
        {
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public virtual Dictionary<string, string> getDictModelDesc()
        {
            return DictModelDesc;
        }

        public virtual string getValueMember()
        {
            return string.Empty;
        }

        public virtual string getDisplayMember()
        {
            return string.Empty;
        }

        public virtual object getSingleByDataSource(string keyValue)
        {
            return null;
        }
    }
}
