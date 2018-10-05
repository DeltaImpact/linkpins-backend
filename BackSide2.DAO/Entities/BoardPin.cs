namespace BackSide2.DAO.Entities
{
    public class BoardPin : BaseEntity
    {
        public Pin Pin { get; set; }
        public Board Board { get; set; }
    }
}
