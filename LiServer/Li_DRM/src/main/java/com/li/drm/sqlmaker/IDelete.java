package com.li.drm.sqlmaker;

import com.li.drm.model.JsonModel;

/**
 * 生成SQL的删除接口
 */
public interface IDelete extends ISqlMaker {
    /**
     * 根据JsonModel删除
     * @param jsonModel
     * @return
     */
    boolean deleteByJsonModel(JsonModel jsonModel);

    /**
     * 根据实体数据删除
     * @param entity
     * @return
     */
    boolean delete(Object entity);
}
