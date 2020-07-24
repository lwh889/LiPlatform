package com.li.drm.sqlmaker.MsSql;

import com.li.drm.model.ProcedureModel;
import lombok.Getter;
import lombok.Setter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Map;

public class SqlMakerMsSqlProcedureByMap  extends SqlMakerMsSql {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlProcedure.class);

    @Getter
    @Setter
    private Map<String, Object> procedureMap;

    public SqlMakerMsSqlProcedureByMap(Map<String, Object> procedureMap){
        super(null);
        this.procedureMap = procedureMap;
    }
    @Override
    public String makeSql() {
        sql = makerProcedureSqlByMap(procedureMap);

        logger.info(sql);
        return sql;
    }
}
