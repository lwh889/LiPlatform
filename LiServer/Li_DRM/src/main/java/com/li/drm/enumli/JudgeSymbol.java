package com.li.drm.enumli;

public enum JudgeSymbol {
    ///等于
    Equal(0),
    ///不等于
    NotEqual(1),
    ///不相等
    Not(2),
    ///不为空
    IsNotNull(3),
    ///为空
    IsNull(4),
    ///大于
    Greater(5),
    ///小于
    Less(6),
    ///大于等于
    GreaterEqual(7),
    ///小于等于
    LessEqual(8),
    ///相似
    Like(9),
    ///两者之间
    BetweenAnd(10),
    ///包含
    In(11);

    private final int iType;
    JudgeSymbol(int iType){
        this.iType = iType;
    }

    public final static JudgeSymbol getJudgeSymbol(int iType){
        switch (iType){
            case 0:
                return JudgeSymbol.Equal;
            case 1:
                return JudgeSymbol.NotEqual;
            case 2:
                return JudgeSymbol.Not;
            case 3:
                return JudgeSymbol.IsNotNull;
            case 4:
                return JudgeSymbol.IsNull;
            case 5:
                return JudgeSymbol.Greater;
            case 6:
                return JudgeSymbol.Less;
            case 7:
                return JudgeSymbol.GreaterEqual;
            case 8:
                return JudgeSymbol.LessEqual;
            case 9:
                return JudgeSymbol.Like;
            case 10:
                return JudgeSymbol.BetweenAnd;
            case 11:
                return JudgeSymbol.In;
        }
        return JudgeSymbol.Equal;
    }

    public final static Integer getJudgeSymbolValue(JudgeSymbol judgeSymbol){
        return judgeSymbol.iType;
    }
}
