using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Interview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VenueController : ControllerBase
    {

        private readonly IVenueRepository _venueRepository;

        public VenueController()
        {
            _venueRepository = new BreweryVenueRepository();
        }

        [HttpGet]
        public IEnumerable<Venue> Get()
        {
            var res = _venueRepository.GetAllVenues().Result;
            return res;
        }

        [HttpGet]
        public Venue Get(int id)
        {
            var res = _venueRepository.GetVenue(id).Result;
            return res;
        }
        
    }
}
