using System;
using System.Threading.Tasks;
using BackSide2.BL.BoardPinService;
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
        private readonly IPinService _pinService;
        private readonly IBoardPinService _boardPinService;

        public PinController(IPinService pinService, IBoardPinService boardPinService)
        {
            _pinService = pinService;
            _boardPinService = boardPinService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var responsePayload = await _pinService.GetPinAsync(id);
                return Ok(responsePayload);
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
                var responsePayload = await _pinService.AddPinAsync(model);
                return Ok(responsePayload);
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
                var responsePayload = await _pinService.DeletePinAsync(model);
                return Ok(responsePayload);
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
                var responsePayload = await _pinService.UpdatePinAsync(model);
                //var responsePayload = "";
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        [Authorize]
        [HttpGet("getBoardsWherePinSaved")]
        public async Task<IActionResult> GetBoardWherePinSaved(
            int pinId
        )
        {
            try
            {
                var responsePayload = await _boardPinService.GetBoardsWherePinsSavedAsync(pinId);
                return Ok(responsePayload);
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
                var responsePayload = await _boardPinService.GetBoardsWherePinsNotSavedAsync(pinId);
                return Ok(responsePayload);
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
                var responsePayload = await _boardPinService.AddPinToBoardAsync(model);
                return Ok(responsePayload);
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
                var responsePayload = await _boardPinService.DeletePinFromBoardAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}