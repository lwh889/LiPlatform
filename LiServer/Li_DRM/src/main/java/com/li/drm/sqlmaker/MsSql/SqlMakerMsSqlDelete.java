package com.li.drm.sqlmaker.MsSql;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.enumli.Relationship;
import com.li.drm.model.JsonModel;
import com.li.drm.sqlmaker.IDelete;
import com.li.drm.util.ClassUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.Assert;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * MsServer方式生成删除SQL
 */
public class SqlMakerMsSqlDelete extends SqlMakerMsSql implements IDelete {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlDelete.class);

    public Object keyValue = null;
    public Map<String, List<Object>> childKeyValueMap = new HashMap<>();
    public SqlMakerMsSqlDelete(EntityInfo entityInfo){
        super(entityInfo);
    }

    /**
     * 插入Json数据值
     * @param jsonModel
     * @return
     */
    public boolean deleteByJsonModel(JsonModel jsonModel){
        Assert.notNull(jsonModel, "删除对象不能为 NULL");

        TableInfo tableInfo = entityInfo.getTableInfo();

        //适合所有相应的字段名信息
        Map<String, FieldInfo> fieldMapper =tableInfo.getFieldMap();
        //适合子表相应的字段名信息
        Map<String,TableInfo> tableInfoMap =tableInfo.getTableInfoMap();

        for (Map.Entry<String, FieldInfo> entry : fieldMapper.entrySet()) {
            FieldInfo fieldInfo = entry.getValue();

            //获取主键值
            if(fieldInfo.getIsPrimaryKey()){
                keyValue = jsonModel.values.get(entry.getKey());
            }

            if(tableInfoMap.containsKey(entry.getKey())){
                TableInfo childTableInfo = tableInfoMap.get(entry.getKey());
                if(fieldInfo.getRelationship() == Relationship.Many){
                    getChildKeys(entry.getKey(), childKeyValueMap, jsonModel, childTableInfo);
                }
            }
        }

        return true;
    }

    /**
     * 获取子表的主键值
     * @param key 字段Key
     * @param childKeyValueMap 保存子表主键
     * @param jsonModel 删除数据
     * @param childTableInfo 主表的子表信息
     * @return
     */
    public void getChildKeys(String key,Map<String, List<Object>> childKeyValueMap, JsonModel jsonModel, TableInfo childTableInfo){
        if(childTableInfo.getTableInfoMap().size()>0){
            List<Object> childKeys = new ArrayList<>();
            List<JsonModel> childValues = jsonModel.relationValues.get(key);
            for (JsonModel childValue : childValues) {
                childKeys.add(childValue.values.get(childTableInfo.getKeyName()));
                for (Map.Entry<String, TableInfo> grandsonTableInfoValue : childTableInfo.getTableInfoMap().entrySet()) {
                    getChildKeys(grandsonTableInfoValue.getKey(), childKeyValueMap, childValue, grandsonTableInfoValue.getValue());
                }
            }
            childKeyValueMap.put(key, childKeys);
        }
    }
    /**
     * 插入实体值
     * @param entity
     * @return
     */
    public boolean delete(Object entity){
        Assert.notNull(entity, "删除对象不能为 NULL");

        TableInfo tableInfo = entityInfo.getTableInfo();

        //适合所有相应的字段名信息
        Map<String, FieldInfo> fieldMapper =tableInfo.getFieldMap();
        //适合子表相应的字段名信息
        Map<String,TableInfo> tableInfoMap =tableInfo.getTableInfoMap();

        for (Map.Entry<String, FieldInfo> entry : fieldMapper.entrySet()) {
            FieldInfo fieldInfo = entry.getValue();

            //获取主键值
            if(fieldInfo.getIsPrimaryKey()){
                keyValue = ClassUtils.getValue(entity, entry.getKey());
            }

            //获取子表的主键值
            //if(tableInfoMap.containsKey(entry.getKey())){
            //    if(fieldInfo.getRelationship() == Relationship.Many){
            //        foreignKeyMap.put(entry.getKey(), keyValue);
            //    }
            //}
        }

        return true;
    }
    private void makerDeleteSql(String key, StringBuilder builder, Map<String, TableInfo> tableInfoMap,TableInfo tableInfo){
        if(tableInfoMap.size()>0){
            for (Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()) {
                TableInfo childTableInfo = entry.getValue();
                makerDeleteSql(entry.getKey(), builder, childTableInfo.getTableInfoMap() ,childTableInfo);
                List<Object> childKeyValues = childKeyValueMap.get(key);
                if(childKeyValues.size()>0){
                    builder.append(" DELETE ").append(tableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(" WHERE ").append( childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName())).append(" in (");
                    for (Object value : childKeyValues){
                        builder.append(getColumnValueFormat(value)).append(",");
                    }
                    builder.deleteCharAt(builder.length()-1);
                    builder.append(")");
                }

            }
        }

    }

    @Override
    public String makeSql() {
        StringBuilder builder = new StringBuilder();

        TableInfo tableInfo = entityInfo.getTableInfo();
        Map<String, TableInfo> tableInfoMap = tableInfo.getTableInfoMap();

        for(Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()){
            TableInfo childTableInfo = entry.getValue();
            makerDeleteSql(entry.getKey(), builder, childTableInfo.getTableInfoMap(), childTableInfo);

            builder.append(" DELETE ").append(tableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(" WHERE ").append( childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName())).append(" = ").append(getColumnValueFormat(keyValue));
        }
        builder.append(" DELETE ").append(tableInfo.getDataBaseName()).append(".dbo.").append(tableInfo.getTableName()).append(" WHERE ").append(tableInfo.getKeyName()).append(" = ").append(getColumnValueFormat(keyValue));

        sql = builder.toString();
        logger.info(sql);
        return sql;
    }
}
