using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        public static string ConnectionString { get; set; }
        private SqlConnection _connection { get; set; }

        private static SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = ConnectionString;

            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        private SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        public DapperController(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("LightningDatabase");
            _connection = GetOpenConnection();
        }

        // GET api/entityframework
        [HttpGet]
        public ActionResult<List<Heckler>> Get()
        {
            return _connection.Query<Heckler>("select * from Hecklers").ToList();
        }

        // GET api/entityframework/5
        [HttpGet("{id}")]
        public ActionResult<Heckler> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heckler = _connection.Query<Heckler>("select HecklerId = @Id", new { Id = id }).FirstOrDefault();

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
            //_context.Add(heckler);
            //_context.SaveChanges();
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
            
                //_context.Update(heckler);
                //_context.SaveChanges();
            return heckler;

        }

        // DELETE api/entityframework/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // var heckler = _context.Hecklers.Find(id);
            // _context.Hecklers.Remove(heckler);
            // _context.SaveChanges();
        }
    }
}
