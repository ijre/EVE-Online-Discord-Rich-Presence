using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using DiscordRPC;

namespace eve_discord_rpc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread.Sleep(15);
            try
            {
                if (File.ReadAllText(Application.StartupPath + "/settings/" + "checkbox setting 1.txt").ToString() == "true")
                    checkBox1.Checked = true;
                else checkBox1.Checked = false;
            }
            catch { FolderCheckem(); }
            try
            {
                if (File.ReadAllText(Application.StartupPath + "/settings/" + "checkbox setting 2.txt").ToString() == "ingame")
                {
                    ingameCB.Checked = true;
                    englishCB.Checked = false;
                }
                else if (File.ReadAllText(Application.StartupPath + "/settings/" + "checkbox setting 2.txt").ToString() == "english")
                {
                    ingameCB.Checked = false;
                    englishCB.Checked = true;
                }
            }
            catch { FolderCheckem(); }
        }

        private void PresenceUpdate(string charName, string state)
        {
            DiscordRpcClient client = new DiscordRpcClient("598364146727124993");
            client.SetPresence(new RichPresence { Details = "Playing EVE as: " + charName, State = state });
            client.Initialize();
            //textBox4.Text = "Presence updated!";
            Thread.Sleep(5000);
            Application.ExitThread();
        }

        private void Loop(string fileDir)
        {
            //textBox4.Text = "Parsing file...";
            Process process = Process.GetProcessesByName("exefile")[0];

            var data = File.ReadAllText(fileDir).ToString();
            var charName = process.MainWindowTitle.Substring(6);

            if (checkBox1.Checked)
                if (englishCB.Checked) { }
                //TODO: game is fucked and i cant switch it off of french, so i need to find a gamelog in english
                else if (ingameCB.Checked)
                {
                    var location1 = data.LastIndexOf("<");
                    var location2 = data.LastIndexOf(">");
                    if (data.Substring(data.IndexOf("]", location2) + 42, 8) == "acceptée")
                    {
                        var half = data.Substring(location1, location2 - location1);
                        var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);
                        Thread presenceThread = new Thread(() => PresenceUpdate(charName, full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Loop(fileDir);
                    }
                }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog
            {
                Title = "Please select your most recent Gamelogs file.",
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                RestoreDirectory = true,
                DefaultExt = ".txt",
                Filter = "Text files only|*.txt",
                AddExtension = true,
            };
            diag.ShowDialog();
            Thread loopThread = new Thread(() => Loop(diag.FileName));
            loopThread.Start();
        }

        private void FolderCheckem()
        {
            if (!File.Exists(Application.StartupPath + "/settings/"))
                Directory.CreateDirectory(Application.StartupPath + "/settings/");
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            FolderCheckem();
            if (checkBox1.Checked == true)
            {
                File.WriteAllText(Application.StartupPath + "/settings/" + "checkbox setting 1.txt", "true");
                englishCB.Visible = true;
                ingameCB.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "/settings/" + "checkbox setting 1.txt", "false");
                englishCB.Visible = false;
                ingameCB.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void EnglishCB_CheckedChanged(object sender, EventArgs e)
        {
            FolderCheckem();
            if (englishCB.Checked == true)
            {
                if (ingameCB.Checked == true)
                    ingameCB.Checked = false;

                File.WriteAllText(Application.StartupPath + "/settings/" + "checkbox setting 2.txt", "english");
            }
        }

        private void IngameCB_CheckedChanged(object sender, EventArgs e)
        {
            FolderCheckem();
            if (ingameCB.Checked == true)
            {
                if (englishCB.Checked == true)
                    englishCB.Checked = false;

                File.WriteAllText(Application.StartupPath + "/settings/" + "checkbox setting 2.txt", "ingame");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DiscordRpcClient client = new DiscordRpcClient("598364146727124993");
            client.SetPresence(new RichPresence { Details = "", State = "" });
            Thread.Sleep(50);
            client.Dispose();
            Process process = Process.GetProcessesByName("eve-discord-rpc")[0];
            process.Kill();
        }
    }
}