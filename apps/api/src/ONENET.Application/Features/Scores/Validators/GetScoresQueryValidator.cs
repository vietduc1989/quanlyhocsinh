// QUAN-20260531-154643
using FluentValidation;
using ONENET.Application.Features.Scores.Queries;

namespace ONENET.Application.Features.Scores.Validators
{
    public class GetScoresQueryValidator : AbstractValidator<GetScoresQuery>
    {
        public GetScoresQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Page size must be at least 1.")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
            RuleFor(x => x.SortOrder)
                .Must(BeAValidSortOrder).WithMessage("Sort order must be 'asc' or 'desc'.")
                .When(x => !string.IsNullOrWhiteSpace(x.SortOrder));
            RuleFor(x => x.SortBy)
                .Must(BeAValidSortByField).WithMessage("SortBy field is not supported.")
                .When(x => !string.IsNullOrWhiteSpace(x.SortBy));
        }

        private bool BeAValidSortOrder(string? sortOrder)
        {
            return sortOrder?.ToLower() == "asc" || sortOrder?.ToLower() == "desc";
        }

        private bool BeAValidSortByField(string? sortBy)
        {
            // Define allowed sort fields
            var allowedSortFields = new[] { "studentname", "value", "createddate" };
            return allowedSortFields.Contains(sortBy?.ToLower());
        }
    }
}