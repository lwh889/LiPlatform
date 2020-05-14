package com.li.drm.sqlmaker.MsSql;

import com.li.drm.entityinfo.EntityInfo;
import com.li.drm.entityinfo.TableInfo;
import com.li.drm.entityinfo.Where;
import com.li.drm.sqlmaker.IQuery;
import com.li.drm.util.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * MsServer方式生成查询SQL
 */
public class SqlMakerMsSqlQuery extends SqlMakerMsSql implements IQuery {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlQuery.class);

    /**
     * 排序条件
     */
    enum Order {
        DESC,
        ASC
    }

    //限制行数
    private String sqlLimit = StringUtils.BLANK;

    //排序
    private String sqlOrderBy = StringUtils.BLANK;

    //汇总
    private String sqlGroupBy = StringUtils.BLANK;

    public SqlMakerMsSqlQuery(EntityInfo entityInfo){
        super(entityInfo);
    }

    //获取排序SQL
    public SqlMakerMsSqlQuery orderBy(Order type, String... orderBy) {
        if(sqlOrderBy.equals(StringUtils.BLANK)){
            sqlOrderBy = " ORDER BY " +
                    StringUtils.join(Arrays.asList(orderBy), StringUtils.COMMA) +
                    StringUtils.SPACE + type.name();
        }else{
            sqlOrderBy = " ," +
                    StringUtils.join(Arrays.asList(orderBy), StringUtils.COMMA) +
                    StringUtils.SPACE + type.name();
        }
        return this;
    }

    //获取汇总SQL
    public SqlMakerMsSqlQuery groupBy(String... groupBy) {
        this.sqlGroupBy = " GROUP BY " + StringUtils.join(Arrays.asList(groupBy), StringUtils.COMMA);
        return this;
    }

    //获取所有条件SQL
    public SqlMakerMsSqlQuery where(Where... wheres) {
        return where(Arrays.asList(wheres));
    }

    //获取条件SQL
    public SqlMakerMsSqlQuery where(List<Where> wheres) {
        for (Where where : wheres) {
            this.wheres.add(where);
        }
        return this;
    }


    @Override
    public String makeSql() {
        //整体表信息
        TableInfo tableInfo = entityInfo.getTableInfo();

        List<String> files = new ArrayList<String>(tableInfo.getColumnFieldMap().keySet());
        StringBuilder builder = new StringBuilder();
        //表SQL
        builder.append("SELECT ")
                .append(StringUtils.join(files, StringUtils.COMMA))
                .append(" FROM ").append(StringUtils.SPACE);

        if(StringUtils.isNotNull(tableInfo.getDataBaseName())){
            builder.append(tableInfo.getDataBaseName()).append(".dbo.");
        }

        builder.append(tableInfo.getTableName())
                .append(sqlWhere())
                .append(sqlGroupBy)
                .append(sqlOrderBy);

        sql = builder.toString();
        logger.info(sql);
        return sql;
    }
}
