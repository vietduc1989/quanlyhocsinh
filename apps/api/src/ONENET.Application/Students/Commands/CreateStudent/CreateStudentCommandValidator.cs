using System;
using FluentValidation;

namespace ONENET.Application.Students.Commands.CreateStudent;

/// <summary>
/// Validator cho luồng thêm mới học sinh bằng FluentValidation.
/// </summary>
public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.StudentCode)
            .NotEmpty().WithMessage("Mã học sinh không được để trống.")
            .MaximumLength(50).WithMessage("Mã học sinh không được dài quá 50 ký tự.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Họ và tên học sinh không được để trống.")
            .MaximumLength(150).WithMessage("Họ và tên không được dài quá 150 ký tự.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Ngày sinh không được để trống.")
            .LessThan(DateTime.Today).WithMessage("Ngày sinh phải ở quá khứ.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Giới tính không được để trống.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Địa chỉ Email không đúng định dạng.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.ClassId)
            .NotEmpty().WithMessage("Lớp học bắt buộc phải được chọn.");
    }
}