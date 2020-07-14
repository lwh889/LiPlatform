using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.Util
{
    public class ModelUtil
    {
        /// <summary>
        /// 根据字段名，设置值
        /// </summary>
        /// <param name="attributeName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="dataSource">实体</param>
        public static void setValue<TEntity>(string attributeName, object value, object dataSource) where TEntity : class
        {
            TEntity entity = dataSource as TEntity;

            var t = entity.GetType();
            var properties = t.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name.Equals(attributeName))
                {
                    property.SetValue(entity, value);
                    break;
                }
            }

        }
        /// <summary>
        /// 根据字段名，获取值
        /// </summary>
        /// <param name="attributeName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="dataSource">实体</param>
        public static TReturnType getValue<TEntity, TReturnType>(string attributeName, object dataSource) where TEntity : class
        {
            TEntity entity = dataSource as TEntity;

            var t = entity.GetType();
            var properties = t.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name.Equals(attributeName))
                {
                    return (TReturnType)property.GetValue(entity);
                }
            }

            return default(TReturnType);
        }
    }
}
