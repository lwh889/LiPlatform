package com.li.drm.annotate;

import com.li.drm.enumli.DatabaseGenerated;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * 关联信息
 */
@Target(ElementType.FIELD)
@Retention(RetentionPolicy.RUNTIME)
public @interface RelationshipKey {
    //外键名称
    String value();
    //自增值类型
    DatabaseGenerated databaseGeneratedValue();
}
