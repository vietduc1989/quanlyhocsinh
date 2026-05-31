// QUAN-20260530-2301
using FluentValidation;
using ONENET.Application.Features.Students.Dtos;
using System;
using System.Text.RegularExpressions;

namespace ONENET.Application.Features.Students.Commands
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.Student.MaHocSinh)
                .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Student.MaHocSinh));

            RuleFor(x => x.Student.HoVaTen)
                .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Student.HoVaTen));

            RuleFor(x => x.Student.NgaySinh)
                .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.")
                .When(x => x.Student.NgaySinh.HasValue);

            RuleFor(x => x.Student.GioiTinh)
                .Must(BeValidGioiTinh).WithMessage("Giới Tính không hợp lệ. Chỉ chấp nhận 'Nam', 'Nữ' hoặc 'Khác'.")
                .When(x => !string.IsNullOrEmpty(x.Student.GioiTinh));

            RuleFor(x => x.Student.DiaChi)
                .MaximumLength(255).WithMessage("Địa chỉ không được vượt quá 255 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Student.DiaChi));

            RuleFor(x => x.Student.SDTPhuHuynh)
                .Matches(@"^(\+84|0)\d{9}$|^$").WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng. (VD: 0901234567, +84901234567).")
                .When(x => !string.IsNullOrEmpty(x.Student.SDTPhuHuynh));

            RuleFor(x => x.Student.EmailPhuHuynh)
                .EmailAddress().WithMessage("Email Phụ Huynh không đúng định dạng.")
                .When(x => !string.IsNullOrEmpty(x.Student.EmailPhuHuynh));

            RuleFor(x => x.Student.NgayNhapHoc)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.")
                .When(x => x.Student.NgayNhapHoc.HasValue);
        }

        private bool BeValidGioiTinh(string? gioiTinh)
        {
            return string.IsNullOrEmpty(gioiTinh) || gioiTinh == "Nam" || gioiTinh == "Nữ" || gioiTinh == "Khác";
        }
    }
}