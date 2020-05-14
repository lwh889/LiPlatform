package com.li.controller;

import com.li.drm.JdbcService;
import com.li.drm.model.ProcedureModel;
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

@RequestMapping("/LiProcedure")
@RestController
public class JsonDrmProcedureController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmProcedureController.class);

    @Autowired
    JdbcService jdbcService;


    @PostMapping("/procedure")
    R query(@RequestBody Map<String, Object> params) {
        R response ;
        try{
            List<ProcedureModel> procedureModels = jdbcService.getProcedureInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<ProcedureModel> procedureModels = jdbcService.queryBy(ProcedureModel.class, "entityKey", params.get("entityKey"));

            //List<JsonModel> jsonModels = jdbcService.queryBy_Json(tableInfos);

            Map<String, Object> datas = ( Map<String, Object> )params.get("datas");

            List<Map<String, Object>> procedureData = jdbcService.procedureBy_Json(procedureModels.get(0), datas);
            response = new R();
            response.put("data", procedureData);
            return response;
        }catch (Exception ex){
            logger.info("procedure",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
