**Tóm tắt Dự án - QuanLyHocSinh**

*   **Module:** "QuanLyHocSinh" trong dự án "quanlyhocsinh".
    *   **Mô tả:** Cung cấp công cụ toàn diện để quản lý thông tin học sinh, bao gồm CRUD, kiểm tra tính hợp lệ dữ liệu, tìm kiếm và phân trang.
    *   **Phạm vi:** Quản lý thông tin chi tiết học sinh (CRUD, tìm kiếm, phân trang, validation).
    *   **Ngoài phạm vi:** Quản lý lớp học, điểm số, lịch học, chuyên cần, tích hợp hệ thống ngoài, quản lý quyền chi tiết (các module/tính năng riêng).
*   **Requirements:**
    *   **Functional:**
        *   **Data Model:** Thực thể `HocSinh` với các trường như `MaHocSinh` (PK), `HoTen`, `NgaySinh`, `GioiTinh`, `MaLopHoc` (FK), `TrangThai`.
        *   **CRUD:** Xem danh sách, thêm mới, xem chi tiết, cập nhật, xóa học sinh (ưu tiên Soft Delete). Admin có toàn quyền; Giáo viên chỉ có quyền xem.
        *   **Tìm kiếm:** Theo Mã học sinh, Họ và tên, Lớp học (chính xác/một phần).
        *   **Phân trang:** Hỗ trợ điều hướng và chọn số lượng mục/trang.
        *   **Validation:** Kiểm tra tính hợp lệ dữ liệu chặt chẽ (bắt buộc, duy nhất, định dạng, khoảng giá trị, tồn tại FK) cho từng trường khi nhập/cập nhật.
    *   **Non-functional:** Hiệu suất (tải danh sách <= 3s, CRUD <= 2s), bảo mật (phân quyền, mã hóa), khả năng sử dụng (UI/UX trực quan, nhất quán), độ tin cậy, khả năng mở rộng, tính bảo trì.
*   **Constraints:**
    *   Sử dụng công nghệ/framework được ONENET phê duyệt.
    *   Tuân thủ quy định bảo vệ dữ liệu cá nhân.
    *   Thời gian triển khai theo lịch định.
    *   **Giả định:** Hệ thống AuthN/AuthZ và module Lớp học đã tồn tại hoặc phát triển song song; hạ tầng kỹ thuật đủ mạnh.
*   **Progress:**
    *   Tài liệu BRD-QUAN-20260530-0942-V1.0 đang ở trạng thái **Bản nháp**.
    *   **Kế hoạch tiếp theo:** BRD Review, Thiết kế UI/UX, Thiết kế Kỹ thuật, Phát triển, Kiểm thử, Triển khai.
*   **Known Issues:** Không có vấn đề cụ thể được liệt kê.