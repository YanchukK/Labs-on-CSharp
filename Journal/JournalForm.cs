using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Journal
{
    public partial class JournalForm : Form
    {
        static string connectionSrting = @"Data Source = (LocalDB)\MSSQLLocalDB;
                                    AttachDbFilename=" +
        Directory.GetCurrentDirectory() + @"\Database1.mdf;
                                    Integrated Security = True;
                                    User Instance=False";
        static SqlConnection connection;

        public JournalForm()
        {
            InitializeComponent();
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {
            disp_data();
        }

        public void disp_data()
        {
            using (connection = new SqlConnection(connectionSrting))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select* from [dbo].[Journal]", connection))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // ф-ция проверки на наличие в БД пары
        public static bool DataCheck(string teacher, string subject)
        {
            using (connection = new SqlConnection(connectionSrting))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM" +
                    "[dbo].[Journal] WHERE NOT EXISTS(SELECT * FROM [dbo].[Journal]" +
                    "WHERE Teacher = @Teacher and Subject = @Subject)", connection);
                cmd.Parameters.AddWithValue("Teacher", teacher);
                cmd.Parameters.AddWithValue("Subject", subject);
                if (cmd.ExecuteScalar().ToString() == "0") // проверяем, есть ли уже такая пара
                {
                    return true;
                }
            }
            return false;
        }
        
        // ф-ция выполения запросов
        void RequestExecution(string request, params string[] array)
        {
            using (connection = new SqlConnection(connectionSrting)) // открываем подключение
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(request, connection);
                if (array.Length == 2) // для вставки и удаления
                {
                    cmd.Parameters.AddWithValue("Teacher", array[0]);
                    cmd.Parameters.AddWithValue("Subject", array[1]);
                }
                else // для обновления
                {
                    cmd.Parameters.AddWithValue("NewTeacher", array[0]);
                    cmd.Parameters.AddWithValue("NewSubject", array[1]);
                    cmd.Parameters.AddWithValue("Teacher", array[2]);
                    cmd.Parameters.AddWithValue("Subject", array[3]);
                }
                cmd.ExecuteNonQuery();
                disp_data();
            }
        }

        // добавляем новую пару значений
        private void AddButton_Click(object sender, EventArgs e)
        {
            string teacher = textTeacherName.Text;
            string subject = textSubject.Text;

            //если пусто, ждем пока поле заполниться
            if (string.IsNullOrEmpty(teacher) || string.IsNullOrEmpty(subject))
            {
                return;
            }
            else if (DataCheck(teacher, subject))
            {
                MessageBox.Show("Такая пара значений уже есть");
            }
            else
            {
                string req = "insert into [dbo].[Journal] values(@Teacher, @Subject)";
                RequestExecution(req, teacher, subject);
            }
        }

        // удаляем пару значений
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string teacher = textTeacherName.Text;
            string subject = textSubject.Text;

            //если пусто, ждем пока поле заполниться
            if (string.IsNullOrEmpty(teacher) || string.IsNullOrEmpty(subject))
            {
                return;
            }
            else if (!DataCheck(teacher, subject))
            {
                MessageBox.Show("Такой пары значений нету");
            }
            else
            {
                string req = "delete from [dbo].[Journal] where Teacher = @Teacher and Subject = @Subject";
                RequestExecution(req, teacher, subject);
            }
        }

        // обновляем
        private void UpdateButton_Click_1(object sender, EventArgs e)
        {
            string teacher = textTeacherName.Text;
            string subject = textSubject.Text;

            //если пусто, ждем пока поле заполниться
            if (string.IsNullOrEmpty(teacher) || string.IsNullOrEmpty(subject))
            {
                return;
            }
            else if (!DataCheck(teacher, subject))
            {
                MessageBox.Show("Такой пары значений нету");
            }
            else // такая пара значений есть
            {
                UpdateForm form = new UpdateForm(); // открывается новая форма для новых значений
                form.ShowDialog();

                string newTeacher = form.NewTeacherName.Trim();
                string newSubject = form.NewSubject.Trim();

                string req = "UPDATE [dbo].[Journal] " +
                        "SET Teacher=@NewTeacher, Subject=@NewSubject " +
                        "WHERE Teacher=@Teacher and Subject=@Subject";
                RequestExecution(req, newTeacher, newSubject, teacher, subject);
            }
        }
    }
}
