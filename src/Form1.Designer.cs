namespace eve_discord_rpc
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.englishPresCB = new System.Windows.Forms.CheckBox();
            this.ingameCB = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.russianCB = new System.Windows.Forms.CheckBox();
            this.germanCB = new System.Windows.Forms.CheckBox();
            this.frenchCB = new System.Windows.Forms.CheckBox();
            this.japaneseCB = new System.Windows.Forms.CheckBox();
            this.englishCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(163, 45);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Use the button below to select your most recent Gamelogs file.";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(106, 216);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(95, 79);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "Would you like your presence to be in English, or your in game language?";
            this.textBox3.Visible = false;
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
            this.englishPresCB.CheckedChanged += new System.EventHandler(this.EnglishCB_CheckedChanged);
            // 
            // ingameCB
            // 
            this.ingameCB.AutoSize = true;
            this.ingameCB.Location = new System.Drawing.Point(189, 301);
            this.ingameCB.Name = "ingameCB";
            this.ingameCB.Size = new System.Drawing.Size(64, 17);
            this.ingameCB.TabIndex = 8;
            this.ingameCB.Text = "In-game";
            this.ingameCB.UseVisualStyleBackColor = true;
            this.ingameCB.Visible = false;
            this.ingameCB.CheckedChanged += new System.EventHandler(this.IngameCB_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, -1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(207, 63);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(70, 48);
            this.textBox4.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(-1, -1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 45);
            this.button3.TabIndex = 2;
            this.button3.Text = "Minimize";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "EVE Online Discord RPC";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(106, 95);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(95, 35);
            this.textBox5.TabIndex = 12;
            this.textBox5.Text = "What language is your game in?";
            // 
            // russianCB
            // 
            this.russianCB.AutoSize = true;
            this.russianCB.Location = new System.Drawing.Point(106, 158);
            this.russianCB.Name = "russianCB";
            this.russianCB.Size = new System.Drawing.Size(41, 17);
            this.russianCB.TabIndex = 13;
            this.russianCB.Text = "РУ";
            this.russianCB.UseVisualStyleBackColor = true;
            this.russianCB.CheckedChanged += new System.EventHandler(this.RussianCB_CheckedChanged);
            // 
            // germanCB
            // 
            this.germanCB.AutoSize = true;
            this.germanCB.Location = new System.Drawing.Point(160, 158);
            this.germanCB.Name = "germanCB";
            this.germanCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.germanCB.Size = new System.Drawing.Size(41, 17);
            this.germanCB.TabIndex = 14;
            this.germanCB.Text = "DE";
            this.germanCB.UseVisualStyleBackColor = true;
            this.germanCB.CheckedChanged += new System.EventHandler(this.GermanCB_CheckedChanged);
            // 
            // frenchCB
            // 
            this.frenchCB.AutoSize = true;
            this.frenchCB.Location = new System.Drawing.Point(106, 181);
            this.frenchCB.Name = "frenchCB";
            this.frenchCB.Size = new System.Drawing.Size(40, 17);
            this.frenchCB.TabIndex = 15;
            this.frenchCB.Text = "FR";
            this.frenchCB.UseVisualStyleBackColor = true;
            this.frenchCB.CheckedChanged += new System.EventHandler(this.FrenchCB_CheckedChanged);
            // 
            // japaneseCB
            // 
            this.japaneseCB.AutoSize = true;
            this.japaneseCB.Location = new System.Drawing.Point(152, 181);
            this.japaneseCB.Name = "japaneseCB";
            this.japaneseCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.japaneseCB.Size = new System.Drawing.Size(62, 17);
            this.japaneseCB.TabIndex = 16;
            this.japaneseCB.Text = "日本語";
            this.japaneseCB.UseVisualStyleBackColor = true;
            this.japaneseCB.CheckedChanged += new System.EventHandler(this.JapaneseCB_CheckedChanged);
            // 
            // englishCB
            // 
            this.englishCB.AutoSize = true;
            this.englishCB.Location = new System.Drawing.Point(132, 136);
            this.englishCB.Name = "englishCB";
            this.englishCB.Size = new System.Drawing.Size(41, 17);
            this.englishCB.TabIndex = 17;
            this.englishCB.Text = "EN";
            this.englishCB.UseVisualStyleBackColor = true;
            this.englishCB.CheckedChanged += new System.EventHandler(this.EnglishCB_CheckedChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(300, 321);
            this.ControlBox = false;
            this.Controls.Add(this.englishCB);
            this.Controls.Add(this.japaneseCB);
            this.Controls.Add(this.frenchCB);
            this.Controls.Add(this.germanCB);
            this.Controls.Add(this.russianCB);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ingameCB);
            this.Controls.Add(this.englishPresCB);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVE Online Discord RPC";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox englishPresCB;
        private System.Windows.Forms.CheckBox ingameCB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox russianCB;
        private System.Windows.Forms.CheckBox germanCB;
        private System.Windows.Forms.CheckBox frenchCB;
        private System.Windows.Forms.CheckBox japaneseCB;
        private System.Windows.Forms.CheckBox englishCB;
    }
}

