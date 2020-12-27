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

        public void ProcitajKorisnika(string email)
        {
            var p = redis.GetHashValues("hash1");
        }
    }
}
