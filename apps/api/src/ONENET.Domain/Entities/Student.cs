using System;
using ONENET.Domain.Enums;

namespace ONENET.Domain.Entities;

/// <summary>
/// Thực thể Học sinh (Student) chứa toàn bộ thông tin nhân khẩu học và quản trị.
/// </summary>
public class Student
{
    public Guid Id { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty; // Nam, Nữ, Khác
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    // Khóa ngoại liên kết tới Lớp học
    public Guid ClassId { get; set; }
    public virtual Class? Class { get; set; }

    // Thông tin phụ huynh
    public string? ParentName { get; set; }
    public string? ParentPhoneNumber { get; set; }

    // Trạng thái và Xóa mềm
    public StudentStatus Status { get; set; } = StudentStatus.Active;
    public bool IsDeleted { get; set; } = false;

    // Audit trail fields
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}