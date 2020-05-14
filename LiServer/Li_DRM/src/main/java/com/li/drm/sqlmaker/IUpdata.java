package com.li.drm.sqlmaker;

import com.li.drm.model.JsonModel;

/**
 * 生成SQL的更新接口
 */
public interface IUpdata extends ISqlMaker {
    /**
     * 根据JsonModel更新
     * @param jsonModel
     * @return
     */
    boolean updateByJsonModel(JsonModel jsonModel);

    /**
     * 根据实体数据更新
     * @param entity
     * @return
     */
    boolean update(Object entity);
}
