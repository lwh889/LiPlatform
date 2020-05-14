using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiEnum;

using LiModel.Interface;

namespace LiModel.Form
{
    /// <summary>
    /// 判断符号
    /// </summary>
    public class JudgmentSymbolModel : GridlookUpEditModel
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
        

        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("name"))
            {
                DisplayColumns.Add("name");
            }
            return DisplayColumns;
        }

        public List<GridlookUpEditModel> getDataSource()
        {
            List<GridlookUpEditModel> _dataSource = getDataSource<List<GridlookUpEditModel>>();

            if (_dataSource.Count<=0)
            {
                _dataSource.Add(new JudgmentSymbolModel() { code = "0", name = "等于" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "1", name = "大于" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "2", name = "小于" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "3", name = "大于等于" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "4", name = "小于等于" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "5", name = "全模糊" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "6", name = "左匹配" });
                _dataSource.Add(new JudgmentSymbolModel() { code = "7", name = "右匹配" });
            }
            return _dataSource; 
        }
    }
}
