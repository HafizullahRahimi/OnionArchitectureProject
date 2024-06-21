using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Domain.Entities;
using OnionArchitectureProject.Application.Contracts.Persistence.IRepositories;
using OnionArchitectureProject.Application.Services.ProductService;
using OnionArchitectureProject.Application.Services.ProductService.Models;


namespace OnionArchitectureProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public ProductController(IProductRepository productRepository, IProductService productService)
    {
        _productRepository = productRepository;
        _productService = productService;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> Get()
    {
        var productDtos = await _productService.GetAllAsync(CancellationToken.None);
        return Ok(productDtos);
    }

    // GET api/Product/5
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> Get(Guid productId)
    {
        var productDto = await _productService.GetByIdAsync(productId, CancellationToken.None);
        if (productDto is null) return NotFound();
        return Ok(productDto);
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
        await _productRepository.CreateAsync(product, CancellationToken.None);
        return Ok(product.Id);
    }

    // PUT api/Product/5
    [HttpPut("{productId}")]
    public async Task<IActionResult> Put(Guid productId, [FromBody] ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(productId, CancellationToken.None);

        if (product is null) return NotFound();

        product.Name = productDto.Name;
        product.Image = productDto.Image;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;

        await _productRepository.UpdateAsync(product, CancellationToken.None);
        return NoContent();
    }

    // DELETE api/Product/5
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var product = await _productRepository.ExistAsync(productId, CancellationToken.None);

        if (!product)
            return BadRequest();

        await _productRepository.DeleteAsync(productId, CancellationToken.None);
        return Ok();
    }
}
