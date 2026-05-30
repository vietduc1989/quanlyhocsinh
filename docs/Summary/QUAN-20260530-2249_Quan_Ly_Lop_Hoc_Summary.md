**Tóm tắt Dự án: Quản lý Lớp Học (quanlyhocsinh)**

**1. Module:**
*   **Tên:** Quản lý Lớp Học
*   **Mã:** `QUAN-20260530-2249`
*   **Dự án:** `quanlyhocsinh`
*   **Mục đích:** Cung cấp chức năng cốt lõi (CRUD: Thêm, Xem, Cập nhật, Xóa) cho thông tin lớp học, bao gồm tìm kiếm, phân trang và kiểm tra dữ liệu. Đây là module nền tảng để tổ chức học sinh, phân công giáo viên và quản lý thời khóa biểu.
*   **Người dùng chính:** Quản trị viên hệ thống (có toàn quyền CRUD).

**2. Requirements:**
*   **Yêu cầu chức năng (FRs):**
    *   **Quản lý danh sách (Read, Search, Pagination):** Hiển thị danh sách lớp học (Mã, Tên, Năm học, Sĩ số hiện tại/tối đa), sắp xếp mặc định (Năm học giảm, Tên lớp tăng). Hỗ trợ tìm kiếm theo Mã lớp, Tên lớp, Năm học (không phân biệt hoa/thường) và phân trang linh hoạt (10, 20, 50 mục/trang).
    *   **Thêm mới (Create):** Giao diện nhập liệu các trường bắt buộc như Mã lớp, Tên lớp, Năm học, Sĩ số tối đa. Yêu cầu Mã lớp là duy nhất.
    *   **Xem chi tiết (Read):** Hiển thị đầy đủ thông tin của một lớp học cụ thể.
    *   **Cập nhật (Update):** Cho phép chỉnh sửa Tên lớp, Sĩ số tối đa, Ghi chú. **Lưu ý: Mã lớp và Năm học không thể chỉnh sửa** sau khi tạo. Đảm bảo Sĩ số tối đa mới không nhỏ hơn Sĩ số hiện tại.
    *   **Xóa (Delete):** Yêu cầu xác nhận và kiểm tra ràng buộc nghiệp vụ trước khi xóa.
    *   **Validation:** Thực hiện kiểm tra dữ liệu đầu vào cho các trường (định dạng, giới hạn ký tự, phạm vi số học) và hiển thị thông báo lỗi chi tiết.
*   **Quy tắc nghiệp vụ (BRs):**
    *   `BR01`: Mã lớp phải là duy nhất toàn hệ thống.
    *   `BR02`: Tên lớp phải duy nhất trong cùng một Năm học.
    *   `BR03`: Sĩ số tối đa không được nhỏ hơn sĩ số hiện tại khi cập nhật.
    *   `BR04`: Không thể xóa lớp học nếu lớp đó đang có học sinh.
    *   `BR05`: Không thể xóa lớp học nếu đã được gán vào thời khóa biểu hoặc các sự kiện khác.
*   **Yêu cầu phi chức năng (NFRs):**
    *   **Hiệu năng:** Tải danh sách lớp học (<3 giây cho 1000 bản ghi), thao tác CRUD (<1 giây).
    *   **Khả năng sử dụng:** Giao diện trực quan, thông báo rõ ràng, thân thiện.
    *   **Bảo mật:** Chỉ Quản trị viên hệ thống có quyền thực hiện các thao tác CRUD.
    *   **Khả năng mở rộng:** Hệ thống phải hỗ trợ số lượng lớp học lớn trong tương lai.

**3. Constraints:**
*   **Tích hợp:** Cần tích hợp với module Quản lý Học Sinh (để kiểm tra sĩ số, học sinh thuộc lớp) và module Thời Khóa Biểu (kiểm tra lớp đã được gán).
*   **Công nghệ:** Tuân thủ kiến trúc và công nghệ hiện có của hệ thống `ONENET`.
*   **Ngôn ngữ:** Giao diện và các thông báo phải bằng tiếng Việt.

**4. Progress:**
*   Giai đoạn hiện tại: Yêu cầu nghiệp vụ (Business Requirement Document - BRD) cho tính năng "Quản lý Lớp Học" đã được định nghĩa chi tiết và hoàn chỉnh.

**5. Known Issues / Potential Risks:**
*   **Known Issues:** Không có vấn đề hiện tại được liệt kê trong BRD này.
*   **Rủi ro tiềm ẩn:**
    *   Các vấn đề đồng bộ dữ liệu sĩ số học sinh nếu có lỗi hoặc độ trễ từ module Quản lý Học Sinh.
    *   Ảnh hưởng hiệu năng khi số lượng lớp học tăng lên rất lớn mà không có tối ưu hóa CSDL.
    *   Rủi ro tích hợp nếu các module liên quan (Học sinh, Thời khóa biểu) chưa sẵn sàng hoặc thay đổi giao diện.
    *   Thay đổi các quy định nghiệp vụ về sĩ số tối đa, năm học trong tương lai có thể yêu cầu cập nhật hệ thống.