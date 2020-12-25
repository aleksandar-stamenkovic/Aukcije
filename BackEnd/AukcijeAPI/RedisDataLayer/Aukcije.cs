using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDataLayer
{
    public class Aukcije
    {
        readonly RedisClient redis = new RedisClient("localhost");

        public string Ucitaj(string key)
        {
            return redis.Get<string>(key);
        }
    }
}
