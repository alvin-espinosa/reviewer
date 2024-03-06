using AppSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AppSettings.Controllers
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
        private readonly IConfiguration configuration;
        private readonly IOptions<Auth0Settings> auth0Options;
        private readonly Auth0Settings auth0Settings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConfiguration configuration,
            IOptions<Auth0Settings> auth0Options,
            Auth0Settings auth0Settings)
        {
            if (auth0Settings is null)
            {
                throw new ArgumentNullException(nameof(auth0Settings));
            }

            _logger = logger;
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.auth0Options = auth0Options ?? throw new ArgumentNullException(nameof(auth0Options));
            this.auth0Settings = auth0Settings;
            /// single use
            //auth0Settings = new Auth0Settings();
            //configuration.GetSection("Auth0").Bind(auth0Settings);
            /// 
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("checkAppSettings")]
        public IActionResult actionResult()
        {
            var test = configuration.GetValue<string>("SendGridKey");
            var sendGridKey = configuration.GetSection("SendGridKey").Value;
            var Auth0Domain = configuration.GetValue<string>("Auth0:Domain");
            var Auth0DomainOtherWay = configuration.GetSection("Auth0").GetValue<string>("Domain"); // a bit complicated
            var Auth0ClientId = configuration.GetValue<string>("Auth0:ClientId"); 
            var assignment = configuration.GetValue<string>("Auth0:KeyValue:value");  
            return Ok( new {
                Auth0Domain,
                Auth0ClientId,
                Auth0DomainOtherWay,
                assignment,
                sendGridKey,
                this.auth0Settings,
                auth0Options = auth0Options.Value,
                injected = auth0Settings
            });
        }
    }
}