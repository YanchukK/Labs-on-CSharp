using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Controller controller = new Controller();

        public Form1()
        {
            InitializeComponent();
        }

        // генерация драйверов. 6 т.к. для мин кол-во устройств для работы машины 6
        private void GenerationDrivers_Click(object sender, EventArgs e)  
        {
            string name = "DriverName";
            string type = "type";
            
            int count = listOfDrivers.Items.Count;
            if (count > 5)
            {
                Driver driver = new Driver((count + 1), name, type);
                controller.AddDriver((count + 1), driver);
                listOfDrivers.Items.Add("Version " + (count+1) + ", name " + name + ", type " + type);
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    Driver driver = new Driver(j + 1, name, type);
                    controller.AddDriver(j + 1, driver);
                    listOfDrivers.Items.Add("Version " + (j + 1) + ", name " + name + ", type " + type);
                }
            }
        }

        // при 2x нажатии на драйвер - открывается форма редактирования 
        private void listOfDrivers_MouseDoubleClick(object sender, EventArgs e)
        {
            int i = listOfDrivers.SelectedIndex;

            if (i == -1)
            {
                return;
            }
            else
            {
                string str = listOfDrivers.SelectedItem.ToString();
                Regex r = new Regex(@"-{0,1}\d+");
                MatchCollection m = r.Matches(str);

                int version = int.Parse(m[0].Value); // индекс драйвера на который нажато

                controller.RemoveDriver(version);

                Form3 f = new Form3();
                f.ShowDialog();

                int newversion = f.Version; // новая версия
                string name = f.NameOf.Trim(); // новое имя
                string type = f.Type.Trim(); // новый тип

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
                {
                    return;
                }
                else
                {
                    Driver driver = new Driver(newversion, name, type);
                    controller.AddDriver(newversion, driver);

                    listOfDrivers.Items.Add("Version " + newversion + ", name " + name + ", type " + type);
                    listOfDrivers.Items.Remove(listOfDrivers.SelectedItem);
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.ShowDialog();

            string name = fr2.NameOf.Trim();
            string type = fr2.Type.Trim();
           
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                return;
            }
            else
            {
                listOfDevices.Items.Remove(listOfDevices.SelectedItem);
                controller.RemoveDevice(0);

                controller.AddDevice(name, type);
                listOfDevices.Items.Add(name + ", " + type);
            }
        }

        // удаление драйвера (тот, который выше по списку)
        private void RemovedDrivers_Click(object sender, EventArgs e)
        {
            int i = listOfDrivers.SelectedIndex;

            if (i == -1)
            {
                return;
            }
            else
            {
                string str = listOfDrivers.SelectedItem.ToString();
                Regex r = new Regex(@"-{0,1}\d+");
                MatchCollection m = r.Matches(str);
                int a = int.Parse(m[0].Value);

                listOfDrivers.Items.Remove(listOfDrivers.SelectedItem);
                controller.RemoveDriver(a);
            }
        }
        
        private void CountOfDrivers_Click(object sender, EventArgs e)
        {
            MessageBox.Show(controller.Lengtch().ToString());
        }

        private void AddDevice_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.ShowDialog();

            string name = fr2.NameOf.Trim();
            string type = fr2.Type.Trim();
            
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                return;
            }
            else
            {
                controller.AddDevice(name, type);
                listOfDevices.Items.Add(name + ", " + type);
            }
        }

        private void CountOfDevice_Click(object sender, EventArgs e)
        {
            MessageBox.Show(controller.CountOfDevice().ToString());
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int i = listOfDevices.SelectedIndex;

            if (i == -1)
            {
                MessageBox.Show("Ай-яй-яй");
            }
            else
            {
                listOfDevices.Items.Remove(listOfDevices.SelectedItem);
                controller.RemoveDevice(0);
            }
        }

        // установить драйвер на устройство
        private void installDriverOnDevice_Click(object sender, EventArgs e)
        {
            int i = listOfDrivers.SelectedIndex;
            int j = listOfDevices.SelectedIndex;
            if (i == -1 || j == -1)
            {
                return;
            }
            else
            {
                string str = listOfDrivers.SelectedItem.ToString();
                string value = listOfDevices.SelectedItem.ToString();

                string[] separators = { ",", " " };
                string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string name = words[0];
                string typeDevice = words[1]; // тип устройства


                string[] separators1 = { ",", " " };
                string[] words1 = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string typeDriver = words1[5]; // тип драйвера


                if (typeDevice == typeDriver)
                {
                    Regex r = new Regex(@"-{0,1}\d+");
                    MatchCollection m = r.Matches(str);
                    int index2 = int.Parse(m[0].Value); // версия драйвера

                    int index1 = listOfDevices.SelectedIndex;

                    controller.AddDriverForDevice(index1, index2, name, typeDevice);

                    if (controller.ready())
                        label3.Text = "Готовность к работе: готово";
                }
                else
                {
                    MessageBox.Show("Типы железок не совпадают");
                }
            }
        }

        private void InfAboutDevice_Click(object sender, EventArgs e)
        {
            int i = listOfDevices.SelectedIndex;

            if (i == -1)
            {
                return;
            }
            else
            {
                int index = listOfDevices.SelectedIndex;
                MessageBox.Show(controller.ObjectOfDeviceToString(index));
            }
        }

        private void removeDriverFromDevice_Click(object sender, EventArgs e)
        {
            int i = listOfDrivers.SelectedIndex;
            int j = listOfDevices.SelectedIndex;
            if (i == -1 || j == -1)
            {
                return;
            }
            else
            {
                int index1 = listOfDevices.SelectedIndex;

                string value = listOfDrivers.SelectedItem.ToString();

                string[] separators = { ",", " " };
                string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string name = words[0];
                string type = words[1];

                controller.RemoveDriverFromDevice(index1, name, type);
            }
        }
    }
}
