package com.li.drm.entityinfo;

import com.li.drm.model.TableModel;
import lombok.Getter;
import lombok.Setter;
import org.springframework.jdbc.core.RowMapper;

import java.util.List;
import java.util.Map;

/**
 * 整个实体的信息，比如整个单据，一对多关系表，用于生SQL语句
 * @param <T>
 */
@Setter
@Getter
public abstract class EntityInfo<T> {

    /**
     * 主表信息
     */
    public TableModel mainTableModel;
    /**
     * 子表信息
     */
    public Map<String,TableModel> tableModels;
    /**
     * 表信息，类解析用
     */
    public Class<T> sourceObject;

    /**
     * 模型映射类
     */
    public RowMapper<T> rowMapper;

    /**
     * 表信息内容，json时，tableModels会分析到tableInfo
     */
    public TableInfo tableInfo;

    /**
     * 解析
     */
    public abstract void analysis();
}
