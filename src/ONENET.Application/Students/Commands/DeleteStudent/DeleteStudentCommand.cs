using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ONENET.Domain.Enums;
using ONENET.Domain.Repositories;

namespace ONENET.Application.Students.Commands.DeleteStudent;

public record DeleteStudentCommand(Guid Id) : IRequest<Unit>;

/// <summary>
/// Xử lý Xóa học sinh (Thực chất thực hiện Soft Delete và đổi trạng thái về Inactive)
/// </summary>
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;

    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (student == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy học sinh cần xóa.");
        }

        // Soft delete bằng cách set IsDeleted và chuyển trạng thái hoạt động
        student.IsDeleted = true;
        student.Status = StudentStatus.Inactive;
        student.LastModifiedAt = DateTime.UtcNow;
        student.LastModifiedBy = "System_Dev";

        _studentRepository.Update(student);
        await _studentRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}