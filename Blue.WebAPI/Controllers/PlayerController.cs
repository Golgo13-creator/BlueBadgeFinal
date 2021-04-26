﻿using Blue.Models;
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
    public class PlayerController : ApiController
    {
        private PlayerService CreatePlayerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var playerService = new PlayerService(userId);
            return playerService;
        }
        //post
        [HttpPost]
        public IHttpActionResult PostPlayer([FromBody] PlayerCreate player)
        {
            if (player is null)
                return BadRequest("Cannot use null values.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlayerService();
            var isSuccessful = service.PostPlayer(player);
            if (!isSuccessful)
                return InternalServerError();
            return Ok("Player Created");
        }
        //get players
        [HttpGet]
        public IHttpActionResult GetPlayers()
        {
            var service = CreatePlayerService();
            var players = service.GetPlayers();
            if (players is null)
                return InternalServerError();
            return Ok(players);
        }
    }
}
