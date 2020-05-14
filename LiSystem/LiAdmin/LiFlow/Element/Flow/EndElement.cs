using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiFlow.UI;
using LiFlow.Element.Shapes;
using System.Drawing;

namespace LiFlow.Element.Flow
{
    [Serializable]
    public class EndElement : RoundRectShape
    {
        public EndElement(FlowControl s)
            : base(s)
        {
            Text = "结束";
            ShapeColor = Color.White;
            Height = 33;

        }

        public EndElement(FlowControl s, bool NoConnector)
            : base(s)
        {
            Text = "结束";
            ShapeColor = Color.White;
            Height = 33;

            if(NoConnector)
                connectors.Clear();
        }
    }
}
