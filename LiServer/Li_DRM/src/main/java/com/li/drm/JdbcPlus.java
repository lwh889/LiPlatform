package com.li.drm;

import com.li.drm.mapper.MapRowMapper;
import com.li.drm.model.ProcedureModel;
import com.li.drm.sqlmaker.IDelete;
import com.li.drm.sqlmaker.IInsert;
import com.li.drm.sqlmaker.IUpdata;
import com.li.drm.sqlmaker.MsSql.SqlMakerMsSqlProcedure;
import com.li.drm.sqlmaker.SqlMaker;
import com.li.drm.util.ISqlMakerUtils;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.PreparedStatementCreator;
import org.springframework.jdbc.support.GeneratedKeyHolder;
import org.springframework.jdbc.support.KeyHolder;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;
import java.util.Map;

/**
 * 基本的连接数据库执行类
 */
public abstract class JdbcPlus {

    /**
     * 连接数据库的对象
     */
    public JdbcTemplate jdbcTemplate;

    /**
     * 每种数据库的字符串格式
     */
    public ISqlMakerUtils sqlMakerUtils;

    public JdbcPlus(JdbcTemplate jdbcTemplate){
        this.jdbcTemplate = jdbcTemplate;
    }

    /**
     * 执行删除
     * @param sqlMaker 生成SQL对象
     * @return
     */
    public Integer deleteBy(IDelete sqlMaker){
        return jdbcTemplate.update(sqlMaker.getSql());
    }

    /**
     * 执行插入
     * @param sqlMaker 生成SQL对象
     * @return
     */
    public Integer insertBy(IInsert sqlMaker){
/*        KeyHolder keyHolder = new GeneratedKeyHolder();
        jdbcTemplate.update(new PreparedStatementCreator() {
            public PreparedStatement createPreparedStatement(Connection connection) throws SQLException {


                PreparedStatement ps = connection.prepareStatement(sqlMaker.getSql(), Statement.RETURN_GENERATED_KEYS);

                return ps;
            }
        }, keyHolder);
        return keyHolder.getKey().intValue();*/

        return jdbcTemplate.update(sqlMaker.getSql());
    }

    /**
     * 执行更新
     * @param sqlMaker 生成SQL对象
     * @return
     */
    public Integer updateBy(IUpdata sqlMaker){
        return jdbcTemplate.update(sqlMaker.getSql());
    }

    /**
     * 执行查询
     * @param sqlMaker 生成SQL对象
     * @return
     */
    public List queryBy(SqlMaker sqlMaker){
        return jdbcTemplate.query(sqlMaker.getSql(), sqlMaker.entityInfo.rowMapper);
    }


    /**
     * 执行存储过程查询
     * @param sqlMaker 生成SQL对象
     * @return
     */
    public List procedureBy(SqlMaker sqlMaker){
        return jdbcTemplate.query(sqlMaker.getSql(), new MapRowMapper());
    }

    /**
     *
     * @param procedureModel
     * @param paramValues
     * @return
     */
    public List procedureBy(ProcedureModel procedureModel, Map<String,Object> paramValues){
        SqlMaker sqlMaker = new SqlMakerMsSqlProcedure(procedureModel,paramValues );
        return procedureBy(sqlMaker);
    }
}
