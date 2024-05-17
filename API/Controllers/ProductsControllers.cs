using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using System.Xml.Linq;
using AutoMapper;

namespace API.Controllers;


public class ProductsControllers : BaseApiController
{
    
    private readonly IGenericRepository<Product> productsRepo;
    private readonly IGenericRepository<ProductBrand> productBrandRepo;
    private readonly IGenericRepository<ProductType> productTypeRepo;
    private readonly IMapper mapper;

    public ProductsControllers(
        IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand>productBrandRepo,
        IGenericRepository<ProductType>productTypeRepo,
        IMapper mapper
        )
    {
        this.productsRepo = productsRepo;
        this.productBrandRepo = productBrandRepo;
        this.productTypeRepo = productTypeRepo;
        this.mapper = mapper;
    }

    [HttpGet("/api/Products")]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await productsRepo.ListAsync(spec);

        return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("/api/Products/{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productsRepo.GetEntityWithSpec(spec);

        return mapper.Map<Product, ProductToReturnDto>(product);
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