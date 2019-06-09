using BackSide2.BL.Exceptions;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BackSide2.BL.UsersConnections
{
    public class ConnectionMapping : IConnectionMapping
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ChatConnectedUser> _chatConnectedUsers;
        private readonly IRepository<Person> _personService;


        public ConnectionMapping(IHttpContextAccessor httpContextAccessor,
            IRepository<ChatConnectedUser> chatConnectedUsers,
            IRepository<Person> personService)
        {
            _httpContextAccessor = httpContextAccessor;
            _chatConnectedUsers = chatConnectedUsers;
            _personService = personService;
        }

        public async Task Add(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb = await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId))
                .FirstOrDefaultAsync();
            if (connectionInDb != null) await _chatConnectedUsers.RemoveAsync(connectionInDb);
            //if (connectionInDb) await Remove(connectionId);
            var connection = new ChatConnectedUser
            {
                CreatedBy = userId,
                UserId = userId,
                ConnectionId = connectionId
            };
            await _chatConnectedUsers.InsertAsync(connection);
        }

        public async Task Remove(string connectionId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var connectionInDb =
                await (await _chatConnectedUsers.GetAllAsync(o => o.ConnectionId == connectionId))
                    .FirstOrDefaultAsync();
            if (connectionInDb == null)
            {
                throw new ObjectNotFoundException("Connection not found.");
            }

            if (connectionInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            var user = (await _personService.GetByIdAsync(userId));
            user.LastOnline = DateTime.Now;
            await _personService.UpdateAsync(user);

            await _chatConnectedUsers.RemoveAsync(connectionInDb);
        }

        public async Task<bool> IsOnline(long userId)
        {
            var isOnline = (await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId)).AnyAsync());
            return isOnline;
        }
    }
}