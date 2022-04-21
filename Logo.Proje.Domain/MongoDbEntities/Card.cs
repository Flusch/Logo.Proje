using System.ComponentModel;

namespace Logo.Proje.Domain.MongoDbEntities
{
    public class Card : MongoDbBaseEntity
    {
        [DisplayName("Kart İsmi")]
        public string CardName { get; set; }
        [DisplayName("Sahibi")]
        public string OwnerId { get; set; }
        [DisplayName("Kart Üzerindeki İsim")]
        public string OwnerFullName { get; set; }
        [DisplayName("Kart Numarası")]
        public string CardNumber { get; set; }
        [DisplayName("Son Kullanma Tarihi")]
        public string ExpirationDate { get; set; }
        [DisplayName("Güvenlik Kodu")]
        public int CVV { get; set; }
    }
}
