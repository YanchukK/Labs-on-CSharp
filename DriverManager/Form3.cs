using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public int Version
        {
            get
            {
                // проверочки на ввод
                return Convert.ToInt16(textBox1.Text); 
            }
        }

        public string NameOf
        {
            get
            {
                // проверочки на ввод
                return textBox2.Text;
            }
        }

        public string Type
        {
            get
            {
                // проверочки на ввод
                return textBox3.Text;
            }
        }

        private void buttonOk_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
