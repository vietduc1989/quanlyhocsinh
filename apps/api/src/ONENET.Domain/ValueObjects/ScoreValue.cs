// QUAN-20260531-154643
using ONENET.Domain.Common; // Assuming BaseValueObject or similar base is in Common

namespace ONENET.Domain.ValueObjects
{
    // Value Object for score to enforce BR01: 0.0-10.0, max 2 decimal places.
    public class ScoreValue : BaseValueObject
    {
        public decimal Value { get; private set; }

        private ScoreValue(decimal value)
        {
            Value = value;
        }

        public static ScoreValue FromDecimal(decimal value)
        {
            if (value < 0.0m || value > 10.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Score value must be between 0.0 and 10.0.");
            }
            if (decimal.Round(value, 2) != value)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Score value can have at most 2 decimal places.");
            }
            return new ScoreValue(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        // Implicit conversion from decimal to ScoreValue
        public static implicit operator ScoreValue(decimal value) => FromDecimal(value);

        // Implicit conversion from ScoreValue to decimal
        public static implicit operator decimal(ScoreValue scoreValue) => scoreValue.Value;

        public override string ToString() => Value.ToString("F2"); // Format to 2 decimal places
    }

    // A minimal BaseValueObject if not already existing in ONENET.Domain.Common
    // Assuming it's somewhere in ONENET.Domain/Common/BaseValueObject.cs
    // If not, I'd define it here or in a more appropriate common domain location.
    // For now, I will assume BaseValueObject exists in ONENET.Domain.Common.
    public abstract class BaseValueObject
    {
        protected static bool EqualOperator(BaseValueObject left, BaseValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(BaseValueObject left, BaseValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (BaseValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}