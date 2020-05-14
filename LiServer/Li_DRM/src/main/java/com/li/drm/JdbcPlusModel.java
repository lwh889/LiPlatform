package com.li.drm;

import com.li.drm.entityinfo.Where;
import org.springframework.jdbc.core.JdbcTemplate;

import java.util.List;

public abstract class JdbcPlusModel extends JdbcPlus {

    public JdbcPlusModel(JdbcTemplate jdbcTemplate){
        super(jdbcTemplate);
    }

    public abstract <T> List<T> queryBy(Class<T> entityClass, Where... wheres);
    public abstract <T>List<T> queryBy(Class<T> entityClass, String columnName, Object columnValue);
}
