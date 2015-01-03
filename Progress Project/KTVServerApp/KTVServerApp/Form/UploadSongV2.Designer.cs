namespace KTVServerApp
{
    partial class UploadSongV2
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
            this.components = new System.ComponentModel.Container();
            this.dgvSong = new System.Windows.Forms.DataGridView();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnUploadSong = new System.Windows.Forms.Button();
            this.Info = new System.Windows.Forms.StatusStrip();
            this.tsInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.cmsAdding = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSong)).BeginInit();
            this.Info.SuspendLayout();
            this.cmsAdding.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSong
            // 
            this.dgvSong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSong.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSong.Location = new System.Drawing.Point(12, 41);
            this.dgvSong.Name = "dgvSong";
            this.dgvSong.Size = new System.Drawing.Size(865, 291);
            this.dgvSong.TabIndex = 0;
            this.dgvSong.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSong_ColumnHeaderMouseClick);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Image = global::KTVServerApp.Properties.Resources.Search_32;
            this.btnBrowse.Location = new System.Drawing.Point(294, 347);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(88, 36);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnUploadSong
            // 
            this.btnUploadSong.Image = global::KTVServerApp.Properties.Resources.Save_32;
            this.btnUploadSong.Location = new System.Drawing.Point(178, 347);
            this.btnUploadSong.Name = "btnUploadSong";
            this.btnUploadSong.Size = new System.Drawing.Size(108, 36);
            this.btnUploadSong.TabIndex = 3;
            this.btnUploadSong.Text = "UploadSong";
            this.btnUploadSong.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUploadSong.UseVisualStyleBackColor = true;
            this.btnUploadSong.Click += new System.EventHandler(this.btnUploadSong_click);
            // 
            // Info
            // 
            this.Info.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.Info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Info.Dock = System.Windows.Forms.DockStyle.None;
            this.Info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsInfo});
            this.Info.Location = new System.Drawing.Point(0, 406);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(17, 22);
            this.Info.TabIndex = 4;
            // 
            // tsInfo
            // 
            this.tsInfo.Name = "tsInfo";
            this.tsInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(97, 347);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 36);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(16, 347);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 36);
            this.btnPause.TabIndex = 6;
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // cmsAdding
            // 
            this.cmsAdding.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.cmsAdding.Name = "cmsAdding";
            this.cmsAdding.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsAdding.Size = new System.Drawing.Size(124, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItem1.Text = "Add New";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(382, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.TabIndex = 71;
            this.label3.Text = "Upload Song";
            // 
            // UploadSongV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(905, 428);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.btnUploadSong);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.dgvSong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UploadSongV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UploadSongV2";
            this.Load += new System.EventHandler(this.UploadSongV2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSong)).EndInit();
            this.Info.ResumeLayout(false);
            this.Info.PerformLayout();
            this.cmsAdding.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSong;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnUploadSong;
        private System.Windows.Forms.StatusStrip Info;
        private System.Windows.Forms.ToolStripStatusLabel tsInfo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ContextMenuStrip cmsAdding;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label label3;

    }
}