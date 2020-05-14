using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;
using System.Windows.Forms;
using System.ComponentModel;

namespace LiContexts.Model
{

    public class PageFormModel : IDisposable
    {
        public static PageFormModel getInstance(int id,   RibbonForm liForm, string typeStr, string code = "", bool bShowStatus=true, bool bReadOnly= false)
        {
            PageFormModel pageFormModel = new PageFormModel();
            pageFormModel.id = id;
            pageFormModel.code = code;
            pageFormModel.bReadOnly = bReadOnly;
            pageFormModel.bShowStatus = bShowStatus;
            pageFormModel.text = liForm.Text;
            pageFormModel.type = typeStr;
            pageFormModel.liForm = liForm;

            return pageFormModel;
        }

        //public static PageFormModel getInstance(int id,RibbonForm liForm, string typeStr, string code = "", bool bShowStatus = true)
        //{
        //    return getInstance(id, liForm, typeStr, code, bShowStatus, false);
        //}

        public static PageFormModel getInstance_ReadOnly(int id,  RibbonForm liForm, string typeStr, string code = "", bool bShowStatus = true)
        {
            return getInstance(id, liForm, typeStr, code, bShowStatus, true);
        }
        /// <summary>
        /// ID
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool bReadOnly { set; get; }

        /// <summary>
        /// 是否显示状态
        /// </summary>
        [DefaultValue(true)]
        public bool bShowStatus { set; get; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string text { set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string type { set; get; }

        public RibbonForm _liForm;
        public RibbonForm liForm
        {
            set
            {
                _liForm = value;
                _liForm.Text = getCaption(this);
            }
            get { return _liForm; }
        }

        public void setFormCaption(Form form, PageFormModel pageFormModel)
        {
            form.Text = getCaption(pageFormModel);
        }

        public static string getCaption(PageFormModel pageFormModel)
        {
            string caption = pageFormModel.text;
            if (pageFormModel.id <= 0)
            {
                caption = string.Format("{0}{1}",pageFormModel.bShowStatus == true ? "[新增]" : "", pageFormModel.text);
            }
            else if (pageFormModel.id > 0 && pageFormModel.bReadOnly)
            {
                caption = string.Format("{0}{1}", pageFormModel.bShowStatus == true ? "[只读]" : "", pageFormModel.text);
            }
            else if (pageFormModel.id > 0 && !pageFormModel.bReadOnly)
            {
                caption = string.Format("{0}{1}", pageFormModel.bShowStatus == true ? "[编辑]" : "", pageFormModel.text);
            }
            return caption;
        }

        public bool Equals(PageFormModel other)
        {
            //this非空，obj如果为空，则返回false
            if (ReferenceEquals(null, other)) return false;

            //如果为同一对象，必然相等
            if (ReferenceEquals(this, other)) return true;

            //对比各个字段值
            if (!int.Equals(id, other.id))
                return false;
            if (!string.Equals(code, other.code, StringComparison.InvariantCulture))
                return false;
            if (!string.Equals(type, other.type, StringComparison.InvariantCulture))
                return false;

            //如果基类不是从Object继承，需要调用base.Equals(other)
            //如果从Object继承，直接返回true
            return true;
        }

        public override bool Equals(object obj)
        {
            //this非空，obj如果为空，则返回false
            if (ReferenceEquals(null, obj)) return false;

            //如果为同一对象，必然相等
            if (ReferenceEquals(this, obj)) return true;

            //如果类型不同，则必然不相等
            if (obj.GetType() != this.GetType()) return false;

            //调用强类型对比
            return Equals((PageFormModel)obj);
        }

        public override string ToString()
        {
            return string.Format("ID:{0},Code:{1},Type:{2}", id, code, type);
        }

        public void Dispose() // NOT virtual
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent finalizer from running.
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (liForm != null)
                {
                    liForm.Close();
                    liForm = null;
                }
            }
            // Release unmanaged resources owned by (just) this object.
            // ...
        }

        ~PageFormModel()
        {
            Dispose(false);
        }

        
    }
}
