package com.li.controller;

import com.li.drm.JdbcService;
import com.li.drm.model.TableModel;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlInsert;
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

@RequestMapping("/LiDelete")
@RestController
public class JsonDrmDeleteController {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(JsonDrmDeleteController.class);

    @Autowired
    JdbcService jdbcService;
    @PostMapping("/delete")
    R update(@RequestBody Map<String, Object> params) {
        R response;

        try{
            List<TableModel> tableInfos = jdbcService.getTableInfo(String.valueOf( params.get("entityKey")),String.valueOf(params.get("systemCode")));
            //List<TableModel> tableInfos = jdbcService.queryBy(TableModel.class, "entityKey", params.get("entityKey"));

            Map<String, Object> datas = ( Map<String, Object> )params.get("datas");
            List<  Map<String, Object>> lists = new ArrayList<>();
            lists.add(datas);

            jdbcService.deleteBy_Json(tableInfos, datas);
            response = new R();
            response.put("data", datas);
        }catch (Exception ex){
            logger.info("delete",ex);
            response = R.error520(ex.getMessage());
        }
        return response;
    }
}
