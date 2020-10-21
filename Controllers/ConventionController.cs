using Interview.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Interview.Controllers
{
    [ApiController]
    [Route("Conventions")]
    public class ConventionController : ControllerBase
    {
        // TODO: Move all repo instantiation to IOC
        private readonly IConventionRepository conventionRepository;
        private readonly ITalkRepository talkRepository;
        private readonly IVenueRepository venueRepository;

        public ConventionController()
        {
        }


        /// <summary>
        /// Returns list of all available conventions. Used by client for browsing.
        /// </summary>
        /// <returns></returns>
        [Authorize("read:conventions")]
        [HttpGet]
        public ActionResult<List<ConventionEntity>> GetConventions()
        {
            var cons = conventionRepository.GetConventions().Result;
            return cons;
        }

        /// <summary>
        /// Return specific convention.
        /// </summary>
        /// <param name="id">Id of convention</param>
        /// <returns></returns>
        [Authorize("read:conventions")]
        [HttpGet("{id}")]
        public ActionResult<ConventionEntity> GetConvention(int id)
        {
            var convention = conventionRepository.GetConvention(id).Result;
            if (convention != null)
            {
                return convention;
            }
            return NotFound();
        }

        [HttpPost("/{conventionId:int}/participants")]
        [Authorize("signup:conventions")]
        public ActionResult<bool> RegisterForConvention([FromRoute]int conventionId, [FromBody]int userId)
        {
            var convention = conventionRepository.GetConvention(conventionId).Result;
            if (convention != null)
            {
                // TODO: Atomic operations required, so we can react on race signups
                if (convention.ParticipantIds.Count() < convention.Capacity)
                {
                    var signedUp = conventionRepository.RegisterUserForConvention(userId, conventionId).Result;
                    // TODO: Better response, such as "Convention at capacity", "User blacklisted", and other errors
                    return signedUp;
                }
                else
                {
                    return StatusCode(406, "Convention is at capacity");
                }

            }
            return NotFound("Convention was not found.");
        }

        [Authorize("create:conventions")]
        [HttpPost]
        public ActionResult<ConventionEntity> CreateConvention(Convention con)
        {
            if (con.Capacity < 1)
                return BadRequest("Capacity must be a positive integer");
            var venue = venueRepository.GetVenue(con.VenueId).Result;
            if (venue == null)
            {
                // TODO: Practical error handling here
                return conventionRepository.CreateConvention(con).Result;
            }
            return NotFound("Could not find the specified venue");
        }

        /// <summary>
        /// Used by convention administrators to change events/venues
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        [Authorize("edit:convention")] //TODO: Provided ID must satisfy ownership of this convention
        [HttpPut("{id:int}")]
        public ActionResult<bool> EditConvention([FromRoute] int id, [FromBody] Convention con)
        {
            var convention = conventionRepository.GetConvention(id).Result;
            if (convention != null) // && ownership can be ensured
            {
                if(con.Capacity < convention.ParticipantIds.Count())
                {
                    return StatusCode(406, "Capacity change would reach below current participation.");
                }
                var edited = conventionRepository.EditConvention(id, con).Result;
            }
            return NotFound();
        }


        /// <summary>
        /// Fetches all talks of a convention
        /// </summary>
        /// <param name="conventionId"></param>
        /// <returns></returns>
        [Authorize("read:convention")]
        [Authorize("read:talks")]
        [HttpGet("{conventionId:int}/talks")]
        public ActionResult<List<TalkEntity>> GetTalks(int conventionId)
        {
            try
            {
                var talks = talkRepository.GetTalks(conventionId).Result;
                return talks;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Register a specific talk
        /// </summary>
        /// <param name="conventionId">Id of the convention. Returns 404 if convention does not exist</param>
        /// <param name="talk">Talk application</param>
        /// <returns></returns>
        [Authorize("register:talks")] // Plus perhaps a check of user info regarding specific convention 
        [HttpPost("{conventionId:int}/talks")]
        public ActionResult<TalkEntity> RegisterTalk(int conventionId, Talk talk)
        {
            try
            {
                var createdTalk = talkRepository.RegisterTalk(conventionId, talk).Result;
                return createdTalk;
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
