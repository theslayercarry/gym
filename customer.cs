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
    public partial class customer : Form
    {

        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        String phone, help, time_from, time_to, title, description;
        String days = "";
        String message = "";
        DateTime date;
        public customer()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable table_memberships = new DataTable();

            MySqlConnection connection_memberships = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select id_type, types_gym_memberships.title as title from gym_memberships\r\njoin types_gym_memberships on gym_memberships.id_type = types_gym_memberships.idtypes\r\nwhere id_customer = @id_customer;", connection_memberships);
                command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_memberships);
            }
            listBox1.DataSource = table_memberships;
            listBox1.DisplayMember = "title";
            listBox1.ValueMember = "id_type";

            listBox1.ClearSelected();

        }

        private void customer_Load(object sender, EventArgs e)
        {
            label_name.Text = main.customer_name;

            MySqlConnection phone_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_phone = new MySqlCommand("select phone from customers where idcustomers = @id_customer;", phone_connection);
            command_phone.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;
            phone_connection.Open();

            phone = command_phone.ExecuteScalar().ToString();
            label_phone.Text = "+" + phone;
            phone_connection.Close();
        }

        private void customer_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.ClearSelected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            memberships frm1 = new memberships();
            this.Hide();
            frm1.ShowDialog();

            DataTable table_memberships = new DataTable();

            MySqlConnection connection_memberships = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select id_type, types_gym_memberships.title as title from gym_memberships\r\njoin types_gym_memberships on gym_memberships.id_type = types_gym_memberships.idtypes\r\nwhere id_customer = @id_customer;", connection_memberships);
                command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_memberships);
            }
            listBox1.DataSource = table_memberships;
            listBox1.DisplayMember = "title";
            listBox1.ValueMember = "id_type";

            listBox1.ClearSelected();


            this.Show();
        }

        private void listBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MySqlConnection description_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_description = new MySqlCommand("select description from types_gym_memberships where idtypes = @id_type;", description_connection);
                command_description.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                description_connection.Open();

                description = command_description.ExecuteScalar().ToString();
                description_connection.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlConnection duration_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_duration = new MySqlCommand("select date_of_purchase from gym_memberships\r\nwhere id_type = @id_type and id_customer = @id_customer;", duration_connection);
                command_duration.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;
                command_duration.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                duration_connection.Open();

                help = command_duration.ExecuteScalar().ToString();

                date = DateTime.Parse(help);
                date.GetDateTimeFormats('u');
                time_from = date.ToString("yyyy-MM-dd");

                duration_connection.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlConnection title_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_title = new MySqlCommand("select title from types_gym_memberships where idtypes = @id_type;", title_connection);
                command_title.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                title_connection.Open();

                help = command_title.ExecuteScalar().ToString();
                title = help;
                title_connection.Close();

                if (title == "Разовый визит")
                {
                    message = "Длительность: 1 день";
                }
                else if (title == "Абонемент на ПОЛГОДА")
                {
                    days = "180";
                }
                else if (title == "Абонемент на МЕСЯЦ")
                {
                    days = "30";
                }
                else if (title == "Абонемент на КВАРТАЛ")
                {
                    days = "90";
                }
                else if (title == "Абонемент на ГОД")
                {
                    days = "360";
                }
                else if (title == "Абонемент на 8 посещений")
                {
                    message = "8 любых групповых тренировок по расписанию\r\n\nОГРАНИЧЕН ПО СРОКУ ДЕЙСТВИЯ: в течение МЕСЯЦА";
                }
                else
                    message = "Длительность: ";

                if(days != "")
                {
                    date = date.AddDays(Convert.ToDouble(days));
                    date.GetDateTimeFormats('u');
                    time_to = date.ToString("yyyy-MM-dd");

                    MessageBox.Show(description + "\r\n\nСрок действия:  " + time_from + "  -  " + time_to, "Описание абонемента");
                }
                else if (message != "")
                {
                    MessageBox.Show(description + "\r\n\n" + message,"Описание абонемента");
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                message = days = "";
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
