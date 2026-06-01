// QUAN-20260531-154643
using FluentValidation;
using ONENET.Application.Subjects.Commands;

namespace ONENET.Application.Subjects.Validators
{
    public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã môn học không được để trống.")
                .MaximumLength(20).WithMessage("Mã môn học không được vượt quá 20 ký tự.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên môn học không được để trống.")
                .MaximumLength(100).WithMessage("Tên môn học không được vượt quá 100 ký tự.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");

            RuleFor(x => x.Credits)
                .InclusiveBetween(1, 10).WithMessage("Số tín chỉ phải là số nguyên dương từ 1 đến 10.");
        }
    }
}