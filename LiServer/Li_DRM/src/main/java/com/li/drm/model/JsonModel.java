package com.li.drm.model;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.*;

/**
 * Json解析后的值内容
 */
public class JsonModel {

    public static SimpleDateFormat sf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    /**
     * 值内容
     */
    public Map<String, Object> values = new HashMap<>();
    /**
     * 子表值内容
     */
    public Map<String, List<JsonModel>> relationValues = new HashMap<>();

    /**
     * 转换成字段名与值
     * @param jsonModels
     * @return
     */
    public static List<Map<String,Object>> convertMapByJsonModel(List<JsonModel> jsonModels){
        List<Map<String,Object>> list = new ArrayList<>();
        for(JsonModel jsonModel : jsonModels){
            Map<String,Object> map = new HashMap<>();

            for (Map.Entry<String, Object> o : jsonModel.values.entrySet()) {

                map.put(o.getKey(),o.getValue());
            }


            for (Map.Entry<String, List<JsonModel>> o : jsonModel.relationValues.entrySet()) {
                map.put(o.getKey(),convertMapByJsonModel(o.getValue()));
            }

            list.add(map);
        }

        return list;
    }
}
