using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackSide2.BL.Exceptions;
using BackSide2.DAO.Entities;
using BackSide2.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.BL.UsersConnections
{
    public class ConnectionMapping : IConnectionMapping
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ChatConnectedUser> _chatConnectedUsers;

        public ConnectionMapping(IHttpContextAccessor httpContextAccessor,
            IRepository<ChatConnectedUser> chatConnectedUsers)
        {
            _httpContextAccessor = httpContextAccessor;
            _chatConnectedUsers = chatConnectedUsers;
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
                await (await _chatConnectedUsers.GetAllAsync(o => o.ConnectionId == connectionId)).FirstOrDefaultAsync();
            if (connectionInDb == null)
            {
                throw new ObjectNotFoundException("Connection not found.");
            }

            if (connectionInDb.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You have no permissions to delete this pin.");
            }

            await _chatConnectedUsers.RemoveAsync(connectionInDb);
        }

        public async Task<bool> IsOnline(long userId)
        {
            var isOnline = (await (await _chatConnectedUsers.GetAllAsync(o => o.UserId == userId)).AnyAsync());
            return isOnline;
        }
    }
}