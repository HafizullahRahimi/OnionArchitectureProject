using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Domain.Categories;
using OnionArchitectureProject.Persistence.Models.Base.Repositories;

namespace OnionArchitectureProject.Persistence.Models.Categories;

public class CategoryRepositoryWithFilter : Repository<Category>, ICategoryRepositoryWithFilter
{
    private IQueryable<Category> query = Enumerable.Empty<Category>().AsQueryable();
    private string? name;
    private string? createdBy;
    private DateTime? createdAt;
    private string? modifiedBy;
    private DateTime? modifiedAt;

    protected CategoryRepositoryWithFilter(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory) { }

    public ICategoryRepositoryWithFilter WithName(string name)
    {
        this.name = name;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedBy(string userId)
    {
        createdBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithCreatedAt(DateTime date)
    {
        createdAt = date;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedBy(string userId)
    {
        modifiedBy = userId;
        return this;
    }

    public ICategoryRepositoryWithFilter WithModifiedAt(DateTime date)
    {
        modifiedAt = date;
        return this;
    }

    public async Task<List<Category>> ToListAsync(CancellationToken cancellationToken)
    {
        var filter = new CategoryFilter(name, createdBy, createdAt, modifiedBy, modifiedAt);
        return await GetAllAsync(filter, cancellationToken);

        //var categories = await GetAllAsync(cancellationToken);
        //query = categories.AsQueryable();

        //if (!string.IsNullOrWhiteSpace(name))
        //{
        //    query = query.Where(c => c.Name == name);
        //}

        //if (!string.IsNullOrWhiteSpace(createdBy))
        //{
        //    query = query.Where(c => c.CreatedBy == createdBy);
        //}

        //if (!string.IsNullOrWhiteSpace(modifiedBy))
        //{
        //    query = query.Where(c => c.ModifiedBy == modifiedBy);
        //}

        //if (createdAt != null)
        //{
        //    query = query.Where(c => c.CreatedDateUtc == createdAt);
        //}

        //if (modifiedAt != null)
        //{
        //    query = query.Where(c => c.ModifiedDateUtc == modifiedAt);
        //}

        //return query.ToList();
    }
}
