using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DiscordRPC;

namespace eve_discord_rpc
{
    public partial class MainForm : Form
    {
        private readonly string LanguageSettings = $"{Application.StartupPath}\\Settings\\LanguageSettings.txt";
        private readonly DiscordRpcClient Client = new DiscordRpcClient("688787694498611260");
        private string file = "";

        enum ActionSearchReturnTypes
        {
            FIRST = 1,
            SECOND,
            THIRD,
            ERROR
        }

        public MainForm()
        {
            InitializeComponent();

            string[] savedSettings;

            try
            {
                savedSettings = File.ReadAllLines(LanguageSettings);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory($"{Application.StartupPath}\\Settings\\");
                return;
            }
            catch (FileNotFoundException) { return; }

            switch (savedSettings[0])
            {
                case "english":
                    English.Checked = true;
                    break;

                case "french":
                    French.Checked = true;
                    break;

                case "german":
                    German.Checked = true;
                    break;

                case "russian":
                    Russian.Checked = true;
                    break;

                case "japanese":
                    Japanese.Checked = true;
                    break;
            }

            switch (savedSettings[1])
            {
                case "english":
                    EnglishPresCB.Checked = true;
                    break;

                case "ingame":
                    IngamePresCB.Checked = true;
                    break;
            }
        }

        private void PresenceTimer_Tick(object sender, EventArgs e)
        {
            string[] dataArray = File.ReadAllLines(file).ToArray();

            var charName = "";

            try
            {
                Process process = Process.GetProcessesByName("exefile")[0];
                charName = process.MainWindowTitle.Substring(6);
            }
            catch (IndexOutOfRangeException)
            {
                Client.SetPresence(new RichPresence
                {
                    Details = "Tell this user they left their RPC program up for EVE Online!",
                    Assets = new Assets
                    {
                        LargeImageKey = "cover",
                        LargeImageText = "https://github.com/ijre/EVE-Online_Discord-RPC"
                    }
                });

                return;
            }

            string details = $"Playing EVE, under the name \"{charName}\"";
            string state = "";

            #region PresenceSetting
            if (English.Checked)
            {
                int[] mostRecentAction = FindLastAction(dataArray, "Jumping", "Undocking", " dock ");

                string data = dataArray[mostRecentAction[0]];
                var mostRecentActionType = (ActionSearchReturnTypes)mostRecentAction[1];

                switch (mostRecentActionType)
                {
                    case ActionSearchReturnTypes.FIRST:
                    {
                        state = $"Flying in: {data.Substring(data.LastIndexOf(" to ") + 4)}";
                        break;
                    }
                    case ActionSearchReturnTypes.SECOND:
                    {
                        string location = data.Substring(data.LastIndexOf(" to ") + 4, data.LastIndexOf(" solar ") - (data.LastIndexOf(" to ") + 4));

                        state = $"Flying in: {location}";
                        break;
                    }
                    case ActionSearchReturnTypes.THIRD:
                    {
                        for (int i = mostRecentAction[0]; i < dataArray.Length; i++)
                        {
                            if (!dataArray[i].Contains("accepted"))
                                continue;

                            string location = data.Substring(data.IndexOf(" at ") + 4,
                                data.LastIndexOf(" station") - (data.IndexOf(" at ") + 4));

                            state = $"Docked at station: {location}";

                            goto SkipReturn;
                        }

                        return;

                        // it'll incorrectly say a player is docked somewhere when they werent accepted if we dont return
                        // thus the reason for using goto to jump over said return if it finds the accepted message

                        SkipReturn:

                        break;
                    }
                    case ActionSearchReturnTypes.ERROR:
                        return;
                }
            }
            else if (French.Checked)
            {
                // Saute = jump
                // Parte de = undock
                // amarrage = dock(ing)

            }/*
            else if (Russian.Checked)
            {
                // Осуществляется = jump
                // Выход из дока = undock
                // Запрос входа в док = dock request
                if (data.LastIndexOf("Осуществляется") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Осуществляется") > data.LastIndexOf("Запрос входа в док"))
                {
                    var half = data.Substring(data.LastIndexOf("Осуществляется"), data.IndexOf("*", data.LastIndexOf("Осуществляется")) - data.LastIndexOf("Осуществляется"));
                    var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - half.IndexOf("\"")) - 1);

                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("Выход из дока") > data.LastIndexOf("Осуществляется") && data.LastIndexOf("Выход из дока") > data.LastIndexOf("Запрос входа в док"))
                {
                    var half = data.Substring(data.LastIndexOf("Выход из дока"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Выход из дока")) + 1) - data.LastIndexOf("Выход из дока"));
                    var mouthfull = half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("\"", (half.IndexOf("\"") + 1)) + 1));
                    var full = half.Substring(mouthfull + 1, (half.IndexOf("\"", mouthfull + 1)) - (mouthfull + 1));

                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Выход из дока") && data.LastIndexOf("Запрос входа в док") > data.LastIndexOf("Осуществляется"))
                {
                    if (data.Substring(data.LastIndexOf("разрешение")).IndexOf("разрешение на использование дока станции. Приготовьтесь к приему буксира.") != -1)
                    {
                        var half = data.Substring(data.LastIndexOf("Запрос входа в док"), data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf("Запрос входа в док")) + 1));
                        var full = half.Substring(half.IndexOf("\"") + 1, (half.LastIndexOf("\"") - 1) - half.IndexOf("\""));


                        state = $"Docked at: {full}";
                    }
                }
                else
                    details = $"Idle{(String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}")}";
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


                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("Springe") > data.LastIndexOf("Andockerlaubnis") && data.LastIndexOf("Springe") > data.LastIndexOf("Abdocken"))
                {
                    var half = data.Substring(data.LastIndexOf("Springe"), data.IndexOf("*", data.IndexOf("*", data.LastIndexOf("Springe")) + 1) - data.LastIndexOf("Springe"));
                    var full = half.Substring(half.IndexOf("\"", half.IndexOf("nach")) + 1, half.IndexOf("\"", half.IndexOf("\"", half.IndexOf("nach")) + 1) - half.IndexOf("\"", half.IndexOf("nach")) - 1);


                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("Andockerlaubnis") > data.LastIndexOf("Abdocken") && data.LastIndexOf("Andockerlaubnis") > data.LastIndexOf("Springe"))
                {
                    if (data.Substring(data.LastIndexOf("akzeptiert")).IndexOf("akzeptiert. Ihr Schiff") == 0)
                    {
                        var full = data.Substring(data.LastIndexOf("Andockerlaubnis"), data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf("Andockerlaubnis")) + 1) - data.LastIndexOf("Andockerlaubnis"));


                        state = $"Docked at station: {full}";
                    }
                }
                else
                    details = $"Idle{(String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}")}";
            }
            else if (Japanese.Checked)
            {
                // ステーションに入港許可を申請 = dock request
                // へ出港 = undock(ing)
                // へジャンプ中 = jump
                if (data.LastIndexOf("へ出港") > data.LastIndexOf("ステーションに入港許可を申請") && data.LastIndexOf("へ出港") > data.LastIndexOf("へジャンプ中"))
                {
                    var full = data.Substring(data.IndexOf("\"", data.LastIndexOf(" から ")) + 1, data.IndexOf("\"", data.IndexOf("\"", data.LastIndexOf(" から ")) + 1) - data.IndexOf("\"", data.LastIndexOf(" から ")) - 1);


                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("へジャンプ中") > data.LastIndexOf("へ出港") && data.LastIndexOf("へジャンプ中") > data.LastIndexOf("ステーションに入港許可を申請"))
                {
                    var full = data.Substring(data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - 1) + 1, data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("へジャンプ中")) - 1) - 1);


                    state = $"Flying in: {full}";
                }
                else if (data.LastIndexOf("ステーションに入港許可を申請") > data.LastIndexOf("へ出港") && data.LastIndexOf("ステーションに入港許可を申請") > data.LastIndexOf("へジャンプ中"))
                {
                    if (data.LastIndexOf("入港許可申請が許可されました。ステーション内に牽引されます") > data.LastIndexOf("ステーションに入港許可を申請"))
                    {
                        var full = data.Substring(data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - 1) + 1, data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - data.LastIndexOf("\"", data.LastIndexOf("\"", data.LastIndexOf("ステーションに入港許可を申請")) - 1) - 1);


                        state = $"Docked at station: {full}";
                    }
                }
                else
                    details = $"Idle{(String.IsNullOrWhiteSpace(charName) ? "" : $", under the name: {charName}")}";
            }*/
            #endregion


            Client.SetPresence(new RichPresence
            {
                Details = details,
                State = state,
                Assets = new Assets
                {
                    LargeImageKey = "cover",
                    LargeImageText = "https://github.com/ijre/EVE-Online_Discord-RPC"
                }
            });

            ParsingBox.Text = "Rich Presence set!";
        }

        private void Browse_Click(object sender, EventArgs e)
        {
#if _NDEBUG
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
#endif

            using (OpenFileDialog diag = new OpenFileDialog
            {
                Title = "Please select your most recent Gamelogs file.",
                InitialDirectory = $"{Environment.SpecialFolder.CommonDocuments}EVE\\logs\\Gamelogs",
                DefaultExt = ".txt",
                Filter = ".txt|*.txt"
            })
            {
                diag.ShowDialog();

                if (string.IsNullOrWhiteSpace(diag.FileName))
                    return;

                file = diag.FileName;
                PresenceTimer.Enabled = true;

                if (!Client.IsInitialized)
                    Client.Initialize();

                PresenceTimer_Tick(sender, e); // force call once to send update
            }
        }

        #region Helpers
        #region PresenceHelpers
        private int[] FindLastAction(string[] data, string first, string second, string third)
        {
            int firstLoc = -1;
            int secondLoc = -1;
            int thirdLoc = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (firstLoc != -1 && secondLoc != -1 && thirdLoc != -1)
                    break;

                if (data[i].LastIndexOf(first) != -1)
                {
                    firstLoc = i;
                }
                else if (data[i].LastIndexOf(second) != -1)
                {
                    secondLoc = i;
                }
                else if (data[i].LastIndexOf(third) != -1)
                {
                    thirdLoc = i;
                }
            }

            if (firstLoc > secondLoc && firstLoc > thirdLoc)
            {
                return new[] { firstLoc, 1 };
            }
            if (secondLoc > firstLoc && secondLoc > thirdLoc)
            {
                return new[] { secondLoc, 2 };
            }
            if (thirdLoc > firstLoc && thirdLoc > secondLoc)
            {
                return new[] { thirdLoc, 3 };
            }

            return new[] { data.Length - 1, 4 };
        }
        #endregion // PresenceHelpers

        private void UncheckOthers(Control check, bool showIngamePres = false)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].GetType() != typeof(CheckBox) || Controls[i] == check || Controls[i].Name == "EnglishPresCB" || Controls[i].Name == "IngamePresCB")
                    continue;

                ((CheckBox)Controls[i]).Checked = false;
            }

            SaveSettings(check.Name.ToLower(), true);
            PresLangQuestion.Visible = check.Name != English.Name;
            EnglishPresCB.Visible = check.Name != English.Name;
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
                UncheckOthers(English);
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