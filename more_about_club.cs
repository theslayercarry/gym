using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gym
{
    public partial class more_about_club : Form
    {
        public more_about_club()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void more_about_club_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gym frm1 = new gym();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            combats frm1 = new combats();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groups frm1 = new groups();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trainings frm1 = new trainings();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }
    }
}
