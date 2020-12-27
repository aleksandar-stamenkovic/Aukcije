using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisDataLayer.Models;

namespace AukcijeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AukcijeController : ControllerBase
    {
        [HttpGet]
        [Route("{key}")]
        // https://localhost:44371/aukcije/kljuc
        public string Get(string key)
        {
            Aukcije aukcije = new Aukcije();

            //Korisnik k =new Korisnik();

            //k.Email = "pera";

            aukcije.DodajNovogKorisnika();

            return "asdasd";// aukcije.Ucitaj(key);
        }
    }
}
