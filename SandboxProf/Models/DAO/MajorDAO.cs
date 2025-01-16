using Microsoft.Data.SqlClient;
using SandboxProf.Models.Domain;

namespace SandboxProf.Models.DAO
{
    public class MajorDAO
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public MajorDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public List<Major> Get()
        {
            var majors = new List<Major>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetMajors", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        majors.Add(new Major
                        {
                            Id = (int)sqlDataReader["Id"],
                            Name = sqlDataReader["Name"].ToString(),
                            Code = sqlDataReader["Code"].ToString()
                        });
                    }

                    connection.Close();
                    return majors;

                }
            }
            catch (SqlException)
            {
                throw;
            }
            
        }
    }
}
