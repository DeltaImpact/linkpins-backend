using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.authorize;
using BackSide2.BL.BoardService;
using BackSide2.BL.Entity;
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

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        [Authorize]
        [HttpPost("addBoard")]
        public async Task<IActionResult> AddBoard(
            AddBoardDto model
        )
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var resopnsePlayload = await _boardService.AddBoardAsync(model, userEmail);
                //var cls = User.Claims.ToArray();
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpPost("deleteBoard")]
        public async Task<IActionResult> DeleteBoard(
            DeleteBoardDto model
        )
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var resopnsePlayload = await _boardService.DeleteBoardAsync(model, userEmail);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("getBoards")]
        public async Task<IActionResult> GetBoards()
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var responsePayload = await _boardService.GetBoardsAsync(userEmail);
                
                //var id = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var cls = User.Claims.ToArray();
                var asd = cls;
                //await _debtService.RemoveDebtAsync(model, id);
                //return Ok(resopnsePlayload);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}