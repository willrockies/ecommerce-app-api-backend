using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using System.Xml.Linq;

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
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await productsRepo.ListAsync(spec);

        return products.Select(product => new ProductToReturnDto {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            ProductType = product.ProductType.Name,
            ProductBrand = product.ProductBrand.Name,
        }).ToList();
    }

    [HttpGet("/api/Products/{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productsRepo.GetEntityWithSpec(spec);

        return new ProductToReturnDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            ProductType = product.ProductType.Name,
            ProductBrand = product.ProductBrand.Name,

        };
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