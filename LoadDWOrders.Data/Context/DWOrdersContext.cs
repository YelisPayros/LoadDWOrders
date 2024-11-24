using LoadDWOrders.Data.Entities.DWOrders;
using Microsoft.EntityFrameworkCore;

namespace LoadDWOrders.Data.Context
{
    public partial class DWOrdersContext : DbContext
    {

        public DWOrdersContext(DbContextOptions<DWOrdersContext> options) : base(options)
        {
        }
        public DbSet<DimCustomer> DimCustomers { get; set; }
        public DbSet<DimEmployee> DimEmployees { get; set; }
        public DbSet<DimShipper> DimShippers { get; set; }
        public DbSet<DimCategory> DimCategories { get; set; }
        public DbSet<DimProduct> DimProducts { get; set; }
        public DbSet<FactSales> FactSales { get; set; }
        public DbSet<FactCustomersAtended> FactCustomersAtendeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DimProduct>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // ...existing code...
        }
    }
}
