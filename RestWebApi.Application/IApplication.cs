using RestWebApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.Application
{
	public interface IApplication<T> : ICrud<T>
	{
	}
}
