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
    public partial class other_tables : Form
    {
        public other_tables()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label_matches_Click(object sender, EventArgs e)
        {
            table_memberships frm1 = new table_memberships();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void label_players_Click(object sender, EventArgs e)
        {
            table_coaches frm1 = new table_coaches();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void label_teams_Click(object sender, EventArgs e)
        {
            table_groups frm1 = new table_groups();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }
    }
}
