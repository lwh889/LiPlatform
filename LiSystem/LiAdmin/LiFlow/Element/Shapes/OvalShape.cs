﻿using System;
using System.Drawing;
using LiFlow.UI;

namespace LiFlow.Element.Shapes
{
    /// <summary>
    /// A simple rectangular shape
    /// 简单的卵形
    /// </summary>
    [Serializable]
    public class OvalShape : ShapeBase
    {
        #region Fields
        /// <summary>
        /// holds the bottom connector
        /// 固定底部连接器
        /// </summary>
        protected Connector cBottom, cLeft, cRight, cTop;
        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="s"></param>
        public OvalShape(FlowControl s)
            : base(s)
        {
            cBottom = new Connector(new Point((int)(rectangle.Left + rectangle.Width / 2), rectangle.Bottom));
            cBottom.Site = this.site;
            cBottom.Name = "Bottom connector";
            cBottom.Index = 0;
            cBottom.ShapeBase = this;
            connectors.Add(cBottom);

            cLeft = new Connector(new Point(rectangle.Left, (int)(rectangle.Top + rectangle.Height / 2)));
            cLeft.Site = this.site;
            cLeft.Name = "Left connector";
            cLeft.Index = 1;
            cLeft.ShapeBase = this;
            connectors.Add(cLeft);

            cRight = new Connector(new Point(rectangle.Right, (int)(rectangle.Top + rectangle.Height / 2)));
            cRight.Site = this.site;
            cRight.Name = "Right connector";
            cRight.Index = 2;
            cRight.ShapeBase = this;
            connectors.Add(cRight);

            cTop = new Connector(new Point((int)(rectangle.Left + rectangle.Width / 2), rectangle.Top));
            cTop.Site = this.site;
            cTop.Name = "Top connector";
            cTop.Index = 3;
            cTop.ShapeBase = this;
            connectors.Add(cTop);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tests whether the mouse hits this shape
        /// 测试鼠标是否击中此形状
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool Hit(System.Drawing.Point p)
        {
            Rectangle r = new Rectangle(p, new Size(5, 5));
            return rectangle.Contains(r);
        }



        /// <summary>
        /// Paints the shape on the canvas
        /// 在画布上画形状
        /// </summary>
        /// <param name="g"></param>
        public override void Paint(System.Drawing.Graphics g)
        {
            g.FillEllipse(shapeBrush, rectangle);


            if (hovered || isSelected)
                g.DrawEllipse(new Pen(Color.Red, 2F), rectangle);
            else
                g.DrawEllipse(blackPen, rectangle);
            for (int k = 0; k < connectors.Count; k++)
            {
                connectors[k].Paint(g);
            }
            //well, a lot should be said here like
            //the fact that one should measure the text before drawing it,
            //resize the width and height if the text if bigger than the rectangle,
            //alignment can be set and changes the drawing as well...
            //here we keep it really simple:
            float stringWidth = g.MeasureString(text, font).Width;
            if (text != string.Empty)
                g.DrawString(text, font, Brushes.Black, (rectangle.X + (rectangle.Width / 2)) - (stringWidth / 2), rectangle.Y + (rectangle.Height / 2) - font.Size / 2);
        }

        /// <summary>
        /// Invalidates the shape
        /// </summary>
        public override void Invalidate()
        {
            Rectangle r = rectangle;
            r.Offset(-5, -5);
            r.Inflate(20, 20);
            site.Invalidate(r);
        }
        public override void Resize(int width, int height)
        {
            base.Resize(width, height);
            cBottom.Point = new Point((int)(rectangle.Left + rectangle.Width / 2), rectangle.Bottom);
            cLeft.Point = new Point(rectangle.Left, (int)(rectangle.Top + rectangle.Height / 2));
            cRight.Point = new Point(rectangle.Right, (int)(rectangle.Top + rectangle.Height / 2));
            cTop.Point = new Point((int)(rectangle.Left + rectangle.Width / 2), rectangle.Top);
            Invalidate();
        }

        #endregion
    }
}