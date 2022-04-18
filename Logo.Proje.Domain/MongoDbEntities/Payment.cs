using System;

namespace Logo.Proje.Domain.MongoDbEntities
{
    public class Payment : MongoDbBaseEntity
    {
        public int CardId { get; set; }
        public int BillId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
