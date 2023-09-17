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
using System.Xml.Linq;

namespace gym
{
    public partial class table_memberships_change : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, description;
        int cost;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_cost.Text == "")
            {
                MessageBox.Show("1.Введите название и описание абонемента.\r\n" +
                        "2.Укажите стоимость абонемента.\r\n", "Несоответствие форме изменения записи");
            }
            else
            {
                cost = Int32.Parse(textBox_cost.Text);
                MySqlCommand cmd = new MySqlCommand("update types_gym_memberships set title = @title, description = @description, cost = @cost where idtypes = @id_membership;", db.getConnection());
                cmd.Parameters.Add("@title", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = textBox_description.Text;
                cmd.Parameters.Add("@cost", MySqlDbType.VarChar).Value = cost;
                cmd.Parameters.Add("@id_membership", MySqlDbType.VarChar).Value = table_memberships.id_membership;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_description.Text == "" || textBox_cost.Text == "")
                {
                    MessageBox.Show("1.Введите название и описание абонемента.\r\n" +
                            "2.Укажите стоимость абонемента.\r\n", "Несоответствие форме изменения записи");
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

        public table_memberships_change()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_memberships_change_Load(object sender, EventArgs e)
        {
            label_id.Text = table_memberships.id_membership.ToString();

            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select title from types_gym_memberships where idtypes = @id_membership;", name_connection);
            command_name.Parameters.Add("@id_membership", MySqlDbType.VarChar).Value = table_memberships.id_membership;
            name_connection.Open();

            name = command_name.ExecuteScalar().ToString();
            name_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection description_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_description = new MySqlCommand("select description from types_gym_memberships where idtypes = @id_membership;", description_connection);
            command_description.Parameters.Add("@id_membership", MySqlDbType.VarChar).Value = table_memberships.id_membership;
            description_connection.Open();

            description = command_description.ExecuteScalar().ToString();
            description_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select cost from types_gym_memberships where idtypes = @id_membership;", cost_connection);
            command_cost.Parameters.Add("@id_membership", MySqlDbType.VarChar).Value = table_memberships.id_membership;
            cost_connection.Open();

            cost = Int32.Parse(command_cost.ExecuteScalar().ToString());
            cost_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            textBox_name.Text = name;
            textBox_description.Text = description;
            textBox_cost.Text = cost.ToString();
        }
    }
}
