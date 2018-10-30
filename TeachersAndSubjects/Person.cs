namespace WindowsFormsApplication5
{
    public class Person
    {
            public string Name { get; set; }        // имя учителя
            public string Subject { get; set; }     // предмет

        public Person(string name, string subject)
        {
            Name = name;
            Subject = subject;
        }
    }
}
