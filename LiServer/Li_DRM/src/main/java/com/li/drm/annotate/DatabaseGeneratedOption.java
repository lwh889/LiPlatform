package com.li.drm.annotate;

import com.li.drm.enumli.DatabaseGenerated;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

@Target(ElementType.FIELD)
@Retention(RetentionPolicy.RUNTIME)
/**
 * 自增值类型
 */
public @interface DatabaseGeneratedOption {
    DatabaseGenerated value();
}
