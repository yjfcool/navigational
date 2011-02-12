namespace Navigational.WindowsForms.NavigationalControls.SlidingPanel
{
    partial class TestingSliding
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.slidingPanel1 = new Navigational.WindowsForms.NavigationalControls.SlidingPanel.SlidingPanel();
            this.SuspendLayout();
            // 
            // slidingPanel1
            // 
            this.slidingPanel1.Collapsed = false;
            this.slidingPanel1.Location = new System.Drawing.Point(12, 12);
            this.slidingPanel1.Name = "slidingPanel1";
            this.slidingPanel1.Size = new System.Drawing.Size(180, 229);
            this.slidingPanel1.TabIndex = 0;
            // 
            // TestingSliding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.slidingPanel1);
            this.Name = "TestingSliding";
            this.Text = "TestingSliding";
            this.ResumeLayout(false);

        }

        #endregion

        private SlidingPanel slidingPanel1;
    }
}