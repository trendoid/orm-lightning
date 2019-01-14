using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperData.Models;

namespace DapperData
{
	public class CommentRespository : ICommentRespository
	{
		private readonly IDbConnection _db;

		public CommentRespository(string connectionString)
		{
			_db = new SqlConnection(connectionString);
		}

		public List<Comment> GetComments(int amount, string sort)
		{
			return this._db.Query<Comment>("SELECT TOP " + amount + " [CommentId],[Content],[HecklerId] FROM [Comment] ORDER BY CommentId " + sort).ToList();
		}

		public Comment GetSingleComment(int commentId)
		{
			return _db.Query<Comment>("SELECT [CommentId],[Content],[HecklerId] FROM [Comment] WHERE CommentId =@CommentId", new { CommentId = commentId }).SingleOrDefault();
		}

		public bool InsertComment(Comment comment)
		{
			int rowsAffected = this._db.Execute(@"INSERT Comment([Content],[HecklerId]) values (@Content, @HecklerId)",
				new { Content = comment.Content, HecklerId = comment.HecklerId });

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}

		public bool DeleteComment(int commentId)
		{
			int rowsAffected = this._db.Execute(@"DELETE FROM [Comment] WHERE CommentId = @CommentId",
				new { CommentId = commentId });

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}

		public bool UpdateComment(Comment comment)
		{
			int rowsAffected = this._db.Execute(
						"UPDATE [Comment] SET [CommentFirstName] = @CommentFirstName ,[CommentLastName] = @CommentLastName, [IsActive] = @IsActive WHERE CommentId = " +
						comment.CommentId, comment);

			if (rowsAffected > 0)
			{
				return true;
			}

			return false;
		}
	}
}