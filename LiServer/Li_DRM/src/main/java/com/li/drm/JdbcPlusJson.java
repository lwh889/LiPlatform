package com.li.drm;

import com.alibaba.fastjson.JSONArray;
import com.alibaba.fastjson.JSONObject;
import com.li.drm.entityinfo.*;
import com.li.drm.model.JsonModel;
import com.li.drm.model.TableModel;
import com.li.drm.sqlmaker.IDelete;
import com.li.drm.sqlmaker.IInsert;
import com.li.drm.sqlmaker.IUpdata;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlDelete;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlInsert;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlQuery;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlUpdata;
import org.springframework.jdbc.core.JdbcTemplate;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public abstract class JdbcPlusJson extends JdbcPlus  {

    public JdbcPlusJson(JdbcTemplate jdbcTemplate){
        super(jdbcTemplate);
    }

    public abstract Integer deleteBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray);
    public abstract Integer updateBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray);
    public abstract Integer insertBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray);
    public abstract List<JsonModel> queryBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, Where... wheres);

    public abstract Integer deleteBy_Json(List<TableModel> tableModels, JSONArray jsonArray);
    public abstract Integer updateBy_Json(List<TableModel> tableModels, JSONArray jsonArray);
    public abstract Integer insertBy_Json(List<TableModel> tableModels, JSONArray jsonArray);

    public Integer deleteBy_Json(EntityInfo entityInfo, List<JsonModel> jsonModels){

        IDelete deleteMaker = new SqlMakerMsSqlDelete(entityInfo);

        StringBuilder stringBuilder = new StringBuilder();
        for (JsonModel jsonModel : jsonModels){
            deleteMaker.deleteByJsonModel(jsonModel);
            deleteBy(deleteMaker);
        }

        return 0;
    }

    public Integer deleteBy_Json(EntityInfo entityInfo, JSONArray jsonArray){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, jsonArray);

        return deleteBy_Json(entityInfo,jsonModels);
    }

    public Integer deleteBatchBy_Json(EntityInfo entityInfo, List<Map<String,Object>> lists){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, lists);

        return deleteBy_Json(entityInfo,jsonModels);
    }

    public Integer updateBy_Json(EntityInfo entityInfo, List<JsonModel> jsonModels){
        IUpdata updateMaker = new SqlMakerMsSqlUpdata(entityInfo);

        StringBuilder stringBuilder = new StringBuilder();
        for (JsonModel jsonModel : jsonModels){
            updateMaker.updateByJsonModel(jsonModel);
            updateBy(updateMaker);
        }

        return 0;
    }

    public Integer updateBy_Json(EntityInfo entityInfo, JSONArray jsonArray){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, jsonArray);

        return updateBy_Json(entityInfo, jsonModels);
    }

    public Integer updateBatchBy_Json(EntityInfo entityInfo, List<Map<String,Object>> lists){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, lists);

        return updateBy_Json(entityInfo, jsonModels);
    }

    public Integer insertBy_Json(EntityInfo entityInfo, List<JsonModel> jsonModels){

        IInsert insertMaker = new SqlMakerMsSqlInsert(entityInfo);
        TableInfo tableInfo = entityInfo.tableInfo;

        Integer i = 0;
        for (JsonModel jsonModel : jsonModels){
            insertMaker.insertByJsonModel(jsonModel);
            i = insertBy(insertMaker);
            jsonModel.values.put(tableInfo.getKeyName(), i);
        }

        return i;
    }

    public Integer insertBy_Json(EntityInfo entityInfo, JSONArray jsonArray){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();
        Map<String, TableModel> tableModelMap = entityInfo.tableInfo.getTableModelMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, jsonArray);

        return insertBy_Json(entityInfo,jsonModels);
    }

    public Integer insertBatchBy_Json(EntityInfo entityInfo, List<Map<String,Object>> lists){
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();

        List<JsonModel> jsonModels = getJsonModels(tableInfoMap, lists);

        return insertBy_Json(entityInfo,jsonModels);
    }

    public List<JsonModel> queryBy_Json(EntityInfo entityInfo, Where... wheres){

        SqlMakerMsSqlQuery queryMaker = new SqlMakerMsSqlQuery(entityInfo);
        queryMaker.where(wheres);
        List<JsonModel> list = queryBy(queryMaker);

        TableInfo tableInfo = entityInfo.tableInfo;
        Map<String, TableInfo> tableInfoMap = entityInfo.tableInfo.getTableInfoMap();
        Map<String, TableModel> tableModelMap = entityInfo.tableInfo.getTableModelMap();
        if(tableInfoMap != null && tableInfoMap.size()>0) {
            for (JsonModel value : list) {
                value.relationValues  = new HashMap<>();
                for(Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()){
                    TableInfo childTableInfo = entry.getValue();

                    EntityInfo childEntityInfo = JsonEntityInfo.getInstance(tableModelMap.get(entry.getKey()), entityInfo.getTableModels());

                    List<JsonModel> childList = queryBy_Json(childEntityInfo, WhereFactory.getInstance(sqlMakerUtils).equal(childTableInfo.getForeignKeyNameMap().get(tableInfo.getTableName()), value.values.get(tableInfo.getKeyName())));
                    value.relationValues.put(entry.getKey(), childList);
                }
            }
        }
        return list;
    }

    /**
     * 根据JSONArray 获取JsonModel
     * @param tableInfoMap
     * @param jsonArray
     * @return
     */
    public List<JsonModel> getJsonModels(Map<String, TableInfo> tableInfoMap, JSONArray jsonArray){

        //Json模型保存数据值
        List<JsonModel> jsonModels = new ArrayList<>();
        for(int i=0;i<jsonArray.size();i++) {
            JsonModel jsonModel = new JsonModel();
            JSONObject object = jsonArray.getJSONObject(i);
            for (Map.Entry<String, Object> o : object.entrySet()) {
                //获取子表数据值
                if (tableInfoMap.containsKey(o.getKey())) {
                    TableInfo childTableInfo = tableInfoMap.get(o.getKey());
                    Map<String, TableInfo> childTableInfoMap = childTableInfo.getTableInfoMap();
                    jsonModel.relationValues.put(o.getKey(), getJsonModels(childTableInfoMap, (JSONArray) o.getValue()));
                } else {
                    //获取主表数据值
                    jsonModel.values.put(o.getKey(), o.getValue());
                }
            }
            jsonModels.add(jsonModel);
        }
        return jsonModels;
    }


    /**
     * 根据JSONArray 获取JsonModel
     * @param tableInfoMap
     * @param datas
     * @return
     */
    public List<JsonModel> getJsonModels(Map<String, TableInfo> tableInfoMap, List<Map<String, Object>> lists){

        //Json模型保存数据值
        List<JsonModel> jsonModels = new ArrayList<>();

        for(Map<String, Object> datas : lists){

            JsonModel jsonModel = new JsonModel();

            for (Map.Entry<String, Object> o : datas.entrySet()) {
                //获取子表数据值
                if (tableInfoMap.containsKey(o.getKey())) {
                    TableInfo childTableInfo = tableInfoMap.get(o.getKey());
                    Map<String, TableInfo> childTableInfoMap = childTableInfo.getTableInfoMap();
                    if(o.getValue() != null && ((List<Map<String, Object>>)o.getValue()).size()>0){
                        jsonModel.relationValues.put(o.getKey(), getJsonModels(childTableInfoMap, (List<Map<String, Object>>) o.getValue()));
                    }
                } else {
                    //获取主表数据值
                    jsonModel.values.put(o.getKey(), o.getValue());
                }
            }

            jsonModels.add(jsonModel);
        }

        return jsonModels;
    }
}
