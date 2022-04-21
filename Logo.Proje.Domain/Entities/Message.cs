namespace Logo.Proje.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
    }
}
