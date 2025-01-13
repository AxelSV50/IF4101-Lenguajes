namespace SandboxProf.Models.Domain
{
    public class Student
    {
        /*
         * Falta la convención de campos privados que inicen con _?
         */
        private string name;
        private string email;
        private string password;
        private Nationality nationality;

        public Student(string name, string email, string password, Nationality nationality)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.nationality = nationality;
        }

        public Student()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public Nationality Nationality { get => nationality; set => nationality = value; }
    }
}
