using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDataLayer.Models
{
    public class Korisnik
    {
        public string Email { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public List<Aukcija> MojeAukcije { get; set; }
    }
}
