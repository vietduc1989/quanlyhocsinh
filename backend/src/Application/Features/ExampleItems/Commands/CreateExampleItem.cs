using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ExampleItems.Commands;

public record CreateExampleItemCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public class CreateExampleItemCommandHandler : IRequestHandler<CreateExampleItemCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateExampleItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateExampleItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExampleItem
        {
            Name = request.Name,
            Description = request.Description,
            IsCompleted = false
        };

        _context.ExampleItems.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}