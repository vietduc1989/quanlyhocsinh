### Tóm tắt Trạng thái Dự án: `QUAN-20260531-1452`

**Module:**
*   **Mã tính năng:** `QUAN-20260531-1452`
*   **Tên tính năng:** Quản Lý Điểm Học Kỳ
*   **Dự án:** `quanlyhocsinh`
*   **Phiên bản:** 1.0
*   **Mục tiêu chính:** Cung cấp tính năng quản lý điểm học kỳ (thêm, xem, sửa, xóa, tìm kiếm, lọc, phân trang) nhằm đảm bảo tính chính xác, hiệu quả và toàn vẹn dữ liệu điểm, hỗ trợ công tác học vụ và cải thiện trải nghiệm người dùng thông qua phân quyền chặt chẽ.

**Requirement:**
*   **Chức năng chính (FRs):**
    *   Xem, thêm, cập nhật, xóa bản ghi điểm học kỳ.
    *   Tìm kiếm và lọc danh sách điểm theo nhiều tiêu chí (tên HS, mã HS, môn học, lớp, học kỳ, năm học).
    *   Phân trang và sắp xếp danh sách điểm.
    *   Kiểm tra hợp lệ dữ liệu đầu vào (validation) cho điểm số và các trường liên quan.
    *   Áp dụng cơ chế phân quyền truy cập và thao tác chặt chẽ theo vai trò (Admin, Giáo viên Bộ môn, Giáo viên Chủ nhiệm).
*   **Yêu cầu phi chức năng (NFRs):**
    *   **Performance:** Thời gian tải trang <2s (với 100 bản ghi), thao tác CRUD <1s. Hỗ trợ 50 người dùng đồng thời.
    *   **Security:** Yêu cầu xác thực & ủy quyền, phân quyền rõ ràng, bảo vệ & mã hóa dữ liệu điểm.
    *   **Availability:** Khả dụng 99.5% trong giờ làm việc.
    *   **Usability:** Giao diện trực quan, dễ sử dụng, thông báo lỗi/thành công rõ ràng.
*   **Quy tắc nghiệp vụ (BRs):**
    *   Điểm số hợp lệ: Số thực trong khoảng 0.0 đến 10.0 (hoặc 0-100).
    *   Duy nhất bản ghi điểm: Mỗi học sinh chỉ có một điểm cho một môn học, học kỳ và năm học cụ thể.
    *   Trường bắt buộc: Học sinh, Môn học, Lớp, Điểm số, Học kỳ, Năm học.
    *   Quyền cập nhật điểm: GVBM theo môn được phân công, GVCN theo lớp chủ nhiệm, Admin toàn quyền.
    *   Dữ liệu tham chiếu (Học sinh, Môn học, Lớp) phải tồn tại trong hệ thống.
*   **Audit Trail:** Hệ thống phải ghi lại lịch sử chi tiết các thao tác thêm, cập nhật, xóa điểm (người thực hiện, thời gian, giá trị cũ/mới).
*   **KPI & Metrics:** Theo dõi số lượng bản ghi thêm/cập nhật, tỷ lệ lỗi validation, thời gian thao tác CRUD, số lượt tìm kiếm/lọc, số lượng người dùng hoạt động.

**Constraints:**
*   **Ngoài phạm vi (Out of Scope):**
    *   Không tự động tính toán điểm trung bình/tổng kết.
    *   Không tích hợp với LMS hoặc hệ thống bên ngoài khác.
    *   Không có chức năng import/export dữ liệu hàng loạt.
    *   Không có giao diện xem điểm riêng cho học sinh/phụ huynh.
    *   Không có chức năng xét duyệt hoặc khóa sổ điểm.
*   **Giả định (Assumptions):**
    *   Dữ liệu cơ bản (Học sinh, Lớp học, Môn học, Giáo viên, Phân công giảng dạy) đã tồn tại trong hệ thống `quanlyhocsinh`.
    *   Người dùng được xác thực thông qua hệ thống quản lý tài khoản của ONENET.
    *   Mã học kỳ và năm học đã được định nghĩa.
    *   Người dùng có kết nối internet ổn định.
*   **Phụ thuộc (Dependencies):**
    *   Module Quản lý Học sinh
    *   Module Quản lý Môn học
    *   Module Quản lý Lớp học
    *   Module Quản lý Người dùng/Phân quyền

**Progress:**
*   **BRD (tài liệu này):** ✅ Đã hoàn thành và hiện tại.
*   **SRS/SAD:** ⏳ Đang chờ BA duyệt.
*   **DEV (mã nguồn):** ⏳ Đang chờ SRS duyệt.
*   **TEST (test cases):** ⏳ Đang chờ DEV duyệt.

**Known Issues:**
*   Không có vấn đề nổi bật nào được ghi nhận trong tài liệu này.