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
    public partial class gym : Form
    {
        public gym()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void gym_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (main.id_customer != -1)
            {
                memberships frm1 = new memberships();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Для записи в группу необходимо авторизоваться. Если у вас уже есть учётная запись, войдите в неё, используя заранее выбранный адрес электронной почты.");
            }
        }
    }
}
