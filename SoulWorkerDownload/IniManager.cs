using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace SoulWorkerDownload
{
    public class IniManager
    {
        string path = string.Empty;

        string lastDownLoadPath = string.Empty;

        String strSection = "SavePath";
        String strKey = "Path";

        public string LastDownLoadPath
        {
            get { return lastDownLoadPath; }
        }

        public IniManager(string iniPath, string saveDirect)
        {
            path = iniPath;

            if (File.Exists(iniPath) == false)
            {
                //File.Create(iniPath);
                SetINIValue(saveDirect);
                //lastDownLoadPath = saveDirect;
            }
            else
            {
                if (string.Equals(GetINIValue(), "NOT_FOUND")) 
                {
                    SetINIValue(saveDirect);
                    //lastDownLoadPath = saveDirect;
                }
                else
                lastDownLoadPath = GetINIValue();
            }
        }

        public   String GetINIValue(  )
        {
            StringBuilder strValue = new StringBuilder(255);
            int i = GetPrivateProfileString(strSection, strKey, "NOT_FOUND", strValue, 255, path);
            return strValue.ToString();
        }

        public  void SetINIValue( String strValue)
        {
            WritePrivateProfileString(strSection, strKey, strValue, path);
            lastDownLoadPath = strValue;
        }

        #region INI Read, Write DLL

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(String section, String key, String val, string filePath);

        #endregion
    }
}
