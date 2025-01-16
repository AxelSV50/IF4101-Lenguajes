using Microsoft.Data.SqlClient;
using SandboxProf.Models.Domain;

namespace SandboxProf.Models.DAO
{
    public class StudentDAO
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        /* CONVENCIONES
         * Métodos Insert & Get tienen más de 7 líneas
         * Get debería ser -> GetStudentByEmail ó al menos GetByEmail
           e Insert -> InsertStudent,  para que los nombres sean significativos y usar el par verbo-objeto. 
         * 
         */
        public int Insert(Student student)
        {
            int result = 0; //saves 1 or 0 depending on the insertion result

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Aunque haya un Try la excepción se lanza si en el catch hay un throw
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertStudent", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@name", student.Name);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("password", student.Password);
                    command.Parameters.AddWithValue("@nationality_id", student.Nationality.Id);
                    command.Parameters.AddWithValue("@major_id", student.Major.Id);

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
                
            }

            return result;
        }

        public Student Get(string email)
        {
            Student student = new Student();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetStudentByEmail", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) //Asks if a user has been found with the given email
                    {
                        student.Name = reader.GetString(1);
                        student.Email = reader.GetString(2);
                        student.Nationality = new Nationality(reader.GetInt32(3), null, null);
                    }
                    connection.Close();
                }
                return student;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Student> Get()
        {
            var students = new List<Student>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetAllStudents", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        students.Add(new Student
                        {
                            Name = sqlDataReader["Name"].ToString(),
                            Email = sqlDataReader["Email"].ToString(),
                            Nationality = new Nationality(0, sqlDataReader["NationalityName"].ToString(), null),
                            Major = new Major(0, sqlDataReader["MajorName"].ToString(), null)
                        });
                    }

                    connection.Close();
                }
                return students;
            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        public int Delete(string email)
        {
            int result = 0; //saves 1 or 0 depending on the insertion result

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteStudent", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);

                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException)
                {
                    throw;
                }

            }

            return result;

        }

    }


}
