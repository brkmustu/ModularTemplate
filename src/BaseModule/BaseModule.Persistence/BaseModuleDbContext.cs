using Microsoft.EntityFrameworkCore;
using BaseModule.Common;
using CoreModule.Domain;
using CoreModule.Domain.Permissions;
using CoreModule.Domain.Roles;
using CoreModule.Domain.Users;
using CoreModule.Application.Common.Interfaces;
using CoreModule.Common;

namespace BaseModule;

public class BaseModuleDbContext : DbContext, IBaseModuleDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public BaseModuleDbContext(DbContextOptions<BaseModuleDbContext> options) : base(options)
    {
    }

    public BaseModuleDbContext(
        DbContextOptions<BaseModuleDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime)
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId.HasValue ? _currentUserService.UserId.Value : Guid.Empty;
                    entry.Entity.CreatedDate = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModifiedDate = _dateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseModuleDbContext).Assembly);
    }
}
