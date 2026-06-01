// QUAN-20260530-2302
using FluentValidation;
using ONENET.Application.Common.Interfaces;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Classes.Validators
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateClassCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.ClassCode)
                .NotEmpty().WithMessage("Mã lớp không được để trống.")
                .MaximumLength(20).WithMessage("Mã lớp không được vượt quá 20 ký tự.")
                .MustAsync(BeUniqueClassCode).WithMessage("Mã lớp '{PropertyValue}' đã tồn tại.");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Tên lớp không được để trống.")
                .MaximumLength(50).WithMessage("Tên lớp không được vượt quá 50 ký tự.");

            RuleFor(x => x.SchoolYear)
                .NotEmpty().WithMessage("Niên khóa không được để trống.")
                .MaximumLength(9).WithMessage("Niên khóa không được vượt quá 9 ký tự.")
                .Must(BeValidSchoolYearFormat).WithMessage("Niên khóa không đúng định dạng YYYY hoặc YYYY-YYYY.");

            RuleFor(x => x.HomeroomTeacherId)
                .MustAsync(BeExistingTeacher).When(x => x.HomeroomTeacherId.HasValue)
                .WithMessage("Giáo viên chủ nhiệm không hợp lệ.");
        }

        private async Task<bool> BeUniqueClassCode(string classCode, CancellationToken ct)
        {
            return !await _context.Classes.AnyAsync(c => c.ClassCode == classCode, ct);
        }

        private bool BeValidSchoolYearFormat(string schoolYear)
        {
            // YYYY format
            if (Regex.IsMatch(schoolYear, @"^\d{4}$"))
            {
                return true;
            }
            // YYYY-YYYY format
            if (Regex.IsMatch(schoolYear, @"^\d{4}-\d{4}$"))
            {
                var years = schoolYear.Split('-');
                if (int.TryParse(years[0], out int startYear) && int.TryParse(years[1], out int endYear))
                {
                    return endYear == startYear + 1; // Ensure consecutive years
                }
            }
            return false;
        }

        private async Task<bool> BeExistingTeacher(Guid? teacherId, CancellationToken ct)
        {
            if (!teacherId.HasValue) return true; // Optional field
            return await _context.Teachers.AnyAsync(t => t.Id == teacherId.Value, ct);
        }
    }
}