using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Data;
using Data.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public static string ConnectionString { get; set; }

        public static HecklerRepository HecklerRepository { get; set; }

        public DataController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("LightningDatabase");
            HecklerRepository = new HecklerRepository(ConnectionString);
        }

        // GET api/data
        [HttpGet]
        public ActionResult<List<Heckler>> Get()
        {
            return HecklerRepository.GetHecklers(1000, "ASC");
        }

        // GET api/data/5
        [HttpGet("{id}")]
        public ActionResult<Heckler> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heckler = HecklerRepository.GetSingleHeckler(id.Value);

            if (heckler == null)
            {
                return NotFound();
            }
            return heckler;
        }

        // POST api/data
        [HttpPost]
        public void Post([FromBody] Heckler heckler)
        {
        }

        // PUT api/data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Heckler heckler)
        {
        }

        // DELETE api/data/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
