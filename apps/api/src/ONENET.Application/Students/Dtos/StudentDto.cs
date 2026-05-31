using System;
using ONENET.Domain.Enums;

namespace ONENET.Application.Students.Dtos;

/// <summary>
/// DTO chứa thông tin trả ra Client, đảm bảo không lộ cấu trúc thực thể gốc.
/// </summary>
public class StudentDto
{
    public Guid Id { get; set; }
    public string StudentCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string? ParentName { get; set; }
    public string? ParentPhoneNumber { get; set; }
    public StudentStatus Status { get; set; }
    public string StatusText => Status.ToString();
}