Dưới đây là bản tóm tắt cực kỳ ngắn gọn về dự án Quản lý Môn học:

**Tóm tắt Dự án: Quản lý Môn học**

*   **Module**: Quản lý Môn học (`QUAN-20260531-154643`) thuộc dự án `quanlyhocsinh`.
*   **Requirements**:
    *   **Functional**: Cung cấp giao diện và chức năng CRUD (Tạo, Xem DS/CT, Cập nhật, Xóa mềm) thông tin môn học cho Admin/Chuyên viên Đào tạo. Giáo viên có quyền xem DS/CT. Bao gồm tìm kiếm, phân trang, sắp xếp, xác thực dữ liệu đầu vào chặt chẽ (FluentValidation theo Business Rules) và ghi nhật ký (Audit Trail) các thao tác quan trọng. API RESTful được thiết kế theo Clean Architecture, CQRS + MediatR.
    *   **Non-functional**:
        *   **Hiệu năng**: Tải trang danh sách < 2s (với 1000 bản ghi), thao tác CRUD < 1s, hỗ trợ 50 người dùng đồng thời (giảm hiệu năng tối đa 20%). Tối ưu bằng indexing (`Code`, `Name`, `IsActive`, `CreatedAt`), `AsNoTracking` cho truy vấn đọc, và phân trang hiệu quả.
        *   **Bảo mật**: Yêu cầu xác thực (JWT Bearer Token) & phân quyền (Role-based: Admin, Chuyên viên Đào tạo, Giáo viên) qua Authorization Policy. Chống SQL Injection (dùng ORM), XSS (mã hóa output, validation đầu vào), và bắt buộc sử dụng HTTPS.
        *   **Sẵn sàng**: 99.5% trong giờ làm việc, hiển thị & ghi log lỗi rõ ràng. Audit Trail ghi lại chi tiết `UserId`, `ActionType`, `Changes` cho mọi thao tác CRUD.
*   **Constraints**:
    *   **Scope Exclusion**: Không bao gồm quản lý học liệu, phân công giảng viên, tích hợp xếp TKB/quản lý lớp, quản lý điểm số sinh viên, nhập/xuất dữ liệu hàng loạt.
    *   **Business Rules**: Mã, Tên môn học phải duy nhất; Số tín chỉ là số nguyên dương (1-10); Xóa môn học là xóa mềm (`IsActive=false`); Giới hạn độ dài ký tự cho các trường; Mã, Tên, Số tín chỉ là các trường bắt buộc. Các quy tắc này được enforce qua FluentValidation và Unique Index CSDL (PostgreSQL).
    *   **Dependencies**: Phụ thuộc vào module quản lý người dùng và phân quyền hiện có, cơ sở dữ liệu (PostgreSQL) và hạ tầng back-end (.NET 8 Web API, Entity Framework Core).
    *   **Assumptions**: Module quản lý người dùng ổn định, quyền truy cập đã được cấp, số lượng bản ghi ban đầu không quá lớn, hỗ trợ tiếng Việt.
*   **Progress**: Tài liệu Business Requirements Document (BRD) và Software Requirements Specification/Solution Architecture Document (SRS/SAD) (phiên bản 1.0) đã hoàn thiện và được phê duyệt. Giai đoạn Phát triển (DEV) và Kiểm thử (TEST) đang chờ triển khai.
*   **Known Issues / Identified Risks**:
    *   **Concurrency**: Rủi ro ghi đè/trùng lặp dữ liệu (trên Mã/Tên môn học) được giảm thiểu bằng Unique Constraints (DB), kiểm tra tại tầng ứng dụng, và Optimistic Concurrency Control.
    *   **Performance**: Rủi ro suy giảm hiệu năng được xử lý qua indexing, `AsNoTracking`, phân trang tối ưu, và phân tích/tối ưu truy vấn SQL.
    *   **Data Inconsistency**: Rủi ro dữ liệu không hợp lệ được giảm thiểu bằng FluentValidation và ràng buộc `NOT NULL` tại CSDL.
    *   **Security**: Đã có giải pháp phòng ngừa SQL Injection (EF Core), XSS (validation đầu vào, mã hóa output), và truy cập trái phép (AuthN/AuthZ mạnh mẽ, HTTPS).