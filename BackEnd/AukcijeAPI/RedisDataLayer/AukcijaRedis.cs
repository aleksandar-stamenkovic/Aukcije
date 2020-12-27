using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDataLayer.Models;

namespace RedisDataLayer
{
    public class AukcijaRedis
    {
        readonly RedisClient redis = new RedisClient("localhost");

        //mainHashKey je vreme osnosno glavni kljuc hash tabele koja cuva
        //objekat jedne aukcije

        private void _Dodaj_UAll_Listi(string id,string mainHashkey)
        {   
            redis.SetEntryInHash("AUKCIJE", id, mainHashkey);
        }
        private string _Procitaj_IzAll_Liste(string id)
        {
            string mainHashKey = redis.GetValueFromHash("AUKCIJE", id);
            return mainHashKey;
        }

        public void DodajNovogKorisnika(Aukcija a)
        {
            _Dodaj_UAll_Listi(a.ID, a.Vreme.ToString("dd MM yyyy hh:mm:ss"));

            string vremeTmp =  a.Vreme.ToString("dd MM yyyy hh:mm:ss");
            redis.SetEntryInHash(vremeTmp, "ID", a.ID);
            redis.SetEntryInHash(vremeTmp, "Naziv", a.Naziv);
            redis.SetEntryInHash(vremeTmp, "Opis", a.Opis);
            redis.SetEntryInHash(vremeTmp, "Cena", a.Cena.ToString());
            redis.SetEntryInHash(vremeTmp, "Trajanje", a.Trajanje.ToString());
            //public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        }

        public Aukcija ProcitajAukciju(string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);

            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(mainHashKey);

            Aukcija tmp = new Aukcija();
            string s;
            dc.TryGetValue("ID", out s);
            tmp.ID = s;
            dc.TryGetValue("Naziv", out s);
            tmp.Naziv = s;
            dc.TryGetValue("Opis", out s);
            tmp.Opis = s;
            dc.TryGetValue("Cena", out s);
            tmp.Cena = float.Parse(s);
            dc.TryGetValue("Trajanej", out s);
            tmp.Trajanje = int.Parse(s);

            return tmp;
        }

        public List<Aukcija> VratiSveAukcije()
        {
            Dictionary<string, string> dc = redis.GetAllEntriesFromHash("AUKCIJE");
            List<Aukcija> lista = new List<Aukcija>();

            foreach(KeyValuePair<string, string> entry in dc)
            {
                lista.Add(ProcitajAukciju(entry.Key));
            }

            return lista;
        }

        public void povecajCenu(float cena, string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);
            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(mainHashKey);

            float novaCena;
            string s;
            dc.TryGetValue("Cena", out s);
            novaCena = float.Parse(s) + cena;
            redis.SetEntryInHash(mainHashKey, "Cena", novaCena.ToString());
        }

    }
}
