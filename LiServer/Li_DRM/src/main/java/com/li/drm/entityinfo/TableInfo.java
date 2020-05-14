package com.li.drm.entityinfo;

import com.li.drm.enumli.DatabaseGenerated;
import com.li.drm.model.TableModel;
import lombok.Getter;
import lombok.Setter;

import java.util.Map;

/**
 * 表信息，用于生成SQL语句
 */
@Setter
@Getter
public class TableInfo {

    /**
     * 类名
     */
    private String className;
    /**
     * 表名
     */
    private String tableName;

    /**
     * 主键名称
     */
    private String keyName;


    /**
     * 数据库名称
     */
    private String dataBaseName;

    /**
     * 主键自增值
     */
    private DatabaseGenerated keyDatabaseGenerated;

    /**
     * 表名-外键名称
     */
    private Map<String,String> foreignKeyNameMap;

    /**
     * 主键值
     */
    private Object keyValue;

    /**
     * 外键值
     */
    private Object foreignKeyValue;

    /**
     * 所有字段
     */
    private Map<String,FieldInfo> fieldMap;

    /**
     * 插入列的集合
     */
    private Map<String,FieldInfo> columnFieldMap;

    /**
     * 插入子表列的集合
     */
    private Map<String,FieldInfo> relationFieldMap;

    /**
     * 子集合映射，字段名-表信息
     */
    private Map<String, TableInfo> tableInfoMap;
    /**
     * 子集合映射，字段名-模型信息
     */
    private Map<String, TableModel> tableModelMap;

    public String getTableInfoMapKey(TableInfo tableInfo,Map<String, TableInfo> tableInfoMap){
        for(Map.Entry<String, TableInfo> entry : tableInfoMap.entrySet()){
            if(entry.getValue() == tableInfo){
                return entry.getKey();
            }
        }
        return "";
    }
}
