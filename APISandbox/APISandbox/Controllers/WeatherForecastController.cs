using Microsoft.AspNetCore.Mvc;

namespace APISandbox.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
         
        //IIS es quien asigna los puertos para correr las apps. En caso de estar ocupado el por defecto, lo reasigna. 
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray(); // LinQ -> Estilo de C# SQL - Friendly 
        }
        //Action es el nombre del método
        [Route("[action]/{date}")]
        [HttpGet]
        public WeatherForecast GetByDate(DateOnly date)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = date,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            };
        }


        [Route("[action]")]
        [HttpPost]
        public WeatherForecast Post(WeatherForecast weatherForecast)
        {
            return new WeatherForecast
            {
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };
        }

        [Route("[action]")]
        [HttpPatch]
        public WeatherForecast Patch(WeatherForecast weatherForecast)
        {
            return new WeatherForecast
            {
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };
        }

        [Route("[action]")]
        [HttpPut]
        public WeatherForecast Put(WeatherForecast weatherForecast)
        {
            return new WeatherForecast
            {
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };
        }
    }
}

