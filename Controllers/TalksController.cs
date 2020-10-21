using Interview.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Controllers
{
    public class TalksController : ControllerBase
    {
        private readonly ITalkRepository talkRepository;

        /// <summary>
        /// Reserve seat 
        /// </summary>
        /// <param name="talkId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{talkId:int}")]
        public ActionResult<bool> ReserveSeat([FromRoute] int talkId, [FromBody] int userId)
        {
            // TODO: Requires errors
            var talk = talkRepository.GetTalk(talkId).Result;
            if (talk != null)
            {
                // TODO: For this to work completely, we will need atomic operations, and reactions on them
                if (talk.ParticipantIds.Count() < talk.Capacity)
                {
                    var registered = talkRepository.ReserveSeat(talkId, userId).Result;
                    return registered;
                }
                else
                {
                    return StatusCode(406, "Talk is at capacity");
                }
            }
            return NotFound();
        }
    }
}
