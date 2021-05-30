using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestWebApi.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;

		public AuthController(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.Values.SelectMany(x => x.Errors).ToList());
			}

			var existingUser = await userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
				return BadRequest("El Email ya se encuentra registrado.");

			var isCreated = await userManager.CreateAsync(new IdentityUser
			{
				UserName = request.Name,
				Email = request.Email
			},
			request.Password);

			if (!isCreated.Succeeded)
				return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());

			return Ok();
		}
	}
}
