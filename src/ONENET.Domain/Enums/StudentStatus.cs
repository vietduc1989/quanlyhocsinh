namespace ONENET.Domain.Enums;

/// <summary>
/// Định nghĩa các trạng thái hoạt động của Học sinh trong hệ thống ONENET.
/// </summary>
public enum StudentStatus
{
    Active = 1,      // Đang học
    Inactive = 2,    // Tạm dừng/Vô hiệu hóa (Soft Deleted)
    Graduated = 3,   // Đã tốt nghiệp
    DroppedOut = 4   // Thôi học
}