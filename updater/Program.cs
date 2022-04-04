using System;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace updater
{
	static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(new WebClient().DownloadString(ServerXML));
			downloadUpdate();
		}


		private static string ServerXML = "http://www.pckstudio.xyz/studio/FUI/update.xml";
		private static string backupServerXML = "http://phoenixarc.github.io/pckstudio.tk/studio/FUI/update.xml";


		private static string localFile = Environment.CurrentDirectory + "\\FUI Studio.exe";

		public static void downloadUpdate()
		{
			try
			{
				try
				{
					foreach (Process proc in Process.GetProcessesByName("FUI Studio"))
					{
						proc.Kill();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				string TryXMLDl = new WebClient().DownloadString(ServerXML);
				string[] raw = TryXMLDl.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
				XmlTextReader reader = new XmlTextReader(ServerXML);
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element: // The node is an element.
							Console.Write("<" + reader.Name + " || " + reader.LineNumber);
							Console.WriteLine(">");
							if (reader.Name == "FileUpdateTask")
							{
								try
								{
									Console.WriteLine(raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\"));
									Directory.CreateDirectory(Path.GetDirectoryName(Environment.CurrentDirectory + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\")));
									string url = ServerXML.Replace(".xml", "") + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "");
									new WebClient().DownloadFile(url, Environment.CurrentDirectory + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\"));
								}
								catch (Exception err)
								{
									Console.WriteLine(err.Message);
									Console.WriteLine(err.StackTrace);
								}
							}
							break;
					}
				}
				new Process
				{
					StartInfo =
				{
					FileName = localFile
				}
				}.Start();
				Application.Exit();
			}
            catch(Exception err1)
            {
				Console.WriteLine(err1.Message);
				Console.WriteLine(err1);
				Console.WriteLine(err1.Source.ToString());
                try
				{
					try
					{
						foreach (Process proc in Process.GetProcessesByName("FUI Studio"))
						{
							proc.Kill();
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					catch { }

					string TryXMLDl = new WebClient().DownloadString(backupServerXML);
					string[] raw = TryXMLDl.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
					XmlTextReader reader = new XmlTextReader(backupServerXML);
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
							case XmlNodeType.Element: // The node is an element.
								Console.Write("<" + reader.Name + " || " + reader.LineNumber);
								Console.WriteLine(">");
								if (reader.Name == "FileUpdateTask")
								{
									try
									{
										Directory.CreateDirectory(Path.GetDirectoryName(Environment.CurrentDirectory + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\")));
										string url = backupServerXML.Replace(".xml", "") + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "");
										new WebClient().DownloadFile(url, Environment.CurrentDirectory + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\"));
										Console.WriteLine(Environment.CurrentDirectory + raw[reader.LineNumber - 1].Replace("	<FileUpdateTask localPath=\"", "").Replace("\">", "").Replace("/", "\\"));
									}
									catch (Exception err)
									{
										Console.WriteLine(err.Message);
									}

								}
								break;
						}
					}
					new Process
					{
						StartInfo =
				{
					FileName = localFile
				}
					}.Start();
					Application.Exit();
				}
				catch (Exception err)
				{
					Console.WriteLine(err.Message);
				}
				Console.ReadLine();
            }
		}
	}
}
