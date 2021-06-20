using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace FUI_Studio.Classes
{
    public static class Networking
    {
        public static string ver = "1.4";
        static string url = "https://www.pckstudio.tk/studio/FUI/api/update.txt";
        static string backupurl = "https://phoenixarc.github.io/pckstudio.tk/studio/FUI/api/update.txt";
        public static bool NeedsUpdate = false;
        static WebClient client = new WebClient();

        public static void checkUpdate()
        {
            try
            {
                if(float.Parse(client.DownloadString(url)) > float.Parse(ver))
                {
                    NeedsUpdate = true;
                    if (MessageBox.Show("Update " + client.DownloadString(url) + " is available!\ndo you want to download it?\nYour current version is:" + ver, "Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    if (float.Parse(client.DownloadString(backupurl)) > float.Parse(ver))
                    {
                        NeedsUpdate = true;
                        if (MessageBox.Show("Update " + client.DownloadString(backupurl) + " is available!\ndo you want to download it?\nYour current version is:" + ver, "Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
    }
}
