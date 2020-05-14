package com.li.controller;

import com.alibaba.fastjson.JSONObject;
import com.alibaba.fastjson.serializer.SerializerFeature;
import com.li.drm.*;
import com.li.drm.entityinfo.QueryInfo;
import com.li.drm.entityinfo.Where;
import com.li.drm.entityinfo.WhereFactory;
import com.li.drm.model.JsonModel;
import com.li.drm.model.TableModel;
import com.li.drm.util.ClassUtils;
import com.li.drm.util.StringUtils;
import com.li.model.Person;
import com.li.util.R;
import com.mchange.v2.collection.MapEntry;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RequestMapping("/LiQuery")
@RestController
public class JsonDrmQueryController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmQueryController.class);

    @Autowired
    JdbcService jdbcService;

    @GetMapping()
    R list(@RequestParam Map<String, Object> params) {
        R response;
        try{

            JSONObject jsonObject = JSONObject
                    .parseObject("{\"type\":\"query\",\"option\":\"queryBy\",\"entityKey\":\"form1\",\"showAllColumn\":true,\"columns\":[],\"wheres\":[{\"logicalOperators\":\"AND\",\"values\":[{\"columnName\":\"id\",\"value\":1}]}]}");
            String typeStr = jsonObject.getString("type");
            String optionStr = jsonObject.getString("option");
            String tableAliasNameStr = jsonObject.getString("entityKey");

            List<TableModel> tableInfos =
                    jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", tableAliasNameStr);

            List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableInfos);

            response = new R();
            response.put("data",JSONObject.toJSONString(JsonModel.convertMapByJsonModel(jsonModels)));
        }catch (Exception ex){
            logger.info("GetMapping",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }


    @PostMapping("/query")
    R query(@RequestBody Map<String, Object> params) {

        R response = new R();
        try{

            List<TableModel> tableInfos = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));

            List<Map<String,Object>> whereLists = ( List<Map<String,Object>>)params.get("wheres");

            QueryInfo queryInfo = (QueryInfo)ClassUtils.mapToObject((Map<String, Object>)params.get("complexWheres"), QueryInfo.class);

            List<JsonModel> jsonModels = null;
            if(queryInfo != null){
                //最新多层次查询
                jsonModels = jdbcService.queryBy_Json(tableInfos, queryInfo);
            }
            else{
                //旧查询，单层次
                jsonModels = jdbcService.queryBy_Json(tableInfos, whereLists);
            }

            response.put("data", JSONObject.toJSONStringWithDateFormat(JsonModel.convertMapByJsonModel(jsonModels),"yyyy-MM-dd HH:mm:ss.SSS", SerializerFeature.WriteMapNullValue));
        }catch (Exception ex){
            logger.info("query",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }

    @PostMapping("/getBasicInfos")
    R getBasicInfos(@RequestBody Map<String, Object> params) {
        R response;
        try{

            List<String> entityKeys = (List<String>)params.get("entityKeys") ;
            List<Map<String, Object>> basicInfos = new ArrayList<>();

            for(String entityKey : entityKeys){
                Map<String, Object> basicInfoMap = new HashMap<>();

                List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", entityKey);
                List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableInfos);
                basicInfoMap.put("BasicInfoKey",entityKey);
                basicInfoMap.put("data", JsonModel.convertMapByJsonModel(jsonModels));
                basicInfos.add(basicInfoMap);
            }

            response = new R();
            response.put("data",JSONObject.toJSONString(basicInfos));
        }catch (Exception ex){
            logger.info("getBasicInfos",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }


    @PostMapping("/getDictInfos")
    R getDictInfos(@RequestBody Map<String, Object> params) {
        R response;
        try{
            List<String> entityKeys = (List<String>)params.get("entityKeys") ;
            List<Map<String, Object>> basicInfos = new ArrayList<>();

            for(String entityKey : entityKeys){
                Map<String, Object> basicInfoMap = new HashMap<>();

                List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", "liDict");

                List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableInfos,"dictParentID", entityKey);
                basicInfoMap.put("DictInfoKey",entityKey);
                basicInfoMap.put("data", JsonModel.convertMapByJsonModel(jsonModels));
                basicInfos.add(basicInfoMap);
            }

            response = new R();
            response.put("data",JSONObject.toJSONString(basicInfos));
        }catch (Exception ex){
            logger.info("getDictInfos",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
