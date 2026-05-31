**Tổng hợp dự án: `QUAN-20260531-150408` - Quản Lý Môn Học**

**1. Module:**
*   **Tên:** Quản Lý Môn Học (thuộc dự án `quanlyhocsinh`).
*   **Mục tiêu:** Cung cấp chức năng CRUD (Tạo, Xem, Chỉnh sửa, Xóa), tìm kiếm và phân trang thông tin môn học cho Quản trị viên và Cán bộ Phòng Đào tạo.
*   **Phạm vi:** Tạo/cập nhật/xóa môn học (Mã, Tên, Số tín chỉ, Mô tả), xem danh sách (phân trang), tìm kiếm (Mã/Tên), kiểm tra tính hợp lệ dữ liệu.
*   **Phụ thuộc:** Module Xác thực & Phân quyền, Module Giao diện người dùng.

**2. Requirements:**
*   **Chức năng (FRs):** Xem, tạo, cập nhật, xóa danh sách môn học; tìm kiếm theo Mã/Tên; phân trang; xác thực dữ liệu đầu vào.
*   **Phi chức năng (NFRs):**
    *   **Performance:** Tải danh sách (<2s/1000 bản ghi), CRUD (<1s); hỗ trợ 50 người dùng đồng thời.
    *   **Security:** Phân quyền Admin/Giáo vụ; làm sạch dữ liệu đầu vào; ghi nhật ký Audit Trail mọi thao tác CRUD.
    *   **Availability:** Uptime >= 99.5% trong giờ làm việc.
*   **Quy tắc nghiệp vụ (BRs):** Mã/Tên môn học là duy nhất; Số tín chỉ là số nguyên dương (>0); Không xóa môn học đã có liên kết.

**3. Constraints:**
*   **Giả định:** Người dùng có quyền hạn; hệ thống cơ bản đã ổn định; Mã/Tên môn học là duy nhất.
*   **Ngoài phạm vi:** Quản lý tài liệu học tập, phân công giáo viên, đăng ký môn học, lịch học, xuất/nhập dữ liệu.
*   **Tích hợp:** Không yêu cầu tích hợp với hệ thống bên thứ ba.

**4. Progress:**
*   **BRD:** Đã hoàn thành và hiện tại.
*   **SRS/SAD:** Đang chờ BA duyệt.
*   **DEV (mã nguồn), TEST (test cases):** Đang chờ các tài liệu trước đó được duyệt.

**5. Known Issues / Validation Notes:**
*   Các trường hợp cần xử lý và hiển thị lỗi rõ ràng bao gồm: bỏ trống thông tin bắt buộc, trùng lặp Mã/Tên môn học khi tạo/cập nhật, cố gắng xóa môn học đã có liên kết, và tìm kiếm không có kết quả. Các kịch bản này đã được mô tả chi tiết trong Acceptance Criteria (Unhappy Path).