using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace gym
{
    public partial class table_coaches_change : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, lastname, education, experience;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_price.Text == "")
            {
                MessageBox.Show("1.Введите имя и фамилию тренера.\r\n" +
                        "2.Укажите профильное образование.\r\n" +
                        "3.Укажите тренеский стаж.\r\n" +
                        "4.Укажите цену за блок из 10 тренировок.\r\n", "Несоответствие форме изменения записи");
            }
            else
            {
                price = Int32.Parse(textBox_price.Text);
                MySqlCommand cmd = new MySqlCommand("update coaches set name = @name, lastname = @lastname, id_gender = @id_gender, id_hall = @id_hall, education = @education, experience = @experience, price_for_10_trainings = @price_for_10_trainings where idcoaches = @id_coach;", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = textBox_lastname.Text;
                cmd.Parameters.Add("@id_gender", MySqlDbType.VarChar).Value = comboBox_gender.SelectedValue;
                cmd.Parameters.Add("@id_hall", MySqlDbType.VarChar).Value = comboBox_hall.SelectedValue;
                cmd.Parameters.Add("@education", MySqlDbType.VarChar).Value = textBox_education.Text;
                cmd.Parameters.Add("@experience", MySqlDbType.VarChar).Value = textBox_experience.Text;
                cmd.Parameters.Add("@price_for_10_trainings", MySqlDbType.VarChar).Value = price;
                cmd.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_lastname.Text == "" || textBox_experience.Text == "" || textBox_education.Text == "" || textBox_price.Text == "")
                {
                    MessageBox.Show("1.Введите имя и фамилию тренера.\r\n" +
                       "2.Укажите профильное образование.\r\n" +
                       "3.Укажите тренеский стаж.\r\n" +
                       "4.Укажите цену за блок из 10 тренировок.\r\n", "Несоответствие форме изменения записи");
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

        int id_gender, id_hall, price;
        public table_coaches_change()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;


            DataTable genders = new DataTable();
            MySqlConnection connection_genders = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idgenders, title from genders;", connection_genders);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(genders);
            }
            comboBox_gender.DataSource = genders;
            comboBox_gender.DisplayMember = "title";
            comboBox_gender.ValueMember = "idgenders";


            DataTable halls = new DataTable();
            MySqlConnection connection_halls = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idhalls, name from halls order by idhalls;", connection_halls);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(halls);
            }
            comboBox_hall.DataSource = halls;
            comboBox_hall.DisplayMember = "name";
            comboBox_hall.ValueMember = "idhalls";

            comboBox_hall.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_gender.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void table_coaches_change_Load(object sender, EventArgs e)
        {
            label_id.Text = table_coaches.id_coach.ToString();

            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select name from coaches where idcoaches = @id_coach;", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            name_connection.Open();

            name = command_name.ExecuteScalar().ToString();
            name_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection lastname_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_lastname = new MySqlCommand("select lastname from coaches where idcoaches = @id_coach;", lastname_connection);
            command_lastname.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            lastname_connection.Open();

            lastname = command_lastname.ExecuteScalar().ToString();
            lastname_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection gender_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_gender = new MySqlCommand("select id_gender from coaches where idcoaches = @id_coach;", gender_connection);
            command_gender.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            gender_connection.Open();

            id_gender = Int32.Parse(command_gender.ExecuteScalar().ToString());
            gender_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection hall_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_hall = new MySqlCommand("select id_hall from coaches where idcoaches = @id_coach;", hall_connection);
            command_hall.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            hall_connection.Open();

            id_hall = Int32.Parse(command_hall.ExecuteScalar().ToString());
            hall_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches where idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            education_connection.Open();

            education = command_education.ExecuteScalar().ToString();
            education_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches where idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            experience_connection.Open();

            experience = command_experience.ExecuteScalar().ToString();
            experience_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection price_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_price = new MySqlCommand("select price_for_10_trainings from coaches where idcoaches = @id_coach;", price_connection);
            command_price.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = table_coaches.id_coach;
            price_connection.Open();

            price = Int32.Parse(command_price.ExecuteScalar().ToString());
            price_connection.Close();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            textBox_name.Text = name;
            textBox_lastname.Text = lastname;
            textBox_experience.Text = experience;
            textBox_education.Text = education;
            textBox_price.Text = price.ToString();
            comboBox_gender.SelectedValue = id_gender;
            comboBox_hall.SelectedValue = id_hall;

        }
    }
}
