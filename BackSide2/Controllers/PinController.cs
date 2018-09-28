using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.BoardService;
using BackSide2.BL.Entity;
using BackSide2.BL.Entity.BoardDto;
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
        private readonly IPinService _pinService;
        public PinController(IBoardService boardService, IPinService pinService)
        {
            _boardService = boardService;
            _pinService = pinService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var resopnsePlayload = await _pinService.GetPinAsync(id);
                //var cls = User.Claims.ToArray();
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]                 
        [HttpPost("addPin")]
        public async Task<IActionResult> AddPin(
            AddPinDto model
        )
        {
            try
            {
                var resopnsePlayload = await _pinService.AddPinAsync(model);
                //var cls = User.Claims.ToArray();
                //var resopnsePlayload = "";
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpDelete("deletePin")]
        public async Task<IActionResult> DeletePin(
            DeletePinDto model
        )
        {
            try
            {
                var resopnsePlayload = await _pinService.DeletePinAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("updatePin")]
        public async Task<IActionResult> UpdatePin(
            UpdatePinDto model
        )
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var resopnsePlayload = await _pinService.UpdatePinAsync(model);
                //var resopnsePlayload = "";
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        //[Authorize]
        //[HttpPost("getPins")]
        //public async Task<IActionResult> GetPins()
        //{
        //    try
        //    {
        //        long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        var responsePayload = await _boardService.GetBoardsAsync();
        //        return Ok(responsePayload);

        //        //long userId = (long)Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //        //var cls = User.Claims.ToArray();
        //        //var asd = cls;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { ex.Message });
        //    }
        //}
    }
}