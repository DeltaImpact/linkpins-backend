using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BackSide2.DAO.Entities;

namespace BackSide2.BL.Entity.AuthorizeDto
{
    public class BoardReturnDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
    }
}