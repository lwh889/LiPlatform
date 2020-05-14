using System;
using System.Drawing;
using System.ComponentModel;
using LiFlow.Collections;
using LiFlow.UI;

namespace LiFlow.Element
{
    /// <summary>
    /// Base class for shapes
    /// </summary>
    [Serializable]
    public class ShapeBase : Entity
    {
        #region Fields
        /// <summary>
        /// the rectangle on which any shape lives
        /// 任何形状所有的矩形
        /// </summary>
        protected Rectangle rectangle;
        /// <summary>
        /// the backcolor of the shapes
        /// 图形背景
        /// </summary>
        protected Color shapeColor = Color.SteelBlue;
        /// <summary>
        /// the brush corresponding to the backcolor
        /// 背景色的画笔
        /// </summary>
        protected Brush shapeBrush;
        /// <summary>
        /// the collection of connectors onto which you can attach a connection
        /// 形状上可以连接的连接器集合
        /// </summary>
        protected ConnectorCollection connectors;
        /// <summary>
        /// the text on the shape
        /// 文字显示
        /// </summary>
        protected string text = string.Empty;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connectors of this shape
        /// 可以连接的连接器集合
        /// </summary>
        [Browsable(false)]
        public ConnectorCollection Connectors
        {
            get { return connectors; }
            set { connectors = value; }
        }

        /// <summary>
        /// Gets or sets the width of the shape
        /// 获取或设置形状的宽度
        /// </summary>
        [Browsable(true), Description("获取或设置形状的宽度"), Category("Layout")]
        public int Width
        {
            get { return this.rectangle.Width; }
            set { Resize(value, this.Height); }
        }

        /// <summary>
        /// Gets or sets the height of the shape
        /// 获取或设置形状的高度
        /// </summary>		
        [Browsable(true), Description("获取或设置形状的高度"), Category("Layout")]
        public int Height
        {
            get { return this.rectangle.Height; }
            set { Resize(this.Width, value); }
        }

        /// <summary>
        /// Gets or sets the text of the shape
        /// 获取或设置形状的文本
        /// </summary>
        [Browsable(true), Description("获取或设置形状的文本"), Category("Layout")]
        public string Text
        {
            get { return text; }
            set { text = value; this.Invalidate(); }
        }

        /// <summary>
        /// the x-coordinate of the upper-left corner
        /// 左上角的x坐标
        /// </summary>
        [Browsable(true), Description("左上角的x坐标"), Category("Layout")]
        public int X
        {
            get { return rectangle.X; }
            set
            {
                Point p = new Point(value - rectangle.X, rectangle.Y);
                this.Move(p);
                Site.Invalidate(); //note that 'this.Invalidate()' will not be enough
            }
        }

        /// <summary>
        /// the y-coordinate of the upper-left corner
        /// 左上角的y坐标
        /// </summary>
        [Browsable(true), Description("左上角的y坐标"), Category("Layout")]
        public int Y
        {
            get { return rectangle.Y; }
            set
            {
                Point p = new Point(rectangle.X, value - rectangle.Y);
                this.Move(p);
                Site.Invalidate();
            }
        }
        /// <summary>
        /// The backcolor of the shape
        /// 形状的背景色
        /// </summary>
        [Browsable(true), Description("形状的背景色"), Category("Layout")]
        public Color ShapeColor
        {
            get { return shapeColor; }
            set { shapeColor = value; SetBrush(); Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the location of the shape;
        /// 获取或设置形状的位置
        /// </summary>
        [Browsable(false)]
        public Point Location
        {
            get { return new Point(this.rectangle.X, this.rectangle.Y); }
            set
            {
                //we use the move method but it requires the delta value, not an absolute position!
                Point p = new Point(value.X - rectangle.X, value.Y - rectangle.Y);
                //if you'd use this it would indeed move the shape but not the connector s of the shape
                //this.rectangle.X = value.X; this.rectangle.Y = value.Y; Invalidate();
                this.Move(p);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default ctor
        /// </summary>
        public ShapeBase()
        {
            Init();
        }
        /// <summary>
        /// Constructor with the site of the shape
        /// </summary>
        /// <param name="site">the graphcontrol instance to which the shape is attached</param>
        public ShapeBase(FlowControl site)
            : base(site)
        {
            Init();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Summarizes the initialization used by the constructors
        /// </summary>
        private void Init()
        {
            rectangle = new Rectangle(0, 0, 100, 70);
            connectors = new ConnectorCollection();
            SetBrush();
        }


        /// <summary>
        /// Returns the connector hit by the mouse, if any
        /// 返回鼠标点击的连接器（如果有）
        /// </summary>
        /// <param name="p">the mouse coordinates</param>
        /// <returns>the connector hit by the mouse</returns>
        public Connector HitConnector(Point p)
        {
            for (int k = 0; k < connectors.Count; k++)
            {
                if (connectors[k].Hit(p))
                {
                    connectors[k].hovered = true;
                    connectors[k].Invalidate();
                    return connectors[k];
                }
                else
                {
                    connectors[k].hovered = false;
                    connectors[k].Invalidate();

                }


            }
            return null;

        }

        /// <summary>
        /// Sets the brush corresponding to the backcolor
        /// 设置与背景色对应的画笔
        /// </summary>
        private void SetBrush()
        {
            shapeBrush = new SolidBrush(shapeColor);

        }

        /// <summary>
        /// Overrides the abstract paint method
        /// </summary>
        /// <param name="g">a graphics object onto which to paint</param>
        public override void Paint(System.Drawing.Graphics g)
        {
            return;
        }

        /// <summary>
        /// Override the abstract Hit method
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool Hit(System.Drawing.Point p)
        {
            return false;
        }

        /// <summary>
        /// Overrides the abstract Invalidate method
        /// </summary>
        public override void Invalidate()
        {
            site.Invalidate(rectangle);
        }



        /// <summary>
        /// Moves the shape with the given shift
        /// </summary>
        /// <param name="p">represent a shift-vector, not the absolute position!</param>
        public override void Move(Point p)
        {
            this.rectangle.X += p.X;
            this.rectangle.Y += p.Y;
            for (int k = 0; k < this.connectors.Count; k++)
            {
                connectors[k].Move(p);
            }
            this.Invalidate();
        }

        /// <summary>
        /// Resizes the shape and moves the connectors
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public virtual void Resize(int width, int height)
        {
            this.rectangle.Height = height;
            this.rectangle.Width = width;
        }


        #endregion
    }
}
