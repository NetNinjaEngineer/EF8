using CA.ValueObjectsUsingComplexTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace CA.ValueObjectsUsingComplexTypes.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.ComplexProperty(e => e.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(100);
                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasMaxLength(50);
                address.Property(a => a.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasMaxLength(20);
                address.Property(a => a.Country)
                    .HasColumnName("Country")
                    .HasMaxLength(50);
            });
        });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=.\\SQLEXPRESS;Database=CA.ValueObjectsUsingComplexTypes;TrustServerCertificate=True;Integrated Security= True;MultipleActiveResultSets=true";

        optionsBuilder.UseSqlServer(connectionString);

    }
}