using EFCoreExampleForLnL.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExampleForLnL.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address)
            .WithMany()
            .HasForeignKey(c => c.AddressId)
            .IsRequired(false);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CustomerId);

        // Seed Addresses
        modelBuilder.Entity<Address>().HasData(
            new Address { Id = 1, StreetNumber = "123 Main St", City = "New York", State = "NY", ZipCode = "10001" },
            new Address { Id = 2, StreetNumber = "456 Oak Ave", City = "Los Angeles", State = "CA", ZipCode = "90001" }
        );

        // Seed Customers (2 with addresses, 2 without)
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Name = "John Doe", PhoneNumber = "555-0101", Email = "john.doe@email.com", AddressId = 1 },
            new Customer { Id = 2, Name = "Jane Smith", PhoneNumber = "555-0102", Email = "jane.smith@email.com", AddressId = 2 },
            new Customer { Id = 3, Name = "Bob Wilson", PhoneNumber = "555-0103", Email = "bob.wilson@email.com", AddressId = null },
            new Customer { Id = 4, Name = "Alice Brown", PhoneNumber = "555-0104", Email = "alice.brown@email.com", AddressId = null }
        );

        // Seed Products (distributed across customers)
        modelBuilder.Entity<Product>().HasData(
            // John Doe's products (5)
            new Product { Id = 1, ProductGuid = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Wireless Mouse", Description = "Ergonomic wireless mouse with adjustable DPI", CustomerId = 1 },
            new Product { Id = 2, ProductGuid = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"), Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard with Cherry MX switches", CustomerId = 1 },
            new Product { Id = 3, ProductGuid = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-123456789012"), Name = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI and ethernet", CustomerId = 1 },
            new Product { Id = 4, ProductGuid = Guid.Parse("d4e5f6a7-b8c9-0123-def0-234567890123"), Name = "Webcam HD", Description = "1080p HD webcam with built-in microphone", CustomerId = 1 },
            new Product { Id = 5, ProductGuid = Guid.Parse("e5f6a7b8-c9d0-1234-ef01-345678901234"), Name = "Monitor Stand", Description = "Adjustable aluminum monitor stand with USB ports", CustomerId = 1 },

            // Jane Smith's products (5)
            new Product { Id = 6, ProductGuid = Guid.Parse("f6a7b8c9-d0e1-2345-f012-456789012345"), Name = "Laptop Sleeve", Description = "Waterproof neoprene laptop sleeve for 15-inch laptops", CustomerId = 2 },
            new Product { Id = 7, ProductGuid = Guid.Parse("a7b8c9d0-e1f2-3456-0123-567890123456"), Name = "Wireless Earbuds", Description = "True wireless earbuds with active noise cancellation", CustomerId = 2 },
            new Product { Id = 8, ProductGuid = Guid.Parse("b8c9d0e1-f2a3-4567-1234-678901234567"), Name = "Phone Charger", Description = "Fast wireless phone charger with LED indicator", CustomerId = 2 },
            new Product { Id = 9, ProductGuid = Guid.Parse("c9d0e1f2-a3b4-5678-2345-789012345678"), Name = "Cable Organizer", Description = "Magnetic cable organizer clips set of 5", CustomerId = 2 },
            new Product { Id = 10, ProductGuid = Guid.Parse("d0e1f2a3-b4c5-6789-3456-890123456789"), Name = "Desk Lamp", Description = "LED desk lamp with adjustable brightness and color temperature", CustomerId = 2 },

            // Bob Wilson's products (4)
            new Product { Id = 11, ProductGuid = Guid.Parse("e1f2a3b4-c5d6-7890-4567-901234567890"), Name = "External SSD", Description = "1TB portable external SSD with USB 3.2", CustomerId = 3 },
            new Product { Id = 12, ProductGuid = Guid.Parse("f2a3b4c5-d6e7-8901-5678-012345678901"), Name = "Bluetooth Speaker", Description = "Portable Bluetooth speaker with 20-hour battery", CustomerId = 3 },
            new Product { Id = 13, ProductGuid = Guid.Parse("a3b4c5d6-e7f8-9012-6789-123456789012"), Name = "Mouse Pad XL", Description = "Extended gaming mouse pad with stitched edges", CustomerId = 3 },
            new Product { Id = 14, ProductGuid = Guid.Parse("b4c5d6e7-f8a9-0123-7890-234567890123"), Name = "Headphone Stand", Description = "Wooden headphone stand with cable hook", CustomerId = 3 },

            // Alice Brown's products (4)
            new Product { Id = 15, ProductGuid = Guid.Parse("c5d6e7f8-a9b0-1234-8901-345678901234"), Name = "Screen Cleaner", Description = "Microfiber screen cleaning kit for electronics", CustomerId = 4 },
            new Product { Id = 16, ProductGuid = Guid.Parse("d6e7f8a9-b0c1-2345-9012-456789012345"), Name = "Wrist Rest", Description = "Memory foam keyboard wrist rest", CustomerId = 4 },
            new Product { Id = 17, ProductGuid = Guid.Parse("e7f8a9b0-c1d2-3456-0123-567890123456"), Name = "Privacy Screen", Description = "14-inch laptop privacy screen filter", CustomerId = 4 },
            new Product { Id = 18, ProductGuid = Guid.Parse("f8a9b0c1-d2e3-4567-1234-678901234567"), Name = "Laptop Cooling Pad", Description = "Laptop cooling pad with 5 quiet fans", CustomerId = 4 }
        );
    }
}
