### Tóm tắt Trạng thái Dự án: Quản lý Thông tin Học sinh

**1. Module:**
*   **Tên:** Quản lý Thông tin Học sinh (Mã: `QUAN-20260604-153038`)
*   **Dự án:** `quanlyhocsinh`
*   **Mục tiêu:** Cho phép người quản trị (Admin) thực hiện các thao tác quản lý cơ bản (CRUD: Thêm, Xem, Sửa, Xóa) thông tin học sinh, cùng với các chức năng tìm kiếm, lọc và phân trang danh sách.
*   **Các bên liên quan chính:** Admin, Đội phát triển, Đội kiểm thử, Quản lý dự án.

**2. Requirement (SRS v1.0, 2026-06-04):**
*   **Yêu cầu Chức năng (FR):**
    *   **CRUD cơ bản:** Hiển thị danh sách (`FR01`), xem chi tiết (`FR02`), thêm mới (`FR03`), chỉnh sửa (`FR04`), xóa học sinh (`FR05`).
    *   **Bảo toàn dữ liệu:** Ngăn chặn xóa học sinh nếu có dữ liệu liên quan (`FR06`).
    *   **Tìm kiếm & Lọc:** Tìm kiếm theo Mã Học sinh/Họ và Tên, lọc theo Lớp Học/Trạng Thái (`FR07`).
    *   **Trải nghiệm người dùng:** Hỗ trợ phân trang danh sách học sinh (`FR08`).
*   **Quy tắc Nghiệp vụ (BR):**
    *   `BR01`: Mã Học sinh phải là duy nhất.
    *   `BR02`: Các trường Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái là bắt buộc.
    *   `BR03`: Ngày Sinh phải hợp lệ và không lớn hơn/bằng ngày hiện tại.
    *   `BR04-BR05`: Số Điện Thoại/Email Phụ Huynh phải theo định dạng hợp lệ (nếu nhập).
    *   `BR06`: Trạng Thái phải là một trong các giá trị định nghĩa trước ("Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
*   **Yêu cầu Phi chức năng (NFR):**
    *   **Hiệu năng:** Tải danh sách <= 3 giây (1000 bản ghi), các thao tác CRUD <= 2 giây/thao tác.
    *   **Bảo mật:** Chỉ người dùng Admin mới có quyền CRUD; yêu cầu xác thực và phân quyền nghiêm ngặt; bảo vệ dữ liệu nhạy cảm.
    *   **Kiến trúc:** Tuân thủ Clean Architecture, sử dụng CQRS (MediatR), Repository Pattern, FluentValidation (cho BR), và Result Pattern.

**3. Constraints:**
*   **Kiến trúc & Công nghệ:** Bắt buộc áp dụng Clean Architecture, CQRS với MediatR, Repository Pattern, FluentValidation và Result Pattern.
*   **Bảo mật:** Chức năng CRUD chỉ dành cho người dùng có vai trò "Admin", yêu cầu phân quyền chặt chẽ.
*   **Hiệu năng:** Các hoạt động hệ thống phải đáp ứng ngưỡng thời gian đã định.
*   **Toàn vẹn dữ liệu:** Mã Học sinh phải duy nhất; không được phép xóa học sinh nếu có dữ liệu liên quan.

**4. Progress:**
*   Tài liệu Đặc tả Yêu cầu Phần mềm (SRS) cho tính năng "Quản lý Thông tin Học sinh" (phiên bản 1.0, ngày 2026-06-04) đã được hoàn thiện.
*   Đã chi tiết hóa các yêu cầu chức năng, quy tắc nghiệp vụ, yêu cầu phi chức năng.
*   Đã xây dựng mô hình dữ liệu sơ bộ cho thực thể `HocSinh` và `LopHoc`, cùng cấu trúc giao diện người dùng (UI/UX) cơ bản.
*   Dự án sẵn sàng chuyển sang giai đoạn thiết kế và phát triển.

**5. Known Issues/Risks:**
*   **Rủi ro dữ liệu liên quan (FR06):** Việc xác định và xử lý các mối quan hệ dữ liệu để ngăn chặn xóa có thể phức tạp, dễ phát sinh lỗi và tốn thời gian.
*   **Rủi ro hiệu năng:** Khả năng hiệu năng bị ảnh hưởng khi xử lý lượng lớn dữ liệu học sinh nếu không có chiến lược tối ưu hóa cơ sở dữ liệu và truy vấn phù hợp.
*   **Rủi ro bảo mật:** Cần triển khai cẩn thận cơ chế xác thực và phân quyền cho Admin để tránh các lỗ hổng bảo mật.
*   **Giả định:** Hệ thống quản lý `LopHoc` đã tồn tại và cung cấp dữ liệu. Phương thức tạo `Mã Học sinh` (tự động hay thủ công) cần được xác nhận rõ ràng.