using System.ComponentModel;

namespace Logo.Proje.Domain.Entities
{
    public class Message : BaseEntity
    {
        [DisplayName("Kimden")]
        public string From { get; set; }
        [DisplayName("Kime")]
        public string To { get; set; }
        [DisplayName("Mesaj")]
        public string Text { get; set; }
        [DisplayName("Okundu mu?")]
        public bool IsRead { get; set; }
    }
}
