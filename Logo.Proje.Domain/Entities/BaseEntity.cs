using System;
using System.ComponentModel;

namespace Logo.Proje.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Oluşturan")]
        public string CreatedBy { get; set; }
        [DisplayName("Son Düzenleme")]
        public DateTime? LastUpdatedAt { get; set; }
        [DisplayName("Son Düzenleyen")]
        public string LastUpdatedBy { get; set; }
    }
}