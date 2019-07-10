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
            ButtonCheck();
        }

        private void PresenceUpdate(string charName, string state)
        {
            DiscordRpcClient client = new DiscordRpcClient("598364146727124993");
            client.SetPresence(new RichPresence { Details = "Jouer à EVE, sous le nom: " + charName, State = state });
            client.Initialize();
#if _NDEBUG
            textBox4.Text = "Presence updated!";
#endif
            Thread.Sleep(14000);
            client.ClearPresence();
            Application.ExitThread();
        }

        private void Loop(string fileDir)
        {
#if _NDEBUG
            textBox4.Text = "Parsing file...";

            try
            {
                Process tryProcess = Process.GetProcessesByName("exefile")[0];
            }
            catch
            {
                MessageBox.Show("Fatal error: Could not find EVE Online process, closing.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
#endif
            Process process = Process.GetProcessesByName("exefile")[0];

            try
            { File.ReadAllText(fileDir).ToString(); }
            catch { Application.Restart(); }
            //99% likely that a crash here is someone closing the file dialog without selecting a file, just make it restart
            var data = File.ReadAllText(fileDir).ToString();
            var charName = process.MainWindowTitle.Substring(6);

            if (checkBox1.Checked)
                if (englishCB.Checked) { }
                //TODO: game is fucked so i cant switch it off of french, i need to find a gamelog in english
                else if (ingameCB.Checked)
                {
                    if (data.LastIndexOf("Saute") > data.LastIndexOf("amarrage") && data.LastIndexOf("Saute") > data.LastIndexOf("Part de"))
                    {
                        Thread.Sleep(1000);
                        var half = data.Substring(data.LastIndexOf("Saute"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Saute")) + 1) - data.LastIndexOf("Saute"));
                        //this creates a substring from "Saute" (jump) and ends it at the second instance of * afterwards
                        var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                        //this skips past 2 quotations to find a 3rd quotation mark (the 3rd mark marks the beginning of the solar system name)
                        var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                        Thread presenceThread = new Thread(() => PresenceUpdate(charName, "En volant dans: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                    else if (data.LastIndexOf("Part de") > data.LastIndexOf("amarrage") && data.LastIndexOf("Part de") > data.LastIndexOf("Saute"))
                    {
                        Thread.Sleep(1000);
                        var half = data.Substring(data.LastIndexOf("Part de"), data.IndexOf(".", data.LastIndexOf("Part de")) + 1 - data.LastIndexOf("Part de"));
                        var mouthfull = half.IndexOf("\"", half.IndexOf("\"") + (half.IndexOf("\"", half.IndexOf("\"") + 1)));
                        //and this skips past 3 quotations to find a 4th quotation mark (the 4th mark marks the beginning of the solar system name)
                        //i know all of this is ungodly painful to look at but i couldnt think of a better way, and you cant use absolute values because different system name lengths and all that
                        var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                        Thread presenceThread = new Thread(() => PresenceUpdate(charName, "En volant dans: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                    else if (data.LastIndexOf("amarrage") > data.LastIndexOf("Part de") && data.LastIndexOf("acceptée") > data.LastIndexOf("Saute"))
                    {
                        Thread.Sleep(1000);
                        if (data.Substring(data.LastIndexOf("amarrage")).IndexOf("amarrage a été acceptée. Votre vaisseau va être remorqué jusqu'à la station.") == 0)
                        {
                            var half = data.Substring(data.LastIndexOf("<", data.LastIndexOf("amarrage") - 1), data.LastIndexOf(">", data.LastIndexOf("amarrage") - 1) - data.LastIndexOf("<", data.LastIndexOf("amarrage")));
                            if (half == "<br")
                                Application.ExitThread();
                            else
                            {
                                var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);
                                Thread presenceThread = new Thread(() => PresenceUpdate(charName, "Amarré(e) à la station: " + full));
                                presenceThread.Start();
                                presenceThread.Join();
                                Application.ExitThread();
                            }
                        }
                        else
                        {
                            Application.ExitThread();
                        }
                    }
                    else
                    {
                        Application.ExitThread();
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
                Filter = ".txt|*.txt",
                AddExtension = true,
            };
            diag.ShowDialog();
            Thread loopThread = new Thread(() => LoopBegin(diag.FileName));
            loopThread.Start();
        }

        private void LoopBegin(string fileName)
        {
            Thread loopThread = new Thread(() => Loop(fileName));
            loopThread.Start();
            loopThread.Join();
            LoopBegin(fileName);
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
            notifyIcon1.Visible = false;
            process.Kill();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                MenuItem menuItem = new MenuItem("&Exit", new EventHandler(Button2_Click));
                menuItem.Index = 0;
                menuItem.Enabled = true;
                menuItem.Visible = true;
                menuItem.Checked = false;

                ContextMenu cmenu = new ContextMenu();
                cmenu.MenuItems.Add(menuItem);

                notifyIcon1.Visible = true;
                notifyIcon1.ContextMenu = cmenu;
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void ButtonCheck()
        {
            if (checkBox1.Checked == true)
            {
                englishCB.Visible = true;
                ingameCB.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                englishCB.Visible = false;
                ingameCB.Visible = false;
                textBox3.Visible = false;
            }
        }
    }
}