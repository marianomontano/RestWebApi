using System.Diagnostics;

namespace RestWebApi.Entities
{
	[DebuggerDisplay("{FirstName} {LastName}", Name = "Player Name")]
	public class Player : Entity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int ShirtNumber { get; set; }
		public int TeamId { get; set; }

	}
}