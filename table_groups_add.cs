using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gym
{
    public partial class table_groups_add : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, purpose, benefits, suitable;

        int peoples;
        public table_groups_add()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_groups_add_Load(object sender, EventArgs e)
        {
            textBox_peoples.Text = "8";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_peoples.Text == "")
            {
                MessageBox.Show("1.Введите название группы.\r\n" +
                        "2.Укажите количество человек в группе.\r\n" +
                        "3.Укажите цель и пользу от тренировки.\r\n" +
                        "4.Укажите кому подходит данная тренировка.\r\n", "Несоответствие форме добавления записи");
            }
            else
            {
                peoples = Int32.Parse(textBox_peoples.Text);
                MySqlCommand cmd = new MySqlCommand("insert into `groups` (name, number_of_people, purpose_of_training, benefits_of_training, suitable_for) values (@name, @number_of_people, @purpose_of_training, @benefits_of_training, @suitable_for);", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@number_of_people", MySqlDbType.VarChar).Value = peoples;
                cmd.Parameters.Add("@purpose_of_training", MySqlDbType.VarChar).Value = textBox_purpose.Text;
                cmd.Parameters.Add("@benefits_of_training", MySqlDbType.VarChar).Value = textBox_benefits.Text;
                cmd.Parameters.Add("@suitable_for", MySqlDbType.VarChar).Value = textBox_suitable.Text;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_purpose.Text == "" || textBox_benefits.Text == "" || textBox_suitable.Text == "" || textBox_peoples.Text == "")
                {
                    MessageBox.Show("1.Введите название группы.\r\n" +
                        "2.Укажите количество человек в группе.\r\n" +
                        "3.Укажите цель и пользу от тренировки.\r\n" +
                        "4.Укажите кому подходит данная тренировка.\r\n", "Несоответствие форме добавления записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Группа '" + textBox_name.Text + "' успешно добавлена.", "Добавление группы...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении группы.", "Ошибка при добавлении...");
                }
            }
        }
    }
}
