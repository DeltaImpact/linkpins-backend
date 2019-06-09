﻿using System;

namespace BackSide2.BL.Models.ProfileDto
{
    public class ProfileReturnDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool? Gender { get; set; }
        public uint? Language { get; set; }
        public bool IsOnline { get; set; }
        public long Id { get; set; }
        public DateTime? LastOnline { get; set; }
    }
}