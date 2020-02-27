using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessage _message;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , IMessage message)
        {
            _logger = logger;
            _message = message;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new RedirectToPageResult("/");
        }
    }
}