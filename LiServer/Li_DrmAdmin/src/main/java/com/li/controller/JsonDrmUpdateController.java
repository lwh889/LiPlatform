package com.li.controller;

import com.li.drm.JdbcService;
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
import java.util.List;
import java.util.Map;

@RequestMapping("/LiUpdate")
@RestController
public class JsonDrmUpdateController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmUpdateController.class);

    @Autowired
    JdbcService jdbcService;


    @PostMapping("/update")
    R update(@RequestBody Map<String, Object> params) {
        R response;
        try{

            List<TableModel> tableInfos = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            //List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableInfos);

            Map<String, Object> datas = ( Map<String, Object> )params.get("datas");
            //List<  Map<String, Object>> lists = new ArrayList<>();
            //lists.add(datas);

            jdbcService.updateBy_Json(tableInfos, datas);
            response = new R();
            response.put("data", datas);
        }catch (Exception ex){
            logger.info("update",ex);
            response = R.error520(ex.toString());
        }

        return response;
    }

    @PostMapping("/updateBatch")
    R updateBatch(@RequestBody Map<String, Object> params) {
        R response;
        try{
            List<TableModel> tableInfos = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            List<  Map<String, Object>> lists = (List<  Map<String, Object>> )params.get("datas");

            jdbcService.updateBy_Json(tableInfos, lists);
            response = new R();
            response.put("data", lists);
        }catch (Exception ex){
            logger.info("updateBatch",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
