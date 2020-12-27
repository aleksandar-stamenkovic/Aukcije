using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDataLayer.Models;

namespace RedisDataLayer
{
    class KorisnikRedis
    {
        readonly RedisClient redis = new RedisClient("localhost");
    }
}
