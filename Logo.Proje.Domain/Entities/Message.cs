namespace Logo.Proje.Domain.Entities
{
    public class Message
    {
        public int From { get; set; }
        public int To { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
    }
}
