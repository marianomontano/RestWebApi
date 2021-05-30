using RestWebApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.Entities
{
	public abstract class Entity : IEntity
	{
		public int Id { get; set; }
	}
}
