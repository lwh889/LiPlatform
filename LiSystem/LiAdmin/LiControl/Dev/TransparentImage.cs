﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LiControl.Dev
{
    public abstract class TransparentImage : UserControl
    {
        protected Graphics graphics;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // 实现透明样式
                return cp;
            }
        }
        public TransparentImage()
        {
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            this.graphics = e.Graphics;
            this.graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            OnDraw();
        }
        protected abstract void OnDraw();
    }
}
