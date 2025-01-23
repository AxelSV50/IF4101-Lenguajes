namespace APISandbox
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        //public virtual int TemperatureF => 32 + (int)(TemperatureC / 0.5556); //Variable efímera. No pertenece al objeto

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; } //? -> Que el tipo puede ser nullable
    }
}
