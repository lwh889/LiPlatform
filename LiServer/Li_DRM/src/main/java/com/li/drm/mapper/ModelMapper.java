package com.li.drm.mapper;

import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.util.ClassUtils;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;

/**
 * 类模型映射
 * @param <T>
 */
public class ModelMapper<T> implements RowMapper<T> {
    private Class<T> tableClass;
    private TableInfo tableInfo;
    public ModelMapper(Class<T> tableClass, TableInfo tableInfo){
        this.tableClass = tableClass;
        this.tableInfo = tableInfo;
    }
    // 查询的时候，有可能会返回多个数据，所有的数据都会放在 rs 结果集中
    // rounum 代表的是记录的下表值
    public T mapRow(ResultSet resultSet, int i) throws SQLException {
        Map<String, FieldInfo> columnFieldMap = tableInfo.getColumnFieldMap();
        Object instance = ClassUtils.getInstance(tableClass);
        for (Map.Entry<String, FieldInfo> entry : columnFieldMap.entrySet()) {
            Object value = resultSet.getObject(entry.getKey());
            ClassUtils.setValue(instance, entry.getKey(), value);
        }
        return (T) instance;
    }
}
