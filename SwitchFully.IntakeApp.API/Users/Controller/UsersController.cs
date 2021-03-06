﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchFully.IntakeApp.API.Users.Dto;
using SwitchFully.IntakeApp.API.Users.Mapper;
using SwitchFully.IntakeApp.Domain.Users;
using SwitchFully.IntakeApp.Service.Security;
using SwitchFully.IntakeApp.Service.Users;
using System;
using System.Threading.Tasks;

namespace SwitchFully.IntakeApp.API.Users.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserMapper _userMapper;
		private readonly IUserService _userService;
		private readonly UserAuthenticationService _userAuthenticationService;

		public UsersController(UserMapper userMapper, IUserService userService, UserAuthenticationService userAuthenticationService)
		{
			_userMapper = userMapper;
			_userService = userService;
			_userAuthenticationService = userAuthenticationService;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult<UserDto>> Register([FromBody] UserRegisterDto userRequestDto)
		{
			User user = _userMapper.UserRegisterDtoToDomain(userRequestDto);
			var userId = await _userService.Create(user);
			UserDto userDto = _userMapper.UserToUserDto(await _userService.GetById(Guid.Parse(userId)));
			return Ok(userDto);
		}

		[HttpPost("authenticate")]
		[AllowAnonymous]
		public ActionResult<TokenDTO> Authenticate([FromBody] UserRegisterDto userRequestDto)
		{
			var securityToken = _userAuthenticationService.Authenticate(userRequestDto.Email, userRequestDto.Password);

			if (securityToken != null)
			{
                TokenDTO token = new TokenDTO();
                token.Token = securityToken.RawData;
                return Ok(token);
            }

			return BadRequest("Email or Password incorrect!");
		}

		[HttpGet("current")]
		[Authorize]
		public ActionResult<UserDto> GeCurrentUser()
		{
			var authenticatedUser =  _userAuthenticationService.GetCurrentLoggedInUser(User);
			if (authenticatedUser != null)
			{
				return Ok( _userMapper.UserToUserDto(authenticatedUser));
			}
			return BadRequest("Could not find your user information... Contact us :)");
		}

	}
}