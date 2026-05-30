Dưới đây là bản tóm tắt cực kỳ ngắn gọn, giữ lại các context quan trọng nhất từ thông tin BRD mới:

---

### **Memory Summary - Quản Lý Môn Học**

*   **Module:** **Quản Lý Môn Học** (Subject Management) trong hệ thống `quanlyhocsinh`.
    *   **Mục tiêu:** Cung cấp chức năng CRUD (Tạo, Đọc, Cập nhật, Xóa) cho danh sách môn học, kèm theo tìm kiếm và phân trang, phục vụ quản trị và làm nền tảng cho các module khác (đăng ký, điểm số).
*   **Requirements:**
    *   **Functional:**
        *   **CRUD Môn học:** Hiển thị danh sách (Mã, Tên, Mô tả, Số tín chỉ, Trạng thái), thêm mới, cập nhật thông tin (Mã không sửa), xóa môn học.
        *   **Tìm kiếm & Phân trang:** Tìm kiếm theo Mã/Tên môn học (tương đối, không phân biệt hoa/thường), hỗ trợ phân trang (mặc định 20 mục/trang, tùy chỉnh).
        *   **Quy tắc nghiệp vụ chính:** Mã môn học duy nhất, không chứa KĐB, độ dài 3-10. Tên môn học bắt buộc. Số tín chỉ là số nguyên dương (>=1). Không được xóa môn học nếu có sinh viên đăng ký hoặc đã có điểm số. Trạng thái mặc định khi thêm mới là "Đang hoạt động".
    *   **Non-Functional:**
        *   **Hiệu năng:** Tải danh sách (5000 môn) <3s, CRUD <1s, tìm kiếm (5000 môn) <1s.
        *   **Bảo mật:** Chỉ QTV/người có quyền, HTTPS, ghi log thao tác CRUD.
        *   **Khả dụng:** Giao diện trực quan, thông báo rõ ràng.
        *   **Khả năng mở rộng:** Hỗ trợ tới 10.000 môn học.
*   **Constraints:**
    *   **Kỹ thuật:** Xây dựng trên nền tảng/DB hiện có của `quanlyhocsinh`, tuân thủ tiêu chuẩn ONENET.
    *   **Vận hành:** Hoạt động ổn định trong môi trường máy chủ hiện tại.
    *   **Tích hợp:** Tích hợp liền mạch UI/API vào hệ thống `quanlyhocsinh` và các module liên quan (QL Lớp, Đăng ký).
*   **Progress:** BRD (Mã: QUAN-20260530-2250, v1.0) đã hoàn thành và được phê duyệt vào ngày 2026-05-30, xác định rõ yêu cầu nghiệp vụ cho tính năng.
*   **Known Issues/Risks:**
    *   **Dữ liệu không nhất quán:** Giảm thiểu bằng validation client/server chặt chẽ, áp dụng BRs, transaction.
    *   **Hiệu năng kém với dữ liệu lớn:** Giảm thiểu bằng tối ưu DB (index), phân trang hiệu quả, caching.
    *   **Xóa nhầm dữ liệu quan trọng:** Giảm thiểu bằng BR05 (ràng buộc xóa), hộp thoại xác nhận rõ ràng, cân nhắc soft delete.

---