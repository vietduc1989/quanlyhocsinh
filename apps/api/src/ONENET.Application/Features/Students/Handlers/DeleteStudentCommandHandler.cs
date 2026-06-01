// QUAN-20260530-2301
using MediatR;
using ONENET.Application.Common.Exceptions;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Domain.Interfaces;

namespace ONENET.Application.Features.Students.Handlers;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ApiResponse>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException($"Học sinh không tìm thấy với ID: {request.Id}");
        }

        // QUAN-20260530-2301-FR06: Ngăn chặn xóa học sinh nếu có dữ liệu liên quan
        // Placeholder for actual related data check. In a real system, this would query
        // other tables (e.g., grades, attendance) that have FK to StudentId.
        if (await _studentRepository.HasRelatedDataAsync(request.Id, cancellationToken))
        {
            throw new ConflictException("Không thể xóa học sinh vì có dữ liệu liên quan. Vui lòng xử lý dữ liệu liên quan trước.", new List<ApiError>
            {
                new() { Code = "STUDENT_HAS_RELATED_DATA", Message = "Học sinh có điểm số hoặc lịch sử học tập liên quan." }
            });
        }

        _studentRepository.Delete(student); // Soft delete (IsDeleted = true)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.SuccessResult("Xóa học sinh thành công");
    }
}