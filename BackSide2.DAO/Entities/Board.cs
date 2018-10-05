using System.Collections.Generic;
using System.ComponentModel;

namespace BackSide2.DAO.Entities
{
    public class Board : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        [DefaultValue(false)] public bool IsPrivate { get; set; }

        public Person Person { get; set; }
        public ICollection<BoardPin> BoardPins { get; set; }
    }
}