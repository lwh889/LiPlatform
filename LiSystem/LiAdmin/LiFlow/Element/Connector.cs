using System;
using System.Drawing;
using LiFlow.Collections;

namespace LiFlow.Element
{
    /// <summary>
    /// Represents an endpoint of a connection or a location of a shape to which
    /// 表示连接的端点或形状的位置
    /// a connection can be attached
    /// 可以连接
    /// </summary>
    [Serializable]
    public class Connector : Entity
    {

        #region Fields
        /// <summary>
        /// 索引
        /// </summary>
        protected int index;
        /// <summary>
        /// 所属线条
        /// </summary>
        protected Connection lineBase;
        /// <summary>
        /// 所属形状
        /// </summary>
        protected ShapeBase shapeBase;
        /// <summary>
        /// the location of this connector
        /// 此连接器的位置
        /// </summary>
        protected Point point;
        /// <summary>
        /// the connectors attached to this connector
        /// 连接到此连接器的连接器
        /// </summary>
        protected ConnectorCollection attachedConnectors;
        /// <summary>
        /// the connector, if any, to which this connector is attached to
        /// 连接此连接器的连接器（如果有）
        /// </summary>
        protected Connector attachedTo;
        /// <summary>
        /// the name of this connector
        /// 连接此连接器的连接器（如果有）
        /// </summary>
        protected string name;
        #endregion

        #region Properties

        /// <summary>
        /// The name of this connector
        /// 索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        /// <summary>
        /// The name of this connector
        /// 此连接器的名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// If the connector is attached to another connector
        /// 如果连接器连接到另一个连接器
        /// </summary>
        public Connector AttachedTo
        {
            get { return attachedTo; }
            set { attachedTo = value; }
        }

        /// <summary>
        /// The location of this connector
        /// 此连接器的位置
        /// </summary>
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        /// <summary>
        /// 所属形状
        /// </summary>
        public ShapeBase ShapeBase
        {
            get { return shapeBase; }
            set { shapeBase = value; }
        }

        /// <summary>
        /// 所属线条
        /// </summary>
        public Connection LineBase
        {
            get { return lineBase; }
            set { lineBase = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default connector
        /// </summary>
        public Connector()
        {
            attachedConnectors = new ConnectorCollection();
        }

        /// <summary>
        /// Constructs a connector, passing its location
        /// </summary>
        /// <param name="p"></param>
        public Connector(Point p)
        {
            attachedConnectors = new ConnectorCollection();
            point = p;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Paints the connector on the canvas
        /// </summary>
        /// <param name="g"></param>
        public override void Paint(Graphics g)
        {
            if (hovered)
                g.FillRectangle(Brushes.Red, point.X - 5, point.Y - 5, 10, 10);
            else
                g.FillRectangle(Brushes.WhiteSmoke, point.X - 2, point.Y - 2, 4, 4);

            //			this is useful when debugging, but annoying otherwise (though you might like it)	
            //这在调试时很有用，但在其他方面很烦人（尽管您可能喜欢它）
            //			if(name!=string.Empty)
            //				g.DrawString(name,new Font("verdana",10),Brushes.Black,point.X+7,point.Y+4);
        }

        /// <summary>
        /// Tests if the mouse hits this connector
        /// 测试鼠标是否碰到此连接器
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool Hit(Point p)
        {
            Point a = p;
            Point b = point;
            b.Offset(-7, -7);
            //a.Offset(-1,-1);
            Rectangle r = new Rectangle(a, new Size(0, 0));
            Rectangle d = new Rectangle(b, new Size(15, 15));
            return d.Contains(r);
        }

        /// <summary>
        /// Invalidates the connector
        /// 使连接器失效
        /// </summary>
        public override void Invalidate()
        {
            Point p = point;
            p.Offset(-5, -5);
            site.Invalidate(new Rectangle(p, new Size(10, 10)));
        }

        /// <summary>
        /// Moves the connector with the given shift-vector
        /// 使用给定的移位向量移动连接器
        /// </summary>
        /// <param name="p"></param>
        public override void Move(Point p)
        {
            this.point.X += p.X;
            this.point.Y += p.Y;
            for (int k = 0; k < attachedConnectors.Count; k++)
                attachedConnectors[k].Move(p);
        }

        /// <summary>
        /// Attaches the given connector to this connector
        /// 将给定连接器连接到此连接器
        /// </summary>
        /// <param name="c"></param>
        public void AttachConnector(Connector c)
        {
            //remove from the previous, if any
            if (c.attachedTo != null)
            {
                c.attachedTo.attachedConnectors.Remove(c);
            }
            attachedConnectors.Add(c);
            c.attachedTo = this;

        }

        /// <summary>
        /// Detaches the given connector from this connector
        /// </summary>
        /// <param name="c"></param>
        public void DetachConnector(Connector c)
        {
            attachedConnectors.Remove(c);
        }

        /// <summary>
        /// Releases this connector from any other
        /// </summary>
        public bool Release()
        {
            if (this.attachedTo != null)
            {
                this.attachedTo.attachedConnectors.Remove(this);
                this.attachedTo = null;
                return true;
            }

            return false;
        }

        #endregion
    }
}
