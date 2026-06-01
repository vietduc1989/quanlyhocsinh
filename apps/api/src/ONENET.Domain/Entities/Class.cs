// QUAN-20260530-2302
using ONENET.Domain.Common;
using ONENET.Domain.Enums; // Assuming Enums for potential future extensions, though not strictly required by current BRD

namespace ONENET.Domain.Entities
{
    public class Class : BaseAuditableEntity
    {
        public string ClassCode { get; private set; }
        public string ClassName { get; private set; }
        public string SchoolYear { get; private set; } // Format YYYY or YYYY-YYYY
        public Guid? HomeroomTeacherId { get; private set; } // Nullable FK to Teacher
        public byte[] Version { get; private set; } // For optimistic concurrency

        // Navigation properties
        public Teacher? HomeroomTeacher { get; private set; }
        public ICollection<Student> Students { get; private set; } = new HashSet<Student>();

        // Private constructor for EF Core and internal creation
        private Class() { }

        // Factory method to create a new Class
        public static Class Create(string classCode, string className, string schoolYear, Guid? homeroomTeacherId, string createdBy)
        {
            var newClass = new Class
            {
                ClassCode = classCode,
                ClassName = className,
                SchoolYear = schoolYear,
                HomeroomTeacherId = homeroomTeacherId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };
            // Domain events could be added here if needed, e.g., newClass.AddDomainEvent(new ClassCreatedEvent(newClass));
            return newClass;
        }

        // Method to update class information
        public void Update(string className, string schoolYear, Guid? homeroomTeacherId, string updatedBy)
        {
            ClassName = className;
            SchoolYear = schoolYear;
            HomeroomTeacherId = homeroomTeacherId;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
            // Domain events could be added here, e.g., this.AddDomainEvent(new ClassUpdatedEvent(this));
        }

        // Method to update homeroom teacher specifically (could be part of Update, but useful for clarity)
        public void SetHomeroomTeacher(Guid? homeroomTeacherId, string updatedBy)
        {
            HomeroomTeacherId = homeroomTeacherId;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}