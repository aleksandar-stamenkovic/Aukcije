using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisDataLayer.Models;
using RedisDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AukcijeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
        [HttpGet]
        [Route("{email}")]
        public async Task<ActionResult<Korisnik>> Get(string email)
        {
            KorisnikRedis redis = new KorisnikRedis();

            return null;
        }
    }
}
