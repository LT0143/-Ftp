using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace SoulWorkerDownload
{
    /// <summary>
    /// FTP文件上传下载
    /// </summary>
    public class FileUpDownload
    {

        #region 变量属性

        private Thread m_thread_Download = null;

        /// <summary>
        /// Ftp服务器ip
        /// </summary>
        private string FtpServerIP = string.Empty;
        /// <summary>
        /// Ftp 指定用户名
        /// </summary>
        private string FtpUserID = string.Empty;
        /// <summary>
        /// Ftp 指定用户密码
        /// </summary>
        private string FtpPassword = string.Empty;

        /// <summary>
        /// 远端文件名
        /// </summary>
        private string remoteFileName = string.Empty;

        /// <summary>
        /// 本地文件名
        /// </summary>
        private string localFileName = string.Empty;

        public string LocalFileName
        {
            get { return localFileName; }
            set {  localFileName = value; }
        }

        private MainForm mainform;


        /// <summary>
        /// 更新进度条
        /// </summary>
        public Action< long> updateProgress;

        #endregion

        #region 从FTP服务器下载文件，指定本地路径和本地文件名

        public FileUpDownload(string IP, string userID, string userPassword,string targetFileName,string saveFileName,MainForm form)
        {
            FtpServerIP = IP;
            FtpUserID = userID;
            FtpPassword = userPassword;
            remoteFileName = targetFileName;
            localFileName = saveFileName;
            mainform = form;
        }

        public void StartSingleDownload()
        {
            m_thread_Download = new Thread(new ThreadStart(FtpDownloadFile));
            m_thread_Download.SetApartmentState(ApartmentState.STA);
            m_thread_Download.Priority = ThreadPriority.Highest;
            m_thread_Download.Start();
        }

        /// <summary>
        /// 停止下载
        /// </summary>
        public void AbortSingleDownload()
        {
            if (m_thread_Download != null && m_thread_Download.IsAlive )
            {
                if (m_thread_Download.ThreadState == ThreadState.Suspended)
                    m_thread_Download.Resume();

                //if(Step.CURRENT_STEP != Step.State._ProcessState_Restart)
                    m_thread_Download.Abort();
            }
        }

        /// <summary>
        /// 暂停。挂起线程，或者如果线程已挂起，则不起作用
        /// </summary>
        /// <param name="_parameter"></param>
        public void SuspendSingleDownload()
        {
            if (m_thread_Download != null && m_thread_Download.IsAlive == true)
            {
                m_thread_Download.Suspend();
            }
        }

        public void ResumeSingleDownload()
        {
            bool isAlive = m_thread_Download.IsAlive;
            ThreadState state = m_thread_Download.ThreadState;

            if (m_thread_Download != null)
            {
                m_thread_Download.Resume();
            }
        }

        /// <summary>
        /// 判断线程是否结束
        /// </summary>
        public bool IsAbortThreadOrStop()
        {
            bool isAbort = false;

            //bool alive = m_thread_Download.IsAlive;

            if (m_thread_Download == null)
                return true;


            if ((m_thread_Download.ThreadState & ThreadState.Aborted) != 0)
            {
                isAbort = true;
            }
            if (!m_thread_Download.IsAlive)
            {
                isAbort = true;
            }
            return isAbort;
        }

        /*
        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localFileName">保存本地的文件名（包含路径）</param>
        /// <param name="updateProgress">报告进度的处理(第一个参数：总大小，第二个参数：当前进度)</param>
        /// <returns>是否下载成功</returns>
        public  bool FtpDownload(string remoteFileName, string localFileName, Action<int, int> updateProgress = null)
        {
            FtpWebRequest reqFTP, ftpsize;
            Stream ftpStream = null;
            FtpWebResponse response = null;
            FileStream outputStream = null;
            try
            {
 
                outputStream = new FileStream(localFileName, FileMode.Create);
                if (FtpServerIP == null || FtpServerIP.Trim().Length == 0)
                {
                    throw new Exception("ftp下载目标服务器地址未设置！");
                }
                Uri uri = new Uri("ftp://" + FtpServerIP + "/" + remoteFileName);
                ftpsize = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpsize.UseBinary = true;
 
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                
                ftpsize.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
                reqFTP.Credentials = new NetworkCredential(FtpUserID, FtpPassword);
                
                ftpsize.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse re = (FtpWebResponse)ftpsize.GetResponse();
                long totalBytes = re.ContentLength;
                re.Close();
 
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
 
                //更新进度  
                if (updateProgress != null)
                {
                    updateProgress((int)totalBytes, 0);//更新进度条   
                }
                long totalDownloadedByte = 0;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);


                while (readCount > 0)
                {
                    totalDownloadedByte = readCount + totalDownloadedByte;
                    outputStream.Write(buffer, 0, readCount);
                    //更新进度  
                    if (updateProgress != null)
                    {
                        updateProgress((int)totalBytes, (int)totalDownloadedByte);//更新进度条   
                    }
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (ftpStream != null)
                {
                    ftpStream.Close();
                }
                if (outputStream != null)
                {
                    outputStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        */

        bool isShowIOError = false;
        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名（支持断点下载）
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localFileName">保存本地的文件名（包含路径）</param>
        /// <param name="ifCredential">是否启用身份验证（false：表示允许用户匿名下载）</param>
        /// <param name="size">已下载文件流大小</param>
        /// <param name="updateProgress">报告进度的处理(第一个参数：总大小，第二个参数：当前进度)</param>
        /// <returns>是否下载成功</returns>
        public  bool FtpBrokenDownload(string remoteFileName, string tempLocalFileName, long size, Action<long> updateProgress = null)
        {
            bool isFinish = false;
            FtpWebRequest reqFTP, ftpsize;
            Stream ftpStream = null;
            FtpWebResponse response = null;
            FileStream outputStream = null;
            long totalBytes = 0;
            long totalDownloadedByte = size;

            try
            {
                Thread primaryThread = Thread.CurrentThread;
                //                 string name = "FtpDownload" + primaryThread.ManagedThreadId;
                //                 MessageBox.Show(name 
                if (FtpServerIP == null || FtpServerIP.Trim().Length == 0)
                {
                    throw new Exception("ftp下载目标服务器地址未设置！");
                }
                Uri uri = new Uri("ftp://" + FtpServerIP + "/" + remoteFileName);
                ftpsize = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpsize.UseBinary = true;
                ftpsize.ContentOffset = size;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.ContentOffset = size;
                ftpsize.Credentials = GetCredentials(); //new NetworkCredential(FtpUserID, FtpPassword);
                reqFTP.Credentials = GetCredentials();// new NetworkCredential(FtpUserID, FtpPassword);
                ftpsize.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse re = (FtpWebResponse)ftpsize.GetResponse();
                totalBytes = re.ContentLength;
                re.Close();
                if (size == totalBytes)
                {
                    return true;
                }
                else
                    Step.CURRENT_STEP = Step.State._ProcessState_Loading;

                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();


                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                using (outputStream = new FileStream(tempLocalFileName, FileMode.Append))
                {
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        totalDownloadedByte = readCount + totalDownloadedByte;

                        //更新进度  
                        if (updateProgress != null)
                        {
                            updateProgress(totalDownloadedByte);//更新进度条   
                        }
                        try
                        {
                            readCount = ftpStream.Read(buffer, 0, bufferSize);

                        }
                        catch
                        {
                            break;
                        }
                    }
                }

                try
                {
                    response.Close();
                    ftpStream.Close();
                }
                catch { }
                outputStream.Close();
                if (totalDownloadedByte == totalBytes)
                {
                    isFinish = true;
                }
            }
            catch(WebException e)
            {
                FtpWebResponse res = (FtpWebResponse)e.Response;
                string code = "下载服务器连接失败，错误码：" + res.StatusCode;
                MessageBox.Show(code);
            }
            catch (IOException e)
            {
                isShowIOError = true;
                MessageBox.Show(e.Message);
            }
            finally
            {
                try
                {

                    if (ftpStream != null)
                    {
                        ftpStream.Close();
                    }
                    if (outputStream != null)
                    {
                        outputStream.Close();
                    }
                    if (response != null)
                    {
                        response.Close();
                    }
                }
                catch { }
            }
            if (totalDownloadedByte == totalBytes)
            {
                isFinish = true;
            }
            return isFinish;
        }
 
        
        /// <summary>
        /// 从FTP服务器下载文件，指定本地路径和本地文件名
        /// </summary>
        /// <param name="remoteFileName">远程文件名</param>
        /// <param name="localFileName">保存本地的文件名（包含路径）</param>
        /// <param name="updateProgress">报告进度的处理(第一个参数：总大小，第二个参数：当前进度)</param>
        /// <param name="brokenOpen">是否断点下载：true 会在localFileName 找是否存在已经下载的文件，并计算文件流大小</param>
        /// <returns>是否下载成功</returns>
        //public static bool FtpDownloadFile(string remoteFileName, string localFileName, bool brokenOpen, Action<int, int> updateProgress = null)
        public void FtpDownloadFile()
        {
            long size0 = 0;
            long size1 = 0;

            if (File.Exists(localFileName))
            {
                using (FileStream outputStream = new FileStream(localFileName, FileMode.Open))
                {
                    size0 = outputStream.Length;
                }
            }

            //超时2次内再重连
            while (!FtpBrokenDownload(remoteFileName, localFileName, size0, updateProgress))
            {
                if (Step.CURRENT_STEP != Step.State._ProcessState_Loading)
                {
                    AbortSingleDownload();
                    return;
                }
            
                if (File.Exists(localFileName))
                {
                    using (FileStream outputStream = new FileStream(localFileName, FileMode.Open))
                    {
                        size1 = outputStream.Length;
                    }
                }
                if (size1 == size0)
                {
                    //MessageBox.Show("下载服务器连接失败，请检查网络并确保防火墙允许SoulWorkerDownload联网！");
                    mainform.AppEnd();
                    return;
                }
                else
                {
                    size0 = size1;
                }
            }

        }
 
        #endregion

        #region ftp文件工具类

        /// <summary>
        /// 检查ftp上该文件是否存在
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public bool FtpFileExists()
        {
            try
            {
                long size = GetFileSize();
                if (size > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    if (ex.Message.Contains("550"))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public long GetFileSize()
        {
            long fileBytes = 0;
            FtpWebResponse re =null;
            try
            {
                if (FtpServerIP == null || FtpServerIP.Trim().Length == 0)
                {
                    throw new Exception("ftp下载目标服务器地址未设置！");
                }
                string uri = "ftp://" + FtpServerIP + "/" + remoteFileName;

                FtpWebRequest ftp = GetRequest(uri);
                ftp.Method = WebRequestMethods.Ftp.GetFileSize;
                re = (FtpWebResponse)ftp.GetResponse();
                fileBytes = re.ContentLength;
                re.Close();
            }
            catch (WebException ex)
            {
                if (!isShowIOError)
                {
                    FtpWebResponse res = (FtpWebResponse)ex.Response;
                    string code = "下载服务器连接失败，错误码：" + res.StatusCode;
                    MessageBox.Show(code);
                }
                mainform.AppEnd();
            }
            finally
            {
                if (re != null)
                    re.Close();
            }
            return fileBytes;
        }



        //请求查看文件夹
        private FtpWebRequest GetRequest(string strURI)
        {
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(strURI);

            result.Credentials = GetCredentials();//创建请求

            result.KeepAlive = false;
            result.Timeout = 20000;
            result.UsePassive = false;

            return result;
        }

        private ICredentials GetCredentials()
        {
            return new NetworkCredential(FtpUserID, FtpPassword);
        }
        #endregion

    }

}
