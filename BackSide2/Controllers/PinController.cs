using System;
using System.Threading.Tasks;
using BackSide2.BL.BoardPinService;
using BackSide2.BL.BoardService;
using BackSide2.BL.Entity.BoardDto;
using BackSide2.BL.Entity.PinDto;
using BackSide2.BL.Models.BoardPinDto;
using BackSide2.BL.Models.PinDto;
using BackSide2.BL.PinService;
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
        private readonly IBoardPinService _boardPinService;

        public PinController(IBoardService boardService, IPinService pinService, IBoardPinService boardPinService)
        {
            _boardService = boardService;
            _pinService = pinService;
            _boardPinService = boardPinService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var resopnsePlayload = await _pinService.GetPinAsync(id);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
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
                return BadRequest(new {ex.Message});
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
                var resopnsePlayload = await _pinService.UpdatePinAsync(model);
                //var resopnsePlayload = "";
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        //[Authorize]
        [HttpGet("getBoardsWherePinSaved")]
        public async Task<IActionResult> GetBoardWherePinSaved(
            int pinId
        )
        {
            try
            {
                var resopnsePlayload = await _boardPinService.GetBoardsWherePinsSavedAsync(pinId);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpGet("getBoardsWherePinNotSaved")]
        public async Task<IActionResult> GetBoardWherePinNotSaved(
            int pinId
        )
        {
            try
            {
                var resopnsePlayload = await _boardPinService.GetBoardsWherePinsNotSavedAsync(pinId);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        [Authorize]
        [HttpPost("addPinToBoard")]
        public async Task<IActionResult> AddPinToBoard(
            AddPinToBoardDto model
        )
        {
            try
            {
                var resopnsePlayload = await _boardPinService.AddPinToBoardAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpDelete("deletePinFromBoard")]
        public async Task<IActionResult> DeletePinFromBoard(
            DeletePinFromBoardDto model
        )
        {
            try
            {
                var resopnsePlayload = await _boardPinService.DeletePinFromBoardAsync(model);
                return Ok(resopnsePlayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}