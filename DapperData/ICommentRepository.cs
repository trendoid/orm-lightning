using System.Collections.Generic;
using DapperData.Models;

namespace DapperData
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