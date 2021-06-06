using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using RestWebApi.Abstractions;
using RestWebApi.Api.Configuration;
using RestWebApi.Services;

namespace RestWebApi.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ITokenHandlerService _tokenHandlerService;

		public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService)
		{
			_tokenHandlerService = tokenHandlerService;
			_userManager = userManager;
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.Values.SelectMany(x => x.Errors).ToList());
			}

			var existingUser = await _userManager.FindByEmailAsync(request.Email);
			if (existingUser != null)
				return BadRequest("El Email ya se encuentra registrado.");

			var isCreated = await _userManager.CreateAsync(new IdentityUser
			{
				UserName = request.Name,
				Email = request.Email
			},
			request.Password);

			if (!isCreated.Succeeded)
				return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
			
			return Ok();
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(new LoginUserResponse
				{
					Login = false,
					Errors = ModelState.Values
									   .SelectMany(x => x.Errors)
									   .Select(e => e.ErrorMessage)
									   .ToList()
				});
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
				return BadRequest(new LoginUserResponse
				{
					Login = false,
					Errors = new List<string>(){"Email no encontrado"}
				});

			bool loginCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
			if (!loginCorrect)
				return BadRequest(new LoginUserResponse
				{
					Login = false,
					Errors = new List<string>(){"Email o contraseña erróneos"}
				});
			var parameters = new TokenParameters
			{
				Id = user.Id,
				UserName = user.UserName,
				PasswordHash = user.PasswordHash
			};
			
			string token = _tokenHandlerService.GenerateJwtToken(parameters);
			return Ok(new LoginUserResponse
			{
				Login = true,
				Token = token,
				Errors = null
			});
		}

		[HttpGet]
		[Route("Me")]
		public IActionResult Me()
		{
			string header = Request.Headers["Authorization"];
			var claims = new List<ClaimReturnValues>();
			_tokenHandlerService.DecodeTokenClaims(header)
				.ToList()
				.ForEach(claim =>
					{
						claims.Add( new ClaimReturnValues { ClaimType = claim.Type, ClaimValue = claim.Value });
					});
			return Ok(claims);
		}
	}
}
