using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Controller]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductBrand> _productBrandRepository;
    private readonly IGenericRepository<ProductType> _productTypeRepository;

    public ProductsController
    (
        IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository
    )
    {
        _productRepository = productRepository;
        _productBrandRepository = productBrandRepository;
        _productTypeRepository = productTypeRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return Ok(await _productRepository.ListAllAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return Ok(await _productRepository.GetByIdAsync(id));
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductBrands()
    {
        return Ok(await _productBrandRepository.ListAllAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
    {
        return Ok(await _productTypeRepository.ListAllAsync());
    }
}