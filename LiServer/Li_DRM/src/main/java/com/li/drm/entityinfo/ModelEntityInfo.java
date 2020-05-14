package com.li.drm.entityinfo;


import com.li.drm.mapper.ModelMapper;

/**
 * Class信息解析方法
 */
public class ModelEntityInfo<T> extends EntityInfo<T> {
    @Override
    public void analysis() {
        this.setTableInfo(TableInfoFactory.getTableInfo(null,sourceObject));
        this.setRowMapper(new ModelMapper(sourceObject, this.getTableInfo()));
        return;
    }
}
