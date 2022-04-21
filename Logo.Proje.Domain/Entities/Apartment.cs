using System.Collections.Generic;
using System.ComponentModel;

namespace Logo.Proje.Domain.Entities
{
    public class Apartment : BaseEntity
    {
        [DisplayName("Blok")]
        public int Block { get; set; }
        [DisplayName("Kat")]
        public int Floor { get; set; }
        [DisplayName("Numara")]
        public int Number { get; set; }
        [DisplayName("Oda Sayısı")]
        public string RoomCount { get; set; }
        [DisplayName("Dolu Mu?")]
        public bool IsSomeoneLiving { get; set; }
        [DisplayName("İkamet Eden")]
        public string ResidentId { get; set; } //Identity User uses guid which is string
        public List<Bill> Bills { get; set; }
    }
}
