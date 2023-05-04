using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Сыроварня
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form1 : Form
    {
        DB database = new DB();

        int selectedRow;
        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Наименование", "Наименование");
            dataGridView1.Columns.Add("Описание", "Описание");
            dataGridView1.Columns.Add("Масса", "Масса");
            dataGridView1.Columns.Add("Дата_производства", "Дата_производства");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView gridView, IDataRecord record)
        {
            gridView.Rows.Add(record.GetInt32(0),
                record.GetString(1),
                record.GetString(2),
                record.GetInt32(3),
                record.GetString(4));
        }

        private void change()
        {
            var selectedRow = dataGridView1.CurrentCell.RowIndex;

            var ID = textBox1.Text;
            var Наименование = textBox2;
            var Описание = textBox3;
            var Дата_производства = textBox5;
            int Масса;

            if (dataGridView1.Rows[selectedRow].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox4.Text, out Масса))
                {
                    dataGridView1.Rows[selectedRow].SetValues(ID, Наименование, Описание, Дата_производства);
                    dataGridView1.Rows[selectedRow].Cells[5].Value = RowState.Modified;
                }
                   else
                {
                    MessageBox.Show("Количество должно иметь числовой формат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                 
            }
        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";


        }

        private void RefreschDataGrid(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string queryString = $"select * from Сыры";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();


        }
        private void Sklad_sirovarni(object sender, EventArgs e)
        {
            CreateColumns();
            RefreschDataGrid(dataGridView1);


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_description(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            Форма_заполнения форма_Заполнения = new Форма_заполнения();
            форма_Заполнения.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
            }

        }

        private void Search(DataGridView gridView)
        {
            gridView.Rows.Clear();

            string searchString = $"select * from Сыры where concat (ID, Наименование, Описание, Масса, Дата_производства) like '%" + textBox6.Text + "%' ";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(gridView, reader);
            }
            reader.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;

           
        }


        private void update()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView1.RowCount; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var ID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Сыры where ID = {ID}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var ID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var Наименование = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var Описание = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var Масса = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var Дата_производства = dataGridView1.Rows[index].Cells[4].Value.ToString();

                    var changeQuery = $"update Сыры set Наименование = '{Наименование}', Описание = '{Описание}', Масса ='{Масса}', Дата_производства = '{Дата_производства}' where ID = '{ID}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            database.closeConnection();
        }
    
        
        private void textBox_ID(object sender, EventArgs e)
        {

        }

        private void textBox_name(object sender, EventArgs e)
        {

        }

        private void textBox_weight(object sender, EventArgs e)
        {

        }

        private void textBox_date(object sender, EventArgs e)
        {

        }

        private void rfhtable_Click(object sender, EventArgs e)
        {
            RefreschDataGrid(dataGridView1);
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void удалить_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void search_Click(object sender, EventArgs e)
        {

        }

        private void stroka_poiska(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            update();
        }
    }
}
