package com.li.drm.sqlmaker.MsSql;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.model.JsonModel;
import com.li.drm.model.ParamModel;
import com.li.drm.model.ProcedureModel;
import com.li.drm.sqlmaker.SqlMaker;
import com.li.drm.util.SqlMakerUtilsMs;
import com.li.drm.util.StringUtils;

import java.util.List;
import java.util.Map;

/**
 * MsServer方式生成SQL
 */
public abstract class SqlMakerMsSql extends SqlMaker {
    public SqlMakerMsSql(EntityInfo entityInfo){
        super(entityInfo);
        sqlMakerUtils = new SqlMakerUtilsMs();
    }

    @Override
    public String getColumnValueFormat(Object value) {
        return sqlMakerUtils.getColumnValueFormat(value);
    }

    @Override
    public  String makerUpdateSql(StringBuilder builder,Object primarykeyValue ,TableInfo tableInfo, TableInfo childTableInfo, Map<String, Object> childMap){
        StringBuilder builderUpdate = new StringBuilder();
        StringBuilder builderIdStr = new StringBuilder();
        Map<String, FieldInfo> childFieldInfoMap = childTableInfo.getFieldMap();

        String paramStr = String.format("@%s_%s",childTableInfo.getKeyName(), childTableInfo.getTableName());

        builder.append(" UPDATE ").append(childTableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(StringUtils.SPACE);
        builder.append(" SET ");

        for (Map.Entry<String, Object> childEntry : childMap.entrySet()) {
            FieldInfo childFieldInfo = childFieldInfoMap.get(childEntry.getKey());
            //抛空
            if(childFieldInfo.getIsPrimaryKey() ){
                builderIdStr.append(String.format(" set %s = %s ",paramStr, getColumnValueFormat(childEntry.getValue())));
            }

            if(childFieldInfo.getIsPrimaryKey() || childFieldInfo.getIsForeignKey()) continue;

            builderUpdate.append(childEntry.getKey()).append("=");
            builderUpdate.append(getColumnValueFormat(childEntry.getValue())).append(",");
        }
        builder.append(builderUpdate.deleteCharAt(builderUpdate.length()-1));
        builder.append(" WHERE ").append(childTableInfo.getKeyName()).append(" = ").append(getColumnValueFormat(primarykeyValue));

        builder.append(builderIdStr.toString());

        return builder.toString();
    }

    @Override
    public void makerUpdatesSql(StringBuilder builder,Object primarykeyValue, JsonModel jsonModel,Map<String, List< Object>> deleteKeyValueMap,  TableInfo tableInfo, TableInfo childTableInfo) {

        Map<String, TableInfo> tableInfoMap = childTableInfo.getTableInfoMap();

        //Map<String, FieldInfo> fieldMap = childTableInfo.getFieldMap();

        /*if(primarykeyValue != null){
            builder.append(String.format(" DELETE %s WHERE %s = %s and %s not in ( ",childTableInfo.getTableName(), childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), getColumnValueFormat(primarykeyValue), childTableInfo.getKeyName()));

            List<Object> childKeyValue = deleteKeyValueMap.get(tableInfo.getTableInfoMapKey(childTableInfo, tableInfo.getTableInfoMap()));
            for(Object value : childKeyValue){
                builder.append( getColumnValueFormat(value)).append(",");
            }
            builder.deleteCharAt(builder.length()-1);
            builder.append(") ");

            //删除三级以上的子表数据，在父表不存在
            builder.append(String.format(" DELETE %s WHERE %s NOT IN (SELECT %s FROM %s )  ",childTableInfo.getTableName(), childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), tableInfo.getKeyName(), tableInfo.getTableName()));

        }*/

        DatabaseGenerated databaseGenerated = childTableInfo.getKeyDatabaseGenerated();
        String paramStr = String.format("@%s_%s ",childTableInfo.getKeyName(), childTableInfo.getTableName());
        if(builder.indexOf(paramStr) < 0){
            if(DatabaseGenerated.Uniqueidentifier == databaseGenerated) {
                builder.append(String.format(" declare %s Uniqueidentifier  ", paramStr));
            }
            if(DatabaseGenerated.Identity == databaseGenerated) {
                builder.append(String.format(" declare %s int ", paramStr));
            }
        }

        Object keyValue = jsonModel.values.get(childTableInfo.getKeyName());
        if(keyValue == null){
            makerInsertSql(builder,tableInfo,childTableInfo, jsonModel.values);
        }else{
            builder.append(String.format(" if exists (select 1 from %s.dbo.%s where %s = %s) begin",childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getKeyName(), getColumnValueFormat(keyValue)));
            makerUpdateSql(builder,keyValue,tableInfo, childTableInfo, jsonModel.values);
            builder.append(" end ");
            builder.append(" else begin ");
            makerInsertSql(builder,tableInfo, childTableInfo, jsonModel.values) ;

            if(DatabaseGenerated.Uniqueidentifier == databaseGenerated){
                builder.append(String.format("set %s= NEWID()",paramStr));
            }
            if(DatabaseGenerated.Identity == databaseGenerated){
                builder.append(String.format(" set %s= @@identity",paramStr));
            }

            builder.append(" end ");
        }

        makerChildUpdateBatch(builder, keyValue,childTableInfo, tableInfoMap, jsonModel.relationValues,deleteKeyValueMap);

    }

    @Override
    public void makerChildUpdate(StringBuilder builder, Object primarykeyValue ,TableInfo tableInfo,  TableInfo childTableInfo, List<JsonModel> childList, List<Object> childKeyValue){
        if(childKeyValue != null && childKeyValue.size()>0){
            builder.append(String.format(" DELETE %s.dbo.%s WHERE %s = %s and %s not in ( ",childTableInfo.getDataBaseName(), childTableInfo.getTableName(), childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), getColumnValueFormat(primarykeyValue), childTableInfo.getKeyName()));
            for(Object value : childKeyValue){
                builder.append( getColumnValueFormat(value)).append(",");
            }
            builder.deleteCharAt(builder.length()-1);
            builder.append(") ");
        }

        for (JsonModel childMap : childList) {
            Object keyValue = childMap.values.get(childTableInfo.getKeyName());
            if(keyValue == null){
                makerInsertSql(builder,tableInfo,childTableInfo, childMap.values);
            }else{
                builder.append(String.format(" if exists (select 1 from %s.dbo.%s where %s = %s) begin", childTableInfo.getDataBaseName(), childTableInfo.getTableName(),childTableInfo.getKeyName(), getColumnValueFormat(keyValue)));
                makerUpdateSql(builder,keyValue,tableInfo, childTableInfo, childMap.values);
                builder.append(" end ");
                builder.append(" else begin ");
                makerInsertSql(builder,tableInfo, childTableInfo, childMap.values) ;
                builder.append(" end ");
            }
        }
    }

    @Override
    public void makerInsertsSql(StringBuilder builder, JsonModel jsonModel, TableInfo tableInfo, TableInfo childTableInfo) {

        Map<String, TableInfo> tableInfoMap = childTableInfo.getTableInfoMap();


        //判断主键类型，设置主键值
        DatabaseGenerated databaseGenerated = childTableInfo.getKeyDatabaseGenerated();
        String paramStr = String.format("@%s_%s ",childTableInfo.getKeyName(), childTableInfo.getTableName());
        if(DatabaseGenerated.Uniqueidentifier == databaseGenerated){
            if(builder.indexOf(paramStr)<0) {
                builder.append(String.format(" declare %s Uniqueidentifier  ",paramStr));
            }
            builder.append(String.format("set %s= NEWID()",paramStr));
        }

        makerInsertSql(builder,tableInfo, childTableInfo, jsonModel.values);

        if(DatabaseGenerated.Identity == databaseGenerated){
            if(builder.indexOf(paramStr)<0) {
                builder.append(String.format(" declare %s int ",paramStr ));
            }
            builder.append(String.format(" set %s= @@identity",paramStr));
        }

        //生成主，子表SQL
        makerChildInsertBatch(builder,childTableInfo, tableInfoMap, jsonModel.relationValues);

    }

    @Override
    public String makerInsertSql(StringBuilder builder, TableInfo tableInfo, TableInfo childTableInfo, Map<String, Object> childMap){
        Map<String, FieldInfo> fieldMap = childTableInfo.getFieldMap();

        StringBuilder builderField = new StringBuilder();
        StringBuilder builderValue = new StringBuilder();
        builder.append(" INSERT INTO ").append(childTableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(StringUtils.SPACE);
        builderField.append("(" );
        builderValue.append(" VALUES (");

        for (Map.Entry<String, Object> childEntry : childMap.entrySet()) {
            FieldInfo fieldInfo = fieldMap.get(childEntry.getKey());
            //if(fieldInfo == null) new 抛出异常
            //如果是自增，就不用写
            if(fieldInfo.getIsPrimaryKey() && ( DatabaseGenerated.Identity == fieldInfo.getDatabaseGenerated() || DatabaseGenerated.Uniqueidentifier == fieldInfo.getDatabaseGenerated())) continue;
            builderField.append(childEntry.getKey()).append(",");

            //如果子表有外键的情况
            String primaryKeyName = fieldInfo.getPrimaryKeyName();
            if(StringUtils.isNotNull(primaryKeyName)){
                if(DatabaseGenerated.Identity == fieldInfo.getPrimaryKeyDatabaseGenerated() || DatabaseGenerated.Uniqueidentifier == fieldInfo.getPrimaryKeyDatabaseGenerated()){
                    String paramStr = String.format("@%s_%s",primaryKeyName,tableInfo == null ? "" : fieldInfo.getPrimaryKeyTableName());
                    if(!fieldInfo.getPrimaryKeyTableName().equals(tableInfo.getTableName()) || builder.indexOf(paramStr)<0 ){
                        builderValue.append("NULL,");
                    }
                    else{
                        builderValue.append( paramStr).append(",");
                    }
                }else{
                    builderValue.append(getColumnValueFormat(childEntry.getValue())).append(",");
                }
            }
            else{
                builderValue.append(getColumnValueFormat(childEntry.getValue())).append(",");
            }
        }

        builder.append(builderField.deleteCharAt(builderField.length()-1)).append(") ").append(builderValue.deleteCharAt(builderValue.length()-1)).append(")");

        return builder.toString();
    }

    @Override
    public void makerChildInsert(StringBuilder builder,TableInfo tableInfo, TableInfo childTableInfo,List<JsonModel> childList){
        Map<String, FieldInfo> fieldMap = childTableInfo.getFieldMap();

        for (JsonModel childMap : childList) {
            StringBuilder builderField = new StringBuilder();
            StringBuilder builderValue = new StringBuilder();
            builder.append(" INSERT INTO ").append(childTableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(StringUtils.SPACE);
            builderField.append("(" );
            builderValue.append(" VALUES (");

            for (Map.Entry<String, Object> childEntry : childMap.values.entrySet()) {
                FieldInfo fieldInfo = fieldMap.get(childEntry.getKey());
                //如果是自增，就不用写
                if((DatabaseGenerated.Identity == fieldInfo.getDatabaseGenerated() || DatabaseGenerated.Uniqueidentifier == fieldInfo.getDatabaseGenerated()) && !fieldInfo.getIsForeignKey()) continue;

                builderField.append(childEntry.getKey()).append(",");


                //如果子表有外键的情况
                String primaryKeyName = fieldInfo.getPrimaryKeyName();
                if(StringUtils.isNotNull(primaryKeyName)) {
                    if (DatabaseGenerated.Identity == fieldInfo.getPrimaryKeyDatabaseGenerated() || DatabaseGenerated.Uniqueidentifier == fieldInfo.getPrimaryKeyDatabaseGenerated()) {
                        builderValue.append(String.format("@%s_%s",primaryKeyName,tableInfo.getTableName() )).append(",");
                    } else {
                        builderValue.append(getColumnValueFormat(childEntry.getValue())).append(",");
                    }
                }else{
                    builderValue.append(getColumnValueFormat(childEntry.getValue())).append(",");
                }
            }

            builder.append(builderField.deleteCharAt(builderField.length()-1)).append(") ").append(builderValue.deleteCharAt(builderValue.length()-1)).append(")");
        }
    }

    @Override
    public String makerProcedureSql(ProcedureModel procedureModel, Map<String, Object> paramValues) {
        StringBuilder builder = new StringBuilder();
        builder.append("exec sp_executesql N' EXEC ").append(procedureModel.getDataBaseName()).append(".dbo.").append(procedureModel.getProcedureName());
        List<ParamModel> paramModels = procedureModel.getDatas();
        for(ParamModel paramModel : paramModels){
            builder.append(" @").append(paramModel.getParamName()).append(",");
        }
        builder.deleteCharAt(builder.length()-1);
        builder.append("',N'");

        for(ParamModel paramModel : paramModels){
            String paramStr = "";
            switch (paramModel.getParamType()){
                case "nvarchar":
                    if(paramModel.getParamLength()>4000){
                        paramStr = "nvarchar(max)";
                    }else
                    {
                        paramStr = String.format("%s(%s)", paramModel.getParamType(),paramModel.getParamLength());
                    }
                    break;
                case "int":
                    paramStr=paramModel.getParamType();
                    break;
                default:
                    paramStr=paramModel.getParamType();
                    break;
            }
            builder.append(" @").append(paramModel.getParamName()).append(" ").append(paramStr).append(",");

        }
        builder.deleteCharAt(builder.length()-1);
        builder.append("',");


        for(ParamModel paramModel : paramModels){
            builder.append(getColumnValueFormat(paramValues.get(paramModel.getParamName()))).append(",");
        }
        builder.deleteCharAt(builder.length()-1);
        return builder.toString();
    }
}
