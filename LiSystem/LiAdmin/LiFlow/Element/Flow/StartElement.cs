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
    public class StartElement : RoundRectShape
    {
        public StartElement(FlowControl s)
            : base(s)
        {
            Text = "开始";
            ShapeColor = Color.White;
            Height = 33;
        }

        public StartElement(FlowControl s, bool NoConnector)
            : base(s)
        {
            Text = "开始";
            ShapeColor = Color.White;
            Height = 33;

            if(NoConnector)
                connectors.Clear();
        }
    }
}
