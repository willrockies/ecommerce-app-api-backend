using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsControllers : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsControllers(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("/api/Products")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _repo.GetProductsAsync();

        return Ok(products);
    }

    [HttpGet("/api/Products/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _repo.GetProductByIdAsync(id);
    }

    [HttpGet("/api/Products/brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _repo.GetProductBrandsAsync());
    }

    [HttpGet("/api/Products/types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _repo.GetProductTypesAsync());
    }
}