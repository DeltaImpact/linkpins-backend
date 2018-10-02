﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackSide2.BL.authorize;
using BackSide2.BL.Entity;
using BackSide2.BL.Extentions;
using BackSide2.BL.Models.AuthorizeDto;
using BackSide2.BL.Models.ProfileDto;
using BackSide2.BL.ProfileService;
using BackSide2.DAO.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BackSide2.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IProfileService _profileService;

        public AccountController(ITokenService tokenService, IProfileService profileService)
        {
            _tokenService = tokenService;
            _profileService = profileService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterDto model
        )
        {
            try
            {
                var resopnsePlayload = await _tokenService.RegisterAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(
            LoginDto model
        )
        {
            try
            {
                var resopnsePlayload = await _tokenService.LoginAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> Profile(
        )
        {
            try
            {
                var user = await _profileService.GetUserProfileInfo();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model
        )
        {
            try
            {
                await _profileService.ChangePasswordAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPut("editProfile")]
        public async Task<IActionResult> EditProfile(EditProfileDto model
        )
        {
            try
            {
                var user = await _profileService.ChangeProfileAsync(model);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }



        [Authorize]
        [HttpPost("is_token_valid")]
        public OkResult IsTokenValid(
            LoginDto model
        )
        {
            return Ok();
        }
    }
}