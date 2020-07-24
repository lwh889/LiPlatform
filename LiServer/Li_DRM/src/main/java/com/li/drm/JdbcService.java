package com.li.drm;

import com.alibaba.fastjson.JSONArray;
import com.li.drm.entityinfo.QueryInfo;
import com.li.drm.entityinfo.Where;
import com.li.drm.entityinfo.WhereFactory;
import com.li.drm.model.JsonModel;
import com.li.drm.model.ProcedureModel;
import com.li.drm.model.TableModel;
import com.mchange.v2.c3p0.ComboPooledDataSource;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

import javax.sql.DataSource;
import java.beans.PropertyVetoException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * 集成所有类型查询接口
 */
@Component
@ConfigurationProperties(prefix = "liorm")
@EnableConfigurationProperties(JdbcService.class)
public class JdbcService implements IJdbcPlusJson,IJdbcPlusModel {
    /**
     * 连接数据库对象
     */
    public JdbcTemplate jdbcTemplate;

    /**
     * 表信息
     */
    public Map<String,List<TableModel>> tableInfoMap = new HashMap<>();

    /**
     * 存储过程信息
     */
    public Map<String,List<ProcedureModel>> procedureInfoMap = new HashMap<>();
    /**
     * 数据库字符串
     */
    private String url;

    /**
     * 用户名
     */
    private String username;

    /**
     * 密码
     */
    private String password;

    public JdbcPlusModelMs jdbcPlusModelMs;

    /**
     * Json类型
     */
    public JdbcPlusJsonMs jdbcPlusJsonMs;

    /**
     * Procedure类型
     */
    public JdbcPlusJsonMs jdbcPlusProcedureMs;
    public JdbcService(){

        //jdbcPlusJsonMs = new JdbcPlusJsonMs(getJdbcTemplate());
    }

    public void Init()
    {
        if(jdbcPlusModelMs == null) jdbcPlusModelMs = new JdbcPlusModelMs(getJdbcTemplate());
        if(jdbcPlusJsonMs == null) jdbcPlusJsonMs = new JdbcPlusJsonMs(getJdbcTemplate());
        if(jdbcPlusProcedureMs == null) jdbcPlusProcedureMs = new JdbcPlusJsonMs(getJdbcTemplate());

    }

    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {
        Init();
        return jdbcPlusJsonMs.deleteBy_Json(tableModels, lists);
    }

    @Override
    public List<Map<String,Object>> procedureByMap_Json(Map<String,Object> procedureMap) {
        Init();
        return jdbcPlusProcedureMs.procedureByMap_Json(procedureMap);
    }
    @Override
    public List<Map<String,Object>> procedureBy_Json(ProcedureModel procedureModel, Map<String, Object> paramValues) {
        Init();
        return jdbcPlusProcedureMs.procedureBy_Json(procedureModel, paramValues);
    }

    @Override
    public Integer procedureNoResult_Json(ProcedureModel procedureModel, Map<String, Object> paramValues) {
        Init();
        return jdbcPlusProcedureMs.procedureNoResult_Json(procedureModel, paramValues);
    }

    @Override
    public Integer procedureNoResultByMap_Json(  Map<String, Object> procedureMap) {
        Init();
        return jdbcPlusProcedureMs.procedureNoResultByMap_Json( procedureMap);
    }

    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        Init();
        return jdbcPlusJsonMs.deleteBy_Json(tableModels, datas);
    }

    @Override
    public Integer deleteBy_Json(List<TableModel> tableModels, JSONArray jsonArray) {
        Init();
        return jdbcPlusJsonMs.deleteBy_Json(tableModels, jsonArray);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        Init();
        return jdbcPlusJsonMs.insertBy_Json(tableModels, datas);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {
        Init();
        return jdbcPlusJsonMs.insertBy_Json(tableModels, lists);
    }

    @Override
    public Integer insertBy_Json(List<TableModel> tableModels, JSONArray jsonArray) {
        Init();
        return jdbcPlusJsonMs.insertBy_Json(tableModels, jsonArray);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists) {

        Init();
        return jdbcPlusJsonMs.updateBy_Json(tableModels, lists);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, Map<String, Object> datas) {
        Init();
        return jdbcPlusJsonMs.updateBy_Json(tableModels, datas);
    }

    @Override
    public Integer updateBy_Json(List<TableModel> tableModels, JSONArray jsonArray) {

        Init();
        return jdbcPlusJsonMs.updateBy_Json(tableModels, jsonArray);
    }

    @Override
    public List<JsonModel> queryBy_Json(List<TableModel> tableModels, Where... wheres) {

        Init();
        return jdbcPlusJsonMs.queryBy_Json(tableModels, wheres);
    }

    public List<JsonModel> queryBy_Json(List<TableModel> tableModels, QueryInfo queryInfo) {

        Init();
        Where where = QueryInfo.getWhereByQueryInfos(queryInfo,jdbcPlusJsonMs.sqlMakerUtils);

        return jdbcPlusJsonMs.queryBy_Json(tableModels, where);
    }

    public List<JsonModel> queryBy_Json(List<TableModel> tableModels, List<Map<String,Object>> logicalOperatorsLists) {

        Init();
        List<Where> wheres = new ArrayList<>();
        for(Map<String,Object> logicalOperatorsMap : logicalOperatorsLists){
            String logicalOperators = (String)logicalOperatorsMap.get("logicalOperators");
            List<Map<String,Object>> whereLists =( List<Map<String,Object>>) logicalOperatorsMap.get("values");
            for(Map<String,Object> whereMap : whereLists){
                wheres.add(WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal((String) whereMap.get("columnName"),whereMap.get("value")));
            }
        }

        return jdbcPlusJsonMs.queryBy_Json(tableModels, wheres.toArray(new Where[wheres.size()]));
    }

    public List<JsonModel> queryBy_Json(List<TableModel> tableModels, String columnName, Object columnValue) {

        Init();
        return jdbcPlusJsonMs.queryBy_Json(tableModels, WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal(columnName,columnValue));

    }
    @Override
    public <T> List<T> queryBy(Class<T> entityClass, Where... wheres) {
        Init();
        return jdbcPlusModelMs.queryBy(entityClass, wheres);
    }

    @Override
    public <T> List<T> queryBy(Class<T> entityClass, String columnName, Object columnValue) {
        Init();
        return jdbcPlusModelMs.queryBy(entityClass, columnName, columnValue);
    }

    //获取存储过程信息
    public List<ProcedureModel> getProcedureInfo( String entityKey, String systemCode){
        Init();
        String KeyStr = String.format("%s_%s", entityKey, systemCode);
        if(!procedureInfoMap.containsKey((KeyStr))){
            Where entityKeyWhere = WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal("entityKey", entityKey);
            Where systemCodeWhere = WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal("systemCode", systemCode);
            List<ProcedureModel> procedureModels = queryBy(ProcedureModel.class, entityKeyWhere,systemCodeWhere);
            procedureInfoMap.put(KeyStr, procedureModels);
        }

        return procedureInfoMap.get(KeyStr);
    }

    //获取表信息
    public List<TableModel> getTableInfo( String entityKey, String systemCode){
        Init();
        String KeyStr = String.format("%s_%s", entityKey, systemCode);
        if(!tableInfoMap.containsKey((KeyStr))){
            Where entityKeyWhere = WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal("entityKey", entityKey);
            Where systemCodeWhere = WhereFactory.getInstance(jdbcPlusJsonMs.sqlMakerUtils).equal("systemCode", systemCode);
            List<TableModel> tableInfos = queryBy(TableModel.class, entityKeyWhere,systemCodeWhere);
            tableInfoMap.put(KeyStr, tableInfos);
        }

        return tableInfoMap.get(KeyStr);
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }


    /**
     * @desc 获取模板的方法
     * @return JdbcTemplate 返回类型
     */
    public JdbcTemplate getJdbcTemplate() {
        if(jdbcTemplate == null ){
            // 创建连接池
            ComboPooledDataSource comboPooledDataSource = new ComboPooledDataSource();
            try {
                if(url.startsWith("jdbc:sqlserver")){
                    comboPooledDataSource.setDriverClass("com.microsoft.sqlserver.jdbc.SQLServerDriver");
                }
            } catch (PropertyVetoException e) {
                e.printStackTrace();
            }
            comboPooledDataSource.setJdbcUrl(url);
            comboPooledDataSource.setUser(username);
            comboPooledDataSource.setPassword(password);
            DataSource dataSource = comboPooledDataSource;


            // 创建模板
            JdbcTemplate jdbcTemplate = new JdbcTemplate();
            jdbcTemplate.setDataSource(dataSource);
            return jdbcTemplate;
        }
        else{
            return jdbcTemplate;
        }
    }
}
