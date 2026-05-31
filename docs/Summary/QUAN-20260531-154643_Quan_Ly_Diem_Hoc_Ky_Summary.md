Đây là bản tóm tắt cực kỳ ngắn gọn về dự án "Quan Ly Diem Hoc Ky" dựa trên thông tin mới nhất từ BRD và SRS/SAD:

**Module:** Quan Ly Diem Hoc Ky (`QUAN-20260531-154643`) thuộc dự án "quanlyhocsinh", phiên bản 1.0. Mục tiêu là cung cấp hệ thống quản lý (tạo, xem, cập nhật, xóa) điểm học kỳ của học sinh cho Giáo viên và Quản trị viên, đảm bảo tính chính xác, hiệu quả và toàn vẹn dữ liệu. Hệ thống sẽ được phát triển với **.NET 10, ASP.NET Web API (Backend), React + Mantine UI (Frontend), PostgreSQL (Database)**, theo kiến trúc **Clean Architecture và CQRS + MediatR**.

**Requirements:**
*   **Chức năng:** Hỗ trợ CRUD điểm học kỳ; xem danh sách có phân trang; xem chi tiết; tìm kiếm theo HS, môn, học kỳ; xác thực dữ liệu đầu vào.
*   **Phi chức năng:**
    *   **Hiệu suất:** Tải trang danh sách (<3s/500 bản ghi), phản hồi CRUD (<2s), tìm kiếm (<3s/10k bản ghi). Sẽ được tối ưu bằng Database Indexing (bao gồm Unique Index cho điểm trùng lặp), Query Optimization (`AsNoTracking`).
    *   **Bảo mật:** Yêu cầu xác thực (Bearer Token JWT) và phân quyền chặt chẽ (Policy-based Authorization) theo vai trò. `Admin` có toàn quyền CRUD; `Teacher` chỉ có quyền CRUD trong phạm vi phân công giảng dạy (dựa trên `StudentId`, `SubjectId`, `SemesterId`). Chống SQLI/XSS, HTTPS bắt buộc.
    *   **Khả dụng:** Uptime >99.5% (24/7).
*   **Quy tắc nghiệp vụ:** Điểm từ 0.0-10.0 (tối đa 2 thập phân); duy nhất 1 bản ghi điểm/HS/môn/học kỳ; các trường Học sinh, Môn học, Học kỳ, Điểm số là bắt buộc.
*   **Audit Trail:** Ghi nhật ký đầy đủ các thao tác CRUD.

**Constraints:**
*   **Ngoài phạm vi:** Không tính toán điểm trung bình/tổng kết; không in/xuất báo cáo; không tích hợp bên thứ 3; không quản lý điểm thành phần; không nhập/xuất dữ liệu hàng loạt.
*   **Giả định:** Dữ liệu học sinh, môn học, lớp học, học kỳ đã có trong hệ thống ONENET; người dùng được xác thực và phân quyền; điểm là điểm học kỳ cuối cùng.
*   **Phụ thuộc:** Sử dụng dữ liệu từ các module nội bộ ONENET (Quản lý Học sinh, Môn học, Học kỳ) và hệ thống Xác thực & Phân quyền.

**Progress:** BRD đã hoàn thành (`✅ Đã duyệt`). **Tài liệu SRS/SAD (`QUAN-20260531-154643`) đã hoàn thành và hiện tại (`✅ Hiện tại`), cung cấp đặc tả yêu cầu kỹ thuật chi tiết, thiết kế kiến trúc giải pháp (C4 Container, Sequence Diagram cho luồng tạo điểm), thiết kế database (ERD với bảng `Scores` và `AuditLogs`, cấu hình EF Core với Unique Index cho BR02), hợp đồng API cho 5 chức năng CRUD, và kế hoạch triển khai chi tiết (danh sách file cần tạo/sửa).** Mã nguồn (DEV) và test cases (TEST) đang ở trạng thái chờ SRS duyệt để bắt đầu phát triển và kiểm thử.

**Known Issues:** Các tình huống cạnh đã xác định trong BRD đã được đưa ra giải pháp cụ thể trong SRS/SAD, cùng với các rủi ro mới được xác định và giảm thiểu:
*   **Xác thực dữ liệu chặt chẽ cho điểm số và các trường bắt buộc:** Sẽ được xử lý bằng FluentValidation (tầng ứng dụng) và Check Constraints (tầng database).
*   **Ngăn chặn việc tạo điểm trùng lặp:** Đảm bảo bằng Unique Index trên database (`StudentId`, `SubjectId`, `SemesterId`) và kiểm tra ở tầng ứng dụng.
*   **Đảm bảo kiểm soát quyền hạn theo vai trò:** Triển khai Policy-based Authorization (cho `Admin` và `Teacher`) ở Authorization Middleware và trong các Command/Query Handlers với dịch vụ kiểm tra quyền hạn chi tiết.
*   **Các rủi ro khác được xác định và có kế hoạch giảm thiểu:** Lỗi đồng thời (Optimistic Concurrency), vi phạm toàn vẹn dữ liệu (FK, Unique/Check Constraints), vượt quyền (HTTPS, AuthZ), suy giảm hiệu suất (Indexing, Query Optimization), không nhất quán nhật ký kiểm toán (Transactions).