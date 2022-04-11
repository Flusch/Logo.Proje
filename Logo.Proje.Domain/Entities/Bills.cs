using System;

namespace Logo.Proje.Domain.Entities
{
    public class Bills : BaseEntity
    {
        public string Type { get; set; }
        public int ApartmentId { get; set; }
        public float Amount { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
