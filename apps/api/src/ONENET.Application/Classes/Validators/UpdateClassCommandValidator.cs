// QUAN-20260530-2302
using FluentValidation;
using ONENET.Application.Common.Interfaces;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Classes.Validators
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClassCommandValidator(IApplicationDbContext context)
        {
            _context = context;

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

            RuleFor(x => x.Version)
                .NotNull().WithMessage("Thông tin phiên bản không được để trống để đảm bảo tính đồng bộ.")
                .NotEmpty().WithMessage("Thông tin phiên bản không hợp lệ.");
        }

        private bool BeValidSchoolYearFormat(string schoolYear)
        {
            if (Regex.IsMatch(schoolYear, @"^\d{4}$"))
            {
                return true;
            }
            if (Regex.IsMatch(schoolYear, @"^\d{4}-\d{4}$"))
            {
                var years = schoolYear.Split('-');
                if (int.TryParse(years[0], out int startYear) && int.TryParse(years[1], out int endYear))
                {
                    return endYear == startYear + 1;
                }
            }
            return false;
        }

        private async Task<bool> BeExistingTeacher(Guid? teacherId, CancellationToken ct)
        {
            if (!teacherId.HasValue) return true;
            return await _context.Teachers.AnyAsync(t => t.Id == teacherId.Value, ct);
        }
    }
}