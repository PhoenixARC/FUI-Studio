using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace FUI_Studio.Classes
{
    public static class Networking
    {
        public static string ver = "1.91";
        static string url = "https://www.pckstudio.xyz";
        static string backupurl = "https://phoenixarc.github.io/pckstudio.tk";
        static string UpdateFilePath = "/studio/FUI/api/update.txt";
        public static bool NeedsUpdate = false;
        private static string Download(string apiPath, string errMsg = "")
        {
            using (var client = new WebClient())
            {
                string data = "";
                try { data = client.DownloadString(url + UpdateFilePath); }
                catch
                {
                    try { data = client.DownloadString(backupurl + UpdateFilePath); }
                    catch { data = ""; }
                }
                finally
                {
                    if (string.IsNullOrEmpty(data))
                    {
                        throw new Exception($"Failed to get {apiPath}\n{errMsg}");
                    }
                }
                return data;
            }
        }

        private static string getServerVersion()
        {
            return Download(UpdateFilePath);
        }

        public static void checkUpdate()
        {
            string server_version = getServerVersion();
            NeedsUpdate = !server_version.Equals(ver);
            if (NeedsUpdate) PromptForUpdate();
        }

        public static void PromptForUpdate()
        {
            string server_version = getServerVersion();
            if (MessageBox.Show("Update " + server_version + " is available!\ndo you want to download it?\nYour current version is:" + ver, "Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Process.Start(Environment.CurrentDirectory + "\\updater.exe");
                Application.Exit();
            }
        }
    }
}
