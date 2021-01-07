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
        KorisnikRedis _kr;

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

        private string _Generisi_Id()
        {
            long idCounter = redis.Incr("AUKCIJA_ID_BROJAC");
            int tmp = (int)idCounter;
            return tmp.ToString();
        }

        public void DodajNovuAukciju(Aukcija a)
        {
            _Dodaj_UAll_Listi(a.ID, a.Vreme.ToString("dd MM yyyy hh:mm:ss"));

            string vremeTmp =  a.Vreme.ToString("dd MM yyyy hh:mm:ss");
            redis.SetEntryInHash(vremeTmp, "ID", _Generisi_Id());
            redis.SetEntryInHash(vremeTmp, "Naziv", a.Naziv);
            redis.SetEntryInHash(vremeTmp, "Opis", a.Opis);
            redis.SetEntryInHash(vremeTmp, "Cena", a.Cena.ToString());
            redis.SetEntryInHash(vremeTmp, "Trajanje", a.Trajanje.ToString());
            redis.SetEntryInHash(vremeTmp, "Vlasnik", a.Vlasnik);
            redis.SetEntryInHash(vremeTmp, "Bideri", vremeTmp + ":BIDERI");

            _kr.DodajAukcijuKorisniku(a.ID, a.Vlasnik);
            //public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        }

        public Aukcija ProcitajAukciju(string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);

            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(mainHashKey);

            Aukcija tmp = new Aukcija();
            string s;
            DateTime tmpTime = DateTime.Parse(mainHashKey);
            tmp.Vreme = tmpTime;

            dc.TryGetValue("ID", out s);
            tmp.ID = s;
            dc.TryGetValue("Naziv", out s);
            tmp.Naziv = s;
            dc.TryGetValue("Opis", out s);
            tmp.Opis = s;
            dc.TryGetValue("Cena", out s);
            tmp.Cena = float.Parse(s);
            dc.TryGetValue("Trajanje", out s);
            tmp.Trajanje = int.Parse(s);
            dc.TryGetValue("Vlasnik", out s);
            tmp.Vlasnik = s;

            List<string> list = redis.GetRangeFromList(mainHashKey + ":BIDERI", 0, -1);

            tmp.Bideri = list;

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

        public void PovecajCenu(float cena, string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);
            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(mainHashKey);

            float novaCena;
            string s;
            dc.TryGetValue("Cena", out s);
            novaCena = float.Parse(s) + cena;
            redis.SetEntryInHash(mainHashKey, "Cena", novaCena.ToString());
        }

        public void DodajBidera(string id, string email, string ime, string prezime, float cena)
        {                   //id aukcije//
            string mainHashKey = _Procitaj_IzAll_Liste(id);
            string listElem = email + " " + ime + " " + prezime + " " + cena;

            redis.PushItemToList(mainHashKey + ":BIDERI", listElem);
        }

        public void ObrisiAukciju(string id)
        {
            /* -> pronadji u glavni hash (AUKCIJE) i nacu kljuc aukcije
             * -> obrisi aukciju na osnovu kljuca
             * -> obrisi aukciju u listi aukcija korisnika
             * -> obrisi listu bidera na osnovu kljuca i sufiksa "BIDERI"
             * -> obrisi entry u glavnom hashu (AUKCIJE)
             */
            string mainHashKey = _Procitaj_IzAll_Liste(id);
            string vlasnikEmail = redis.GetValueFromHash(mainHashKey, "Vlasnik");
            _kr.ObrisiAukcijuKorisnika(id, vlasnikEmail);
            redis.DeleteById<string>(mainHashKey);
            redis.DeleteById<string>(mainHashKey + "BIDERI");
            redis.RemoveEntryFromHash("AUKCIJE", id);
        }

    }
}

/*
 * frontend funcija koja refreshuje cene svih aukcija na strani na kojoj su izlistane sve aukcije
 * backend proverava da li je doslo do promene cene i kad god se cena promeni pozove
 * callback na frontendu koji refreshuje sve cene
 * (sve ovo iznad se moze implementirati bez koriscenja PUB/SUB Redis mehanizma
 * 
 * Ukoliko zelimo da upotrebimo negde PUB/SUB Redis mehanizam
 * 
 * PRVA VARIJANTA
 * Moze se dodati mogucnost gde se korisnik moze automatski subscribe-ovati na aukciju na kojoj
 * je licitirao i time se omoguci da se korisnik obavesti svaki put kad se promeni cena
 * 
 * DRUGA VARIJANTA..
 * 
 */


/*
        //OBRISATI
        //test funkcija ne radi
        public string test()
        {
            var x = redis.ReceiveMessages();
            return "" + x;
        }

        //OBRISATI
        //test funkcija ne radi
        public void test2()
        {
            redis.Subscribe("test");
            return;
        }
*/