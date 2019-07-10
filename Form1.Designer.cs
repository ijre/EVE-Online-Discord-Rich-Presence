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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.englishCB = new System.Windows.Forms.CheckBox();
            this.ingameCB = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
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
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(163, 45);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Use the button below to select your most recent Gamelogs file.";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Window;
            this.checkBox1.Location = new System.Drawing.Point(184, 101);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(106, 98);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(95, 59);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Is your game in any language that isn\'t English?";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(106, 163);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(95, 79);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "Would you like your presence to be in English, or your in game language?";
            // 
            // englishCB
            // 
            this.englishCB.AutoSize = true;
            this.englishCB.Location = new System.Drawing.Point(79, 248);
            this.englishCB.Name = "englishCB";
            this.englishCB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.englishCB.Size = new System.Drawing.Size(60, 17);
            this.englishCB.TabIndex = 7;
            this.englishCB.Text = "English";
            this.englishCB.UseVisualStyleBackColor = true;
            this.englishCB.CheckedChanged += new System.EventHandler(this.EnglishCB_CheckedChanged);
            // 
            // ingameCB
            // 
            this.ingameCB.AutoSize = true;
            this.ingameCB.Location = new System.Drawing.Point(178, 248);
            this.ingameCB.Name = "ingameCB";
            this.ingameCB.Size = new System.Drawing.Size(64, 17);
            this.ingameCB.TabIndex = 8;
            this.ingameCB.Text = "In-game";
            this.ingameCB.UseVisualStyleBackColor = true;
            this.ingameCB.CheckedChanged += new System.EventHandler(this.IngameCB_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, -1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 45);
            this.button2.TabIndex = 9;
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
            this.textBox4.Size = new System.Drawing.Size(70, 75);
            this.textBox4.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(-1, -1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 45);
            this.button3.TabIndex = 11;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(300, 285);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ingameCB);
            this.Controls.Add(this.englishCB);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(316, 324);
            this.MinimumSize = new System.Drawing.Size(316, 324);
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox englishCB;
        private System.Windows.Forms.CheckBox ingameCB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

