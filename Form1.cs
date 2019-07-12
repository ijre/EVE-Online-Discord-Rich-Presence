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
            ButtonCheck();
        }

        private void PresenceUpdate(string details, string state)
        {
            DiscordRpcClient client = new DiscordRpcClient("598364146727124993");
            client.SetPresence(new RichPresence { Details = details, State = state });
            client.Initialize();
#if _NDEBUG
            textBox4.Text = "Presence updated!";
#endif
            Thread.Sleep(9000);
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

            if (russianCB.Checked)
            {
                //Осуществляется = jump
                //Выход из дока = undock
                //Запрос входа в док = dock request
                if (data.LastIndexOf("Осуществляется") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Осуществляется") > data.LastIndexOf("Запрос входа в док"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Осуществляется"), data.IndexOf("*", data.LastIndexOf("Осуществляется")) - data.LastIndexOf("Осуществляется"));
                    var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);
                    Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                    presenceThread.Start();
                    presenceThread.Join();
                    Application.ExitThread();
                }
                else if (data.LastIndexOf("Выход из дока") > data.LastIndexOf("Осуществляется") && data.LastIndexOf("Выход из дока") > data.LastIndexOf("Запрос входа в док"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Выход из дока"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Выход из дока")) + 1) - data.LastIndexOf("Выход из дока"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                    Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                    presenceThread.Start();
                    presenceThread.Join();
                    Application.ExitThread();
                }
                else if (data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Осуществляется"))
                {
                    Thread.Sleep(1000);
                    if (data.Substring(data.LastIndexOf("разрешение")).IndexOf("разрешение на использование дока станции. Приготовьтесь к приему буксира.") != -1)
                    {
                        var half = data.Substring(data.LastIndexOf("Запрос входа в док"), data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf("Запрос входа в док")) + 1));
                        var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - 1) - half.IndexOf("\""));
                        Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Docked at: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                }
            }

            if (frenchCB.Checked)
            {
                if (data.LastIndexOf("Saute") > data.LastIndexOf("amarrage") && data.LastIndexOf("Saute") > data.LastIndexOf("Part de"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Saute"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Saute")) + 1) - data.LastIndexOf("Saute"));
                    //this creates a substring from "Saute" (jump) and ends it at the second instance of * afterwards
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                    //this skips past 2 quotations to find a 3rd quotation mark (the 3rd mark marks the beginning of the solar system name)
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                    if (englishPresCB.Checked)
                    {
                        Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                    else if (ingameCB.Checked)
                    {
                        Thread presenceThread = new Thread(() => PresenceUpdate("Jouer à EVE, sous le nom: " + charName, "En volant dans: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                }
                else if (data.LastIndexOf("Part de") > data.LastIndexOf("amarrage") && data.LastIndexOf("Part de") > data.LastIndexOf("Saute"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Part de"), data.IndexOf(".", data.LastIndexOf("Part de")) + 1 - data.LastIndexOf("Part de"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"") + (half.IndexOf("\"", half.IndexOf("\"") + 1)));
                    //and this skips past 3 quotations to find a 4th quotation mark (the 4th mark marks the beginning of the solar system name)
                    //i know all of this is ungodly painful to look at but i couldnt think of a better way, and you cant use absolute values because different system name lengths and all that
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                    if (englishPresCB.Checked)
                    {
                        Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                    else if (ingameCB.Checked)
                    {
                        Thread presenceThread = new Thread(() => PresenceUpdate("Jouer à EVE, sous le nom: " + charName, "En volant dans: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
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
                            if (englishPresCB.Checked)
                            {
                                Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                                presenceThread.Start();
                                presenceThread.Join();
                                Application.ExitThread();
                            }
                            else if (ingameCB.Checked)
                            {
                                Thread presenceThread = new Thread(() => PresenceUpdate("Jouer à EVE, sous le nom: " + charName, "En volant dans: " + full));
                                presenceThread.Start();
                                presenceThread.Join();
                                Application.ExitThread();
                            }
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
            else if (englishCB.Checked)
            {
                if (data.LastIndexOf(" dock ") > data.LastIndexOf("Undocking") && data.LastIndexOf(" dock ") > data.LastIndexOf("Jumping"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf(" dock "), data.LastIndexOf("station") - data.LastIndexOf(" dock "));
                    if (half.IndexOf("accepted") != -1)
                    {
                        var full = half.Substring(half.IndexOf("at") + 3, half.IndexOf("station") - 10);
                        Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Docked at station: " + full));
                        presenceThread.Start();
                        presenceThread.Join();
                        Application.ExitThread();
                    }
                }

                else if (data.LastIndexOf("Jumping") > data.LastIndexOf(" dock ") && data.LastIndexOf("Jumping") > data.LastIndexOf("Undocking"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Jumping"), data.LastIndexOf("\n") - data.LastIndexOf("Jumping"));
                    var full = half.Substring(half.LastIndexOf("to") + 3, (half.LastIndexOf("\r") - half.LastIndexOf("to")) - 3);
                    Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                    presenceThread.Start();
                    presenceThread.Join();
                    Application.ExitThread();
                }

                else if (data.LastIndexOf("Undocking") > data.LastIndexOf(" dock ") && data.LastIndexOf("Undocking") > data.LastIndexOf("Jumping"))
                {
                    Thread.Sleep(1000);
                    var half = data.Substring(data.LastIndexOf("Undocking"), data.LastIndexOf("\r") - data.LastIndexOf("Undocking"));
                    var full = half.Substring(half.LastIndexOf("to") + 3, (half.LastIndexOf("solar") - 1) - half.LastIndexOf("to") - 3);
                    Thread presenceThread = new Thread(() => PresenceUpdate("Playing EVE, under the name: " + charName, "Flying in: " + full));
                    presenceThread.Start();
                    presenceThread.Join();
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
            Thread.Sleep(1000);
            LoopBegin(fileName);
        }

        private void FolderCheckem()
        {
            if (!File.Exists(Application.StartupPath + "/settings/"))
                Directory.CreateDirectory(Application.StartupPath + "/settings/");
        }

        private void EnglishCB_CheckedChanged(object sender, EventArgs e)
        {
            FolderCheckem();
            if (englishPresCB.Checked == true)
            {
                if (ingameCB.Checked == true)
                    ingameCB.Checked = false;

                File.WriteAllText(Application.StartupPath + "/settings/" + "presenceSetting.txt", "english");
            }
        }

        private void IngameCB_CheckedChanged(object sender, EventArgs e)
        {
            FolderCheckem();
            if (ingameCB.Checked == true)
            {
                if (englishPresCB.Checked == true)
                    englishPresCB.Checked = false;

                File.WriteAllText(Application.StartupPath + "/settings/" + "presenceSetting.txt", "ingame");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DiscordRpcClient client = new DiscordRpcClient("598364146727124993");
            client.SetPresence(new RichPresence { Details = "", State = "" });
            //Fun Fact: setting the presence to this technically throws an error (state and details must be at least two letters long)
            //but it's actually really effective for clearing presences
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
            try
            {
                switch (File.ReadAllText(Application.StartupPath + "/settings/" + "presenceSettings.txt"))
                {
                    case "english":
                        englishPresCB.Checked = true;
                        break;
                    case "ingame":
                        ingameCB.Checked = true;
                        break;
                }
                switch (File.ReadAllText(Application.StartupPath + "/settings/" + "language.txt"))
                {
                    case "russian":
                        russianCB.Checked = true;
                        ingameCB.Visible = false;
                        textBox3.Visible = true;
                        englishPresCB.Visible = true;
                        break;
                    case "french":
                        frenchCB.Checked = true;
                        ingameCB.Visible = true;
                        textBox3.Visible = true;
                        englishPresCB.Visible = true;
                        break;
                    case "japanese":
                        japaneseCB.Checked = true;
                        ingameCB.Visible = false;
                        textBox3.Visible = true;
                        englishPresCB.Visible = true;
                        break;
                    case "english":
                        englishCB.Checked = true;
                        ingameCB.Visible = false;
                        textBox3.Visible = false;
                        englishPresCB.Visible = false;
                        break;
                    case "german":
                        germanCB.Checked = true;
                        ingameCB.Visible = false;
                        textBox3.Visible = true;
                        englishPresCB.Visible = true;
                        break;
                }
            }
            catch
            { FolderCheckem(); }
        }

        private void ButtonSave()
        {
            var path = Application.StartupPath + "/settings/" + "language.txt";
            if (russianCB.Checked == true)
                File.WriteAllText(path, "russian");

            else if (frenchCB.Checked == true)
                File.WriteAllText(path, "french");

            else if (germanCB.Checked == true)
                File.WriteAllText(path, "german");

            else if (japaneseCB.Checked == true)
                File.WriteAllText(path, "japanese");

            else if (englishCB.Checked == true)
                File.WriteAllText(path, "english");
        }

        private void RussianCB_CheckedChanged(object sender, EventArgs e)
        {
            if (russianCB.Checked == true)
            {
                if (frenchCB.Checked == true || germanCB.Checked == true || japaneseCB.Checked == true || englishCB.Checked == true)
                {
                    frenchCB.Checked = false;
                    germanCB.Checked = false;
                    japaneseCB.Checked = false;
                    englishCB.Checked = false;

                    russianCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
                else
                {
                    russianCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
            }
        }

        private void GermanCB_CheckedChanged(object sender, EventArgs e)
        {
            if (germanCB.Checked == true)
            {
                if (frenchCB.Checked == true || russianCB.Checked == true || japaneseCB.Checked == true || englishCB.Checked == true)
                {
                    frenchCB.Checked = false;
                    russianCB.Checked = false;
                    japaneseCB.Checked = false;
                    englishCB.Checked = false;

                    germanCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
                else
                {
                    germanCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
            }
        }

        private void FrenchCB_CheckedChanged(object sender, EventArgs e)
        {
            if (frenchCB.Checked == true)
            {
                if (germanCB.Checked == true || russianCB.Checked == true || japaneseCB.Checked == true || englishCB.Checked == true)
                {
                    germanCB.Checked = false;
                    russianCB.Checked = false;
                    japaneseCB.Checked = false;
                    englishCB.Checked = false;

                    frenchCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = true;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
                else
                {
                    frenchCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = true;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
            }
        }

        private void JapaneseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (japaneseCB.Checked == true)
            {
                if (frenchCB.Checked == true || russianCB.Checked == true || germanCB.Checked == true || englishCB.Checked == true)
                {
                    frenchCB.Checked = false;
                    russianCB.Checked = false;
                    germanCB.Checked = false;
                    englishCB.Checked = false;

                    japaneseCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
                else
                {
                    japaneseCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = true;
                    englishPresCB.Visible = true;
                }
            }
        }

        private void EnglishCB_CheckedChanged_1(object sender, EventArgs e)
        {
            if (englishCB.Checked == true)
            {
                if (frenchCB.Checked == true || russianCB.Checked == true || germanCB.Checked == true || japaneseCB.Checked == true)
                {
                    frenchCB.Checked = false;
                    russianCB.Checked = false;
                    germanCB.Checked = false;
                    japaneseCB.Checked = false;

                    englishCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = false;
                    englishPresCB.Visible = false;
                }
                else
                {
                    englishCB.Checked = true;
                    ButtonSave();
                    ingameCB.Visible = false;
                    textBox3.Visible = false;
                    englishPresCB.Visible = false;
                }
            }
        }
    }
}