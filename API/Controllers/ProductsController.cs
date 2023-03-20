using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Controller]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return Ok(await _context.Products.ToListAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return Ok(await _context.Products.FindAsync(id));
    }
}