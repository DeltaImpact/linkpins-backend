using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackSide2.BL.authorize;
using BackSide2.BL.Entity;
using BackSide2.BL.Extentions;
using BackSide2.DAO.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BackSide2.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AccountController(ITokenService tokenService
        )
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public string Get()
        {
            string messageToVisitor = "You are not logged.";
            if (User.Identity.IsAuthenticated)
            {
                messageToVisitor = $"Hello, {User.Claims.First().Value}.";
            }

            return DateTime.Now + "\n" + messageToVisitor;
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
        public async Task<IActionResult> UserInfo(
        )
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var userBd = (Person) await _tokenService.GetUserProfileInfo(userEmail);
            var user = userBd.toProfileOwnReturnDto();
            return Ok(user);
        }

        [Authorize]
        [HttpPost("is_token_valid")]
        public async Task<IActionResult> IsTokenValid(
            LoginDto model
        )
        {
            return Ok();
        }
    }
}