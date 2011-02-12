namespace Navigational.WindowsForms
{
    partial class Form1
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
            this.trackEditor1 = new Navigational.WindowsForms.NavigationalControls.TrackEditor.TrackEditor();
            this.SuspendLayout();
            // 
            // trackEditor1
            // 
            this.trackEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackEditor1.Location = new System.Drawing.Point(0, 0);
            this.trackEditor1.Name = "trackEditor1";
            this.trackEditor1.Size = new System.Drawing.Size(624, 442);
            this.trackEditor1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.trackEditor1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private NavigationalControls.TrackEditor.TrackEditor trackEditor1;



    }
}

