namespace Navigational.WindowsForms.NavigationalControls.FileManager
{
    partial class FileManagerItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileManagerItem));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelDateTimeField = new System.Windows.Forms.Label();
            this.labelDescriptionField = new System.Windows.Forms.Label();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelGPSFileVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelGPSFileVersion, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelFilename, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.labelTime, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelDescription, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelDateTimeField, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelDescriptionField, 1, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // labelTime
            // 
            resources.ApplyResources(this.labelTime, "labelTime");
            this.labelTime.Name = "labelTime";
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // labelDateTimeField
            // 
            resources.ApplyResources(this.labelDateTimeField, "labelDateTimeField");
            this.labelDateTimeField.Name = "labelDateTimeField";
            // 
            // labelDescriptionField
            // 
            resources.ApplyResources(this.labelDescriptionField, "labelDescriptionField");
            this.labelDescriptionField.AutoEllipsis = true;
            this.labelDescriptionField.Name = "labelDescriptionField";
            // 
            // labelFilename
            // 
            resources.ApplyResources(this.labelFilename, "labelFilename");
            this.labelFilename.AutoEllipsis = true;
            this.labelFilename.Name = "labelFilename";
            // 
            // labelGPSFileVersion
            // 
            resources.ApplyResources(this.labelGPSFileVersion, "labelGPSFileVersion");
            this.labelGPSFileVersion.Name = "labelGPSFileVersion";
            // 
            // FileManagerItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "FileManagerItem";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label labelGPSFileVersion;
        private System.Windows.Forms.Label labelDateTimeField;
        private System.Windows.Forms.Label labelDescriptionField;
    }
}
