namespace SandboxProf.Models.Domain
{
    public class Student
    {
        /*
         * Falta la convención de campos privados que inicien con _?
         */

        private string name;
        private string email;
        private string password;
        private Nationality nationality;
        private Major major;

        public Student(string name, string email, string password, Nationality nationality, Major major)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.nationality = nationality;
            this.major = major;
        }

        public Student()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public Nationality Nationality { get => nationality; set => nationality = value; }
        public Major Major { get => major; set => major = value; }
    }
}
