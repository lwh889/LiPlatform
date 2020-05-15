package com.li.drm;

import com.alibaba.fastjson.JSONArray;
import com.li.drm.entityinfo.*;
import com.li.drm.model.JsonModel;
import com.li.drm.model.ProcedureModel;
import com.li.drm.model.TableModel;
import com.li.drm.sqlmaker.IDelete;
import com.li.drm.sqlmaker.IInsert;
import com.li.drm.sqlmaker.IUpdata;
import com.li.drm.sqlmaker.MsSql.*;
import com.li.drm.sqlmaker.SqlMaker;
import com.li.drm.util.SqlMakerUtilsMs;
import org.springframework.jdbc.core.JdbcTemplate;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class JdbcPlusJsonMs extends JdbcPlusJson implements IJdbcPlusJson {
    private static JdbcPlusJsonMs jdbcPlusJsonMs = null;
    public JdbcPlusJsonMs(JdbcTemplate jdbcTemplate){
        super(jdbcTemplate);
        sqlMakerUtils = new SqlMakerUtilsMs();
    }

    /**
     * 获取单实例
     * @param jdbcTemplate
     * @return
     */
    public static JdbcPlusJsonMs getInstance(JdbcTemplate jdbcTemplate){
        if(jdbcPlusJsonMs == null){
            jdbcPlusJsonMs = new JdbcPlusJsonMs(jdbcTemplate);
        }
        return jdbcPlusJsonMs;
    }

    @Override
    public List<Map<String, Object>> procedureBy_Json(ProcedureModel procedureModel, Map<String, Object> paramValues) {
        return super.procedureBy(procedureModel, paramValues);
    }

    @Override
    public Integer procedureNoResult_Json(ProcedureModel procedureModel, Map<String, Object> paramValues) {
        return super.procedureNoResult(procedureModel, paramValues);
    }
    @Override
    public Integer deleteBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance(mainTableModel, tableModels);
        return deleteBy_Json(entityInfo, jsonArray);
    }

    @Override
    public Integer updateBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance(mainTableModel, tableModels);
        return updateBy_Json(entityInfo, jsonArray);
    }

    @Override
    public Integer insertBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance(mainTableModel, tableModels);
        return insertBy_Json(entityInfo, jsonArray);
    }

    @Override
    public List<JsonModel> queryBy_Json(TableModel mainTableModel, Map<String,TableModel> tableModels, Where... wheres){
        EntityInfo entityInfo = JsonEntityInfo.getInstance(mainTableModel, tableModels);
        return queryBy_Json(entityInfo, wheres);
    }


    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance(tableModels);
        return deleteBy_Json(entityInfo, jsonArray);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return updateBy_Json(entityInfo, jsonArray);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, JSONArray jsonArray){
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return insertBy_Json(entityInfo, jsonArray);
    }

    @Override
    public List<JsonModel> queryBy_Json(List<TableModel> tableModels, Where... wheres){

        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return queryBy_Json(entityInfo, wheres);
    }

    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return deleteBatchBy_Json(entityInfo, lists);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return updateBatchBy_Json(entityInfo, lists);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        return insertBatchBy_Json(entityInfo, lists);
    }


    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        List<Map<String, Object> > lists = new ArrayList<>();
        lists.add(datas);
        return deleteBatchBy_Json(entityInfo, lists);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        List<Map<String, Object> > lists = new ArrayList<>();
        lists.add(datas);
        return updateBatchBy_Json(entityInfo, lists);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        EntityInfo entityInfo = JsonEntityInfo.getInstance( tableModels);
        List<Map<String, Object> > lists = new ArrayList<>();
        lists.add(datas);
        return insertBatchBy_Json(entityInfo, lists);
    }
}
