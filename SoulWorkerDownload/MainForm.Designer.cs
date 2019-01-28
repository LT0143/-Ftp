namespace SoulWorkerDownload
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                this.timer_MainForm.Stop();
                this.timer_MainForm.Dispose();
                if (Step.CURRENT_STEP == Step.State._ProcessState_Exit)
                {
                    if (m_ftpEXEDownload != null && !m_ftpEXEDownload.IsAbortThreadOrStop())
                        m_ftpEXEDownload.AbortSingleDownload();
                    if (m_ftpFileDownload != null && !m_ftpFileDownload.IsAbortThreadOrStop())
                        m_ftpFileDownload.AbortSingleDownload();
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.progressBar_Total = new System.Windows.Forms.ProgressBar();
            this.label_Total = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.button_downLoad = new System.Windows.Forms.Button();
            this.textBox_OpenFile = new System.Windows.Forms.TextBox();
            this.label_savePath = new System.Windows.Forms.Label();
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.label_downloadSpeed = new System.Windows.Forms.Label();
            this.button_min = new System.Windows.Forms.Button();
            this.timer_MainForm = new System.Windows.Forms.Timer(this.components);
            this.button_Logo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar_Total
            // 
            this.progressBar_Total.BackColor = System.Drawing.Color.PapayaWhip;
            this.progressBar_Total.ForeColor = System.Drawing.Color.LawnGreen;
            this.progressBar_Total.Location = new System.Drawing.Point(220, 477);
            this.progressBar_Total.Name = "progressBar_Total";
            this.progressBar_Total.Size = new System.Drawing.Size(682, 16);
            this.progressBar_Total.TabIndex = 1;
            // 
            // label_Total
            // 
            this.label_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label_Total.BackColor = System.Drawing.Color.Transparent;
            this.label_Total.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Total.ForeColor = System.Drawing.Color.White;
            this.label_Total.Location = new System.Drawing.Point(467, 497);
            this.label_Total.Name = "label_Total";
            this.label_Total.Size = new System.Drawing.Size(159, 57);
            this.label_Total.TabIndex = 2;
            this.label_Total.Text = "0M/6.5G";
            this.label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_close
            // 
            this.button_close.BackColor = System.Drawing.Color.Transparent;
            this.button_close.BackgroundImage = global::SoulWorkerDownload.Properties.Resources.Btn_Close;
            this.button_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_close.FlatAppearance.BorderSize = 0;
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Location = new System.Drawing.Point(932, 7);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(19, 19);
            this.button_close.TabIndex = 3;
            this.button_close.UseVisualStyleBackColor = false;
            this.button_close.Click += new System.EventHandler(this.button_CloseClick);
            // 
            // button_downLoad
            // 
            this.button_downLoad.BackColor = System.Drawing.Color.OrangeRed;
            this.button_downLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_downLoad.FlatAppearance.BorderSize = 0;
            this.button_downLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_downLoad.Font = new System.Drawing.Font("黑体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_downLoad.ForeColor = System.Drawing.Color.Gold;
            this.button_downLoad.Location = new System.Drawing.Point(467, 344);
            this.button_downLoad.Name = "button_downLoad";
            this.button_downLoad.Size = new System.Drawing.Size(159, 58);
            this.button_downLoad.TabIndex = 4;
            this.button_downLoad.Text = "开始下载";
            this.button_downLoad.UseVisualStyleBackColor = false;
            this.button_downLoad.Click += new System.EventHandler(this.button_downLoad_Click);
            // 
            // textBox_OpenFile
            // 
            this.textBox_OpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_OpenFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_OpenFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_OpenFile.ForeColor = System.Drawing.Color.White;
            this.textBox_OpenFile.Location = new System.Drawing.Point(439, 442);
            this.textBox_OpenFile.Name = "textBox_OpenFile";
            this.textBox_OpenFile.Size = new System.Drawing.Size(251, 22);
            this.textBox_OpenFile.TabIndex = 5;
            this.textBox_OpenFile.Text = "F:\\DownLoad";
            // 
            // label_savePath
            // 
            this.label_savePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_savePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_savePath.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_savePath.ForeColor = System.Drawing.Color.White;
            this.label_savePath.Location = new System.Drawing.Point(313, 442);
            this.label_savePath.Name = "label_savePath";
            this.label_savePath.Size = new System.Drawing.Size(122, 22);
            this.label_savePath.TabIndex = 6;
            this.label_savePath.Text = "安装包保存路径";
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.BackColor = System.Drawing.Color.Black;
            this.button_OpenFile.BackgroundImage = global::SoulWorkerDownload.Properties.Resources.Btn_File;
            this.button_OpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_OpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_OpenFile.FlatAppearance.BorderSize = 0;
            this.button_OpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button_OpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Moccasin;
            this.button_OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_OpenFile.Location = new System.Drawing.Point(696, 441);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(25, 24);
            this.button_OpenFile.TabIndex = 7;
            this.button_OpenFile.UseVisualStyleBackColor = false;
            this.button_OpenFile.Click += new System.EventHandler(this.button_OpenFile_Click);
            // 
            // label_downloadSpeed
            // 
            this.label_downloadSpeed.BackColor = System.Drawing.Color.Transparent;
            this.label_downloadSpeed.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_downloadSpeed.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_downloadSpeed.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_downloadSpeed.Location = new System.Drawing.Point(467, 409);
            this.label_downloadSpeed.Name = "label_downloadSpeed";
            this.label_downloadSpeed.Size = new System.Drawing.Size(159, 25);
            this.label_downloadSpeed.TabIndex = 8;
            this.label_downloadSpeed.Text = "0KB/S";
            this.label_downloadSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_downloadSpeed.Visible = false;
            // 
            // button_min
            // 
            this.button_min.BackColor = System.Drawing.Color.Transparent;
            this.button_min.BackgroundImage = global::SoulWorkerDownload.Properties.Resources.Btn_Min;
            this.button_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_min.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_min.FlatAppearance.BorderSize = 0;
            this.button_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_min.Location = new System.Drawing.Point(906, 7);
            this.button_min.Name = "button_min";
            this.button_min.Size = new System.Drawing.Size(19, 18);
            this.button_min.TabIndex = 10;
            this.button_min.UseVisualStyleBackColor = false;
            this.button_min.Click += new System.EventHandler(this.button_min_Click);
            // 
            // timer_MainForm
            // 
            this.timer_MainForm.Tick += new System.EventHandler(this.timer_Update7ZDownloadSpeed);
            // 
            // button_Logo
            // 
            this.button_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button_Logo.BackColor = System.Drawing.Color.DimGray;
            this.button_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_Logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Logo.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.button_Logo.FlatAppearance.BorderSize = 0;
            this.button_Logo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_Logo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_Logo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Logo.Image = global::SoulWorkerDownload.Properties.Resources.bgLogo;
            this.button_Logo.Location = new System.Drawing.Point(353, 50);
            this.button_Logo.Margin = new System.Windows.Forms.Padding(0);
            this.button_Logo.Name = "button_Logo";
            this.button_Logo.Size = new System.Drawing.Size(601, 266);
            this.button_Logo.TabIndex = 13;
            this.button_Logo.UseVisualStyleBackColor = false;
            this.button_Logo.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(220, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(407, 50);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SoulWorkerDownload.Properties.Resources.bgBlack;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(959, 565);
            this.ControlBox = false;
            this.Controls.Add(this.button_Logo);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_min);
            this.Controls.Add(this.button_downLoad);
            this.Controls.Add(this.label_downloadSpeed);
            this.Controls.Add(this.button_OpenFile);
            this.Controls.Add(this.label_savePath);
            this.Controls.Add(this.textBox_OpenFile);
            this.Controls.Add(this.label_Total);
            this.Controls.Add(this.progressBar_Total);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gPanelTitleBack_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

 



        #endregion

        private System.Windows.Forms.Label label_Total;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button button_downLoad;
        private System.Windows.Forms.ProgressBar progressBar_Total;
        private System.Windows.Forms.TextBox textBox_OpenFile;
        private System.Windows.Forms.Label label_savePath;
        private System.Windows.Forms.Button button_OpenFile;
        private System.Windows.Forms.Label label_downloadSpeed;
        private System.Windows.Forms.Button button_min;
        private System.Windows.Forms.Timer timer_MainForm;
        private System.Windows.Forms.Button button_Logo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

