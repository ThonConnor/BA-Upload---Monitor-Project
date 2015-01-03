namespace KTVServerApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadLocalSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadExcelSongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadSingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCountryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadProductionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadAlbumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolMonitoring = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolUploadSong = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.entryToolStripMenuItem,
            this.queryToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(876, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::KTVServerApp.Properties.Resources.System_Taskbar_Start_Menu_icon;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 24);
            this.toolStripMenuItem1.Text = "Menu";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Quit_128;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // entryToolStripMenuItem
            // 
            this.entryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadSongToolStripMenuItem,
            this.uploadSingerToolStripMenuItem,
            this.uploadCountryToolStripMenuItem,
            this.uploadProductionToolStripMenuItem,
            this.uploadCategoryToolStripMenuItem,
            this.uploadAlbumToolStripMenuItem});
            this.entryToolStripMenuItem.Name = "entryToolStripMenuItem";
            this.entryToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.entryToolStripMenuItem.Text = "&Entry";
            // 
            // uploadSongToolStripMenuItem
            // 
            this.uploadSongToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadLocalSongToolStripMenuItem,
            this.uploadExcelSongToolStripMenuItem});
            this.uploadSongToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Song_128;
            this.uploadSongToolStripMenuItem.Name = "uploadSongToolStripMenuItem";
            this.uploadSongToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadSongToolStripMenuItem.Text = "Upload Song";
            this.uploadSongToolStripMenuItem.Click += new System.EventHandler(this.uploadSongToolStripMenuItem_Click);
            // 
            // uploadLocalSongToolStripMenuItem
            // 
            this.uploadLocalSongToolStripMenuItem.Name = "uploadLocalSongToolStripMenuItem";
            this.uploadLocalSongToolStripMenuItem.Size = new System.Drawing.Size(192, 24);
            this.uploadLocalSongToolStripMenuItem.Text = "Upload Local Song";
            this.uploadLocalSongToolStripMenuItem.Click += new System.EventHandler(this.uploadLocalSongToolStripMenuItem_Click);
            // 
            // uploadExcelSongToolStripMenuItem
            // 
            this.uploadExcelSongToolStripMenuItem.Name = "uploadExcelSongToolStripMenuItem";
            this.uploadExcelSongToolStripMenuItem.Size = new System.Drawing.Size(192, 24);
            this.uploadExcelSongToolStripMenuItem.Text = "Upload Excel Song";
            this.uploadExcelSongToolStripMenuItem.Click += new System.EventHandler(this.uploadExcelSongToolStripMenuItem_Click);
            // 
            // uploadSingerToolStripMenuItem
            // 
            this.uploadSingerToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Singer_128;
            this.uploadSingerToolStripMenuItem.Name = "uploadSingerToolStripMenuItem";
            this.uploadSingerToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadSingerToolStripMenuItem.Text = "Upload Singer";
            this.uploadSingerToolStripMenuItem.Click += new System.EventHandler(this.uploadSingerToolStripMenuItem_Click);
            // 
            // uploadCountryToolStripMenuItem
            // 
            this.uploadCountryToolStripMenuItem.Name = "uploadCountryToolStripMenuItem";
            this.uploadCountryToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadCountryToolStripMenuItem.Text = "Upload Country";
            this.uploadCountryToolStripMenuItem.Click += new System.EventHandler(this.uploadCountryToolStripMenuItem_Click);
            // 
            // uploadProductionToolStripMenuItem
            // 
            this.uploadProductionToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Production_128;
            this.uploadProductionToolStripMenuItem.Name = "uploadProductionToolStripMenuItem";
            this.uploadProductionToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadProductionToolStripMenuItem.Text = "Upload Production";
            this.uploadProductionToolStripMenuItem.Click += new System.EventHandler(this.uploadProductionToolStripMenuItem_Click);
            // 
            // uploadCategoryToolStripMenuItem
            // 
            this.uploadCategoryToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Category_128;
            this.uploadCategoryToolStripMenuItem.Name = "uploadCategoryToolStripMenuItem";
            this.uploadCategoryToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadCategoryToolStripMenuItem.Text = "Upload Category";
            this.uploadCategoryToolStripMenuItem.Click += new System.EventHandler(this.uploadCategoryToolStripMenuItem_Click);
            // 
            // uploadAlbumToolStripMenuItem
            // 
            this.uploadAlbumToolStripMenuItem.Image = global::KTVServerApp.Properties.Resources.Album_128;
            this.uploadAlbumToolStripMenuItem.Name = "uploadAlbumToolStripMenuItem";
            this.uploadAlbumToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.uploadAlbumToolStripMenuItem.Text = "Upload Album";
            this.uploadAlbumToolStripMenuItem.Click += new System.EventHandler(this.uploadAlbumToolStripMenuItem_Click);
            // 
            // queryToolStripMenuItem
            // 
            this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
            this.queryToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
            this.queryToolStripMenuItem.Text = "&Query";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.contactToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("contactToolStripMenuItem.Image")));
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.contactToolStripMenuItem.Text = "Contact";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMonitoring,
            this.toolStripSeparator2,
            this.toolUploadSong,
            this.toolStripSeparator3,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(876, 55);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolMonitoring
            // 
            this.toolMonitoring.Image = global::KTVServerApp.Properties.Resources.Monitor_128;
            this.toolMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMonitoring.Name = "toolMonitoring";
            this.toolMonitoring.Size = new System.Drawing.Size(119, 52);
            this.toolMonitoring.Text = "Monitoring";
            this.toolMonitoring.Click += new System.EventHandler(this.toolMonitoring_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 55);
            // 
            // toolUploadSong
            // 
            this.toolUploadSong.Image = global::KTVServerApp.Properties.Resources.Singer_128;
            this.toolUploadSong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolUploadSong.Name = "toolUploadSong";
            this.toolUploadSong.Size = new System.Drawing.Size(127, 52);
            this.toolUploadSong.Text = "Upload Song";
            this.toolUploadSong.Click += new System.EventHandler(this.uploadSongToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::KTVServerApp.Properties.Resources.Song_128;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(115, 52);
            this.toolStripButton2.Text = "Song\'s List";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 379);
            this.panel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(876, 463);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolMonitoring;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolUploadSong;
        private System.Windows.Forms.ToolStripMenuItem uploadSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadSingerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadCountryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadProductionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem uploadAlbumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadLocalSongToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadExcelSongToolStripMenuItem;
    }
}