using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AukcijeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            Aukcija aukcija = new Aukcija();
            aukcija.Upisi("key123", "TEST");
        }
    }
}
