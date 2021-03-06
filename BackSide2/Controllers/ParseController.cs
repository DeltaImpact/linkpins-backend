﻿using System;
using System.Linq;
using System.Threading.Tasks;
using BackSide2.BL.Models.ParseDto;
using BackSide2.BL.ParsePageService;
using Microsoft.AspNetCore.Mvc;

namespace BackSide2.Controllers
{
    [Route("parse")]
    [ApiController]
    public class ParseController : ControllerBase
    {
        private readonly IParsePageService _parsePageService;

        public ParseController(IParsePageService parsePageService
        )
        {
            _parsePageService = parsePageService;
        }

        [HttpGet]
        public string Get()
        {
            var messageToVisitor = "You are not logged.";
            if (User.Identity.IsAuthenticated)
            {
                messageToVisitor = $"Hello, {User.Claims.First().Value}.";
            }

            return DateTime.Now + "\n" + messageToVisitor;
        }


        [HttpPost]
        public async Task<IActionResult> ParsePage(
            ParsePageDto model
        )
        {
            try
            {
                var responsePayload = await _parsePageService.ParsePageAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}