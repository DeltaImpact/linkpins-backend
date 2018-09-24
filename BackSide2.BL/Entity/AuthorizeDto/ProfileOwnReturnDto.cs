using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Entity.AuthorizeDto
{
    public class ProfileOwnReturnDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool? Gender { get; set; }
        public uint? Language { get; set; }
    }
}