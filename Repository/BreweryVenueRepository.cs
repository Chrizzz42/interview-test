using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

// TODO: External service. Consider introducing circuit breaker to not overload
public class BreweryVenueRepository : IVenueRepository
{
    private readonly HttpClient client;
    private readonly string path = "https://api.openbrewerydb.org/breweries";
    public BreweryVenueRepository()
    {
        client = new HttpClient();
    }
    public async Task<IEnumerable<Venue>> GetAllVenues()
    {
        var response = await client.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsAsync<IEnumerable<Venue>>();
            return res;
        }
        throw new Exception();
    }

    public async Task<IEnumerable<Venue>> GetAllVenues(VenueType venueType)
    {
        string typeString;
        switch (venueType)
        {
            case VenueType.MICRO:
                typeString = "micro";
                break;
            case VenueType.REGIONAL:
                typeString = "regional";
                break;
            case VenueType.BREWPUB:
                typeString = "brewpub";
                break;
            case VenueType.LARGER:
                typeString = "larger";
                break;
            default:
                return new List<Venue>();
        }
        var response = await client.GetAsync(path + "?by_type=" + typeString);
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsAsync<IEnumerable<Venue>>();
            return res;
        }
        throw new Exception();

    }

    public async Task<Venue> GetVenue(int id)
    {
        var response = await client.GetAsync(path + "/" + id.ToString());
        if (response.IsSuccessStatusCode)
        {
            var res = await response.Content.ReadAsAsync<Venue>();
            return res;
        }
        throw new Exception();
    }
}
