// QUAN-20260530-2302
using FluentValidation;

namespace ONENET.Application.Classes.Validators
{
    public class GetClassesQueryValidator : AbstractValidator<GetClassesQuery>
    {
        private static readonly int[] _allowedPageSizes = { 10, 25, 50, 100 };

        public GetClassesQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Số trang phải lớn hơn hoặc bằng 1.");

            RuleFor(x => x.PageSize)
                .Must(size => _allowedPageSizes.Contains(size))
                .WithMessage($"Kích thước trang phải là một trong các giá trị: {string.Join(", ", _allowedPageSizes)}.");

            RuleFor(x => x.SchoolYear)
                .MaximumLength(9).WithMessage("Niên khóa không được vượt quá 9 ký tự.");

            RuleFor(x => x.HomeroomTeacherName)
                .MaximumLength(100).WithMessage("Tên giáo viên chủ nhiệm không được vượt quá 100 ký tự.");

            RuleFor(x => x.Search)
                .MaximumLength(100).WithMessage("Chuỗi tìm kiếm không được vượt quá 100 ký tự.");
        }
    }
}