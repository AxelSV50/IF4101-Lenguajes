﻿
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

        public int Insert(Student student)
        {
            int result = 0; //saves 1 or 0 depending on the insertion result

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("InsertStudent", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@Password", student.Password);

                result = command.ExecuteNonQuery();
                connection.Close();

            }

            return result;
        }

        public Student Get(string email)
        {
            Student student = new Student();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetStudentByEmail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    student.Name = reader.GetString(1);
                    student.Email = reader.GetString(2);
                }
                connection.Close();

            }

            return student;
        }
    }
}
