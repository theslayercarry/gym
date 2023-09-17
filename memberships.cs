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
    public partial class memberships : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        String help;
        public memberships()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;


            DataTable table_memberships = new DataTable();

            MySqlConnection connection_memberships = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idtypes, title from types_gym_memberships order by idtypes;", connection_memberships);
                command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_memberships);
            }
            listBox1.DataSource = table_memberships;
            listBox1.DisplayMember = "title";
            listBox1.ValueMember = "idtypes";

            listBox1.ClearSelected();
        }

        private void memberships_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MySqlConnection description_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_description = new MySqlCommand("select description from types_gym_memberships where idtypes = @id_type;", description_connection);
                command_description.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                description_connection.Open();

                help = command_description.ExecuteScalar().ToString();
                label_description.Text = help;
                description_connection.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_cost = new MySqlCommand("select cost from types_gym_memberships where idtypes = @id_type;", cost_connection);
                command_cost.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                cost_connection.Open();

                help = command_cost.ExecuteScalar().ToString();
                label_cost.Text = help + " ₽";
                cost_connection.Close();
            }
        }

        private void memberships_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.ClearSelected();
            label_cost.Text = "";
            label_description.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (main.id_customer != -1)
            {
                if (listBox1.SelectedValue != null)
                {
                    MySqlCommand cmd = new MySqlCommand("insert into gym_memberships (id_type, id_customer, date_of_purchase) values (@id_type, @id_customer, now());", db.getConnection());
                    cmd.Parameters.Add("@id_type", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                    cmd.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;



                    // --------------------------------------------------------------------------------------- //

                    db.openConnection();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Вы успешно приобрели '" + listBox1.Text + "'.", "Приобретение абонемента...");
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, не удалось приобрести абонемент.", "Ошибка при приобретении абонемента");
                    }
                }
                else
                {
                    MessageBox.Show("Ни один абонемент не выбран.");
                }

            }
            else
            {
                MessageBox.Show("Для записи в группу необходимо авторизоваться. Если у вас уже есть учётная запись, войдите в неё, используя заранее выбранный адрес электронной почты.");
            }
        }
    }
}
