using ONENET.Domain.Common;

namespace ONENET.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public int Credits { get; private set; }

        // EF Core constructor
        private Subject() { }

        // Factory method for creation
        public static Subject Create(string code, string name, string? description, int credits, string createdBy)
        {
            var subject = new Subject
            {
                Code = code,
                Name = name,
                Description = description,
                Credits = credits,
                CreatedBy = createdBy,
                IsDeleted = false // Mặc định là hoạt động
            };
            return subject;
        }

        // Method for updating
        public void Update(string name, string? description, int credits, bool isActive, string updatedBy)
        {
            Name = name;
            Description = description;
            Credits = credits;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // Method for soft deleting (setting IsActive to false)
        public void SoftDelete(string updatedBy)
        {
            IsDeleted = true; // Theo BaseEntity
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}