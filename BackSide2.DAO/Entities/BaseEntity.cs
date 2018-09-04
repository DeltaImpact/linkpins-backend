using System;
using System.ComponentModel;

namespace BackSide2.DAO.Entities
{
    public class BaseEntity
    {
        
        public long Id { get; set; }
        public DateTime Created { get; set; }
        [DefaultValue(null)]
        public DateTime? Modified { get; set; }
        [DefaultValue(null)]
        public long? CreatedBy { get; set; }
        [DefaultValue(null)]
        public long? UpdatedBy { get; set; }
    }
}
