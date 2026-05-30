**Tóm tắt Dự án:**

**Module:** Quản lý Thông tin Học sinh (Mã: `QUAN-20260530-2301`)
*   Thuộc dự án: `quanlyhocsinh` - nền tảng quản lý trường học/trung tâm giáo dục.
*   Vai trò: Thành phần cốt lõi, duy trì và tra cứu dữ liệu cơ bản về học sinh.
*   Người dùng chính: Người quản trị (Administrator) - có toàn quyền CRUD, tìm kiếm, lọc, phân trang.

**Yêu cầu (Requirements):**

*   **Chức năng (FR):**
    *   Hiển thị danh sách, xem chi tiết học sinh (`FR01`, `FR02`).
    *   Thêm mới, cập nhật, xóa thông tin học sinh (`FR03`, `FR04`, `FR05`).
    *   Tìm kiếm (Mã/Tên) & Lọc (Lớp/Trạng Thái) (`FR07`).
    *   Phân trang danh sách học sinh (`FR08`).
    *   Ngăn chặn xóa học sinh có dữ liệu liên quan (`FR06` - chỉ cảnh báo, chưa xử lý sâu).
*   **Nghiệp vụ (BR):**
    *   Mã Học sinh là duy nhất (`BR01`).
    *   Các trường bắt buộc: Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái (`BR02`).
    *   Ngày Sinh hợp lệ (< ngày hiện tại) (`BR03`).
    *   Định dạng hợp lệ cho SĐT Phụ Huynh, Email Phụ Huynh (nếu nhập) (`BR04`, `BR05`).
    *   Trạng Thái học sinh phải từ danh sách định nghĩa trước (`BR06`).
*   **Phi chức năng (NFR):**
    *   **Hiệu năng:** Tải danh sách (<1000 bản ghi) < 3s; CRUD < 2s.
    *   **Bảo mật:** Chỉ Admin thực hiện CRUD; bảo vệ dữ liệu truy cập trái phép.
    *   **Khả năng sử dụng:** UI trực quan, dễ dùng, theo chuẩn ONENET UI/UX.
    *   **Khả năng mở rộng:** Xử lý hàng chục nghìn bản ghi.

**Ràng buộc (Constraints):**

*   Xây dựng trên nền tảng công nghệ và tuân thủ hướng dẫn UI/UX hiện có của ONENET.
*   Tuân thủ quy định bảo mật dữ liệu cá nhân (GDPR, Luật An ninh mạng).

**Tiến độ (Progress):**

*   Đã hoàn thành và phê duyệt **Tài liệu Yêu cầu Nghiệp vụ (BRD) V1.0** (Ngày: 2026-05-30) cho tính năng "Quản lý Thông tin Học sinh".
*   BRD mô tả chi tiết các yêu cầu chức năng, phi chức năng, quy tắc nghiệp vụ, use cases và tiêu chí chấp nhận.

**Vấn đề/Điểm cần làm rõ (Known Issues/Open Points):**

*   **Xử lý dữ liệu liên quan khi xóa học sinh (`FR06`):** Cần phân tích sâu hơn về chính sách xử lý (ví dụ: xóa cascade, đánh dấu không hoạt động) trong tương lai. Hiện tại chỉ dừng ở cảnh báo và ngăn chặn xóa.
*   **Module quản lý lớp học:** Trường `LopHoc` có thể được liên kết qua ID nếu có module quản lý lớp học riêng.