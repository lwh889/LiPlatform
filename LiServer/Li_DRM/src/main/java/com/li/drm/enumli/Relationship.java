package com.li.drm.enumli;

/**
 * 表关系枚举
 */
public enum Relationship {
    None(0), One(1),Many(2),Self(3);
    private int iType;
    Relationship(int iType){
        this.iType = iType;
    }
    public final static Relationship getRelationship(int iType){
        switch (iType){
            case 0:
                return Relationship.None;
            case 1:
                return Relationship.One;
            case 2:
                return Relationship.Many;
            case 3:
                return Relationship.Self;
        }
        return Relationship.None;
    }
}
