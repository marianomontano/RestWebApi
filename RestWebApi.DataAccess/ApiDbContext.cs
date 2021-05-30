using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWebApi.DataAccess
{
	public class ApiDbContext : IdentityDbContext
	{

		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{

		}
		
		public virtual DbSet<FootballTeam> FootballTeams { get; set; }
		public virtual DbSet<Player> Players { get; set; }
	
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//No crear tabla Entity
			builder.Ignore<Entity>();
			base.OnModelCreating(builder);
		}
	}
}
