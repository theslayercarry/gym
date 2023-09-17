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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace gym
{
    public partial class main : Form
    {
        public static int id_customer;
        public static String customer_name, help;

        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        public main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            textBox_phone.Text = "+7 XXX XXX XX XX";
            textBox_phone.ForeColor = Color.LightSlateGray;

            textBox_name.Text = "Имя";
            textBox_name.ForeColor = Color.LightSlateGray;

            textBox_lastname.Text = "Фамилия";
            textBox_lastname.ForeColor = Color.LightSlateGray;

            textBox_patronymic.Text = "Отчество";
            textBox_patronymic.ForeColor = Color.LightSlateGray;

            textBox_email.Text = "@gmail.com";
            textBox_email.ForeColor = Color.LightSlateGray;

            maskedTextBox_birthday.ForeColor = Color.LightSlateGray;

            textBox_find_email.Text = "Введите адрес эл.почты";
            textBox_find_email.ForeColor = Color.LightSlateGray;

            comboBox_gender.DropDownStyle = ComboBoxStyle.DropDownList;


            DataTable table_genders = new DataTable();

            MySqlConnection connection_genders = new MySqlConnection(myConnectionString);
            {
                MySqlCommand command = new MySqlCommand("select idgenders, title from genders", connection_genders);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table_genders);
            }
            comboBox_gender.DataSource = table_genders;
            comboBox_gender.DisplayMember = "title";
            comboBox_gender.ValueMember = "idgenders";

            id_customer = -1;
        }

        private void linkLabel_about_club_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            more_about_club frm1 = new more_about_club();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_membership.Visible = true;
        }

        private void main_Load(object sender, EventArgs e)
        {
            panel_membership.Visible = false;
            panel_find_customer.Visible = false;

            textBox_phone.MaxLength = 11;
            textBox_name.MaxLength = 100;
            textBox_lastname.MaxLength = 100;
            textBox_patronymic.MaxLength = 100;
            maskedTextBox_birthday.MaxLength = 10;
            textBox_email.MaxLength = 100;

            maskedTextBox_birthday.Mask = "0000-00-00";
            maskedTextBox_birthday.ValidatingType = typeof(DateTime);

            if(id_customer == -1)
            {
                label3.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel_membership.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkuser())
            {
                return;
            }

            MySqlCommand cmd = new MySqlCommand("insert into customers (name, lastname, patronymic, date_of_birth, phone, email, id_gender) values (@name, @lastname, @patronymic, @date_of_birth, @phone, @email, @id_gender);", db.getConnection());
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox_name.Text;
            cmd.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = textBox_lastname.Text;
            cmd.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = textBox_patronymic.Text;
            cmd.Parameters.Add("@date_of_birth", MySqlDbType.VarChar).Value = maskedTextBox_birthday.Text;
            cmd.Parameters.Add("@phone", MySqlDbType.VarChar).Value = textBox_phone.Text;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox_email.Text;
            cmd.Parameters.Add("@id_gender", MySqlDbType.VarChar).Value = comboBox_gender.SelectedValue;


            // --------------------------------------------------------------------------------------- //

            db.openConnection();
            if (textBox_name.TextLength < 1 || textBox_lastname.TextLength < 1 || textBox_patronymic.TextLength < 1 || maskedTextBox_birthday.MaskCompleted == false || textBox_phone.TextLength < 11 || textBox_email.TextLength < 9 || textBox_phone.Text == "+7 XXX XXX XX XX" || textBox_email.Text == "@gmail.com" || textBox_name.Text == "Имя" || textBox_lastname.Text == "Фамилия" || textBox_patronymic.Text == "Отчество")
            {
                MessageBox.Show("1.Введите имя, фамилию и отчество.\r\n" +
                    "2.Укажите дату рождения в формате ГГГГ.ММ.ДД,\nнапример,1990-12-25\r\n" +
                    "3.Введите существующий адрес эл.почты\r\n" +
                    "4.Укажите номер телефона\r\n", "Ошибка при добавлении данных\r\n");
            }
            else if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные успешно добавлены.", "Добавление данных...");


                MySqlConnection customers_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_max = new MySqlCommand("select max(idcustomers) from customers;", customers_connection);
                customers_connection.Open();

                help = command_max.ExecuteScalar().ToString();
                id_customer = Convert.ToInt32(help);
                customers_connection.Close();

                MySqlConnection email_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_email = new MySqlCommand("select concat(lastname,\" \",name,\" \",patronymic) from customers where idcustomers = @id_customer;", email_connection);
                command_email.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = id_customer;
                email_connection.Open();

                customer_name = command_email.ExecuteScalar().ToString();
                email_connection.Close();

                label3.Text = customer_name;

                db.closeConnection();

                panel_membership.Visible = false;
                memberships frm1 = new memberships();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных.", "Ошибка при добавлении...");
            }
        }

        private Boolean checkuser()
        {
            var user_email = textBox_email.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select email from customers where email = @email;", db.getConnection());
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = user_email;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Клиент с таким email уже зарегистрирован. Пожалуйста, введите другой адрес эл.почты.", "Ошибка при регистрации");
                return true;
            }
            else
            {
                return false;
            }
        }


        private Boolean checkemail()
        {
            var user_email = textBox_find_email.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("select idcustomers from customers where email = @email;", db.getConnection());
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = user_email;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                db.openConnection();
                help = command.ExecuteScalar().ToString();
                id_customer = Convert.ToInt32(help);

                MySqlConnection email_connection = new MySqlConnection(myConnectionString);
                MySqlCommand command_email = new MySqlCommand("select concat(lastname,\" \",name,\" \",patronymic) from customers where idcustomers = @id_customer;", email_connection);
                command_email.Parameters.Add("@id_customer", MySqlDbType.VarChar).Value = id_customer;
                email_connection.Open();

                customer_name = command_email.ExecuteScalar().ToString();
                email_connection.Close();



                MessageBox.Show("Найдена учётная запись '" + customer_name + "'.\r\n\n    Нажмите ОК для дальнейшей авторизации.", "Поиск учётной записи");
                panel_membership.Visible = false;
                label3.Text = customer_name;
                db.closeConnection();
                return true;
            }
            else
            {
                MessageBox.Show("К сожалению, нам не удалось найти адрес электронной почты, который соответствует вашему запросу.\r\n", "Поиск учётной записи");
                return false;
            }
        }

        private void textBox_phone_Enter(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "+7 XXX XXX XX XX")
            {
                textBox_phone.Text = "";
                textBox_phone.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_phone_Leave(object sender, EventArgs e)
        {
            if (textBox_phone.Text == "")
            {
                textBox_phone.Text = "+7 XXX XXX XX XX";
                textBox_phone.ForeColor = Color.LightSlateGray;
            }
        }

        private void textBox_phone_MouseLeave(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////
        }

        private void textBox_email_Enter(object sender, EventArgs e)
        {
            if (textBox_email.Text == "@gmail.com")
            {
                textBox_email.Text = "";
                textBox_email.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_email_Leave(object sender, EventArgs e)
        {
            if (textBox_email.Text == "")
            {
                textBox_email.Text = "@gmail.com";
                textBox_email.ForeColor = Color.LightSlateGray;
            }
        }

        private void maskedTextBox_birthday_Enter(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                maskedTextBox_birthday.Select(0, 0);
            });
            if (maskedTextBox_birthday.Text == "    -  -")
            {
                maskedTextBox_birthday.Text = "";
                maskedTextBox_birthday.ForeColor = Color.WhiteSmoke;
            }
        }

        private void maskedTextBox_birthday_Leave(object sender, EventArgs e)
        {
            if (maskedTextBox_birthday.Text == "    -  -")
            {
                maskedTextBox_birthday.ForeColor = Color.LightSlateGray;
            }
        }

        private void textBox_patronymic_Enter(object sender, EventArgs e)
        {
            if (textBox_patronymic.Text == "Отчество")
            {
                textBox_patronymic.Text = "";
                textBox_patronymic.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_patronymic_Leave(object sender, EventArgs e)
        {
            if (textBox_patronymic.Text == "")
            {
                textBox_patronymic.Text = "Отчество";
                textBox_patronymic.ForeColor = Color.LightSlateGray;
            }
        }

        private void textBox_lastname_Enter(object sender, EventArgs e)
        {
            if (textBox_lastname.Text == "Фамилия")
            {
                textBox_lastname.Text = "";
                textBox_lastname.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_lastname_Leave(object sender, EventArgs e)
        {
            if (textBox_lastname.Text == "")
            {
                textBox_lastname.Text = "Фамилия";
                textBox_lastname.ForeColor = Color.LightSlateGray;
            }
        }

        private void textBox_name_Enter(object sender, EventArgs e)
        {
            if (textBox_name.Text == "Имя")
            {
                textBox_name.Text = "";
                textBox_name.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_name_Leave(object sender, EventArgs e)
        {
            if (textBox_name.Text == "")
            {
                textBox_name.Text = "Имя";
                textBox_name.ForeColor = Color.LightSlateGray;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            panel_find_customer.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel_find_customer.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkemail())
            {
                return;
            }
        }

        private void textBox_find_email_Enter(object sender, EventArgs e)
        {
            if (textBox_find_email.Text == "Введите адрес эл.почты")
            {
                textBox_find_email.Text = "";
                textBox_find_email.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textBox_find_email_Leave(object sender, EventArgs e)
        {
            if (textBox_find_email.Text == "")
            {
                textBox_find_email.Text = "Введите адрес эл.почты";
                textBox_find_email.ForeColor = Color.LightSlateGray;
            }
        }

        private void linkLabel_gym_memberships_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            memberships frm1 = new memberships();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void linkLabel_coaches_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            coaches frm1 = new coaches();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void linkLabel_groups_trainings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            trainings frm1 = new trainings();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            other_tables frm1 = new other_tables();
            this.Hide();
            frm1.ShowDialog();
            this.Show();
        }

        private void label_customer(object sender, EventArgs e)
        {
            if (id_customer == -1)
            { 

            }
            else
            {
                customer frm1 = new customer();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }
        }
    }
}
