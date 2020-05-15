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

import java.util.HashMap;
import java.util.Map;

/**
 * MsServer方式生成删除SQL
 */
public class SqlMakerMsSqlDelete extends SqlMakerMsSql implements IDelete {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlDelete.class);

    public Object keyValue = null;
    public Map<String, Object> foreignKeyMap = new HashMap<>();
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

            //获取子表的主键值
            if(tableInfoMap.containsKey(entry.getKey())){
                if(fieldInfo.getRelationship() == Relationship.Many){
                    TableInfo childTableInfo = tableInfoMap.get(entry.getKey());

                    foreignKeyMap.put(childTableInfo.getKeyName(), keyValue);
                }
            }
        }

        return true;
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
            if(tableInfoMap.containsKey(entry.getKey())){
                if(fieldInfo.getRelationship() == Relationship.Many){
                    foreignKeyMap.put(entry.getKey(), keyValue);
                }
            }
        }

        return true;
    }

    @Override
    public String makeSql() {
        StringBuilder builder = new StringBuilder();

        TableInfo tableInfo = entityInfo.getTableInfo();
        Map<String, TableInfo> tableInfoMap = tableInfo.getTableInfoMap();

        for (Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()) {
            TableInfo childTableInfo = entry.getValue();
            builder.append(" DELETE ").append(tableInfo.getDataBaseName()).append(".dbo.").append(childTableInfo.getTableName()).append(" WHERE ").append( childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName())).append(" = ").append(getColumnValueFormat(keyValue));
        }
        builder.append(" DELETE ").append(tableInfo.getDataBaseName()).append(".dbo.").append(tableInfo.getTableName()).append(" WHERE ").append(tableInfo.getKeyName()).append(" = ").append(getColumnValueFormat(keyValue));

        sql = builder.toString();
        logger.info(sql);
        return sql;
    }
}
