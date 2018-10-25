using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        List<Person> list = new List<Person>(); // Список учителей и предметов
        private BindingSource bindingSource1 = new BindingSource();

        DataSet ds = new DataSet();
        private void Add_Click(object sender, EventArgs e)
        {
            string teacher = Teacher.Text;
            string subject = textSubj.Text;

            if (string.IsNullOrEmpty(teacher) || string.IsNullOrEmpty(subject))
            {
                return;
            }
            else
            {
                if (dataGridView.ColumnCount == 0)
                {
                    return;
                }
                else
                {
                    if (list.Exists(x => x.Name == teacher) && list.Exists(x => x.Subject == subject))
                    {
                        MessageBox.Show("Такая пара значений уже есть");
                    }
                    else
                    {
                        list.Add(new Person(teacher, subject));
                        ds.Tables[0].Rows.Add(teacher, subject);
                    }
                }
            }
        }
        
        // Вводим предмет - получаем учителя (список учителей), что ведет этот предмет
        private void TeacherOn_Click(object sender, EventArgs e)
        {
            string s = textSubject.Text;
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            else
            {
                // использование технологии LINQ to data 
                var selectedUsers = (from user in list 
                                     where user.Subject == s
                                     select user.Name).ToList();

                string result = null;
                foreach (string user in selectedUsers)
                {
                    result += user + "\n";
                }

                if (result == null)
                {
                    MessageBox.Show("Учителей не найдено");
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
        }
        
        // Сохраняем исходный файл в формате XML
        private void Save_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                return;
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "XML|*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ds.Tables[0].WriteXml(sfd.FileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        // Вводим имя учителя - получаем предмет (список предметов), что он ведет
        private void SubjectOn_Click(object sender, EventArgs e)
        {
            string s = textTeacher.Text;
            if (string.IsNullOrEmpty(s))
            {
                return;
            }
            else
            {
                var selectedUsers = (from user in list
                                     where user.Name == s
                                     select user.Subject).ToList();

                string result = null;
                foreach (string user in selectedUsers)
                {
                    result += user + "\n";
                }

                if (result == null)
                {
                    MessageBox.Show("Предметов не найдено");
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
        }


        private void Remove_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(textIndex.Text) - 1;
                string teacher = dataGridView.Rows[index].Cells["Name"].Value.ToString();
                string subject = dataGridView.Rows[index].Cells["Subject"].Value.ToString();

                var indexOf = list.FindIndex(x => x.Name == teacher && x.Subject == subject);
                list.RemoveAt(indexOf);
                ds.Tables[0].Rows.RemoveAt(index);
            }
            catch
            {
                return;
            }
        }

        // начало работы
        private void Start_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlReader xmlFile = XmlReader.Create(ofd.FileName, new XmlReaderSettings());
                    ds.ReadXml(xmlFile);
                    dataGridView.DataSource = ds.Tables[0].DefaultView;
                    dataGridView.Rows.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
