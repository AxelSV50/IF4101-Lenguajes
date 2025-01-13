using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        /* 
         * CONVENCIONES
         * M�ltiples sentencias return
         * Mensajes de error poco espec�ficos y descriptivos
         * Falta nombrar los m�todos con el par verbo-objeto para que sean significativos
         */
        public IActionResult Insert([FromBody] Student student)
        {
            studentDAO = new StudentDAO(_configuration);
            if(studentDAO.Get(student.Email).Email == null)
            {
                int result = studentDAO.Insert(student);
                return Ok(result);
            }
            else
            {
                return Error();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
