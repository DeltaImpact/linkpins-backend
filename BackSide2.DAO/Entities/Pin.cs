using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.DAO.Entities
{
    public class Pin : BaseEntity
    {
        //public long BoardId { get; set; }
        public string Name { get; set; }
        [DefaultValue(null)] public string Description { get; set; }
        [DefaultValue(null)] public string Img { get; set; }
        [DefaultValue(null)] public string Link { get; set; }
        public ICollection<BoardPin> BoardPins { get; set; }
    }
}