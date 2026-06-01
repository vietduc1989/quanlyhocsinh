using FluentValidation;
using ONENET.Application.Features.Students.Queries;

namespace ONENET.Application.Features.Students.Validators
{
    public class GetStudentsQueryValidator : AbstractValidator<GetStudentsQuery>
    {
        public GetStudentsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Số trang phải lớn hơn hoặc bằng 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Kích thước trang phải lớn hơn hoặc bằng 1.")
                .LessThanOrEqualTo(100).WithMessage("Kích thước trang không được vượt quá 100.");

            RuleFor(x => x.SortBy)
                .Must(BeAValidSortByField).WithMessage("Trường sắp xếp không hợp lệ. Các giá trị cho phép: maHocSinh, hoVaTen, lopHoc, trangThai.")
                .When(x => !string.IsNullOrEmpty(x.SortBy));

            RuleFor(x => x.SortOrder)
                .Must(BeAValidSortOrder).WithMessage("Thứ tự sắp xếp không hợp lệ. Các giá trị cho phép: asc, desc.")
                .When(x => !string.IsNullOrEmpty(x.SortOrder));
        }

        private bool BeAValidSortByField(string? sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return true; // Handled by When clause
            return sortBy.ToLower() == "mahocsinh" ||
                   sortBy.ToLower() == "hovaten" ||
                   sortBy.ToLower() == "lophoc" ||
                   sortBy.ToLower() == "trangthai";
        }

        private bool BeAValidSortOrder(string? sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder)) return true; // Handled by When clause
            return sortOrder.ToLower() == "asc" ||
                   sortOrder.ToLower() == "desc";
        }
    }
}