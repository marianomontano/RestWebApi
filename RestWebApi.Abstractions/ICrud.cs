using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.Abstractions
{
	public interface ICrud<T>
	{
		IList<T> GetAll();

		T GetById(int id);

		bool Create(T element);

		bool Update(T oldElement, T newElement);

		bool Delete(int id);
	}
}
