// QUAN-20260531-154643
namespace ONENET.Domain.Entities
{
    // Placeholder entity for Subject, assumed to exist externally.
    // Inherits BaseEntity to conform to system conventions and allow FK relations.
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        // Navigation property for Scores (optional, but good for completeness)
        // public ICollection<Score> Scores { get; private set; } = new List<Score>();
    }
}