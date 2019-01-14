using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFData;
using EFData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFDataController : ControllerBase
    {        
        private readonly LightningContext _context;

        public EFDataController(LightningContext context)
        {
            _context = context;
        }

        // GET api/efdata
        [HttpGet]
        public ActionResult<List<Heckler>> Get()
        {
            return _context.Hecklers.Include("Comments").ToList();
        }

        // GET api/efdata/5
        [HttpGet("{id}")]
        public ActionResult<Heckler> Get(int id)
        {
            return _context.Hecklers.SingleOrDefault(x => x.HecklerId == id);
        }

        // POST api/efdata
        [HttpPost]
        public void Post([FromBody] Heckler heckler)
        {
            _context.Hecklers.Add(new Heckler{
                Name = heckler.Name,
                Comments = heckler.Comments,
                Url = heckler.Url 
            });
            _context.SaveChanges();
        }

        // PUT api/efdata/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Heckler heckler)
        {
            var existing = _context.Hecklers.SingleOrDefault(x => x.HecklerId == id);
            existing.Name = heckler.Name;
            existing.Comments = heckler.Comments;
            existing.Url = heckler.Url;
            _context.SaveChanges();
        }

        // DELETE api/efdata/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Hecklers.Remove(new Heckler{
                HecklerId = id 
            });
            _context.SaveChanges();
        }
    }
}
