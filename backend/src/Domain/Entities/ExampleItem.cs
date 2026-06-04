using Domain.Common;

namespace Domain.Entities;

public class ExampleItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}