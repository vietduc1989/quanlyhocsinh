using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ExampleItems.Queries;

public record ExampleItemDto(Guid Id, string Name, string Description, bool IsCompleted);

public record GetExampleItemsQuery : IRequest<IEnumerable<ExampleItemDto>>;

public class GetExampleItemsQueryHandler : IRequestHandler<GetExampleItemsQuery, IEnumerable<ExampleItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetExampleItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExampleItemDto>> Handle(GetExampleItemsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ExampleItems
            .Select(item => new ExampleItemDto(item.Id, item.Name, item.Description, item.IsCompleted))
            .ToListAsync(cancellationToken);
    }
}