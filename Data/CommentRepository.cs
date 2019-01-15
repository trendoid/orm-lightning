using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Data.Models;

namespace Data
{
    public class CommentRespository : ICommentRespository
    {
        private readonly string _connectionString;

        public CommentRespository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Comment> GetComments(int amount, string sort)
        {
            var comments = new List<Comment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT TOP " + amount + " [CommentId],[Content],[HecklerId] FROM [Comments] ORDER BY CommentId " + sort, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            CommentId = int.Parse(reader[0].ToString()),
                            Content = reader[1].ToString(),
                            HecklerId = int.Parse(reader[2].ToString())

                        });
                    }
                }
            }
            return comments;
        }

        public Comment GetSingleComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public bool InsertComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}