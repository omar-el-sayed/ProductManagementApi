namespace ProductManagementApi.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Range(0, double.PositiveInfinity, ErrorMessage = "The Price cannot be negative.")]
    public double Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "The Stock cannot be negative.")]
    public int Stock { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
