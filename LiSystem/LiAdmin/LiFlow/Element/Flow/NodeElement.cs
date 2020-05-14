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
    public class NodeElement : SimpleRectangle
    {
        public NodeElement(FlowControl s)
            : base(s)
        {
            Text = "节点";
            ShapeColor = Color.White;
            Height = 33;

        }

        public NodeElement(FlowControl s, bool NoConnector)
            : base(s)
        {
            Text = "节点";
            ShapeColor = Color.White;
            Height = 33;

            if(NoConnector)
                connectors.Clear();
        }
    }
}
