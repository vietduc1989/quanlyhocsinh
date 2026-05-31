**Tóm tắt trạng thái dự án**

**Module:**
*   **Tên tính năng:** Quản lý Môn học (`QUAN-20260531-1452`)
*   **Dự án:** `quanlyhocsinh`
*   **Mục tiêu chính:** Cung cấp hệ thống quản lý môn học hiệu quả (tạo, xem, cập nhật, xóa, tìm kiếm, phân trang) cho cán bộ quản lý, đảm bảo tính nhất quán và tự động hóa quy trình.

**Requirement:**
*   **Yêu cầu chức năng (FRs):**
    *   Hỗ trợ đầy đủ các thao tác CRUD (Tạo mới, Xem danh sách, Xem chi tiết, Cập nhật, Xóa) thông tin môn học.
    *   Cung cấp chức năng tìm kiếm môn học theo mã hoặc tên.
    *   Thực hiện phân trang cho danh sách môn học.
    *   Kiểm tra tính hợp lệ của dữ liệu đầu vào (validation) khi tạo mới và cập nhật.
*   **Yêu cầu nghiệp vụ (BRs):**
    *   Tên và mã môn học là bắt buộc và phải là duy nhất.
    *   Tên môn học tối đa 255 ký tự, mã môn học tối đa 50 ký tự.
    *   Không thể xóa môn học nếu đang được gán cho lớp học hoặc chương trình học.
    *   Trạng thái mặc định khi tạo mới là "Hoạt động".
*   **Yêu cầu phi chức năng (NFRs):**
    *   **Hiệu suất:** Tải danh sách (1000 bản ghi) < 3 giây; các thao tác CRUD/Tìm kiếm < 1 giây. Hỗ trợ 50 người dùng đồng thời.
    *   **Bảo mật:** Chỉ Cán bộ quản lý/Quản trị viên được truy cập. Sử dụng HTTPS. Ngăn chặn SQL Injection, XSS, CSRF.
    *   **Khả dụng:** Uptime 99.5% hàng tháng.
*   **Yêu cầu tích hợp:** Tích hợp nội bộ với các module khác của hệ thống `quanlyhocsinh` (ví dụ: quản lý lớp học, thời khóa biểu).
*   **Audit Trail:** Ghi nhật ký đầy đủ cho các thao tác Tạo, Cập nhật, Xóa môn học (bao gồm thời gian, người thực hiện, loại thao tác, ID môn học và nội dung thay đổi).

**Constraints:**
*   **Ngoài phạm vi (Out of Scope):** Gán môn học cho giáo viên/lớp học, quản lý học liệu, xuất/nhập khẩu, phân quyền chi tiết (chỉ phân quyền tổng quát Admin), lịch sử thay đổi chi tiết từng trường dữ liệu.
*   **Giả định (Assumptions):** Người dùng được xác thực và có quyền phù hợp, hệ thống CSDL sẵn sàng, môi trường mạng ổn định, mã môn học là duy nhất.
*   **Phụ thuộc (Dependencies):** Cần module Xác thực & Phân quyền hiện có trong hệ thống.

**Progress:**
*   **BRD** (Business Requirements Document) cho tính năng "Quản lý Môn học" (`QUAN-20260531-1452`) đã **hoàn thành** (phiên bản 1.0).
*   Tài liệu **SRS/SAD** đang **chờ BA duyệt**.
*   Công việc **phát triển (DEV)** và **kiểm thử (TEST)** đang **chờ duyệt các tài liệu trước đó**.

**Known Issues:**
*   Chưa có vấn đề nổi bật nào được xác định hoặc ghi nhận trong tài liệu BRD này.