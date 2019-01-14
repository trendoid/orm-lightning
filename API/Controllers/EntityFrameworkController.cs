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
    public class EntityFrameworkController : ControllerBase
    {
        private readonly LightningContext _context;

        private bool HecklerExists(int id)
        {
            return _context.Hecklers.Any(e => e.HecklerId == id);
        }

        public EntityFrameworkController(LightningContext context)
        {
            _context = context;
        }

        // GET api/entityframework
        [HttpGet]
        public ActionResult<List<Heckler>> Get()
        {
            return _context.Hecklers.Include("Comments").ToList();
        }

        // GET api/entityframework/5
        [HttpGet("{id}")]
        public ActionResult<Heckler> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var heckler = _context.Hecklers.FirstOrDefault(x => x.HecklerId == id);

            if (heckler == null)
            {
                return NotFound();
            }
            return heckler;
        }

        // POST api/entityframework
        [HttpPost]
        public ActionResult<Heckler> Post([FromBody] Heckler heckler)
        {
            _context.Add(heckler);
            _context.SaveChanges();
            return heckler;
        }

        // PUT api/entityframework/5
        [HttpPut("{id}")]
        public ActionResult<Heckler> Put(int? id, [FromBody] Heckler heckler)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                _context.Update(heckler);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HecklerExists(heckler.HecklerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return heckler;

        }

        // DELETE api/entityframework/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var heckler = _context.Hecklers.Find(id);
            _context.Hecklers.Remove(heckler);
            _context.SaveChanges();
        }
    }
}
