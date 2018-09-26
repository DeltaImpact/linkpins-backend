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
using BackSide2.BL.PinService;
using BackSide2.DAO.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackSide2.Controllers
{
    [Route("board")]
    [ApiController]
    public class DeskController : Controller
    {
        private readonly IBoardService _boardService;

        public DeskController(IBoardService boardService
        )
        {
            _boardService = boardService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var resopnsePlayload = await _boardService.GetBoardAsync(id, userId);
                //var cls = User.Claims.ToArray();
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addBoard")]
        public async Task<IActionResult> AddBoard(
            AddBoardDto model
        )
        {
            try
            {
                long userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var resopnsePlayload = await _boardService.AddBoardAsync(model, userId);
                //var cls = User.Claims.ToArray();
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        //[Authorize]
        [HttpPost("deleteBoard")]
        public async Task<IActionResult> DeleteBoard(
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
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpPost("getBoards")]
        public async Task<IActionResult> GetBoards()
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
                return BadRequest(new {ex.Message});
            }
        }
    }
}