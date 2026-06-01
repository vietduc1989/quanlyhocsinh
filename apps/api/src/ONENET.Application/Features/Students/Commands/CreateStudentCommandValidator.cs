using FluentValidation;
using ONENET.Application.Features.Students.Dtos;
using System;
using System.Text.RegularExpressions;

namespace ONENET.Application.Features.Students.Commands
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.MaHocSinh)
                .NotEmpty().WithMessage("Mã Học sinh không được để trống.")
                .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.");

            RuleFor(x => x.HoVaTen)
                .NotEmpty().WithMessage("Họ và Tên không được để trống.")
                .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.");

            RuleFor(x => x.NgaySinh)
                .NotEmpty().WithMessage("Ngày Sinh không được để trống.")
                .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.GioiTinh)
                .NotEmpty().WithMessage("Giới Tính không được để trống.")
                .IsInEnum().WithMessage("Giới Tính không hợp lệ. Chỉ chấp nhận 'Nam', 'Nữ' hoặc 'Khác'.");

            RuleFor(x => x.DiaChi)
                .MaximumLength(255).WithMessage("Địa chỉ không được vượt quá 255 ký tự.");

            RuleFor(x => x.SdtPhuHuynh)
                .Matches(@"^(\+84|0)\d{9}$|^$").WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng. (VD: 0901234567, +84901234567).")
                .When(x => !string.IsNullOrEmpty(x.SdtPhuHuynh));

            RuleFor(x => x.EmailPhuHuynh)
                .EmailAddress().WithMessage("Email Phụ Huynh không đúng định dạng.")
                .When(x => !string.IsNullOrEmpty(x.EmailPhuHuynh));

            RuleFor(x => x.LopId)
                .NotEmpty().WithMessage("Lớp Học không được để trống.");

            RuleFor(x => x.NgayNhapHoc)
                .NotEmpty().WithMessage("Ngày Nhập Học không được để trống.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.");

            RuleFor(x => x.TrangThaiId)
                .NotEmpty().WithMessage("Trạng Thái không được để trống.");
        }

        private bool BeValidGioiTinh(string gioiTinh)
        {
            return gioiTinh == "Nam" || gioiTinh == "Nữ" || gioiTinh == "Khác";
        }
    }
}
