package com.li.controller;

import com.alibaba.fastjson.JSONObject;
import com.li.drm.JdbcService;
import com.li.drm.model.ColumnModel;
import com.li.drm.model.JsonModel;
import com.li.drm.model.TableModel;
import com.li.util.R;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RequestMapping("/LiQueryControlData")
@RestController
public class JsonDrmQueryControlDataController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmQueryControlDataController.class);

    @Autowired
    JdbcService jdbcService;

    @PostMapping("/refControl")
    R queryRefInfo(@RequestBody Map<String, Object> params) {
        R response;
        try{

            List<TableModel> tableModels = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableModels = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableModels);
            List<Map<String, Object>> refs = new ArrayList<>();
            Map<String, Object> refMap = new HashMap<>();
            refMap.put("BasicInfoKey", params.get("entityKey"));

            List<Map<String, Object>> refDatas = JsonModel.convertMapByJsonModel(jsonModels);
            refMap.put("data", refDatas);
            refs.add(refMap);

            response = new R();
            response.put("data", JSONObject.toJSONString(refs));
        }catch (Exception ex){
            logger.info("refControl",ex);
            response = R.error520(ex.getMessage());
        }

        return response;
    }


}
