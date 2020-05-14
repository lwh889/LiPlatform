
using log4net.Layout;
namespace LiLog
{
    public class LiLayout : PatternLayout
    {
        public LiLayout()
        {
            this.AddConverter("Property", typeof(LiPatternCConverter));
        }
    }
}
