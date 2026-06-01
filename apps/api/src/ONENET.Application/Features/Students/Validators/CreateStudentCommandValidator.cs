// QUAN-20260530-2301
using FluentValidation;
using ONENET.Application.Features.Students.Commands;

namespace ONENET.Application.Features.Students.Validators;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    private static readonly string[] AllowedGenders = { "Nam", "Nữ", "Khác" };
    private static readonly string PhoneNumberRegex = @"^\+?[0-9\s-]{7,15}$"; // Simple regex for phone numbers

    public CreateStudentCommandValidator()
    {
        // QUAN-20260530-2301-BR02: Các trường bắt buộc
        RuleFor(x => x.MaHocSinh)
            .NotEmpty().WithMessage("Mã Học sinh không được để trống.")
            .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.");

        RuleFor(x => x.HoVaTen)
            .NotEmpty().WithMessage("Họ và Tên không được để trống.")
            .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
            .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.");

        // QUAN-20260530-2301-BR03: Ngày Sinh hợp lệ
        RuleFor(x => x.NgaySinh)
            .NotEmpty().WithMessage("Ngày Sinh không được để trống.")
            .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.");

        RuleFor(x => x.GioiTinh)
            .NotEmpty().WithMessage("Giới Tính không được để trống.")
            .Must(x => AllowedGenders.Contains(x)).WithMessage($"Giới Tính phải là một trong các giá trị: {string.Join(", ", AllowedGenders)}.");

        RuleFor(x => x.LopId)
            .NotEmpty().WithMessage("Lớp Học không được để trống.");

        RuleFor(x => x.NgayNhapHoc)
            .NotEmpty().WithMessage("Ngày Nhập Học không được để trống.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.");

        RuleFor(x => x.TrangThaiId)
            .NotEmpty().WithMessage("Trạng Thái không được để trống.");

        // Optional fields validation
        RuleFor(x => x.DiaChi)
            .MaximumLength(255).WithMessage("Địa chỉ không được vượt quá 255 ký tự.");

        // QUAN-20260530-2301-BR04: Số Điện Thoại Phụ Huynh định dạng hợp lệ
        RuleFor(x => x.SdtPhuHuynh)
            .Matches(PhoneNumberRegex).WithMessage("Số Điện Thoại Phụ Huynh không đúng định dạng.")
            .When(x => !string.IsNullOrWhiteSpace(x.SdtPhuHuynh));

        // QUAN-20260530-2301-BR05: Email Phụ Huynh định dạng hợp lệ
        RuleFor(x => x.EmailPhuHuynh)
            .EmailAddress().WithMessage("Email Phụ Huynh không đúng định dạng.")
            .When(x => !string.IsNullOrWhiteSpace(x.EmailPhuHuynh));
    }
}