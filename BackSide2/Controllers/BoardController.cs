﻿using System;
using System.Threading.Tasks;
using BackSide2.BL.BoardPinService;
using BackSide2.BL.BoardService;
using BackSide2.BL.Models.BoardDto;
using BackSide2.BL.Models.BoardPinDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackSide2.Controllers
{
    [Route("board")]
    [ApiController]
    public class DeskController : Controller
    {
        private readonly IBoardService _boardService;
        private readonly IBoardPinService _boardPinService;

        public DeskController(IBoardService boardService, IBoardPinService boardPinService)
        {
            _boardService = boardService;
            _boardPinService = boardPinService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var responsePayload = await _boardService.GetBoardAsync(id);
                return Ok(responsePayload);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpGet("getBoardPins")]
        public async Task<IActionResult> GetBoardPins(
            [FromQuery] GetBoardPinsDto model
        )
        {
            try
            {
                var responsePayload = await _boardPinService.GetBoardPinsAsync(model);
                return Ok(responsePayload);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
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
                var responsePayload = await _boardService.AddBoardAsync(model);
                return Ok(responsePayload);
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
                var responsePayload = await _boardService.DeleteBoardAsync(model);
                return Ok(responsePayload);
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
                var responsePayload = await _boardService.UpdateBoardAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpGet("getBoards")]
        public async Task<IActionResult> GetBoards(string userNickname)
        {
            try
            {
                var responsePayload = await _boardService.GetBoardsAsync(userNickname);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}