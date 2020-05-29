using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LiControl.Dev
{
    public class TransparentImageExtend : TransparentImage
    {
        public TransparentImageExtend()
        {
            //this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
        }
        protected override void OnDraw()
        {
            // 获取图像
            Image imageModel = global::LiControl.Properties.Resources.U8_audit2_32x32;
            //int width = imageModel.Size.Width;
            //int height = imageModel.Size.Height;
            int width = this.Width;
            int height = this.Height;
            Rectangle recModel = new Rectangle(0, 0, width, height);

            this.graphics.DrawImage(imageModel, recModel);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TransparentImageExtend
            // 
            this.Name = "TransparentImageExtend";
            this.Size = new System.Drawing.Size(391, 147);
            this.ResumeLayout(false);

        }
    }
}
