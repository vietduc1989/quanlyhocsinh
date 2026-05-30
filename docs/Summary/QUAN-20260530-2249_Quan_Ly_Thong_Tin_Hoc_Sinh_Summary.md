Dưới đây là bản tóm tắt cực kỳ ngắn gọn về dự án hiện tại và thông tin mới:

**Dự án:** `quanlyhocsinh`
**Module:** Quản lý Thông tin Học sinh (`QUAN-20260530-2249`)

**Requirements:**
*   **Chức năng (FRs):**
    *   Thực hiện các thao tác CRUD (Thêm, Xem, Chỉnh sửa, Xóa) thông tin học sinh.
    *   Xem danh sách học sinh có phân trang, sắp xếp mặc định theo Mã học sinh.
    *   Tìm kiếm học sinh theo Mã HS/Họ tên và lọc theo Lớp học/Trạng thái.
    *   Hỗ trợ validation dữ liệu đầu vào cho các trường thông tin học sinh (Mã HS, Họ tên, Ngày sinh, Giới tính, Địa chỉ, SĐT/Email phụ huynh, Lớp học, Trạng thái).
*   **Nghiệp vụ (BRs):**
    *   Mã học sinh là duy nhất và không được chỉnh sửa sau khi tạo.
    *   Các trường bắt buộc (Họ tên, Ngày sinh, Giới tính, Lớp học, Trạng thái) không được trống.
    *   Ngày sinh phải hợp lệ và trong quá khứ.
    *   SĐT/Email phụ huynh phải đúng định dạng (nếu có nhập).
    *   **Ràng buộc Xóa:** KHÔNG được phép xóa học sinh nếu có dữ liệu liên quan (điểm danh, điểm số, học phí,...); thay vào đó, khuyến khích thay đổi trạng thái học sinh.
*   **Phi chức năng (NFRs):**
    *   **Hiệu năng:** Tải trang danh sách < 3s (1000 bản ghi), thao tác CRUD < 1s.
    *   **Bảo mật:** Chỉ "Người Quản Lý" có quyền truy cập và thao tác.
    *   **Khả năng sử dụng:** Giao diện trực quan, thông báo rõ ràng.
    *   **Khả năng mở rộng:** Hỗ trợ xử lý hàng chục nghìn học sinh.

**Constraints:**
*   **Công nghệ:** Tích hợp với công nghệ hiện có của ONENET.
*   **Thời gian:** Tuân thủ kế hoạch dự án.
*   **Dữ liệu liên quan:** Cần tích hợp/tham chiếu danh mục "Lớp học".

**Progress:**
*   Tài liệu BRD (Business Requirements Document) cho tính năng "Quản lý Thông tin Học sinh" đã được hoàn thành, mô tả chi tiết các yêu cầu nghiệp vụ, chức năng, quy tắc nghiệp vụ và tiêu chí chấp nhận.

**Known Issues:**
*   (Từ BRD, đây là vấn đề hiện trạng hệ thống giải quyết) Hiện tại, việc quản lý thông tin học sinh còn thủ công/rời rạc, gây khó khăn trong tra cứu, cập nhật, tổng hợp và thiếu kiểm soát dữ liệu.