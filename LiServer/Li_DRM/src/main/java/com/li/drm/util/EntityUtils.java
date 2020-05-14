package com.li.drm.util;


import com.li.drm.annotate.*;
import lombok.experimental.UtilityClass;
import org.springframework.util.Assert;

import java.lang.annotation.Annotation;
import java.lang.reflect.Field;

/**
 * 实体类的工具类
 *
 * @author hjx
 */
@UtilityClass
public class EntityUtils {
    static final String IS_NOT_TABLE = "Class 不是一个Table";
    static final String IS_NOT_DATABASEGENERATEDOPTION = "Field 不是一个DatabaseGeneratedOption";
    static final String IS_NOT_RELATIONSHIPOPTION = "Class 不是一个RelationshipOption";
    static final String IS_NOT_RELATIONSHIPENTITY = "Class 不是一个RelationshipEntity";
    static final String CLASS_NOT_NULL = "class 不能为 null";
    static final String FIELD_NOT_NULL = "field 不能为 null";
    static final String ANNOTATIONCLASS_NOT_NULL = "annotationClass 不能为 null";

    /**
     * 是否是一个外键
     *
     * @param field
     * @return
     */
    public static boolean isForeignKey(Field field) {
        if (hasAnnotation(field, ForeignKey.class)) {
            return true;
        }
        return false;
    }

    /**
     * 是否是一个Key
     *
     * @param field
     * @return
     */
    static boolean isKey(Field field) {
        if (hasAnnotation(field, Key.class)) {
            return true;
        }
        return false;
    }
    /**
     * 找到clz中包含Key注解的属性
     *
     * @param clz
     * @return
     */
    public static Field getKeyField(Class<?> clz) {
        Assert.isTrue(isTable(clz), IS_NOT_TABLE);
        for (Field field : clz.getDeclaredFields()) {
            //同时是Id 和 Column
            if ( isKey(field)) {
                return field;
            }
        }
        return null;
    }

    /**
     * 找到clz中包含Key注解的属性
     *
     * @param clz
     * @return
     */
    public static Field getForeignKeyField(Class<?> clz) {
        Assert.isTrue(isTable(clz), IS_NOT_TABLE);
        for (Field field : clz.getDeclaredFields()) {
            //同时是Id 和 Column
            if ( isForeignKey(field)) {
                return field;
            }
        }
        return null;
    }

    /**
     * 找到id的数据库字段名称
     *
     * @param clz
     * @return
     */
    public static String getKeyFieldName(Class<?> clz) {
        Assert.isTrue(isTable(clz), IS_NOT_TABLE);
        Field keyField = getKeyField(clz);
        //entity中不存在@ID注解时，忽略。
        if (keyField == null) {
            return null;
        }

        return keyField.getName();
    }

    /**
     * 找到外键的数据库字段名称
     *
     * @param clz
     * @return
     */
    public static String getForeignKeyFieldName(Class<?> clz) {
        Assert.isTrue(isTable(clz), IS_NOT_TABLE);
        Field keyField = getForeignKeyField(clz);
        //entity中不存在@ID注解时，忽略。
        if (keyField == null) {
            return null;
        }

        return keyField.getName();
    }

    /**
     * 找到clz上Table注解中的值
     *
     * @param clz
     * @return
     */
    public static String getTableName(Class<?> clz) {
        Assert.isTrue(isTable(clz), IS_NOT_TABLE);
        return getAnnotation(clz, Table.class).value();
    }

    /**
     * 是否是一个Table
     *
     * @param aClass
     * @return
     */
    public static boolean isTable(Class aClass) {
        if (hasAnnotation(aClass, Table.class)) {
            return true;
        }
        return false;
    }

    /**
     * 获取类上的注解
     *
     * @param clz
     * @param annotationClass
     * @param <T>
     * @return
     */
    public static <T extends Annotation> T getAnnotation(Class<?> clz, Class<T> annotationClass) {
        Assert.notNull(clz, CLASS_NOT_NULL);
        Assert.notNull(annotationClass, ANNOTATIONCLASS_NOT_NULL);
        return clz.getAnnotation(annotationClass);
    }

    /**
     * 获取属性上的注解
     *
     * @param field
     * @param annotationClass
     * @param <T>
     * @return
     */
    public static <T extends Annotation> T getAnnotation(Field field, Class<T> annotationClass) {
        Assert.notNull(field, FIELD_NOT_NULL);
        Assert.notNull(annotationClass, ANNOTATIONCLASS_NOT_NULL);
        return field.getAnnotation(annotationClass);
    }

    public static boolean hasAnnotation(Class<?> clz, Class<? extends Annotation> annotationClass) {
        Assert.notNull(clz, CLASS_NOT_NULL);
        Assert.notNull(annotationClass, ANNOTATIONCLASS_NOT_NULL);
        return clz.isAnnotationPresent(annotationClass);
    }

    public static boolean hasAnnotation(Field field, Class<? extends Annotation> annotationClass) {
        Assert.notNull(field, FIELD_NOT_NULL);
        Assert.notNull(annotationClass, ANNOTATIONCLASS_NOT_NULL);
        return field.isAnnotationPresent(annotationClass);
    }
}

