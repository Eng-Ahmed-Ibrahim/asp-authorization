using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ProductsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetProducts(

             int page = 1,
        int pageSize = 10
    )
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 10, 100);
        var query = context.Products.AsNoTracking();
        var totalItems = await query.CountAsync();

        var products = await query.OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
        var totalPages = (int)Math.Ceiling(
            totalItems / (double)pageSize
        );
        var data =
        new
        {
            Count = totalItems,
            page = page,
            pageSize = pageSize,
            totalPages=totalPages,
            Products = products,

        };
        return Ok(data);
    }
}