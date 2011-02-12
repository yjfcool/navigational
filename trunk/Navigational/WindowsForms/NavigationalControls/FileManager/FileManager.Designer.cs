namespace Navigational.WindowsForms.NavigationalControls.FileManager
{
    partial class FileManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManager));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.tableLayoutPanelFiles = new Navigational.WindowsForms.NavigationalControls.CustomScrollableTableLayoutPanel();
            this.navigationalMapControl1 = new Navigational.WindowsForms.NavigationalMap.NavigationalMapControl();
            this.slidingPanel1 = new Navigational.WindowsForms.NavigationalControls.SlidingPanel.SlidingPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.navigationalMapControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.slidingPanel1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.vScrollBar1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelFiles, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // vScrollBar1
            // 
            resources.ApplyResources(this.vScrollBar1, "vScrollBar1");
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // tableLayoutPanelFiles
            // 
            resources.ApplyResources(this.tableLayoutPanelFiles, "tableLayoutPanelFiles");
            this.tableLayoutPanelFiles.Name = "tableLayoutPanelFiles";
            this.tableLayoutPanelFiles.Layout += new System.Windows.Forms.LayoutEventHandler(this.tableLayoutPanelFiles_Layout);
            // 
            // navigationalMapControl1
            // 
            this.navigationalMapControl1.Bearing = 0F;
            this.navigationalMapControl1.CanDragMap = true;
            resources.ApplyResources(this.navigationalMapControl1, "navigationalMapControl1");
            this.navigationalMapControl1.GrayScaleMode = false;
            this.navigationalMapControl1.LevelsKeepInMemmory = 5;
            this.navigationalMapControl1.MarkersEnabled = true;
            this.navigationalMapControl1.MaxZoom = 17;
            this.navigationalMapControl1.MinZoom = 2;
            this.navigationalMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.navigationalMapControl1.Name = "navigationalMapControl1";
            this.navigationalMapControl1.PolygonsEnabled = true;
            this.navigationalMapControl1.Position = ((GMap.NET.PointLatLng)(resources.GetObject("navigationalMapControl1.Position")));
            this.navigationalMapControl1.RasterBaseFolder = "C:\\AmudAnan\\Tiles";
            this.navigationalMapControl1.RetryLoadTile = 0;
            this.navigationalMapControl1.RoutesEnabled = true;
            this.navigationalMapControl1.ShowTileGridLines = false;
            this.navigationalMapControl1.Zoom = 2D;
            // 
            // slidingPanel1
            // 
            this.slidingPanel1.Collapsed = false;
            resources.ApplyResources(this.slidingPanel1, "slidingPanel1");
            this.slidingPanel1.Name = "slidingPanel1";
            // 
            // FileManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "FileManager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        //private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFiles;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        
        private CustomScrollableTableLayoutPanel tableLayoutPanelFiles;
        private NavigationalMap.NavigationalMapControl navigationalMapControl1;
        private SlidingPanel.SlidingPanel slidingPanel1;
    }
}
