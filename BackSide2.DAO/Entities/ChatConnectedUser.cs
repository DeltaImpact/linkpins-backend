using System;
using System.Collections.Generic;
using System.Text;

namespace BackSide2.DAO.Entities
{
    public class ChatConnectedUser : BaseEntity
    {
        public long UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}


