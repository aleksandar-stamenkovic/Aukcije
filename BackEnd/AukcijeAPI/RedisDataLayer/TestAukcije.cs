using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDataLayer.Models;


namespace RedisDataLayer
{
    public class TestAukcije
    {
        readonly RedisClient redis = new RedisClient("localhost");
        public void Ucitaj()
        {
            redis.Incr("tester");
        }


    }
}
