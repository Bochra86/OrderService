using Microsoft.AspNetCore.Mvc;

using OrderService.Application.DTOs;
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
    public async Task<IActionResult> Create([FromBody] RequestOrderDto dto)
    {
        var id = await _handler.HandleAsync(new CreateOrderCommand(dto.Total));

        return Ok(new { OrderId = id });
    }
}