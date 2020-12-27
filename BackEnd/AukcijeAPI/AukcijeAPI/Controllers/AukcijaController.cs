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
            redis.povecajCenu(cena, id);

            return Ok();
        }
    }
}
