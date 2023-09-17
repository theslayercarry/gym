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
    public partial class trainings : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";
        String m, help;
        int id_training, selectedRow, id_gender;
        public trainings()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void trainings_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_trainings(dataGridView1);
            dataGridView1.ClearSelection();
            panel_about.Visible = false;

            pictureBox_man.Visible = false;
            pictureBox_girl.Visible = false;

        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idtrainings", "id");
            dataGridView1.Columns.Add("title", "Название тренировки");
            dataGridView1.Columns.Add("time", "Время проведения");
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("coach", "Тренер");
            dataGridView1.Columns.Add("group", "Группа");
            dataGridView1.Columns.Add("hall", "Зал");

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 290;
            dataGridView1.Columns[2].Width = 190;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 210;
            dataGridView1.Columns[5].Width = 155;
            dataGridView1.Columns[6].Width = 190;

            dataGridView1.Columns[0].Visible = false;

            this.dataGridView1.Columns[1].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            this.dataGridView1.Columns[3].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            this.dataGridView1.Columns[4].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            this.dataGridView1.Columns[5].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment =
                   DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private void ReadSingleRow_trainings(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetDateTime(3), record.GetString(4), record.GetString(5), record.GetString(6));

        }

        private void RefreshDataGrid_trainings(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("select idgroups_trainings, trainings.title as title, concat(training_time_from,\" - \",training_time_to) as time, date, concat(coaches.lastname,\" \",coaches.name) as coach, `groups`.name as `group`, halls.name as hall from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\norder by date, training_time_from;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_trainings(dwg, reader);
            }
            reader.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////////////
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            String i;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                i = row.Cells[0].Value.ToString();
                m = i;
                id_training = Convert.ToInt32(i);
            }
            panel_about.Visible = true;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection coaches_gender_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_coaches_gender = new MySqlCommand("select coaches.id_gender from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", coaches_gender_connection);
            command_coaches_gender.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            coaches_gender_connection.Open();

            help = command_coaches_gender.ExecuteScalar().ToString();
            id_gender = Convert.ToInt32(help);
            coaches_gender_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (id_gender == 1)
            {
                pictureBox_man.Visible = true;
                pictureBox_girl.Visible = false;
            }
            else if (id_gender == 2)
            {
                pictureBox_girl.Visible = true;
                pictureBox_man.Visible = false;
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection training_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_training = new MySqlCommand("select trainings.title from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", training_connection);
            command_training.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            training_connection.Open();

            help = command_training.ExecuteScalar().ToString();
            label_name.Text = help;
            training_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection time_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_time = new MySqlCommand("select training_time_from from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", time_connection);
            command_time.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            time_connection.Open();

            help = command_time.ExecuteScalar().ToString();
            label_time.Text = help;
            time_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection duration_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_duration = new MySqlCommand("select trainings.duration from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", duration_connection);
            command_duration.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            duration_connection.Open();

            help = command_duration.ExecuteScalar().ToString();
            label_duration.Text = "Продолжительность:  " + help + " мин.";
            duration_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection coach_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_coach = new MySqlCommand("select concat(coaches.lastname,\" \",coaches.name) as coach from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", coach_connection);
            command_coach.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            coach_connection.Open();

            help = command_coach.ExecuteScalar().ToString();
            label_coach.Text = "Тренер:  " + help;
            coach_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            MySqlConnection description_connection = new MySqlConnection(myConnectionString);
            MySqlCommand command_description = new MySqlCommand("select trainings.description from groups_to_trainings\r\njoin trainings on groups_to_trainings.id_training = trainings.idtrainings\r\njoin `groups` on groups_to_trainings.id_group = `groups`.idgroups\r\njoin coaches on groups_to_trainings.id_coach = coaches.idcoaches\r\njoin halls on groups_to_trainings.id_hall = halls.idhalls\r\nwhere idgroups_trainings = @id_train\r\norder by date, training_time_from;", description_connection);
            command_description.Parameters.Add("@id_train", MySqlDbType.VarChar).Value = id_training;
            description_connection.Open();

            help = command_description.ExecuteScalar().ToString();
            label_description.Text = "О занятии:  " + help;
            description_connection.Close();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel_about.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
