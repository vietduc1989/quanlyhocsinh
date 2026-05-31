**Tóm tắt Dự án: Quản lý Môn học**

*   **Module**: Quản lý Môn học (`QUAN-20260531-154643`) thuộc dự án `quanlyhocsinh`.
*   **Requirements**:
    *   **Functional**: Cung cấp giao diện và chức năng cho người dùng có thẩm quyền (Admin, Chuyên viên phòng Đào tạo) để quản lý (Tạo, Xem danh sách/chi tiết, Cập nhật, Xóa mềm) thông tin môn học. Bao gồm tìm kiếm, phân trang, xác thực dữ liệu đầu vào và ghi nhật ký (Audit Trail) các thao tác quan trọng. Giáo viên có quyền xem danh sách và chi tiết môn học.
    *   **Non-functional**:
        *   **Hiệu năng**: Tải trang danh sách < 2s (1000 bản ghi), thao tác CRUD < 1s, hỗ trợ 50 người dùng đồng thời.
        *   **Bảo mật**: Yêu cầu xác thực & phân quyền (vai trò Admin/Chuyên viên Đào tạo mới được CRUD), bảo vệ dữ liệu, chống SQL Injection/XSS.
        *   **Sẵn sàng**: 99.5% trong giờ làm việc, hiển thị & ghi log lỗi rõ ràng.
*   **Constraints**:
    *   **Scope Exclusion**: Không bao gồm quản lý học liệu, phân công giảng viên, tích hợp xếp TKB/quản lý lớp, quản lý điểm số sinh viên, chức năng nhập/xuất dữ liệu hàng loạt.
    *   **Business Rules**: Mã môn học, Tên môn học phải duy nhất; Số tín chỉ là số nguyên dương (1-10); Xóa môn học là xóa mềm (chuyển trạng thái sang "Không hoạt động"); Giới hạn độ dài ký tự cho các trường; Mã, Tên, Số tín chỉ là các trường bắt buộc.
    *   **Dependencies**: Phụ thuộc vào module quản lý người dùng và phân quyền hiện có, cơ sở dữ liệu và hạ tầng back-end.
    *   **Assumptions**: Module quản lý người dùng ổn định, quyền truy cập đã được cấp, số lượng bản ghi ban đầu không quá lớn, hỗ trợ tiếng Việt.
*   **Progress**: Tài liệu Business Requirements Document (BRD) đã hoàn thiện và hiện tại (phiên bản 1.0). Các giai đoạn tiếp theo (SRS/SAD, Phát triển, Kiểm thử) đang chờ duyệt/thực hiện.
*   **Known Issues**: Chưa có vấn đề phát sinh hoặc được xác định tại thời điểm này.