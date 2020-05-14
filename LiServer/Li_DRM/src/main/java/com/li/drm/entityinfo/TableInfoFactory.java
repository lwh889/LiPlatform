package com.li.drm.entityinfo;

import com.li.drm.annotate.*;
import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.enumli.Relationship;
import com.li.drm.model.ColumnModel;
import com.li.drm.model.TableModel;
import com.li.drm.util.EntityUtils;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 表信息工厂
 */
public class TableInfoFactory {


    /**
     * 数据表信息模型解析，用于生成SQL信息
     * @param model 表头模型
     * @param tableModelMap 表体模型
     * @return
     */
    public static TableInfo getTableInfo(TableModel model,Map<String, TableModel> tableModelMap){


        TableInfo tableInfo = new TableInfo();
        tableInfo.setTableModelMap(tableModelMap);

        tableInfo.setTableName(model.getTableName());
        tableInfo.setClassName(model.getClassName());
        tableInfo.setDataBaseName(model.getDataBaseName());
        Map<String,FieldInfo> fieldMap =  new HashMap<>();
        Map<String,FieldInfo> columnFieldMap =  new HashMap<>();
        Map<String,FieldInfo> relationFieldMap =  new HashMap<>();
        Map<String,TableInfo> tableInfoMap = new HashMap<>();
        Map<String,String> foreignKeyNameMap = new HashMap<>();

        List<ColumnModel> columnModels = model.getDatas();

        for(ColumnModel columnModel : columnModels){

            FieldInfo fieldInfo = new FieldInfo();
            fieldInfo.setColumnName(columnModel.getColumnName());
            fieldInfo.setIsPrimaryKey(columnModel.getPrimaryKey());
            fieldInfo.setIsForeignKey(columnModel.getForeignKey());
            fieldInfo.setPrimaryKeyName(columnModel.getPrimaryKeyName());
            fieldInfo.setPrimaryKeyTableName(columnModel.getPrimaryKeyTableName());
            int i = columnModel.getPrimaryKeyDatabaseGenerated();
            fieldInfo.setPrimaryKeyDatabaseGenerated(DatabaseGenerated.getDatabaseGenerated(i));
            fieldInfo.setIsWillCascadeOnDelete(false);


            if (columnModel.getPrimaryKey()) {
                tableInfo.setKeyName(columnModel.getColumnName());
                //tableInfo.setKeyValue();
                fieldInfo.setIsPrimaryKey(true);
                tableInfo.setKeyDatabaseGenerated(DatabaseGenerated.getDatabaseGenerated(columnModel.getDatabaseGeneratedType()));
            }
            if (columnModel.getForeignKey()) {
                foreignKeyNameMap.put(columnModel.getPrimaryKeyTableName(), columnModel.getColumnName());
                //tableInfo.setForeignKeyValue();
                fieldInfo.setIsForeignKey(true);
            }
            if (tableModelMap != null && tableModelMap.size()>0 && columnModel.getRelationshipType() != null && columnModel.getRelationshipType() != 0) {
                fieldInfo.setRelationship(Relationship.getRelationship(columnModel.getRelationshipType()));

                //fieldInfo.setRelationshipTableName(columnModel.getRelationshipTableName());
                TableModel childTableModel = tableModelMap.get(columnModel.getColumnName());
                TableInfo childTableInfo = getTableInfo(childTableModel, tableModelMap);
                tableInfoMap.put(columnModel.getColumnName(), childTableInfo);
                fieldInfo.setIsWillCascadeOnDelete(true);

            }

            if (columnModel.getDatabaseGeneratedType() != null) {
                fieldInfo.setDatabaseGenerated(DatabaseGenerated.getDatabaseGenerated(columnModel.getDatabaseGeneratedType()));
            }

            fieldMap.put(columnModel.getColumnName(), fieldInfo);
            if (columnModel.getRelationshipType() != null && columnModel.getRelationshipType() != 0){
                relationFieldMap.put(columnModel.getColumnName(),fieldInfo);
            }
            else{
                columnFieldMap.put(columnModel.getColumnName(),fieldInfo);
            }

        }
        tableInfo.setFieldMap(fieldMap);
        tableInfo.setRelationFieldMap(relationFieldMap);
        tableInfo.setColumnFieldMap(columnFieldMap);
        tableInfo.setTableInfoMap(tableInfoMap);
        tableInfo.setForeignKeyNameMap(foreignKeyNameMap);
        return tableInfo;
    }

    /**
     * 根据类名解析，用于生成SQL信息
     * @param clz
     * @return
     */
    public static TableInfo getTableInfo(Class parentClz, Class clz){

        TableInfo tableInfo = new TableInfo();
        tableInfo.setTableName(EntityUtils.getTableName(clz));
        tableInfo.setClassName(clz.getName());

        Map<String,FieldInfo> fieldMap =  new HashMap<>();
        Map<String,FieldInfo> columnFieldMap =  new HashMap<>();
        Map<String,FieldInfo> relationFieldMap =  new HashMap<>();
        Map<String,TableInfo> tableInfoMap = new HashMap<>();
        Map<String,String> foreignKeyNameMap = new HashMap<>();

        for (Field field : clz.getDeclaredFields()) {
            if (EntityUtils.hasAnnotation(field, No.class)) continue;

            FieldInfo fieldInfo = new FieldInfo();
            fieldInfo.setColumnName(field.getName());
            fieldInfo.setIsPrimaryKey(false);
            fieldInfo.setIsForeignKey(false);
            fieldInfo.setIsWillCascadeOnDelete(false);
            //fieldInfo.setColumnType();
            //fieldInfo.setValue();

            if (EntityUtils.hasAnnotation(field, Key.class)) {
                tableInfo.setKeyName(field.getName());
                //tableInfo.setKeyValue();
                fieldInfo.setIsPrimaryKey(true);
                tableInfo.setKeyDatabaseGenerated(EntityUtils.getAnnotation(field, DatabaseGeneratedOption.class).value());
            }
            if (EntityUtils.hasAnnotation(field, ForeignKey.class)) {
                if(parentClz != null){
                    foreignKeyNameMap.put(EntityUtils.getTableName(parentClz),field.getName());
                    //模型再调试
                    fieldInfo.setPrimaryKeyTableName("外键表名");
                }
                //tableInfo.setForeignKeyValue();
                fieldInfo.setIsForeignKey(true);
            }
            if (EntityUtils.hasAnnotation(field, RelationshipOption.class)) {
                fieldInfo.setRelationship(EntityUtils.getAnnotation(field, RelationshipOption.class).value());


                Class relationClz = EntityUtils.getAnnotation(field, RelationshipEntity.class).value();
                TableInfo relationTableInfo = getTableInfo(clz, relationClz);
                //fieldInfo.setRelationshipTableName(EntityUtils.getTableName(relationClz));
                tableInfoMap.put(field.getName(), relationTableInfo);
            }
            if(EntityUtils.hasAnnotation(field, RelationshipKey.class)){
                RelationshipKey relationshipKey = EntityUtils.getAnnotation(field, RelationshipKey.class);
                fieldInfo.setPrimaryKeyName(relationshipKey.value());
                fieldInfo.setPrimaryKeyDatabaseGenerated(relationshipKey.databaseGeneratedValue());
            }
            if (EntityUtils.hasAnnotation(field, WillCascadeOnDelete.class)) {
                fieldInfo.setIsWillCascadeOnDelete(true);
            }
            if (EntityUtils.hasAnnotation(field, DatabaseGeneratedOption.class)) {
                fieldInfo.setDatabaseGenerated(EntityUtils.getAnnotation(field, DatabaseGeneratedOption.class).value());
            }

            fieldMap.put(field.getName(), fieldInfo);
            if (EntityUtils.hasAnnotation(field, RelationshipOption.class)){
                relationFieldMap.put(field.getName(),fieldInfo);
            }
            else{
                columnFieldMap.put(field.getName(),fieldInfo);
            }
        }

        tableInfo.setFieldMap(fieldMap);
        tableInfo.setRelationFieldMap(relationFieldMap);
        tableInfo.setColumnFieldMap(columnFieldMap);
        tableInfo.setTableInfoMap(tableInfoMap);
        tableInfo.setForeignKeyNameMap(foreignKeyNameMap);
        return  tableInfo;
    }
}
