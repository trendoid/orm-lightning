using System.Collections.Generic;
using DapperData.Models;

namespace DapperData
{
	internal interface IHecklerRespository
	{
		List<Heckler> GetHecklers(int amount, string sort);

		Heckler GetSingleHeckler(int hecklerId);

		bool InsertHeckler(Heckler heckler);

		bool DeleteHeckler(int hecklerId);

		bool UpdateHeckler(Heckler heckler);
	}
}