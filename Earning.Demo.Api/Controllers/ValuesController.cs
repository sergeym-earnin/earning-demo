using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Earning.Demo.Api.Services;

namespace Earning.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IDictionary<string, string> Get()
        {
            using (StorageService storage = new StorageService())
            {
                return new Dictionary<string, string>
                {
                    { storage.REDIS_API_ITEM_KEY, storage.Get(storage.REDIS_API_ITEM_KEY) },
                    { storage.REDIS_ITEM_KEY, storage.Get(storage.REDIS_ITEM_KEY) }
                };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            using (StorageService storage = new StorageService())
            {
                return storage.Get(id);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]int value = 1)
        {
            using (StorageService storage = new StorageService())
            {
                storage.Increment(storage.REDIS_API_ITEM_KEY, value);
            }
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
