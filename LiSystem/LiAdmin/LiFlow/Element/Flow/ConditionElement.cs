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
    public class ConditionElement : DiamondShape
    {
        public ConditionElement(FlowControl s)
            : base(s)
        {
            Text = "条件";
            ShapeColor = Color.White;
            Height = 33;

        }


        public ConditionElement(FlowControl s, bool NoConnector)
            : base(s)
        {
            Text = "条件";
            ShapeColor = Color.White;
            Height = 33;

            if(NoConnector)
                connectors.Clear();
        }
    }
}
