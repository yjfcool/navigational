namespace Navigational.WindowsForms.NavigationalControls.TrackEditor
{
    partial class TrackEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackEditor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.navigationalMapControl1 = new Navigational.WindowsForms.NavigationalMap.NavigationalMapControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.navigationalMapControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 480);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // navigationalMapControl1
            // 
            this.navigationalMapControl1.Bearing = 0F;
            this.navigationalMapControl1.CanDragMap = true;
            this.navigationalMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationalMapControl1.GrayScaleMode = false;
            this.navigationalMapControl1.LevelsKeepInMemmory = 5;
            this.navigationalMapControl1.Location = new System.Drawing.Point(3, 3);
            this.navigationalMapControl1.MarkersEnabled = true;
            this.navigationalMapControl1.MaxZoom = 15;
            this.navigationalMapControl1.MinZoom = 7;
            this.navigationalMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.navigationalMapControl1.Name = "navigationalMapControl1";
            this.navigationalMapControl1.PolygonsEnabled = true;
            //this.navigationalMapControl1.Position = ((GMap.NET.PointLatLng)(resources.GetObject("navigationalMapControl1.Position")));
            this.navigationalMapControl1.RasterBaseFolder = "C:\\AmudAnan\\Tiles";
            this.navigationalMapControl1.RetryLoadTile = 0;
            this.navigationalMapControl1.RoutesEnabled = true;
            this.navigationalMapControl1.ShowTileGridLines = false;
            this.navigationalMapControl1.Size = new System.Drawing.Size(634, 474);
            this.navigationalMapControl1.TabIndex = 0;
            this.navigationalMapControl1.Zoom = 7D;
            // 
            // TrackEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TrackEditor";
            this.Size = new System.Drawing.Size(640, 480);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NavigationalMap.NavigationalMapControl navigationalMapControl1;
    }
}
