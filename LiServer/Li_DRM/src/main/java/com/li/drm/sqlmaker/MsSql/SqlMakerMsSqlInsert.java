package com.li.drm.sqlmaker.MsSql;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.FieldInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.enumli.Relationship;
import com.li.drm.model.JsonModel;
import com.li.drm.sqlmaker.IInsert;
import com.li.drm.util.ClassUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.Assert;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * MsServer方式生成插入SQL
 */
public class SqlMakerMsSqlInsert extends SqlMakerMsSql implements IInsert {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlInsert.class);

    public JsonModel jsonModel;
    public SqlMakerMsSqlInsert(EntityInfo entityInfo){
        super(entityInfo);
    }

    /**
     * 插入Json数据值
     * @param jsonModel
     * @return
     */
    public boolean insertByJsonModel(JsonModel jsonModel){
        isAnalysis = false;

        //转化子表数据值，只适合一级关系
        this.jsonModel = jsonModel;

        return true;
    }

    /**
     * 插入实体数据值
     * @param entity
     * @return
     */
    public boolean insert(Object entity){
        Assert.notNull(entity, "插入对象不能为 NULL");
        TableInfo tableInfo = entityInfo.getTableInfo();
        jsonModel = new JsonModel();

        //适合所有相应的字段名信息
        Map<String, FieldInfo> fieldMapper =tableInfo.getFieldMap();
        //适合子表相应的字段名信息
        Map<String,TableInfo> tableInfoMap =tableInfo.getTableInfoMap();

        for (Map.Entry<String, FieldInfo> entry : fieldMapper.entrySet()) {
            FieldInfo fieldInfo = entry.getValue();

            //主表数据值
            Map<String, Object> insertColumnValueMap = new HashMap<>();
            //子表所有值
            Map<String, List<JsonModel>> insertRelationColumnValueMap = new HashMap<>();

            //转化子表数据值
            if(tableInfoMap.containsKey(entry.getKey())){
                if(fieldInfo.getRelationship() == Relationship.Many){
                    List<Object> list = (List<Object>) ClassUtils.getValue(entity, entry.getKey());
                    TableInfo childTableInfo = tableInfoMap.get(entry.getKey());
                    Map<String, FieldInfo> columnFieldMap =childTableInfo.getColumnFieldMap();

                    List<JsonModel> childList = new ArrayList<>();
                    for(Object o: list){
                        JsonModel childJsonModel = new JsonModel();
                        Map<String, Object> values = new HashMap<>();
                        for(Map.Entry<String, FieldInfo> childEntry : columnFieldMap.entrySet()) {
                            Object childValue = ClassUtils.getValue(o, childEntry.getKey());
                            values.put(childEntry.getKey(), childValue);
                        }
                        childJsonModel.values = values;
                        childList.add(childJsonModel);
                    }
                    insertRelationColumnValueMap.put(entry.getKey(), childList);

                }
            }
            //否则转化主表数据值
            else{
                Object value = ClassUtils.getValue(entity, entry.getKey());

                insertColumnValueMap.put(entry.getKey(), value);
            }

            jsonModel.values = insertColumnValueMap;
            jsonModel.relationValues = insertRelationColumnValueMap;
        }
        //添加插入数量
        return true;
    }

    @Override
    public String makeSql() {
        StringBuilder builder = new StringBuilder();

        TableInfo tableInfo = entityInfo.getTableInfo();
        makerInsertsSql(builder, jsonModel,null, tableInfo);
        sql = builder.toString();
        logger.info(sql);
        return sql;
    }
}
