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

        public void DodajNovogKorisnika(Korisnik k)
        {
            redis.SetEntryInHash(k.Email, "ID", k.ID);
            redis.SetEntryInHash(k.Email, "Password", k.Password);
            redis.SetEntryInHash(k.Email, "Ime", k.Ime);
            redis.SetEntryInHash(k.Email, "Prezime", k.Prezime);
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
            if(password == redis.GetValueFromHash(email,"Password"))
                return true;
            return false;
        }
    }
}
