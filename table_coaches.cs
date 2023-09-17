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
    public partial class table_coaches : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        public static int id_coach;
        String m; int selectedRow;
        public table_coaches()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_coaches_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_coaches(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idcoaches", "id");
            dataGridView1.Columns.Add("name", "Имя");
            dataGridView1.Columns.Add("gender", "Пол");
            dataGridView1.Columns.Add("hall", "Зал");
            dataGridView1.Columns.Add("education", "Профильное образование");
            dataGridView1.Columns.Add("experience", "Тренерский стаж");
            dataGridView1.Columns.Add("price", "Цена за блок из 10 тренировок");
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 235;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 220;
            dataGridView1.Columns[5].Width = 135;
            dataGridView1.Columns[6].Width = 151;

            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            this.dataGridView1.Columns[3].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1.Columns[5].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1.Columns[6].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ReadSingleRow_coaches(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6));

        }

        private void RefreshDataGrid_coaches(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("select idcoaches, concat(coaches.name,\" \",coaches.lastname), genders.title, halls.name, education, experience, price_for_10_trainings from coaches\r\njoin genders on coaches.id_gender = genders.idgenders\r\njoin halls on coaches.id_hall = halls.idhalls\r\norder by idcoaches;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_coaches(dwg, reader);
            }
            reader.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            String i;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                i = row.Cells[0].Value.ToString();
                m = i;
                id_coach = Convert.ToInt32(i);
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            table_coaches_add frm1 = new table_coaches_add();
            this.Hide();
            frm1.ShowDialog();
            RefreshDataGrid_coaches(dataGridView1);
            dataGridView1.ClearSelection();
            m = null;
            this.Show();
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                table_coaches_change frm1 = new table_coaches_change();
                this.Hide();
                frm1.ShowDialog();

                RefreshDataGrid_coaches(dataGridView1);
                dataGridView1.ClearSelection();

                this.Show();
                m = null;
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("delete from coaches where idcoaches = @id_coach", db.getConnection());
                command.Parameters.Add("@id_coach", MySqlDbType.VarChar).Value = id_coach;
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно удалена.", "Удаление записи...");

                m = null;

                RefreshDataGrid_coaches(dataGridView1);

                db.closeConnection();
                dataGridView1.ClearSelection();
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }
    }
}
