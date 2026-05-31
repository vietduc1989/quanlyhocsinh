**Tóm tắt Dự án Hiện tại:**

*   **Module:** `quanlyhocsinh` - Tính năng `Test Concurrency ThreadSafe` (Mã: `QUAN-20260531-1443`)
    *   **Mục tiêu chính:** Cung cấp quản lý thông tin học sinh (CRUD, tìm kiếm, phân trang) đảm bảo tính toàn vẹn dữ liệu trong môi trường đa luồng (thread-safe) và xử lý đồng thời.
    *   **Phạm vi:** Xây dựng giao diện và nghiệp vụ CRUD cho học sinh; triển khai tìm kiếm, phân trang; xác thực dữ liệu đầu vào; đảm bảo tính thread-safe và cơ chế xử lý xung đột dữ liệu (concurrency control) bằng Optimistic Locking.
    *   **Ngoài phạm vi:** Quản lý điểm số, import/export hàng loạt, báo cáo, tích hợp hệ thống bên ngoài, quản lý danh mục chung.

*   **Yêu cầu (Requirements):**
    *   **Chức năng (FRs):**
        *   `FR01-04`: Quản lý CRUD (Tạo, Cập nhật, Xem, Xóa) thông tin học sinh.
        *   `FR05`: Tìm kiếm học sinh theo nhiều tiêu chí.
        *   `FR06`: Phân trang danh sách học sinh.
        *   `FR07`: Xác thực chặt chẽ dữ liệu đầu vào cho thông tin học sinh.
    *   **Phi chức năng (NFRs):**
        *   **Hiệu năng:** Thời gian phản hồi CRUD < 2s (50 người dùng đồng thời); Khả năng chịu tải đọc > 100 req/s, ghi > 20 req/s; Tìm kiếm/phân trang 1-2s (10k HS).
        *   **Bảo mật:** Xác thực/phân quyền (chỉ Quản trị viên), bảo vệ dữ liệu nhạy cảm, chống tấn công (SQLi, XSS, CSRF).
        *   **Tính khả dụng:** Uptime tối thiểu 99.5%; khả năng phục hồi nhanh chóng và nhất quán dữ liệu.
    *   **Quy tắc nghiệp vụ (BRs):**
        *   `BR01`: Mã Học sinh duy nhất.
        *   `BR02`: Tên Học sinh không được để trống.
        *   `BR03`: Xử lý cập nhật đồng thời (Concurrency Control) bằng Optimistic Locking.
        *   `BR04`: Giới hạn độ dài tên học sinh (<= 100 ký tự).
    *   **Yêu cầu khác:** Ghi lại lịch sử thao tác (Audit Trail) cho các thay đổi trên thông tin học sinh.

*   **Ràng buộc (Constraints):**
    *   **Giả định:** Người dùng có vai trò Quản trị viên, cấu trúc CSDL và các trường thông tin học sinh đã định nghĩa, hệ thống xác thực/phân quyền đã tồn tại, môi trường hệ thống hỗ trợ xử lý đa luồng và tải cao.
    *   **Phụ thuộc:** Module quản lý quyền truy cập và xác thực người dùng, CSDL và hạ tầng hệ thống đã triển khai, API/dịch vụ tương tác CSDL học sinh.

*   **Tiến độ (Progress):**
    *   BRD (tài liệu này): ✅ Hoàn thành.
    *   SRS/SAD: ⏳ Chờ BA duyệt.
    *   DEV (mã nguồn): ⏳ Chờ SRS duyệt.
    *   TEST (test cases): ⏳ Chờ DEV duyệt.

*   **Vấn đề đã biết (Known Issues):** Không có vấn đề nào được nêu rõ trong tài liệu này.