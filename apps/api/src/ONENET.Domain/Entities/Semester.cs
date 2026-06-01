using ONENET.Domain.Common;
namespace ONENET.Domain.Entities
{
    // Placeholder entity for Semester, assumed to exist externally.
    // Inherits BaseEntity to conform to system conventions and allow FK relations.
    public class Semester : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;

        // Navigation property for Scores (optional, but good for completeness)
        // public ICollection<Score> Scores { get; private set; } = new List<Score>();
    }
}