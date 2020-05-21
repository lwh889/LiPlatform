package com.li.drm.sqlmaker.MsSql;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.enumli.Relationship;
import com.li.drm.model.JsonModel;
import com.li.drm.sqlmaker.IUpdata;
import com.li.drm.util.ClassUtils;
import com.li.drm.util.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.Assert;

import java.util.*;

/**
 * MsServer方式生成更新SQL
 */
public class SqlMakerMsSqlUpdata extends SqlMakerMsSql implements IUpdata {
 /*   //主表数据值
    public Map<String, Object> updateColumnValueMap = new HashMap<>();
    //子表所有值
    public Map<String, List<Map<String, Object>>> updateRelationColumnValueMap = new HashMap<>();*/
    //用于删除不存在的子实体
 //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlUpdata.class);

    public Map<String, List< Object>> updateRelationKeyValueMap = new HashMap<>();

    public JsonModel jsonModel;

    public SqlMakerMsSqlUpdata(EntityInfo entityInfo){
        super(entityInfo);
    }

    //更新Json数据值
    public boolean updateByJsonModel(JsonModel jsonModel){
        isAnalysis = false;

        //转化主表数据值
        this.jsonModel = jsonModel;



        //转化子表数据值，只适合一级关系
        Map<String , TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        getDeleteID(this.jsonModel.relationValues, tableInfoMap, updateRelationKeyValueMap);
        return true;
    }

    private void getDeleteID(Map<String, List<JsonModel>> relationValues, Map<String , TableInfo> tableInfoMap, Map<String, List< Object>> updateRelationKeyValueMap ){
        if(relationValues != null) {
            for (Map.Entry<String, List<JsonModel>> entry : relationValues.entrySet()) {
                TableInfo childTableInfo = tableInfoMap.get(entry.getKey());
                Map<String,FieldInfo> fieldMap = childTableInfo.getFieldMap();

                List<JsonModel> jsonModels = entry.getValue();

                List<Object> keyValues = new ArrayList<>();
                for(JsonModel model : jsonModels){
                    Map<String, Object> childMap = model.values;

                    for (Map.Entry<String, Object> childEntry : childMap.entrySet()) {
                        FieldInfo childFieldInfo = fieldMap.get(childEntry.getKey());
                        if(childFieldInfo.getIsPrimaryKey()){
                            keyValues.add(childEntry.getValue());
                        }
                    }

                    if(model.relationValues.size()>0){
                        getDeleteID(model.relationValues,childTableInfo.getTableInfoMap(),updateRelationKeyValueMap);
                    }
                }

                //子表所存在的主键值，用于删除已经删除的行
                if(updateRelationKeyValueMap.containsKey(entry.getKey())){
                    updateRelationKeyValueMap.get(entry.getKey()).addAll(keyValues);
                }
                else{
                    updateRelationKeyValueMap.put(entry.getKey(), keyValues);
                }
            }
        }
    }

    /**
     * 插入实体值
     * @param entity
     * @return
     */
    public boolean update(Object entity){
        Assert.notNull(entity, "更新对象不能为 NULL");
        TableInfo tableInfo = entityInfo.getTableInfo();
        //适合所有相应的字段名信息
        Map<String, FieldInfo> fieldMapper =tableInfo.getFieldMap();
        //适合子表相应的字段名信息
        Map<String,TableInfo> tableInfoMap =tableInfo.getTableInfoMap();

        Map<String, Object> updateColumnValueMap = new HashMap<>();
        //子表所有值
        Map<String, List<JsonModel>> updateRelationColumnValueMap = new HashMap<>();

        for (Map.Entry<String, FieldInfo> entry : fieldMapper.entrySet()) {
            FieldInfo fieldInfo = entry.getValue();

            //转化子表数据值
            if(tableInfoMap.containsKey(entry.getKey())){
                if(fieldInfo.getRelationship() == Relationship.Many){
                    List<Object> list = (List<Object>) ClassUtils.getValue(entity, entry.getKey());
                    TableInfo childTableInfo = tableInfoMap.get(entry.getKey());
                    Map<String, FieldInfo> columnFieldMap =childTableInfo.getColumnFieldMap();

                    List<JsonModel> childList = new ArrayList<>();
                    List<Object> keyValues = new ArrayList<>();

                    for(Object o: list){
                        JsonModel childJsonModel = new JsonModel();
                        Map<String, Object> values = new HashMap<>();
                        for(Map.Entry<String, FieldInfo> childEntry : columnFieldMap.entrySet()) {
                            Object childValue = ClassUtils.getValue(o, childEntry.getKey());
                            if(childValue != null && childEntry.getKey().equals(childTableInfo.getKeyName())) keyValues.add(childValue);
                            values.put(childEntry.getKey(), childValue);
                        }
                        childJsonModel.values = values;
                        childList.add(childJsonModel);
                    }
                    updateRelationColumnValueMap.put(entry.getKey(), childList);
                    updateRelationKeyValueMap.put(entry.getKey(), keyValues);
                }
            }
            //否则转化主表数据值
            else{
                Object value = ClassUtils.getValue(entity, entry.getKey());

                updateColumnValueMap.put(entry.getKey(), value);
            }

            jsonModel.values = updateColumnValueMap;
            jsonModel.relationValues = updateRelationColumnValueMap;
        }

        return true;
    }
    @Override
    public String makeSql() {

        TableInfo tableInfo = entityInfo.getTableInfo();

        StringBuilder builder = new StringBuilder();

        Deque<String> deleteSqls = new LinkedList<>();
        Object primarykeyValue = jsonModel.values.get(tableInfo.getKeyName());

        makeDeleteSql(primarykeyValue,null, tableInfo, jsonModel.relationValues, deleteSqls);

        while (deleteSqls.size()>0){
            String deleteSql = deleteSqls.pop();
            if(builder.indexOf(deleteSql) < 0){
                builder.append(deleteSql);
            }
        }

        makerUpdatesSql(builder,null, jsonModel, updateRelationKeyValueMap, null, tableInfo);
        sql = builder.toString();
        logger.info(sql);
        return sql;
    }

    public void makeDeleteSql(Object primaryKeyValue, String limitSelectSql, TableInfo tableInfo,Map<String, List<JsonModel>> jsonModelMap, Deque<String> deleteSqls){
        if(tableInfo == null) return;

        Map<String, TableInfo> tableInfoMap = tableInfo.getTableInfoMap();
        for(Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()){
            TableInfo childTableInfo = entry.getValue();

            StringBuilder builderDelete2 = new StringBuilder();
            if(StringUtils.isNotNull(limitSelectSql)){
                builderDelete2.append(String.format(" DELETE %s.dbo.%s WHERE %s in (  %s   ) "
                        ,childTableInfo.getDataBaseName(), childTableInfo.getTableName(), childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()),limitSelectSql ));
                deleteSqls.push(builderDelete2.toString());
            }

            if(primaryKeyValue == null) return;

            List<JsonModel> jsonModels = jsonModelMap.get(tableInfo.getTableInfoMapKey(childTableInfo, tableInfo.getTableInfoMap()));
            if(jsonModels == null ){
                StringBuilder builderDelete = new StringBuilder();
                StringBuilder buildSelect = new StringBuilder();
                builderDelete.append(String.format(" DELETE %s.dbo.%s WHERE %s = %s  ",childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), getColumnValueFormat(primaryKeyValue) )) ;
                buildSelect.append(String.format(" SELECT %s FROM %s.dbo.%s WHERE %s = %s  ", childTableInfo.getKeyName(),childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), getColumnValueFormat(primaryKeyValue) )) ;
                deleteSqls.push(builderDelete.toString());
                makeDeleteSql(null,buildSelect.toString(), childTableInfo, null, deleteSqls);
            }
            else{

                for(JsonModel jsonModel : jsonModels) {
                    StringBuilder builderDelete = new StringBuilder();
                    StringBuilder buildSelect = new StringBuilder();

                    List<Object> objects = updateRelationKeyValueMap.get(tableInfo.getTableInfoMapKey(childTableInfo, tableInfo.getTableInfoMap()));
                    builderDelete.append(String.format(" DELETE %s.dbo.%s WHERE %s = %s and %s not in (",childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), getColumnValueFormat(primaryKeyValue), childTableInfo.getKeyName())) ;
                    buildSelect.append(String.format(" SELECT %s FROM %s.dbo.%s WHERE %s = %s and %s not in (",childTableInfo.getKeyName(),childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()),getColumnValueFormat(primaryKeyValue), childTableInfo.getKeyName() ));

//null
                    boolean bExistDeleteValue = true;
                    for(Object value : objects){
                        String valueStr = getColumnValueFormat(value);
                        if(StringUtils.isNull(valueStr) || "null".equals(valueStr)) continue;

                        bExistDeleteValue = false;
                        builderDelete.append( valueStr).append(",");
                        buildSelect.append( valueStr).append(",");
                    }

                    if(!bExistDeleteValue){
                        builderDelete.deleteCharAt(builderDelete.length()-1);
                        buildSelect.deleteCharAt(buildSelect.length()-1);
                        builderDelete.append(") ");
                        buildSelect.append(") ");
                    }else{
                        builderDelete.delete(0, builderDelete.length()-1);
                        buildSelect.delete(0, builderDelete.length()-1);
                    }

                    deleteSqls.push(builderDelete.toString());

                    Object keyValue = jsonModel.values.get(tableInfo.getKeyName());
                    makeDeleteSql(keyValue,buildSelect.toString(), childTableInfo, jsonModel.relationValues, deleteSqls);

                    if(childTableInfo.getTableInfoMap().size()<=0) break;
                }
            }
        }
    }
}
