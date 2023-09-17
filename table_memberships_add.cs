using MySql.Data.MySqlClient;
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
    public partial class table_memberships_add : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, description;
        int cost;
        public table_memberships_add()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_memberships_add_Load(object sender, EventArgs e)
        {
            textBox_cost.Text = "1580";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_cost.Text == "")
            {
                MessageBox.Show("1.Введите название и описание абонемента.\r\n" +
                        "2.Укажите стоимость абонемента.\r\n", "Несоответствие форме добавления записи");
            }
            else
            {
                cost = Int32.Parse(textBox_cost.Text);
                MySqlCommand cmd = new MySqlCommand("insert into types_gym_memberships (title, description, cost) values (@title, @description, @cost);", db.getConnection());
                cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = textBox_description.Text;
                cmd.Parameters.Add("@cost", MySqlDbType.VarChar).Value = cost;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_description.Text == "" || textBox_cost.Text == "")
                {
                    MessageBox.Show("1.Введите название и описание абонемента.\r\n" +
                            "2.Укажите стоимость абонемента.\r\n", "Несоответствие форме добавления записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("'" + textBox_name.Text + "' успешно добавлен.", "Добавление абонемента...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении абонемента.", "Ошибка при добавлении...");
                }
            }
        }
    }
}
