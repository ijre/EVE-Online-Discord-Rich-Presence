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
        #region Globals
        private readonly DiscordRpcClient Client = new DiscordRpcClient("688787694498611260");
        private readonly string LanguageSettings = $"{Application.StartupPath}\\Settings\\LanguageSettings.txt";

        private string file = "";

        private Language CurrentLanguage;
        private int[] PreviousAction = { 0, 0 };
        #endregion // Globals

        private enum ActionTypes
        {
            JUMPING,
            UNDOCKING,
            DOCKING
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
        private int[] FindLastAction(string[] data)
        {
            var jump = CurrentLanguage.GetStringsFromActionType(ActionTypes.JUMPING)[0];
            var undock = CurrentLanguage.GetStringsFromActionType(ActionTypes.UNDOCKING)[0];
            var dock = CurrentLanguage.GetStringsFromActionType(ActionTypes.DOCKING)[0];
            // using shorter names for readability

            int jumpLoc = -1;
            int undockLoc = -1;
            int dockLoc = -1;

            for (int i = PreviousAction[0]; i < data.Length; i++)
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
                return new[] { jumpLoc, 0 };
            }
            if (undockLoc > jumpLoc && undockLoc > dockLoc)
            {
                return new[] { undockLoc, 1 };
            }
            if (dockLoc > jumpLoc && dockLoc > undockLoc)
            {
                return new[] { dockLoc, 2 };
            }

            return new[] { 0, 3 };
        }

        private string GetStringBetween(string main, string before, string after)
        {
            bool getLocalized = main.Contains("localized hint") && EnglishPresCB.Checked;

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
                    Details = CurrentLanguage.m_DetailsStrings[1],
                    Assets = new Assets
                    {
                        LargeImageKey = "cover",
                        LargeImageText = "https://github.com/ijre/EVE-Online_Discord-RPC"
                    }
                });

                return;
#endif
            }

            int[] mostRecentAction = FindLastAction(dataArray);
            string data = dataArray[mostRecentAction[0]];
            var mostRecentActionType = (ActionTypes)mostRecentAction[1];

            var actionStrings = CurrentLanguage.GetStringsFromActionType(mostRecentActionType);

            string details = $"{CurrentLanguage.m_DetailsStrings[0]} {charName}";
            string state = "";

            if (mostRecentActionType < ActionTypes.DOCKING)
            {
                state = $"{CurrentLanguage.m_StateStrings[0]} {GetStringBetween(data, actionStrings[1], actionStrings[2])}";
            }
            else if (mostRecentActionType == ActionTypes.DOCKING)
            {
                for (int i = mostRecentAction[0]; i < dataArray.Length; i++)
                {
                    if (!dataArray[i].Contains(actionStrings[3]))
                        continue;

                    state = $"{CurrentLanguage.m_StateStrings[1]} {GetStringBetween(data, actionStrings[1], actionStrings[2])}";
                    break;
                }

                if (string.IsNullOrEmpty(state))
                    return;
            }


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

            PreviousAction = mostRecentAction;
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

                if (!Client.IsInitialized)
                    Client.Initialize();

                PresenceTimer.Enabled = true;
                PresenceTimer_Tick(sender, e); // force call once to send update
            }
        }

        #region Helpers
        private void UncheckOthers(CheckBox check)
        {
            for (int i = 0; i < LanguagesPanel.Controls.Count; i++)
            {
                if (LanguagesPanel.Controls[i] == check)
                    continue;

                ((CheckBox)LanguagesPanel.Controls[i]).Checked = false;
            }

            SaveSettings(check.Name.ToLower(), true);
            PresLangQuestion.Visible = check.Name != English.Name;
            EnglishPresCB.Visible = check.Name != English.Name;
            IngamePresCB.Visible = check.Name != English.Name;

            switch (check.Name)
            {
                case "English":
                {
                    CurrentLanguage = new Language(
                        new[] { "Jumping", " to ", "" },
                        new[] { "Undocking", " to ", " solar " },
                        new[] { " dock ", " at ", " station", "accepted" });

                    break;
                }
                case "French":
                {
                    CurrentLanguage = new Language(
                        new[] { "Saute de", "à ", "*" },
                        new[] { "Part de", " solaire ", "*." },
                        new[] { "s'amarrer", "station ", "*", "acceptée" },
                        new[] { "Jouer à EVE, sous le nom: ", "Dites cette user on a oublié(e) de fermer le program RPC pour EVE Online!" },
                        new[] { "En volant dans:", "Amarré(e) à la station:" });

                    break;
                }
                case "Russian":
                {
                    CurrentLanguage = new Language(
                        new[] { "Осуществляется", " в ", "*" },
                        new[] { "Выход из", "систему ", "*" },
                        new[] { "док на", "станции ", "*", "разрешение" });

                    break;
                }
                default:
                    CurrentLanguage = null;
                    break;
            }
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

        #region WinForms Events
        #region CheckedChanged Events
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
        #endregion // CheckedChanged Events

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ReSharper disable once InvertIf
            if (Client.IsInitialized)
            {
                Client.ClearPresence();
                Client.Dispose();
            }
        }
        #endregion // WinForms Events

        private class Language
        {
            private string[] m_JumpActionStrings;
            private string[] m_UndockActionStrings;
            private string[] m_DockActionStrings;

            public string[] m_DetailsStrings = { "Playing EVE, under the name:", "Tell this user they left their RPC program for EVE Online up!" };
            public string[] m_StateStrings = { "Flying in:", "Docked at station:" };

            public Language(string[] JumpActionStrings, string[] UndockActionStrings, string[] DockActionStrings,
                string[] DetailsStrings = null, string[] StateStrings = null)
            {
                DetailsStrings ??= m_DetailsStrings;
                StateStrings ??= m_StateStrings;

                CheckSize(JumpActionStrings.Length, UndockActionStrings.Length, DockActionStrings.Length,
                    DetailsStrings.Length, StateStrings.Length);


                m_JumpActionStrings = JumpActionStrings;
                m_UndockActionStrings = UndockActionStrings;
                m_DockActionStrings = DockActionStrings;
                m_DetailsStrings = DetailsStrings;
                m_StateStrings = StateStrings;
            }

            public string[] GetStringsFromActionType(ActionTypes action)
            {
                return action switch
                {
                    ActionTypes.JUMPING => m_JumpActionStrings,
                    ActionTypes.UNDOCKING => m_UndockActionStrings,
                    ActionTypes.DOCKING => m_DockActionStrings,
                    _ => new[] { "" }
                };
            }

            private static void CheckSize(int jump, int undock, int dock, int details, int state)
            {
                string paramName;

                int size = 3;
                const int sizeDock = 4;
                const int sizePresStrings = 2;

                if (jump > size)
                {
                    paramName = nameof(m_JumpActionStrings);
                }
                else if (undock > size)
                {
                    paramName = nameof(m_UndockActionStrings);
                }
                else if (dock > sizeDock)
                {
                    paramName = nameof(m_DockActionStrings);
                    size = sizeDock;
                }
                else if (details > sizePresStrings)
                {
                    paramName = nameof(m_DetailsStrings);
                    size = sizePresStrings;
                }
                else if (state > sizePresStrings)
                {
                    paramName = nameof(m_StateStrings);
                    size = sizePresStrings;
                }
                else
                    return;

                throw new ArgumentOutOfRangeException(paramName, "The parameter for an instance of the Language class isn't the correct size." +
                                                                $"(Must be {size})");
            }
        }
    }
}