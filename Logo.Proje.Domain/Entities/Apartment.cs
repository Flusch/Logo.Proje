namespace Logo.Proje.Domain.Entities
{
    public class Apartment : BaseEntity
    {
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string RoomCount { get; set; }
        public bool isSomeoneLiving { get; set; }
        public int residentId { get; set; }
    }
}
