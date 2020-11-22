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
            JUMPING = 1,
            UNDOCKING,
            DOCKING,
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

        #region PresenceHelpers
        private int[] FindLastAction(string[] data, string jump, string undock, string dock)
        {
            int jumpLoc = -1;
            int undockLoc = -1;
            int dockLoc = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (jumpLoc != -1 && undockLoc != -1 && dockLoc != -1)
                    break;

                if (data[i].LastIndexOf(jump) != -1)
                {
                    jumpLoc = i;
                }
                else if (data[i].LastIndexOf(undock) != -1)
                {
                    undockLoc = i;
                }
                else if (data[i].LastIndexOf(dock) != -1)
                {
                    dockLoc = i;
                }
            }

            if (jumpLoc > undockLoc && jumpLoc > dockLoc)
            {
                return new[] { jumpLoc, 1 };
            }
            if (undockLoc > jumpLoc && undockLoc > dockLoc)
            {
                return new[] { undockLoc, 2 };
            }
            if (dockLoc > jumpLoc && dockLoc > undockLoc)
            {
                return new[] { dockLoc, 3 };
            }

            return new[] { 0, 4 };
        }

        private string GetStringBetween(string main, string before, string after, bool getLocalized)
        {
            if (!getLocalized && main.Contains("localized hint"))
            {
                main = main.Remove(main.IndexOf("<localized hint=", main.LastIndexOf(before)),
                    main.IndexOf(">", main.IndexOf("<localized hint=", main.LastIndexOf(before))) + 1 - main.IndexOf("<localized hint=", main.LastIndexOf(before)));
            }

            return getLocalized ?
                main.Substring(main.IndexOf("\"", main.LastIndexOf(before)) + 1, main.LastIndexOf("\"", main.LastIndexOf(after)) - (main.IndexOf("\"", main.LastIndexOf(before)) + 1))
                :
                main.Substring(main.LastIndexOf(before) + before.Length, main.LastIndexOf(after) - (main.LastIndexOf(before) + before.Length));
        }
        #endregion

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
#if _NDEBUG
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
#endif
            }

            string details = $"Playing EVE, under the name: \"{charName}\"";
            string state = "";

            #region PresenceSetting
            if (English.Checked)
            {
                int[] mostRecentAction = FindLastAction(dataArray, "Jumping", "Undocking", " dock ");
                var mostRecentActionType = (ActionSearchReturnTypes)mostRecentAction[1];

                string data = dataArray[mostRecentAction[0]];

                switch (mostRecentActionType)
                {
                    case ActionSearchReturnTypes.ERROR:
                        return;
                    case ActionSearchReturnTypes.JUMPING:
                    {
                        state = $"Flying in: {data.Substring(data.LastIndexOf(" to ") + 4)}";
                        break;
                    }
                    case ActionSearchReturnTypes.UNDOCKING:
                    {
                        state = $"Flying in: {GetStringBetween(data, " to ", " solar ", false)}";
                        break;
                    }
                    case ActionSearchReturnTypes.DOCKING:
                    {
                        for (int i = mostRecentAction[0]; i < dataArray.Length; i++)
                        {
                            if (!dataArray[i].Contains("accepted"))
                                continue;

                            state = $"Docked at station: {GetStringBetween(data, " at ", " station", false)}";

                            break;
                        }

                        if (string.IsNullOrWhiteSpace(state))
                            return;

                        break;
                    }
                }
            }
            else if (French.Checked)
            {
                // Saute = jump
                // Part de = undock
                // amarrer = dock (in request)
                // amarrage = dock (in request acceptance)
                // acceptée = accepted

                int[] mostRecentAction = FindLastAction(dataArray, "Saute de", "Part de", "s'amarrer");
                var mostRecentActionType = (ActionSearchReturnTypes)mostRecentAction[1];

                string data = dataArray[mostRecentAction[0]];

                bool usingEnglishTooltips = data.Contains("localized hint") && EnglishPresCB.Checked;

                switch (mostRecentActionType)
                {
                    case ActionSearchReturnTypes.ERROR:
                        return;
                    case ActionSearchReturnTypes.JUMPING:
                    {
                        state = (EnglishPresCB.Checked ? "Flying in: " : "En volant dans: ") + GetStringBetween(data, "à ", "*", usingEnglishTooltips);
                        break;
                    }
                    case ActionSearchReturnTypes.UNDOCKING:
                    {
                        state = (EnglishPresCB.Checked ? "Flying in: " : "En volant dans: ") + GetStringBetween(data, " solaire ", "*.", usingEnglishTooltips);
                        break;
                    }
                    case ActionSearchReturnTypes.DOCKING:
                    {
                        for (int i = mostRecentAction[0]; i < dataArray.Length; i++)
                        {
                            if (!dataArray[i].Contains("acceptée"))
                                continue;

                            state = (EnglishPresCB.Checked ? "Docked at station: " : "Amarré(e) à la station: ") + GetStringBetween(data, "station ", "*", usingEnglishTooltips);

                            break;
                        }

                        if (string.IsNullOrWhiteSpace(state))
                            return;

                        break;
                    }
                }

                if (IngamePresCB.Checked)
                    details = $"Jouer à EVE, sous le nom: \"{charName}\"";
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
        private void UncheckOthers(Control check)
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
            IngamePresCB.Visible = check.Name != English.Name;
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
                UncheckOthers(French);
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