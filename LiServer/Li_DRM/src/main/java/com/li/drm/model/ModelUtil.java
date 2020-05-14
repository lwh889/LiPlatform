package com.li.drm.model;

import com.li.drm.annotate.RelationshipEntity;
import com.li.drm.annotate.RelationshipOption;
import com.li.drm.util.EntityUtils;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ModelUtil {
    public static List<Map<String, Object>> convertMapByModel(Class clz){
        List<Map<String, Object>> lists = new ArrayList<>();
        Map<String, Object> map = new HashMap<>();
        for (Field field : clz.getDeclaredFields()) {
            if (EntityUtils.hasAnnotation(field, RelationshipOption.class)) {
                Class relationClz = EntityUtils.getAnnotation(field, RelationshipEntity.class).value();
                map.put(field.getName(), convertMapByModel(relationClz));
            }
            else{
                map.put(field.getName(),field.getType().getSimpleName());
            }
        }
        lists.add(map);

        return lists;
    }

    public static Map<String, Object> convertMapByData(TableModel tableModel,List<TableModel> tableModels){

        Map<String, Object> map = new HashMap<>();
        List<ColumnModel> masterColumnModels = tableModel.getDatas();
        for (ColumnModel columnModel: masterColumnModels) {
            if(columnModel.getColumnType().equals("collection")){
                for(TableModel slaveTableModel : tableModels){
                    if(columnModel.getColumnName().equals(slaveTableModel.getEntityColumnName())){
                        if(columnModel.getColumnType().equals("collection")){
                            map.put(columnModel.getColumnName(), convertMapByDatas(slaveTableModel, tableModels));
                        }
                        else{
                            map.put(columnModel.getColumnName(), convertMapByData(slaveTableModel, tableModels));
                        }
                    }
                }
            }
            else {
                map.put(columnModel.getColumnName(), columnModel.getColumnType());
            }
        }
        return map;
    }


    public static List<Map<String, Object>> convertMapByDatas(TableModel tableModel,List<TableModel> tableModels){
        List<Map<String, Object>> list = new ArrayList<>();

        Map<String, Object> map = new HashMap<>();
        List<ColumnModel> masterColumnModels = tableModel.getDatas();
        for (ColumnModel columnModel: masterColumnModels) {
            if(columnModel.getColumnType().equals("collection")){
                for(TableModel slaveTableModel : tableModels){
                    if(columnModel.getColumnName().equals(slaveTableModel.getEntityColumnName())){
                        map.put(columnModel.getColumnName(), convertMapByData(slaveTableModel, tableModels));
                    }
                }
            }
            else {
                map.put(columnModel.getColumnName(), columnModel.getColumnType());
            }
        }
        list.add(map);
        return list;
    }
}
