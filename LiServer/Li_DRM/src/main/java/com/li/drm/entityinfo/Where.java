package com.li.drm.entityinfo;

import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * where条件 默认使用 and 连接多个条件
 *
 * @author hjx
 */
@Getter
@Setter
public class Where {

    /**
     * 替换符，暂时没用，MySql可能会用到
     */
    public static final String PLACEHOLDER = "#{COLUMN}";

    static final String AND = "AND ";

    static final String OR = "OR ";

    private String sql;

    private String column;

    /**
     * 连接符
     */
    private String connect = AND;


    /**
     * 子查询值
     */
    private List<Where> wheres = new ArrayList<>();
    /**
     * 查询值，暂时没用，MySql可能会用到
     */
    private List<Object> values;

    /**
     * 是否有值（null 也代表有值）
     */
    private boolean hasValue;

    /**
     * @param connect 连接符
     */
    public Where(String connect) {
        this.connect = connect;
    }
    /**
     * @param column 被操作的列
     * @param sql    操作的sql
     */
    public Where(String column, String sql) {
        this.column = column;
        this.sql = sql;
        this.hasValue = false;
        this.values = new ArrayList<>();
    }

    /**
     * @param column 被操作的列
     * @param sql    操作的sql
     * @param value  sql的参数
     */
    public Where(String column, String sql, Object value) {
        this.sql = sql;
        this.column = column;
        this.values = new ArrayList<>();
        this.values.add(value);
        this.hasValue = true;
    }

    /**
     * @param column 被操作的列
     * @param sql    操作的sql
     * @param values sql的参数
     */
    public Where(String column, String sql, Object[] values) {
        this.sql = sql;
        this.column = column;
        this.values = Arrays.asList(values);
        this.hasValue = true;
    }

    public Where or() {
        this.connect = OR;
        return this;
    }

    public Where and() {
        this.connect = AND;
        return this;
    }
}

