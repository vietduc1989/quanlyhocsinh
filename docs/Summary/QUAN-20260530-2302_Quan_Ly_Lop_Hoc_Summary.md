Chào bạn,

Dưới đây là bản tóm tắt cực kỳ ngắn gọn về thông tin dự án mới cho tính năng "Quản lý Lớp học":

---

### Tóm tắt dự án: Quản lý Lớp học (QUAN-20260530-2302)

*   **Module:** **Quản lý Lớp học** (thuộc hệ thống `quanlyhocsinh`)
    *   **Mục đích:** Cung cấp module tập trung để quản lý thông tin lớp học, đảm bảo tính chính xác, nhất quán dữ liệu, và hỗ trợ các module khác.
    *   **Phạm vi:** Cho phép thực hiện các thao tác CRUD (Xem danh sách, Tìm kiếm, Phân trang, Thêm mới, Cập nhật, Xóa) đối với Lớp học.
    *   **Ngoài phạm vi:** Không bao gồm quản lý thời khóa biểu, điểm số học sinh theo lớp, hoặc phân công học sinh vào lớp (chỉ xem sĩ số).

*   **Requirements (Yêu cầu):**
    *   **Chức năng:**
        *   **Xem/Tìm kiếm/Phân trang:** Hiển thị danh sách lớp học (Mã, Tên, Niên khóa, Sĩ số, GVCN), hỗ trợ tìm kiếm theo nhiều tiêu chí (Mã, Tên, Niên khóa, GVCN) và phân trang linh hoạt.
        *   **Thêm mới:** Giao diện nhập thông tin lớp học (Mã, Tên, Niên khóa, GVCN), áp dụng validation.
        *   **Cập nhật:** Chỉnh sửa thông tin lớp học (Tên, Niên khóa, GVCN), Mã lớp không thay đổi, áp dụng validation.
        *   **Xóa:** Cho phép xóa lớp học nếu không có ràng buộc dữ liệu.
        *   **Ngăn chặn xóa:** Từ chối xóa nếu lớp học có học sinh hoặc giáo viên chủ nhiệm đang hoạt động, hiển thị thông báo lỗi rõ ràng.
    *   **Quy tắc nghiệp vụ (Validation):**
        *   **Mã lớp:** Bắt buộc, duy nhất, tối đa 20 ký tự.
        *   **Tên lớp:** Bắt buộc, tối đa 50 ký tự.
        *   **Niên khóa:** Bắt buộc, định dạng YYYY hoặc YYYY-YYYY, tối đa 9 ký tự.
        *   **GVCN:** Tùy chọn, phải là ID giáo viên hiện có trong hệ thống.
    *   **Phi chức năng:**
        *   **Hiệu suất:** Tải danh sách (<1000 bản ghi) < 3s; CRUD < 2s.
        *   **Bảo mật:** Chỉ "Quản trị viên hệ thống" có quyền CRUD; "Giáo viên" chỉ xem.
        *   **Khả năng sử dụng:** Giao diện trực quan, thông báo rõ ràng.
        *   **Toàn vẹn dữ liệu:** Đảm bảo ràng buộc giữa Lớp học với Học sinh/Giáo viên.

*   **Constraints (Ràng buộc):**
    *   **Tích hợp:** Phải tích hợp với module "Học sinh" (để cập nhật sĩ số) và "Giáo viên" (để gán GVCN).
    *   **Công nghệ:** Phát triển trên nền tảng công nghệ hiện có của `quanlyhocsinh`.
    *   **Dữ liệu:** Dữ liệu học sinh, giáo viên được quản lý từ các module riêng biệt.

*   **Progress (Tiến độ):**
    *   Tài liệu BRD cho tính năng "Quản lý Lớp học" (**QUAN-20260530-2302**) đã **Hoàn thành**.

*   **Known Issues (Vấn đề đã biết):**
    *   Không có vấn đề đã biết được nêu trong BRD.

---