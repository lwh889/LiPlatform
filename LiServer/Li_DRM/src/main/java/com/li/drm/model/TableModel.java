package com.li.drm.model;

import com.li.drm.annotate.*;
import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.enumli.Relationship;
import lombok.Getter;
import lombok.Setter;

import java.sql.Timestamp;
import java.util.List;

/**
 * 表信息模型，数据库模型
 */
@Getter
@Setter

@Table("TableInfo")
public class TableModel {
    @Key
    @DatabaseGeneratedOption(DatabaseGenerated.Identity)
    private Integer id;

    /**
     * 所属数据库
     */
    private String dataBaseName;

    /**
     * 实体类型，单据？档案？
     */
    private String entityType;
    /**
     * 比如单据Key
     */
    private String entityKey;

    /**
     * 比如单据名称
     */
    private String entityName;
    /**
     * 是否是主表master
     */
    private String entityOrder;

    /**
     * 对应主键上的字段名
     */
    private String entityColumnName;

    /**
     * 表名称
     */
    private String tableName;

    /**
     * 表别名
     */
    private String tableAliasName;

    /**
     * 表中文名
     */
    private String tableAbbName;

    /**
     * 表描述
     */
    private String tableDesc;

    /**
     * 类名
     */
    private String className;

    /**
     * 主键名
     */
    private String keyName;

    /**
     * 子表实体名
     */
    private String childTableEntityColumnName;

    /**
     * 修改日期
     */
    private Timestamp modifyDate;

    @RelationshipOption(Relationship.Many)
    @WillCascadeOnDelete(true)
    @RelationshipEntity(ColumnModel.class)
    private List<ColumnModel> datas;
}
