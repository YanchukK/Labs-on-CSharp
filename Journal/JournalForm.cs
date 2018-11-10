using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TeachersAndSubjects
{
    public partial class JournalForm : Form
    {
        static string connectionSrting = @"Data Source = (LocalDB)\MSSQLLocalDB;
                                    AttachDbFilename=C:\Users\Dasha\source\repos\
                                    TeachersAndSubjects\TeachersAndSubjects\Journal.mdf;
                                    Integrated Security = True";
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
                using (connection = new SqlConnection(connectionSrting))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into [dbo].[Journal] values(@Teacher, @Subject)", connection);
                    cmd.Parameters.AddWithValue("Teacher", teacher);
                    cmd.Parameters.AddWithValue("Subject", subject);
                    cmd.ExecuteNonQuery();
                    disp_data();
                }
            }
        }

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
                using (connection = new SqlConnection(connectionSrting))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("delete from [dbo].[Journal] where Teacher=@Teacher and Subject=@Subject", connection);
                    cmd.Parameters.AddWithValue("Teacher", teacher);
                    cmd.Parameters.AddWithValue("Subject", subject);
                    cmd.ExecuteNonQuery();
                    disp_data();
                }
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
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
                Update form = new Update();
                form.ShowDialog();

                string newTeacher = form.NewTeacherName.Trim();
                string newSubject = form.NewSubject.Trim();

                using (connection = new SqlConnection(connectionSrting))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Journal] " +
                        "SET Teacher=@NewTeacher, Subject=@NewSubject " +
                        "WHERE Teacher=@Teacher and Subject=@Subject", connection);
                    cmd.Parameters.AddWithValue("NewTeacher", newTeacher);
                    cmd.Parameters.AddWithValue("NewSubject", newSubject);
                    cmd.Parameters.AddWithValue("Teacher", teacher);
                    cmd.Parameters.AddWithValue("Subject", subject);
                    cmd.ExecuteNonQuery();
                    disp_data();
                }
            }
        }
    }
}
