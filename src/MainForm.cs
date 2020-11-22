using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DiscordRPC;

namespace eve_discord_rpc
{
    public partial class MainForm : Form
    {
        private readonly string LanguageSettings = $"{Application.StartupPath}\\Settings\\LanguageSettings.txt";
        private readonly DiscordRpcClient Client = new DiscordRpcClient("688787694498611260");
        private string file = "";

        public MainForm()
        {
            InitializeComponent();

            try
            {
                switch (File.ReadAllText(LanguageSettings))
                {
                    case "english":
                        EnglishPresCB.Checked = true;
                        break;
                    case "ingame":
                        IngamePresCB.Checked = true;
                        break;
                }

                switch (File.ReadAllText(LanguageSettings))
                {
                    case "russian":
                        Russian.Checked = true;
                        IngamePresCB.Visible = false;
                        break;

                    case "french":
                        French.Checked = true;
                        IngamePresCB.Visible = true;
                        break;

                    case "japanese":
                        Japanese.Checked = true;
                        IngamePresCB.Visible = false;
                        break;

                    case "english":
                        English.Checked = true;
                        IngamePresCB.Visible = false;
                        break;

                    case "german":
                        German.Checked = true;
                        IngamePresCB.Visible = false;
                        break;
                }
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Settings\\");
            }
            catch (FileNotFoundException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PresenceTimer_Tick(object sender, EventArgs e)
        {
            string data = File.ReadAllText(file);

            Process process = Process.GetProcessesByName("exefile")[0];
            var charName = process.MainWindowTitle.Substring(6);

            string details = "";
            string state = "";

            #region PresenceSetting
            if (Russian.Checked)
            {
                // Осуществляется = jump
                // Выход из дока = undock
                // Запрос входа в док = dock request
                if (data.LastIndexOf("Осуществляется") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Осуществляется") > data.LastIndexOf("Запрос входа в док"))
                {
                    var half = data.Substring(data.LastIndexOf("Осуществляется"), data.IndexOf("*", data.LastIndexOf("Осуществляется")) - data.LastIndexOf("Осуществляется"));
                    var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("Выход из дока") > data.LastIndexOf("Осуществляется") && data.LastIndexOf("Выход из дока") > data.LastIndexOf("Запрос входа в док"))
                {
                    var half = data.Substring(data.LastIndexOf("Выход из дока"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Выход из дока")) + 1) - data.LastIndexOf("Выход из дока"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Осуществляется"))
                {
                    if (data.Substring(data.LastIndexOf("разрешение")).IndexOf("разрешение на использование дока станции. Приготовьтесь к приему буксира.") != -1)
                    {
                        var half = data.Substring(data.LastIndexOf("Запрос входа в док"), data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf("Запрос входа в док")) + 1));
                        var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - 1) - half.IndexOf("\""));

                        details = "Playing EVE, under the name: " + charName;
                        state = "Docked at: " + full;
                    }
                }
                else
                    details = "Idle" + (String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}");
            }
            else if (French.Checked)
            {
                // Saute = jump
                // Parte de = undock
                // amarrage = dock(ing)
                if (data.LastIndexOf("Saute") > data.LastIndexOf("amarrage") && data.LastIndexOf("Saute") > data.LastIndexOf("Part de"))
                {
                    var half = data.Substring(data.LastIndexOf("Saute"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Saute")) + 1) - data.LastIndexOf("Saute"));
                    // this creates a substring from "Saute" (jump) and ends it at the second instance of * afterwards
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                    // this skips past 2 quotations to find a 3rd quotation mark (the 3rd mark marks the beginning of the solar system name)
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                    if (EnglishPresCB.Checked)
                    {
                        details = "Playing EVE, under the name: " + charName;
                        state = "Flying in: " + full;
                    }
                    else
                    {
                        details = "Jouer à EVE, sous le nom: " + charName;
                        state = "En volant dans: " + full;
                    }
                }
                else if (data.LastIndexOf("Part de") > data.LastIndexOf("amarrage") && data.LastIndexOf("Part de") > data.LastIndexOf("Saute"))
                {
                    var half = data.Substring(data.LastIndexOf("Part de"), data.IndexOf(".", data.LastIndexOf("Part de")) + 1 - data.LastIndexOf("Part de"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"") + (half.IndexOf("\"", half.IndexOf("\"") + 1)));
                    // and this skips past 3 quotations to find a 4th quotation mark (the 4th mark marks the beginning of the solar system name)
                    // i know all of this is ungodly painful to look at but i couldnt think of a better way, and you cant use absolute values because different system name lengths and all that
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));
                    if (EnglishPresCB.Checked)
                    {
                        details = "Playing EVE, under the name: " + charName;
                        state = "Flying in: " + full;
                    }
                    else
                    {
                        details = "Jouer à EVE, sous le nom: " + charName;
                        state = "En volant dans: " + full;
                    }
                }
                else if (data.LastIndexOf("amarrage") > data.LastIndexOf("Part de") && data.LastIndexOf("amarrage") > data.LastIndexOf("Saute"))
                {
                    if (data.Substring(data.LastIndexOf("amarrage")).IndexOf("amarrage a été acceptée. Votre vaisseau va être remorqué jusqu'à la station.") == 0)
                    {
                        var half = data.Substring(data.LastIndexOf("<", data.LastIndexOf("amarrage") - 1), data.LastIndexOf(">", data.LastIndexOf("amarrage") - 1) - data.LastIndexOf("<", data.LastIndexOf("amarrage")));
                        if (half == "<br")
                            Application.ExitThread();
                        else
                        {
                            var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);
                            if (EnglishPresCB.Checked)
                            {
                                details = "Playing EVE, under the name: " + charName;
                                state = "Docked at: " + full;
                            }
                            else
                            {
                                details = "Jouer à EVE, sous le nom: " + charName;
                                state = "Amarré(e) à la station: " + full;
                            }
                        }
                    }
                }
                else
                {
                    if (EnglishPresCB.Checked)
                        details = "Idle" + (String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}");
                    else
                        details = "Actuellment absent" + (String.IsNullOrWhiteSpace(charName) ? "" : $", sous le nom: {charName}");
                }

            }
            else if (English.Checked)
            {
                if (data.LastIndexOf("Jumping") > data.LastIndexOf(" dock ") && data.LastIndexOf("Jumping") > data.LastIndexOf("Undocking"))
                {
                    var half = data.Substring(data.LastIndexOf("Jumping"), data.LastIndexOf("\n") - data.LastIndexOf("Jumping"));
                    var full = half.Substring(half.LastIndexOf("to") + 3, (half.LastIndexOf("\r") - half.LastIndexOf("to")) - 3);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("Undocking") > data.LastIndexOf(" dock ") && data.LastIndexOf("Undocking") > data.LastIndexOf("Jumping"))
                {
                    var half = data.Substring(data.LastIndexOf("Undocking"), data.LastIndexOf("\r") - data.LastIndexOf("Undocking"));
                    var full = half.Substring(half.LastIndexOf("to") + 3, (half.LastIndexOf("solar") - 1) - half.LastIndexOf("to") - 3);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf(" dock ") > data.LastIndexOf("Undocking") && data.LastIndexOf(" dock ") > data.LastIndexOf("Jumping"))
                {
                    var half = data.Substring(data.LastIndexOf(" dock "), data.LastIndexOf("station") - data.LastIndexOf(" dock "));
                    if (half.IndexOf("accepted") != -1)
                    {
                        var full = half.Substring(half.IndexOf("at") + 3, half.IndexOf("station") - 10);

                        details = "Playing EVE, under the name: " + charName;
                        state = "Docked at station: " + full;
                    }
                }
                else
                    details = "Idle" + (String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}");
            }
            else if (German.Checked)
            {
                // Springe = jump
                // Abdocken = undock
                // Andockerlaubnis = dock request
                if (data.LastIndexOf("Abdocken") > data.LastIndexOf("Andockerlaubnis") && data.LastIndexOf("Abdocken") > data.LastIndexOf("Springe"))
                {
                    var half = data.Substring(data.LastIndexOf("Abdocken"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Abdocken")) + 1) - data.LastIndexOf("Abdocken"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"") + (half.IndexOf("\"", half.IndexOf("\"") + 1)));
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1) - mouthfull) - 1);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("Springe") > data.LastIndexOf("Andockerlaubnis") && data.LastIndexOf("Springe") > data.LastIndexOf("Abdocken"))
                {
                    var half = data.Substring(data.LastIndexOf("Springe"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Springe")) + 1) - data.LastIndexOf("Springe"));
                    var full = half.Substring(half.IndexOf("\"", half.IndexOf("nach")) + 1, half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("nach")) + 1) - half.IndexOf("\"", half.IndexOf("nach")) - 1);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("Andockerlaubnis") > data.LastIndexOf("Abdocken") && data.LastIndexOf("Andockerlaubnis") > data.LastIndexOf("Springe"))
                {
                    if (data.Substring(data.LastIndexOf("akzeptiert")).IndexOf("akzeptiert. Ihr Schiff") == 0)
                    {
                        var full = data.Substring(data.LastIndexOf("Andockerlaubnis"), data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf("Andockerlaubnis")) + 1) - data.LastIndexOf("Andockerlaubnis"));

                        details = "Playing EVE, under the name: " + charName;
                        state = "Docked at station: " + full;
                    }
                }
                else
                    details = "Idle" + (String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}");
            }
            else if (Japanese.Checked)
            {
                // ステーションに入港許可を申請 = dock request
                // へ出港 = undock(ing)
                // へジャンプ中 = jump
                if (data.LastIndexOf("へ出港") > data.LastIndexOf("ステーションに入港許可を申請") && data.LastIndexOf("へ出港") > data.LastIndexOf("へジャンプ中"))
                {
                    var full = data.Substring(data.IndexOf("\"", data.LastIndexOf(" から ")) + 1, data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf(" から ")) + 1) - data.IndexOf("\"", data.LastIndexOf(" から ")) - 1);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("へジャンプ中") > data.LastIndexOf("へ出港") && data.LastIndexOf("へジャンプ中") > data.LastIndexOf("ステーションに入港許可を申請"))
                {
                    var full = data.Substring(data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - 1) + 1, data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - 1) - 1);

                    details = "Playing EVE, under the name: " + charName;
                    state = "Flying in: " + full;
                }
                else if (data.LastIndexOf("ステーションに入港許可を申請") > data.LastIndexOf("へ出港") && data.LastIndexOf("ステーションに入港許可を申請") > data.LastIndexOf("へジャンプ中"))
                {
                    if (data.LastIndexOf("入港許可申請が許可されました。ステーション内に牽引されます") > data.LastIndexOf("ステーションに入港許可を申請"))
                    {
                        var full = data.Substring(data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - 1) + 1, data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - 1) - 1);

                        details = "Playing EVE, under the name: " + charName;
                        state = "Docked at station: " + full;
                    }
                }
                else
                    details = "Idle" + (String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}");
            }
            #endregion


            Client.SetPresence(new RichPresence { Details = details, State = state, Assets = new Assets { LargeImageKey = "cover", LargeImageText = "https://github.com/ijre/EVE-Online_Discord-RPC" } });
            ParsingBox.Text = "Rich Presence set!";
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var forceThrow = Process.GetProcessesByName("exefile")[0].Id;
            }
            catch
            {
                MessageBox.Show("Note: This program requires the most recent Gamelogs file, a file that is created after you load into your character.\n" +
                                "Please start the game and select your character before continuing."
                                , "Start game before continuing", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            using (OpenFileDialog diag = new OpenFileDialog
            {
                Title = "Please select your most recent Gamelogs file.",
                InitialDirectory = Environment.SpecialFolder.CommonDocuments + "EVE\\logs\\Gamelogs",
                DefaultExt = ".txt",
                Filter = ".txt|*.txt"
            })
            {
                diag.ShowDialog();

                if (string.IsNullOrWhiteSpace(diag.FileName))
                    return;

                file = diag.FileName;
                PresenceTimer.Enabled = true;

                Client.Initialize();
                PresenceTimer_Tick(sender, e); // force call once to send update
            }
        }

        #region Helpers
        private void UncheckOthers(Control check, bool showIngamePres = false)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].GetType() != typeof(CheckBox) || Controls[i] == check || Controls[i].Name == "EnglishPresCB" || Controls[i].Name == "IngamePresCB")
                    continue;

                ((CheckBox)Controls[i]).Checked = false;
            }

            SaveSettings(check.Name, true);
            PresLangQuestion.Visible = true;
            EnglishPresCB.Visible = true;
            IngamePresCB.Visible = showIngamePres;
        }

        private void SaveSettings(string newSetting, bool isGameLang)
        {
            // first line is the game's language, second line is presence language
            string[] currentSettings;

            try
            {
                currentSettings = File.ReadAllLines(LanguageSettings);
            }
            catch (FileNotFoundException)
            {
                currentSettings = new[] { "", "" };
            }

            File.WriteAllLines(LanguageSettings,
                isGameLang ? new[] { newSetting, currentSettings[1] } : new[] { currentSettings[0], newSetting });
        }
        #endregion

        #region CheckedChangedEvent
        private void EnglishPresCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!EnglishPresCB.Checked)
                return;

            IngamePresCB.Checked = false;

            SaveSettings("english", false);
            Browse.Visible = true;
        }

        private void IngameCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!IngamePresCB.Checked)
                return;

            EnglishPresCB.Checked = false;

            SaveSettings("ingame", false);
            Browse.Visible = true;
        }

        private void English_CheckedChanged(object sender, EventArgs e)
        {
            if (English.Checked)
                UncheckOthers(English, true);
        }

        private void French_CheckedChanged(object sender, EventArgs e)
        {
            if (French.Checked)
                UncheckOthers(French, true);
        }

        private void Russian_CheckedChanged(object sender, EventArgs e)
        {
            if (Russian.Checked)
                UncheckOthers(Russian);
        }

        private void German_CheckedChanged(object sender, EventArgs e)
        {
            if (German.Checked)
                UncheckOthers(German);
        }

        private void Japanese_CheckedChanged(object sender, EventArgs e)
        {
            if (Japanese.Checked)
                UncheckOthers(Japanese);
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ReSharper disable once InvertIf
            if (Client.IsInitialized)
            {
                Client.ClearPresence();
                Client.Dispose();
            }
        }
    }
}