Dưới đây là bản tóm tắt cực kỳ ngắn gọn về dự án "Quan Ly Diem Hoc Ky" dựa trên thông tin mới nhất:

**Module:** Quan Ly Diem Hoc Ky (`QUAN-20260531-154643`) thuộc dự án "quanlyhocsinh", phiên bản 1.0. Mục tiêu là cung cấp hệ thống quản lý (tạo, xem, cập nhật, xóa) điểm học kỳ của học sinh cho Giáo viên và Quản trị viên, đảm bảo tính chính xác, hiệu quả và toàn vẹn dữ liệu.

**Requirements:**
*   **Chức năng:** Hỗ trợ CRUD điểm học kỳ; xem danh sách có phân trang; xem chi tiết; tìm kiếm theo HS, môn, học kỳ; xác thực dữ liệu đầu vào.
*   **Phi chức năng:**
    *   **Hiệu suất:** Tải trang danh sách (<3s/500 bản ghi), phản hồi CRUD (<2s), tìm kiếm (<3s/10k bản ghi).
    *   **Bảo mật:** Xác thực/phân quyền chặt chẽ (Giáo viên theo phạm vi, QTV toàn quyền), chống SQLI/XSS, HTTPS.
    *   **Khả dụng:** Uptime >99.5% (24/7).
*   **Quy tắc nghiệp vụ:** Điểm từ 0.0-10.0 (tối đa 2 thập phân); duy nhất 1 bản ghi điểm/HS/môn/học kỳ; các trường Học sinh, Môn học, Học kỳ, Điểm số là bắt buộc.
*   **Audit Trail:** Ghi nhật ký đầy đủ các thao tác CRUD.

**Constraints:**
*   **Ngoài phạm vi:** Không tính toán điểm trung bình/tổng kết; không in/xuất báo cáo; không tích hợp bên thứ 3; không quản lý điểm thành phần; không nhập/xuất dữ liệu hàng loạt.
*   **Giả định:** Dữ liệu học sinh, môn học, lớp học, học kỳ đã có trong hệ thống ONENET; người dùng được xác thực và phân quyền; điểm là điểm học kỳ cuối cùng.
*   **Phụ thuộc:** Sử dụng dữ liệu từ các module nội bộ ONENET (Quản lý Học sinh, Môn học, Học kỳ) và hệ thống Xác thực & Phân quyền.

**Progress:**
*   BRD (tài liệu này) đã hoàn thành và hiện tại.
*   SRS/SAD, mã nguồn (DEV) và test cases (TEST) đang ở trạng thái chờ duyệt hoặc chờ bắt đầu phát triển.
*   Luồng nghiệp vụ đã được định nghĩa chi tiết (Mermaid Diagram) và các tiêu chí chấp nhận (BDD) đã được xác định cho cả luồng chính và luồng lỗi.

**Known Issues:**
*   Hiện tại không có lỗi hệ thống, nhưng BRD đã xác định các tình huống cạnh cần xử lý:
    *   Xác thực dữ liệu chặt chẽ cho điểm số và các trường bắt buộc.
    *   Ngăn chặn việc tạo điểm trùng lặp cho cùng một học sinh, môn học và học kỳ.
    *   Đảm bảo kiểm soát quyền hạn theo vai trò (Giáo viên chỉ thao tác trong phạm vi được giao).