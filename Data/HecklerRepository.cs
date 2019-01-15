using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Data.Models;

namespace Data
{
    public class HecklerRepository : IHecklerRespository
    {
        private readonly string _connectionString;

        public HecklerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Heckler> GetHecklers(int amount, string sort)
        {
            var hecklers = new List<Heckler>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT TOP " + amount + " [HecklerId],[Name],[Url] FROM [Hecklers] ORDER BY HecklerId " + sort, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
						hecklers.Add(new Heckler{
							HecklerId = int.Parse(reader[0].ToString()),
							Name = reader[1].ToString(),
							Url = reader[2].ToString()

						});
                    }
                }
            }
			return hecklers;
        }

        public Heckler GetSingleHeckler(int hecklerId)
        {
            throw new NotImplementedException();
        }

        public bool InsertHeckler(Heckler heckler)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHeckler(int hecklerId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHeckler(Heckler heckler)
        {
            throw new NotImplementedException();
        }
    }
}