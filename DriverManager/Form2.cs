using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string NameOf
        {
            get
            {
                // проверочки на ввод
                return textBox1.Text;
            }
        }

        public string Type
        {
            get
            {
                return textBox2.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
