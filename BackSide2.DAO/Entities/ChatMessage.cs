namespace BackSide2.DAO.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string MessageContent { get; set; }
        public Person ReceivedBy { get; set; }
        public bool Received { get; set; } = false;
    }
}