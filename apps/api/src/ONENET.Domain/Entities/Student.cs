// QUAN-20260530-2302
// Assume Student.cs already exists, adding ClassId and Class navigation property
using ONENET.Domain.Common;

namespace ONENET.Domain.Entities
{
    public class Student : BaseAuditableEntity
    {
        public string FullName { get; private set; }
        // ... other student properties ...

        public Guid? ClassId { get; private set; } // Foreign key to Class (nullable)

        // Navigation property
        public Class? Class { get; private set; }

        private Student() { }

        // Assuming existing factory/update methods. Adding a constructor for example.
        public Student(string fullName, Guid? classId, string createdBy)
        {
            FullName = fullName;
            ClassId = classId;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }

        public void AssignClass(Guid? classId, string updatedBy)
        {
            ClassId = classId;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // ... existing methods to update student details ...`r`n// QUAN-20260531-154643
namespace ONENET.Domain.Entities
{
    // Placeholder entity for Student, assumed to exist externally.
    // Inherits BaseEntity to conform to system conventions and allow FK relations.
    public class Student : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        // Navigation property for Scores (optional, but good for completeness)
        // public ICollection<Score> Scores { get; private set; } = new List<Score>();
    }
}