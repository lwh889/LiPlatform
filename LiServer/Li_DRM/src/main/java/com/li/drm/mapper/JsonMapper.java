package com.li.drm.mapper;

import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.model.JsonModel;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

/**
 * Json模型映射解析
 */
public class JsonMapper implements RowMapper<JsonModel> {
    private TableInfo tableInfo;
    public JsonMapper(TableInfo tableInfo){
        this.tableInfo = tableInfo;
    }
    // 查询的时候，有可能会返回多个数据，所有的数据都会放在 rs 结果集中
    // rounum 代表的是记录的下表值
    public JsonModel mapRow(ResultSet resultSet, int i) throws SQLException {
        Map<String, FieldInfo> columnFieldMap = tableInfo.getColumnFieldMap();
        JsonModel instance = new JsonModel();
        instance.values = new HashMap<>();
        for (Map.Entry<String, FieldInfo> entry : columnFieldMap.entrySet()) {
            Object value = resultSet.getObject(entry.getKey());
            instance.values.put(entry.getKey(),value);
            //ClassUtils.setValue(instance, entry.getKey(), value);
        }
        return instance;
    }
}
