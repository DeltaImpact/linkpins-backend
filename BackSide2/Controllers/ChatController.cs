using BackSide2.BL.Models.ChatDto;
using BackSide2.BL.ProfileService;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackSide2.Controllers
{
    [Route("chat")]
    //[ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRepository<Person> _personService;
        private readonly IRepository<ChatMessage> _chatMessageRepository;


        public ChatController(IProfileService profileService, IHubContext<ChatHub> hubContext,
            IRepository<Person> personService, IRepository<ChatMessage> chatMessageRepository)
        {
            _hubContext = hubContext;
            _personService = personService;
            _chatMessageRepository = chatMessageRepository;
        }


        [Authorize]
        [HttpPost("sendMessage")]
        public async void SendMessage(
            GetPmDto message
        )
        {
            try
            {
                var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userId2 = message.SentTo;
                await _hubContext.Clients.All.SendAsync("MessageSend", userId, userId2, message);
            }
            catch (Exception ex)
            {
                BadRequest(new {ex.Message});
            }
        }

        [Authorize]
        [HttpPut("startTyping")]
        public async Task StartTyping()
        {
            var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _personService.GetByIdAsync(userId);
            if (user == null)
            {
                BadRequest("User not found.");
            }
            else
                await _hubContext.Clients.All.SendAsync("StartTyping", user.UserName);
        }

        [Authorize]
        [HttpPut("stopTyping")]
        public async Task StopTyping()
        {
            var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _personService.GetByIdAsync(userId);
            if (user == null)
            {
                BadRequest("User not found.");
            }
            else
                await _hubContext.Clients.All.SendAsync("StopTyping", user.UserName);
        }

        [Authorize]
        [HttpGet("getDialogs")]
        public async Task GetDialogs()
        {
            var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
        }
    }
}