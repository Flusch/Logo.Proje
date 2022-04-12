namespace Logo.Proje.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string From { get; set; } //Identity User uses guid which is string
        public string To { get; set; } //Identity User uses guid which is string
        public string Text { get; set; }
        public bool IsRead { get; set; }
    }
}
