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

@RequestMapping("/LiQueryControlInfo")
@RestController
public class JsonDrmQueryControlInfoController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmQueryControlInfoController.class);

    @Autowired
    JdbcService jdbcService;


    @PostMapping("/refControl")
    R queryRefInfo(@RequestBody Map<String, Object> params) {
        R response;
        try{

            List<TableModel> tableModels =
                    jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableModels = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            List<Map<String, Object>> refInfos = new ArrayList<>();
            for(TableModel tableModel : tableModels ){

                Map<String, Object> refInfoMap = new HashMap<>();
                refInfoMap.put("BasicInfoKey", params.get("entityKey"));

                Map<String, Object> refInfoDataMap = new HashMap<>();

                List<String> searchColumns = new ArrayList<>();
                List<String> displayColumns = new ArrayList<>();
                Map<String, String> dictModelDesc = new HashMap<>();
                for (ColumnModel columnModel : tableModel.getDatas()){
                    if(columnModel.getBSearchColumns()){
                        searchColumns.add(columnModel.getColumnName());
                    }
                    if(columnModel.getBDisplayColumn()){
                        displayColumns.add(columnModel.getColumnName());
                    }
                    dictModelDesc.put(columnModel.getColumnName(),columnModel.getColumnAbbName());
                }
                refInfoDataMap.put("searchColumns", searchColumns);
                refInfoDataMap.put("displayColumns", displayColumns);
                refInfoDataMap.put("dictModelDesc", dictModelDesc);
                refInfoMap.put("data", refInfoDataMap);
                refInfos.add(refInfoMap);
            }
            response = new R();
            response.put("data", JSONObject.toJSONString(refInfos));
        }catch (Exception ex){
            logger.info("refControl",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
