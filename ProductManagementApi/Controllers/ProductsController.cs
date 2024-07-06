namespace ProductManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductDto dto)
    {
        Product product = new()
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            OrderId = dto.OrderId
        };

        await _context.AddAsync(product);
        await _context.SaveChangesAsync();

        return Created("", product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return NotFound($"No product was found with ID: {id}");

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.OrderId = dto.OrderId;

        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return NotFound($"No product was found with ID: {id}");

        _context.Remove(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
