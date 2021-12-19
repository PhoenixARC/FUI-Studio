using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace FUI_Studio
{
    static class Program
    {


        public static string TempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fui Studio\\";
        public static bool IsPortable = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                //Console.WriteLine(BitConverter.ToString(GetMD5(Environment.CurrentDirectory + "\\FUI Studio.exe")).Replace("-", ""));
                //string dat = File.ReadAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData.db");
                //File.WriteAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db", Encrypt(dat));
                if (!Decrypt(File.ReadAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db")).StartsWith("--skinGraphicsInGame"))
                {
                    MessageBox.Show("You are using a Modified Database!\ndoing so may result in unintended effects!");
                }
            }
            catch
            {

            }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Forms.Form1(args, false, true, true, true, false));
            
        }

        static byte[] GetMD5(string filename)
        {

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }


        public static string Encrypt(string clearText)
        {
            string EncryptionKey = BitConverter.ToString(GetMD5(Environment.CurrentDirectory + "\\FUI Studio.exe")).Replace("-","");
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "A628649BB7FF7348998B60EFD4791E1A";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
