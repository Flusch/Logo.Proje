namespace Logo.Proje.Domain.MongoDbEntities
{
    public class Card : MongoDbBaseEntity
    {
        public string CardName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerFullName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
