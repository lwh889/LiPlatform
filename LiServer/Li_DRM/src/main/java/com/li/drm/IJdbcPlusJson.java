package com.li.drm;

import com.alibaba.fastjson.JSONArray;
import com.li.drm.entityinfo.Where;
import com.li.drm.model.JsonModel;
import com.li.drm.model.ProcedureModel;
import com.li.drm.model.TableModel;

import java.util.List;
import java.util.Map;

/**
 * Json类型连接数据库
 */
public interface IJdbcPlusJson {


    /**
     * 根据JSONArray删除
     * @param tableModels 数据表信息
     * @param datas
     * @return
     */
    Integer deleteBy_Json(List<TableModel> tableModels,Map<String, Object> datas);

    /**
     * 根据JSONArray更新
     * @param tableModels 数据表信息
     * @param datas
     * @return
     */
    Integer updateBy_Json(List<TableModel> tableModels,Map<String, Object> datas);

    /**
     * 根据JSONArray插入
     * @param tableModels 数据表信息
     * @param datas
     * @return
     */
    Integer insertBy_Json(List<TableModel> tableModels, Map<String, Object> datas);

    /**
     * 根据JSONArray删除
     * @param tableModels 数据表信息
     * @param lists
     * @return
     */
    Integer deleteBy_Json(List<TableModel> tableModels,List<Map<String, Object>> lists);

    /**
     * 根据JSONArray更新
     * @param tableModels 数据表信息
     * @param lists
     * @return
     */
    Integer updateBy_Json(List<TableModel> tableModels,List<Map<String, Object>> lists);

    /**
     * 根据JSONArray插入
     * @param tableModels 数据表信息
     * @param lists
     * @return
     */
    Integer insertBy_Json(List<TableModel> tableModels, List<Map<String, Object>> lists);


    /**
     * 根据JSONArray删除
     * @param tableModels 数据表信息
     * @param jsonArray
     * @return
     */
    Integer deleteBy_Json(List<TableModel> tableModels, JSONArray jsonArray);

    /**
     * 根据JSONArray更新
     * @param tableModels 数据表信息
     * @param jsonArray
     * @return
     */
    Integer updateBy_Json(List<TableModel> tableModels, JSONArray jsonArray);

    /**
     * 根据JSONArray插入
     * @param tableModels 数据表信息
     * @param jsonArray
     * @return
     */
    Integer insertBy_Json(List<TableModel> tableModels, JSONArray jsonArray);

    /**
     * 获取数据
     * @param tableModels
     * @param wheres
     * @return
     */
    List<JsonModel> queryBy_Json(List<TableModel> tableModels, Where... wheres);

    /**
     * 存储过程获取数据
     * @param procedureModel
     * @param paramValues
     * @return
     */
    List<Map<String, Object>> procedureBy_Json(ProcedureModel procedureModel, Map<String,Object> paramValues);

}
