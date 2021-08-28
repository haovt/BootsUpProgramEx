namespace CSharpBasicAndAdvanced
{
    public class Student
    {
        public Student()
        {
        }

        public Student(int rollNr, string name)
        {
            RollNr = rollNr;
            Name = name;
        }

        public int RollNr { get; set; }
        public string Name { get; set; }

        public string DisplayData()
        {
            return $"{Name}: {RollNr}";
        }
    }
}
