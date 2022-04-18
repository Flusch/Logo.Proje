using System;
using System.ComponentModel.DataAnnotations;

namespace Logo.Proje.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public string Type { get; set; }
        public int ApartmentId { get; set; }
        public float Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BillDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Apartment Apartment { get; set; }
    }
}
