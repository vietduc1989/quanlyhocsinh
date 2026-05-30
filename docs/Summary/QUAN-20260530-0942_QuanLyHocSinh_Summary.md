Dưới đây là bản tóm tắt dự án "QuanLyHocSinh" được nén lại, tích hợp thông tin mới từ SRS và SAD:

---

**Tóm tắt Dự án - QuanLyHocSinh (Cập nhật SRS/SAD)**

*   **Module:** "QuanLyHocSinh" (thuộc dự án `quanlyhocsinh`) là module cốt lõi, cung cấp công cụ toàn diện để quản lý thông tin học sinh (CRUD, kiểm tra tính hợp lệ dữ liệu, tìm kiếm, phân trang). Đây là nền tảng cho các module quản lý khác trong tương lai.
*   **Requirements:**
    *   **Functional:**
        *   **Data Model:** Thực thể `HocSinh` với các trường chi tiết như `MaHocSinh` (PK), `HoTen`, `NgaySinh`, `GioiTinh`, `DiaChi`, `SoDienThoaiPH`, `EmailPH`, `MaLopHoc` (FK), `TrangThai` (hỗ trợ xóa mềm), cùng các trường audit tự động (`NgayTao`, `NguoiTao`, `NgayCapNhatCuoi`, `NguoiCapNhatCuoi`).
        *   **CRUD & Phân quyền:** Quản trị viên có toàn quyền CRUD (thêm, xem, sửa, xóa mềm). Giáo viên chỉ có quyền xem danh sách, chi tiết, tìm kiếm và phân trang.
        *   **Tìm kiếm & Phân trang:** Tìm kiếm theo Mã HS, Họ tên, Lớp học (chính xác/một phần); phân trang linh hoạt (10, 25, 50, 100 mục/trang).
        *   **Validation:** Kiểm tra dữ liệu chặt chẽ ở cả Client-side và Server-side, bao gồm các ràng buộc về định dạng, khoảng giá trị, duy nhất, và kiểm tra tồn tại `MaLopHoc` qua API của module Lớp học.
    *   **Non-functional:** Đảm bảo hiệu suất cao (tải danh sách <=3s/1000 bản ghi, thao tác CRUD <=2s, tìm kiếm <=2s), bảo mật (tích hợp AuthN/AuthZ của ONENET, HTTPS, validation đầu vào), khả năng sử dụng (UI/UX trực quan theo chuẩn ONENET), độ tin cậy, khả năng mở rộng (kiến trúc stateless, DB replication), và tính bảo trì (thiết kế modular, tài liệu rõ ràng, clean code).
*   **Constraints:** Tuân thủ công nghệ/framework của ONENET, quy định bảo vệ dữ liệu cá nhân, và lịch trình dự án.
    *   **Giả định/Phụ thuộc:** Hệ thống AuthN/AuthZ và module Lớp học của ONENET đã tồn tại hoặc đang phát triển song song, cung cấp API để tra cứu/xác thực `MaLopHoc`; hạ tầng kỹ thuật đủ mạnh.
*   **Progress:**
    *   BRD-QUAN-20260530-0942-V1.0 đang ở trạng thái **Bản nháp**.
    *   **Cập nhật quan trọng:** Tài liệu Yêu cầu Phần mềm (SRS-V1.0) và Tài liệu Kiến trúc Phần mềm (SAD-V1.0) cho tính năng "QuanLyHocSinh" đã được Solution Architect của ONENET lập và hiện đang ở trạng thái **Bản nháp**. SAD chi tiết kiến trúc phân lớp (Frontend, Backend RESTful API, Database), tích hợp với các hệ thống AuthN/AuthZ và Module Lớp học bên ngoài, cùng với kế hoạch triển khai sử dụng Docker/Kubernetes và ngăn xếp công nghệ tiêu chuẩn của ONENET (ví dụ: Spring Boot, PostgreSQL, React/Vue/Angular).
*   **Known Issues:** Không có vấn đề cụ thể được liệt kê. Cần lưu ý trạng thái "Bản nháp" của BRD, SRS và SAD đòi hỏi các vòng review và phê duyệt tiếp theo.

---