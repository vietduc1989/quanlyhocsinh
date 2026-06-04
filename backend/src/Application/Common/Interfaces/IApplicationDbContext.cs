using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ExampleItem> ExampleItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}