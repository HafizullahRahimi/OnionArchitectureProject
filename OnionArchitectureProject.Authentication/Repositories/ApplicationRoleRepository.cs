using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Repositories.Common;
using OnionArchitectureProject.Domain.Authentication.ApplicationRole;

namespace OnionArchitectureProject.Authentication.Repositories;
public class ApplicationRoleRepository : AuthenticationRepository<ApplicationRole>, IApplicationRoleRepository
{
    public ApplicationRoleRepository(IDbContextFactory<AuthenticationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}