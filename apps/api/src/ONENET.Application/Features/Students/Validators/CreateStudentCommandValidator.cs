using System;
using FluentValidation;
using ONENET.Application.Features.Students.Commands;
using ONENET.Domain.Enums;

namespace ONENET.Application.Features.Students.Validators
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            // QUAN-20260530-2301-BR02: Mã Học sinh là bắt buộc, giới hạn độ dài
            RuleFor(x => x.MaHocSinh)
                .NotEmpty().WithMessage("Mã Học sinh không được để trống.")
                .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.");

            // QUAN-20260530-2301-BR02: Họ và Tên là bắt buộc, giới hạn độ dài
            RuleFor(x => x.HoVaTen)
                .NotEmpty().WithMessage("Họ và Tên không được để trống.")
                .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.");

            // QUAN-20260530-2301-BR02, QUAN-20260530-2301-BR03: Ngày Sinh là bắt buộc, hợp lệ và không lớn hơn ngày hiện tại
            RuleFor(x => x.NgaySinh)
                .NotEmpty().WithMessage("Ngày Sinh không được để trống.")
                .Must(BeValidDate).WithMessage("Ngày Sinh không hợp lệ.")
                .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.");

            // QUAN-20260530-2301-BR02: Giới Tính là bắt buộc và phải là giá trị hợp lệ của enum
            RuleFor(x => x.GioiTinh)
                .IsInEnum().WithMessage("Giới Tính không hợp lệ. Vui lòng chọn Nam, Nữ hoặc Khác.");

            // QUAN-20260530-2301-BR02: Lớp Học là bắt buộc
            RuleFor(x => x.LopId)
                .NotEmpty().WithMessage("Lớp Học không được để trống.");

            // QUAN-20260530-2301-BR02, QUAN-20260530-2301-BR06: Trạng Thái là bắt buộc
            RuleFor(x => x.TrangThaiId)
                .NotEmpty().WithMessage("Trạng Thái không được để trống.");

            // QUAN-20260530-2301-BR04: Số Điện Thoại Phụ Huynh (nếu có) phải đúng định dạng
            RuleFor(x => x.SdtPhuHuynh)
                .Matches(@"^\+?[0-9]{7,15}$").When(x => !string.IsNullOrEmpty(x.SdtPhuHuynh))
                .WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng. Chỉ chứa số, có thể có dấu '+' ở đầu, độ dài 7-15.");

            // QUAN-20260530-2301-BR05: Email Phụ Huynh (nếu có) phải đúng định dạng
            RuleFor(x => x.EmailPhuHuynh)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailPhuHuynh))
                .WithMessage("Email Phụ Huynh không đúng định dạng.");

            // NgayNhapHoc: required, <= current date
            RuleFor(x => x.NgayNhapHoc)
                .NotEmpty().WithMessage("Ngày Nhập Học không được để trống.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.");

            RuleFor(x => x.DiaChi)
                .MaximumLength(255).When(x => !string.IsNullOrEmpty(x.DiaChi))
                .WithMessage("Địa chỉ không được vượt quá 255 ký tự.");
        }

        private bool BeValidDate(DateTime date)
        {
            return date != default(DateTime); // Checks if date is not its default value
        }
    }
}