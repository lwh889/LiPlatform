package com.li.test2;


import java.lang.annotation.*;

@Target({ElementType.PARAMETER, ElementType.METHOD})
@Retention(RetentionPolicy.RUNTIME)
@Documented
public @interface ArchivesLog {
    /**
     * 操作类型
     * @return
     */
    public String operationType() default "";

    /**
     * 操作名称
     * @return
     */
    public String operationName() default "";

}