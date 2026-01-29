namespace EFCoreExampleForLnL.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int? AddressId { get; set; }
    public Address? Address { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
