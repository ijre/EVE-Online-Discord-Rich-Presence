namespace eve_discord_rpc
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Browse = new System.Windows.Forms.Button();
            this.GamelogsText = new System.Windows.Forms.TextBox();
            this.PresLangQuestion = new System.Windows.Forms.TextBox();
            this.EnglishPresCB = new System.Windows.Forms.CheckBox();
            this.IngamePresCB = new System.Windows.Forms.CheckBox();
            this.ParsingBox = new System.Windows.Forms.TextBox();
            this.GameLangQuestion = new System.Windows.Forms.TextBox();
            this.Russian = new System.Windows.Forms.CheckBox();
            this.German = new System.Windows.Forms.CheckBox();
            this.French = new System.Windows.Forms.CheckBox();
            this.Japanese = new System.Windows.Forms.CheckBox();
            this.English = new System.Windows.Forms.CheckBox();
            this.PresenceTimer = new System.Windows.Forms.Timer(this.components);
            this.LanguagesPanel = new System.Windows.Forms.Panel();
            this.LanguagesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(106, 63);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(95, 26);
            this.Browse.TabIndex = 3;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Visible = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // GamelogsText
            // 
            this.GamelogsText.Location = new System.Drawing.Point(79, 12);
            this.GamelogsText.Multiline = true;
            this.GamelogsText.Name = "GamelogsText";
            this.GamelogsText.ReadOnly = true;
            this.GamelogsText.Size = new System.Drawing.Size(163, 45);
            this.GamelogsText.TabIndex = 6;
            this.GamelogsText.Text = "Use the button below to select your most recent Gamelogs file.";
            // 
            // PresLangQuestion
            // 
            this.PresLangQuestion.Location = new System.Drawing.Point(106, 216);
            this.PresLangQuestion.Multiline = true;
            this.PresLangQuestion.Name = "PresLangQuestion";
            this.PresLangQuestion.ReadOnly = true;
            this.PresLangQuestion.Size = new System.Drawing.Size(95, 79);
            this.PresLangQuestion.TabIndex = 4;
            this.PresLangQuestion.Text = "Would you like your presence to be in English, or your in game language?";
            this.PresLangQuestion.Visible = false;
            // 
            // EnglishPresCB
            // 
            this.EnglishPresCB.AutoSize = true;
            this.EnglishPresCB.Location = new System.Drawing.Point(59, 301);
            this.EnglishPresCB.Name = "EnglishPresCB";
            this.EnglishPresCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EnglishPresCB.Size = new System.Drawing.Size(60, 17);
            this.EnglishPresCB.TabIndex = 1;
            this.EnglishPresCB.Text = "English";
            this.EnglishPresCB.UseVisualStyleBackColor = true;
            this.EnglishPresCB.Visible = false;
            this.EnglishPresCB.CheckedChanged += new System.EventHandler(this.EnglishPresCB_CheckedChanged);
            // 
            // IngamePresCB
            // 
            this.IngamePresCB.AutoSize = true;
            this.IngamePresCB.Location = new System.Drawing.Point(189, 301);
            this.IngamePresCB.Name = "IngamePresCB";
            this.IngamePresCB.Size = new System.Drawing.Size(64, 17);
            this.IngamePresCB.TabIndex = 2;
            this.IngamePresCB.Text = "In-game";
            this.IngamePresCB.UseVisualStyleBackColor = true;
            this.IngamePresCB.Visible = false;
            this.IngamePresCB.CheckedChanged += new System.EventHandler(this.IngameCB_CheckedChanged);
            // 
            // ParsingBox
            // 
            this.ParsingBox.Location = new System.Drawing.Point(207, 63);
            this.ParsingBox.Multiline = true;
            this.ParsingBox.Name = "ParsingBox";
            this.ParsingBox.ReadOnly = true;
            this.ParsingBox.Size = new System.Drawing.Size(70, 48);
            this.ParsingBox.TabIndex = 5;
            // 
            // GameLangQuestion
            // 
            this.GameLangQuestion.Location = new System.Drawing.Point(106, 95);
            this.GameLangQuestion.Multiline = true;
            this.GameLangQuestion.Name = "GameLangQuestion";
            this.GameLangQuestion.ReadOnly = true;
            this.GameLangQuestion.Size = new System.Drawing.Size(95, 35);
            this.GameLangQuestion.TabIndex = 7;
            this.GameLangQuestion.Text = "What language is your game in?";
            // 
            // Russian
            // 
            this.Russian.AutoSize = true;
            this.Russian.Location = new System.Drawing.Point(0, 60);
            this.Russian.Name = "Russian";
            this.Russian.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Russian.Size = new System.Drawing.Size(41, 17);
            this.Russian.TabIndex = 4;
            this.Russian.Text = "РУ";
            this.Russian.UseVisualStyleBackColor = true;
            this.Russian.CheckedChanged += new System.EventHandler(this.Russian_CheckedChanged);
            // 
            // German
            // 
            this.German.AutoSize = true;
            this.German.Location = new System.Drawing.Point(0, 37);
            this.German.Name = "German";
            this.German.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.German.Size = new System.Drawing.Size(41, 17);
            this.German.TabIndex = 2;
            this.German.Text = "DE";
            this.German.UseVisualStyleBackColor = true;
            this.German.CheckedChanged += new System.EventHandler(this.German_CheckedChanged);
            // 
            // French
            // 
            this.French.AutoSize = true;
            this.French.Location = new System.Drawing.Point(47, 37);
            this.French.Name = "French";
            this.French.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.French.Size = new System.Drawing.Size(40, 17);
            this.French.TabIndex = 1;
            this.French.Text = "FR";
            this.French.UseVisualStyleBackColor = true;
            this.French.CheckedChanged += new System.EventHandler(this.French_CheckedChanged);
            // 
            // Japanese
            // 
            this.Japanese.AutoSize = true;
            this.Japanese.Location = new System.Drawing.Point(47, 60);
            this.Japanese.Name = "Japanese";
            this.Japanese.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Japanese.Size = new System.Drawing.Size(62, 17);
            this.Japanese.TabIndex = 3;
            this.Japanese.Text = "日本語";
            this.Japanese.UseVisualStyleBackColor = true;
            this.Japanese.CheckedChanged += new System.EventHandler(this.Japanese_CheckedChanged);
            // 
            // English
            // 
            this.English.AutoSize = true;
            this.English.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.English.Location = new System.Drawing.Point(31, 0);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(26, 31);
            this.English.TabIndex = 0;
            this.English.Text = "EN";
            this.English.UseVisualStyleBackColor = true;
            this.English.CheckedChanged += new System.EventHandler(this.English_CheckedChanged);
            // 
            // PresenceTimer
            // 
            this.PresenceTimer.Interval = 15000;
            this.PresenceTimer.Tick += new System.EventHandler(this.PresenceTimer_Tick);
            // 
            // LanguagesPanel
            // 
            this.LanguagesPanel.Controls.Add(this.English);
            this.LanguagesPanel.Controls.Add(this.Japanese);
            this.LanguagesPanel.Controls.Add(this.French);
            this.LanguagesPanel.Controls.Add(this.German);
            this.LanguagesPanel.Controls.Add(this.Russian);
            this.LanguagesPanel.Location = new System.Drawing.Point(105, 136);
            this.LanguagesPanel.Name = "LanguagesPanel";
            this.LanguagesPanel.Size = new System.Drawing.Size(105, 83);
            this.LanguagesPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(300, 321);
            this.Controls.Add(this.LanguagesPanel);
            this.Controls.Add(this.GameLangQuestion);
            this.Controls.Add(this.ParsingBox);
            this.Controls.Add(this.IngamePresCB);
            this.Controls.Add(this.EnglishPresCB);
            this.Controls.Add(this.PresLangQuestion);
            this.Controls.Add(this.GamelogsText);
            this.Controls.Add(this.Browse);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVE Online Discord RPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.LanguagesPanel.ResumeLayout(false);
            this.LanguagesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox GamelogsText;
        private System.Windows.Forms.TextBox PresLangQuestion;
        private System.Windows.Forms.CheckBox EnglishPresCB;
        private System.Windows.Forms.CheckBox IngamePresCB;
        private System.Windows.Forms.TextBox ParsingBox;
        private System.Windows.Forms.TextBox GameLangQuestion;
        private System.Windows.Forms.CheckBox Russian;
        private System.Windows.Forms.CheckBox German;
        private System.Windows.Forms.CheckBox French;
        private System.Windows.Forms.CheckBox Japanese;
        private System.Windows.Forms.CheckBox English;
        private System.Windows.Forms.Timer PresenceTimer;
        private System.Windows.Forms.Panel LanguagesPanel;
    }
}

