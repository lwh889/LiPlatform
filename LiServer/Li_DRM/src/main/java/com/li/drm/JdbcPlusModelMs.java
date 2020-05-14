package com.li.drm;

import com.li.drm.entityinfo.*;
import com.li.drm.enumli.Relationship;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlQuery;
import com.li.drm.util.ClassUtils;
import com.li.drm.util.SqlMakerUtilsMs;
import org.springframework.dao.support.DataAccessUtils;
import org.springframework.jdbc.core.JdbcTemplate;

import java.util.List;
import java.util.Map;

public class JdbcPlusModelMs extends JdbcPlusModel implements IJdbcPlusModel {

    private static JdbcPlusModelMs jdbcPlusModelMs = null;
    public JdbcPlusModelMs(JdbcTemplate jdbcTemplate){
        super(jdbcTemplate);
        sqlMakerUtils = new SqlMakerUtilsMs();
    }

    /**
     * 获取单实例
     * @param jdbcTemplate
     * @return
     */
    public static JdbcPlusModelMs getInstance(JdbcTemplate jdbcTemplate){
        if(jdbcPlusModelMs == null){
            jdbcPlusModelMs = new JdbcPlusModelMs(jdbcTemplate);
        }
        return jdbcPlusModelMs;
    }
    @Override
    public  <T>List<T> queryBy(Class<T> entityClass, String columnName, Object columnValue){
        return queryBy(entityClass, WhereFactory.getInstance(sqlMakerUtils).equal(columnName, columnValue));
    }

    @Override
    public <T>List<T> queryBy(Class<T> entityClass, Where... wheres){
        EntityInfo entityInfo = new ModelEntityInfo();
        entityInfo.setSourceObject(entityClass);

        entityInfo.analysis();

        SqlMakerMsSqlQuery queryMaker = new SqlMakerMsSqlQuery(entityInfo);
        queryMaker.where(wheres);
        List<T> list = queryBy(queryMaker);

        TableInfo tableInfo = entityInfo.tableInfo;
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();
        Map<String, FieldInfo> relationFieldMap = entityInfo.tableInfo.getRelationFieldMap();
        if(tableInfoMap != null && tableInfoMap.size()>0) {
            for (T value : list) {
                for(Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()){
                    TableInfo childTableInfo = entry.getValue();
                    Class clz = ClassUtils.forName(childTableInfo.getClassName());

                    FieldInfo fieldInfo = relationFieldMap.get(entry.getKey());
                    List childList = queryBy(clz, WhereFactory.getInstance(sqlMakerUtils).equal(childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), ClassUtils.getValue(value,tableInfo.getKeyName()) ));

                    if(fieldInfo.getRelationship() == Relationship.Many){
                        ClassUtils.setValue(value, entry.getKey(), childList);
                    }
                    else if(fieldInfo.getRelationship() == Relationship.One){
                        ClassUtils.setValue(value, entry.getKey(), DataAccessUtils.requiredSingleResult(childList));
                    }
                }
            }
        }

        return list;
    }
}
