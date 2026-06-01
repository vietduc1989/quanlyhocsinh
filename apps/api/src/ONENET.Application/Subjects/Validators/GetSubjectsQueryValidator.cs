// QUAN-20260531-154643
using FluentValidation;
using ONENET.Application.Subjects.Queries;

namespace ONENET.Application.Subjects.Validators
{
    public class GetSubjectsQueryValidator : AbstractValidator<GetSubjectsQuery>
    {
        private static readonly string[] _validSortByFields = { "code", "name", "credits", "createdAt" };
        private static readonly string[] _validSortOrders = { "asc", "desc" };

        public GetSubjectsQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("Số trang (pageIndex) phải lớn hơn hoặc bằng 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Kích thước trang (pageSize) phải nằm trong khoảng từ 1 đến 100.");

            RuleFor(x => x.SortBy)
                .Must(BeAValidSortByField).When(x => !string.IsNullOrWhiteSpace(x.SortBy))
                .WithMessage($"Trường sắp xếp (sortBy) không hợp lệ. Các giá trị cho phép: {string.Join(", ", _validSortByFields)}.");

            RuleFor(x => x.SortOrder)
                .Must(BeAValidSortOrder).When(x => !string.IsNullOrWhiteSpace(x.SortOrder))
                .WithMessage($"Thứ tự sắp xếp (sortOrder) không hợp lệ. Các giá trị cho phép: {string.Join(", ", _validSortOrders)}.");
        }

        private bool BeAValidSortByField(string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy)) return true; // Handled by When clause
            return _validSortByFields.Contains(sortBy.ToLower());
        }

        private bool BeAValidSortOrder(string? sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortOrder)) return true; // Handled by When clause
            return _validSortOrders.Contains(sortOrder.ToLower());
        }
    }
}