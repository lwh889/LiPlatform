using System;
using System.Drawing;
using System.ComponentModel;
using LiFlow.UI;

namespace LiFlow.Element
{
    /// <summary>
    /// Abstract base class for every object part of the diagram
    /// 图中每个对象部分的抽象基类
    /// </summary>
    [Serializable]
    public abstract class Entity
    {
        #region Fields
        /// <summary>
        /// tells whether the current entity is hovered by the mouse
        /// 指示当前实体是否被鼠标悬停
        /// </summary>
        protected internal bool hovered = false;
        /// <summary>
        /// the control to which the eneity belongs
        /// 利益所属的控制权
        /// </summary>
        protected FlowControl site;
        /// <summary>
        /// tells whether the entity is selected
        /// 指示是否选择实体
        /// </summary>
        protected bool isSelected = false;

        /// <summary>
        /// Default font for drawing text
        /// 绘图文本的默认字体
        /// </summary>
        protected Font font = new Font("Verdana", 10F);

        /// <summary>
        /// Default black pen
        /// 默认黑色笔
        /// </summary>
        protected Pen blackPen = new Pen(Brushes.Black, 1F);

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether the entity is selected
        /// 获取或设置是否选择实体
        /// </summary>
        [Browsable(false)]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        /// <summary>
        /// Gets or sets the site of the entity
        /// 获取或设置实体的站点
        /// </summary>
        [Browsable(false)]
        public FlowControl Site
        {
            get { return site; }
            set { site = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        public Entity()
        {
        }

        /// <summary>
        /// Ctor with the site of the entity
        /// </summary>
        /// <param name="site"></param>
        public Entity(FlowControl site)
        {
            this.site = site;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Paints the entity on the control
        /// 在控件上绘制实体
        /// </summary>
        /// <param name="g">the graphics object to paint on</param>
        public abstract void Paint(Graphics g);
        /// <summary>
        /// Tests whether the shape is hit by the mouse
        /// 测试形状是否被鼠标击中
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public abstract bool Hit(Point p);
        /// <summary>
        /// Invalidates the entity
        /// 使实体无效
        /// </summary>
        public abstract void Invalidate();
        /// <summary>
        /// Moves the entity on the canvas
        /// 在画布上移动实体
        /// </summary>
        /// <param name="p">the shifting vector, not an absolute position!</param>
        public abstract void Move(Point p);

        #endregion
    }
}
