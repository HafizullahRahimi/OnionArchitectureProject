using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Domain.Products;
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
    public async Task<ActionResult<ProductDto>> Post([FromBody] CreateProductDto productDto)
    {
        var produtDto = await _productService.CreateAsync(productDto, CancellationToken.None);
        return produtDto is null ? BadRequest() : Ok(produtDto);
    }

    // PUT api/Product/5
    [HttpPut("{productId}")]
    public async Task<ActionResult<ProductDto>> Put(Guid productId, [FromBody] CreateProductDto productDto)
    {
        var product = await _productService.ExistAsync(productId, CancellationToken.None);
        if (!product) return NotFound();

        var produtDto = await _productService.UpdateAsync(productDto, CancellationToken.None);
        return Ok(produtDto);

    }

    // DELETE api/Product/5
    [HttpDelete("{productId}")]
    public async Task<ActionResult<ProductDto>> Delete(Guid productId)
    {
        var productDto = await _productService.GetByIdAsync(productId, CancellationToken.None);
        if (productDto is null) return NotFound();

        await _productService.DeleteAsync(productId, CancellationToken.None);
        return Ok(productDto);
    }
}
