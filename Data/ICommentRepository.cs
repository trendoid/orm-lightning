using System.Collections.Generic;
using Data.Models;

namespace Data
{
	internal interface ICommentRespository
	{
		List<Comment> GetComments(int amount, string sort);

		Comment GetSingleComment(int commentId);

		bool InsertComment(Comment comment);

		bool DeleteComment(int commentId);

		bool UpdateComment(Comment comment);
	}
}