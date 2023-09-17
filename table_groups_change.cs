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
using System.Xml.Linq;

namespace gym
{
    public partial class table_groups_change : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, purpose, benefits, suitable;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_peoples.Text == "")
            {
                MessageBox.Show("1.Введите название группы.\r\n" +
                        "2.Укажите количество человек в группе.\r\n" +
                        "3.Укажите цель и пользу от тренировки.\r\n" +
                        "4.Укажите кому подходит данная тренировка.\r\n", "Несоответствие форме изменения записи");
            }
            else
            {
                peoples = Int32.Parse(textBox_peoples.Text);
                MySqlCommand cmd = new MySqlCommand("update `groups` set name = @name, number_of_people = @number_of_people, purpose_of_training = @purpose_of_training, benefits_of_training = @benefits_of_training, suitable_for = @suitable_for where idgroups = @id_group;", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@number_of_people", MySqlDbType.VarChar).Value = peoples;
                cmd.Parameters.Add("@purpose_of_training", MySqlDbType.VarChar).Value = textBox_purpose.Text;
                cmd.Parameters.Add("@benefits_of_training", MySqlDbType.VarChar).Value = textBox_benefits.Text;
                cmd.Parameters.Add("@suitable_for", MySqlDbType.VarChar).Value = textBox_suitable.Text;
                cmd.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_purpose.Text == "" || textBox_benefits.Text == "" || textBox_suitable.Text == "" || textBox_peoples.Text == "")
                {
                    MessageBox.Show("1.Введите название группы.\r\n" +
                        "2.Укажите количество человек в группе.\r\n" +
                        "3.Укажите цель и пользу от тренировки.\r\n" +
                        "4.Укажите кому подходит данная тренировка.\r\n", "Несоответствие форме изменения записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные успешно изменены.", "Изменение данных...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Ошибка при изменении данных.");
                }
            }
        }

        int peoples;
        public table_groups_change()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_groups_change_Load(object sender, EventArgs e)
        {
            label_id.Text = table_groups.id_group.ToString();

            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select name from `groups` where idgroups = @id_group;", name_connection);
            command_name.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;
            name_connection.Open();

            name = command_name.ExecuteScalar().ToString();
            name_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection peoples_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_peoples = new MySqlCommand("select number_of_people from `groups` where idgroups = @id_group;", peoples_connection);
            command_peoples.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;
            peoples_connection.Open();

            peoples = Int32.Parse(command_peoples.ExecuteScalar().ToString());
            peoples_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection purpose_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_purpose = new MySqlCommand("select purpose_of_training from `groups` where idgroups = @id_group;", purpose_connection);
            command_purpose.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;
            purpose_connection.Open();

            purpose = command_purpose.ExecuteScalar().ToString();
            purpose_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection benefits_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_benefits = new MySqlCommand("select benefits_of_training from `groups` where idgroups = @id_group;", benefits_connection);
            command_benefits.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;
            benefits_connection.Open();

            benefits = command_benefits.ExecuteScalar().ToString();
            benefits_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection suitable_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_suitable = new MySqlCommand("select suitable_for from `groups` where idgroups = @id_group;", suitable_connection);
            command_suitable.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = table_groups.id_group;
            suitable_connection.Open();

            suitable = command_suitable.ExecuteScalar().ToString();
            suitable_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            textBox_name.Text = name;
            textBox_peoples.Text = peoples.ToString();
            textBox_purpose.Text = purpose;
            textBox_benefits.Text = benefits;
            textBox_suitable.Text = suitable;
        }
    }
}
