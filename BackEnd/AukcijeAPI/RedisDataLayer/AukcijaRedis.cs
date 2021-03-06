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
        
        public static KorisnikRedis _kr = new KorisnikRedis();
        public static AukcijaRedis _ar = new AukcijaRedis();
        public AukcijaRedis()
        {

        }

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

        private void _DodajUListuFlegova(string element)
        {
            redis.PushItemToList("FLEGOVIAUKCIJA", element);
        }

        private void _DodajFleg(string id, int expireTime)
        {
            byte[] array = Encoding.ASCII.GetBytes(id);
            redis.SetEx(id + "FLEG", expireTime, array);

            _DodajUListuFlegova(id + "FLEG");
        }

        private void _DodajUListuPobednika(string idA)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(idA);

            string aData = redis.GetValueFromHash(mainHashKey, "Naziv");
            List<string> list = redis.GetRangeFromList(mainHashKey + ":BIDERI", -1, -1);
            if (list.Count > 0)
            {
                string biderData = list[0];

                string element = "POBEDNIK:" + biderData + "- Aukcija:" + idA + " - " + aData;
                redis.PushItemToList("POBEDNICI", element);
            }
            else
            {
                string element = "NEMA POBEDNIKA" + "- Aukcija:" + idA + " - " + aData;
                redis.PushItemToList("POBEDNICI", element);
            }
        }

        public List<string> ProcitajListuPobednika()
        {
            return redis.GetRangeFromList("POBEDNICI", 0, -1);
        }

        public string DodajNovuAukciju(Aukcija a)
        {
            string idAukcije = _Generisi_Id();
            _Dodaj_UAll_Listi(idAukcije, a.Vreme.ToString("dd MM yyyy hh:mm:ss"));

            string vremeTmp =  a.Vreme.ToString("dd MM yyyy hh:mm:ss");
            redis.SetEntryInHash(vremeTmp, "ID", idAukcije);
            redis.SetEntryInHash(vremeTmp, "Naziv", a.Naziv);
            redis.SetEntryInHash(vremeTmp, "Opis", a.Opis);
            redis.SetEntryInHash(vremeTmp, "Cena", a.Cena.ToString());
            redis.SetEntryInHash(vremeTmp, "Trajanje", a.Trajanje.ToString());
            redis.SetEntryInHash(vremeTmp, "Vlasnik", a.Vlasnik);
            redis.SetEntryInHash(vremeTmp, "Bideri", vremeTmp + ":BIDERI");

            _DodajFleg(idAukcije, a.Trajanje);

            _kr.DodajAukcijuKorisniku(idAukcije, a.Vlasnik);

            return idAukcije;
            //public void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);
        }

        public Aukcija ProcitajAukciju(string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);

            Dictionary<string, string> dc = redis.GetAllEntriesFromHash(mainHashKey);
            /*Dictionary<string, string> dc;
            if (redis.GetAllEntriesFromHash(mainHashKey) != null)
                dc = redis.GetAllEntriesFromHash(mainHashKey);
            else return null;*/

            Aukcija tmp = new Aukcija();
            string s;
            //DateTime tmpTime = DateTime.Parse(mainHashKey);
            DateTime tmpTime = DateTime.ParseExact(mainHashKey, "dd MM yyyy hh:mm:ss", null);
            tmp.Vreme = tmpTime;

            dc.TryGetValue("ID", out s);
            tmp.ID = s;
            dc.TryGetValue("Naziv", out s);
            tmp.Naziv = s;
            dc.TryGetValue("Opis", out s);
            tmp.Opis = s;
            dc.TryGetValue("Cena", out s);
            tmp.Cena = float.Parse(s);

            //dc.TryGetValue("Trajanje", out s);
            //tmp.Trajanje = int.Parse(s);
            tmp.Trajanje = (int)redis.Ttl(id + "FLEG") / 60;

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

        public List<string> ProcitajBidere(string id)
        {
            string mainHashKey = _Procitaj_IzAll_Liste(id);
            List<string> lista = redis.GetRangeFromList(mainHashKey + ":BIDERI", 0, -1);

            return lista;
        }

        

        public void ObrisiAukciju(string id)
        {
            /* -> prvo se doda pobednik aukcije u listu pobednika "POBEDNICI"
             * -> pronadji u glavni hash (AUKCIJE) i nacu kljuc aukcije
             * -> obrisi aukciju na osnovu kljuca
             * -> obrisi aukciju u listi aukcija korisnika
             * -> obrisi listu bidera na osnovu kljuca i sufiksa "BIDERI"
             * -> obrisi entry u glavnom hashu (AUKCIJE)
             */
            _DodajUListuPobednika(id);

            string mainHashKey = _Procitaj_IzAll_Liste(id);
            if (mainHashKey == null)
                return;
            string vlasnikEmail = redis.GetValueFromHash(mainHashKey, "Vlasnik");
            _kr.ObrisiAukcijuKorisnika(id, vlasnikEmail);
            redis.Del(mainHashKey);
            redis.Del(mainHashKey + ":BIDERI");
            redis.RemoveEntryFromHash("AUKCIJE", id);
        }


        public void ProveriExpireAukcija()
        {
            List<string> listaPostojecih = redis.GetAllItemsFromList("FLEGOVIAUKCIJA");

            foreach(string s in listaPostojecih)
            {
                if(redis.Ttl(s) < 0)
                {
                    byte[] idbytes = Encoding.ASCII.GetBytes(s);
                    //redis.LRem("FLEGOVIAUKCIJA", 0, idbytes);
                        string[] parsed = s.Split('F');
                    //_DodajUListuPobednika(parsed[0]);
                    ObrisiAukciju(parsed[0]);
                    redis.LRem("FLEGOVIAUKCIJA", 0, idbytes);
                }
            }
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