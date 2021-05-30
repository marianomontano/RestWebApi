using RestWebApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestWebApi.DataAccess
{
	public class InMemoryDbContext<T> : IDbContext<T> where T:IEntity
	{
		List<T> datos;

		public InMemoryDbContext()
		{
			datos = new List<T>();
		}

		public bool Create(T element)
		{
			try
			{
				datos.Add(element);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw new ArgumentException();
			}
		}

		public bool Delete(int id)
		{
			try
			{
				var entity = datos.FirstOrDefault(x => x.Id == id);
				if (entity == null)
				{
					throw new ArgumentException($"No existe {typeof(T)} con id = {id}");
				}

				datos.Remove(entity);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw new ArgumentException();
			}
		}

		public IList<T> GetAll()
		{
			return datos;
		}

		public T GetById(int id)
		{
			var entity = datos.FirstOrDefault(x => x.Id == id);

			if (entity == null)
			{
				throw new ArgumentException($"No existe {typeof(T)} con id = {id}");
			}

			return entity;
		}

		public bool Update(T oldElement, T newElement)
		{
			try
			{
				var entity = datos.FirstOrDefault(x => x.Id == oldElement.Id);
				if (entity == null)
				{
					throw new ArgumentException($"No existe {typeof(T)} con id = {oldElement.Id}");
				}
				entity = newElement;
				return true;
			}
			catch (Exception)
			{
				return false;
				throw new ArgumentException();
			}
		}
	}
}
