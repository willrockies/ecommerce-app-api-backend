using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using System.Xml.Linq;
using AutoMapper;
using API.Errors;
using API.Helpers;

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
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        [FromQuery]ProductSpecParams productParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

        var countSpec = new ProductWithFiltersForCounterSpecification(productParams);

        var totalItems = await productsRepo.CountAsync(countSpec);

        var products = await productsRepo.ListAsync(spec);

        var data = mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        
        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, 
            productParams.PageSize, totalItems, data));
    }

    [HttpGet("/api/Products/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productsRepo.GetEntityWithSpec(spec);
        if (product is null) return NotFound(new ApiResponse(404));
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