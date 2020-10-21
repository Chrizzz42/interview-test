using System;
using System.Collections.Generic;

public class Talk
{
	public Talk()
	{
	}
	public int PresenterId { get; set; }
	public int ConventionId { get; set; }
	public int Capacity { get; set; }
	public List<int> ParticipantIds { get; set; }
}

public class TalkEntity: Talk
{
	public int Id;
	// Other cool metadata, like time created, etc.
}
