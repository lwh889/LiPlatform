package com.li.drm.util;

import java.text.SimpleDateFormat;

public class SqlMakerUtilsMs implements ISqlMakerUtils{
    public String getColumnValueFormat(Object value){
        if(value == null){
            return "null";
        }else{
            String className = value.getClass().getSimpleName();
            switch (className){
                case "Boolean":
                    return String.format("%s", (Boolean)value == true ? "1" : "0");
                case "Timestamp":
                case "Date":
                    SimpleDateFormat ft = new SimpleDateFormat ("yyyy-MM-dd hh:mm:ss");
                    return String.format("'%s'", ft.format(value));
                case "Character":
                case "String":
                    return String.format("'%s'", value);
                case "Byte":
                case "Short":
                case "Long":
                case "Integer":
                case "Float":
                case "Double":
                    return String.format("%s", value);
                default:
                    return "";
            }
        }
    }
}
