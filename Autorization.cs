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
    public partial class Autorization : Form
    {
        DB database = new DB();
        public Autorization()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Autorization_window(object sender, EventArgs e)
        {
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
        }

        private void Login (object sender, EventArgs e)
        {

        }

        private void Password (object sender, EventArgs e)
        {

        }

        private void Autorizacia (object sender, EventArgs e)
        {

        }

        private void Ввод_логина (object sender, EventArgs e)
        {

        }

        private void Ввод_пароля (object sender, EventArgs e)
        {

        }

        private void button_enter_click(object sender, EventArgs e)
        {
            var LoginUser = textBox1.Text;
            var PasswordUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user from autorization where login_user ='{LoginUser}' and password_user ='{PasswordUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вход выполнен!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Show();
}
        else 
                MessageBox.Show("Введите корректные данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
