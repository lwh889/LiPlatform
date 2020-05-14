package com.li.drm.enumli;

/**
 * 自增型枚举
 */
public enum DatabaseGenerated {
    ///表示不处理
    None(0),
    ///表示自增长
    Identity(1),
    ///表示计算所得
    Computed(2),
    ///表示NEWID
    Uniqueidentifier(3);

    private final int iType;
    DatabaseGenerated(int iType){
        this.iType = iType;
    }

    public final static DatabaseGenerated getDatabaseGenerated(int iType){
        switch (iType){
            case 0:
                return DatabaseGenerated.None;
            case 1:
                return DatabaseGenerated.Identity;
            case 2:
                return DatabaseGenerated.Computed;
            case 3:
                return DatabaseGenerated.Uniqueidentifier;
        }
        return DatabaseGenerated.None;
    }
}
