using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDataLayer.Models;


namespace RedisDataLayer
{
    public class Aukcije
    {
        readonly RedisClient redis = new RedisClient("localhost");
        Korisnik ksa;
        public string Ucitaj(string key)
        {
            var p = redis.GetHashValues("hash1");
            return redis.Get<string>(key);
        }

        public void DodajNovogKorisnika(/*Korisnik k*/)
        {
            //-----------------kljuc properti vrednost
            /*redis.SetEntryInHash(k.Email, "Ime", k.Ime);
            redis.SetEntryInHash(k.Email, "Prezime", k.Prezime);
            redis.SetEntryInHash(k.Email, "Ime", k.Ime);
            redis.SetEntryInHash(k.Email, "Ime", k.Ime);*/
            redis.SetEntryInHash("1", "Ime", "iiiiiimmmeee");
            redis.SetEntryInHash("1", "Prezime", "prezimeeee");
            //public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        }
    }
}
