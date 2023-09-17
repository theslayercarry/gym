namespace gym
{
    partial class other_tables
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_teams = new System.Windows.Forms.Label();
            this.label_players = new System.Windows.Forms.Label();
            this.label_matches = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.BackgroundImage = global::gym.Properties.Resources.button_color4;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(-4, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1308, 74);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Condensed", 24.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(16, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(699, 53);
            this.label4.TabIndex = 19;
            this.label4.Text = "Реализация добавления, редактирования и удаления";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gym.Properties.Resources.button_color4;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label_teams);
            this.panel2.Controls.Add(this.label_players);
            this.panel2.Controls.Add(this.label_matches);
            this.panel2.Location = new System.Drawing.Point(-4, 255);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 381);
            this.panel2.TabIndex = 19;
            // 
            // label_teams
            // 
            this.label_teams.BackColor = System.Drawing.Color.Transparent;
            this.label_teams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_teams.Font = new System.Drawing.Font("Bahnschrift Condensed", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_teams.ForeColor = System.Drawing.Color.White;
            this.label_teams.Location = new System.Drawing.Point(13, 132);
            this.label_teams.Name = "label_teams";
            this.label_teams.Size = new System.Drawing.Size(369, 39);
            this.label_teams.TabIndex = 18;
            this.label_teams.Text = "Группы";
            this.label_teams.Click += new System.EventHandler(this.label_teams_Click);
            // 
            // label_players
            // 
            this.label_players.BackColor = System.Drawing.Color.Transparent;
            this.label_players.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_players.Font = new System.Drawing.Font("Bahnschrift Condensed", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_players.ForeColor = System.Drawing.Color.White;
            this.label_players.Location = new System.Drawing.Point(13, 79);
            this.label_players.Name = "label_players";
            this.label_players.Size = new System.Drawing.Size(369, 39);
            this.label_players.TabIndex = 17;
            this.label_players.Text = "Тренеры";
            this.label_players.Click += new System.EventHandler(this.label_players_Click);
            // 
            // label_matches
            // 
            this.label_matches.BackColor = System.Drawing.Color.Transparent;
            this.label_matches.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_matches.Font = new System.Drawing.Font("Bahnschrift Condensed", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_matches.ForeColor = System.Drawing.Color.White;
            this.label_matches.Location = new System.Drawing.Point(13, 27);
            this.label_matches.Name = "label_matches";
            this.label_matches.Size = new System.Drawing.Size(369, 40);
            this.label_matches.TabIndex = 16;
            this.label_matches.Text = "Абонементы";
            this.label_matches.Click += new System.EventHandler(this.label_matches_Click);
            // 
            // other_tables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::gym.Properties.Resources._21;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1302, 634);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "other_tables";
            this.Text = "Работа с таблицами";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_teams;
        private System.Windows.Forms.Label label_players;
        private System.Windows.Forms.Label label_matches;
    }
}