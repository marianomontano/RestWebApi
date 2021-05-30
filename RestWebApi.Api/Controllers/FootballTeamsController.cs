﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.Application;
using RestWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestWebApi.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class FootballTeamsController : ControllerBase
	{
		private IApplication<FootballTeam> FootballTeams;
		private IApplication<Player> Players;

		public FootballTeamsController(IApplication<FootballTeam> footbalTeams, IApplication<Player> players)
		{
			FootballTeams = footbalTeams;
			Players = players;
		}

		[HttpGet("getall")]
		public IActionResult GetAll()
		{
			var allTeams = FootballTeams.GetAll().ToList();
			allTeams.ForEach(t =>
				t.Players.AddRange(Players.GetAll()
					.Where(p => p.TeamId == t.Id)
					.Select(p => p)
					)
				);
			return Ok(allTeams);
		}

		[HttpGet("getbyid/{id:int}")]
		public IActionResult GetById(int id)
		{
			if (id < 1)
				return BadRequest();

			var team = FootballTeams.GetById(id);
			
			if (team == null)
				return NotFound();

			team.Players
				.AddRange(Players.GetAll()
					.Where(p => p.TeamId == id)
					.Select(p => p)
				);

			return Ok(team);
		}

		[HttpPost("addteam")]
		public IActionResult AddTeam(FootballTeam team)
		{
			if (!FootballTeams.Create(team))
				return StatusCode(500);

			return Created($"footballteams/getbyid/{team.Id}", team);
		}

		[HttpPut("editteam/{id:int}")]
		public IActionResult EditTeam(int id, FootballTeam newTeam)
		{
			var oldTeam = FootballTeams.GetById(id);
			if (oldTeam == null)
				return NotFound();

			if (!FootballTeams.Update(oldTeam, newTeam))
				return StatusCode(500);

			return NoContent();
		}

		[HttpDelete("deleteteam/{id:int}")]
		public IActionResult DeleteTeam(int id)
		{
			var team = FootballTeams.GetById(id);
			if (team == null)
				return NotFound();

			if (!FootballTeams.Delete(id))
				return StatusCode(500);

			return NoContent();
		}
	}
}