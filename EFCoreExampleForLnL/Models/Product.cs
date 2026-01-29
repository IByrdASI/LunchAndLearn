namespace EFCoreExampleForLnL.Models;

public class Product
{
    public int Id { get; set; }
    public Guid ProductGuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
