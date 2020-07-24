package com.li.drm.sqlmaker;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.entityinfo.Where;
import com.li.drm.model.JsonModel;
import com.li.drm.model.ProcedureModel;
import com.li.drm.util.ISqlMakerUtils;
import com.li.drm.util.StringUtils;
import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * SQL生成抽象类
 */
public abstract class SqlMaker implements ISqlMaker {

    /**
     * SQL生成工具类，比如字符串生成格式
     */
    @Setter
    @Getter
    public ISqlMakerUtils sqlMakerUtils;

    /**
     * 整体信息，比如整个单据的信息
     */
    public EntityInfo entityInfo;

    @Setter
    @Getter
    public String sql;

    /**
     * 是否已解析
     */
    @Setter
    @Getter
    public boolean isAnalysis;

    /**
     * sql中的条件
     */
    @Setter
    @Getter
    public List<Where> wheres = new ArrayList<>();

    public SqlMaker(EntityInfo entityInfo){
        this.entityInfo = entityInfo;
    }

    /**
     * 生成SQL
     */
    public abstract String makeSql();

    /**
     * 字符串格式处理
     * @param value 数据值
     * @return
     */
    public abstract String getColumnValueFormat(Object value);

    /**
     * 生成更新SQL
     * @param primarykeyValue 主表主键值
     * @param childTableInfo 子表表信息
     * @param childMap 子表字段名-数据值
     * @return
     */
    public abstract String makerUpdateSql(StringBuilder builder,Object primarykeyValue ,TableInfo tableInfo,TableInfo childTableInfo,Map<String, Object> childMap);

    /**
     * 生成子表SQL
     * @param builder SQL字符串容器
     * @param primarykeyValue 主表主键数据值
     * @param childTableInfo 子表信息
     * @param childList 所有行数据值
     * @param childKeyValue 子表主键值
     */
    public abstract void makerChildUpdate(StringBuilder builder,Object primarykeyValue ,TableInfo tableInfo,  TableInfo childTableInfo,List<JsonModel> childList,List<Object> childKeyValue);

    public abstract void makerUpdatesSql(StringBuilder builder, Object primarykeyValue,JsonModel jsonModel ,Map<String, List< Object>> deleteKeyValueMap,TableInfo tableInfo, TableInfo childTableInfo);

    /**
     * 批量生成子表SQL
     * @param primarykeyValue 主表主键数据值
     * @param childTableInfoMap 所有子表信息
     * @param childColumnValueMap 所有子表所有行数据值
     * @param childRelationKeyValueMap 所有子表的主键数据值
     * @return
     */
    public String makerChildUpdateBatch(StringBuilder builder,Object primarykeyValue ,TableInfo tableInfo,Map<String, TableInfo> childTableInfoMap, Map<String, List<JsonModel>> childColumnValueMap,Map<String, List< Object>> childRelationKeyValueMap){

        for (Map.Entry<String, List<JsonModel>> entry : childColumnValueMap.entrySet()) {
            TableInfo childTableInfo = childTableInfoMap.get(entry.getKey());

            List<JsonModel> childList = entry.getValue();
            List<Object> childKeyValue = childRelationKeyValueMap.get(entry.getKey());

            for(JsonModel jsonModel : childList){
                makerUpdatesSql(builder,primarykeyValue, jsonModel,childRelationKeyValueMap,tableInfo,childTableInfo  );
            }
        }
        return builder.toString();
    }

    /**
     * 插入SQL
     * @param childTableInfo 子表信息
     * @param childMap 子表行数据值
     * @return
     */
    public abstract String makerInsertSql(StringBuilder builder,TableInfo tableInfo,TableInfo childTableInfo, Map<String, Object> childMap);

    /**
     * 生成主表和所属表，所有SQL
     * @param builder 字符串容器
     * @param tableInfo 主表
     * @param childTableInfo 子表
     * @return
     */
    public abstract void makerInsertsSql(StringBuilder builder,JsonModel jsonModel, TableInfo tableInfo, TableInfo childTableInfo);
    /**
     * 生成子表插入SQL
     * @param builder 字符串容器
     * @param childTableInfo 子表信息
     * @param childList 子表所有行信息
     */
    public abstract void makerChildInsert(StringBuilder builder,TableInfo tableInfo, TableInfo childTableInfo,List<JsonModel> childList);

    public String makerChildInsertBatch(StringBuilder builder, TableInfo tableInfo, Map<String, TableInfo> childTableInfoMap, Map<String, List<JsonModel>> jsonModels){

        for (Map.Entry<String, List<JsonModel>> entry : jsonModels.entrySet()) {
            TableInfo childTableInfo = childTableInfoMap.get(entry.getKey());
            List<JsonModel> childList = entry.getValue();
            if(childTableInfo.getTableInfoMap().size()>0){
                for(JsonModel jsonModel : childList){
                    makerInsertsSql(builder,jsonModel, tableInfo, childTableInfo);
                }
            }
            else{
                makerChildInsert(builder,tableInfo, childTableInfo, childList);
            }

        }
        return builder.toString();
    }

    /**
     * 创造存储过程
     * @param procedureModel 存储过程模型
     * @param paramValues 存储过程参数
     */
    public abstract String makerProcedureSql( ProcedureModel procedureModel, Map<String, Object> paramValues);
    /**
     * 创造存储过程
     * @param procedureMap 存储过程参数
     */
    public abstract String makerProcedureSqlByMap(  Map<String, Object> procedureMap);

    /**
     * 对外公开获取SQL
     * @return
     */
    public String getSql(){
        if(!isAnalysis){
            makeSql();
            this.isAnalysis = true;
        }

        return sql;
    }

    /**
     * 获取sql 中where 条件
     *
     * @return
     */
    public String sqlWhere() {
        StringBuilder sql = new StringBuilder();
        if (wheres.size() != 0) {
            sql.append(" WHERE ");
            sql.append(sqlWhere(null, wheres));
        }
        return sql.toString();
    }

    public String sqlWhere(Where parentWhere, List<Where> wheres){
        StringBuilder sql = new StringBuilder();
        for (int i = 0; i < wheres.size(); i++) {
            Where where = wheres.get(i);

            if (i != 0) {
                //用于旧的查询判断
                if(parentWhere == null)
                {
                    parentWhere = where;
                }
                sql.append(parentWhere.getConnect());
            }

            if(StringUtils.isNull(where.getColumn()) && where.getWheres().size()>0){
                sql.append("( ");
                sql.append(sqlWhere(where, where.getWheres()));
                sql.append(" ) ");
            }
            else{
                String realSql = where.getSql();
                sql.append(realSql);
            }

        }
        return  sql.toString();
    }
}
