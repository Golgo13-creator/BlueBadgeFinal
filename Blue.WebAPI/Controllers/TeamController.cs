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
    public class TeamController : ApiController
    {
        private TeamService CreateTeamService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var teamService = new TeamService(userId);
            return teamService;
        }
        //post a team
        public IHttpActionResult Post(TeamCreate team)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTeamService();
            if (!service.CreateTeam(team))
                return InternalServerError();
            return Ok("Team was added");
        }
        //get all teams
        public IHttpActionResult Get()
        {
            TeamService teamService = CreateTeamService();
            var teams = teamService.GetTeams();
            return Ok(teams);
        }
        //get teams by sport
        public IHttpActionResult Get(int sportId)
        {
            TeamService teamService = CreateTeamService();
            var teams = teamService.GetTeamsBySport(sportId);
            return Ok(teams);
        }
        [HttpPut]
        public IHttpActionResult Put(int teamId, TeamEdit team)
        {
            if (teamId < 1)
                return BadRequest("Invalid Team Entry");
            if (team.TeamId != teamId)
                return BadRequest("Team Number missmatch");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateTeamService();
            if (!service.UpdateTeam(team))
                return InternalServerError();
            return Ok("Team was updated!");
        }
        //delete team
        public IHttpActionResult Delete(int id)
        {
            var service = CreateTeamService();
            if (!service.DeleteTeam(id))
                return InternalServerError();
            return Ok("Team was deleted.");
        }
    }
}
