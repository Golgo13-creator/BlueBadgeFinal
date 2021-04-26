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
        public IHttpActionResult Post(SportCreate sport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateSportService();
            if (!service.CreateSport(sport))
                return InternalServerError();
            return Ok("Sport was added.");
        }
        //Get all Sport
        public IHttpActionResult Get()
        {
            SportService sportService = CreateSportService();
            var sports = sportService.GetSport();
            return Ok(sports);
        }
    }
}
