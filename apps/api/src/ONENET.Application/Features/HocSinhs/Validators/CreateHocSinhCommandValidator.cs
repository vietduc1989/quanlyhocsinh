// QUAN-20260604-153038
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Features.HocSinhs.Commands;
using ONENET.Domain.Enums;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ONENET.Application.Features.HocSinhs.Validators
{
    public class CreateHocSinhCommandValidator : AbstractValidator<CreateHocSinhCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateHocSinhCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            // BR02: Required fields
            RuleFor(x => x.HoTen)
                .NotEmpty().WithMessage("Họ và Tên không được để trống.");

            RuleFor(x => x.NgaySinh)
                .NotEmpty().WithMessage("Ngày Sinh không được để trống.")
                .Must(BeAValidDate).WithMessage("Ngày Sinh không hợp lệ.") // BR03: Valid date
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại."); // BR03: Not in future

            RuleFor(x => x.GioiTinh)
                .IsInEnum().WithMessage("Giới Tính không hợp lệ."); // BR02: Required implicitly by Enum, and must be valid enum value

            RuleFor(x => x.LopHocId)
                .NotEmpty().WithMessage("Lớp Học không được để trống.")
                .MustAsync(BeExistingLopHocId).WithMessage("Lớp Học không tồn tại."); // BR02: Required, and must exist

            RuleFor(x => x.TrangThai)
                .IsInEnum().WithMessage("Trạng Thái không hợp lệ.") // BR06: Predefined values (enum)
                .Must(BeAValidTrangThai).WithMessage("Trạng Thái không hợp lệ. Vui lòng chọn một trong các giá trị cho phép: 'Đang học', 'Đã tốt nghiệp', 'Đã chuyển trường', 'Tạm dừng'."); // BR06: Custom message

            // BR04: SĐT Phụ Huynh format
            RuleFor(x => x.SoDienThoaiPH)
                .Matches(new Regex(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$")).When(x => !string.IsNullOrWhiteSpace(x.SoDienThoaiPH))
                .WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng.");

            // BR05: Email Phụ Huynh format
            RuleFor(x => x.EmailPH)
                .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.EmailPH))
                .WithMessage("Email Phụ Huynh không đúng định dạng.");
        }

        private bool BeAValidDate(DateOnly date)
        {
            try
            {
                // DateOnly's constructor already validates, so this is just a sanity check
                var _ = new DateOnly(date.Year, date.Month, date.Day);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        private async Task<bool> BeExistingLopHocId(Guid lopHocId, CancellationToken cancellationToken)
        {
            return await _context.LopHocs.AnyAsync(l => l.Id == lopHocId, cancellationToken);
        }

        private bool BeAValidTrangThai(TrangThaiHocSinh trangThai)
        {
            // BR06: Ensure the enum value is one of the explicitly allowed business states.
            // In case the enum has more values than what's allowed by BR06.
            return Enum.IsDefined(typeof(TrangThaiHocSinh), trangThai) &&
                   (trangThai == TrangThaiHocSinh.DangHoc ||
                    trangThai == TrangThaiHocSinh.DaTotNghiep ||
                    trangThai == TrangThaiHocSinh.DaChuyenTruong ||
                    trangThai == TrangThaiHocSinh.TamDung);
        }
    }
}