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
    public partial class coaches : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        String help, training_date;
        public static int id_coach;
        float price, count_of_trainings;
        DateTime Date;
        public coaches()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            DataTable table_mans = new DataTable();

            MySqlConnection connection_mans = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idcoaches, concat(lastname,\" \",name) as name from coaches where id_gender = 1;", connection_mans);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_mans);
            }
            comboBox_mans.DataSource = table_mans;
            comboBox_mans.DisplayMember = "name";
            comboBox_mans.ValueMember = "idcoaches";

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            DataTable table_girls = new DataTable();

            MySqlConnection connection_girls = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idcoaches, concat(lastname,\" \",name) as name from coaches where id_gender = 2;", connection_girls);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_girls);
            }
            comboBox_girls.DataSource = table_girls;
            comboBox_girls.DisplayMember = "name";
            comboBox_girls.ValueMember = "idcoaches";

            comboBox_girls.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_mans.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void coaches_Load(object sender, EventArgs e)
        {
            radioButton_mans.Checked = true;
            panel_confirming.Visible = false;
            textBox_trainings.Text = "10";

            id_coach = (int)comboBox_mans.SelectedValue;
            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name.Text = help;
            name_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection halls_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_halls = new MySqlCommand("select halls.name from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", halls_connection);
            command_halls.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            halls_connection.Open();

            help = command_halls.ExecuteScalar().ToString();
            label_hall.Text = help;
            halls_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            education_connection.Open();

            help = command_education.ExecuteScalar().ToString();
            label_education.Text = "Профильное образование:  " + help;
            education_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            experience_connection.Open();

            help = command_experience.ExecuteScalar().ToString();
            label_experience.Text = "Тренерский стаж:  " + help;
            experience_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select price_for_10_trainings from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", cost_connection);
            command_cost.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            cost_connection.Open();

            help = command_cost.ExecuteScalar().ToString();
            label_price.Text = "Цена:  " + help + "р за блок из 10 тренировок";
            cost_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection achievements_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_achievements = new MySqlCommand("select group_concat(' ',achievements.title) from coaches\r\njoin coaches_to_achievements on coaches.idcoaches = coaches_to_achievements.id_coach\r\njoin achievements on coaches_to_achievements.id_achievement = achievements.idachievements\r\nwhere idcoaches = @id_coach;", achievements_connection);
            command_achievements.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            achievements_connection.Open();

            help = command_achievements.ExecuteScalar().ToString();
            label_achievements.Text = "Достижения:  " + help;
            achievements_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection specializations_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_specializations = new MySqlCommand("select group_concat(' ',specializations.name) from coaches\r\njoin coaches_to_specializations on coaches.idcoaches = coaches_to_specializations.id_coach\r\njoin specializations on coaches_to_specializations.id_specialization = specializations.idspecializations\r\nwhere idcoaches = @id_coach;", specializations_connection);
            command_specializations.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            specializations_connection.Open();

            help = command_specializations.ExecuteScalar().ToString();
            label_specializations.Text = "Специализация:  " + help;
            specializations_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void radioButton_mans_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(664, 63);

            id_coach = (int)comboBox_mans.SelectedValue;
            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name.Text = help;
            name_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection halls_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_halls = new MySqlCommand("select halls.name from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", halls_connection);
            command_halls.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            halls_connection.Open();

            help = command_halls.ExecuteScalar().ToString();
            label_hall.Text = help;
            halls_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            education_connection.Open();

            help = command_education.ExecuteScalar().ToString();
            label_education.Text = "Профильное образование:  " + help;
            education_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            experience_connection.Open();

            help = command_experience.ExecuteScalar().ToString();
            label_experience.Text = "Тренерский стаж:  " + help;
            experience_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select price_for_10_trainings from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", cost_connection);
            command_cost.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            cost_connection.Open();

            help = command_cost.ExecuteScalar().ToString();
            label_price.Text = "Цена:  " + help + "р за блок из 10 тренировок";
            cost_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection achievements_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_achievements = new MySqlCommand("select group_concat(' ',achievements.title) from coaches\r\njoin coaches_to_achievements on coaches.idcoaches = coaches_to_achievements.id_coach\r\njoin achievements on coaches_to_achievements.id_achievement = achievements.idachievements\r\nwhere idcoaches = @id_coach;", achievements_connection);
            command_achievements.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            achievements_connection.Open();

            help = command_achievements.ExecuteScalar().ToString();
            label_achievements.Text = "Достижения:  " + help;
            achievements_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection specializations_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_specializations = new MySqlCommand("select group_concat(' ',specializations.name) from coaches\r\njoin coaches_to_specializations on coaches.idcoaches = coaches_to_specializations.id_coach\r\njoin specializations on coaches_to_specializations.id_specialization = specializations.idspecializations\r\nwhere idcoaches = @id_coach;", specializations_connection);
            command_specializations.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            specializations_connection.Open();

            help = command_specializations.ExecuteScalar().ToString();
            label_specializations.Text = "Специализация:  " + help;
            specializations_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void radioButton_girls_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Location = new Point(12, 63);

            id_coach = (int)comboBox_girls.SelectedValue;
            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name.Text = help;
            name_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection halls_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_halls = new MySqlCommand("select halls.name from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", halls_connection);
            command_halls.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            halls_connection.Open();

            help = command_halls.ExecuteScalar().ToString();
            label_hall.Text = help;
            halls_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            education_connection.Open();

            help = command_education.ExecuteScalar().ToString();
            label_education.Text = "Профильное образование:  " + help;
            education_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            experience_connection.Open();

            help = command_experience.ExecuteScalar().ToString();
            label_experience.Text = "Тренерский стаж:  " + help;
            experience_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select price_for_10_trainings from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", cost_connection);
            command_cost.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            cost_connection.Open();

            help = command_cost.ExecuteScalar().ToString();
            label_price.Text = "Цена:  " + help + "р за блок из 10 тренировок";
            cost_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection achievements_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_achievements = new MySqlCommand("select group_concat(' ',achievements.title) from coaches\r\njoin coaches_to_achievements on coaches.idcoaches = coaches_to_achievements.id_coach\r\njoin achievements on coaches_to_achievements.id_achievement = achievements.idachievements\r\nwhere idcoaches = @id_coach;", achievements_connection);
            command_achievements.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            achievements_connection.Open();

            help = command_achievements.ExecuteScalar().ToString();
            label_achievements.Text = "Достижения:  " + help;
            achievements_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection specializations_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_specializations = new MySqlCommand("select group_concat(' ',specializations.name) from coaches\r\njoin coaches_to_specializations on coaches.idcoaches = coaches_to_specializations.id_coach\r\njoin specializations on coaches_to_specializations.id_specialization = specializations.idspecializations\r\nwhere idcoaches = @id_coach;", specializations_connection);
            command_specializations.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            specializations_connection.Open();

            help = command_specializations.ExecuteScalar().ToString();
            label_specializations.Text = "Специализация:  " + help;
            specializations_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }



        private void comboBox_mans_SelectedValueChanged(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////
        }

        private void comboBox_girls_SelectedValueChanged(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////
        }

        private void comboBox_mans_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_coach = (int)comboBox_mans.SelectedValue;
            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name.Text = help;
            name_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection halls_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_halls = new MySqlCommand("select halls.name from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", halls_connection);
            command_halls.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            halls_connection.Open();

            help = command_halls.ExecuteScalar().ToString();
            label_hall.Text = help;
            halls_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            education_connection.Open();

            help = command_education.ExecuteScalar().ToString();
            label_education.Text = "Профильное образование:  " + help;
            education_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            experience_connection.Open();

            help = command_experience.ExecuteScalar().ToString();
            label_experience.Text = "Тренерский стаж:  " + help;
            experience_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select price_for_10_trainings from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", cost_connection);
            command_cost.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            cost_connection.Open();

            help = command_cost.ExecuteScalar().ToString();
            label_price.Text = "Цена:  " + help + "р за блок из 10 тренировок";
            cost_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection achievements_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_achievements = new MySqlCommand("select group_concat(' ',achievements.title) from coaches\r\njoin coaches_to_achievements on coaches.idcoaches = coaches_to_achievements.id_coach\r\njoin achievements on coaches_to_achievements.id_achievement = achievements.idachievements\r\nwhere idcoaches = @id_coach;", achievements_connection);
            command_achievements.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            achievements_connection.Open();

            help = command_achievements.ExecuteScalar().ToString();
            label_achievements.Text = "Достижения:  " + help;
            achievements_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection specializations_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_specializations = new MySqlCommand("select group_concat(' ',specializations.name) from coaches\r\njoin coaches_to_specializations on coaches.idcoaches = coaches_to_specializations.id_coach\r\njoin specializations on coaches_to_specializations.id_specialization = specializations.idspecializations\r\nwhere idcoaches = @id_coach;", specializations_connection);
            command_specializations.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_mans.SelectedValue;
            specializations_connection.Open();

            help = command_specializations.ExecuteScalar().ToString();
            label_specializations.Text = "Специализация:  " + help;
            specializations_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void comboBox_girls_SelectionChangeCommitted(object sender, EventArgs e)
        {
            id_coach = (int)comboBox_girls.SelectedValue;
            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name.Text = help;
            name_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection halls_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_halls = new MySqlCommand("select halls.name from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", halls_connection);
            command_halls.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            halls_connection.Open();

            help = command_halls.ExecuteScalar().ToString();
            label_hall.Text = help;
            halls_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection education_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_education = new MySqlCommand("select education from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", education_connection);
            command_education.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            education_connection.Open();

            help = command_education.ExecuteScalar().ToString();
            label_education.Text = "Профильное образование:  " + help;
            education_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection experience_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_experience = new MySqlCommand("select experience from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", experience_connection);
            command_experience.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            experience_connection.Open();

            help = command_experience.ExecuteScalar().ToString();
            label_experience.Text = "Тренерский стаж:  " + help;
            experience_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection cost_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_cost = new MySqlCommand("select price_for_10_trainings from coaches\r\njoin halls on coaches.id_hall = halls.idhalls\r\nwhere idcoaches = @id_coach;", cost_connection);
            command_cost.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            cost_connection.Open();

            help = command_cost.ExecuteScalar().ToString();
            label_price.Text = "Цена:  " + help + "р за блок из 10 тренировок";
            cost_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection achievements_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_achievements = new MySqlCommand("select group_concat(' ',achievements.title) from coaches\r\njoin coaches_to_achievements on coaches.idcoaches = coaches_to_achievements.id_coach\r\njoin achievements on coaches_to_achievements.id_achievement = achievements.idachievements\r\nwhere idcoaches = @id_coach;", achievements_connection);
            command_achievements.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            achievements_connection.Open();

            help = command_achievements.ExecuteScalar().ToString();
            label_achievements.Text = "Достижения:  " + help;
            achievements_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection specializations_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_specializations = new MySqlCommand("select group_concat(' ',specializations.name) from coaches\r\njoin coaches_to_specializations on coaches.idcoaches = coaches_to_specializations.id_coach\r\njoin specializations on coaches_to_specializations.id_specialization = specializations.idspecializations\r\nwhere idcoaches = @id_coach;", specializations_connection);
            command_specializations.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = comboBox_girls.SelectedValue;
            specializations_connection.Open();

            help = command_specializations.ExecuteScalar().ToString();
            label_specializations.Text = "Специализация:  " + help;
            specializations_connection.Close();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_confirming.Location = new Point(269, 160);
            panel_confirming.Visible = true;

            MySqlConnection name_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_name = new MySqlCommand("select concat(lastname,\" \",name) as name from coaches where idcoaches = @id_coach", name_connection);
            command_name.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = id_coach;
            name_connection.Open();

            help = command_name.ExecuteScalar().ToString();
            label_name2.Text = help;
            name_connection.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel_confirming.Visible = false;
        }

        private void textBox_trainings_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (main.id_customer != -1)
            {
                if (textBox_trainings.Text == "" || textBox_trainings.Text == "0")
                {
                    MessageBox.Show("Введите количество тренировок.");
                }
                else
                {


                    MySqlConnection price_connection = new MySqlConnection(myConnectionString);
                    MySqlCommand command_price = new MySqlCommand("select price_for_10_trainings from coaches where idcoaches = @id_coach", price_connection);
                    command_price.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = id_coach;
                    price_connection.Open();

                    help = command_price.ExecuteScalar().ToString();
                    price = Int32.Parse(help);
                    price_connection.Close();

                    count_of_trainings = Int32.Parse(textBox_trainings.Text);
                    price = (count_of_trainings / 10) * price;

                    Date = dateTimePicker1.Value;
                    Date.GetDateTimeFormats('u');
                    training_date = Date.ToString("yyyy-MM-dd");

                    MySqlCommand cmd = new MySqlCommand("insert into coaches_to_customers (id_coach, id_customer, number_of_trainings, training_start_date) values (@id_coach, @id_customer, @number_of_trainings, @training_start_date);", db.getConnection());
                    cmd.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = id_coach;
                    cmd.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = main.id_customer;
                    cmd.Parameters.Add("@number_of_trainings", MySqlDbType.VarChar).Value = textBox_trainings.Text;
                    cmd.Parameters.Add("@training_start_date", MySqlDbType.VarChar).Value = training_date;


                    // --------------------------------------------------------------------------------------- //

                    db.openConnection();
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show(label_name2.Text + "\r\n\nЦена за блок из " + count_of_trainings + " тренировок " + price + "р.\r\nДата начала тренировок:  " + training_date + "\r\n\nВы успешно записались к тренеру.", "Запись к тренеру...");
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, не удалось записаться к тренеру.", "Ошибка при записи к тренеру");
                    }




                }
            }
            else
            {
                MessageBox.Show("Для записи в группу необходимо авторизоваться. Если у вас уже есть учётная запись, войдите в неё, используя заранее выбранный адрес электронной почты.");
            }
        }
    }
}
