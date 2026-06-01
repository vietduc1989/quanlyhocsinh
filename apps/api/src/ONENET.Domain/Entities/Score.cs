using ONENET.Domain.Common;
using ONENET.Domain.ValueObjects;

namespace ONENET.Domain.Entities
{
    public class Score : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Guid SubjectId { get; private set; }
        public Guid SemesterId { get; private set; }
        public ScoreValue Value { get; private set; } = default!; // Use ScoreValue Value Object

        // Private constructor for EF Core and factory method
        private Score() { }

        // Factory method to create a new Score instance
        public static Score Create(Guid studentId, Guid subjectId, Guid semesterId, ScoreValue value, string createdBy)
        {
            var score = new Score
            {
                StudentId = studentId,
                SubjectId = subjectId,
                SemesterId = semesterId,
                Value = value,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            // Id is set by BaseEntity constructor Guid.NewGuid()
            return score;
        }

        public void UpdateValue(ScoreValue newValue, string updatedBy)
        {
            Value = newValue;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted(string updatedBy)
        {
            IsDeleted = true;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}