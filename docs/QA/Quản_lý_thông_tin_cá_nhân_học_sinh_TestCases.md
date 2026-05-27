# TÀI LIỆU KỊCH BẢN KIỂM THỬ (TEST CASES DOCUMENT)

*   **Hệ thống:** Hệ thống Quản lý Đào tạo ONENET
*   **Phân hệ:** Quản lý Học sinh (Student Management)
*   **Giai đoạn:** Phase 2 - Triển khai CRUD Học sinh
*   **Tác giả:** Đội ngũ QA - ONENET
*   **Phiên bản:** 1.0
*   **Ngày tạo:** [Ngày hiện tại]
*   **Tài liệu tham chiếu:** BRD Phase 2, Mã nguồn Backend (.NET 10), Mã nguồn Frontend (React/Mantine v7)

---

## I. MÔI TRƯỜNG VÀ THÔNG TIN KIỂM THỬ

*   **Backend:** ASP.NET Core 10 Web API, Entity Framework Core, PostgreSQL 17 (múi giờ UTC mặc định cho dữ liệu lịch).
*   **Frontend:** React 18, Mantine UI v7, TanStack Query v5 (React Query), Axios.
*   **Bộ dữ liệu mẫu (Seeded Data):**
    *   Lớp học: `Lớp 10A` (Guid `classId_1`), `Lớp 11B` (Guid `classId_2`), `Lớp 12C` (Guid `classId_3`).
    *   Học sinh mẫu đã tồn tại:
        *   Mã: `HS0001` | Tên: `Nguyễn Văn A` | Ngày sinh: `12/05/2008` | Lớp: `Lớp 10A`.
        *   Mã: `HS0002` | Tên: `Trần Thị B` | Ngày sinh: `20/09/2007` | Lớp: `Lớp 11B`.

---

## II. DANH SÁCH KỊCH BẢN KIỂM THỬ CHI TIẾT

### 1. Nhóm Chức năng: Thêm mới Học sinh (US-HS-001)

| Mã TC | Tên Kịch Bản Kiểm Thử | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ liệu Đầu Vào | Kết Quả Mong Đợi (Expected Result) | Loại Test | Độ Ưu Tiên |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_STU_CRT_001** | Thêm mới học sinh thành công với đầy đủ thông tin hợp lệ | Người dùng đã đăng nhập và truy cập màn hình Danh sách học sinh. | 1. Nhấn nút **"Thêm mới Học sinh"**.<br>2. Nhập đầy đủ, hợp lệ tất cả các trường thông tin bắt buộc và tự chọn.<br>3. Nhấn nút **"Lưu lại"**. | • Mã HS: `HS0003`<br>• Họ tên: `Phạm Văn C`<br>• Ngày sinh: `15/08/2008`<br>• Giới tính: `Nam`<br>• Lớp học: `Lớp 10A`<br>• Email: `vanc@onenet.edu.vn`<br• SĐT: `0961234567`<br>• Tên PH: `Phạm Văn Bố`<br>• SĐT PH: `0911223344`<br>• Địa chỉ: `Hải Phòng` | • Form đóng lại.<br>• Hệ thống hiển thị thông báo thành công: *"Thêm mới học sinh thành công."* màu xanh lá.<br>• Học sinh mới hiển thị đúng thông tin ở dòng đầu/danh sách học sinh.<br>• Database lưu đúng thông tin với trường `IsDeleted = false` và `Status = 1 (Active)`. | Positive | High |
| **TC_STU_CRT_002** | Thêm mới học sinh thất bại do bỏ trống các trường bắt buộc | Màn hình popup **"Thêm mới Học sinh"** đang mở. | 1. Bỏ trống tất cả các trường.<br>2. Nhấn nút **"Lưu lại"**. | Không nhập gì. | • Hệ thống chặn submit.<br>• Hiển thị thông báo lỗi Validation màu đỏ ngay dưới các trường bắt buộc:<br>  - Mã HS: *"Mã học sinh không được để trống"*<br>  - Họ tên: *"Tên phải chứa ít nhất 2 ký tự"*<br>  - Ngày sinh: *"Vui lòng chọn ngày sinh"*<br>  - Lớp học: *"Vui lòng chọn lớp học"*. | Negative | High |
| **TC_STU_CRT_003** | Thêm mới học sinh thất bại do trùng Mã Học sinh | Màn hình popup **"Thêm mới Học sinh"** đang mở.<br>Mã `HS0001` đã tồn tại. | 1. Nhập Mã HS trùng với mã đã có.<br>2. Nhập đầy đủ thông tin hợp lệ khác.<br>3. Nhấn nút **"Lưu lại"**. | • Mã HS: `HS0001` *(Trùng)*<br>• Họ tên: `Lê Hoàng Long`<br>• Ngày sinh: `10/10/2008`<br>• Lớp: `Lớp 10A` | • Backend trả về mã lỗi `400 Bad Request`. <br>• Frontend hiển thị Popup Notification góc trên bên phải màu đỏ với nội dung: *"Mã học sinh đã tồn tại trong hệ thống."*<br>• Popup Thêm mới không bị đóng, dữ liệu cũ được giữ nguyên để sửa đổi. | Negative | High |
| **TC_STU_CRT_004** | Thêm mới học sinh thất bại do độ dài ký tự vượt biên giới hạn | Màn hình popup **"Thêm mới Học sinh"** đang mở. | 1. Nhập các trường vượt giới hạn ký tự cho phép.<br>2. Nhấn nút **"Lưu lại"**. | • Mã HS: `HS00000000000000000001` *(21 ký tự, giới hạn là 20)*<br>• Họ tên: Chuỗi ký tự ngẫu nhiên có độ dài 101 ký tự. | • Hệ thống chặn không cho submit hoặc Backend trả về lỗi lỗi kiểm thực (FluentValidation):<br>  - *"Mã học sinh không quá 20 ký tự."*<br>  - *"Họ và tên không quá 100 ký tự."* | Boundary | Medium |
| **TC_STU_CRT_005** | Kiểm tra ràng buộc Ngày sinh không được ở hiện tại hoặc tương lai | Màn hình popup **"Thêm mới Học sinh"** đang mở. | 1. Chọn ngày sinh là ngày hiện tại hoặc tương lai.<br>2. Nhấn nút **"Lưu lại"**. | • Ngày sinh: Ngày hiện tại hoặc Ngày mai. | • Trình chọn ngày (`DateInput`) của Mantine giới hạn không cho chọn (`maxDate={new Date()}`).<br>• Nếu bypass bằng cách nhập text: Hiển thị lỗi dưới trường nhập: *"Ngày sinh phải ở trong quá khứ."* | Boundary | High |
| **TC_STU_CRT_006** | Thêm mới học sinh thất bại do nhập sai định dạng Email | Màn hình popup **"Thêm mới Học sinh"** đang mở. | 1. Nhập Email sai định dạng chuẩn.<br>2. Nhấn nút **"Lưu lại"**. | • Email: `nguyenvana_onenet.edu.vn` (thiếu ký tự `@`) | • Hệ thống hiển thị thông báo lỗi màu đỏ ngay dưới ô Email: *"Email không hợp lệ"*. • Dữ liệu không được gửi lên backend. | Boundary | High |
| **TC_STU_CRT_007** | Thao tác hủy bỏ việc thêm mới học sinh | Màn hình popup **"Thêm mới Học sinh"** đang mở và đã nhập một số thông tin. | 1. Nhấn nút **"Hủy"** hoặc biểu tượng **"X"** để đóng modal. | Đã nhập thông tin nháp. | • Popup đóng lại.<br>• Không có học sinh mới nào được thêm vào danh sách.<br>• Khi mở lại popup, các ô nhập liệu được reset về trống rỗng (`form.reset()`). | Positive | Low |

---

### 2. Nhóm Chức năng: Xem danh sách, Tìm kiếm và Lọc Học sinh (US-HS-002)

| Mã TC | Tên Kịch Bản Kiểm Thử | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ liệu Đầu Vào | Kết Quả Mong Đợi (Expected Result) | Loại Test | Độ Ưu Tiên |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_STU_RD_001** | Xem danh sách học sinh mặc định | Hệ thống đã có sẵn danh sách học sinh mẫu trong database. | Truy cập trang Quản lý Học sinh. | Không có. | • Hiển thị danh sách học sinh dưới dạng bảng (Table) với các cột đúng thiết kế.<br>• Hiển thị đúng dữ liệu mặc định ban đầu (`HS0001` - Nguyễn Văn A, `HS0002` - Trần Thị B).<br>• Các trạng thái hiển thị đúng màu Badge định nghĩa (`Active` hiển thị màu xanh lá). | Positive | High |
| **TC_STU_RD_002** | Tìm kiếm học sinh theo Họ và tên | Trang Quản lý học sinh đang mở. | Nhập một phần họ tên của học sinh mẫu vào ô tìm kiếm. | Từ khóa: `Văn A` hoặc `văn a` (Test không phân biệt chữ hoa/thường). | • Danh sách lập tức tự động filter (hoặc hiển thị sau khi debounce) chỉ còn học sinh `Nguyễn Văn A`. | Positive | High |
| **TC_STU_RD_003** | Tìm kiếm học sinh theo Mã học sinh | Trang Quản lý học sinh đang mở. | Nhập mã học sinh mẫu vào ô tìm kiếm. | Từ khóa: `HS0002`. | • Danh sách chỉ hiển thị học sinh `Trần Thị B`. | Positive | High |
| **TC_STU_RD_004** | Tìm kiếm không có kết quả | Trang Quản lý học sinh đang mở. | Nhập từ khóa tìm kiếm không tồn tại trong DB. | Từ khóa: `Không Có Ai`. | • Danh sách trống rỗng.<br>• Không xảy ra lỗi trắng màn hình.<br>• Hệ thống không bị treo. | Negative | Medium |
| **TC_STU_RD_005** | Lọc học sinh theo Lớp học | Trang Quản lý học sinh đang mở. | Chọn một lớp học cụ thể từ combobox **"Lọc theo lớp học"**. | Chọn: `Lớp 11B`. | • Danh sách chỉ hiển thị học sinh thuộc lớp `Lớp 11B` (ví dụ: `Trần Thị B`). | Positive | High |
| **TC_STU_RD_006** | Kết hợp tìm kiếm theo tên và lọc theo lớp học | Trang Quản lý học sinh đang mở. | 1. Nhập từ khóa tìm kiếm.<br>2. Chọn bộ lọc Lớp học. | Từ khóa: `Nguyễn Văn A` + Chọn lớp: `Lớp 11B`. | • Danh sách trống rỗng (vì học sinh Nguyễn Văn A thuộc lớp `Lớp 10A`, không phải `11B`). | Positive | Medium |
| **TC_STU_RD_007** | Phân trang danh sách học sinh | Database có nhiều hơn 10 học sinh hoạt động. | 1. Cuộn xuống cuối bảng.<br>2. Quan sát thanh phân trang.<br>3. Nhấn chuyển sang trang 2. | Không có. | • Thanh phân trang hiển thị đúng tổng số trang (`TotalPages`).<br>• Khi chuyển sang trang 2, danh sách hiển thị đúng tập học sinh từ thứ 11 trở đi.<br>• UI chuyển hướng mượt mà, không giật lag. | Positive | High |

---

### 3. Nhóm Chức năng: Xem chi tiết và Cập nhật Học sinh (US-HS-003 & US-HS-004)

| Mã TC | Tên Kịch Bản Kiểm Thử | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ liệu Đầu Vào | Kết Quả Mong Đợi (Expected Result) | Loại Test | Độ Ưu Tiên |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_STU_UPD_001** | Xem chi tiết thông tin học sinh khi mở form Chỉnh sửa | Màn hình Danh sách học sinh đang mở. | Nhấn vào biểu tượng bút chì (Chỉnh sửa) ở dòng chứa học sinh `Nguyễn Văn A`. | Không có. | • Popup **"Chỉnh sửa Học sinh"** hiển thị.<br>• Tất cả các ô nhập liệu được điền chính xác thông tin hiện tại của học sinh `Nguyễn Văn A` lấy từ DB.<br>• Ô **"Mã Học sinh"** bị mờ (disabled) không cho chỉnh sửa để bảo toàn tính toàn vẹn dữ liệu. | Positive | High |
| **TC_STU_UPD_002** | Cập nhật thông tin học sinh thành công với dữ liệu hợp lệ | Popup **"Chỉnh sửa Học sinh"** đang mở cho học sinh `Nguyễn Văn A`. | 1. Thay đổi thông tin địa chỉ và SĐT.<br>2. Nhấn nút **"Lưu lại"**. | • Địa chỉ: `Hà Nội đổi mới`<br>• SĐT: `0999999999` | • Form đóng lại.<br>• Hệ thống hiển thị thông báo: *"Cập nhật học sinh thành công."* màu xanh lá.<br>• Dòng dữ liệu của học sinh `Nguyễn Văn A` cập nhật SĐT mới ngay lập tức trên bảng hiển thị.<br>• Database lưu đúng địa chỉ và SĐT mới, cập nhật trường `LastModifiedBy = "Admin"` và `LastModifiedAt` bằng thời gian hiện tại. | Positive | High |
| **TC_STU_UPD_003** | Cập nhật thất bại do xóa dữ liệu bắt buộc | Popup **"Chỉnh sửa Học sinh"** đang mở. | 1. Xóa nội dung trong ô **"Họ và tên"**.<br>2. Nhấn nút **"Lưu lại"**. | • Họ và tên: `""` (Rỗng) | • Hệ thống chặn submit.<br>• Hiển thị thông báo lỗi ngay dưới ô Họ tên: *"Tên phải chứa ít nhất 2 ký tự"*. | Negative | High |
| **TC_STU_UPD_004** | Thay đổi Trạng thái học sinh sang ngừng hoạt động | Popup **"Chỉnh sửa Học sinh"** đang mở. | 1. Chọn giá trị trạng thái là **"Tạm dừng"** (hoặc Nghỉ học).<br>2. Nhấn nút **"Lưu lại"**. | • Trạng thái: Chọn `Tạm dừng` (`Status = 2`). | • Cập nhật thành công.<br>• Trên danh sách học sinh, Badge trạng thái của học sinh chuyển từ màu xanh lá (`Hoạt động`) sang màu xám (`Tạm dừng`). | Positive | High |
| **TC_STU_UPD_005** | Hủy bỏ thao tác cập nhật thông tin | Popup **"Chỉnh sửa Học sinh"** đang mở và đã sửa đổi dữ liệu. | 1. Nhấn nút **"Hủy"**.<br>2. Kiểm tra dữ liệu ngoài danh sách học sinh. | Có chỉnh sửa thông tin nháp. | • Popup đóng lại.<br>• Dữ liệu học sinh không thay đổi.<br>• Database giữ nguyên giá trị gốc cũ. | Positive | Low |

---

### 4. Nhóm Chức năng: Xóa Học sinh (US-HS-005)

| Mã TC | Tên Kịch Bản Kiểm Thử | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ liệu Đầu Vào | Kết Quả Mong Đợi (Expected Result) | Loại Test | Độ Ưu Tiên |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_STU_DEL_001** | Xác nhận xóa học sinh thành công (Xóa mềm - Soft Delete) | Trang Danh sách học sinh đang mở. | 1. Nhấn biểu tượng thùng rác (Xóa) tại dòng của học sinh `Trần Thị B`.<br>2. Kiểm tra hiển thị popup xác nhận.<br>3. Nhấn **"Xóa"** (Confirm). | Không có. | • Popup xác nhận hiển thị cảnh báo: *"Bạn có chắc chắn muốn xóa học sinh [Trần Thị B] không?..."*<br>• Khi bấm xác nhận: Hệ thống hiển thị thông báo thành công: *"Đã xóa học sinh thành công."*<br>• Học sinh `Trần Thị B` biến mất hoàn toàn khỏi bảng hiển thị trên giao diện.<br>• **Kiểm tra Database:** Bản ghi của Trần Thị B **không bị mất** (không dùng lệnh DELETE vật lý) mà trường `IsDeleted` chuyển thành `true`, đồng thời trường `LastModifiedAt` được cập nhật thời gian xóa. | Positive | High |
| **TC_STU_DEL_002** | Hủy bỏ thao tác xóa học sinh | Trang Danh sách học sinh đang mở. | 1. Nhấn biểu tượng thùng rác (Xóa) tại dòng của học sinh `Nguyễn Văn A`.<br>2. Nhấn nút **"Hủy"** trên hộp thoại xác nhận. | Không có. | • Hộp thoại xác nhận đóng lại.<br>• Học sinh `Nguyễn Văn A` vẫn hiển thị bình thường trong danh sách.<br>• Database không thay đổi dữ liệu (`IsDeleted` giữ nguyên bằng `false`). | Positive | Medium |

---

### 5. Nhóm Kiểm thử API & Tích Hợp Hệ Thống (Integration & API Tests)

*Đây là các test cases dành cho mức độ tích hợp hệ thống, kiểm tra trực tiếp qua các công cụ như Postman/Swagger để đảm bảo tính an toàn bảo mật từ phía Server-side.*

| Mã TC | Tên Kịch Bản Kiểm Thử | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ liệu Đầu Vào | Kết Quả Mong Đợi (Expected Result) | Loại Test | Độ Ưu Tiên |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_STU_API_001** | Gọi trực tiếp API Delete với ID không tồn tại | Backend Web API đang chạy. | Sử dụng công cụ (Postman) gửi yêu cầu `DELETE` trực tiếp lên endpoint với GUID ngẫu nhiên không có trong database. | Phương thức: `DELETE`<br>URL: `/api/students/00000000-0000-0000-0000-000000000000` | • Server xử lý qua Middleware bắt ngoại lệ và trả về mã lỗi HTTP `404 Not Found`.<br>• Nội dung phản hồi JSON thân thiện dạng:<br>`{"error": "Student with ID 00000000-0000-0000-0000-000000000000 not found."}`<br>• Hệ thống API không bị crash. | Security/ Robustness | Medium |
| **TC_STU_API_002** | Gọi API Create với Lớp học (ClassId) không tồn tại hoặc đã bị vô hiệu hóa | Backend Web API đang chạy. | Gửi request `POST` tạo học sinh mới với mã ID của một lớp không có thực trong Database. | Phương thức: `POST`<br>URL: `/api/students`<br>Payload chứa: `"classId": "99999999-9999-9999-9999-999999999999"` | • Server xử lý nghiệp vụ qua FluentValidation chặn lại và trả về mã lỗi HTTP `400 Bad Request`.<br>• Chi tiết phản hồi chứa thông báo rõ ràng:<br>`"Lớp học được chọn không tồn tại."` | Security/ Integration | High |
| **TC_STU_API_003** | Xác thực múi giờ UTC đồng bộ khi ghi nhận dữ liệu Ngày sinh | Database PostgreSQL đang chạy. | Gửi yêu cầu lưu học sinh mới, sau đó kiểm tra định dạng múi giờ lưu trữ thực tế trong bảng PostgreSQL. | `DateOfBirth: "2008-05-12T00:00:00.000Z"` | • Trong backend, trường được ép kiểu bằng lệnh `DateTime.SpecifyKind(..., DateTimeKind.Utc)`.<br>• Khi kiểm tra trực tiếp trong PostgreSQL: Giá trị phải được lưu dưới dạng múi giờ UTC, không bị sai lệch lệch múi giờ (lệch giờ GMT+7 của Việt Nam dẫn đến đổi ngày sinh). | Integration | Medium |

---

## III. CHỈ TIÊU ĐÁNH GIÁ ĐẠT YÊU CẦU (EXIT CRITERIA)

*   **100%** kịch bản kiểm thử loại **High** (Độ ưu tiên cao) phải được chạy thử nghiệm và đạt kết quả **Pass** (Đạt).
*   Không còn lỗi tồn đọng có độ nghiêm trọng từ **Medium** (Trung bình) trở lên trước khi merge code vào nhánh môi trường Production/Staging.
*   Cơ chế **Soft Delete** phải hoạt động hoàn hảo: kiểm tra không có câu lệnh `DELETE FROM Students WHERE...` nào được thực thi, thay thế bằng lệnh `UPDATE Students SET IsDeleted = true WHERE...` để bảo toàn lịch sử thông tin điểm và học tập.