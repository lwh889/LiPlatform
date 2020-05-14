package com.li.controller;

import com.alibaba.fastjson.JSONObject;
import com.li.drm.JdbcService;
import com.li.drm.model.ColumnModel;
import com.li.drm.model.ModelUtil;
import com.li.drm.model.TableModel;
import com.li.util.R;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.*;

@RequestMapping("/LiGetNewData")
@RestController
public class JsonDrmGetNewDataController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmGetNewDataController.class);

    @Autowired
    JdbcService jdbcService;

    @PostMapping("/getNewModel")
    R getNewModel(@RequestBody Map<String, String> params) {
        R response;
        try{
            List<Map<String, Object>> lists = ModelUtil.convertMapByModel(TableModel.class);

            response = new R();
            response.put("data", JSONObject.toJSONString(lists));
        }catch (Exception ex){
            logger.info("getNewModel",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }

    @PostMapping("/getNewData")
    R getNewData(@RequestBody Map<String, Object> params) {
        R response;

        try{

            List<TableModel> tableModels = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableModels = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            TableModel masterTableModel = null;
            for (TableModel tableModel: tableModels) {
                if(tableModel.getEntityOrder().equals("master")){
                    masterTableModel = tableModel;
                    break;
                }
            }
            Map<String, Object> map = ModelUtil.convertMapByData(masterTableModel, tableModels);

            response = new R();
            response.put("data", JSONObject.toJSONString(map));
        }catch (Exception ex){
            logger.info("getNewData",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
