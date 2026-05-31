Dưới đây là bản tóm tắt cực kỳ ngắn gọn về thông tin dự án "Quan Ly Diem Hoc Ky":

---

**Tóm tắt Dự án: Quan Ly Diem Hoc Ky (`QUAN-20260531-150407`)**

**Dự án:** `quanlyhocsinh`
**Tính năng:** Quản lý Điểm Học Kỳ
**Mục tiêu chính:** Cung cấp hệ thống CRUD (Tạo, Đọc, Cập nhật, Xóa) điểm học kỳ tập trung, hiệu quả và chính xác, bao gồm tìm kiếm, phân trang và kiểm tra tính hợp lệ dữ liệu.

**1. Module:**
*   **Module chính:** "Quản lý Điểm Học Kỳ" (trong hệ thống `quanlyhocsinh`).
*   **Tích hợp/Phụ thuộc:** Kết nối chặt chẽ với các module Quản lý Học sinh, Môn học, Học kỳ, Lớp học và Người dùng & Phân quyền hiện có trong hệ thống để truy xuất dữ liệu và kiểm tra quyền hạn.

**2. Requirements:**
*   **Functional (FRs - Must-have):**
    *   Xem, thêm, cập nhật, xóa (theo quyền) điểm học kỳ.
    *   Tìm kiếm điểm theo mã/tên HS, môn học, học kỳ.
    *   Phân trang danh sách điểm.
    *   Kiểm tra tính hợp lệ dữ liệu nhập (validation).
*   **Non-functional (NFRs):**
    *   **Performance:** Tải trang < 2s; thao tác CRUD/Tìm kiếm < 3s (10k bản ghi); hỗ trợ 50 người dùng đồng thời; mở rộng lên 100k bản ghi.
    *   **Security:** Xác thực/phân quyền theo vai trò (Giáo viên, BGH, QTV); mã hóa HTTPS; kiểm soát đầu vào (SQLI, XSS).
    *   **Availability:** Uptime 99.5% trong giờ hành chính.
*   **Business Rules (BRs):**
    *   Điểm phải từ 0.0 đến 10.0.
    *   Mỗi học sinh chỉ có một điểm cho một môn học trong một học kỳ.
    *   Giáo viên chỉ quản lý điểm các môn mình phụ trách.
    *   Ban Giám Hiệu/Quản trị viên có toàn quyền.
    *   Điểm là số thập phân (tối đa 1 chữ số sau dấu phẩy).
*   **Audit Trail:** Ghi lại lịch sử tạo, cập nhật, xóa điểm (người thực hiện, thời gian, dữ liệu).

**3. Constraints:**
*   **Trong phạm vi:** CRUD điểm, tìm kiếm, phân trang, kiểm tra tính hợp lệ, quản lý quyền.
*   **Ngoài phạm vi:** Tính toán điểm trung bình/tổng kết, tạo báo cáo học bạ chính thức, tích hợp hệ thống chấm điểm bên ngoài, cổng xem điểm cho học sinh/phụ huynh.
*   **Giả định:** Dữ liệu về Học sinh, Môn học, Lớp học, Học kỳ, Người dùng đã tồn tại và được quản lý; người dùng có quyền và kiến thức cơ bản.

**4. Progress:**
*   **BRD:** ✅ Hiện tại
*   **SRS/SAD:** ⏳ Chờ BA duyệt
*   **DEV (mã nguồn):** ⏳ Chờ SRS duyệt
*   **TEST (test cases):** ⏳ Chờ DEV duyệt

**5. Known Issues:**
*   Chưa có vấn đề cụ thể nào được đề cập trong tài liệu này.

---