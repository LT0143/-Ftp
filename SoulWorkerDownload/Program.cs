using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace SoulWorkerDownload
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] processesClient = Process.GetProcessesByName(processName);
            if (processesClient.Length > 1)
            {
                string text = "已经启动了游戏下载程序：" + processName + ".exe！";
                MessageBox.Show(text);

                Step.CURRENT_STEP = Step.State._ProcessState_Exit;
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
