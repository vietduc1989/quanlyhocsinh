Dưới đây là bản tóm tắt cực kỳ ngắn gọn và cập nhật về thông tin dự án cho tính năng "Quản lý Lớp học":

---

### Tóm tắt dự án: Quản lý Lớp học (QUAN-20260530-2302)

Dự án phát triển module cốt lõi "Quản lý Lớp học" trong hệ thống `quanlyhocsinh` nhằm chuẩn hóa việc quản lý thông tin lớp học.

*   **Module & Phạm vi:**
    *   Cung cấp các thao tác CRUD (Xem danh sách/tìm kiếm/phân trang, Thêm, Cập nhật, Xóa) cho thông tin lớp học.
    *   **Ngoài phạm vi:** Không quản lý thời khóa biểu, điểm số, hoặc phân công học sinh vào lớp (chỉ xem sĩ số hiện tại).

*   **Yêu cầu (Requirements):**
    *   **Chức năng:**
        *   **API RESTful:** Triển khai các API (`GET`, `POST`, `PUT`, `DELETE` tại `/api/v1/classes`) hỗ trợ CRUD.
        *   **Xem/Tìm kiếm/Phân trang:** Hiển thị danh sách lớp (Mã, Tên, Niên khóa, Sĩ số, GVCN). Hỗ trợ tìm kiếm đa tiêu chí và phân trang.
        *   **Thêm/Cập nhật:** Giao diện nhập liệu với validation chặt chẽ (Mã lớp duy nhất, Tên, Niên khóa bắt buộc; GVCN phải tồn tại). Mã lớp không được sửa khi cập nhật.
        *   **Xóa:** Cho phép xóa nếu không có ràng buộc dữ liệu. **Ngăn chặn xóa** nếu lớp có học sinh đang theo học hoặc có GVCN được gán (phản hồi 409 Conflict). Xóa cứng sẽ được áp dụng.
    *   **Quy tắc nghiệp vụ:** Mã lớp (duy nhất, bắt buộc, max 20), Tên lớp (bắt buộc, max 50), Niên khóa (bắt buộc, định dạng YYYY hoặc YYYY-YYYY, max 9), GVCN (ID giáo viên hợp lệ hoặc null).
    *   **Phi chức năng:**
        *   **Hiệu suất:** Tải danh sách (< 3s), CRUD (< 2s). Tối ưu bằng `AsNoTracking()`, indexes, phân trang, `SELECT` projection.
        *   **Bảo mật:** AuthN/AuthZ qua JWT. "Quản trị viên hệ thống" có quyền CRUD. "Giáo viên" và "Ban giám hiệu" chỉ có quyền xem (GET).
        *   **Toàn vẹn dữ liệu:** Đảm bảo ràng buộc DB (`DeleteBehavior.Restrict` cho Students-Classes) và logic ứng dụng.

*   **Ràng buộc (Constraints):**
    *   **Tích hợp:** Phải tích hợp với module "Học sinh" (cập nhật sĩ số) và "Giáo viên" (gán GVCN).
    *   **Công nghệ:** Phát triển trên nền tảng công nghệ hiện có của `quanlyhocsinh`. Dữ liệu học sinh, giáo viên được quản lý từ các module riêng biệt.

*   **Kiến trúc & Công nghệ:**
    *   **Công nghệ:** Backend (.NET 10, ASP.NET Web API, EF Core), Frontend (React + Mantine UI), Database (PostgreSQL).
    *   **Kiến trúc:** Clean Architecture, CQRS + MediatR.
    *   **Thiết kế DB:** Bảng `Classes` với các index quan trọng (ClassCode UK, HomeroomTeacherId, SchoolYear) và mối quan hệ với `Students` (FK `ClassId`, `OnDelete.Restrict`) và `Teachers` (FK `HomeroomTeacherId`). Sử dụng cột `Version` cho Optimistic Concurrency.
    *   **Logic ứng dụng:** Cụ thể hóa qua các Command/Query và Validators (ví dụ: `CreateClassCommand`, `GetClassesQuery`). Sơ đồ tuần tự minh họa luồng "Tạo Lớp học mới" đã được cung cấp.

*   **Tiến độ:**
    *   Tài liệu BRD đã hoàn thành.
    *   **Tài liệu SRS/SAD (`QUAN-20260530-2302`) đã được duyệt và hiện là tài liệu hiện hành.**
    *   Các tài liệu DEV và TEST đang chờ duyệt.

*   **Vấn đề đã biết (Known Issues):** Không có vấn đề đã biết được nêu trong BRD hoặc SRS/SAD.

*   **Rủi ro & Giảm thiểu:** Đã xác định các rủi ro chính về Concurrency (sử dụng Optimistic Concurrency), Toàn vẹn dữ liệu (kiểm tra tầng ứng dụng & DB `Restrict`), Hiệu suất (phân trang, index, `AsNoTracking()`) và Bảo mật (AuthN/AuthZ chặt chẽ). Kèm theo đó là các phương án giảm thiểu cụ thể.

---