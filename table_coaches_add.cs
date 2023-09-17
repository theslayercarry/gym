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
    public partial class table_coaches_add : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        String help, name, lastname, education, experience;
        int id_gender, id_hall, price;
        public table_coaches_add()
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

        private void table_coaches_add_Load(object sender, EventArgs e)
        {
            textBox_experience.Text = "с 2012г.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_price.Text == "")
            {
                MessageBox.Show("1.Введите имя и фамилию тренера.\r\n" +
                        "2.Укажите профильное образование.\r\n" +
                        "3.Укажите тренеский стаж.\r\n" +
                        "4.Укажите цену за блок из 10 тренировок.\r\n", "Несоответствие форме добавления записи");
            }
            else
            {
                price = Int32.Parse(textBox_price.Text);
                MySqlCommand cmd = new MySqlCommand("insert into coaches (name, lastname, id_gender, id_hall, education, experience, price_for_10_trainings) values (@name, @lastname, @id_gender, @id_hall, @education, @experience, @price_for_10_trainings);", db.getConnection());
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
                cmd.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = textBox_lastname.Text;
                cmd.Parameters.Add("@id_gender", MySqlDbType.VarChar).Value = comboBox_gender.SelectedValue;
                cmd.Parameters.Add("@id_hall", MySqlDbType.VarChar).Value = comboBox_hall.SelectedValue;
                cmd.Parameters.Add("@education", MySqlDbType.VarChar).Value = textBox_education.Text;
                cmd.Parameters.Add("@experience", MySqlDbType.VarChar).Value = textBox_experience.Text;
                cmd.Parameters.Add("@price_for_10_trainings", MySqlDbType.VarChar).Value = price;


                db.openConnection();
                if (textBox_name.Text == "" || textBox_lastname.Text == "" || textBox_experience.Text == "" || textBox_education.Text == "" || textBox_price.Text == "")
                {
                    MessageBox.Show("1.Введите имя и фамилию тренера.\r\n" +
                       "2.Укажите профильное образование.\r\n" +
                       "3.Укажите тренеский стаж.\r\n" +
                       "4.Укажите цену за блок из 10 тренировок.\r\n", "Несоответствие форме добавления записи");
                }
                else if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Тренер '" + textBox_lastname.Text + " " + textBox_name.Text + "' успешно добавлен.", "Добавление тренера...");

                    db.closeConnection();

                }
                else
                {
                    MessageBox.Show("Произошла ошибка при добавлении тренера.", "Ошибка при добавлении...");
                }
            }
        }
    }
}
