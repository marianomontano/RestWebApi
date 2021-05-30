using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace RestWebApi.Entities
{
	[DebuggerDisplay("{Name}", Name = "Team Name")]
	public class FootballTeam : Entity
	{
		public string Name { get; set; }
		public List<Player> Players { get; set; } = new List<Player>();
	}
}
