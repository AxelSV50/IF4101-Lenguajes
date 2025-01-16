namespace SandboxProf.Models.Domain
{
    public class Major
    {
        private int id;
        private string name;
        private string code;

        public Major(int id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        public Major()
        {
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
    }
}
