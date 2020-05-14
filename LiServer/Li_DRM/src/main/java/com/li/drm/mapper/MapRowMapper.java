package com.li.drm.mapper;

import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.model.JsonModel;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

public class MapRowMapper implements RowMapper<Map<String, Object>>{

    @Override
    public Map<String, Object> mapRow(ResultSet resultSet, int i){
        HashMap<String, Object> map = new HashMap<String,Object>();

        ResultSetMetaData rsmd;
        try {
            rsmd = resultSet.getMetaData();
            int columnCount = rsmd.getColumnCount();
            for(int column = 1;column <= columnCount;column++) {
                String columnName = rsmd.getColumnLabel(column);
                Object columnValue = resultSet.getObject(columnName);
                map.put(columnName, columnValue);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return map;
    }
}
