using System;
using FluentValidation;
using ONENET.Application.Features.Students.Commands;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.Validators
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID học sinh không được để trống.");

            // QUAN-20260530-2301-BR01: Mã Học sinh (nếu có)
            RuleFor(x => x.MaHocSinh)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.MaHocSinh))
                .WithMessage("Mã Học sinh không được vượt quá 20 ký tự.");

            // Họ và Tên (nếu có)
            RuleFor(x => x.HoVaTen)
                .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.HoVaTen))
                .WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.HoVaTen))
                .WithMessage("Họ và Tên không được vượt quá 100 ký tự.");

            // QUAN-20260530-2301-BR03: Ngày Sinh (nếu có)
            RuleFor(x => x.NgaySinh)
                .Must(BeValidDate).When(x => x.NgaySinh.HasValue)
                .WithMessage("Ngày Sinh không hợp lệ.")
                .LessThan(DateTime.Today).When(x => x.NgaySinh.HasValue)
                .WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.");

            // Giới Tính (nếu có)
            RuleFor(x => x.GioiTinh)
                .IsInEnum().When(x => x.GioiTinh.HasValue)
                .WithMessage("Giới Tính không hợp lệ. Vui lòng chọn Nam, Nữ hoặc Khác.");

            // QUAN-20260530-2301-BR04: Số Điện Thoại Phụ Huynh (nếu có)
            RuleFor(x => x.SdtPhuHuynh)
                .Matches(@"^\+?[0-9]{7,15}$").When(x => !string.IsNullOrEmpty(x.SdtPhuHuynh))
                .WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng. Chỉ chứa số, có thể có dấu '+' ở đầu, độ dài 7-15.");

            // QUAN-20260530-2301-BR05: Email Phụ Huynh (nếu có)
            RuleFor(x => x.EmailPhuHuynh)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailPhuHuynh))
                .WithMessage("Email Phụ Huynh không đúng định dạng.");

            // NgayNhapHoc (nếu có)
            RuleFor(x => x.NgayNhapHoc)
                .LessThanOrEqualTo(DateTime.Today).When(x => x.NgayNhapHoc.HasValue)
                .WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.");
            
            RuleFor(x => x.DiaChi)
                .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.DiaChi))
                .WithMessage("Địa chỉ không được vượt quá 255 ký tự.");
        }

        private bool BeValidDate(DateTime? date)
        {
            return date.HasValue && date.Value != default(DateTime);
        }
    }
}