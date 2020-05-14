package com.li.drm.entityinfo;

import com.li.drm.mapper.JsonMapper;
import com.li.drm.model.JsonModel;
import com.li.drm.model.TableModel;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Json信息解析方法
 */
public class JsonEntityInfo extends EntityInfo<JsonModel> {
    @Override

    public void analysis() {
        this.setTableInfo(TableInfoFactory.getTableInfo(mainTableModel,tableModels));
        this.setRowMapper(new JsonMapper( this.getTableInfo()));
    }

    /**
     *
     * @param mainTableModel 主表
     * @param tableModels 所有子表
     * @return
     */
    public static EntityInfo getInstance(TableModel mainTableModel, Map<String,TableModel> tableModels){

        EntityInfo entityInfo = new JsonEntityInfo();
        entityInfo.setMainTableModel(mainTableModel);
        entityInfo.setTableModels(tableModels);
        entityInfo.analysis();
        return entityInfo;
    }

    /**
     * 根据master分别主和次
     * @param tableModels 所有表
     * @return
     */
    public static EntityInfo getInstance(List<TableModel> tableModels){
        TableModel mainTableModel = null;
        Map<String, TableModel> tableModelMap = new HashMap<>();

        for(TableModel tableModel : tableModels){
            if("master".equals(tableModel.getEntityOrder()) ){
                mainTableModel = tableModel;
            }
            else{
                tableModelMap.put(tableModel.getEntityColumnName(), tableModel);
            }
        }
        EntityInfo entityInfo = new JsonEntityInfo();
        entityInfo.setMainTableModel(mainTableModel);
        entityInfo.setTableModels(tableModelMap);
        entityInfo.analysis();
        return entityInfo;
    }
}
