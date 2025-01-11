namespace SandboxProf.Models.Domain
{
    public class Student
    {
        private string name;
        private string email;
        private string password;

        public Student(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public Student()
        {

        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
    }
}
