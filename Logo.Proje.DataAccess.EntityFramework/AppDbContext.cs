using Logo.Proje.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logo.Proje.DataAccess.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartments").HasKey(x => x.Id);
            });
            builder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bills").HasKey(x => x.Id);
                entity.HasOne(x => x.Apartment).WithMany(x => x.Bills).HasForeignKey(x => x.ApartmentId);
            });
            builder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages").HasKey(x => x.Id);
            });
        }
    }
}
