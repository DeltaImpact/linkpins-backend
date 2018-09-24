using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.BoardService;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.PinDto;
using BackSide2.BL.PinService;
using BackSide2.DAO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackSide2.Controllers
{
    [Route("pin")]
    [ApiController]
    public class PinController : Controller
    {
        private readonly IBoardService _boardService;
        public PinController(IBoardService boardService
        )
        {
            _boardService = boardService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        [Authorize]
        [HttpPost("addPin")]
        public async Task<IActionResult> AddPin(
            AddPinDto model
        )
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                //var resopnsePlayload = await _boardService.AddBoardAsync(model, userId);
                //var cls = User.Claims.ToArray();
                var resopnsePlayload = "";
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        //[Authorize]
        [HttpPost("deletePin")]
        public async Task<IActionResult> DeletePin(
            DeleteBoardDto model
        )
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var resopnsePlayload = await _boardService.DeleteBoardAsync(model, userId);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("getPins")]
        public async Task<IActionResult> GetPins()
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var responsePayload = await _boardService.GetBoardsAsync(userId);
                return Ok(responsePayload);
                
                //long userId = (long)Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
                //var cls = User.Claims.ToArray();
                //var asd = cls;
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}