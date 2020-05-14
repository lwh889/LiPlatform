package com.li.drm.sqlmaker.MsSql;

import com.li.drm.model.ProcedureModel;
import lombok.Getter;
import lombok.Setter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Map;

public class SqlMakerMsSqlProcedure extends SqlMakerMsSql {
    //调试日志
    private final static Logger logger = LoggerFactory.getLogger(SqlMakerMsSqlProcedure.class);

    @Getter
    @Setter
    private ProcedureModel procedureModel;

    @Getter
    @Setter
    private Map<String, Object> paramValues;

    public SqlMakerMsSqlProcedure(ProcedureModel procedureModel, Map<String, Object> paramValues){
        super(null);
        this.procedureModel = procedureModel;
        this.paramValues = paramValues;
    }

    @Override
    public String makeSql() {
        sql = makerProcedureSql(procedureModel,paramValues);

        logger.info(sql);
        return sql;
    }
}
