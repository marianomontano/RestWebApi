using RestWebApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.DataAccess
{
	public interface IDbContext<T> : ICrud<T>
	{
	}
}
