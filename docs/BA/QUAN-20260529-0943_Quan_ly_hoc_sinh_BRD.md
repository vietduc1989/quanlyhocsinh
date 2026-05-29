# BRD – QUAN-20260529-0943: Quản lý học sinh

**Hệ thống:** quanlyhocsinh  
**Phân hệ:** Quản lý học sinh  
**Phiên bản:** v1.0  
**Ngày:** 2026-05-29  

---

## 1. Mục tiêu
### Mục tiêu chính
Số hóa toàn bộ quy trình quản lý hồ sơ học sinh tại ONENET nhằm thay thế phương thức quản lý thủ công truyền thống. Hệ thống giúp tối ưu hóa thời gian nhập liệu, tăng độ chính xác của thông tin, hỗ trợ tra cứu nhanh chóng và đảm bảo tính toàn vẹn, bảo mật của dữ liệu học sinh thông qua cơ chế lưu trữ tập trung và kiểm vết (Audit Trail).

---

## 2. Phạm vi
### 2.1 In Scope
*   **Chức năng cốt lõi (CRUD):** Thêm mới, chỉnh sửa thông tin học sinh và xóa học sinh dưới dạng xóa mềm (Soft Delete).
*   **Quản lý danh sách:** Hiển thị danh sách học sinh, hỗ trợ phân trang chuẩn 20 bản ghi/trang.
*   **Tra cứu thông tin:** Tìm kiếm học sinh linh hoạt theo cả hai tiêu chí: Họ tên và Mã học sinh.
*   **Xử lý dữ liệu hàng loạt:** 
    *   Nhập danh sách học sinh từ file Excel mẫu (Import).
    *   Xuất danh sách học sinh hiện tại ra file Excel (Export).
*   **Cơ chế sinh mã tự động:** Hệ thống tự sinh mã định danh duy nhất cho từng học sinh theo cấu trúc quy định.
*   **Kiểm soát dữ liệu & Nhật ký:** Kiểm tra hợp lệ dữ liệu (Validation) và ghi log mọi tác động lên hệ thống (Audit Trail).

### 2.2 Out of Scope
*   Quản lý thông tin lớp học, phân lớp và chuyển lớp cho học sinh.
*   Quản lý kết quả học tập, điểm số, rèn luyện và học bạ điện tử.
*   Quản lý học phí, các khoản thu nộp và phát hành hóa đơn.
*   Cổng thông tin tương tác dành riêng cho phụ huynh/học sinh.

---

## 3. Nhóm người dùng
*   **Giáo vụ / Nhân viên tuyển sinh:** Nhóm người dùng chính thực hiện toàn bộ các tác vụ: Thêm mới, sửa, xóa, tìm kiếm học sinh, import/export danh sách và tải file mẫu.
*   **Ban giám hiệu / Quản trị viên hệ thống (Admin):** Xem danh sách học sinh, quản lý tìm kiếm thông tin, xuất file báo cáo Excel và kiểm tra nhật ký hệ thống (Audit Trail).
*   **Giáo viên chủ nhiệm:** Xem danh sách và tìm kiếm thông tin học sinh thuộc lớp/khối mình được phân quyền quản lý (không có quyền thêm, sửa, xóa hoặc import hệ thống).

---

## 4. User Flow nghiệp vụ

### 4.1. Quy trình thêm mới học sinh trực tiếp trên UI
1. Người dùng chọn chức năng **"Thêm mới học sinh"**.
2. Hệ thống hiển thị form nhập liệu thông tin học sinh.
3. Người dùng nhập các thông tin yêu cầu: *Họ tên, Ngày sinh, Giới tính, Địa chỉ, SĐT phụ huynh, Email*.
4. Người dùng bấm **"Lưu"**.
5. Hệ thống thực hiện kiểm tra dữ liệu đầu vào (Validation Rule):
   * *Nếu dữ liệu không hợp lệ:* Hệ thống hiển thị thông báo lỗi chi tiết tại các trường tương ứng để người dùng sửa đổi.
   * *Nếu dữ liệu hợp lệ:* Hệ thống tự động sinh Mã học sinh theo định dạng `HS-YYYYMMDD-XXXX`, lưu thông tin vào cơ sở dữ liệu với trạng thái hoạt động (Active), ghi nhận nhật ký hệ thống (Audit Trail) và hiển thị thông báo thành công.

### 4.2. Quy trình Import học sinh từ file Excel
1. Người dùng truy cập màn hình Danh sách học sinh và bấm chọn **"Import Excel"**.
2. Người dùng tải file Excel mẫu từ hệ thống, điền đầy đủ dữ liệu học sinh vào file và thực hiện tải (Upload) file lên hệ thống.
3. Hệ thống tiếp nhận file, đọc dữ liệu từng dòng và thực hiện kiểm tra định dạng dữ liệu (Validation):
   * *Trường hợp toàn bộ file hợp lệ:* Hệ thống lưu dữ liệu vào cơ sở dữ liệu, tự động sinh mã học sinh cho từng bản ghi, ghi nhận log và thông báo Import thành công.
   * *Trường hợp có dòng bị lỗi:* Hệ thống thực hiện Rollback giao dịch (hoặc bỏ qua dòng lỗi tùy cấu hình chi tiết), trả ra file Excel báo cáo lỗi chi tiết chỉ rõ dòng nào, cột nào không hợp lệ để người dùng sửa lại.

### 4.3. Quy trình Xóa học sinh (Soft Delete)
1. Tại danh sách học sinh, người dùng tìm kiếm và chọn học sinh cần xóa.
2. Người dùng nhấn nút **"Xóa"**.
3. Hệ thống hiển thị popup xác nhận: *"Bạn có chắc chắn muốn xóa học sinh [Tên Học Sinh] ra khỏi hệ thống không?"*.
4. Người dùng xác nhận **"Đồng ý"**.
5. Hệ thống thực hiện cập nhật trường trạng thái của học sinh thành `is_deleted = true`, ẩn học sinh khỏi danh sách hiển thị thông thường, lưu vết thao tác (Audit Trail) và hiển thị thông báo xóa thành công.

---

## 5. Functional Requirements

| ID | Chức năng | Mô tả chi tiết yêu cầu |
| :--- | :--- | :--- |
| **FR-01** | Thêm mới học sinh | Cho phép người dùng nhập trực tiếp thông tin học sinh qua giao diện Form bao gồm: Họ tên, Ngày sinh, Giới tính, Địa chỉ, SĐT phụ huynh, Email. Hệ thống tự động sinh mã học sinh duy nhất. |
| **FR-02** | Chỉnh sửa học sinh | Cho phép cập nhật các thông tin của học sinh hiện hành. Không cho phép sửa đổi Mã học sinh đã sinh ra trước đó. |
| **FR-03** | Xóa học sinh | Thực hiện cơ chế **Soft Delete** (Xóa mềm). Bản ghi của học sinh chỉ cập nhật cờ xóa `is_deleted = true` để lưu trữ lịch sử, không xóa vật lý khỏi database. |
| **FR-04** | Tìm kiếm học sinh | Cho phép tìm kiếm nhanh học sinh dựa trên ô tìm kiếm kết hợp (hoặc riêng biệt) theo: Họ và tên (tìm kiếm gần đúng) và Mã học sinh (tìm kiếm chính xác). |
| **FR-05** | Phân trang | Hiển thị danh sách học sinh dưới dạng bảng lưới, áp dụng phân trang cố định **20 bản ghi trên một trang** để đảm bảo hiệu năng tải trang. |
| **FR-06** | Import Excel | Cung cấp tính năng tải file mẫu `.xlsx`. Người dùng điền dữ liệu và upload lên hệ thống. Hệ thống thực hiện validate dữ liệu hàng loạt và thông báo số lượng bản ghi import thành công/thất bại. |
| **FR-07** | Export Excel | Cho phép xuất toàn bộ danh sách học sinh hiện hành hoặc xuất danh sách dựa trên kết quả đã lọc/tìm kiếm ra file định dạng Excel `.xlsx`. |

---

## 6. Non-functional Requirements

### Performance
*   Thời gian phản hồi (Response Time) đối với các thao tác truy vấn tìm kiếm và tải trang danh sách học sinh phải nhỏ hơn **1.5 giây** trong điều kiện quy mô dữ liệu đạt 100,000 bản ghi.
*   Thời gian xử lý luồng Import file Excel có dung lượng dưới 2000 dòng dữ liệu không quá **5 giây**.

### Security
*   Hệ thống áp dụng phân quyền truy cập theo vai trò người dùng (Role-Based Access Control - RBAC). Chỉ những tài khoản có quyền "Giáo vụ" hoặc "Admin" mới được thao tác thêm, sửa, xóa, import dữ liệu.
*   Mọi dữ liệu truyền tải giữa client và server phải được mã hóa qua giao thức an toàn **HTTPS (TLS 1.3)**.

### Availability
*   Hệ thống phải đảm bảo hoạt động liên tục với mức độ sẵn sàng cao, chỉ số uptime đạt tối thiểu **99.9%** (loại trừ các khoảng thời gian bảo trì định kỳ đã thông báo trước).

---

## 7. Business Rules

*   **BR-01: Quy tắc định dạng Mã học sinh (Unique ID)**
    *   Mỗi học sinh khi tạo mới thành công phải được hệ thống cấp duy nhất một Mã học sinh tự động tăng theo cấu trúc: `HS-YYYYMMDD-XXXX`
    *   Trong đó:
        *   `HS`: Tiền tố cố định.
        *   `YYYYMMDD`: Ngày tạo hồ sơ (Năm - Tháng - Ngày).
        *   `XXXX`: Số thứ tự tự động tăng từ `0001` đến `9999` reset lại theo mỗi ngày.
        *   *Ví dụ:* Học sinh đầu tiên được tạo vào ngày 29/05/2026 sẽ có mã là `HS-20260529-0001`.

*   **BR-02: Quy tắc kiểm tra dữ liệu đầu vào (Validation Rules)**
    *   **Họ tên:** Là trường bắt buộc nhập, không được bỏ trống. Không được phép chứa các ký tự số hoặc các ký tự đặc biệt (ngoại trừ khoảng trắng).
    *   **Ngày sinh:** Là trường bắt buộc nhập. Ngày sinh của học sinh phải luôn nhỏ hơn ngày hiện tại của hệ thống (`Ngày sinh < Current_Date`).
    *   **Email:** Nếu có nhập, phải tuân thủ đúng định dạng Email chuẩn RFC 5322 (Ví dụ: `nguyenvana@gmail.com`).
    *   **SĐT phụ huynh:** Phải là định dạng số điện thoại hợp lệ tại Việt Nam (gồm 10 chữ số và bắt đầu bằng các đầu số di động hiện hành như 03, 05, 07, 08, 09).

*   **BR-03: Quy tắc xóa mềm (Soft Delete)**
    *   Bản ghi bị xóa sẽ không hiển thị trên danh sách tìm kiếm và danh sách hoạt động thường nhật.
    *   Tuy nhiên, các dữ liệu lịch sử liên quan đến học sinh này vẫn được giữ lại nguyên vẹn trong cơ sở dữ liệu nhằm phục vụ mục đích báo cáo tài chính hoặc đối soát học bạ cũ nếu cần.

---

## 8. Integration Requirements

*   **Tích hợp hệ thống lưu trữ:** Tích hợp với dịch vụ lưu trữ file tập trung (như AWS S3 hoặc hệ thống File Server nội bộ của ONENET) để lưu trữ và truy xuất các file mẫu Excel, cũng như lưu trữ file báo cáo kết quả lỗi phát sinh khi người dùng Import không thành công.
*   **Hỗ trợ API kết nối nội bộ:** Phát triển các RESTful API chuẩn hóa, trả dữ liệu dạng JSON dựa trên Mã học sinh (`HS-YYYYMMDD-XXXX`) để sẵn sàng tích hợp với các phân hệ mở rộng trong tương lai của ONENET (như phân hệ Quản lý Điểm số, phân hệ Điểm danh thông minh, phân hệ Quản lý Học phí).

---

## 9. Audit Trail

Để đảm bảo tính minh bạch thông tin và phục vụ công tác rà soát bảo mật dữ liệu học sinh, hệ thống yêu cầu ghi nhận nhật ký chi tiết cho **tất cả các thao tác CRUD** trực tiếp từ người dùng. Mỗi bản ghi nhật ký (Audit Log) bắt buộc phải chứa đầy đủ các thông tin sau:

| Tên trường thông tin | Mô tả dữ liệu lưu trữ | Ví dụ thực tế |
| :--- | :--- | :--- |
| **Log ID** | Mã định danh duy nhất của bản ghi log | `LOG-987654321` |
| **User ID / Username** | Tài khoản của nhân viên thực hiện thao tác | `lan.nguyen@onenet.vn` |
| **Action** | Thao tác tác động (CREATE, UPDATE, DELETE, IMPORT, EXPORT) | `UPDATE` |
| **Object Affected** | Thực thể bị tác động (Mã học sinh chịu ảnh hưởng) | `HS-20260529-0001` |
| **Timestamp** | Thời điểm chính xác thực hiện hành động | `2026-05-29 10:15:30.452` |
| **IP Address** | Địa chỉ IP của máy khách gửi yêu cầu | `192.168.1.45` |
| **Old Values** | Trạng thái dữ liệu trước khi thay đổi (chỉ áp dụng cho Update/Delete) | `{"dia_chi": "12 Chùa Bộc, Đống Đa, Hà Nội"}` |
| **New Values** | Trạng thái dữ liệu mới được lưu (áp dụng cho Create/Update) | `{"dia_chi": "25 Tây Sơn, Đống Đa, Hà Nội"}` |

---

## 10. KPI theo dõi

*   **Tỷ lệ nhập liệu thành công (Import Success Rate):** Đạt tối thiểu **95%** tổng số dòng dữ liệu trên file Excel được đưa vào hệ thống thành công ngay trong lần import đầu tiên của giáo vụ (sau khi đã cung cấp file mẫu chuẩn hóa).
*   **Thời gian hoàn thành thao tác trực tiếp (Entry Time):** Thời gian trung bình để một nhân viên giáo vụ hoàn thiện form thêm mới trực tiếp một học sinh trên giao diện Web UI không vượt quá **45 giây**.
*   **Tỷ lệ lỗi dữ liệu trùng lặp (Zero Duplication Rate):** Hệ thống phải phát hiện và ngăn chặn trùng lặp mã học sinh đạt tỷ lệ tuyệt đối **100%** trong suốt quá trình vận hành hệ thống.
*   **Tốc độ xử lý tìm kiếm (Search Latency):** Thời gian trả về kết quả tìm kiếm học sinh theo Tên hoặc Mã học sinh trên giao diện hiển thị danh sách phải luôn dưới **1 giây**.