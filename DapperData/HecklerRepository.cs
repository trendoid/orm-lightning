using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperData.Models;

namespace DapperData
{
	public class HecklerRepository : IHecklerRespository
	{
		private readonly IDbConnection _db;

		public HecklerRepository(string connectionString)
		{
			_db = new SqlConnection(connectionString);
		}

		public List<Heckler> GetHecklers(int amount, string sort)
		{
			return this._db.Query<Heckler>("SELECT TOP " + amount + " [HecklerId],[Name],[Url],[Comments] FROM [Heckler] ORDER BY HecklerId " + sort).ToList();
		}

		public Heckler GetSingleHeckler(int hecklerId)
		{
			return _db.Query<Heckler>("SELECT[HecklerId],[Name],[Url],[Comments] FROM [Heckler] WHERE HecklerId =@HecklerId", new { HecklerId = hecklerId }).SingleOrDefault();
		}

		public bool InsertHeckler(Heckler heckler)
		{
			int rowsAffected = this._db.Execute(@"INSERT Heckler([Name],[Url],[Comments]) values (@Name, @Url, @Comments)",
				new { Name = heckler.Name, Url = heckler.Url, Comments = heckler.Comments });

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}

		public bool DeleteHeckler(int hecklerId)
		{
			int rowsAffected = this._db.Execute(@"DELETE FROM [Heckler] WHERE HecklerId = @HecklerId",
				new { HecklerId = hecklerId });

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}

		public bool UpdateHeckler(Heckler heckler)
		{
			int rowsAffected = this._db.Execute(
						"UPDATE [Heckler] SET [Name] = @Name ,[Url] = @Url, [Comments] = @Comments WHERE HecklerId = " +
						heckler.HecklerId, heckler);

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}
	}
}