using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SandboxProf.Models;
using SandboxProf.Models.DAO;
using SandboxProf.Models.Domain;

namespace SandboxProf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        StudentDAO studentDAO;
        NationalityDAO nationalityDAO;
        MajorDAO majorsDAO;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            //TODO instanciar un StudentDAO
        }

        public IActionResult Index()
        {
            return View();
        }
        /* 
         * CONVENCIONES
         * Múltiples sentencias return
         * Mensajes de error poco específicos y descriptivos
         * Falta nombrar los métodos con el par verbo-objeto para que sean significativos
         */
        public IActionResult Insert([FromBody] Student student)
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);
                if (studentDAO.Get(student.Email).Email == null)
                {
                    int result = studentDAO.Insert(student);
                    return Ok(result);
                }
                else
                {
                    return Error(null);
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetAllStudents()
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);
                return Ok(studentDAO.Get());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
        public IActionResult GetStudentByEmail(string email)
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);
                return Ok(studentDAO.Get(email));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult DeleteStudent(string email)
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);

                return Ok(studentDAO.Delete(email));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
        public IActionResult GetNationalities()
        {
            try
            {
                nationalityDAO = new NationalityDAO(_configuration);

                return Json(nationalityDAO.Get());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

        }

        public IActionResult GetMajors()
        {
            try
            {
                majorsDAO = new MajorDAO(_configuration);
                return Json(majorsDAO.Get());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

        }

        public IActionResult Error(Exception exception)
        {
            if (exception is SqlException e)
            {
                return View(e.Message);
            }

            return StatusCode(500, new
            {
                message = "An unknown error occurred.",
                details = exception.Message,
                requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }


    }

}
