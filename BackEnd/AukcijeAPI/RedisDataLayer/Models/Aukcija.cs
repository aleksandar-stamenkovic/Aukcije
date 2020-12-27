using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RedisDataLayer.Models
{
    public class Aukcija
    {
        public DateTime Vreme { get; set; }
        public string ID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public float Cena { get; set; }
        public int Trajanje { get; set; }
        public List<Korisnik> Bideri { get; set; }
    }
}
