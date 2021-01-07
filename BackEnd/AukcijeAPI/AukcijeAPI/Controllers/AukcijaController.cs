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
    public class AukcijaController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Aukcija> GetAukcija(string id)
        {
            AukcijaRedis redis = new AukcijaRedis();
            var aukcija = redis.ProcitajAukciju(id);

            return aukcija;
        }

        [HttpPost/*("{email}")*/]
        public ActionResult<Aukcija> PostAukcija(Aukcija aukcija)
        {
            AukcijaRedis redis = new AukcijaRedis();
            redis.DodajNovuAukciju(aukcija);

            return Ok();
        }

        [HttpGet("sveaukcije")]
        public ActionResult<List<Aukcija>> GetSveAukcije()
        {
            AukcijaRedis redis = new AukcijaRedis();
            List<Aukcija> listaAukcija = redis.VratiSveAukcije();

            return listaAukcija;
        }

        [HttpPut("{id}/{cena}")]
        public ActionResult PutPovecajCenu(string id, int cena)
        {
            AukcijaRedis redis = new AukcijaRedis();
            redis.PovecajCenu(cena, id);

            return Ok();
        }

        [HttpGet("{id}/{email}/{ime}/{prezime}/{cena}")]
        public ActionResult DodajBidera(string id, string email, string ime, string prezime, float cena)
        {                             //id aukcije, email korinika, ime korisnika, prezime korisnika 
            AukcijaRedis redis = new AukcijaRedis();
            redis.DodajBidera(id, email, ime, prezime, cena);

            return Ok();
        }

        [HttpDelete("obrisiAukciju/{id}")]
        public ActionResult ObrisiAukciju(string id)
        {
            AukcijaRedis redis = new AukcijaRedis();
            redis.ObrisiAukciju(id);

            return Ok();
        }

    }
}


/*
        //OBRISATI
        //test funkcija ne radi
        [HttpGet("testRead")]
        public string test()
        {
            AukcijaRedis redis = new AukcijaRedis();
            string tmp = redis.test();

            return tmp;
        }

        //OBRISATI
        //test funkcija ne radi
        [HttpGet("testSub")]
        public string test2(string name)
        {
            AukcijaRedis redis = new AukcijaRedis();
            redis.test2();
            return null;
        }
*/