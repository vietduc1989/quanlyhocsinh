Chào bạn, tôi là QA Team Lead của dự án ONENET. Dưới đây là bộ tài liệu **Kế hoạch Kiểm thử (Test Plan) và Danh sách Kịch bản Kiểm thử Chi tiết (Detailed Test Cases)** cho Module **CRUD Học sinh - Phase 2** dựa trên tài liệu phân tích nghiệp vụ (BRD/SRS) và mã nguồn triển khai thực tế (.NET 10, PostgreSQL, React/Mantine).

Tài liệu này được thiết kế theo chuẩn QA chuyên nghiệp nhằm đảm bảo độ bao phủ (test coverage) tối đa từ mức API (Integration/Back-End) đến giao diện người dùng UI (E2E/Front-End) và kiểm tra tính nhất quán dữ liệu dưới DB (PostgreSQL).

---

# FILE: TestCases.md

# TÀI LIỆU KỊCH BẢN KIỂM THỬ (TEST CASES SPECIFICATION)
## MODULE: QUẢN LÝ HỌC SINH (CRUD) - PHASE 2
*   **Hệ thống:** ONENET
*   **Phiên bản tài liệu:** 1.0
*   **Tác giả:** QA Team ONENET
*   **Công nghệ kiểm thử:** Manual Test + API/UI Automation Ready

---

## I. THÔNG TIN CHUNG & QUY ƯỚC KIỂM THỬ

### 1. Phân loại mức độ nghiêm trọng (Severity)
*   **Blocker (S1):** Lỗi gây sập hệ thống, treo app, mất dữ liệu nghiêm trọng hoặc không thể thực hiện các bước kiểm thử tiếp theo.
*   **Critical (S2):** Lỗi nghiệp vụ chính không hoạt động (ví dụ: Không thêm được học sinh hợp lệ, xóa cứng thay vì xóa mềm...).
*   **Major (S3):** Lỗi xử lý dữ liệu sai lệch nhỏ, thiếu validation nghiệp vụ (ví dụ: Trùng mã học sinh nhưng vẫn lưu, ngày sinh ở tương lai nhưng không chặn...).
*   **Minor (S4):** Lỗi UI/UX, sai chính tả, căn lề, màu sắc thông báo không khớp thiết kế.

### 2. Môi trường kiểm thử (Test Environment)
*   **Back-End:** .NET 10 WebAPI, MediatR, FluentValidation.
*   **Database:** PostgreSQL v15+.
*   **Front-End:** React 18+, Mantine Core Components v7, `@tanstack/react-query`.
*   **Database Tool:** pgAdmin hoặc DBeaver.
*   **API Client:** Postman.

---

## II. DANH SÁCH KỊCH BẢN KIỂM THỬ CHI TIẾT

### PHẦN 1: API INTEGRATION TESTING (BACK-END)

#### US-HS-001: THÊM MỚI HỌC SINH (POST /api/students)

| Test Case ID | Mục tiêu kiểm thử | Dữ liệu đầu vào (Payload) | Kết quả mong đợi (API Response) | Xác thực Cơ sở dữ liệu (PostgreSQL) | Severity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **TC-API-001** | Thêm mới Học sinh thành công với thông tin hợp lệ đầy đủ. | `{ "studentCode": "HS2026-0001", "fullName": "Nguyễn Hoàng Vương", "dateOfBirth": "2010-05-15T00:00:00Z", "gender": "Nam", "address": "123 Trần Hưng Đạo, Hà Nội", "phoneNumber": "0912345678", "email": "hoangvuong@onenet.vn", "classId": "7c8b0561-bd3e-4861-ba0f-07e11942fc0c", "parentName": "Nguyễn Hoàng Long", "parentPhoneNumber": "0987654321" }` | - HTTP Status: **201 Created**<br>- Header Location chứa URL chi tiết học sinh vừa tạo.<br>- Response Body trả về chuỗi `Guid` định danh học sinh. | Truy vấn: `SELECT * FROM "Students" WHERE "StudentCode" = 'HS2026-0001';`<br>- Dữ liệu lưu đúng, đủ các trường.<br>- `"Status" = 1` (Active).<br>- `"IsDeleted" = false`.<br>- `"CreatedBy" = 'System_Dev'`.<br>- `"CreatedAt"` khớp thời gian UTC hiện tại. | **S2** |
| **TC-API-002** | Thêm mới thất bại do **Trùng Mã Học Sinh** (`StudentCode` đã tồn tại). | Gửi lại Payload của **TC-API-001** (Mã `"HS2026-0001"`). | - HTTP Status: **400 Bad Request**<br>- Response Body:<br>`{ "message": "Mã Học sinh 'HS2026-0001' đã tồn tại trong hệ thống." }` | Không có dòng dữ liệu mới nào được chèn thêm vào database. Tổng số lượng học sinh mang mã `"HS2026-0001"` vẫn là 1. | **S2** |
| **TC-API-003** | Thêm mới thất bại do **Thiếu các trường bắt buộc** (Mã học sinh, Họ tên, Ngày sinh, Giới tính, Lớp). | `{ "studentCode": "", "fullName": "", "dateOfBirth": "0001-01-01T00:00:00Z", "gender": "", "classId": "00000000-0000-0000-0000-000000000000" }` | - HTTP Status: **400 Bad Request** (Do FluentValidation chặn)<br>- Response chứa danh sách lỗi:<br>+ *"Mã học sinh không được để trống."*<br>+ *"Họ và tên học sinh không được để trống."*<br>+ *"Ngày sinh không được để trống."*<br>+ *"Giới tính không được để trống."*<br>+ *"Lớp học bắt buộc phải được chọn."* | Không lưu bản ghi lỗi vào Database. | **S2** |
| **TC-API-004** | Thêm mới thất bại do **Ngày sinh ở Tương lai/Hiện tại**. | Lấy payload hợp lệ nhưng sửa `"dateOfBirth"` thành ngày mai. | - HTTP Status: **400 Bad Request**<br>- Message lỗi: *"Ngày sinh phải ở quá khứ."* | Không lưu bản ghi lỗi vào Database. | **S2** |
| **TC-API-005** | Thêm mới thất bại do **Vượt quá độ dài ký tự tối đa** quy định trong Configuration. | `{ "studentCode": "HS-Cực-Dài-Hơn-50-Ký-Tự-1234567890-1234567890-1234567890", "fullName": "Nguyễn Văn A" }` | - HTTP Status: **400 Bad Request**<br>- Message lỗi: *"Mã học sinh không được dài quá 50 ký tự."* (Tương tự kiểm tra `fullName` > 150 ký tự). | Không lưu bản ghi lỗi vào Database. | **S3** |
| **TC-API-006** | Thêm mới thất bại do **Sai định dạng Email**. | Lấy payload hợp lệ nhưng sửa `"email"` thành `"hoangvuong@onenet...vn"`. | - HTTP Status: **400 Bad Request**<br>- Message lỗi: *"Địa chỉ Email không đúng định dạng."* | Không lưu bản ghi lỗi vào Database. | **S3** |

---

#### US-HS-002: LẤY DANH SÁCH HỌC SINH CÓ PHÂN TRANG (GET /api/students)

| Test Case ID | Mục tiêu kiểm thử | Tham số URL (Query Params) | Kết quả mong đợi (API Response) | Xác thực Cơ sở dữ liệu (PostgreSQL) | Severity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **TC-API-007** | Lấy danh sách mặc định (Page 1, Size 10). | `/api/students?pageNumber=1&pageSize=10` | - HTTP Status: **200 OK**<br>- Trả về đối tượng `PagedListDto` chứa các thuộc tính: `items`, `totalCount`, `pageNumber`, `pageSize`, `totalPages`.<br>- Các record không bị lộ trường nhạy cảm như `IsDeleted`. | Truy vấn:<br>`SELECT COUNT(*) FROM "Students" WHERE "IsDeleted" = false;`<br>Số lượng bản ghi trong database phải khớp với `totalCount` trả ra ở API. | **S2** |
| **TC-API-008** | Lọc danh sách học sinh theo **Lớp học** (`ClassId`). | `/api/students?classId=7c8b0561-bd3e-4861-ba0f-07e11942fc0c` | - HTTP Status: **200 OK**<br>- Toàn bộ học sinh trong mảng `items` đều có `classId` bằng giá trị lọc.<br>- `className` hiển thị đúng tên lớp thật từ DB (không phải mặc định). | Khớp với truy vấn Join:<br>`SELECT COUNT(*) FROM "Students" WHERE "ClassId" = '7c8b0561-bd3e-4861-ba0f-07e11942fc0c' AND "IsDeleted" = false;` | **S2** |
| **TC-API-009** | Tìm kiếm theo **Họ và Tên** hoặc **Mã học sinh** (Case-Insensitive). | `/api/students?searchTerm=hoanG vUonG` | - HTTP Status: **200 OK**<br>- Trả về các học sinh có Tên hoặc Mã chứa chuỗi tìm kiếm (không phân biệt hoa/thường). | Truy vấn kiểm chứng:<br>`SELECT * FROM "Students" WHERE LOWER("FullName") LIKE '%hoang vuong%' AND "IsDeleted" = false;` | **S2** |
| **TC-API-010** | Đảm bảo **Không hiển thị** học sinh đã bị Xóa mềm (`IsDeleted = true`). | Lấy toàn bộ danh sách. | - HTTP Status: **200 OK**<br>- Kiểm tra mảng `items` đảm bảo không chứa bất kỳ học sinh nào có thuộc tính `IsDeleted` là true trong DB. | Do backend cấu hình `builder.HasQueryFilter(s => !s.IsDeleted)`, EF Core tự động loại bỏ các bản ghi này. Đảm bảo tổng số lượng `totalCount` của API nhỏ hơn số bản ghi thực tế trong DB (nếu có bản ghi đã bị xóa mềm). | **S2** |

---

#### US-HS-003: XEM CHI TIẾT HỌC SINH (GET /api/students/{id})

| Test Case ID | Mục tiêu kiểm thử | Tham số URL | Kết quả mong đợi (API Response) | Xác thực Cơ sở dữ liệu (PostgreSQL) | Severity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **TC-API-011** | Lấy chi tiết học sinh thành công bằng ID hợp lệ. | `/api/students/71916327-1424-4f2b-8772-0f04e4a0555d` | - HTTP Status: **200 OK**<br>- Trả về đầy đủ thông tin Học sinh kèm theo tên lớp học tương ứng (`className`). | Dữ liệu trả ra từ API phải trùng khớp 100% với từng cột trong bảng `"Students"` và `"Classes"`. | **S2** |
| **TC-API-012** | Lấy chi tiết thất bại do **ID không tồn tại**. | `/api/students/00000000-0000-0000-0000-000000000000` | - HTTP Status: **404 Not Found**<br>- Response Body: `{ "message": "Học sinh không tồn tại." }` | Không áp dụng. | **S2** |
| **TC-API-013** | Lấy chi tiết thất bại đối với học sinh **đã bị xóa mềm**. | `/api/students/{deleted_id}` (ID của học sinh có `IsDeleted = true` trong DB). | - HTTP Status: **404 Not Found**<br>- Do cơ chế Query Filter chặn không cho truy xuất thực thể đã xóa mềm. | SQL test:<br>`SELECT * FROM "Students" WHERE "Id" = '{deleted_id}' AND "IsDeleted" = true;` (Dưới DB vẫn tồn tại bản ghi nhưng API phải chặn). | **S2** |

---

#### US-HS-004: CẬP NHẬT HỌC SINH (PUT /api/students/{id})

| Test Case ID | Mục tiêu kiểm thử | Đường dẫn & Payload | Kết quả mong đợi (API Response) | Xác thực Cơ sở dữ liệu (PostgreSQL) | Severity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **TC-API-014** | Cập nhật thông tin học sinh thành công (Đổi địa chỉ, số điện thoại, giữ nguyên Mã học sinh). | **PUT** `/api/students/71916327-1424-4f2b-8772-0f04e4a0555d`<br>Payload:<br>`{ "id": "71916327-1424-4f2b-8772-0f04e4a0555d", "studentCode": "HS2026-0001", "fullName": "Nguyễn Hoàng Vương (Updated)", "dateOfBirth": "2010-05-15T00:00:00Z", "gender": "Nam", "address": "Số 456 Đường Láng, Hà Nội", "classId": "7c8b0561-bd3e-4861-ba0f-07e11942fc0c", "status": 1 }` | - HTTP Status: **204 No Content** | Truy vấn:<br>`SELECT * FROM "Students" WHERE "Id" = '71916327-1424-4f2b-8772-0f04e4a0555d';`<br>- `"FullName" = 'Nguyễn Hoàng Vương (Updated)'`<br>- `"Address" = 'Số 456 Đường Láng, Hà Nội'`<br>- `"LastModifiedBy" = 'System_Dev'`<br>- `"LastModifiedAt"` cập nhật sang thời gian hiện tại. | **S2** |
| **TC-API-015** | Cập nhật thất bại do **Mã học sinh trùng với mã của học sinh khác**. | **PUT** `/api/students/71916327-1424-4f2b-8772-0f04e4a0555d`<br>Payload:<br>`{ ..., "studentCode": "HS2026-9999" }` (Trong đó mã này đã được đăng ký cho học sinh khác). | - HTTP Status: **400 Bad Request**<br>- Response Body:<br>`{ "message": "Mã Học sinh 'HS2026-9999' đã được sử dụng bởi học sinh khác." }` | Bản ghi cũ không bị thay đổi dữ liệu dưới Database. | **S2** |
| **TC-API-016** | Cập nhật thất bại do **ID trong URL không khớp với ID trong Payload body**. | **PUT** `/api/students/71916327-1424-4f2b-8772-0f04e4a0555d`<br>Payload:<br>`{ "id": "00000000-0000-0000-0000-000000000000", ... }` | - HTTP Status: **400 Bad Request**<br>- Response Body:<br>`{ "message": "Mã ID không khớp." }` | Bản ghi không bị thay đổi. | **S3** |

---

#### US-HS-005: XÓA MỀM HỌC SINH (DELETE /api/students/{id})

| Test Case ID | Mục tiêu kiểm thử | Tham số URL | Kết quả mong đợi (API Response) | Xác thực Cơ sở dữ liệu (PostgreSQL) | Severity |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **TC-API-017** | Xóa học sinh thành công (Yêu cầu nghiệp vụ bắt buộc **Xóa mềm**). | **DELETE** `/api/students/71916327-1424-4f2b-8772-0f04e4a0555d` | - HTTP Status: **204 No Content** | Truy vấn DB:<br>`SELECT "IsDeleted", "Status", "LastModifiedBy" FROM "Students" WHERE "Id" = '71916327-1424-4f2b-8772-0f04e4a0555d';`<br>- `"IsDeleted" = true` (Chứng tỏ không bị xóa cứng khỏi DB).<br>- `"Status" = 2` (Đã chuyển sang Inactive).<br>- `"LastModifiedBy" = 'System_Dev'`. | **S2** |
| **TC-API-018** | Xóa học sinh thất bại do ID học sinh không tồn tại. | **DELETE** `/api/students/00000000-0000-0000-0000-000000000000` | - HTTP Status: **404 Not Found**<br>- Response Body:<br>`{ "message": "Học sinh không tồn tại." }` | Không có sự thay đổi nào trong cơ sở dữ liệu. | **S2** |

---

### PHẦN 2: END-TO-END UI TESTING (FRONT-END REACT/MANTINE)

#### US-HS-001/004: THAO TÁC TRÊN FORM MODAL (THÊM / SỬA HỌC SINH)

| Test Case ID | Mục tiêu kiểm thử | Các bước thực hiện | Kết quả mong đợi (Giao diện người dùng) | Severity |
| :--- | :--- | :--- | :--- | :--- |
| **TC-UI-001** | Kiểm tra hiển thị đầy đủ các trường thông tin trên Modal Thêm mới. | 1. Truy cập trang Quản lý Học sinh.<br>2. Nhấn nút **"Thêm học sinh mới"**. | - Modal xuất hiện chính xác, căn giữa màn hình.<br>- Tiêu đề modal: *"Thêm mới học sinh"*. <br>- Hiển thị đầy đủ các trường nhập liệu tương ứng trong code:<br>  + Mã học sinh, Họ và tên (Yêu cầu)<br>  + Ngày sinh (Yêu cầu, type: date)<br>  + Giới tính (Select: Nam, Nữ, Khác)<br>  + Lớp học (Dropdown Select hiển thị danh sách lớp học)<br>  + Email, Địa chỉ, Họ tên phụ huynh, SĐT phụ huynh (Không bắt buộc) | **S3** |
| **TC-UI-002** | Kiểm tra cơ chế Client Validation ngay trên Form (Mantine Form). | 1. Trên Form Thêm mới, không điền gì.<br>2. Nhấn nút **"Lưu lại"**. | - Form hiển thị các dòng text báo lỗi đỏ phía dưới các input bắt buộc:<br>  + *Mã học sinh không được để trống*<br>  + *Họ tên không được để trống*<br>  + *Vui lòng chọn ngày sinh*<br>  + *Vui lòng chọn lớp học*<br>- Form không được submit lên hệ thống. | **S2** |
| **TC-UI-003** | Thêm mới thành công và đồng bộ danh sách phía ngoài. | 1. Điền đầy đủ dữ liệu hợp lệ vào form.<br>2. Nhấn **"Lưu lại"**. | - Modal tự động đóng lại.<br>- Thông báo toast notification hiện ở góc phải màn hình: **"Thành công"** - *"Thêm mới học sinh thành công!"* (Màu xanh lá - green).<br>- Danh sách học sinh bên ngoài tự động làm mới (Invalidate query) và hiển thị học sinh vừa thêm lên đầu hoặc theo thứ tự phân trang. | **S2** |
| **TC-UI-004** | Nhấn **"Sửa"** học sinh và hiển thị chính xác dữ liệu gốc. | 1. Chọn 1 học sinh trong danh sách.<br>2. Nhấn nút **"Sửa"** bên cột Hành động. | - Modal xuất hiện với tiêu đề: *"Cập nhật thông tin học sinh"*. <br>- Toàn bộ dữ liệu của học sinh được tự động điền (Pre-populated) vào các ô nhập liệu.<br>- Trường **Ngày sinh** hiển thị đúng định dạng `YYYY-MM-DD` trên ô input date. | **S2** |
| **TC-UI-005** | Nhấn nút **"Hủy"** hoặc click ra ngoài Modal khi đang nhập liệu dở. | 1. Nhấn nút "Sửa" một học sinh.<br>2. Thay đổi một số trường thông tin.<br>3. Nhấn nút **"Hủy"** (hoặc nhấn nút close `X` của Modal). | - Modal đóng lại.<br>- Khi mở lại, các trường đã thay đổi không bị lưu tạm mà khôi phục lại giá trị gốc ban đầu. | **S3** |

---

#### US-HS-002: TRANG DANH SÁCH & TRA CỨU HỌC SINH (StudentsPage)

| Test Case ID | Mục tiêu kiểm thử | Các bước thực hiện | Kết quả mong đợi (Giao diện người dùng) | Severity |
| :--- | :--- | :--- | :--- | :--- |
| **TC-UI-006** | Tìm kiếm nhanh tức thời (Instant Search). | 1. Nhập một cụm từ tìm kiếm vào ô *"Nhập tên hoặc mã học sinh..."* (Ví dụ: "HS001"). | - Danh sách tự động thực hiện tìm kiếm mà không cần bấm Enter.<br>- Bảng dữ liệu lọc đúng học sinh có mã "HS001".<br>- Trang hiện tại tự động chuyển về Trang 1 để tránh lỗi hiển thị nếu trang cũ không có dữ liệu. | **S3** |
| **TC-UI-007** | Lọc dữ liệu theo lớp bằng Select Dropdown. | 1. Nhấp chọn ô *"Lọc theo Lớp học"*.<br>2. Chọn một lớp học cụ thể (Ví dụ: *"Lớp 10A"*). | - Danh sách chỉ hiển thị các học sinh thuộc Lớp 10A.<br>- Nếu không có học sinh nào, bảng hiển thị dòng chữ thông báo: *"Không tìm thấy học sinh nào phù hợp."* | **S2** |
| **TC-UI-008** | Reset bộ lọc khi nhấn biểu tượng xóa lọc. | 1. Nhấn nút xóa bộ lọc lớp (nút `x` nhỏ trên component Select của Mantine). | - Dropdown xóa giá trị đã chọn.<br>- Bảng tự động tải lại danh sách đầy đủ tất cả các lớp của trang hiện tại. | **S3** |
| **TC-UI-009** | Chuyển trang (Pagination) khi tổng số bản ghi vượt quá 10. | 1. Nhấn nút số trang "2" hoặc nút mũi tên Next ở phần Pagination. | - Bảng dữ liệu hiển thị đúng danh sách học sinh trang tiếp theo.<br>- Thông báo ở dưới chân bảng hiển thị đúng dạng: *"Hiển thị [X] trên tổng số [Y] học sinh"*. | **S2** |

---

#### US-HS-003/005: XEM CHI TIẾT & HÀNH ĐỘNG XÓA (VÔ HIỆU HÓA)

| Test Case ID | Mục tiêu kiểm thử | Các bước thực hiện | Kết quả mong đợi (Giao diện người dùng) | Severity |
| :--- | :--- | :--- | :--- | :--- |
| **TC-UI-010** | Xem Chi tiết Học sinh (Trang StudentDetailPage). | 1. Click vào tên Học sinh hoặc nút xem chi tiết (nếu có). | - Chuyển hướng hoặc render component `StudentDetailPage` thay thế danh sách.<br>- Giao diện hiển thị dạng thẻ hồ sơ chuyên nghiệp.<br>- Các trường dữ liệu đều ở chế độ Chỉ đọc (Read-only).<br>- Trạng thái hiển thị Badge màu rõ ràng (Ví dụ: Active -> Badge xanh lá; Inactive -> Badge đỏ/xám). | **S2** |
| **TC-UI-011** | Chức năng quay lại trang Danh sách từ Trang Chi tiết. | 1. Đang ở trang Chi tiết Học sinh.<br>2. Nhấn nút **"Quay lại danh sách"**. | - Quay lại trang chủ Quản lý học sinh.<br>- Các cấu hình tìm kiếm, bộ lọc hoặc trang hiện tại trước đó vẫn được giữ nguyên (nếu có cache state). | **S3** |
| **TC-UI-012** | Thực hiện hành động Xóa học sinh (Confirm & Toast). | 1. Tại hàng học sinh muốn xóa, bấm nút **"Xóa"** màu đỏ.<br>2. Hệ thống bật thông báo xác nhận: *"Bạn có chắc chắn muốn vô hiệu hóa học sinh này khỏi hệ thống?"*<br>3. Bấm **"OK"** (Xác nhận). | - Hệ thống gửi request DELETE thành công.<br>- Gửi thông báo Toast màu xanh mòng két (teal): **"Xóa thành công"** - *"Thông tin học sinh đã được dọn dẹp mềm khỏi hệ thống."*<br>- Bản ghi biến mất khỏi bảng danh sách ngay lập tức. | **S2** |

---

## III. KỊCH BẢN KIỂM THỬ ĐẶC BIỆT & ĐỒNG THỜI (EDGE CASES)

Những trường hợp biên này rất quan trọng để đảm bảo tính ổn định tối đa của hệ thống trước các tác vụ không bình thường hoặc từ người dùng cuối.

### 1. Kiểm thử ngày sinh trong năm nhuận (Leap Year Date Check)
*   **Mô tả:** Nhập ngày sinh học sinh là ngày `29/02/2012` (năm nhuận) và `29/02/2013` (năm không nhuận).
*   **Kết quả mong đợi:**
    *   `29/02/2012`: Hệ thống chấp nhận và lưu trữ chính xác dưới Database.
    *   `29/02/2013`: Trình chọn ngày hoặc validator front-end/back-end phải báo lỗi ngày tháng không hợp lệ trước khi gửi lên DB.

### 2. Kiểm thử bảo mật đầu vào - Ngăn chặn tấn công SQL Injection
*   **Mô tả:** Nhập chuỗi đặc biệt vào trường tìm kiếm `searchTerm` hoặc `studentCode` nhằm phá vỡ câu lệnh SQL: `' OR '1'='1` hoặc `; DROP TABLE "Students"; --`.
*   **Kết quả mong đợi:**
    *   Hệ thống không phát sinh lỗi 500 Internal Server Error.
    *   Hệ thống xử lý chuỗi đầu vào như là một text thông thường (Do EF Core sử dụng Parameterized Queries ngầm định bảo mật).
    *   Trả về kết quả danh sách rỗng (Không bị lộ thông tin của toàn bộ học sinh).

### 3. Kiểm thử ngăn chặn tấn công XSS (Cross-Site Scripting)
*   **Mô tả:** Nhập mã độc JavaScript vào trường `Address` hoặc `fullName` khi thêm mới: `<script>alert('hacked')</script>`.
*   **Kết quả mong đợi:**
    *   Dữ liệu được lưu trữ dạng text thuần túy dưới DB.
    *   Khi hiển thị ra màn hình danh sách học sinh và trang chi tiết học sinh, hệ thống hiển thị nguyên văn chuỗi `<script>...` chứ không biên dịch và thực thi câu lệnh Javascript đó (React tự động mã hóa HTML thực thể để phòng chống XSS).

### 4. Kiểm thử xung đột đồng thời khi cập nhật (Concurrency Check)
*   **Mô tả:** Người dùng A mở modal chỉnh sửa của Học sinh X. Cùng lúc đó, Người dùng B thực hiện XÓA học sinh X này khỏi hệ thống. Sau đó, Người dùng A nhấn "Lưu lại" cập nhật.
*   **Kết quả mong đợi:**
    *   Khi người dùng A nhấn "Lưu", API kiểm tra dưới Database và nhận ra học sinh X đã bị xóa mềm (`IsDeleted = true`).
    *   Hệ thống ném ra lỗi `KeyNotFoundException` và trả về mã lỗi **404 Not Found** kèm message thân thiện: *"Học sinh không tồn tại."*

---

## IV. BIỂU MẪU BÁO CÁO LỖI TIÊU CHUẨN (BUG REPORT TEMPLATE)

Nếu phát hiện bất kỳ sai lệch nào so với "Kết quả mong đợi" trong bộ Test Case trên, vui lòng tạo ticket lỗi trên hệ thống quản lý (Jira/Azure DevOps) theo cấu trúc chuẩn sau:

```markdown
## [PHASE 2 BUG] [Mức độ nghiêm trọng] - [Tóm tắt ngắn gọn lỗi xảy ra]

**1. ID Test Case liên quan:** (Ví dụ: TC-API-002 / TC-UI-012)
**2. Môi trường kiểm thử:** Localhost / Staging - Trình duyệt Chrome v122
**3. Các bước tái hiện (Steps to Reproduce):**
   - Bước 1: Truy cập trang quản lý học sinh.
   - Bước 2: Nhấn vào nút "Thêm mới" học sinh.
   - Bước 3: Nhập mã học sinh trùng với mã đã có dưới Database.
   - Bước 4: Nhấn nút "Lưu lại".
**4. Kết quả thực tế (Actual Result):** 
   - Hệ thống báo lỗi 500 Internal Server Error / Hoặc vẫn lưu thành công gây trùng mã.
**5. Kết quả mong đợi (Expected Result):**
   - Phải báo lỗi 400 Bad Request kèm message "Mã Học sinh đã tồn tại..."
**6. Ảnh chụp màn hình / Logs lỗi API:** (Chèn ảnh hoặc mã lỗi console/Postman vào đây)
```

---
*Tài liệu này được phê duyệt bởi QA Lead của dự án ONENET và sẵn sàng chuyển giao cho đội ngũ Dev và QA tiến hành nghiệm thu kỹ thuật Phase 2.*