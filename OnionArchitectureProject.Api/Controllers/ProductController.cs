using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Api.Dto;
using OnionArchitectureProject.Persistence;

namespace OnionArchitectureProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ProductController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> Get()
    {
        var products = await _dbContext.Products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            Price = p.Price,
            CategoryId = p.CategoryId,

            IsDeleted = p.IsDeleted,
            DeletedDateUTC = p.DeletedDateUTC,

            CreatedBy = p.CreatedBy,
            CreatedDateUTC = p.CreatedDateUTC,

            ModifiedBy = p.ModifiedBy,
            ModifiedDateUTC = p.ModifiedDateUTC,

        }).ToListAsync();
        return products;
    }

    // GET api/<ProductController>/5
    [HttpGet("{productId}")]
    public async Task<ActionResult<Product>> Get(Guid productId)
    {
        var product = await _dbContext.Products.AsNoTracking().Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Image = p.Image,
            Price = p.Price,
            CategoryId = p.CategoryId,

            IsDeleted = p.IsDeleted,
            DeletedDateUTC = p.DeletedDateUTC,

            CreatedBy = p.CreatedBy,
            CreatedDateUTC = p.CreatedDateUTC,

            ModifiedBy = p.ModifiedBy,
            ModifiedDateUTC = p.ModifiedDateUTC,

        }).SingleOrDefaultAsync(p => p.Id == productId);

        if (product is null) return NotFound();
        return Ok(product);
    }

    // POST api/<ProductController>
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
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return Ok(product.Id);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{productId}")]
    public async Task<IActionResult> Put(Guid productId, [FromBody] ProductDto productDto)
    {
        var product = await _dbContext.Products.FindAsync(productId);

        if (product is null) return NotFound();

        product.Name = productDto.Name;
        product.Image = productDto.Image;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var product = _dbContext.Products.AsNoTracking().SingleOrDefault(_ => _.Id == productId);

        if (product is null)
            return BadRequest();

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
