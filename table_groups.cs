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
    public partial class table_groups : Form
    {
        Database db = new Database();
        string myConnectionString = "Database=gym;Data Source=127.0.0.1;User Id=root;Password=1337";

        public static int id_group;
        String m; int selectedRow;
        public table_groups()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void table_groups_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid_groups(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("idcoaches", "id");
            dataGridView1.Columns.Add("name", "Название");
            dataGridView1.Columns.Add("number_of_people", "Количество человек");
            dataGridView1.Columns.Add("purpose_of_training", "Цель тренировки");
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 280;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 671;

            this.dataGridView1.Columns[2].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

            this.dataGridView1.Columns[3].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleLeft;

            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void ReadSingleRow_groups(DataGridView dwg, IDataRecord record)
        {
            dwg.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3));

        }

        private void RefreshDataGrid_groups(DataGridView dwg)
        {
            dwg.Rows.Clear();
            MySqlCommand command = new MySqlCommand("select idgroups, name, number_of_people, purpose_of_training from `groups`\r\norder by idgroups;", db.getConnection());
            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow_groups(dwg, reader);
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
                id_group = Convert.ToInt32(i);
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            table_groups_add frm1 = new table_groups_add();
            this.Hide();
            frm1.ShowDialog();
            RefreshDataGrid_groups(dataGridView1);
            dataGridView1.ClearSelection();
            m = null;
            this.Show();
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (m != null)
            {
                table_groups_change frm1 = new table_groups_change();
                this.Hide();
                frm1.ShowDialog();

                RefreshDataGrid_groups(dataGridView1);
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
                MySqlCommand command = new MySqlCommand("delete from `groups` where idgroups = @id_group", db.getConnection());
                command.Parameters.Add("@id_group", MySqlDbType.VarChar).Value = id_group;
                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно удалена.", "Удаление записи...");

                m = null;

                RefreshDataGrid_groups(dataGridView1);

                db.closeConnection();
                dataGridView1.ClearSelection();
            }
            else
                MessageBox.Show("Ни одна запись не выбрана.");
        }
    }
}
