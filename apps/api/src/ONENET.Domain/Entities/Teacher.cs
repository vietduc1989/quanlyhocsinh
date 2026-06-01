// QUAN-20260530-2302
// Assume Teacher.cs already exists, adding ICollection<Class> for bidirectional navigation
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities
{
    public class Teacher : BaseAuditableEntity
    {
        public string FullName { get; private set; }
        // ... other teacher properties ...

        // Navigation property for classes where this teacher is homeroom teacher
        public ICollection<Class> HomeroomClasses { get; private set; } = new HashSet<Class>();

        private Teacher() { }

        // Assuming existing factory/update methods. Adding a constructor for example.
        public Teacher(string fullName, string createdBy)
        {
            FullName = fullName;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        // ... existing methods to update teacher details ...
    }
}