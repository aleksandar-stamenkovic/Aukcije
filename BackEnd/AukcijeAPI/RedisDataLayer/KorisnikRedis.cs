using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDataLayer.Models;

namespace RedisDataLayer
{
    public class KorisnikRedis
    {
        readonly RedisClient redis = new RedisClient("localhost");
        AukcijaRedis _ar;

        //objekat _ar jer objekat tipa koji se koristi kako bi
        //korisnik mogao da pristupa funkcijama klase AukcijaRedis

        public void DodajNovogKorisnika(Korisnik k)
        {
            redis.SetEntryInHash(k.Email, "ID", k.ID);
            redis.SetEntryInHash(k.Email, "Password", k.Password);
            redis.SetEntryInHash(k.Email, "Ime", k.Ime);
            redis.SetEntryInHash(k.Email, "Prezime", k.Prezime);            
            redis.SetEntryInHash(k.Email, "MojeAukcije", k.Email+ ":MOJEAUKCIJE");//email:MOJEAUKCIJE je kljuc za pribavljanje liste aukcija
            //public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        }

        public Korisnik ProcitajKorisnika(string email)
        {

            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(email);            

            Korisnik tmp = new Korisnik();
            string s;
            dc.TryGetValue("ID", out s);
            tmp.ID = s;
            dc.TryGetValue("Password", out s);
            tmp.Password = s;
            dc.TryGetValue("Ime", out s);
            tmp.Ime = s;
            dc.TryGetValue("Prezime", out s);
            tmp.Prezime = s;

            return tmp;
        }

        public bool ProveriLogIn(string email, string password)
        {
            if(password == redis.GetValueFromHash(email,"password"))
                return true;
            return false;
        }

        public void DodajAukcijuKorisniku(string id, string email)
        {
            redis.PushItemToList(email + ":MOJEAUKCIJE", id);
        }

        public List<Aukcija> ProcitajSveAukcijeKorisnika(string email)
        {
            List<string> list = redis.GetRangeFromList(email + ":MOJEAUKCIJE", 0, -1);
            List<Aukcija> listaAukcija = new List<Aukcija>();
            foreach(string el in list)
            {
                listaAukcija.Add(_ar.ProcitajAukciju(el));
            }
            return listaAukcija;
        }

        public bool ObrisiAukcijuKorisnika(string id, string email)
        {
            byte[] idbytes = Encoding.ASCII.GetBytes(id);
            if (redis.LRem(email + ":MOJEAUKCIJE", 0, idbytes) != 0)
                return true;
            return false;
        }

    }
}
