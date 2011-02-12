using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Navigational.WindowsForms.NavigationalControls
{
    public enum HiddenScrollBars
    {
        VerticalScrollBar,
        HorizontalScroolBar,
    }

    public class CustomScrollableTableLayoutPanel : TableLayoutPanel
    {
        private bool _isVerticalScrollHidden = false;
        private bool _isHorizontalScrollHidden = false;

        public CustomScrollableTableLayoutPanel()
            : base()
        {
            _isVerticalScrollHidden = true;
        }

        public CustomScrollableTableLayoutPanel(HiddenScrollBars hiddenScrollBars)
            : base()
        {
            switch (hiddenScrollBars)
            {
                case HiddenScrollBars.HorizontalScroolBar:
                    _isHorizontalScrollHidden = true;
                    break;
                case HiddenScrollBars.VerticalScrollBar:
                    _isVerticalScrollHidden = true;
                    break;
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int verticalScrollWidth = 0;
            int horizontalScrollWidth = 0;

            if (_isVerticalScrollHidden)
            {
                verticalScrollWidth = this.Size.Width - this.ClientSize.Width + 2;
            }
            if (_isHorizontalScrollHidden)
            {
                horizontalScrollWidth = this.Size.Height - this.ClientSize.Height + 2;
            }

            base.SetBoundsCore(x, y, width + verticalScrollWidth, height + horizontalScrollWidth, specified);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

    }
}
