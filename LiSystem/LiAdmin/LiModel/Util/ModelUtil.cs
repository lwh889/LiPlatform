using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using LiModel.LiAttribute;

namespace LiModel.Util
{
    public class ModelUtil
    {
        /// <summary>
        /// 深度复制
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TEntity copyEntity<TEntity>(TEntity entity) where TEntity : new()
        {
            var copyEntity = new TEntity();

            var t = copyEntity.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {

                if (property.IsDefined(typeof(NotCopyAttribute), true) ) continue;

                property.SetValue(copyEntity, property.GetValue(entity));

            }

            return copyEntity;


        }

        public static List<TEntity> copyEntitys<TEntity>(List<TEntity> entitys) where TEntity : new()
        {
            List<TEntity> copyEntitys = new List<TEntity>();

            foreach (TEntity entity in entitys)
            {
                copyEntitys.Add(copyEntity<TEntity>(entity));
            }

            return copyEntitys;


        }

        public static void setModelValue<TEntity>(string propertyName, object value, object dataSource)
            where TEntity : class
        {
            if (value == null) return;

            TEntity entity = dataSource as TEntity;
            Type type = entity.GetType();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Equals(propertyName))
                {
                    switch (property.PropertyType.Name)
                    {
                        case "Boolean":
                            property.SetValue(dataSource, Convert.ToBoolean(value));
                            break;
                        case "Int16":
                            property.SetValue(dataSource, Convert.ToInt16(value));
                            break;
                        case "Int32":
                            property.SetValue(dataSource, Convert.ToInt32(value));
                            break;
                        case "Int64":
                            property.SetValue(dataSource, Convert.ToInt64(value));
                            break;
                        case "Decimal":
                            property.SetValue(dataSource, Convert.ToDouble(value));
                            break;
                        case "Float":
                        case "Double":
                            property.SetValue(dataSource, Convert.ToDouble(value));
                            break;
                        case "DateTime":
                            property.SetValue(dataSource, Convert.ToDateTime(value));
                            break;
                        case "Nullable`1":
                            break;
                        case "Image":
                            property.SetValue(dataSource, value);
                            break;
                        default:
                            property.SetValue(dataSource, value);
                            break;
                    }
                }
            }
        }

        public static object getModelValue<TEntity>(string propertyName, object dataSource)
            where TEntity : class
        {

            TEntity entity = dataSource as TEntity;
            Type type = entity.GetType();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Equals(propertyName))
                {
                    return property.GetValue(dataSource);
                }
            }

            return null;
        }
    }
}
