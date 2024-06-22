using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Categories;
using OnionArchitectureProject.Api.Dto;
using OnionArchitectureProject.Persistence;

namespace OnionArchitectureProject.Api.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET api/category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> Get()
    {
        var Categories = await _dbContext.Categories.Select(p => new CategoryResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            IsDeleted = p.IsDeleted,
        }).ToListAsync();
        return Categories;
    }

    // GET api/category/5
    [HttpGet("{categoryId}")]
    public async Task<ActionResult<CategoryResponseDto>> Get(Guid categoryId)
    {
        var category = await _dbContext.Categories.AsNoTracking().Select(p => new CategoryResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            IsDeleted = p.IsDeleted,
        }).SingleOrDefaultAsync(p => p.Id == categoryId);

        if (category is null) return NotFound();
        return Ok(category);
    }

    // POST api/category
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name
        };
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return Ok(category.Id);
    }

    // PUT api/category/5
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Put(Guid categoryId, [FromBody] CategoryDto categoryDto)
    {
        var category = await _dbContext.Categories.FindAsync(categoryId);

        if (category is null) return NotFound();

        category.Name = categoryDto.Name;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/category/5
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(Guid categoryId)
    {
        var category = _dbContext.Categories.AsNoTracking().SingleOrDefault(_ => _.Id == categoryId);

        if (category is null)
            return BadRequest();

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
