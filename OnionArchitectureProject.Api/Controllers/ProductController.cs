using Microsoft.AspNetCore.Mvc;
using OnionArchitectureProject.Application.Services.CategoryService;
using OnionArchitectureProject.Application.Services.ProductService;
using OnionArchitectureProject.Application.Services.ProductService.Models;


namespace OnionArchitectureProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
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
        if (productDto is null) return NotFound("Product not found");
        return Ok(productDto);
    }

    // POST api/Product
    [HttpPost]
    public async Task<ActionResult<Guid>> Post([FromBody] UpsertProductDto productDto)
    {
        var existCategory = await _categoryService.ExistAsync(productDto.CategoryId, CancellationToken.None);
        if (!existCategory) return NotFound("Category not found");

        var productId = await _productService.CreateAsync(productDto, CancellationToken.None);
        return Ok($"Added product with id: {productId}");
    }

    // PUT api/Product/5
    [HttpPut("{productId}")]
    public async Task<ActionResult> Put(Guid productId, [FromBody] UpsertProductDto productDto)
    {
        var existProduct = await _productService.ExistAsync(productId, CancellationToken.None);
        if (!existProduct) return NotFound("Product not found");

        var existCategory = await _categoryService.ExistAsync(productDto.CategoryId, CancellationToken.None);
        if (!existCategory) return NotFound("Category not found");

        await _productService.UpdateAsync(productId, productDto, CancellationToken.None);
        return Ok($"Updated product with id: {productId}");

    }

    // DELETE api/Product/5
    [HttpDelete("{productId}")]
    public async Task<ActionResult> Delete(Guid productId)
    {
        var productDto = await _productService.GetByIdAsync(productId, CancellationToken.None);
        if (productDto is null) return NotFound("Product not found");

        await _productService.DeleteAsync(productId, CancellationToken.None);
        return Ok("Product deleted");
    }
}
