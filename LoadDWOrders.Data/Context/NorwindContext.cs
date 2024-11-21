
using LoadDWOrders.Data.Entities.NorWind;
using Microsoft.EntityFrameworkCore;

namespace LoadDWOrders.Data.Context
{
    public partial class NorwindContext : DbContext
    {

        public NorwindContext(DbContextOptions<NorwindContext> options) : base(options)
        {
        }

        //DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<VwFactSale> VwFactSales { get; set; }

        public DbSet<VwFactCustomersAtended> VwFactCustomersAtendeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwFactSale>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vwFactSales", "DWH");

                entity.Property(e => e.City).HasMaxLength(15);
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("CustomerID");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            });

            modelBuilder.Entity<VwFactCustomersAtended>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_FactCustomersAtended", "DWH");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(31);
            });

        }
    }
}
