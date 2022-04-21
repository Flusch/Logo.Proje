using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Logo.Proje.Domain.Entities
{
    public class Bill : BaseEntity
    {
        [DisplayName("Tipi")]
        public string Type { get; set; }
        [DisplayName("Daire Id")]
        public int ApartmentId { get; set; }
        [DisplayName("Miktarı")]
        public float Amount { get; set; }
        [DisplayName("Fatura Kesim Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime BillDate { get; set; }
        [DisplayName("Son Ödeme Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime DueDate { get; set; }
        [DisplayName("Ödenme Durumu")]
        public bool IsPaid { get; set; }
        [DisplayName("Ödenme Tarihi")]
        public DateTime? PaymentDate { get; set; }
        public Apartment Apartment { get; set; }
    }
}
