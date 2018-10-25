using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication_4laba
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, EventArgs e)
        { 
            string min = textMin.Text.Trim();
            string max = textMax.Text.Trim();
            string count = textCount.Text.Trim();
            int number;
            bool result1 = Int32.TryParse(count, out number);
            bool result2 = Int32.TryParse(max, out number);
            bool result3 = Int32.TryParse(min, out number);

            if (string.IsNullOrEmpty(min) || string.IsNullOrEmpty(max) || string.IsNullOrEmpty(count))
            {
                return;
            }
            else if(!result2 || !result3 || !result1)
            {
                MessageBox.Show("Неверный ввод!\nТрай эгейн");
            }
            else
            {

                int countInt; // количество

                var result = Int32.TryParse(textCount.Text, out countInt);
                int minInt = Convert.ToInt16(textMin.Text);
                int maxInt = Convert.ToInt16(textMax.Text);
                if (countInt <= 0)
                {
                    MessageBox.Show("Количество не может быть\nотрицательным числом либо 0");
                }
                else if(maxInt < minInt)
                {
                    MessageBox.Show("Минимальное число больше максимального");
                }
                else
                {
                    string pathin = @"D:\СПЗ\laba_4\in.txt";  //пишем полный путь к файлу
                    string pathout = @"D:\СПЗ\laba_4\out.txt";  //пишем полный путь к файлу

                    string firts = null;

                    int[] array = new int[countInt];
                    Random rand = new Random();
                    for (int i = 0; i < countInt; i++)
                    {
                        array[i] = rand.Next(minInt, maxInt);
                        firts += (array[i] + ", ");
                    }
                    firts = firts.Substring(0, firts.Length - 2);
                    File.WriteAllText(pathin, firts);

                    File.WriteAllText(pathout, Class1.OutString(array));

                    MessageBox.Show("Подсчет выполнен");
                }
            }
        }
    }
}
