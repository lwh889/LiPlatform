package com.li.drm.entityinfo;

import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.enumli.Relationship;
import lombok.Getter;
import lombok.Setter;

/**
 * 字段信息，用于生成SQL语句
 */
@Setter
@Getter
public class FieldInfo {

    /**
     * 列名
     */
    private String columnName;

    /**
     * 列类型
     */
    private Class columnType;

    /**
     * 是否主键
     */
    private Boolean isPrimaryKey;

    /**
     * 主表主键
     */
    private String primaryKeyName;
    /**
     * 主表主键增值类型
     */
    private DatabaseGenerated primaryKeyDatabaseGenerated;

    /**
     * 外键表名
     */
    private String primaryKeyTableName;
     /**
     * 是否外键
     */
    private Boolean isForeignKey;

    /**
     * 是关联
     */
    private Boolean isWillCascadeOnDelete;

    /**
     * 关联关系
     */
    private Relationship relationship;

    /**
     * 主键增值类型
     */
    private DatabaseGenerated databaseGenerated;

    /**
     * 值
     */
    private Object value;

}
