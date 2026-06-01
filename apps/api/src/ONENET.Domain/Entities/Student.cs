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

        // ... existing methods to update student details ...
    }
}