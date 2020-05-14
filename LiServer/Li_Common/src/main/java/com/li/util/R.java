package com.li.util;

import java.util.HashMap;
import java.util.Map;

public class R extends HashMap<String, Object> {
    private static final long serialVersionUID = 1L;

    public R() {
        put("code", 0);
        put("msg", "操作成功");
    }

    public static R error() {
        return error(500, "操作失败");
    }

    public static R operate(boolean b){
        if(b){
            return R.ok();
        }
        return R.error();
    }

    public static R error(String msg) {
        return error(500, msg);
    }

    public static R error(int code, String msg) {
        R r = new R();
        r.put("code", code);
        r.put("msg", msg);
        return r;
    }

    public static R ok(String msg) {
        R r = new R();
        r.put("msg", msg);
        return r;
    }

    public static R ok(Map<String, Object> map) {
        R r = new R();
        r.putAll(map);
        return r;
    }

    public static R ok() {
        return new R();
    }

    public static R error401() {
        return error(401, "你还没有登录");
    }

    public static R error403() {
        return error(403, "你没有访问权限");
    }

    public static R error500() {
        return error(500, "服务器内部错误，无法完成请求");
    }

    public static R error501() {
        return error(501, "服务器不支持请求的功能，无法完成请求");
    }

    public static R error502() {
        return error(502, "作为网关或者代理工作的服务器尝试执行请求时，从远程服务器接收到了一个无效的响应");
    }

    public static R error503() {
        return error(503, "由于超载或系统维护，服务器暂时的无法处理客户端的请求。延时的长度可包含在服务器的Retry-After头信息中");
    }

    public static R error504() {
        return error(504, "充当网关或代理的服务器，未及时从远端服务器获取请求");
    }

    public static R error505() {
        return error(505, "服务器不支持请求的HTTP协议的版本，无法完成处理");
    }

    public static R error520() {
        return error(520, "服务器处理出错！");
    }
    public static R error520(String msg) {
        return error(520, msg);
    }

    public static R data(Object data){
        return R.ok().put("data",data);
    }

    public static R page(Object page){
        return R.ok().put("page",page);
    }

    @Override
    public R put(String key, Object value) {
        super.put(key, value);
        return this;
    }
}
