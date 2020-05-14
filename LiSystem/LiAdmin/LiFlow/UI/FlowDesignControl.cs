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
    public partial class FlowDesignControl : FlowControl
    {
        #region Events and delegates
        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void ShowProps(object ent);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event ShowProps OnShowProps;


        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void LiDoubleClick(object ent);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event LiDoubleClick OnLiDoubleClick;


        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void AttactConnector(int shapeBaseFormIndex, int shapeBaseToIndex, ShapeBase shapeBaseForm, ShapeBase shapeBaseTo);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event AttactConnector OnAttactConnector;

        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void RemoveConnector(ShapeBase shapeBaseForm, ShapeBase shapeBaseTo);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event RemoveConnector OnRemoveConnector;

        /// <summary>
        /// the info coming with the show-props event
        /// 
        /// </summary>
        public delegate void RemoveConnection(Connection connection);

        /// <summary>
        /// notifies the host to show the properties usually in the property grid
        /// </summary>
        public event RemoveConnection OnRemoveConnection;
        #endregion

        #region Fields

        /// <summary>
        /// 双击
        /// </summary>
        private DateTime m_LastClick = System.DateTime.Now;

        /// <summary>
        /// the collection of connections on the canvas
        /// 画布上的连接集合
        /// </summary>
        protected ConnectionCollection connections;

        /// <summary>
        /// whether we are tracking, i.e. moving something around
        /// 我们是否在追踪，即四处移动某物
        /// </summary>
        protected bool tracking = false;

        /// <summary>
        /// the context menu of the control
        /// 控件的上下文菜单
        /// </summary>
        protected ContextMenu menu;

        /// <summary>
        /// simple proxy for the propsgrid of the control
        /// 控件propsgrid的简单代理
        /// </summary>
        protected Proxy proxy;

        /// <summary>
        /// drawing a grid on the canvas?
        /// 在画布上绘制网格
        /// </summary>
        protected bool showGrid = true;

        /// <summary>
        /// just the default gridsize used in the paint-background method
        /// 只是paint background方法中使用的默认gridsize
        /// </summary>
        protected Size gridSize = new Size(10, 10);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether the grid is drawn on the canvas
        /// 获取或设置是否在画布上绘制网格
        /// </summary>
        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; Invalidate(); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default ctor
        /// </summary>
        public FlowDesignControl()
        {

            //init the collections
            connections = new ConnectionCollection();

            //menu
            menu = new ContextMenu();
            BuildMenu();
            this.ContextMenu = menu;

            //init the proxy
            proxy = new Proxy(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the context menu
        /// 右键菜单
        /// </summary>
        private void BuildMenu()
        {
            MenuItem mnuDelete = new MenuItem("Delete", new EventHandler(OnDelete));
            menu.MenuItems.Add(mnuDelete);

            MenuItem mnuProps = new MenuItem("Properties", new EventHandler(OnProps));
            menu.MenuItems.Add(mnuProps);

            MenuItem mnuDash = new MenuItem("-");
            menu.MenuItems.Add(mnuDash);

            MenuItem mnuNewConnection = new MenuItem("Add connection", new EventHandler(OnNewConnection));
            menu.MenuItems.Add(mnuNewConnection);

            MenuItem mnuShapes = new MenuItem("Shapes");
            menu.MenuItems.Add(mnuShapes);

            MenuItem mnuRecShape = new MenuItem("Rectangular", new EventHandler(OnRecShape));
            mnuShapes.MenuItems.Add(mnuRecShape);

            MenuItem mnuOvalShape = new MenuItem("Oval", new EventHandler(OnOvalShape));
            mnuShapes.MenuItems.Add(mnuOvalShape);

            MenuItem mnuTLShape = new MenuItem("Text label", new EventHandler(OnTextLabelShape));
            mnuShapes.MenuItems.Add(mnuTLShape);



        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;

            //画表格
            if (showGrid)
                ControlPaint.DrawGrid(g, this.ClientRectangle, gridSize, this.BackColor);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="selectedEntity"></param>
        public override void deleteEntity(object selectedEntity)
        {
            base.deleteEntity(selectedEntity);

            if (selectedEntity != null)
            {
                if (typeof(Connection).IsInstanceOfType(selectedEntity))
                {
                    this.connections.Remove(selectedEntity as Connection);
                    OnRemoveConnection(selectedEntity as Connection);
                    this.Invalidate();
                }
            }
        }

        #region Menu handlers
        /// <summary>
        /// Deletes the currently selected object from the canvas
        /// 从画布中删除当前选定的对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDelete(object sender, EventArgs e)
        {
            deleteEntity(selectedEntity);
        }


        /// <summary>
        /// Asks the host to show the props
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProps(object sender, EventArgs e)
        {
            object thing;
            if (this.selectedEntity == null)
                thing = this.proxy;
            else
                thing = selectedEntity;
            if (this.OnShowProps != null)
                OnShowProps(thing);

        }

        /// <summary>
        /// Adds a rectangular shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRecShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.Rectangular, refp);
        }

        private void OnOvalShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.Oval, refp);
        }
        private void OnTextLabelShape(object sender, EventArgs e)
        {
            AddShape(ShapeTypes.TextLabel, refp);
        }
        private void OnNewConnection(object sender, EventArgs e)
        {
            AddConnection(refp);
        }
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
            //similarly for the connections
            //画线和点
            for (int k = 0; k < connections.Count; k++)
            {
                connections[k].Paint(g);
                connections[k].From.Paint(g);
                connections[k].To.Paint(g);
            }
            //loop over the shapes and draw
            //画形状
            for (int k = 0; k < shapes.Count; k++)
            {
                shapes[k].Paint(g);
            }


        }


        #region AddConnection overloads
        /// <summary>
        /// Adds a connection to the diagram
        /// 添加连接线
        /// </summary>
        /// <param name="con"></param>
        public Connection AddConnection(Connection con)
        {
            connections.Add(con);
            con.Site = this;
            con.From.Site = this;
            con.To.Site = this;
            this.Invalidate();
            return con;
        }

        /// <summary>
        /// 添加连接线，只有起始点
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        public Connection AddConnection(Point startPoint)
        {
            //let's take a random point and assume this control is not infinitesimal (bigger than 20x20).
            Point rndPoint = new Point(rnd.Next(20, this.Width - 20), rnd.Next(20, this.Height - 20));
            Connection con = new Connection(startPoint, rndPoint);

            AddConnection(con);
            //use the end-point and simulate a dragging operation, see the OnMouseDown handler
            selectedEntity = con.To;
            tracking = true;
            refp = rndPoint;
            this.Invalidate();
            return con;

        }

        public Connection AddConnection(Connector from, Connector to)
        {
            Connection con = AddConnection(from.Point, to.Point);
            from.AttachConnector(con.From);
            to.AttachConnector(con.To);
            con.ShapeBaseFrom = from.ShapeBase;
            con.ShapeBaseTo = to.ShapeBase;
            con.ShapeBaseFromIndex = to.Index;
            con.ShapeBaseToIndex = from.Index;

            if(OnAttactConnector != null)
                OnAttactConnector(from.Index, to.Index, from.ShapeBase, to.ShapeBase);


            return con;

        }

        public Connection AddConnection(Point from, Point to)
        {
            Connection con = new Connection(from, to);
            this.AddConnection(con);
            return con;
        }
        #endregion

        #region Mouse event handlers

        /// <summary>
        /// Handles the mouse-down event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point p = new Point(e.X, e.Y);

            #region LMB & RMB

            //test for connectors
            //连接点
            for (int k = 0; k < connections.Count; k++)
            {
                if (connections[k].From.Hit(p))
                {
                    UpdateSelected(connections[k].From);
                    tracking = true;
                    refp = p;
                    return;
                }

                if (connections[k].To.Hit(p))
                {
                    UpdateSelected(connections[k].To);
                    tracking = true;
                    refp = p;
                    return;
                }
            }

            //test for connections
            //连接线
            for (int k = 0; k < this.connections.Count; k++)
            {
                if (connections[k].Hit(p))
                {
                    UpdateSelected(this.connections[k]);
                    if (OnShowProps != null)
                        OnShowProps(this.connections[k]);
                    if (e.Button == MouseButtons.Right)
                    {
                        if (OnShowProps != null)
                            OnShowProps(this);
                    }
                    return;
                }
            }
            //test for shapes
            //形状
            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k].Hit(p))
                {
                    //shapes[k].ShapeColor = Color.WhiteSmoke;
                    tracking = true;
                    UpdateSelected(shapes[k]);
                    refp = p;
                    if (OnShowProps != null)
                        OnShowProps(this.shapes[k]);
                    if (e.Button == MouseButtons.Right)
                    {
                        if (OnShowProps != null)
                            OnShowProps(this);
                    }
                    return;
                }
            }
            if (selectedEntity != null) selectedEntity.IsSelected = false;
            selectedEntity = null;
            Invalidate();
            refp = p; //useful for all kind of things
            //nothing was selected but we'll show the props of the control in this case
            //没有选择任何内容，但在本例中我们将显示控件的道具
            if (OnShowProps != null)
                OnShowProps(this.proxy);
            #endregion
        }

        /// <summary>
        /// Handles the mouse-up event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            //test if we connected a connection
            if (tracking)
            {
                Point p = new Point(e.X, e.Y);
                if (typeof(Connector).IsInstanceOfType(selectedEntity))
                {
                    Connector con;
                    Connector selectedConnector = selectedEntity as Connector;
                    for (int k = 0; k < shapes.Count; k++)
                    {
                        if ((con = shapes[k].HitConnector(p)) != null)
                        {
                            //con是被附在上面的形状的节点
                            //selectedEntity线条的To节点
                            con.AttachConnector(selectedConnector);
                            if (selectedConnector.Name == "From")
                            {
                                selectedConnector.LineBase.ShapeBaseFrom = con.ShapeBase;
                                selectedConnector.LineBase.ShapeBaseFromIndex = con.Index;
                            }
                            if (selectedConnector.Name == "To")
                            {
                                selectedConnector.LineBase.ShapeBaseTo = con.ShapeBase;
                                selectedConnector.LineBase.ShapeBaseToIndex = con.Index;
                            }

                            if (OnAttactConnector != null)
                                OnAttactConnector(selectedConnector.LineBase.ShapeBaseFromIndex, selectedConnector.LineBase.ShapeBaseToIndex, (selectedConnector).LineBase.ShapeBaseFrom, selectedConnector.LineBase.ShapeBaseTo);

                            tracking = false;
                            return;
                        }
                    }

                    ShapeBase shapeBase = null;
                    if (selectedConnector.Name == "To" && selectedConnector.AttachedTo != null)
                    {
                        if (selectedConnector.Release())
                        {
                            if (OnRemoveConnector != null)
                                OnRemoveConnector(selectedConnector.LineBase.ShapeBaseFrom, selectedConnector.LineBase.ShapeBaseTo);
                        }
                        selectedConnector.LineBase.ShapeBaseTo = null;
                    }
                    if (selectedConnector.Name == "From" && selectedConnector.AttachedTo != null)
                    {
                        if (selectedConnector.Release())
                        {
                            if(OnRemoveConnector != null)
                                OnRemoveConnector(selectedConnector.LineBase.ShapeBaseFrom, selectedConnector.LineBase.ShapeBaseTo);
                        }
                        selectedConnector.LineBase.ShapeBaseFrom = null;

                    }

                    

                }
                tracking = false;
            }

            //双击
            if (e.Button == MouseButtons.Left)
            {

                DateTime dt = DateTime.Now;
                TimeSpan span = dt.Subtract(m_LastClick);
                if (span.TotalMilliseconds < 300)  //如果两次点击的时间间隔小于300毫秒，则认为是双击
                {

                    object thing;
                    if (this.selectedEntity == null)
                        thing = this.proxy;
                    else
                        thing = selectedEntity;
                    if (this.OnShowProps != null)
                        OnLiDoubleClick(thing);

                    m_LastClick = dt.AddMinutes(-1);
                }
                else
                {
                    m_LastClick = dt;
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
            if (tracking)
            {
                selectedEntity.Move(new Point(p.X - refp.X, p.Y - refp.Y));
                refp = p;
                Invalidate();
                if (typeof(Connector).IsInstanceOfType(selectedEntity))
                {
                    //test the connecting points of hovered shapes
                    for (int k = 0; k < shapes.Count; k++)
                        shapes[k].HitConnector(p);
                }   // if(typeof(Connector).IsInstanceOfType(selectedEntity))
            }


            //hovering stuff
            for (int k = 0; k < shapes.Count; k++)
            {
                if (shapes[k].Hit(p))
                {
                    UpdateHovered(shapes[k]);
                }
            }

            for (int k = 0; k < connections.Count; k++)
            {
                if (connections[k].Hit(p))
                {
                    UpdateHovered(connections[k]);
                }
            }

            for (int k = 0; k < connections.Count; k++)
            {
                if (connections[k].From.Hit(p))
                {
                    UpdateHovered(connections[k].From);
                }

                if (connections[k].To.Hit(p))
                {
                    UpdateHovered(connections[k].To);
                }
            }
        }


        #endregion


        #endregion

        /// <summary>
        /// 全清空
        /// </summary>
        public void Clear()
        {
            connections.Clear();
            shapes.Clear();
            Refresh();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            this.Invalidate();
        }
    }

    /// <summary>
    /// Simple proxy class for the control to display only specific properties.
    /// Not as sophisticated as the property-bag of the full Netron-control
    /// but does the job in this simple context.
    /// 控件的简单代理类，仅显示特定属性。
//不像完全由Netron控制的属性包那么复杂
//但在这个简单的背景下做这项工作。
    /// </summary>
    public class Proxy
    {
        #region Fields
        private FlowDesignControl site;
        #endregion

        #region Constructor
        public Proxy(FlowDesignControl site)
        { this.site = site; }
        #endregion

        #region Methods
        [Browsable(false)]
        public FlowDesignControl Site
        {
            get { return site; }
            set { site = value; }
        }
        [Browsable(true), Description("The backcolor of the canvas"), Category("Layout")]
        public Color BackColor
        {
            get { return this.site.BackColor; }
            set { this.site.BackColor = value; }
        }

        [Browsable(true), Description("Gets or sets whether the grid is shown"), Category("Layout")]
        public bool ShowGrid
        {
            get { return this.site.ShowGrid; }
            set { this.site.ShowGrid = value; }
        }
        #endregion
    }
}
