using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BackSide2.DAO.Entities
{
    public class Person : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool? Gender { get; set; }
        public uint? Language { get; set; }
        public DateTime? LastOnline { get; set; }
        public ICollection<Board> Boards { get; set; }
    }

    public enum Role
    {
        Admin = 1,
        User = 2
    }
}