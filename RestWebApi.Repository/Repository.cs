using RestWebApi.Abstractions;
using RestWebApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.Repository
{
	public class Repository<T> : IRepository<T> where T : IEntity
	{
		private IDbContext<T> dbContext;

		public Repository(IDbContext<T> dbContext)
		{
			this.dbContext = dbContext;
		}
		public bool Delete(int id)
		{
			return dbContext.Delete(id);
		}

		public IList<T> GetAll()
		{
			return dbContext.GetAll();
		}

		public T GetById(int id)
		{
			return dbContext.GetById(id);
		}

		public bool Create(T element)
		{
			return dbContext.Create(element);
		}

		public bool Update(T oldElement, T newElement)
		{
			return dbContext.Update(oldElement, newElement);
		}
	}
}
