using System;
using System.Drawing;

namespace LiFlow.Element
{
    /// <summary>
    /// Represents the connection between two connectors
    /// 表示两个连接器之间的连接
    /// </summary>
    [Serializable]
    public class Connection : Entity
    {

        #region Fields
        protected Connector from;
        protected Connector to;
        protected ShapeBase shapeBaseFrom;
        protected ShapeBase shapeBaseTo;
        protected int shapeBaseFromIndex;
        protected int shapeBaseToIndex;

        #endregion

        #region Properties
        public Connector From
        {
            get { return from; }
            set { from = value; }
        }

        public Connector To
        {
            get { return to; }
            set { to = value; }
        }

        /// <summary>
        /// 源节点
        /// </summary>
        public ShapeBase ShapeBaseFrom
        {
            get { return shapeBaseFrom; }
            set { shapeBaseFrom = value; }
        }

        /// <summary>
        /// 目标节点
        /// </summary>
        public ShapeBase ShapeBaseTo
        {
            get { return shapeBaseTo; }
            set { shapeBaseTo = value; }
        }

        /// <summary>
        /// 源哪个锚点
        /// </summary>
        public int ShapeBaseFromIndex
        {
            get { return shapeBaseFromIndex; }
            set { shapeBaseFromIndex = value; }
        }

        /// <summary>
        /// 目标哪个锚点
        /// </summary>
        public int ShapeBaseToIndex
        {
            get { return shapeBaseToIndex; }
            set { shapeBaseToIndex = value; }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        public Connection()
        {

        }
        /// <summary>
        /// Constructs a connection between the two given points
        /// </summary>
        /// <param name="from">the starting point of the connection</param>
        /// <param name="to">the end-point of the connection</param>
        public Connection(Point from, Point to)
        {
            this.from = new Connector(from);
            this.from.Name = "From";
            this.from.LineBase = this;

            this.to = new Connector(to);
            this.To.Name = "To";
            this.to.LineBase = this;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Paints the connection on the canvas
        /// </summary>
        /// <param name="g"></param>
        public override void Paint(System.Drawing.Graphics g)
        {
            //箭头
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap =
                new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true);
            //被鼠标悬停和选择，画线
            if (hovered || isSelected)
            {
                //红色
                Pen pen = new Pen(Color.Red, 2F);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pen.CustomEndCap = lineCap;
                g.DrawLine(pen, From.Point, To.Point);
            }
            else
            {
                //黑色
                Pen pen = new Pen(Color.Black,1F);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                pen.CustomEndCap = lineCap;
                g.DrawLine(pen, From.Point, To.Point);
            }
        }
        /// <summary>
        /// Invalidates the connection
        /// </summary>
        public override void Invalidate()
        {
            //画前后两点
            Rectangle f = new Rectangle(from.Point, new Size(10, 10));
            Rectangle t = new Rectangle(to.Point, new Size(10, 10));

            //重画两点之间的区域
            site.Invalidate(Rectangle.Union(f, t));
        }

        /// <summary>
        /// Tests if the mouse hits this connection
        /// 测试鼠标是否点击此连接
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool Hit(Point p)
        {
            Point p1, p2, s;
            RectangleF r1, r2;
            float o, u;
            p1 = from.Point; p2 = to.Point;

            // p1 must be the leftmost point.
            //p1必须是最左边的点
            if (p1.X > p2.X) { s = p2; p2 = p1; p1 = s; }

            r1 = new RectangleF(p1.X, p1.Y, 0, 0);
            r2 = new RectangleF(p2.X, p2.Y, 0, 0);
            //原地放大3个点
            r1.Inflate(3, 3);
            r2.Inflate(3, 3);
            //this is like a topological neighborhood
            //这就像一个拓扑邻域
            //the connection is shifted left and right
            //连接左右移动
            //and the point under consideration has to be in between.
            //考虑的问题必须介于两者之间
			//是否包含在矩形里			
            if (RectangleF.Union(r1, r2).Contains(p))
            {
                if (p1.Y < p2.Y) //SWNE
                {
                    o = r1.Left + (((r2.Left - r1.Left) * (p.Y - r1.Bottom)) / (r2.Bottom - r1.Bottom));
                    u = r1.Right + (((r2.Right - r1.Right) * (p.Y - r1.Top)) / (r2.Top - r1.Top));
                    return ((p.X > o) && (p.X < u));
                }
                else //NWSE
                {
                    o = r1.Left + (((r2.Left - r1.Left) * (p.Y - r1.Top)) / (r2.Top - r1.Top));
                    u = r1.Right + (((r2.Right - r1.Right) * (p.Y - r1.Bottom)) / (r2.Bottom - r1.Bottom));
                    return ((p.X > o) && (p.X < u));
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the connection with the given shift
        /// </summary>
        /// <param name="p"></param>
        public override void Move(Point p)
        {

        }


        #endregion

    }
}
