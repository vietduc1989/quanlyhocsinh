// QUAN-20260530-2301
using FluentValidation;
using ONENET.Application.Features.Students.Commands;

namespace ONENET.Application.Features.Students.Validators;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    private static readonly string[] AllowedGenders = { "Nam", "Nữ", "Khác" };
    private static readonly string PhoneNumberRegex = @"^\+?[0-9\s-]{7,15}$"; // Simple regex for phone numbers

    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID Học sinh là bắt buộc.");

        // QUAN-20260530-2301-BR02: Các trường bắt buộc (nếu được cung cấp để cập nhật)
        When(x => !string.IsNullOrWhiteSpace(x.MaHocSinh), () =>
        {
            RuleFor(x => x.MaHocSinh)
                .MaximumLength(20).WithMessage("Mã Học sinh không được vượt quá 20 ký tự.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.HoVaTen), () =>
        {
            RuleFor(x => x.HoVaTen)
                .MinimumLength(3).WithMessage("Họ và Tên phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Họ và Tên không được vượt quá 100 ký tự.");
        });

        // QUAN-20260530-2301-BR03: Ngày Sinh hợp lệ (nếu được cung cấp để cập nhật)
        When(x => x.NgaySinh.HasValue, () =>
        {
            RuleFor(x => x.NgaySinh)
                .LessThan(DateTime.Today).WithMessage("Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.GioiTinh), () =>
        {
            RuleFor(x => x.GioiTinh)
                .Must(x => AllowedGenders.Contains(x!)).WithMessage($"Giới Tính phải là một trong các giá trị: {string.Join(", ", AllowedGenders)}.");
        });

        When(x => x.NgayNhapHoc.HasValue, () =>
        {
            RuleFor(x => x.NgayNhapHoc)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Ngày Nhập Học không thể lớn hơn ngày hiện tại.");
        });

        // Optional fields validation
        RuleFor(x => x.DiaChi)
            .MaximumLength(255).When(x => !string.IsNullOrWhiteSpace(x.DiaChi)).WithMessage("Địa chỉ không được vượt quá 255 ký tự.");

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