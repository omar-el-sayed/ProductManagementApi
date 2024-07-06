namespace ProductManagementApi.Models;

public class Order
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public double TotalAmount { get; set; }
    public ICollection<Product> Products { get; set; }
}
