package com.li.controller;


import com.alibaba.fastjson.JSONObject;
import com.li.test2.ArchivesLog;
import com.li.test2.ExceptionService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping("/exception")
public class ExceptionController {
    @Autowired
    private ExceptionService service;
    public Logger logger = LoggerFactory.getLogger(ExceptionController.class);

    @RequestMapping(value = "/test/{id}", method = RequestMethod.GET, produces = "application/json;charset=UTF-8" )
    @ResponseBody
    @ArchivesLog(operationType = "测试", operationName = "测试异常或者测试返回")
    public JSONObject test(@PathVariable Integer id) throws Exception {
        JSONObject result = new JSONObject();
        result.put("zhouyu", "asdasdasdasd");
        logger.info("测试输出日志");
//        try {
//            service.test();
//        } catch (Exception ex) {
//            throw ex;
//        }
        return result;
    }
}
