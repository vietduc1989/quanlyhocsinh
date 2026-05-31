**Tóm tắt Dự án:**

**Module:** Quản lý Thông tin Học sinh (Mã: `QUAN-20260530-2301`) thuộc dự án `quanlyhocsinh`.
*   **Vai trò:** Thành phần cốt lõi quản lý dữ liệu học sinh.
*   **Người dùng chính:** Administrator (toàn quyền CRUD), Giáo viên (chỉ xem danh sách/chi tiết, tìm kiếm, lọc).
*   **Công nghệ sử dụng:** Backend (.NET 10, ASP.NET Web API, Clean Architecture, CQRS+MediatR, EF Core, PostgreSQL), Frontend (React, Mantine UI).

**Yêu cầu (Requirements):**
*   **Chức năng (FR):**
    *   Thực hiện đầy đủ các thao tác CRUD (thêm mới, xem danh sách/chi tiết, cập nhật, xóa) thông tin học sinh.
    *   Hỗ trợ Tìm kiếm (Mã/Tên), Lọc (Lớp/Trạng Thái), và Phân trang danh sách học sinh.
    *   **Cập nhật `FR06`:** Ngăn chặn xóa học sinh nếu có dữ liệu liên quan, trả về lỗi 409 Conflict.
    *   **API Interface:** Toàn bộ API endpoint đã được đặc tả chi tiết (method, endpoint, authorization, request/response bodies) với xác thực JWT và phân quyền theo vai trò (Admin: CRUD; Teacher: GET).
*   **Nghiệp vụ (BR):** Đảm bảo Mã Học sinh duy nhất, các trường bắt buộc, Ngày Sinh hợp lệ (< ngày hiện tại), định dạng SĐT/Email Phụ Huynh, và Trạng Thái học sinh lấy từ danh sách định nghĩa trước. Các ràng buộc này được triển khai trong logic nghiệp vụ và validation.
*   **Phi chức năng (NFR):**
    *   **Hiệu năng/Khả năng mở rộng:** Tối ưu hóa truy vấn (indexing, `AsNoTracking`), sử dụng phân trang.
    *   **Bảo mật:** Xác thực JWT, phân quyền Role-Based Access Control (RBAC) chặt chẽ ở cả tầng API và Application.
    *   **Tính nhất quán dữ liệu:** Đảm bảo qua FluentValidation và Foreign Keys ở cấp độ database.
    *   **Đồng thời:** Sử dụng `RowVersion` (xmin của PostgreSQL) để phát hiện và xử lý xung đột đồng thời.

**Ràng buộc (Constraints):**
*   Xây dựng trên nền tảng công nghệ và tuân thủ hướng dẫn UI/UX hiện có của ONENET.
*   Tuân thủ các quy định bảo mật dữ liệu cá nhân (GDPR, Luật An ninh mạng).

**Tiến độ (Progress):**
*   **Tài liệu Yêu cầu Nghiệp vụ (BRD) V1.0** đã hoàn thành và phê duyệt (2026-05-30).
*   **Software Requirements Specification (SRS) & Solution Architecture Document (SAD) V1.0** cho tính năng này **đã hoàn thành và hiện hành** (2026-05-31), sẵn sàng cho giai đoạn phát triển.

**Vấn đề/Điểm cần làm rõ (Known Issues/Open Points):**
*   **Xử lý dữ liệu liên quan khi xóa học sinh (`FR06`):** Chính sách xử lý sâu hơn (ví dụ: soft delete, cascade delete) cần được phân tích và quyết định trong tương lai. Hiện tại chỉ dừng ở việc ngăn chặn xóa cứng.
*   **Module quản lý lớp học:** Các bảng `Lop` và `TrangThaiHocSinh` đã được thiết kế như lookup tables với dữ liệu mẫu (seed data). Một module quản lý lớp học chuyên biệt trong tương lai sẽ có thể liên kết chặt chẽ hơn.