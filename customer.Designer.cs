namespace gym
{
    partial class customer
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label_name = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label_phone = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Bahnschrift SemiLight Condensed", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.ForeColor = System.Drawing.Color.LightGray;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 30;
            this.listBox1.Location = new System.Drawing.Point(6, 55);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(383, 330);
            this.listBox1.TabIndex = 21;
            this.listBox1.TabStop = false;
            this.listBox1.UseTabStops = false;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            this.listBox1.MouseHover += new System.EventHandler(this.listBox1_MouseHover);
            // 
            // label_name
            // 
            this.label_name.BackColor = System.Drawing.Color.Transparent;
            this.label_name.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_name.Font = new System.Drawing.Font("Bahnschrift Light Condensed", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_name.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label_name.Location = new System.Drawing.Point(17, 20);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(689, 42);
            this.label_name.TabIndex = 20;
            this.label_name.Text = "ФИО";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = global::gym.Properties.Resources.button_color4;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(211, 573);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 46);
            this.button1.TabIndex = 24;
            this.button1.TabStop = false;
            this.button1.Text = "К АБОНЕМЕНТАМ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 20.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(405, 42);
            this.label2.TabIndex = 25;
            this.label2.Text = "Действующие абонементы:";
            // 
            // label_phone
            // 
            this.label_phone.BackColor = System.Drawing.Color.Transparent;
            this.label_phone.Cursor = System.Windows.Forms.Cursors.Default;
            this.label_phone.Font = new System.Drawing.Font("Bahnschrift Light Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_phone.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label_phone.Location = new System.Drawing.Point(20, 71);
            this.label_phone.Name = "label_phone";
            this.label_phone.Size = new System.Drawing.Size(187, 34);
            this.label_phone.TabIndex = 26;
            this.label_phone.Text = "телефон";
            this.label_phone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(40)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Location = new System.Drawing.Point(23, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(406, 429);
            this.panel1.TabIndex = 27;
            // 
            // customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::gym.Properties.Resources.girl_blur3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1302, 634);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_phone);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "customer";
            this.Text = "Личный профиль";
            this.Load += new System.EventHandler(this.customer_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.customer_MouseClick);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_phone;
        private System.Windows.Forms.Panel panel1;
    }
}