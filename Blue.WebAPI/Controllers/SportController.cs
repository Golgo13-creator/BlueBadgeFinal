using Blue.Models;
using Blue.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blue.WebAPI.Controllers
{
    [Authorize]
    public class SportController : ApiController
    {
        private SportService CreateSportService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var sportService = new SportService(userId);
            return sportService;
        }
        //Post Sport
        public IHttpActionResult PostPlayer(SportCreate sport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateSportService();
            if (!service.CreateSport(sport))
                return InternalServerError();
            return Ok("Sport was added.");
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            SportService sportService = CreateSportService();
            var sports = sportService.GetSport();
            return Ok(sports);
        }
        [HttpPut]
        public IHttpActionResult Put(int sportId, SportEdit sport)
        {
            if (sportId < 1)
                return BadRequest("Invalid Sport Number Entry");
            if (sport.SportId != sportId)
                return BadRequest("Sport Number missmatch");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateSportService();
            if (!service.UpdateSport(sport))
                return InternalServerError();
            return Ok("Update Successful.");
        }
        //Delete Sport
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSportService();
            if (!service.DeleteSport(id))
                return InternalServerError();
            return Ok("Sport Was Delted.");
        }
    }
}
