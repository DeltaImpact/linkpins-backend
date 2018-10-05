using System.Collections.Generic;
using System.ComponentModel;

namespace BackSide2.DAO.Entities
{
    public class Pin : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public ICollection<BoardPin> BoardPins { get; set; }
    }
}