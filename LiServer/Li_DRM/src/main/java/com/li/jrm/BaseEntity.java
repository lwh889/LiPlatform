package com.li.jrm;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

public abstract class BaseEntity {
    private transient CglibProxy cglibProxy;
    private String tableName = "";
    private String idString = "ID";
    private String idField = "id";
    private final Map<String, String> columnMap = new HashMap<>();
    public BaseEntity(){
        if(this.getClass().getSimpleName().indexOf("$$EnhancerByCGLIB$$")>0){
            return;
        }
        Table table = this.getClass().getAnnotation(Table.class);
        tableName=(table.name()== "" ? this.getClass().getName().toUpperCase() : table.name());
        Field[] fields = Student.class.getDeclaredFields();
        for (Field field : fields) {
            if (field.isAnnotationPresent(Id.class)) {
                if (field.isAnnotationPresent(Column.class)) {
                    Column column = field.getAnnotation(Column.class);
                    idString = (column.name() == "" ? "ID" : column.name());
                    idField = field.getName();
                    columnMap.put(field.getName(), column.name());
                }
            } else {
                if (field.isAnnotationPresent(Column.class)) {
                    Column column = field.getAnnotation(Column.class);
                    columnMap.put(field.getName(), column.name());
                }
            }
        }
    }

    public BaseEntity getProxyObject() {
        cglibProxy = new CglibProxy();
        return (BaseEntity) cglibProxy.getInstance(this);
    }

    public BaseEntity getNotProxyObject() {
        return (BaseEntity) cglibProxy.getTarget();
    }

    public CglibProxy getCglibProxy() {
        return cglibProxy;
    }
    public String getIdString() {
        return idString;
    }

    public Map<String, String> getColumnMap() {
        return columnMap;
    }

    public String getTableName() {
        return tableName;
    }

    public String getIdField() {
        return idField;
    }
}
