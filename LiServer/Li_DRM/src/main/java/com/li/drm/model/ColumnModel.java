package com.li.drm.model;

import com.li.drm.annotate.*;
import com.li.drm.enumli.DatabaseGenerated;
import lombok.Getter;
import lombok.Setter;

import java.sql.Timestamp;

/**
 * 列信息，数据库模型
 */
@Getter
@Setter
@Table("ColumnInfo")
public class ColumnModel {

    @Key
    @DatabaseGeneratedOption(DatabaseGenerated.Identity)
    private Integer id;

    @ForeignKey
    @RelationshipKey(value = "id", databaseGeneratedValue = DatabaseGenerated.Identity)
    private Integer fid;

    /**
     * 列名
     */
    private String columnName;

    /**
     * 列名简称
     */
    private String columnAbbName;

    /**
     * 列类型
     */
    private String columnType;

    /**
     * 控件类型
     */
    private String controlType;

    /**
     * 长度
     */
    private Integer length;

    /**
     * 是否主键
     */
    private Boolean primaryKey;

    /**
     * 是否外键
     */
    private Boolean foreignKey;

    /**
     * 关系类型，一对多，还是一对一
     */
    private Integer relationshipType;

    /**
     * 自增类型
     */
    private Integer databaseGeneratedType;

    /**
     * 对应主表主键列名称
     */
    private String primaryKeyName;

    /**
     * 对应主表主键列，自增类型
     */
    private Integer primaryKeyDatabaseGenerated;

    /**
     * 对应主表表名称
     */
    private String primaryKeyTableName;

    /**
     * 列顺序
     */
    private Integer columnOrder;

    /**
     * 列宽度
     */
    private Integer columnWidth;

    /**
     * 列小数位
     */
    private Integer columnScale;

    /**
     * 列是否为空
     */
    private Boolean columnIsNull;

    /**
     * 列默认值
     */
    private Object columnDefaultValue;

    /**
     * 该列是否可搜索
     */
    private Boolean bSearchColumns;

    /**
     * 该列是否显示
     */
    private Boolean bDisplayColumn;

    /**
     * 是否显示
     */
    private Boolean bVisible;

    /**
     * 基础档案类型
     */
    private String basicInfoType;

    /**
     * 字典类型
     */
    private String dictInfoType;

    /**
     * 基础档案显示属性
     */
    private String basicInfoShowFieldName;

    /**
     * 基础档案关联字段名
     */
    private String basicInfoRelationFieldName;

    /**
     * 基础档案关联字段名
     */
    private String basicInfoKeyFieldName;

    /**
     * 显示配置
     */
    private String gridlookUpEditShowModelJson;
    /**
     * 修改时间
     */
    private Timestamp modifyDate;
}
