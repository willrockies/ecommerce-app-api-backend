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
    
    private readonly IGenericRepository<Product> productsRepo;
    private readonly IGenericRepository<ProductBrand> productBrandRepo;
    private readonly IGenericRepository<ProductType> productTypeRepo;

    public ProductsControllers(
        IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand>productBrandRepo,
        IGenericRepository<ProductType>productTypeRepo
        )
    {
        this.productsRepo = productsRepo;
        this.productBrandRepo = productBrandRepo;
        this.productTypeRepo = productTypeRepo;
    }

    [HttpGet("/api/Products")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await productsRepo.ListAllAsync();

        return Ok(products);
    }

    [HttpGet("/api/Products/{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await productsRepo.GetByIdAsync(id);
    }

    [HttpGet("/api/Products/brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await productBrandRepo.ListAllAsync());
    }

    [HttpGet("/api/Products/types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await productTypeRepo.ListAllAsync());
    }
}