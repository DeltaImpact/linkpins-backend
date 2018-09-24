using System;
using System.Collections.Generic;
using System.Text;

namespace BackSide2.DAO.Entities
{
    public class BoardPin : BaseEntity
    {
        //public int PinId { get; set; }
        public Pin Pin { get; set; }
        //public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
