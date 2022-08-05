using Microsoft.AspNetCore.Mvc;

namespace Company2.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        //public IActionResult Get()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "SELECT * FROM Department";
        //    cmd.Connection = conn;

        //    // open database connection.
        //    conn.Open();
        //    //Execute the query 
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    ////Retrieve data from table and Display result
        //    while (sdr.Read())
        //    {
        //        int id = (int)sdr["DepartmentId"];
        //        return id;
        //    }
        //    //Close the connection
        //    conn.Close();
        //}
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}