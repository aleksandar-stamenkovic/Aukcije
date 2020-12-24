using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDataLayer
{
    public class Aukcija
    {
        readonly RedisClient redis = new RedisClient("localhost");

        public void Upisi(string key, string value)
        {
            redis.Set(key, value);
        }
    }
}
