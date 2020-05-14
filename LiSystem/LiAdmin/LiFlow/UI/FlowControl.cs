using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LiFlow.Collections;
using LiFlow.Element;
using LiFlow.Enums;
using LiFlow.Element.Shapes;
using LiFlow.Element.Flow;

namespace LiFlow.UI
{
    /// <summary>
    /// A 'light' version of the Netron graph control
    /// without all the advanced diagramming stuff
    /// see however http://netron.sf.net for more info.
    /// This control shows the simplicity with which you can still achieve good results,
    /// it's a toy-model to explore and can eventually help you if you want to go for a 
    /// bigger adventure in the full Netron control.
    /// Question and comments are welcome via the forum of The Netron Project or mail me
    /// [Illumineo@users.sourceforge.net]
    /// 
    /// Thank you for downloading the code and your feedback!
    /// 
    /// Netron图形控件的“轻”版本
    ////没有所有先进的图表材料
    ////但是，有关更多信息，请参见http://netron.sf.net。
    ////这个控件显示了仍然可以获得良好结果的简单性，
    ////这是一个值得探索的玩具模型，如果你想
    ////更大的冒险在完全的尼特龙控制。
    ////欢迎通过Netron项目论坛或邮件向我提出问题和意见
    /// </summary>
    public partial class FlowControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Events and delegates
        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void AddNode(object ent);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event AddNode OnAddNode;

        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void RemoveNode(object ent);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event RemoveNode OnRemoveNode;
        #endregion

        #region Fields

        /// <summary>
        /// the entity hovered by the mouse
        /// 鼠标悬停的实体
        /// </summary>
        protected Entity hoveredEntity;

        /// <summary>
        /// the unique entity currently selected
        /// 当前选定的唯一实体
        /// </summary>
        protected Entity selectedEntity;

        /// <summary>
        /// the collection of shapes on the canvas
        /// 画布上的形状集合
        /// </summary>
        protected ShapeCollection shapes;

        /// <summary>
        /// A simple, general purpose random generator
        /// 一个简单的通用随机发生器
        /// </summary>
        protected Random rnd;

        /// <summary>
        /// just a reference point for the OnMouseDown event
        /// 只是OnMouseDown事件的参考点
        /// </summary>
        protected Point refp;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the shape collection
        /// 获取或设置形状集合
        /// </summary>
        public ShapeCollection Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        public FlowControl()
        {
            //double-buffering
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            //init the collections
            shapes = new ShapeCollection();

            //init the randomizer
            rnd = new Random();
        }

        #endregion

        #region Methods


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

        }


        #region Menu handlers
        #endregion

        /// <summary>
        /// Paints the control
        /// </summary>
        /// <remarks>
        /// If you switch the painting order of connections and shapes the connection line
        /// will be underneath/above the shape
        /// </remarks>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //use the best quality, with a performance penalty
            //使用最好的质量，并对性能进行惩罚
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //loop over the shapes and draw
            //画形状
            for (int k = 0; k < shapes.Count; k++)
            {
                shapes[k].Paint(g);
            }


        }


        /// <summary>
        /// Adds a shape to the canvas or diagram
        /// 向画布或图表添加形状
        /// 动态添加
        /// </summary>
        /// <param name="shape"></param>
        public ShapeBase AddShape(ShapeBase shape)
        {
            shapes.Add(shape);
            shape.Site = this;
            if (OnAddNode != null)
                OnAddNode(shape);

            this.Invalidate();
            return shape;
        }

        /// <summary>
        /// Adds a shape to the canvas or diagram
        /// 向画布或图表添加形状
        /// 动态添加
        /// </summary>
        /// <param name="shape"></param>
        public ShapeBase AddShape(ShapeBase shape, Point location)
        {
            shapes.Add(shape);
            shape.Site = this;
            shape.Location = location;
            if (OnAddNode != null)
                OnAddNode(shape);

            this.Invalidate();
            return shape;
        }

        public ShapeBase NewShape(ShapeTypes type, bool NoConnector = false)
        {

            ShapeBase shape = null;
            switch (type)
            {
                case ShapeTypes.Rectangular:
                    shape = new SimpleRectangle(this);
                    shape.ShapeColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    break;
                case ShapeTypes.Oval:
                    shape = new OvalShape(this);
                    shape.ShapeColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    break;
                case ShapeTypes.RoundRect:
                    shape = new RoundRectShape(this);
                    shape.ShapeColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    break;
                case ShapeTypes.Diamond:
                    shape = new DiamondShape(this);
                    shape.ShapeColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    break;
                case ShapeTypes.Start:
                    shape = new StartElement(this, NoConnector);
                    break;
                case ShapeTypes.End:
                    shape = new EndElement(this, NoConnector);
                    break;
                case ShapeTypes.Node:
                    shape = new NodeElement(this, NoConnector);
                    break;
                case ShapeTypes.Condition:
                    shape = new ConditionElement(this, NoConnector);
                    break;
                case ShapeTypes.TextLabel:
                    shape = new TextLabel(this);
                    shape.ShapeColor = Color.Transparent;
                    shape.Text = "A text label (change the text in the property grid)";
                    shape.Width = 350;
                    shape.Height = 30;
                    break;


            }
            return shape;
        }

        /// <summary>
        /// Adds a predefined shape
        /// 添加预定义形状
        /// 静态添加
        /// </summary>
        /// <param name="type"></param>
        public ShapeBase AddShape(ShapeTypes type, Point location, bool NoConnector = false)
        {
            ShapeBase shape = NewShape(type, NoConnector);

            if (shape == null) return null;
            shape.Location = location;
            shapes.Add(shape);
            if(OnAddNode != null)
                OnAddNode(shape);

            return shape;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="selectedEntity"></param>
        public virtual void deleteEntity(object selectedEntity)
        {

            if (selectedEntity != null)
            {
                if (typeof(ShapeBase).IsInstanceOfType(selectedEntity))
                {
                    ShapeBase selectedShapeBase = selectedEntity as ShapeBase;

                    this.shapes.Remove(selectedShapeBase);
                    OnRemoveNode(selectedShapeBase);
                    this.Invalidate();
                }
            }
        }

        #region Mouse event handlers

        /// <summary>
        /// Thanks to Mark Johnson and Radek Cervinka for corrections!
        /// </summary>
        /// <param name="oEnt"></param>
        public void UpdateHovered(Entity oEnt)
        {
            //先取消上一个，再重新选
            if (hoveredEntity != null)
            {
                hoveredEntity.hovered = false;
                hoveredEntity.Invalidate();
            }
            oEnt.hovered = true;
            hoveredEntity = oEnt;
            hoveredEntity.Invalidate();
        }

        public void CancelHovered(Entity oEnt)
        {
            oEnt.hovered = false;
            hoveredEntity = oEnt;
            hoveredEntity.Invalidate();
        }

        /// <summary>
        /// Thanks to Mark Johnson and Radek Cervinka for corrections!
        /// </summary>
        /// <param name="oEnt"></param>
        public void UpdateSelected(Entity oEnt)
        {
            //先取消上一个，再重新选
            if (selectedEntity != null)
            {
                selectedEntity.IsSelected = false;
                selectedEntity.Invalidate();
            }
            selectedEntity = oEnt;
            oEnt.IsSelected = true;
            oEnt.Invalidate();
        }

        /// <summary>
        /// Handles the mouse-down event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point p = new Point(e.X, e.Y);
            //test for shapes
            //形状
            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k].Hit(p))
                {
                    refp = p;
                    return;
                }
            }
        }
        
        /// <summary>
        /// Handles the mouse-move event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point p = new Point(e.X, e.Y);

            bool bFlag = false;
            //hovering stuff
            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k].Hit(p))
                {
                    bFlag = true;
                    UpdateHovered(shapes[k]);
                }
            }

            //if (!bFlag)
            //{
            //    for (int k = 0; k < shapes.Count; k++)
            //    {
            //        CancelHovered(shapes[k]);
            //    }
            //}
        }


        #endregion
        #endregion

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public ShapeBase getFoucsedEntity()
        {
            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k].Hit(refp))
                {
                    return shapes[k];
                }
            }
            return null;
        }
    }

}
