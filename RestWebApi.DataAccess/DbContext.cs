using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RestWebApi.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace RestWebApi.DataAccess
{
	public class DbContext<T> : IDbContext<T> where T : class, IEntity
	{
		private readonly ApiDbContext db;

		public virtual DbSet<T> Entity { get; set; }
		public DbContext(ApiDbContext db)
		{
			db.Database.Migrate();
			this.db = db;
			this.Entity = db.Set<T>();
		}

		public bool Create(T element)
		{
			try
			{
				Entity.Add(element);
				db.SaveChanges();
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
				Entity.Remove(GetById(id));
				db.SaveChanges();
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
			return Entity.ToList();
		}

		public T GetById(int id)
		{
			var entity = Entity.FirstOrDefault(x => x.Id == id);

			return entity;
		}

		public bool Update(T oldElement, T newElement)
		{
			try
			{
				db.Entry(oldElement).CurrentValues.SetValues(newElement);
				db.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
				throw new ArgumentException();
			}
		}
	}
}
