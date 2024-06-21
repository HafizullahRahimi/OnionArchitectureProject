using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Api.Dto;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
using OnionArchitectureProject.Persistence;

namespace OnionArchitectureProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController( IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> Get()
    {
        var products = await _productRepository.GetByAllWithCategoryAsync(CancellationToken.None);

        var productResponseDtos = products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,

            IsDeleted = p.IsDeleted,

            CreatedBy = p.CreatedBy,
            CreatedDateUTC = p.CreatedDateUTC,

            ModifiedBy = p.ModifiedBy,
            ModifiedDateUTC = p.ModifiedDateUTC,

        }).ToList();
        return productResponseDtos;
    }

    // GET api/Product/5
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductResponseDto>> Get(Guid productId)
    {
        var p = await _productRepository.GetByIdWithCategoryAsync(productId, CancellationToken.None);
        var productResponseDto = new ProductResponseDto()
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name,

            IsDeleted = p.IsDeleted,

            CreatedBy = p.CreatedBy,
            CreatedDateUTC = p.CreatedDateUTC,

            ModifiedBy = p.ModifiedBy,
            ModifiedDateUTC = p.ModifiedDateUTC,
        };

        if (productResponseDto is null) return NotFound();
        return Ok(productResponseDto);
    }

    // POST api/Product
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Image = productDto.Image,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
        };
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        return Ok(product.Id);
    }

    // PUT api/Product/5
    [HttpPut("{productId}")]
    public async Task<IActionResult> Put(Guid productId, [FromBody] ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null) return NotFound();

        product.Name = productDto.Name;
        product.Image = productDto.Image;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/Product/5
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var product = await _productRepository.ExistAsync(productId);

        if (!product)
            return BadRequest();

        _productRepository.RemoveById(productId);
        await _productRepository.SaveChangesAsync();

        return Ok();
    }
}
