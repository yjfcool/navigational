using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Navigational.WindowsForms.NavigationalControls.SlidingPanel
{
    public partial class SlidingPanel : UserControl
    {
        private bool _isCollapsed = true;
        private Rectangle _buttonRectangle = new Rectangle();

        /// <summary>
        /// Gets or sets if the control is collapsed.
        /// </summary>
        public bool Collapsed
        {
            get
            {
                return _isCollapsed;
            }
            set
            {
                _isCollapsed = value;
            }
        }

        public SlidingPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (Collapsed == false)
            {
                LinearGradientBrush brush = new LinearGradientBrush(e.ClipRectangle, SystemColors.GradientActiveCaption, SystemColors.WindowFrame, 45.0f);
                e.Graphics.FillRectangle(brush, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 40, e.ClipRectangle.Height);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            PaintButton(e.ClipRectangle, e.Graphics);

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_buttonRectangle.Contains(new Point(e.X, e.Y)))
            {
                Collapsed = !Collapsed;
                Invalidate();
            }
        }

        private void PaintButton(Rectangle rectangle, Graphics graphics)
        {
            int middle = (rectangle.Bottom - rectangle.Top) / 2;
            Image buttonImage = null;
            if (Collapsed)
            {
                _buttonRectangle = new Rectangle(-10, middle - 20, 40, 40);
                buttonImage = Properties.Resources.feature_nextButton;
            }
            else
            {
                _buttonRectangle = new Rectangle(rectangle.Width-40, middle - 20, 40, 40);
                buttonImage = Properties.Resources.feature_nextButton;
            }
            graphics.DrawImage(buttonImage, _buttonRectangle);
        }
    }
}
