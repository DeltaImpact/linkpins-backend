using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackSide2.DAO.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}