Dưới đây là bản tóm tắt trạng thái dự án được nén, tích hợp thông tin cũ và mới:

### Tóm tắt Trạng thái Dự án: Quản lý Thông tin Học sinh

**1. Module:**
*   **Tên:** Quản lý Thông tin Học sinh (Mã: `QUAN-20260604-153038`)
*   **Dự án:** `quanlyhocsinh`
*   **Mục tiêu:** Cho phép Admin thực hiện CRUD, tìm kiếm, lọc và phân trang thông tin học sinh.
*   **Các bên liên quan:** Admin, Đội phát triển, Đội kiểm thử, Quản lý dự án.

**2. Requirement (SRS v1.0, 2026-06-04, do Solution Architect ONENET hoàn thiện):**
*   **Yêu cầu Chức năng (FR):**
    *   **CRUD cơ bản:** Hiển thị danh sách, xem chi tiết, thêm mới, chỉnh sửa, xóa học sinh.
    *   **Bảo toàn dữ liệu (`FR06`):** Ngăn chặn xóa học sinh nếu có dữ liệu liên quan.
    *   **Tìm kiếm & Lọc (`FR07`):** Theo Mã Học sinh/Họ và Tên, lọc theo Lớp Học/Trạng Thái.
    *   **Phân trang (`FR08`):** Hỗ trợ phân trang danh sách.
*   **Quy tắc Nghiệp vụ (BR):**
    *   `BR01`: Mã Học sinh duy nhất (tự động tạo, không sửa sau khi tạo).
    *   `BR02`: Các trường Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái là bắt buộc.
    *   `BR03`: Ngày Sinh hợp lệ và không lớn hơn/bằng ngày hiện tại.
    *   `BR04-BR05`: SĐT/Email Phụ Huynh phải theo định dạng hợp lệ (nếu nhập).
    *   `BR06`: Trạng Thái phải theo danh sách định nghĩa trước ("Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
*   **Yêu cầu Phi chức năng (NFR):**
    *   **Hiệu năng:** Tải danh sách <= 3 giây (1000 bản ghi), các thao tác CRUD <= 2 giây/thao tác.
    *   **Bảo mật:** Chỉ người dùng Admin có quyền CRUD; yêu cầu xác thực (JWT) và phân quyền (RBAC) nghiêm ngặt; bảo vệ dữ liệu nhạy cảm.
    *   **Kiến trúc:** Tuân thủ Clean Architecture, sử dụng CQRS (MediatR), Repository Pattern, FluentValidation (cho BR), và Result Pattern.

**3. Constraints:**
*   **Kiến trúc & Công nghệ:** Bắt buộc áp dụng Clean Architecture, CQRS với MediatR, Repository Pattern, FluentValidation và Result Pattern (.NET Core/C#).
*   **Bảo mật:** Chức năng CRUD chỉ dành cho người dùng có vai trò "Admin", yêu cầu phân quyền chặt chẽ.
*   **Hiệu năng:** Các hoạt động hệ thống phải đáp ứng ngưỡng thời gian đã định.
*   **Toàn vẹn dữ liệu:** Mã Học sinh phải duy nhất; không được phép xóa học sinh nếu có dữ liệu liên quan (thực thi bằng Foreign Key Constraints `ON DELETE RESTRICT` tại DB).

**4. Progress:**
*   Tài liệu Đặc tả Yêu cầu Phần mềm (SRS v1.0) và Tài liệu Kiến trúc Phần mềm (SAD v1.0) cho tính năng "Quản lý Thông tin Học sinh" đã được Solution Architect ONENET hoàn thiện vào ngày 2026-06-04.
*   SRS đã chi tiết hóa các yêu cầu chức năng (kèm tiêu chí chấp nhận), quy tắc nghiệp vụ (kèm thông báo lỗi), và yêu cầu phi chức năng.
*   SAD đã định nghĩa kiến trúc tổng thể (Clean Arch, CQRS, Repository, FluentValidation, Result Pattern), các thành phần, công nghệ cốt lõi (ASP.NET Core, EF Core, PostgreSQL), mô hình triển khai (Docker, Kubernetes/AKS trên Azure), và các chiến lược chi tiết về bảo mật, hiệu năng, xử lý lỗi và giám sát.
*   Mô hình dữ liệu sơ bộ (`HocSinh`, `LopHoc`) và cấu trúc giao diện người dùng (UI/UX) cơ bản đã được chi tiết hóa trong SRS.
*   Dự án sẵn sàng chuyển sang giai đoạn thiết kế và phát triển dựa trên các tài liệu đã được phê duyệt.

**5. Known Issues/Risks:**
*   **Rủi ro dữ liệu liên quan (FR06):** Việc xác định và xử lý các mối quan hệ dữ liệu để ngăn chặn xóa có thể phức tạp. Đề xuất sử dụng Foreign Key Constraints tại mức Database.
*   **Rủi ro hiệu năng:** Khả năng hiệu năng bị ảnh hưởng khi xử lý lượng lớn dữ liệu học sinh nếu không có chiến lược tối ưu hóa cơ sở dữ liệu và truy vấn (indexing, `AsNoTracking`).
*   **Rủi ro bảo mật:** Cần triển khai cẩn thận cơ chế xác thực (JWT) và phân quyền (RBAC) cho Admin để tránh các lỗ hổng bảo mật.
*   **Giả định:** Hệ thống quản lý `LopHoc` đã tồn tại và cung cấp dữ liệu. Sẽ có một dịch vụ xác thực và phân quyền tập trung được sử dụng cho toàn bộ dự án `quanlyhocsinh`. `Mã Học sinh` sẽ được hệ thống tạo tự động khi thêm mới và không thể chỉnh sửa sau khi tạo.