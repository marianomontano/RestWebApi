using RestWebApi.Abstractions;
using RestWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Text;


namespace RestWebApi.Application
{
	public class Application<T> : IApplication<T> where T : IEntity
	{
		private IRepository<T> repository;
		public Application(IRepository<T> repository)
		{
			this.repository = repository;
		}

		public bool Delete(int id)
		{
			return repository.Delete(id);
		}

		public IList<T> GetAll()
		{
			return repository.GetAll();
		}

		public T GetById(int id)
		{
			return repository.GetById(id);
		}

		public bool Create(T element)
		{
			return repository.Create(element);
		}

		public bool Update(T oldElement, T newElement)
		{
			return repository.Update(oldElement, newElement);
		}
	}
}
