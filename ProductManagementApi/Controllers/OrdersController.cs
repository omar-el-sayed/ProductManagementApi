namespace ProductManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _context.Orders.ToListAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] OrderDto dto)
    {
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            OrderDate = dto.OrderDate,
            TotalAmount = dto.TotalAmount
        };

        await _context.AddAsync(order);
        await _context.SaveChangesAsync();

        return Created("", order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] OrderDto dto)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order is null)
            return NotFound($"No order was found with ID: {id}");

        order.CustomerId = dto.CustomerId;
        order.OrderDate = dto.OrderDate;
        order.TotalAmount = dto.TotalAmount;

        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order is null)
            return NotFound($"No order was found with ID: {id}");

        _context.Remove(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }
}
