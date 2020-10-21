using System;
using System.Collections.Generic;

public class Convention
{
	public Convention()
	{
	}

	public string Name { get; set; }
	public int VenueId { get; set; }
	public int Capacity { get; set; }
}

public class ConventionEntity : Convention
{
	public int Id { get; set; }
	public DateTime TimeCreated { get; set; }
	public List<int> ParticipantIds { get; set; }
}
