package com.li.drm.entityinfo;

import com.li.drm.util.ISqlMakerUtils;
import com.li.drm.util.StringUtils;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

/**
 * 条件工厂
 */
public class WhereFactory {
    final static Map<String,WhereFactory> whereFactoryMap = new HashMap<>();
    public static WhereFactory getInstance(ISqlMakerUtils sqlMaker){
        String className = sqlMaker.getClass().getName();
        if(!whereFactoryMap.containsKey(className)){
            whereFactoryMap.put(className, new WhereFactory(sqlMaker));
        }
        return whereFactoryMap.get(className);
    }
    private ISqlMakerUtils sqlMaker;
    public WhereFactory(ISqlMakerUtils sqlMaker){
        this.sqlMaker = sqlMaker;
    }
    public Where equal(final String columnName, final Object value) {
        return new Where(columnName, String.format("%s = %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where notEqual(final String columnName, final Object value) {
        return new Where(columnName, String.format("%s != %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where not(final String columnName, final Object value) {
        return new Where(columnName, String.format("%s <> %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where isNotNull(final String columnName) {
        return new Where(columnName, String.format("%s IS NOT NULL ",columnName ) );
    }

    public Where isNull(final String columnName) {
        return new Where(columnName, String.format("%s IS NULL ",columnName ) );
    }

    public Where greater(final String columnName, final Object value, final boolean andEquals) {
        if (andEquals) {
            return new Where(columnName, String.format("%s >= %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
        }
        return new Where(columnName, String.format("%s > %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where less(final String columnName, final Object value, final boolean andEquals) {
        if (andEquals) {
            return new Where(columnName, String.format("%s <= %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
        }
        return new Where(columnName, String.format("%s < %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where like(final String columnName, final Object value) {
        return new Where(columnName, String.format("%s like %s ",columnName,sqlMaker.getColumnValueFormat(value)) );
    }

    public Where betweenAnd(final String columnName, final Object value1st, final Object value2nd) {
        return new Where(columnName, String.format("%s between  %s and  %s ",columnName,sqlMaker.getColumnValueFormat(value1st),sqlMaker.getColumnValueFormat(value2nd)) );
    }

    public Where in(final String columnName, final Object[] values) {
        Object[] sqlVal = values;
        if (sqlVal.length == 0) {
            sqlVal = new Object[]{null};
        }
        String[] sqlStr = new String[sqlVal.length];
        for(int i = 0; i<sqlVal.length;i++){
            sqlStr[i] = sqlMaker.getColumnValueFormat(sqlVal[i]);
        }

        StringBuffer inSql = new StringBuffer();
        inSql.append(Where.PLACEHOLDER);
        inSql.append(" IN ( ");
        inSql.append(StringUtils.join(Arrays.asList(sqlStr), ", "));
        inSql.append(" ) ");
        return new Where(columnName, inSql.toString(), sqlVal);
    }
}
