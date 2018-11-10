using System;
using System.Windows.Forms;

namespace Journal
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        public string NewTeacherName
        {
            get { return textNewTeacherName.Text; }
        }

        public string NewSubject
        {
            get { return textNewSubject.Text; }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //если пусто, ждем пока поле заполниться
            if (string.IsNullOrEmpty(NewTeacherName) || string.IsNullOrEmpty(NewSubject)) 
            {
                return;
            }
            else if (JournalForm.DataCheck(NewTeacherName, NewSubject))
            {
                MessageBox.Show("Такая пара значений уже есть");
            }
            else
            {
                Close();
            }
        }
    }
}
