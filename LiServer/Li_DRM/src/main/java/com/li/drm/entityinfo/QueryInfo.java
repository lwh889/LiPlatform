package com.li.drm.entityinfo;

import com.li.drm.enumli.JudgeSymbol;
import com.li.drm.util.ClassUtils;
import com.li.drm.util.ISqlMakerUtils;
import com.li.drm.util.StringUtils;
import lombok.Getter;
import lombok.Setter;

import java.util.List;
import java.util.Map;

/**
 * 多层次查询条件
 * {"connect":"AND","wheres":[{"columnName":"ABC","columnValue":"123","connect":"AND","wheres":[]},{"columnName":"ASD","columnValue":"456","connect":"AND","wheres":[]},{"columnName":"","columnValue":"","connect":"OR","wheres":[{"columnName":"ASD","columnValue":"456","connect":"OR","wheres":[]},{"columnName":"ASD","columnValue":"456","connect":"OR","wheres":[]}]}]}
 */
@Setter
@Getter
public class QueryInfo {

    /**
     * 连接符，用于子查询的连接条件
     */
    private String connect;

    /**
     * 列名，为空时，则是子查询的父类
     */
    private String columnName;

    /**
     * 判断符号
     */
    private Integer judgeSymbol;

    /**
     * 查询值
     */
    private Object columnValue;

    /**
     * 查询值2
     */
    private Object columnValue2;

    /**
     * 查询值2
     */
    private List<Object> columnValues;

    /**
     * 子查询
     */
    private List<Map<String, Object>> wheres;

    public static Where  getWhereByQueryInfos(QueryInfo queryInfo, ISqlMakerUtils sqlMaker){
        Where where = new Where(Where.AND);
        Where whereTemp = null;
        if(queryInfo.getConnect().equals(Where.OR.trim())){
            where.or();
        }

        try{

            List<Map<String, Object>> mapList = (List<Map<String, Object>>)queryInfo.getWheres();
            for(Map<String, Object> map : mapList) {
                QueryInfo entity  = (QueryInfo) ClassUtils.mapToObject(map, QueryInfo.class);

                if(StringUtils.isNull(entity.getColumnName())){
                    where.getWheres().add(getWhereByQueryInfos(entity, sqlMaker));
                }
                else {
                    switch (JudgeSymbol.getJudgeSymbol(entity.judgeSymbol)){
                        case Equal:
                            whereTemp = WhereFactory.getInstance(sqlMaker).equal(entity.getColumnName(), entity.getColumnValue());
                            break;
                        case NotEqual:
                            whereTemp = WhereFactory.getInstance(sqlMaker).notEqual(entity.getColumnName(), entity.getColumnValue());
                            break;
                        case Not:
                            whereTemp = WhereFactory.getInstance(sqlMaker).not(entity.getColumnName(), entity.getColumnValue());
                            break;
                        case IsNotNull:
                            whereTemp = WhereFactory.getInstance(sqlMaker).isNotNull(entity.getColumnName());
                            break;
                        case IsNull:
                            whereTemp = WhereFactory.getInstance(sqlMaker).isNull(entity.getColumnName());
                            break;
                        case Greater:
                            whereTemp = WhereFactory.getInstance(sqlMaker).greater(entity.getColumnName(), entity.getColumnValue(), false);
                            break;
                        case Less:
                            whereTemp = WhereFactory.getInstance(sqlMaker).less(entity.getColumnName(), entity.getColumnValue(), false);
                            break;
                        case GreaterEqual:
                            whereTemp = WhereFactory.getInstance(sqlMaker).greater(entity.getColumnName(), entity.getColumnValue(), true);
                            break;
                        case LessEqual:
                            whereTemp = WhereFactory.getInstance(sqlMaker).less(entity.getColumnName(), entity.getColumnValue(), true);
                            break;
                        case Like:
                            whereTemp = WhereFactory.getInstance(sqlMaker).like(entity.getColumnName(), entity.getColumnValue());
                            break;
                        case BetweenAnd:
                            whereTemp = WhereFactory.getInstance(sqlMaker).betweenAnd(entity.getColumnName(), entity.getColumnValue(), entity.getColumnValue2());
                            break;
                        case In:
                            whereTemp = WhereFactory.getInstance(sqlMaker).in(entity.getColumnName(), entity.getColumnValues().toArray());
                            break;
                    }
                    whereTemp.setConnect(entity.getConnect());
                    where.getWheres().add(whereTemp);
                }
            }
        }catch (Exception ex){

        }
        return where;
    }
}
