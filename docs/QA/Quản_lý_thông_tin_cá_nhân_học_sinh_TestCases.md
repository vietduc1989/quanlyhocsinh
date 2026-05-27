# TÀI LIỆU KỊCH BẢN KIỂM THỬ (TEST CASES SPECIFICATION)

*   **Hệ thống:** ONENET - Phân hệ Quản lý Giáo dục
*   **Module:** Quản lý Học sinh (Student Management) - Phase 2
*   **Tác giả:** Đội ngũ QA ONENET (Senior QA Engineer)
*   **Phiên bản:** 1.0.0
*   **Ngày tạo:** 24/02/2025
*   **Môi trường áp dụng:** Staging / UAT (Backend .NET 10 Web API, PostgreSQL DB, Frontend React 18 / Mantine UI v7)

---

## I. THÔNG TIN CHUNG & QUY ƯỚC

### 1. Mức độ ưu tiên (Priority - P)
*   **P0 (Critical):** Các chức năng cốt lõi lỗi khiến luồng nghiệp vụ bị tắc nghẽn hoàn toàn (ví dụ: Không thể thêm học sinh, lỗi crash trang, lỗi API 500 khi lưu dữ liệu hợp lệ).
*   **P1 (High):** Lỗi kiểm soát nghiệp vụ, sai sót dữ liệu nghiêm trọng hoặc sai lệch logic validation (ví dụ: Trùng mã học sinh vẫn lưu được, số điện thoại nhập chữ vẫn hợp lệ).
*   **P2 (Medium):** Các lỗi về hiển thị, giao diện không đồng bộ, lỗi phân trang nhẹ, thông báo lỗi thiếu thân thiện.
*   **P3 (Low):** Lỗi thẩm mỹ, căn lề, font chữ, gợi ý UI/UX chưa tối ưu.

### 2. Dữ liệu Test mẫu định sẵn (Seed Data)
Để phục vụ việc kiểm thử tích hợp chính xác, dữ liệu mẫu dưới đây được giả định đã có sẵn trong database (được cấu hình qua `ApplicationDbContext`):
*   **Lớp học 1:** ID: `a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1` | Tên lớp: `Lớp 10A` | Niên khóa: `2025-2026`
*   **Lớp học 2:** ID: `b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2` | Tên lớp: `Lớp 11B` | Niên khóa: `2025-2026`

---

## II. DANH SÁCH CÁC KỊCH BẢN KIỂM THỬ CHI TIẾT

### 1. KỊCH BẢN KIỂM THỬ CHỨC NĂNG THÊM MỚI HỌC SINH (CREATE STUDENT)

#### **TC_UI_CREATE_001: Thêm mới Học sinh thành công với toàn bộ thông tin hợp lệ (P0)**
*   **Mục đích:** Đảm bảo hệ thống cho phép thêm mới một học sinh khi nhập đầy đủ và chính xác tất cả thông tin (bắt buộc & tự chọn).
*   **Điều kiện đầu vào (Pre-conditions):**
    *   Người dùng đã đăng nhập tài khoản có quyền "Nhân viên Giáo vụ" hoặc "Admin".
    *   Đang ở màn hình danh sách Học sinh.
*   **Dữ liệu kiểm thử (Test Data):**
    *   Mã học sinh: `HS-2025-0001`
    *   Họ và tên: `Nguyễn Văn Anh`
    *   Ngày sinh: `15/05/2010` (Hợp lệ - nhỏ hơn ngày hiện tại)
    *   Giới tính: Chọn `Nam`
    *   Lớp học: Chọn `Lớp 10A`
    *   Số điện thoại: `0987654321` (10 số)
    *   Email: `vananh.nguyen@onenet.edu.vn`
    *   Địa chỉ: `Số 123 Đường Cầu Giấy, Hà Nội`
    *   Tên phụ huynh: `Nguyễn Văn Cha`
    *   SĐT phụ huynh: `0912345678`
*   **Các bước thực hiện:**
    1. Click nút "Thêm Học Sinh Mới" trên Header.
    2. Điền đầy đủ dữ liệu từ phần *Test Data* vào form.
    3. Click nút "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   **UI:** Modal đóng lại. Hiển thị thông báo Toast màu xanh lá (Success): "Thêm mới học sinh thành công".
    *   **UI:** Học sinh mới hiển thị đầu tiên trong danh sách (do mặc định sắp xếp theo Tên hoặc thứ tự tạo mới).
    *   **Database:** Truy vấn bảng `Students` thấy xuất hiện bản ghi mới với dữ liệu khớp 100% dữ liệu đã nhập. Trường `IsDeleted` = `false`, `Status` = `1` (Active), `CreatedAt` ghi nhận thời gian UTC hiện tại, `CreatedBy` = "System" hoặc tên User đăng nhập.

#### **TC_UI_CREATE_002: Thêm mới thất bại do bỏ trống các trường bắt buộc (P0)**
*   **Mục đích:** Đảm bảo hệ thống không cho phép lưu và hiển thị cảnh báo lỗi màu đỏ khi bỏ trống các trường bắt buộc trên Form.
*   **Các bước thực hiện:**
    1. Click nút "Thêm Học Sinh Mới".
    2. Bỏ trống các trường: `Mã Học Sinh`, `Họ và Tên`, `Ngày Sinh`, `Lớp Học`.
    3. Nhập các trường không bắt buộc khác tùy ý.
    4. Click nút "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   Form không được submit, không gửi request lên Server.
    *   Dưới các trường bị bỏ trống hiển thị thông báo lỗi bằng tiếng Việt (màu đỏ - Mantine Validation):
        *   Mã Học Sinh: `"Mã học sinh là bắt buộc"`
        *   Họ và Tên: `"Họ và tên là bắt buộc"`
        *   Ngày Sinh: `"Vui lòng chọn ngày sinh"`
        *   Lớp Học: `"Lớp học là bắt buộc"`
    *   **Database:** Không có bản ghi nào được thêm mới.

#### **TC_UI_CREATE_003: Thêm mới thất bại do trùng Mã Học sinh (P1)**
*   **Mục đích:** Đảm bảo hệ thống kiểm tra tính duy nhất (Unique) của `StudentCode` ở cả Frontend và Backend để tránh trùng lặp dữ liệu.
*   **Điều kiện đầu vào:** Đã có học sinh mã `HS-2025-0001` trong hệ thống.
*   **Dữ liệu kiểm thử:**
    *   Mã học sinh: `HS-2025-0001` (trùng)
    *   Họ và tên: `Trần Thị Bích`
    *   Các trường khác điền hợp lệ.
*   **Các bước thực hiện:**
    1. Click nút "Thêm Học Sinh Mới".
    2. Nhập Mã Học Sinh là `HS-2025-0001`.
    3. Điền các thông tin hợp lệ khác.
    4. Click nút "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   Hệ thống thực hiện gửi API `POST /api/students`.
    *   **Backend:** Validator `CreateStudentCommandValidator` bắt lỗi trùng lặp dữ liệu, API trả về mã lỗi `400 Bad Request` đi kèm nội dung: `"Mã học sinh đã tồn tại trên hệ thống."`
    *   **UI (Frontend):** Bắt được lỗi của API, hiển thị một Toast notification lỗi màu đỏ chứa thông báo `"Mã học sinh đã tồn tại trên hệ thống."`
    *   **Database:** Không có bản ghi nào được thêm mới.

#### **TC_UI_CREATE_004: Validation - Ngày sinh bằng hoặc vượt quá ngày hiện tại (P1)**
*   **Mục đích:** Kiểm tra quy tắc nghiệp vụ ngày sinh của học sinh phải luôn nằm trong quá khứ.
*   **Dữ liệu kiểm thử:** Ngày sinh chọn là Ngày hôm nay (Today) hoặc Ngày mai (Future Date).
*   **Các bước thực hiện:**
    1. Click nút "Thêm Học Sinh Mới".
    2. Tại ô "Ngày Sinh", chọn ngày hiện tại hoặc một ngày trong tương lai.
    3. Nhập đầy đủ các thông tin hợp lệ khác.
    4. Click nút "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   Frontend ngăn chặn hành động click hoặc hiển thị lỗi trực tiếp tại ô DateInput: `"Ngày sinh phải ở trong quá khứ."`
    *   Trường hợp bypass được FE, API trả về lỗi `400 Bad Request`: `"Ngày sinh phải ở trong quá khứ."`

#### **TC_UI_CREATE_005: Validation - Sai định dạng Email hoặc Số điện thoại (P1)**
*   **Mục đích:** Kiểm tra tính toàn vẹn định dạng dữ liệu liên lạc theo RegEx.
*   **Dữ liệu kiểm thử:**
    *   Email sai: `nguyenvanan.com`, `@onenet.vn`, `nguyenvanan@`
    *   Số điện thoại sai: `0987`, `0987abc123`, `09876543211234` (quá dài)
*   **Các bước thực hiện:**
    1. Click "Thêm Học Sinh Mới".
    2. Nhập Email và Số điện thoại sai định dạng như trên.
    3. Click "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   Frontend hiển thị lỗi ngay tại các trường tương ứng:
        *   Email: `"Email không đúng định dạng"`
        *   Số điện thoại: `"Số điện thoại không đúng định dạng."` (Kiểm định mẫu regex `^\+?[0-9]{10,12}$`).

---

### 2. KỊCH BẢN KIỂM THỬ XEM DANH SÁCH & TRA CỨU HỌC SINH (READ / LIST / SEARCH)

#### **TC_UI_READ_001: Hiển thị danh sách học sinh mặc định có phân trang (P0)**
*   **Mục đích:** Đảm bảo hệ thống tải đúng danh sách học sinh theo từng trang, hiển thị đầy đủ các cột thuộc tính cơ bản.
*   **Các bước thực hiện:**
    1. Truy cập trang "Quản lý Học sinh".
*   **Kết quả mong đợi:**
    *   Danh sách tải nhanh, xuất hiện biểu tượng Loading che phủ mờ (LoadingOverlay) trong lúc gọi API.
    *   Bảng hiển thị đầy đủ 7 cột: `Mã HS`, `Họ và Tên`, `Ngày Sinh`, `Giới Tính`, `Lớp`, `Trạng Thái`, `Hành Động`.
    *   Bên dưới bảng hiển thị: `"Tổng số: X học sinh"` (X là số bản ghi thực tế trong DB) và thanh phân trang (Pagination).
    *   Số lượng bản ghi tối đa hiển thị trên 1 trang mặc định là 10 (theo tham số `PageSize = 10` của API).
    *   Học sinh hiển thị được sắp xếp mặc định theo bảng chữ cái A-Z của trường Họ và Tên.

#### **TC_UI_READ_002: Tìm kiếm học sinh theo Tên hoặc Mã học sinh (P0)**
*   **Mục đích:** Đảm bảo thanh tìm kiếm hoạt động chính xác theo từ khóa không phân biệt chữ hoa, chữ thường và có hỗ trợ tìm kiếm gần đúng (Like).
*   **Dữ liệu kiểm thử:** Có học sinh mã `HS-2025-0001` tên `Nguyễn Văn Anh` và `HS-2025-0002` tên `Trần Thị Bích`.
*   **Các bước thực hiện:**
    1. Nhập từ khóa `"văn anh"` vào ô "Tìm theo Tên hoặc Mã học sinh...".
    2. Quan sát kết quả hiển thị trên bảng.
    3. Xóa ô tìm kiếm, nhập tiếp từ khóa `"Bích"`.
    4. Xóa ô tìm kiếm, nhập mã `"HS-2025-0001"`.
*   **Kết quả mong đợi:**
    *   Bước 1 & 2: Chỉ hiển thị học sinh `Nguyễn Văn Anh` trong bảng.
    *   Bước 3: Chỉ hiển thị học sinh `Trần Thị Bích`.
    *   Bước 4: Chỉ hiển thị học sinh `Nguyễn Văn Anh`.
    *   Giao diện tự động lọc/tìm kiếm mượt mà mà không làm reload lại toàn bộ trang (sử dụng cache của React Query).

#### **TC_UI_READ_003: Lọc học sinh theo Lớp học (P0)**
*   **Mục đích:** Đảm bảo bộ lọc Combobox lọc chính xác học sinh thuộc về lớp học đã chọn.
*   **Dữ liệu kiểm thử:** Chọn lớp `Lớp 10A`.
*   **Các bước thực hiện:**
    1. Click vào Dropdown "Lọc theo lớp học".
    2. Chọn `Lớp 10A`.
*   **Kết quả mong đợi:**
    *   Bảng dữ liệu chỉ hiển thị các học sinh thuộc cột Lớp là `Lớp 10A`.
    *   Các học sinh thuộc lớp khác (`Lớp 11B`,...) không xuất hiện trong danh sách.
    *   Trường hợp bấm nút Clear (xóa chọn lớp) -> Danh sách tự động hiển thị đầy đủ học sinh của tất cả các lớp.

#### **TC_UI_READ_004: Xử lý trạng thái Danh sách rỗng / Không có kết quả tìm kiếm (P1)**
*   **Mục đích:** Giao diện hiển thị thân thiện khi không có dữ liệu phù hợp.
*   **Các bước thực hiện:**
    1. Nhập vào ô tìm kiếm một chuỗi ký tự ngẫu nhiên vô nghĩa: `"xyz99999999"`.
*   **Kết quả mong đợi:**
    *   Bảng dữ liệu không hiển thị bản ghi nào.
    *   Màn hình hiển thị thông điệp trống rõ ràng: `"Không tìm thấy học sinh nào phù hợp với tiêu chí tìm kiếm."` hoặc bảng rỗng không bị vỡ giao diện.
    *   Phân trang (Pagination) biến mất hoặc đưa về trạng thái 1 trang duy nhất không thể click.

#### **TC_UI_READ_005: Xem chi tiết thông tin Học sinh (P0)**
*   **Mục đích:** Đảm bảo người dùng có thể xem đầy đủ, chi tiết và chính xác thông tin nội bộ của học sinh dưới dạng chỉ đọc (Read-only).
*   **Các bước thực hiện:**
    1. Click biểu tượng con mắt (IconEye - Xem chi tiết) tại dòng của học sinh `Nguyễn Văn Anh`.
*   **Kết quả mong đợi:**
    *   Mở một Modal mới với tiêu đề: `"Thông tin chi tiết học sinh"`.
    *   Các trường thông tin chi tiết hiển thị đầy đủ bao gồm cả Địa chỉ, Tên phụ huynh, SĐT phụ huynh, Email.
    *   Giao diện gọn gàng, chia lưới Grid hợp lý, dữ liệu hiển thị chính xác tương ứng với học sinh được chọn.
    *   Không có ô nhập liệu nào cho phép chỉnh sửa trực tiếp tại màn hình này. Có nút "Đóng" để thoát modal an toàn.

---

### 3. KỊCH BẢN KIỂM THỬ CẬP NHẬT THÔNG TIN HỌC SINH (UPDATE / EDIT)

#### **TC_UI_UPDATE_001: Cập nhật thông tin cơ bản thành công (P0)**
*   **Mục đích:** Đảm bảo hệ thống cho phép chỉnh sửa thông tin và đồng bộ hóa thành công dữ liệu thay đổi lên Database.
*   **Các bước thực hiện:**
    1. Tìm học sinh `Nguyễn Văn Anh`. Click biểu tượng cây bút (IconEdit - Chỉnh sửa).
    2. Màn hình Modal hiển thị Form đã đổ đầy đủ dữ liệu cũ của học sinh đó.
    3. Thay đổi số điện thoại thành: `0909123456`.
    4. Thay đổi địa chỉ thành: `Số 456 Đường Trần Hưng Đạo, Quận 1, TP. HCM`.
    5. Click nút "Lưu Thông Tin".
*   **Kết quả mong đợi:**
    *   Gửi request thành công đến API `PUT /api/students/{id}`.
    *   Giao diện hiển thị Toast thành công: `"Cập nhật học sinh thành công"`.
    *   Bảng danh sách học sinh cập nhật ngay lập tức dữ liệu mới.
    *   **Database:** Bản ghi học sinh được cập nhật đúng 2 trường `PhoneNumber` và `Address`. Đồng thời, kiểm tra cột `LastModifiedAt` tự động ghi nhận thời gian chỉnh sửa (UTC), `LastModifiedBy` lưu tên người chỉnh sửa.

#### **TC_UI_UPDATE_002: Kiểm tra cho phép giữ nguyên Mã Học Sinh cũ nhưng cấm trùng với Học Sinh khác (P1)**
*   **Mục đích:** Khi sửa thông tin, hệ thống phải cho phép giữ nguyên mã học sinh của chính bản ghi đó, nhưng nếu sửa mã học sinh trùng với mã của một học sinh khác thì phải chặn lại.
*   **Dữ liệu kiểm thử:** Học sinh A có mã `HS-2025-0001`, Học sinh B có mã `HS-2025-0002`.
*   **Các bước thực hiện:**
    *   **Case A (Hợp lệ):** Mở form sửa Học sinh A, thay đổi số điện thoại, giữ nguyên mã `HS-2025-0001` -> Click Lưu.
    *   **Case B (Bị chặn):** Mở form sửa Học sinh B, sửa mã học sinh của B thành `HS-2025-0001` (trùng mã Học sinh A) -> Click Lưu.
*   **Kết quả mong đợi:**
    *   **Case A:** Lưu thành công bình thường (Backend xử lý loại trừ Id hiện tại qua tham số `excludeId` của hàm `IsCodeUniqueAsync`).
    *   **Case B:** Backend bắt lỗi và trả về lỗi 400 Bad Request. Hệ thống hiển thị Toast lỗi: `"Mã học sinh đã tồn tại trên hệ thống."`

#### **TC_UI_UPDATE_003: Hủy bỏ thao tác chỉnh sửa (P2)**
*   **Mục đích:** Đảm bảo tính toàn vẹn của dữ liệu cũ nếu người dùng hủy thao tác.
*   **Các bước thực hiện:**
    1. Click nút "Chỉnh sửa" tại dòng học sinh bất kỳ.
    2. Xóa trắng trường "Họ và Tên", nhập lung tung các trường khác.
    3. Click nút "Hủy" (hoặc click ra ngoài modal, hoặc click dấu X của modal).
*   **Kết quả mong đợi:**
    *   Modal đóng lại.
    *   Dữ liệu của học sinh trên bảng danh sách và trong Database không có bất kỳ thay đổi nào.

---

### 4. KỊCH BẢN KIỂM THỬ XÓA / VÔ HIỆU HÓA HỌC SINH (SOFT DELETE / INACTIVE)

#### **TC_UI_DELETE_001: Vô hiệu hóa học sinh thành công (Soft Delete - P0)**
*   **Mục đích:** Đảm bảo hệ thống sử dụng phương pháp "Xóa mềm" (Soft Delete). Học sinh bị xóa sẽ chuyển trạng thái và biến mất khỏi UI chính nhưng dữ liệu gốc không bị mất vĩnh viễn trong DB để bảo toàn lịch sử.
*   **Các bước thực hiện:**
    1. Click biểu tượng thùng rác (IconTrash - Xóa) tại dòng của học sinh `Nguyễn Văn Anh`.
    2. Hộp thoại thông báo xác nhận của trình duyệt hoặc UI hiển thị: `"Bạn có chắc chắn muốn vô hiệu hóa học sinh này khỏi hệ thống?"`.
    3. Click "Xác nhận" (OK).
*   **Kết quả mong đợi:**
    *   Giao diện gọi API `DELETE /api/students/{id}` thành công.
    *   Hiển thị thông báo Toast: `"Xóa học sinh thành công (Soft-deleted)"`.
    *   **UI:** Học sinh `Nguyễn Văn Anh` biến mất khỏi danh sách quản lý học sinh hiện hành.
    *   **Database:** Truy vấn trực tiếp SQL `SELECT * FROM "Students" WHERE "Id" = '{Id_Của_Học_Sinh}'`:
        *   Dữ liệu bản ghi **vẫn tồn tại** (Không bị xóa cứng vật lý khỏi PostgreSQL).
        *   Cột `IsDeleted` chuyển thành `True`.
        *   Cột `Status` chuyển thành `2` (Tương ứng `StudentStatus.Inactive`).
        *   Trường `LastModifiedAt` cập nhật thời gian xóa.

#### **TC_UI_DELETE_002: Kiểm tra Global Query Filter ở Backend (P1)**
*   **Mục đích:** Đảm bảo các API lấy danh sách học sinh tự động loại bỏ các học sinh đã bị xóa mềm.
*   **Điều kiện đầu vào:** Học sinh `Nguyễn Văn Anh` đã thực hiện Soft Delete thành công ở bước trước (`IsDeleted` = `True`).
*   **Các bước thực hiện:**
    1. Reload lại trang web "Quản lý Học sinh".
    2. Thực hiện tìm kiếm học sinh `Nguyễn Văn Anh` bằng ô tìm kiếm.
    3. Gọi trực tiếp API qua Swagger: `GET /api/students` hoặc `GET /api/students/{id}` của học sinh đã xóa.
*   **Kết quả mong đợi:**
    *   Không thể tìm thấy học sinh `Nguyễn Văn Anh` trên UI nữa.
    *   API `GET /api/students` tuyệt đối không trả về thông tin học sinh này trong mảng `items`.
    *   API `GET /api/students/{id}` trả về mã lỗi `404 Not Found` kèm thông điệp `"Học sinh không tìm thấy."`.
    *   *Giải thích kỹ thuật:* Logic này hoạt động nhờ config `modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted)` trong DB Context hoạt động chuẩn xác.

#### **TC_UI_DELETE_003: Hủy bỏ thao tác xóa (P2)**
*   **Mục đích:** Đảm bảo người dùng có cơ hội sửa sai trước khi xóa dữ liệu.
*   **Các bước thực hiện:**
    1. Click biểu tượng "Xóa" tại một dòng học sinh bất kỳ.
    2. Hộp thoại xác nhận hiển thị. Click "Hủy" (Cancel).
*   **Kết quả mong đợi:**
    *   Hộp thoại đóng lại.
    *   Học sinh vẫn nằm trong danh sách với trạng thái hoạt động bình thường, không có thay đổi nào dưới Database.

---

### 5. KỊCH BẢN KIỂM THỬ BẢO MẬT & API (SECURITY & API ROBUSTNESS)

#### **TC_SEC_001: Bypass Validation từ Frontend bằng cách dùng Postman/Swagger gửi dữ liệu lỗi (P1)**
*   **Mục đích:** Đảm bảo Backend luôn thực hiện xác thực chặt chẽ (FluentValidation) phòng trường hợp kẻ xấu bypass lớp UI.
*   **Các bước thực hiện:**
    1. Sử dụng công cụ Postman hoặc Swagger UI để gửi trực tiếp một HTTP Request `POST /api/students` với Payload chứa dữ liệu sai nguyên tắc nghiệp vụ:
        ```json
        {
          "studentCode": "", // Trống
          "fullName": "A",
          "dateOfBirth": "2030-12-31T00:00:00Z", // Ngày tương lai
          "email": "invalid_email_format", // Sai email
          "phoneNumber": "abc12345", // Sai điện thoại
          "classId": "00000000-0000-0000-0000-000000000000" // Id trống rỗng
        }
        ```
*   **Kết quả mong đợi:**
    *   API Backend phải từ chối xử lý, trả về mã lỗi HTTP `400 Bad Request` hoặc `422 Unprocessable Entity`.
    *   Nội dung trả về phải hiển thị rõ các lỗi validation chi tiết của từng trường dưới định dạng JSON lỗi chuẩn để Frontend hiển thị.
    *   Không có bất kỳ dữ liệu rác nào được chèn vào DB PostgreSQL.

#### **TC_SEC_002: Kiểm thử chống tấn công SQL Injection thông qua trường Tìm kiếm (SearchTerm) (P1)**
*   **Mục đích:** Đảm bảo hệ thống an toàn trước các chuỗi truy vấn độc hại cố tình phá hoại Database.
*   **Dữ liệu kiểm thử:** Nhập vào ô Tìm kiếm chuỗi ký tự phá hoại: `' OR 1=1 --` hoặc `' UNION SELECT * FROM "Students" --`.
*   **Các bước thực hiện:**
    1. Điền chuỗi dữ liệu độc hại trên vào ô Tìm kiếm trên UI hoặc tham số `SearchTerm` trên API.
    2. Ấn Tìm kiếm / Gửi Request.
*   **Kết quả mong đợi:**
    *   Hệ thống không bị sập (Crash 500), không bị rò rỉ toàn bộ thông tin học sinh ra ngoài.
    *   Hệ thống coi chuỗi đó là một chuỗi ký tự thường để so sánh tìm kiếm và trả về kết quả rỗng (vì không có ai tên như vậy).
    *   *Giải thích kỹ thuật:* ORM Entity Framework Core sử dụng tham số hóa truy vấn (Parameterized Queries) tự động nên chống SQL Injection chuẩn xác.

---

## III. KẾT LUẬN & HƯỚNG DẪN THỰC THI CHO QA
1.  **Chạy khép kín luồng (End-to-End Test):** Khuyến nghị thực hiện theo chu trình: `TC_UI_CREATE_001` (Thêm mới) -> `TC_UI_READ_002` (Tìm kiếm đối tượng vừa tạo) -> `TC_UI_READ_005` (Xem chi tiết xem đúng chưa) -> `TC_UI_UPDATE_001` (Sửa thông tin) -> `TC_UI_DELETE_001` (Xóa mềm học sinh đó) -> `TC_UI_DELETE_002` (Xác minh không hiển thị nữa).
2.  **Đo lường thời gian đáp ứng (Performance SLA):** Mọi thao tác tìm kiếm, lọc, phân trang trên màn hình React/Mantine phải phản hồi trong vòng **< 500ms** ở môi trường mạng tiêu chuẩn. Thao tác lưu/sửa học sinh phản hồi trong vòng **< 1000ms**.