using System;
using System.Threading.Tasks;
using BackSide2.BL.BoardService;
using BackSide2.BL.Entity.BoardDto;
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
                var resopnsePlayload = await _boardService.GetBoardAsync(id);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
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
                var resopnsePlayload = await _boardService.AddBoardAsync(model);
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
                var resopnsePlayload = await _boardService.DeleteBoardAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpPost("updateBoard")]
        public async Task<IActionResult> UpdateBoard(
            UpdateBoardDto model
        )
        {
            try
            {
                var resopnsePlayload = await _boardService.UpdateBoardAsync(model);
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
                var responsePayload = await _boardService.GetBoardsAsync();
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}