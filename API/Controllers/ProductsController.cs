using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductWithTypeAndBrandsSpecification();
        var products = await _productRepository.ListAsync(spec);
        
        return products.Select(product => new ProductToReturnDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductType = product.ProductType.Name,
            ProductBrand = product.ProductBrand.Name,
        }).ToList();
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductWithTypeAndBrandsSpecification(id);
        var product = await _productRepository.GetEntityWithSpec(spec);
        return new ProductToReturnDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductType = product.ProductType.Name,
            ProductBrand = product.ProductBrand.Name,
        };
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