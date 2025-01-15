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
                    return Error();
                }
            }
            catch (SqlException e)
            {
                //TODO enviar el mensaje más específico al frontend, 
                //Se debe incrustar en el cshtml para mostrarlo
                ViewBag.Message = e.Message;
                return View(e.ToString());

                //return Error(e);
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
            catch (SqlException e)
            {
                //TODO enviar el mensaje más específico al frontend, 
                //Se debe incrustar en el cshtml para mostrarlo
                ViewBag.Message = e.Message;
                return View(e.ToString());

                //return Error(e);
            }

            return View();
        }
        public IActionResult GetStudentByEmail(string email)
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);
                return Ok(studentDAO.Get(email));
            }
            catch (SqlException e)
            {
                //TODO enviar el mensaje más específico al frontend, 
                //Se debe incrustar en el cshtml para mostrarlo
                ViewBag.Message = e.Message;
                return View(e.ToString());

                //return Error(e);
            }

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetNationalities()
        {
            nationalityDAO = new NationalityDAO(_configuration);
            
            return Json(nationalityDAO.Get());
        }

        public IActionResult DeleteStudent(string email)
        {
            try
            {
                studentDAO = new StudentDAO(_configuration);

                return Ok(studentDAO.Delete(email));
            }
            catch (SqlException)
            {
                return Error();
            }
        }

    }

}
