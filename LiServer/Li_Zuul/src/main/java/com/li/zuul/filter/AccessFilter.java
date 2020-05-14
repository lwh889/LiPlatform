package com.li.zuul.filter;

import com.li.constants.CommonConstants;
import com.li.util.R;
import com.netflix.zuul.ZuulFilter;
import com.netflix.zuul.context.RequestContext;
import org.springframework.beans.factory.annotation.Autowired;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Set;

public class AccessFilter extends ZuulFilter {

    private String ignorePath = "/api-admin/login";

    @Override
    public String filterType() {
        return "pre";
    }

    @Override
    public int filterOrder() {
        return 10000;
    }

    @Override
    public boolean shouldFilter() {
        return true;
    }


    @Override
    public Object run() {
        RequestContext ctx = RequestContext.getCurrentContext();
        try{

            HttpServletRequest request = ctx.getRequest();
            final String requestUri = request.getRequestURI();
            if (isStartWith(requestUri)) {
                return null;
            }
/*            //登录验证
            String accessToken = request.getHeader(CommonConstants.CONTEXT_TOKEN);
            if(null == accessToken || accessToken == ""){
                accessToken = request.getParameter(CommonConstants.TOKEN);
            }
            if (null == accessToken) {
                setFailedRequest(R.error401(), 200);
                return null;
            }*/
/*
            try {
                UserToken userToken = JwtUtils.getInfoFromToken(accessToken);
            } catch (Exception e) {
                setFailedRequest(R.error401(), 200);
                return null;
            }
            FilterContextHandler.setToken(accessToken);
            if(!havePermission(request)){
                setFailedRequest(R.error403(), 200);
                return null;
            }*/
            Set<String> headers = (Set<String>) ctx.get("ignoredHeaders");
            //We need our JWT tokens relayed to resource servers
            //添加自己header
//        ctx.addZuulRequestHeader(CommonConstants.CONTEXT_TOKEN, accessToken);
            //移除忽略token
            headers.remove("authorization");
        }
        catch (Exception ex)
        {
            ctx.set("error.status_code", HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
            ctx.set("error.exception", ex);
        }
        return null;
//        RequestContext ctx = RequestContext.getCurrentContext();
//        Set<String> headers = (Set<String>) ctx.get("ignoredHeaders");
//        // We need our JWT tokens relayed to resource servers
//        headers.remove("authorization");
//        return null;
    }
/*
    private void setFailedRequest(Object body, int code) {
        RequestContext ctx = RequestContext.getCurrentContext();
        ctx.setResponseStatusCode(code);
        HttpServletResponse response = ctx.getResponse();
        PrintWriter out = null;
        try{
            out = response.getWriter();
            out.write(JSONUtils.beanToJson(body));
            out.flush();
        }catch(IOException e){
            e.printStackTrace();
            ctx.set("error.status_code", HttpServletResponse.SC_INTERNAL_SERVER_ERROR);
            ctx.set("error.exception", e);
        }
        ctx.setSendZuulResponse(false);
    }*/
/*

    private boolean havePermission(HttpServletRequest request){
        String currentURL = request.getRequestURI();
        List<MenuDTO> menuDTOS = menuService.userMenus();
        for(MenuDTO menuDTO:menuDTOS){
            //例外
            if(currentURL != null && (currentURL.contains("_Exemption") || currentURL.contains("Excel") || currentURL.contains("currentUser") || currentURL.contains("updateLang"))) {
                return true;
            }
            if(currentURL!=null  &&null!=menuDTO.getUrl()&&currentURL.contains(menuDTO.getUrl())){
                return true;
            }
        }
        return false;
    }
*/

    private boolean isStartWith(String requestUri) {
        boolean flag = false;
        for (String s : ignorePath.split(",")) {

            if (requestUri.startsWith(s)) {
                return true;
            }
        }
        return flag;
    }
}
