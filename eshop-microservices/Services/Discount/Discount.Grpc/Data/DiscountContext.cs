using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, Amount = 100, Description = "I Phone X", ProductName = "I Phone X" },
                new Coupon { Id = 2, Amount = 101, Description = "I Phone XI", ProductName = "I Phone XI" }
            );
        }
    }
}
