using System;
using System.ComponentModel;

namespace Logo.Proje.Domain.MongoDbEntities
{
    public class Payment : MongoDbBaseEntity
    {
        [DisplayName("Kart Id")]
        public int CardId { get; set; }
        [DisplayName("Fatura Id")]
        public int BillId { get; set; }
        [DisplayName("Ödenme Tarihi")]
        public DateTime PaymentDate { get; set; }
    }
}
