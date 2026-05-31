using System;
using System.Collections.Generic;

namespace ONENET.Domain.Entities;

/// <summary>
/// Thực thể Lớp học (Class) - Dữ liệu tham chiếu cho Học sinh.
/// </summary>
public class Class
{
    public Guid Id { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string SchoolYear { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}