package com.li.drm.model;

import com.li.drm.annotate.*;
import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.enumli.Relationship;
import lombok.Getter;
import lombok.Setter;

import java.sql.Timestamp;
import java.util.List;

/**
 * 存储过程K信息模型
 */
@Getter
@Setter

@Table("ProcedureInfo")
public class ProcedureModel {
    @Key
    @DatabaseGeneratedOption(DatabaseGenerated.Identity)
    private Integer id;

    /**
     * 所属数据库
     */
    private String dataBaseName;

    /**
     * 比如存储过程Key
     */
    private String entityKey;

    /**
     * 存储过程名称
     */
    private String procedureName;

    /**
     * 修改日期
     */
    private Timestamp modifyDate;

    @RelationshipOption(Relationship.Many)
    @WillCascadeOnDelete(true)
    @RelationshipEntity(ParamModel.class)
    private List<ParamModel> datas;
}
