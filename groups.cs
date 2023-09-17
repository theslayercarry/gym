using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gym
{
    public partial class groups : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        String help;
        public groups()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable table_groups = new DataTable();

            MySqlConnection connection_groups = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idgroups, name from `groups` order by idgroups;", connection_groups);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_groups);
            }
            listBox1.DataSource = table_groups;
            listBox1.DisplayMember = "name";
            listBox1.ValueMember = "idgroups";

            listBox1.ClearSelected();
        }

        private void groups_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                MySqlConnection purpose_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_purpose = new MySqlCommand("select purpose_of_training from `groups` where idgroups = @id_group;", purpose_connection);
                command_purpose.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                purpose_connection.Open();

                help = command_purpose.ExecuteScalar().ToString();
                label_purpose.Text = "Цель тренировки:  " + help;
                purpose_connection.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlConnection benefits_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_benefits = new MySqlCommand("select benefits_of_training from `groups` where idgroups = @id_group;", benefits_connection);
                command_benefits.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                benefits_connection.Open();

                help = command_benefits.ExecuteScalar().ToString();
                label_benefits.Text = "Зачем это нам:  " + help;
                benefits_connection.Close();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                MySqlConnection suitable_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_suitable = new MySqlCommand("select suitable_for from `groups` where idgroups = @id_group;", suitable_connection);
                command_suitable.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
                suitable_connection.Open();

                help = command_suitable.ExecuteScalar().ToString();
                label_suitable.Text = "Кому подходят:  " + help;
                suitable_connection.Close();
            }
            else
            {

            }
        }

        private void groups_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.ClearSelected();
            label_benefits.Text = "";
            label_suitable.Text = "";
            label_purpose.Text = "";
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.ClearSelected();
            label_benefits.Text = "";
            label_suitable.Text = "";
            label_purpose.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                if (main.id_customer != -1)
                {
                    if (checkmemberships())
                    {
                        return;
                    }
                    if (checkmemberships_2())
                    {
                        return;
                    }
                    if (checkmemberships_3())
                    {
                        return;
                    }

                    MySqlCommand cmd = new MySqlCommand("insert into customers_to_groups(id_group, id_customer, date_of_joining) values (@id_group, @id_customer, now());", db.getConnection());
                        cmd.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;
                        cmd.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = listBox1.SelectedValue;


                        // --------------------------------------------------------------------------------------- //

                        db.openConnection();

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вы запиcались в группу '" + listBox1.Text + "'.", "Присоединение к группе...");

                            db.closeConnection();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка, не удалось записаться в группу.", "Ошибка при присоединении к группе");
                        }
                    
                }
                else
                {
                    MessageBox.Show("Для записи в группу необходимо авторизоваться. Если у вас уже есть учётная запись, войдите в неё, используя заранее выбранный адрес электронной почты.");
                }    

            }
            else
            {
                MessageBox.Show("Ни одна группа не выбрана.");
            }
        }

        private Boolean checkmemberships_2()
        {
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from gym_memberships \r\njoin types_gym_memberships on gym_memberships.id_type = types_gym_memberships.idtypes\r\nwhere id_customer = @id_customer and types_gym_memberships.description like \"%групп%\";", db.getConnection());
            command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                    
                MessageBox.Show("В ваш абонемент не входят занятия в группах.", "Ошибка при присоединении к группе");
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private Boolean checkmemberships()
        {
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from gym_memberships \r\njoin types_gym_memberships on gym_memberships.id_type = types_gym_memberships.idtypes\r\nwhere id_customer = @id_customer;", db.getConnection());
            command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("У вас нет ни одного абонемента.", "Ошибка при присоединении к группе");
                return true;
            }
            else
            {
                return false;
            }

        }


        private Boolean checkmemberships_3()
        {
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select * from customers_to_groups\r\nwhere id_group = @id_group and id_customer = @id_customer and date_of_leaving is null;", db.getConnection());
            command.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = listBox1.SelectedValue;
            command.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                MessageBox.Show("Вы уже состоите в данной группе.", "Ошибка при присоединении к группе");
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
