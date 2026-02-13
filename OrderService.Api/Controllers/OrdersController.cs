using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _handler;

    public OrdersController(CreateOrderHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(decimal total)
    {
        var id = await _handler.Handle(new CreateOrderCommand(total));
        return Ok(new { OrderId = id });
    }
}
