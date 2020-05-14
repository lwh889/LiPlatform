using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiEnum
{
    public class JudgmentSymbol
    {
        public const string Equal = "0";

        public const string GreaterThan = "1";

        public const string LessThan = "2";

        public const string GreaterThanEqual = "3";

        public const string LessThanEqual = "4";

        public const string Like = "5";

        public const string LikeLeft = "6";

        public const string LikeRight = "7";

        public static string getJudeSymbol(string sJudgeSymbol)
        {
            switch (sJudgeSymbol)
            {
                case JudgmentSymbol.Equal:
                    return "=";
                case JudgmentSymbol.GreaterThan:
                    return ">";
                case JudgmentSymbol.LessThan:
                    return "<";
                case JudgmentSymbol.GreaterThanEqual:
                    return ">=";
                case JudgmentSymbol.LessThanEqual:
                    return "<=";
                case JudgmentSymbol.Like:
                case JudgmentSymbol.LikeLeft:
                case JudgmentSymbol.LikeRight:
                    return "like";
                default:
                    return "=";
            }
        }
    }
}
