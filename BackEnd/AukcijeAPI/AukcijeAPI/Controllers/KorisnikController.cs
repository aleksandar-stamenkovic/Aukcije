using Microsoft.AspNetCore.Mvc;
using RedisDataLayer;
using RedisDataLayer.Models;
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
        [HttpGet("{email}")]
        public ActionResult<Korisnik> GetKorisnik(string email)
        {
            KorisnikRedis redis = new KorisnikRedis();
            var korisnik = redis.ProcitajKorisnika(email);

            return korisnik;
        }

        [HttpPost/*("{email}")*/]
        public ActionResult<Korisnik> PostKorisnik(Korisnik korisnik)
        {
            KorisnikRedis redis = new KorisnikRedis();
            redis.DodajNovogKorisnika(korisnik);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<bool> Login([FromBody] Korisnik korisnik)
        {
            KorisnikRedis redis = new KorisnikRedis();
            bool uspesno = redis.ProveriLogIn(korisnik.Email, korisnik.Password);

            return uspesno;
        }

        [HttpGet]
        [Route("korisnik-aukcije")]
        public ActionResult<List<Aukcija>> GetAukcije(string email)
        {
            KorisnikRedis redis = new KorisnikRedis();
            List<Aukcija> sveAukcije = redis.ProcitajSveAukcijeKorisnika(email);
            return sveAukcije;
        }
    }
}
