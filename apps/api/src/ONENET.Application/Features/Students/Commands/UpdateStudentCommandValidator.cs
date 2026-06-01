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
            RuleFor(x => x.MaHocSinh)
                .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.MaHocSinh));

            RuleFor(x => x.HoVaTen)
                .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.HoVaTen));

            RuleFor(x => x.NgaySinh)
                .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.")
                .When(x => x.NgaySinh.HasValue);

            RuleFor(x => x.GioiTinh)
                .IsInEnum().WithMessage("Gi?i T�nh kh�ng h?p l?.").When(x => x.GioiTinh.HasValue);
            RuleFor(x => x.DiaChi)
                .MaximumLength(255).WithMessage("Địa chỉ không được vượt quá 255 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.DiaChi));

            RuleFor(x => x.SdtPhuHuynh)
                .Matches(@"^(\+84|0)\d{9}$|^$").WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng. (VD: 0901234567, +84901234567).")
                .When(x => !string.IsNullOrEmpty(x.SdtPhuHuynh));

            RuleFor(x => x.EmailPhuHuynh)
                .EmailAddress().WithMessage("Email Phụ Huynh không đúng định dạng.")
                .When(x => !string.IsNullOrEmpty(x.EmailPhuHuynh));

            RuleFor(x => x.NgayNhapHoc)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.")
                .When(x => x.NgayNhapHoc.HasValue);
        }

        private bool BeValidGioiTinh(string? gioiTinh)
        {
            return string.IsNullOrEmpty(gioiTinh) || gioiTinh == "Nam" || gioiTinh == "Nữ" || gioiTinh == "Khác";
        }
    }
}
