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
    public partial class Форма_заполнения : Form
    {
        DB database = new DB();
        public Форма_заполнения()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Форма_заполнения_Load(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var Наименование = textBox1.Text;
            var Описание = textBox2.Text;
            var Дата_производства = textBox4.Text;
            int Масса;

            if (int.TryParse(textBox3.Text, out Масса)) 
            {
                var fzQuery = $"insert into Сыры (Наименование, Описание, Масса, Дата_производства) values ('{Наименование}', '{Описание}', '{Масса}', '{Дата_производства}')";

                var command = new SqlCommand(fzQuery, database.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись проведена!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Перепроверьте вводимые данные!", "Ошибка чтения записи!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         database.closeConnection();

        }

        private void textBox1_name(object sender, EventArgs e)
        {

        }

        private void textBox2_description(object sender, EventArgs e)
        {

        }

        private void textBox3_weight(object sender, EventArgs e)
        {

        }

        private void textBox4_date(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
