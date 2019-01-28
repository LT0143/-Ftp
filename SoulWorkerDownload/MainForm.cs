using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading; //线程
using System.Diagnostics;

namespace SoulWorkerDownload
{
    public partial class MainForm : Form
    {
        private long currentDownloadSize = 0;
        //private int currentSecondDownloadSize = 0;
        private long secondDownloadSize = 0;

        private long resource7zFileSize = 0;
        private long exeFileSize = 0;

        private long totalSize = 0;


        private FileUpDownload m_ftpFileDownload = null;
        private FileUpDownload m_ftpEXEDownload = null;

        private string savePathFileName = "Path.ini";

        private string localSetupPath = @"C:\DownLoad";

        private string load7zResourceName = @"\SoulWorker.7z.ttd"; //SoulWorker.7z.ttd   DX9.7z.ttd
        private string local7zFileName = string.Empty;  //7z资源本地存放路径
        private string Ftp7ZTargetPath = @"SoulWorker/SoulWorker.7z"; //SoulWorker.7z    DX9.7z

        private string FtpExeTargetPath = @"SoulWorker/SoulWorkerSetup.exe";
        private string loadSetupExeName = @"SoulWorkerSetup.exe";
        private string localSetupExeName = string.Empty;


        private string currentLoadFileName = string.Empty;     //当前下载的资源在FTP的位置
        private string currentLocalFileName = string.Empty;  // 当前下载的资源本地存放路径

        private string FtpSeverIP = "update.playsw2.com";//update2.playsw2.com  101.55.38.156
        private string FtpSeverID = "swclient";  //swtest swclient
        private string FtpSeverPassword = "Swclient"; //swtest Swclient

        //定义无边框窗体Form
        [DllImport("user32.dll")]//*********************拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private IniManager iniManager;
        public void initData()
        {
            String curPath = Application.ExecutablePath;
            int len = curPath.LastIndexOf(@"\");
            String strExcute = curPath.Substring(0, len + 1);
            strExcute = strExcute + savePathFileName;
            iniManager = new IniManager(strExcute, localSetupPath);
            localSetupPath = iniManager.LastDownLoadPath;

            local7zFileName = localSetupPath + load7zResourceName;
            localSetupExeName = localSetupPath + @"\" + loadSetupExeName;

            currentLoadFileName = Ftp7ZTargetPath;
            currentLocalFileName = local7zFileName;

            m_ftpFileDownload = new FileUpDownload(FtpSeverIP, FtpSeverID, FtpSeverPassword, Ftp7ZTargetPath, local7zFileName,this);
            m_ftpEXEDownload = new FileUpDownload(FtpSeverIP, FtpSeverID, FtpSeverPassword, FtpExeTargetPath, localSetupExeName,this);

            resource7zFileSize = m_ftpFileDownload.GetFileSize(); //7z文件的大小
            if (resource7zFileSize == 0)
                return;
            exeFileSize = m_ftpEXEDownload.GetFileSize();       //exe文件的大小
            totalSize = resource7zFileSize + exeFileSize;
        }

        /// <summary>
        /// 初始化界面！
        /// </summary>
        public void InitWindow()
        {
            initData();
            UpdateWindow();
        }


        public void UpdateWindow()
        {
            if (resource7zFileSize == 0 || exeFileSize == 0)
                return;
            textBox_OpenFile.Text = localSetupPath;
            string tempName = local7zFileName.Substring(0, local7zFileName.LastIndexOf('.'));
            string localName = string.Empty;

            if (File.Exists(local7zFileName))
            {
                localName = local7zFileName;
            }
            else if (File.Exists(tempName))
            {
                localName = tempName;
            }

            if (!string.IsNullOrEmpty(localName))
            {
                //DisableOpenFileUI();

                button_downLoad.Text = "继续下载";
                Step.CURRENT_STEP = Step.State._ProcessState_Restart;

                //存在7z则查看是否已经下载完成，否就继续下载，是就查看exe是否下载完成。
                FileInfo file = new FileInfo(localName);
                if (file.Length > 0)
                {
                    currentDownloadSize = file.Length;

                    if (file.Length >= resource7zFileSize)
                    {
                        currentLoadFileName = FtpExeTargetPath;
                        currentLocalFileName = localSetupExeName;

                        if (File.Exists(localSetupExeName))
                        {
                            FileInfo fileExe = new FileInfo(localSetupExeName);
                            currentDownloadSize += fileExe.Length;
                            //存在exe安装文件，需要检查是否已经下载完成！
                            if (fileExe.Length >= exeFileSize)
                            {
                                button_downLoad.Text = "立即安装";
                                Step.CURRENT_STEP = Step.State._ProcessState_Ready;
                                label_downloadSpeed.Text = "下载完毕";
                                DisableOpenFileUI();
                            }
                        }
                    }
                }
            }

            secondDownloadSize = currentDownloadSize;
            timer_Update7ZDownloadSpeed(null, null);
        }


        private void gPanelTitleBack_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);//*********************调用移动无窗体控件函数
        }

        public MainForm()
        {
            InitializeComponent();
            InitWindow();
        }

        private void button_CloseClick(object sender, EventArgs e)
        {
            Step.CURRENT_STEP = Step.State._ProcessState_Exit;
            if (MessageBox.Show("是否要结束客户端下载？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Step.CURRENT_STEP = Step.State._ProcessState_Exit;
                AppEnd();
            }
        }

        public void AppEnd()
        {
            this.Dispose(true);
            Application.Exit();
        }

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.ShowDialog();
            string path = ofd.SelectedPath;
            textBox_OpenFile.Text = path;
            //localSetupPath = path;
            //iniManager.SetINIValue(path);
        }

        private void button_downLoad_Click(object sender, EventArgs e)
        {
            StepManager();
        }

        private void StepManager()
        {
            switch (Step.CURRENT_STEP)
            {
                case Step.State._ProcessState_begin:
                    BeginDownloadClick();
                    break;
                case Step.State._ProcessState_Restart:
                    RestartDownloadClick();
                    break;
                case Step.State._ProcessState_Loading:
                    PauseDownloadClick();
                    break;
                case Step.State._ProcessState_Pause:
                    ResumeDownloadClick();
                    break;
                case Step.State._ProcessState_Ready:
                    ReadyDownloadClick();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 开始暂停时间
        /// </summary>
        private TimeSpan  pauseStartTime ;


        /// <summary>
        /// 暂停结束时间
        /// </summary>
        private TimeSpan pauseEndTime ;


        /// <summary>
        ///1.暂停下载。2.改文本为开始下载 
        /// </summary>
        private void PauseDownloadClick()
        {
            button_downLoad.Text = "继续下载";

            label_downloadSpeed.Text =  "0KB/S";

            this.timer_MainForm.Stop();
            pauseStartTime = new TimeSpan(DateTime.Now.Ticks);

            Step.CURRENT_STEP = Step.State._ProcessState_Pause;
            m_ftpFileDownload.SuspendSingleDownload();

        }


        public bool CheckExeRunning(string name)
        {
            Process[] app = Process.GetProcessesByName(name);
            if (app.Length > 0)
            {
          
                return true;
            }
            return false;
        }

        /// <summary>
        /// 1.检测是否已经有启动的SoulWorkerSetup.exe，是就激活窗口提示已经存在，否就启动进程
        /// </summary>
        private void ReadyDownloadClick()
        {
            int index = loadSetupExeName.LastIndexOf('.');
            string name = loadSetupExeName.Substring(0, index);
            if (!CheckExeRunning(name)) 
            {
                System.Diagnostics.Process.Start(localSetupExeName);
            }
            else
            {
                string text = "已经启动了程序：" + name + ".exe,请先关闭后再试！";
                MessageBox.Show(text);
            }
        }

        /// <summary>
        ///2.恢复下载。2.改文本为暂停下载 3.超过半分钟重新开始下载！
        /// </summary>
        private void ResumeDownloadClick()
        {
            button_downLoad.Text = "暂停下载";
            this.timer_MainForm.Start();

            pauseEndTime = new TimeSpan(DateTime.Now.Ticks);
            double times = pauseEndTime.Subtract(pauseStartTime).Duration().TotalSeconds; //时间差的绝对值
            if (times < 30)
            {
                Step.CURRENT_STEP = Step.State._ProcessState_Loading;
                m_ftpFileDownload.ResumeSingleDownload();
            }
            else
            {
                Step.CURRENT_STEP = Step.State._ProcessState_Restart;
                m_ftpFileDownload.AbortSingleDownload();
                while (!m_ftpFileDownload.IsAbortThreadOrStop())
                {

                }
                StepManager();
            }
        }

        /// <summary>
        ///1.检查路径是否存在，不存在则创建。2.开始下载 3.隐藏保存路径栏，4.文字变为暂停下载,5.显示下载网速文本,
        /// </summary>
        private void BeginDownloadClick()
        {

            string path = textBox_OpenFile.Text ;

            if (!CurrentLocalFileNameIsEmpty(path))
            {
                return;
            }
            if (!CheckHardDiskFreeSpace())
            {
                AppEnd();
                return;
            }

            localSetupPath = path;
            iniManager.SetINIValue(path);

            local7zFileName = localSetupPath + load7zResourceName;
            localSetupExeName = localSetupPath + @"\" + loadSetupExeName;
            m_ftpFileDownload.LocalFileName = local7zFileName;
            m_ftpEXEDownload.LocalFileName = localSetupExeName;

            Directory.CreateDirectory(localSetupPath);
            button_downLoad.Text = "暂停下载";

            UpdateWindow();


            //启动定时器更新下载进度显示
            timer_MainForm.Enabled = true;
            timer_MainForm.Interval = 1000;
            timer_MainForm.Start();

            DisableOpenFileUI();

            DownloadingFile();

        }


        private void RestartDownloadClick()
        {
            BeginDownloadClick();
        }

        private bool CurrentLocalFileNameIsEmpty(string path )
        {
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请选择下载地址！");
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 隐藏 打开文件那栏。
        /// </summary>
        private void DisableOpenFileUI()
        {
            //label_savePath.e = false;
            textBox_OpenFile.Enabled = false;
            button_OpenFile.Enabled = false;
            label_downloadSpeed.Visible = true;
        }

        /// <summary>
        /// 下载文件，判断是资源文件还是exe文件。
        /// </summary>
        private void DownloadingFile()
        {
           
            if (currentLoadFileName == Ftp7ZTargetPath)
            {
                m_ftpFileDownload.updateProgress = Update7ZProgressBar;
                m_ftpFileDownload.StartSingleDownload();
            }
            else
            {
                if(m_ftpEXEDownload == null)
                    m_ftpEXEDownload = new FileUpDownload(FtpSeverIP, FtpSeverID, FtpSeverPassword, FtpExeTargetPath, localSetupExeName,this);
                m_ftpEXEDownload.updateProgress = UpdateEXEProgressBar;
                m_ftpEXEDownload.StartSingleDownload();
            }
        }
 
        private void Update7ZProgressBar( long downloadSize)
        {
            //totalSize = totalBytes;
            currentDownloadSize = downloadSize;
        }

        bool isStarDownLoadExe = false;
   

        private void timer_Update7ZDownloadSpeed(object sender, EventArgs e)
        {
            float value = 0;
            if (totalSize != 0)
            {
                value = currentDownloadSize * 100f / totalSize;
            }
            long speed = currentDownloadSize - secondDownloadSize;  //下载速度，当前进度-之前下载的进度

            if (currentDownloadSize == resource7zFileSize && !isStarDownLoadExe)
            {
                //todo开始下载exe,停止7z的线程 
                m_ftpFileDownload.AbortSingleDownload();
                while (!m_ftpFileDownload.IsAbortThreadOrStop())
                {

                }

                if (currentLoadFileName != FtpExeTargetPath)
                {
                    currentLoadFileName = FtpExeTargetPath;
                    currentLocalFileName = localSetupExeName;
                    isStarDownLoadExe = true;
                    DownloadingFile();
                }
            }

            if (currentDownloadSize >= resource7zFileSize)
            {
                string tempName = local7zFileName.Substring(0, local7zFileName.LastIndexOf('.'));

                if (File.Exists(local7zFileName))
                {
                    File.Move(local7zFileName, tempName);
                }
            }

            value = value < 0 ? 0 : value;
            value = value > 100 ? 100 : value;
            progressBar_Total.Value = (int) value ;

            SetDownloadSpeedLabel(speed);

            secondDownloadSize = currentDownloadSize;
            label_Total.Text = ShowBytes(currentDownloadSize, totalSize);// currentDownloadSize.ToString() + "/" + totalSize.ToString();

            if (Step.CURRENT_STEP == Step.State._ProcessState_Ready)
            {
                progressBar_Total.Value = 100;
                button_downLoad.Text = "立即安装";
                DisableOpenFileUI();

                int index = loadSetupExeName.LastIndexOf('.');
                string name = loadSetupExeName.Substring(0, index);
                if (!CheckExeRunning(name))
                {
                    System.Diagnostics.Process.Start(localSetupExeName);
                }
                this.timer_MainForm.Stop();
                SetDownloadSpeedLabel(0);
            }
        }


        /// <summary>
        /// 更新setup.exe的进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateEXEProgressBar(long downloadSize)
        {
            long size = resource7zFileSize + downloadSize;
            currentDownloadSize = size;
            if (currentDownloadSize >= totalSize)
            {
                Step.CURRENT_STEP = Step.State._ProcessState_Ready;
            }
        }


        private string ShowBytes(long currentBytes,long totalBytes = 0)
        {
            string str = "";
            String[] units = new String[] { "B", "KB", "MB", "GB" };
            double mod = 1024.0;
            double size = currentBytes * 1.0f;

            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            str = Math.Round(size,1) + units[i];

            if (totalBytes > 0)
            {
                double totalSize = totalBytes * 1.0f;
                int j = 0;
                while (totalSize >= mod)
                {
                    totalSize /= mod;
                    j++;
                }
                str += @"/" + Math.Round(totalSize,1) + units[j];
            }
 
            return str;
        }

        private void button_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.playsw2.com"); 
        }

        private void SetDownloadSpeedLabel(long speed)
        {
            if (speed < 0)
                speed = 0;
            if (speed == 0 )
            {
                if (Step.CURRENT_STEP == Step.State._ProcessState_Loading)
                label_downloadSpeed.Text = "资源校验中...";
                else if(Step.CURRENT_STEP == Step.State._ProcessState_Ready)
                    label_downloadSpeed.Text = "下载完成";
            }
            else
            {
                label_downloadSpeed.Text = ShowBytes(speed)+ "/S";
            }
        }

        /// <summary>
        /// 检查磁盘空间大小
        /// </summary>
        /// <returns></returns>
        public bool CheckHardDiskFreeSpace()
        {
            long freeSpace = 0;
            bool isOk = false;
            string hardDiskName = currentLocalFileName.Substring(0, currentLocalFileName.IndexOf(":"));

            string hardDiskPath = hardDiskName + ":\\";
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach(DriveInfo drive in drives)
            {
                if (drive.Name == hardDiskPath)
                {
                    freeSpace = drive.TotalFreeSpace;
                }
            }

            if (freeSpace > totalSize)
            {
                isOk = true;
            }
            else
                MessageBox.Show("磁盘空间不足，请重新选择！");
            return isOk;
        }
    }


}
