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
            this.PresenceLanguage = new System.Windows.Forms.TextBox();
            this.englishPresCB = new System.Windows.Forms.CheckBox();
            this.ingame = new System.Windows.Forms.CheckBox();
            this.ParsingBox = new System.Windows.Forms.TextBox();
            this.LanguageText = new System.Windows.Forms.TextBox();
            this.Russian = new System.Windows.Forms.CheckBox();
            this.German = new System.Windows.Forms.CheckBox();
            this.French = new System.Windows.Forms.CheckBox();
            this.Japanese = new System.Windows.Forms.CheckBox();
            this.English = new System.Windows.Forms.CheckBox();
            this.PresenceTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(106, 63);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(95, 26);
            this.Browse.TabIndex = 0;
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
            this.GamelogsText.TabIndex = 3;
            this.GamelogsText.Text = "Use the button below to select your most recent Gamelogs file.";
            // 
            // PresenceLanguage
            // 
            this.PresenceLanguage.Location = new System.Drawing.Point(106, 216);
            this.PresenceLanguage.Multiline = true;
            this.PresenceLanguage.Name = "PresenceLanguage";
            this.PresenceLanguage.ReadOnly = true;
            this.PresenceLanguage.Size = new System.Drawing.Size(95, 79);
            this.PresenceLanguage.TabIndex = 6;
            this.PresenceLanguage.Text = "Would you like your presence to be in English, or your in game language?";
            this.PresenceLanguage.Visible = false;
            // 
            // englishPresCB
            // 
            this.englishPresCB.AutoSize = true;
            this.englishPresCB.Location = new System.Drawing.Point(59, 301);
            this.englishPresCB.Name = "englishPresCB";
            this.englishPresCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.englishPresCB.Size = new System.Drawing.Size(60, 17);
            this.englishPresCB.TabIndex = 7;
            this.englishPresCB.Text = "English";
            this.englishPresCB.UseVisualStyleBackColor = true;
            this.englishPresCB.Visible = false;
            this.englishPresCB.CheckedChanged += new System.EventHandler(this.EnglishPresCB_CheckedChanged);
            // 
            // ingame
            // 
            this.ingame.AutoSize = true;
            this.ingame.Location = new System.Drawing.Point(189, 301);
            this.ingame.Name = "ingame";
            this.ingame.Size = new System.Drawing.Size(64, 17);
            this.ingame.TabIndex = 8;
            this.ingame.Text = "In-game";
            this.ingame.UseVisualStyleBackColor = true;
            this.ingame.Visible = false;
            this.ingame.CheckedChanged += new System.EventHandler(this.IngameCB_CheckedChanged);
            // 
            // ParsingBox
            // 
            this.ParsingBox.Location = new System.Drawing.Point(207, 63);
            this.ParsingBox.Multiline = true;
            this.ParsingBox.Name = "ParsingBox";
            this.ParsingBox.ReadOnly = true;
            this.ParsingBox.Size = new System.Drawing.Size(70, 48);
            this.ParsingBox.TabIndex = 10;
            // 
            // LanguageText
            // 
            this.LanguageText.Location = new System.Drawing.Point(106, 95);
            this.LanguageText.Multiline = true;
            this.LanguageText.Name = "LanguageText";
            this.LanguageText.ReadOnly = true;
            this.LanguageText.Size = new System.Drawing.Size(95, 35);
            this.LanguageText.TabIndex = 12;
            this.LanguageText.Text = "What language is your game in?";
            // 
            // Russian
            // 
            this.Russian.AutoSize = true;
            this.Russian.Location = new System.Drawing.Point(106, 158);
            this.Russian.Name = "Russian";
            this.Russian.Size = new System.Drawing.Size(41, 17);
            this.Russian.TabIndex = 13;
            this.Russian.Text = "РУ";
            this.Russian.UseVisualStyleBackColor = true;
            this.Russian.CheckedChanged += new System.EventHandler(this.Russian_CheckedChanged);
            // 
            // German
            // 
            this.German.AutoSize = true;
            this.German.Location = new System.Drawing.Point(160, 158);
            this.German.Name = "German";
            this.German.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.German.Size = new System.Drawing.Size(41, 17);
            this.German.TabIndex = 14;
            this.German.Text = "DE";
            this.German.UseVisualStyleBackColor = true;
            this.German.CheckedChanged += new System.EventHandler(this.German_CheckedChanged);
            // 
            // French
            // 
            this.French.AutoSize = true;
            this.French.Location = new System.Drawing.Point(106, 181);
            this.French.Name = "French";
            this.French.Size = new System.Drawing.Size(40, 17);
            this.French.TabIndex = 15;
            this.French.Text = "FR";
            this.French.UseVisualStyleBackColor = true;
            this.French.CheckedChanged += new System.EventHandler(this.French_CheckedChanged);
            // 
            // Japanese
            // 
            this.Japanese.AutoSize = true;
            this.Japanese.Location = new System.Drawing.Point(152, 181);
            this.Japanese.Name = "Japanese";
            this.Japanese.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Japanese.Size = new System.Drawing.Size(62, 17);
            this.Japanese.TabIndex = 16;
            this.Japanese.Text = "日本語";
            this.Japanese.UseVisualStyleBackColor = true;
            this.Japanese.CheckedChanged += new System.EventHandler(this.Japanese_CheckedChanged);
            // 
            // English
            // 
            this.English.AutoSize = true;
            this.English.Location = new System.Drawing.Point(132, 136);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(41, 17);
            this.English.TabIndex = 17;
            this.English.Text = "EN";
            this.English.UseVisualStyleBackColor = true;
            this.English.CheckedChanged += new System.EventHandler(this.English_CheckedChanged);
            // 
            // PresenceTimer
            // 
            this.PresenceTimer.Interval = 15000;
            this.PresenceTimer.Tick += new System.EventHandler(this.PresenceTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(300, 321);
            this.Controls.Add(this.English);
            this.Controls.Add(this.Japanese);
            this.Controls.Add(this.French);
            this.Controls.Add(this.German);
            this.Controls.Add(this.Russian);
            this.Controls.Add(this.LanguageText);
            this.Controls.Add(this.ParsingBox);
            this.Controls.Add(this.ingame);
            this.Controls.Add(this.englishPresCB);
            this.Controls.Add(this.PresenceLanguage);
            this.Controls.Add(this.GamelogsText);
            this.Controls.Add(this.Browse);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVE Online Discord RPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox GamelogsText;
        private System.Windows.Forms.TextBox PresenceLanguage;
        private System.Windows.Forms.CheckBox englishPresCB;
        private System.Windows.Forms.CheckBox ingame;
        private System.Windows.Forms.TextBox ParsingBox;
        private System.Windows.Forms.TextBox LanguageText;
        private System.Windows.Forms.CheckBox Russian;
        private System.Windows.Forms.CheckBox German;
        private System.Windows.Forms.CheckBox French;
        private System.Windows.Forms.CheckBox Japanese;
        private System.Windows.Forms.CheckBox English;
        private System.Windows.Forms.Timer PresenceTimer;
    }
}

