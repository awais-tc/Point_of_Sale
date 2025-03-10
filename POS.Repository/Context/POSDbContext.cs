using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS.Core.Dtos;
using System.Data;


namespace POS.Repository.Context
{
    public class POSDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public POSDbContext(DbContextOptions<POSDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-J5IS95J\\SQLEXPRESS;Initial Catalog=POS_Updated;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure Username is Unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Ensure RoleName is Unique
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.RoleName)
                .IsUnique();

            // Ensure Barcode Number is Unique
            modelBuilder.Entity<Barcode>()
                .HasIndex(b => b.BarcodeNumber)
                .IsUnique();

            // Many-to-Many Relationship: User <-> Payment
            modelBuilder.Entity<UserPayment>()
                .HasKey(up => new { up.UserId, up.PaymentId });

            modelBuilder.Entity<UserPayment>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPayments)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserPayment>()
                .HasOne(up => up.Payment)
                .WithMany(p => p.UserPayments)
                .HasForeignKey(up => up.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure SaleItem has a required Quantity with a minimum value
            modelBuilder.Entity<SaleItem>()
                .Property(si => si.Quantity)
                .IsRequired()
                .HasDefaultValue(1);
            
            //Ensure Delete Behavior to avoid Sql Errors
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }

}
