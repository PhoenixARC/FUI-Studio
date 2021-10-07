using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace FUI_Studio.Classes
{
    public static class Networking
    {
        public static string ver = "1.9";
        static string url = "https://www.pckstudio.xyz/";
        static string backupurl = "https://phoenixarc.github.io/pckstudio.tk/";
        static string UpdateFilePath = "studio/FUI/api/update.txt";
        public static bool NeedsUpdate = false;
        static WebClient client = new WebClient();

        public static void checkUpdate()
        {
            try
            {
                if(float.Parse(client.DownloadString(url + UpdateFilePath)) > float.Parse(ver))
                {
                    NeedsUpdate = true;
                    if (MessageBox.Show("Update " + client.DownloadString(url + UpdateFilePath) + " is available!\ndo you want to download it?\nYour current version is:" + ver, "Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(Environment.CurrentDirectory + "\\updater.exe");
                        Application.Exit();
                    }
                }
            }
            catch
            {
                try
                {
                    if (float.Parse(client.DownloadString(backupurl + UpdateFilePath)) > float.Parse(ver))
                    {
                        NeedsUpdate = true;
                        if (MessageBox.Show("Update " + client.DownloadString(backupurl + UpdateFilePath) + " is available!\ndo you want to download it?\nYour current version is:" + ver, "Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Process.Start(Environment.CurrentDirectory + "\\updater.exe");
                            Application.Exit();
                        }
                    }
                }
                catch
                {
                    NeedsUpdate = false;
                    MessageBox.Show("Server unreachable!\nUnable to check for updates!", "Server Error", MessageBoxButtons.OK);
                }
            }
        }

        public static void TryDlDatabse()
        {
            try
            {
                string WebDB = client.DownloadString(url + "studio/FUI/api/FUI_Image_Names.db");
                string LocalDB = File.ReadAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db");
                if (WebDB != LocalDB)
                {
                    DialogResult Dr = MessageBox.Show("Online Database is different from the one you have! do you want to proceed?", "Database Update available", MessageBoxButtons.YesNo);
                    if (Dr == DialogResult.Yes)
                    {
                        File.WriteAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db", WebDB);
                    }
                    else if (Dr == DialogResult.No)
                    {

                    }
                }
            }
            catch
            {
                try
                {
                    string WebDB = client.DownloadString(backupurl + "studio/FUI/api/FUI_Image_Names.db");
                    string LocalDB = File.ReadAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db");
                    if (WebDB != LocalDB)
                    {
                        DialogResult Dr = MessageBox.Show("Online Database is different from the one you have! do you want to proceed?", "Database Update available", MessageBoxButtons.YesNo);
                        if (Dr == DialogResult.Yes)
                        {
                            File.WriteAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db", WebDB);
                        }
                        else if (Dr == DialogResult.No)
                        {

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Server Path unreachable!\nUnable to check Online Databse!", "Server Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
