package com.li.drm;

import com.li.drm.entityinfo.Where;

import java.util.List;

/**
 * 实体类型连接数据库
 */
public interface IJdbcPlusModel {
    /**
     * 获取数据
     * @param entityClass
     * @param wheres
     * @param <T>
     * @return
     */
    <T> List<T> queryBy(Class<T> entityClass, Where... wheres);
    <T>List<T> queryBy(Class<T> entityClass, String columnName, Object columnValue);
}
