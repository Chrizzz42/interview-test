using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public enum VenueType
{
    MICRO, REGIONAL, BREWPUB, LARGER
}
public interface IVenueRepository
{
    Task<IEnumerable<Venue>> GetAllVenues();
    Task<IEnumerable<Venue>> GetAllVenues(VenueType venueType);
    Task<Venue> GetVenue(int id);
}
