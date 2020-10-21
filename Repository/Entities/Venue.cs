using System;
using System.Runtime.Serialization;

[DataContract]
public class Venue
{
	public Venue()
	{
	}

    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember(Name = "brewery_type")]
    public string Type { get; set; }
    [DataMember]
    public string Street { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public string State { get; set; }
    [DataMember]
    public string Country { get; set; }
    [DataMember]
    public decimal Longitude { get; set; }
    [DataMember]
    public decimal Latitude { get; set; }
    [DataMember]
    public string Phone { get; set; }
    [DataMember]
    public DateTime Updated_at { get; set; }
}
