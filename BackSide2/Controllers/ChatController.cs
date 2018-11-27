using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BackSide2.BL.Models.ChatDto;
using Microsoft.AspNetCore.Mvc;
using BackSide2.BL.ProfileService;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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
                //var targetUsr = await (await _personService.GetAllAsync(d => d.Id == userId2)).FirstOrDefaultAsync();
                await _hubContext.Clients.All.SendAsync("MessageSend", userId, userId2, message);
                //await _hubContext.Clients.All.SendAsync("SendAction", userId, userId2, message);

                //var user = await _personService.GetByIdAsync(userId);
                //var sentToUser = await _personService.GetByIdAsync(message.SentTo);
                //var sentToUser1 = await _personService.GetByIdAsync(message.SentTo);
                //var sentToUser2 = await _personService.GetByIdAsync(message.SentTo);

                //var user = await (await _personService.GetAllAsync(d => d.Id == userId)).FirstOrDefaultAsync();

                //var userId = long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                //var callerUserTask = _personService.GetByIdAsync(userId);
                //var targetUserTask = _personService.GetByIdAsync(message.SentTo);

                //var userId2 = message.SentTo;
                //var user = await callerUserTask;
                //var sentToUser = await targetUserTask;
                //var user = await _personService.GetByIdAsync(userId);
                //var sentToUser = await _personService.GetByIdAsync(message.SentTo);

                //if (sentToUser == null)
                //    throw new ArgumentException("User with this id does not exist");

                //var newChatMessage = new ChatMessage
                //{
                //    CreatedBy = user.Id,
                //    MessageContent = message.Message,
                //    ReceivedBy = sentToUser
                //};
                //await _chatMessageRepository.InsertAsync(newChatMessage);
                //await _hubContext.Clients.User(sentToUser?.Id.ToString())
                //    .SendAsync("MessageReceived", user.UserName, message, true);
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
    }
}