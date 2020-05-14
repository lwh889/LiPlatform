package com.li.drm.sqlmaker;

import com.li.drm.model.JsonModel;

/**
 * 生成SQL的插入接口
 */
public interface IInsert extends ISqlMaker {
    /**
     * 根据JsonModel插入
     * @param jsonModel
     * @return
     */
    boolean insertByJsonModel(JsonModel jsonModel);

    /**
     * 根据实体数据插入
     * @param entity
     * @return
     */
    boolean insert(Object entity);
}
