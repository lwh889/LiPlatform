package com.li.drm.model;

import com.li.drm.annotate.*;
import com.li.drm.enumli.DatabaseGenerated;
import lombok.Getter;
import lombok.Setter;

import java.sql.Timestamp;

/**
 * 存储过程参数信息
 */
@Getter
@Setter
@Table("ParamInfo")
public class ParamModel {

    @Key
    @DatabaseGeneratedOption(DatabaseGenerated.Identity)
    private Integer id;

    @ForeignKey
    @RelationshipKey(value = "id", databaseGeneratedValue = DatabaseGenerated.Identity)
    private Integer fid;

    /**
     * 列名
     */
    private String paramName;

    /**
     *
     */
    private String paramType;

    /**
     * 数据长度
     */
    private int paramLength;

    /**
     * 修改日期
     */
    private Timestamp modifyDate;
}
