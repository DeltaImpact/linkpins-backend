using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BackSide2
{
    //[ApiController]
    //[Authorize]
    public class ChatHub : Hub
    {
        private readonly IRepository<Person> _profileRepository;
        private readonly IRepository<ChatMessage> _chatMessageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public static ConcurrentDictionary<string, MyUserType> MyUsers = new ConcurrentDictionary<string, MyUserType>();

        public ChatHub(IRepository<Person> profileRepository, IRepository<ChatMessage> chatMessageRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _profileRepository = profileRepository;
            _chatMessageRepository = chatMessageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync(name, message);
        }

        ////[AllowAnonymous]
        //public override async Task OnConnectedAsync()
        //{
        //    //var identity = Context.User.Identity;

        //    //if (identity.IsAuthenticated)
        //    //{
        //    //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName:(identity.Name)).ConfigureAwait(false);
        //    //}


        //    //var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    //var userId1 = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Email).Value);
        //    //var userId12 = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub).Value);
        //    //var userId1 = _httpContextAccessor.HttpContext.User.Claims.ToList();
        //    var token = Context.Items;
        //    var ConnectionId = Context.ConnectionId;
        //    MyUsers.TryAdd(Context.ConnectionId, new MyUserType() {ConnectionId = Context.ConnectionId});
        //    await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, Context.ConnectionId, "joined");
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception ex)
        //{
        //    MyUserType garbage;

        //    MyUsers.TryRemove(Context.ConnectionId, out garbage);

        //    await Clients.All.SendAsync("SendAction", Context.User.Identity.Name, "left");
        //}

        //[Authorize]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task AddMessage(string message, MessageReceivedContext receivedContext)
        {
            var asd = receivedContext.Request.Headers["Accept"];

            await Clients.All.SendAsync("SendAction", message);
        }

        public async Task AddMessage1(string message, MessageReceivedContext receivedContext)
        {
            var asd = Context;
            await Clients.All.SendAsync("SendAction", message);
        }

        //public async Task Send(string message)
        //{
        //    await Clients.All.SendAsync("SendMessage", Context.User.Identity.Name, message);
        //}
    }

    public class MyUserType
    {
        public string ConnectionId { get; set; }
        // Can have whatever you want here
    }
}