using System.Collections.Generic;

namespace Logo.Proje.Domain.Entities
{
    public class Apartment : BaseEntity
    {
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string RoomCount { get; set; }
        public bool IsSomeoneLiving { get; set; }
        public string ResidentId { get; set; } //Identity User uses guid which is string
        public List<Bill> Bills { get; set; }
    }
}
