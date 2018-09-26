using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BackSide2.DAO.Entities
{
    public class Board : BaseEntity
    {
        //public long UserId { get; set; }
        public string Name { get; set; }
        [DefaultValue(null)] public string Description { get; set; }
        [DefaultValue(null)] public string Img { get; set; }
        [DefaultValue(false)] public bool IsPrivate { get; set; }

        public Person Person { get; set; }
        public ICollection<BoardPin> BoardPins { get; set; }
        //public ICollection<Pin> Pins { get; set; }
    }
}