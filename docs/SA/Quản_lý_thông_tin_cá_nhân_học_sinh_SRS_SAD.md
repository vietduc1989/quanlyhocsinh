Tuyệt vời! Với vai trò là Solution Architect của ONENET, tôi sẽ phân tích BRD này để tạo ra tài liệu SRS và SAD chi tiết.

Dựa trên yêu cầu, tôi sẽ sử dụng các công cụ và quy tắc thiết kế kiến trúc hệ thống để đảm bảo tính nhất quán, hiệu năng và khả năng mở rộng. Đối với việc kiểm tra schema hiện tại, vì không có thông tin về schema hiện có được cung cấp, tôi sẽ thiết kế schema mới cho phần quản lý học sinh và đảm bảo tính duy nhất của Mã học sinh theo yêu cầu nghiệp vụ, cũng như thêm một khóa chính nội bộ để quản lý dữ liệu hiệu quả hơn.

---

# TÀI LIỆU TỔNG HỢP: SRS & SAD - PHÂN HỆ QUẢN LÝ HỌC SINH

## Mục lục

**PHẦN 1: TÀI LIỆU ĐẶC TẢ YÊU CẦU PHẦN MỀM (SRS)**
1.  Giới thiệu
    1.1. Mục đích
    1.2. Phạm vi Hệ thống
    1.3. Đối tượng sử dụng
    1.4. Định nghĩa và Viết tắt
    1.5. Tổng quan tài liệu
2.  Mô tả Tổng quan Hệ thống
    2.1. Mô tả Sản phẩm/Hệ thống
    2.2. Chức năng của Sản phẩm
    2.3. Vai trò Người dùng
    2.4. Các ràng buộc
3.  Yêu cầu Chức năng (Functional Requirements)
    3.1. ONENET-HS-001: Thêm học sinh mới
    3.2. ONENET-HS-002: Sửa thông tin học sinh
    3.3. ONENET-HS-003: Xóa học sinh
    3.4. ONENET-HS-004: Xem danh sách học sinh và tìm kiếm/lọc
4.  Yêu cầu Phi Chức năng (Non-functional Requirements)
    4.1. ONENET-NFR-001: Giao diện thân thiện, dễ sử dụng (Usability)
    4.2. Hiệu năng (Performance)
    4.3. Bảo mật (Security)
    4.4. Khả năng mở rộng (Scalability)
    4.5. Khả năng bảo trì (Maintainability)
5.  Quy tắc Nghiệp vụ (Business Rules)
    5.1. BR-HS-001: Tính duy nhất của Mã học sinh
    5.2. BR-HS-002: Các trường bắt buộc
    5.3. BR-HS-003: Định dạng ngày sinh
    5.4. BR-HS-004: Định dạng số điện thoại
    5.5. BR-HS-005: Tính toàn vẹn dữ liệu khi xóa
6.  Mô hình Dữ liệu Logic (High-level Logical Data Model)

**PHẦN 2: TÀI LIỆU KIẾN TRÚC HỆ THỐNG (SAD)**
1.  Giới thiệu
    1.1. Mục đích
    1.2. Phạm vi tài liệu
    1.3. Đối tượng độc giả
2.  Kiến trúc Tổng quan
    2.1. Tầm nhìn Kiến trúc
    2.2. Sơ đồ Kiến trúc Tổng quan
    2.3. Các lựa chọn Công nghệ (Tech Stack)
3.  Kiến trúc Chi tiết
    3.1. Kiến trúc Ứng dụng (Application Architecture)
        3.1.1. Frontend (React với Mantine UI)
        3.1.2. Backend (.NET 10 - Clean Architecture)
    3.2. Kiến trúc Dữ liệu (Data Architecture)
        3.2.1. Cơ sở dữ liệu (PostgreSQL)
        3.2.2. Schema thiết kế
    3.3. Kiến trúc Hạ tầng (Infrastructure Architecture)
4.  Các quyết định thiết kế chính
5.  Các vấn đề cần xem xét thêm

---

# PHẦN 1: TÀI LIỆU ĐẶC TẢ YÊU CẦU PHẦN MỀM (SRS)

## 1. Giới thiệu

### 1.1. Mục đích
Tài liệu này đặc tả chi tiết các yêu cầu chức năng và phi chức năng cho phân hệ "Quản lý Thông tin Cá nhân Học sinh" thuộc hệ thống ONENET. Mục đích là cung cấp một bản mô tả rõ ràng, đầy đủ và nhất quán về các yêu cầu, làm cơ sở cho việc thiết kế, phát triển và kiểm thử phần mềm.

### 1.2. Phạm vi Hệ thống
Phạm vi của tài liệu này tập trung vào các chức năng liên quan đến việc quản lý thông tin cá nhân của học sinh, bao gồm: thêm mới, xem danh sách, tìm kiếm, lọc, chỉnh sửa và xóa thông tin học sinh trong hệ thống ONENET.

### 1.3. Đối tượng sử dụng
*   Business Analysts (BA)
*   Solution Architects (SA)
*   Development Team (Backend Developers, Frontend Developers)
*   Quality Assurance (QA) Team
*   Project Managers (PM)
*   Stakeholders/End-users (Giáo vụ, Quản trị viên)

### 1.4. Định nghĩa và Viết tắt
*   **ONENET:** Tên hệ thống chung.
*   **HS:** Học sinh.
*   **BRD:** Business Requirement Document.
*   **SRS:** Software Requirements Specification.
*   **SAD:** System Architecture Document.
*   **API:** Application Programming Interface.
*   **UI:** User Interface.
*   **UX:** User Experience.
*   **Giáo vụ:** Người chịu trách nhiệm quản lý hồ sơ và thông tin học sinh.

### 1.5. Tổng quan tài liệu
Phần này mô tả tổng quan về các yêu cầu nghiệp vụ, các chức năng chính, vai trò người dùng, ràng buộc công nghệ. Sau đó, đi sâu vào các yêu cầu chức năng và phi chức năng chi tiết, cùng với các quy tắc nghiệp vụ và mô hình dữ liệu logic.

## 2. Mô tả Tổng quan Hệ thống

### 2.1. Mô tả Sản phẩm/Hệ thống
Hệ thống ONENET là một hệ thống quản lý toàn diện. Phân hệ "Quản lý Học sinh" là một phần của ONENET, cho phép các vai trò được cấp phép (đặc biệt là Giáo vụ) thực hiện các thao tác quản lý thông tin cá nhân của học sinh, đảm bảo dữ liệu chính xác và cập nhật kịp thời.

### 2.2. Chức năng của Sản phẩm
Các chức năng chính của phân hệ bao gồm:
*   Thêm mới thông tin học sinh vào hệ thống.
*   Cập nhật các thông tin cá nhân của học sinh.
*   Xóa thông tin học sinh khỏi hệ thống.
*   Xem danh sách học sinh, cùng với khả năng tìm kiếm và lọc dữ liệu.

### 2.3. Vai trò Người dùng
*   **Giáo vụ:** Có đầy đủ quyền để thêm, sửa, xóa, xem, tìm kiếm và lọc thông tin học sinh.

### 2.4. Các ràng buộc
*   **Công nghệ (Tech Stack):**
    *   Database: PostgreSQL
    *   Backend: .NET 10 (theo Clean Architecture)
    *   Frontend: React (sử dụng Mantine UI library)
*   **Thời gian:** Mọi chức năng phải hoạt động ổn định và đáp ứng kịp thời.
*   **Dữ liệu:** Các yêu cầu về tính duy nhất, định dạng và tính toàn vẹn của dữ liệu phải được tuân thủ nghiêm ngặt.
*   **Kiến trúc:** Phải tuân thủ mô hình Clean Architecture cho Backend.

## 3. Yêu cầu Chức năng (Functional Requirements)

Dựa trên các User Stories và Acceptance Criteria trong BRD:

### 3.1. ONENET-HS-001: Thêm học sinh mới
*   **ID:** FR-HS-001
*   **Mô tả:** Hệ thống phải cho phép Giáo vụ thêm một hồ sơ học sinh mới vào hệ thống với các thông tin cá nhân.
*   **Các yêu cầu con:**
    *   **FR-HS-001.1: Giao diện nhập liệu:** Hệ thống phải cung cấp một giao diện người dùng cho phép Giáo vụ nhập các thông tin sau của học sinh: Họ và tên, Ngày sinh, Giới tính, Địa chỉ, Số điện thoại phụ huynh, Lớp học, Mã học sinh.
    *   **FR-HS-001.2: Kiểm tra dữ liệu bắt buộc:** Hệ thống phải kiểm tra và yêu cầu Giáo vụ nhập đầy đủ các trường bắt buộc (Họ và tên, Ngày sinh, Giới tính, Lớp học, Mã học sinh) trước khi lưu. Nếu thiếu, hệ thống phải hiển thị thông báo lỗi tương ứng.
    *   **FR-HS-001.3: Kiểm tra tính duy nhất của Mã học sinh:** Hệ thống phải kiểm tra xem "Mã học sinh" nhập vào đã tồn tại trong hệ thống hay chưa. Nếu đã tồn tại, hệ thống phải hiển thị thông báo lỗi và không cho phép lưu.
    *   **FR-HS-001.4: Kiểm tra định dạng dữ liệu:** Hệ thống phải kiểm tra định dạng và tính hợp lệ của các trường dữ liệu như:
        *   Ngày sinh: Phải là một ngày hợp lệ và không được lớn hơn ngày hiện tại.
        *   Số điện thoại phụ huynh: Phải chứa các ký tự số và có độ dài hợp lệ (theo chuẩn Việt Nam).
        *   Các trường khác theo yêu cầu định dạng cụ thể.
        Nếu định dạng không hợp lệ, hệ thống phải hiển thị thông báo lỗi tương ứng.
    *   **FR-HS-001.5: Lưu trữ thông tin:** Khi tất cả thông tin hợp lệ, hệ thống phải lưu trữ thông tin học sinh vào cơ sở dữ liệu.
    *   **FR-HS-001.6: Thông báo xác nhận:** Sau khi thêm thành công, hệ thống phải hiển thị thông báo "Thêm học sinh thành công".
    *   **FR-HS-001.7: Hiển thị danh sách:** Học sinh mới được thêm phải xuất hiện trong danh sách học sinh.

### 3.2. ONENET-HS-002: Sửa thông tin học sinh
*   **ID:** FR-HS-002
*   **Mô tả:** Hệ thống phải cho phép Giáo vụ chỉnh sửa thông tin cá nhân của một học sinh đã tồn tại.
*   **Các yêu cầu con:**
    *   **FR-HS-002.1: Truy cập chức năng sửa:** Hệ thống phải cung cấp chức năng "Sửa" trên màn hình chi tiết hoặc danh sách học sinh để Giáo vụ truy cập chế độ chỉnh sửa.
    *   **FR-HS-002.2: Hiển thị thông tin hiện có:** Hệ thống phải hiển thị các thông tin hiện tại của học sinh vào giao diện chỉnh sửa.
    *   **FR-HS-002.3: Hạn chế chỉnh sửa Mã học sinh:** Hệ thống không được phép cho phép Giáo vụ chỉnh sửa trường "Mã học sinh" sau khi đã tạo.
    *   **FR-HS-002.4: Kiểm tra định dạng dữ liệu khi cập nhật:** Tương tự FR-HS-001.4, hệ thống phải kiểm tra định dạng và tính hợp lệ của các trường thông tin được chỉnh sửa. Nếu định dạng không hợp lệ, hệ thống phải hiển thị thông báo lỗi.
    *   **FR-HS-002.5: Lưu trữ thay đổi:** Khi thông tin chỉnh sửa hợp lệ, hệ thống phải lưu các thay đổi vào hồ sơ học sinh tương ứng trong cơ sở dữ liệu.
    *   **FR-HS-002.6: Thông báo xác nhận:** Sau khi cập nhật thành công, hệ thống phải hiển thị thông báo "Cập nhật thông tin học sinh thành công".
    *   **FR-HS-002.7: Cập nhật hiển thị:** Thông tin của học sinh trên giao diện phải được cập nhật ngay lập tức theo các thay đổi mới.
    *   **FR-HS-002.8: Hủy bỏ thao tác:** Hệ thống phải cung cấp chức năng "Hủy" để Giáo vụ có thể thoát khỏi chế độ chỉnh sửa mà không lưu bất kỳ thay đổi nào.

### 3.3. ONENET-HS-003: Xóa học sinh
*   **ID:** FR-HS-003
*   **Mô tả:** Hệ thống phải cho phép Giáo vụ xóa thông tin của một học sinh khỏi hệ thống.
*   **Các yêu cầu con:**
    *   **FR-HS-003.1: Truy cập chức năng xóa:** Hệ thống phải cung cấp chức năng "Xóa" trên danh sách hoặc màn hình chi tiết học sinh.
    *   **FR-HS-003.2: Xác nhận xóa:** Khi Giáo vụ chọn xóa, hệ thống phải hiển thị hộp thoại xác nhận với thông tin "Bạn có chắc chắn muốn xóa học sinh [Tên học sinh] không? Thao tác này không thể hoàn tác."
    *   **FR-HS-003.3: Thực hiện xóa:** Nếu Giáo vụ xác nhận xóa, hệ thống phải xóa thông tin học sinh đó cùng với các dữ liệu liên quan trực tiếp khỏi cơ sở dữ liệu.
    *   **FR-HS-003.4: Cập nhật danh sách:** Học sinh bị xóa không còn xuất hiện trong danh sách học sinh.
    *   **FR-HS-003.5: Thông báo xác nhận:** Sau khi xóa thành công, hệ thống phải hiển thị thông báo "Xóa học sinh thành công".
    *   **FR-HS-003.6: Hủy bỏ thao tác xóa:** Nếu Giáo vụ chọn "Hủy" trong hộp thoại xác nhận, hệ thống không được xóa học sinh và thông tin của học sinh vẫn được giữ nguyên.

### 3.4. ONENET-HS-004: Xem danh sách học sinh và tìm kiếm/lọc
*   **ID:** FR-HS-004
*   **Mô tả:** Hệ thống phải cho phép Giáo vụ xem danh sách tất cả học sinh và có khả năng tìm kiếm, lọc theo các tiêu chí khác nhau.
*   **Các yêu cầu con:**
    *   **FR-HS-004.1: Hiển thị danh sách:** Hệ thống phải hiển thị danh sách tất cả học sinh hiện có khi Giáo vụ truy cập chức năng "Quản lý học sinh".
    *   **FR-HS-004.2: Thông tin hiển thị cơ bản:** Mỗi học sinh trong danh sách phải hiển thị tối thiểu các thông tin: Mã học sinh, Họ và tên, Lớp học, Ngày sinh, Giới tính.
    *   **FR-HS-004.3: Tìm kiếm theo từ khóa:** Hệ thống phải cung cấp ô tìm kiếm cho phép Giáo vụ nhập từ khóa. Tìm kiếm phải hoạt động trên các trường như Họ và tên, Mã học sinh, Lớp học, Số điện thoại phụ huynh. Tìm kiếm không phân biệt chữ hoa/thường.
    *   **FR-HS-004.4: Lọc theo tiêu chí:** Hệ thống phải cung cấp các tùy chọn lọc theo tiêu chí (ví dụ: Lớp học, Giới tính).
    *   **FR-HS-004.5: Kết hợp tiêu chí:** Hệ thống phải hỗ trợ kết hợp nhiều tiêu chí lọc và tìm kiếm.
    *   **FR-HS-004.6: Xóa/Thiết lập lại bộ lọc:** Hệ thống phải có chức năng để xóa bỏ tất cả các tiêu chí tìm kiếm và lọc, hiển thị lại danh sách học sinh ban đầu.

## 4. Yêu cầu Phi Chức năng (Non-functional Requirements)

### 4.1. ONENET-NFR-001: Giao diện thân thiện, dễ sử dụng (Usability)
*   **Mô tả:** Giao diện người dùng phải trực quan, dễ hiểu, dễ điều hướng. Các thao tác thêm, sửa, xóa, xem phải rõ ràng, có hướng dẫn cần thiết và phản hồi rõ ràng từ hệ thống.
*   **Tiêu chí chấp nhận:**
    *   Giáo vụ mới có thể thực hiện các thao tác cơ bản (thêm, xem, sửa) sau thời gian làm quen dưới 5 phút cho mỗi chức năng.
    *   Các nút chức năng phải được đặt ở vị trí hợp lý và dễ nhìn thấy.
    *   Màu sắc và bố cục phải hài hòa, tuân thủ hướng dẫn thiết kế của Mantine UI và không gây mỏi mắt khi sử dụng lâu.
    *   Phản hồi của hệ thống (thông báo lỗi, thông báo thành công) phải rõ ràng, dễ hiểu và nhất quán.

### 4.2. Hiệu năng (Performance)
*   **Mô tả:** Hệ thống phải đáp ứng nhanh chóng các yêu cầu của người dùng.
*   **Tiêu chí chấp nhận:**
    *   Thời gian tải danh sách học sinh (tối đa 1000 bản ghi) không quá 2 giây.
    *   Thời gian thực hiện các thao tác thêm, sửa, xóa học sinh không quá 1 giây (không bao gồm thời gian truyền tải dữ liệu mạng).
    *   Tìm kiếm và lọc dữ liệu phải phản hồi trong vòng 1-2 giây.

### 4.3. Bảo mật (Security)
*   **Mô tả:** Thông tin học sinh là dữ liệu nhạy cảm, hệ thống phải đảm bảo tính bảo mật.
*   **Tiêu chí chấp nhận:**
    *   Chỉ Giáo vụ hoặc Quản trị viên đã xác thực mới có thể truy cập các chức năng quản lý học sinh.
    *   Phân quyền truy cập theo vai trò.
    *   Dữ liệu truyền tải giữa client và server phải được mã hóa (HTTPS).
    *   Hệ thống phải có cơ chế chống tấn công XSS, SQL Injection.

### 4.4. Khả năng mở rộng (Scalability)
*   **Mô tả:** Hệ thống phải có khả năng mở rộng để xử lý số lượng học sinh và người dùng tăng lên trong tương lai.
*   **Tiêu chí chấp nhận:**
    *   Kiến trúc Microservices (hoặc Monolith với khả năng chia nhỏ) để mở rộng các dịch vụ riêng lẻ.
    *   Cơ sở dữ liệu có khả năng mở rộng (ví dụ: replication, sharding).

### 4.5. Khả năng bảo trì (Maintainability)
*   **Mô tả:** Hệ thống phải dễ dàng nâng cấp, sửa lỗi và thêm tính năng mới.
*   **Tiêu chí chấp nhận:**
    *   Mã nguồn phải rõ ràng, có cấu trúc tốt theo Clean Architecture.
    *   Tài liệu hóa đầy đủ cho các thành phần chính.
    *   Sử dụng các công cụ quản lý phiên bản (Git).

## 5. Quy tắc Nghiệp vụ (Business Rules)

### 5.1. BR-HS-001: Tính duy nhất của Mã học sinh
*   Mỗi học sinh trong hệ thống phải có một Mã học sinh duy nhất. Mã học sinh không thể trùng lặp. Mã học sinh là duy nhất trên toàn hệ thống ONENET.

### 5.2. BR-HS-002: Các trường bắt buộc
*   Họ và tên, Ngày sinh, Giới tính, Mã học sinh, Lớp học là các trường thông tin bắt buộc và không được phép để trống khi thêm mới hoặc cập nhật.

### 5.3. BR-HS-003: Định dạng ngày sinh
*   Ngày sinh phải là một ngày hợp lệ (ví dụ: không phải 30/02) và không được là ngày trong tương lai (lớn hơn ngày hiện tại). Định dạng ngày tháng năm phải thống nhất (ví dụ: DD/MM/YYYY).

### 5.4. BR-HS-004: Định dạng số điện thoại
*   Số điện thoại phụ huynh phải có định dạng số hợp lệ (chỉ chứa chữ số, có thể có dấu "+" ở đầu, độ dài hợp lệ theo chuẩn điện thoại Việt Nam).

### 5.5. BR-HS-005: Tính toàn vẹn dữ liệu khi xóa
*   Khi xóa một học sinh, tất cả dữ liệu liên quan trực tiếp đến thông tin cá nhân của học sinh đó (ví dụ: các bản ghi trong bảng `HocSinh`) phải được xóa bỏ. Cần lưu ý các ràng buộc khóa ngoại (Foreign Key) nếu có liên kết với các phân hệ khác trong tương lai (ví dụ: điểm số, chuyên cần, thông tin thi cử) để quyết định phương án xử lý (xóa cascade, xóa mềm, hoặc ngăn không cho xóa). Đối với yêu cầu hiện tại, chỉ xóa thông tin cá nhân.

## 6. Mô hình Dữ liệu Logic (High-level Logical Data Model)

**Entity: HocSinh (Student)**

| Attribute Name              | Data Type      | Constraints           | Description                                    |
| :-------------------------- | :------------- | :-------------------- | :--------------------------------------------- |
| `HocSinhId`                 | GUID           | Primary Key (PK)      | Định danh duy nhất của học sinh (khóa nội bộ)  |
| `MaHocSinh`                 | VARCHAR(20)    | Unique, Not Null      | Mã học sinh duy nhất, không trùng lặp         |
| `HoTen`                     | NVARCHAR(100)  | Not Null              | Họ và tên đầy đủ của học sinh                 |
| `NgaySinh`                  | DATE           | Not Null              | Ngày tháng năm sinh của học sinh             |
| `GioiTinh`                  | NVARCHAR(10)   | Not Null              | Giới tính (Nam, Nữ, Khác)                      |
| `DiaChi`                    | NVARCHAR(255)  | Nullable              | Địa chỉ liên hệ của học sinh                  |
| `SoDienThoaiPhuHuynh`       | VARCHAR(15)    | Nullable              | Số điện thoại của phụ huynh                   |
| `LopHoc`                    | NVARCHAR(20)   | Not Null              | Lớp học hiện tại của học sinh                 |
| `NgayTao`                   | TIMESTAMP WITH TIME ZONE | Not Null              | Thời điểm bản ghi được tạo                     |
| `NguoiTao`                  | NVARCHAR(50)   | Not Null              | Người tạo bản ghi (User ID/Name)               |
| `NgayCapNhat`               | TIMESTAMP WITH TIME ZONE | Nullable              | Thời điểm bản ghi được cập nhật gần nhất      |
| `NguoiCapNhat`              | NVARCHAR(50)   | Nullable              | Người cập nhật bản ghi gần nhất (User ID/Name)|

*Lưu ý về `HocSinhId` và `MaHocSinh`: `HocSinhId` được dùng làm khóa chính nội bộ, giúp việc quản lý mối quan hệ trong CSDL dễ dàng và hiệu quả hơn, độc lập với mã nghiệp vụ. `MaHocSinh` là mã nghiệp vụ duy nhất được yêu cầu bởi BA, sẽ được định nghĩa là một ràng buộc UNIQUE.*

---

# PHẦN 2: TÀI LIỆU KIẾN TRÚC HỆ THỐNG (SAD)

## 1. Giới thiệu

### 1.1. Mục đích
Tài liệu này mô tả kiến trúc tổng thể và chi tiết của phân hệ "Quản lý Thông tin Cá nhân Học sinh" trong hệ thống ONENET, dựa trên các yêu cầu từ SRS và tuân thủ các quy định công nghệ bắt buộc. Mục đích là cung cấp một cái nhìn toàn diện về cấu trúc hệ thống, cách thức các thành phần tương tác, và các quyết định thiết kế quan trọng.

### 1.2. Phạm vi tài liệu
Tài liệu này bao gồm kiến trúc ứng dụng (Frontend & Backend), kiến trúc dữ liệu và kiến trúc hạ tầng cho phân hệ quản lý học sinh.

### 1.3. Đối tượng độc giả
*   Development Team (Backend Developers, Frontend Developers)
*   DevOps Engineers
*   Quality Assurance (QA) Team
*   Project Managers (PM)
*   Other Solution Architects

## 2. Kiến trúc Tổng quan

### 2.1. Tầm nhìn Kiến trúc
Kiến trúc của phân hệ quản lý học sinh sẽ được thiết kế theo hướng hiện đại, phân lớp, có khả năng mở rộng, bảo trì cao và hiệu suất tốt. Sử dụng Clean Architecture cho Backend giúp tách biệt rõ ràng các mối quan tâm, dễ dàng kiểm thử và thay đổi. Frontend được xây dựng với React và Mantine UI để đảm bảo trải nghiệm người dùng tốt và phát triển nhanh chóng.

### 2.2. Sơ đồ Kiến trúc Tổng quan

```mermaid
graph TD
    A[Người dùng - Giáo vụ] -->|Truy cập qua Web Browser| B(Frontend - React/Mantine UI)
    B -->|API Requests (HTTP/HTTPS)| C(Backend - .NET 10 API)
    C -->|CRUD Operations| D(Database - PostgreSQL)

    subgraph Hệ thống ONENET
        B
        C
        D
    end
```

**Mô tả:**
*   **Frontend:** Giao diện người dùng được phát triển bằng React với thư viện Mantine UI, chạy trên trình duyệt của người dùng.
*   **Backend:** API được phát triển bằng .NET 10 theo Clean Architecture, xử lý logic nghiệp vụ và tương tác với cơ sở dữ liệu.
*   **Database:** PostgreSQL là cơ sở dữ liệu quan hệ được sử dụng để lưu trữ tất cả thông tin học sinh.

### 2.3. Các lựa chọn Công nghệ (Tech Stack)
Tuân thủ các quy định công nghệ bắt buộc:
*   **Frontend Framework:** React
*   **Frontend UI Library:** Mantine UI
*   **Backend Framework:** .NET 10
*   **Backend Architecture:** Clean Architecture
*   **Database:** PostgreSQL

## 3. Kiến trúc Chi tiết

### 3.1. Kiến trúc Ứng dụng (Application Architecture)

#### 3.1.1. Frontend (React với Mantine UI)
*   **Cấu trúc dự án:** Sẽ theo cấu trúc chuẩn của ứng dụng React, có thể sử dụng Vite hoặc Create React App.
    *   `src/components`: Các thành phần UI tái sử dụng (ví dụ: StudentTable, StudentForm).
    *   `src/pages`: Các trang chính của ứng dụng (ví dụ: StudentListPage, StudentDetailPage, AddStudentPage).
    *   `src/services`: Logic gọi API Backend.
    *   `src/hooks`: Custom hooks cho logic tái sử dụng.
    *   `src/context` hoặc `src/store`: Quản lý trạng thái (ví dụ: React Context API, Redux Toolkit, Zustand).
*   **Routing:** Sử dụng React Router để điều hướng giữa các trang.
*   **State Management:** Tùy chọn, có thể sử dụng React Context API hoặc các thư viện như Zustand/Redux Toolkit cho trạng thái phức tạp hơn.
*   **UI Components:** Toàn bộ giao diện người dùng sẽ được xây dựng bằng các components từ Mantine UI, đảm bảo tính nhất quán và tối ưu UX.
*   **API Interaction:** Sử dụng `fetch` API hoặc thư viện như `Axios` để gửi các yêu cầu HTTP/HTTPS tới Backend API.
*   **Validation:** Thực hiện kiểm tra định dạng dữ liệu phía client-side để cung cấp phản hồi tức thì cho người dùng, sử dụng các thư viện như `react-hook-form` kết hợp với `Zod` để tối ưu trải nghiệm người dùng và giảm tải cho backend.

#### 3.1.2. Backend (.NET 10 - Clean Architecture)
Cấu trúc dự án sẽ tuân thủ Clean Architecture, chia thành các layer logic:

*   **`ONENET.Application` (Application Layer):**
    *   Chứa các **Use Cases** (ví dụ: `CreateStudentCommand`, `UpdateStudentCommand`, `DeleteStudentCommand`, `GetStudentListQuery`, `GetStudentByIdQuery`) và **Handlers** (sử dụng MediatR).
    *   Định nghĩa các **Interfaces** cho Persistence (`IStudentRepository`) và External Services.
    *   Chứa logic nghiệp vụ cụ thể cho từng Use Case.
    *   Thực hiện các validation nghiệp vụ (ví dụ: `MaHocSinh` duy nhất).
*   **`ONENET.Domain` (Domain Layer):**
    *   Chứa các **Entities** (ví dụ: `Student` - ánh xạ tới `HocSinh` trong CSDL).
    *   Định nghĩa **Value Objects** (nếu có, ví dụ: `PhoneNumber`).
    *   Chứa các quy tắc nghiệp vụ cốt lõi (Domain Business Rules).
    *   Hoàn toàn độc lập với các layer khác.
*   **`ONENET.Infrastructure` (Infrastructure Layer):**
    *   Triển khai các Interfaces được định nghĩa trong `Application` và `Domain`.
    *   **Persistence:** `DbContext` (Entity Framework Core) cho PostgreSQL, triển khai `IStudentRepository` để tương tác với cơ sở dữ liệu.
    *   **External Services:** Nếu có các dịch vụ bên ngoài, sẽ triển khai tại đây.
    *   Cấu hình DI, Logging, Authentication/Authorization.
*   **`ONENET.Presentation` (API - Web API Layer):**
    *   Chứa các **Controllers** (ví dụ: `StudentsController`) để nhận các yêu cầu HTTP.
    *   Sử dụng **DTOs** (Data Transfer Objects) để chuyển đổi dữ liệu giữa API và Application layer.
    *   Xử lý ánh xạ các yêu cầu HTTP thành các `Command` hoặc `Query` và gửi đến `Application` layer thông qua MediatR.
    *   Quản lý lỗi và phản hồi HTTP.
    *   Thực hiện validation cơ bản của đầu vào request (Model Validation).

```mermaid
graph TD
    UserRequest[HTTP Request] --> API[ONENET.Presentation (Web API)]
    API -->|DTOs, Commands/Queries| Application[ONENET.Application (Use Cases, Handlers)]
    Application -->|Entities, Domain Services| Domain[ONENET.Domain (Entities, Business Rules)]
    Application -->|Interfaces| Infrastructure[ONENET.Infrastructure (EF Core, Repositories)]
    Infrastructure --> Database[PostgreSQL Database]

    style API fill:#f9f,stroke:#333,stroke-width:2px
    style Application fill:#ccf,stroke:#333,stroke-width:2px
    style Domain fill:#cfc,stroke:#333,stroke-width:2px
    style Infrastructure fill:#ffc,stroke:#333,stroke-width:2px
    style Database fill:#cff,stroke:#333,stroke-width:2px
```

### 3.2. Kiến trúc Dữ liệu (Data Architecture)

#### 3.2.1. Cơ sở dữ liệu (PostgreSQL)
*   **Lựa chọn:** PostgreSQL được chọn vì tính mạnh mẽ, mã nguồn mở, hỗ trợ tốt cho các kiểu dữ liệu phức tạp và hiệu suất cao.
*   **ORM:** Entity Framework Core sẽ được sử dụng để tương tác với PostgreSQL, giúp quản lý schema, migration và các thao tác CRUD hiệu quả.

#### 3.2.2. Schema thiết kế
Dựa trên mô hình dữ liệu logic và yêu cầu nghiệp vụ:

**Table: `Students`** (Tên bảng thường là số nhiều trong tiếng Anh)

| Column Name              | Data Type                     | Constraints                                     | Description                                    |
| :----------------------- | :---------------------------- | :---------------------------------------------- | :--------------------------------------------- |
| `Id`                     | `UUID` (GUID)                 | `PRIMARY KEY`                                   | Khóa chính duy nhất cho bảng                  |
| `StudentCode`            | `VARCHAR(20)`                 | `UNIQUE`, `NOT NULL`                            | Mã học sinh duy nhất (ví dụ: HS2024001)       |
| `FullName`               | `VARCHAR(100)`                | `NOT NULL`                                      | Họ và tên đầy đủ                               |
| `DateOfBirth`            | `DATE`                        | `NOT NULL`                                      | Ngày tháng năm sinh                            |
| `Gender`                 | `VARCHAR(10)`                 | `NOT NULL`                                      | Giới tính (Nam, Nữ, Khác)                      |
| `Address`                | `VARCHAR(255)`                | `NULLABLE`                                      | Địa chỉ liên hệ                                |
| `ParentPhoneNumber`      | `VARCHAR(15)`                 | `NULLABLE`                                      | Số điện thoại phụ huynh                        |
| `ClassName`              | `VARCHAR(20)`                 | `NOT NULL`                                      | Lớp học hiện tại                               |
| `CreatedAt`              | `TIMESTAMP WITH TIME ZONE`    | `NOT NULL`, `DEFAULT NOW()`                     | Thời điểm tạo bản ghi                          |
| `CreatedBy`              | `VARCHAR(50)`                 | `NOT NULL`                                      | Người tạo bản ghi                              |
| `UpdatedAt`              | `TIMESTAMP WITH TIME ZONE`    | `NULLABLE`                                      | Thời điểm cập nhật cuối cùng                   |
| `UpdatedBy`              | `VARCHAR(50)`                 | `NULLABLE`                                      | Người cập nhật cuối cùng                       |

*   **Tên bảng/cột:** Sử dụng quy ước đặt tên Snake Case hoặc Pascal Case consistent cho PostgreSQL. (Ví dụ: `student_code` hoặc `StudentCode`). Với EF Core, Pascal Case thường được dùng trong code và được chuyển đổi sang Snake Case khi tạo bảng trong PostgreSQL.
*   **Ràng buộc `UNIQUE` cho `StudentCode`:** Đảm bảo BR-HS-001 được tuân thủ.
*   **`DateOfBirth` Validation:** Sẽ được kiểm tra ở Application Layer (và client-side) để đảm bảo không phải là ngày trong tương lai.
*   **Audit Fields:** `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy` là các trường tiêu chuẩn để theo dõi lịch sử thay đổi của dữ liệu.

### 3.3. Kiến trúc Hạ tầng (Infrastructure Architecture)
*   **Môi trường:**
    *   **Development:** Docker Compose cho phép các nhà phát triển chạy cục bộ môi trường ứng dụng (Backend API, PostgreSQL) một cách dễ dàng và nhất quán.
    *   **Staging/Production:** Sử dụng Kubernetes (K8s) để triển khai và quản lý các container của Backend API và Frontend (có thể thông qua Nginx/Ingress Controller). K8s cung cấp khả năng tự động cân bằng tải, mở rộng và tự phục hồi.
*   **Deployment:**
    *   CI/CD Pipelines (ví dụ: Azure DevOps Pipelines, GitHub Actions) để tự động hóa quá trình build, test và deploy.
    *   Docker Images cho Backend và Frontend.
*   **Networking:**
    *   API Gateway: Có thể được sử dụng để định tuyến các yêu cầu đến Backend API, cung cấp các tính năng như authentication, rate limiting, logging tập trung.
    *   Load Balancers: Để phân phối lưu lượng truy cập đến các instance của Backend API, đảm bảo tính sẵn sàng và hiệu suất.
    *   HTTPS: Tất cả giao tiếp client-server phải được mã hóa bằng HTTPS.
*   **Monitoring & Logging:**
    *   Logging: Sử dụng Serilog cho Backend để ghi log vào các hệ thống tập trung như ELK Stack (Elasticsearch, Logstash, Kibana) hoặc Prometheus/Grafana.
    *   Monitoring: Prometheus để thu thập metrics và Grafana để visualize.
    *   Alerting: Cấu hình cảnh báo cho các sự cố hoặc ngưỡng hiệu năng.
*   **Backup & Recovery:** Thiết lập cơ chế sao lưu định kỳ cho cơ sở dữ liệu PostgreSQL và kế hoạch phục hồi thảm họa.

## 4. Các quyết định thiết kế chính

*   **Clean Architecture cho Backend:** Được chọn để đảm bảo tính module hóa, khả năng kiểm thử cao, dễ dàng bảo trì và mở rộng trong tương lai. Nó cũng giúp tách biệt logic nghiệp vụ khỏi các chi tiết công nghệ.
*   **Sử dụng MediatR:** Giúp triển khai các Command/Query Pattern và Request/Response Pattern một cách rõ ràng, giúp quản lý các Use Case trong Application Layer hiệu quả hơn.
*   **Entity Framework Core:** Là ORM mạnh mẽ và được hỗ trợ tốt cho .NET, giúp tương tác với PostgreSQL một cách hiệu quả và tự động hóa nhiều tác vụ liên quan đến CSDL.
*   **Mantine UI:** Cung cấp bộ component UI phong phú, giúp đẩy nhanh quá trình phát triển Frontend và đảm bảo tính nhất quán về giao diện người dùng.
*   **UUID cho PK (`Id`) và `StudentCode` là `UNIQUE`:** Tách biệt khóa chính kỹ thuật với mã nghiệp vụ giúp linh hoạt hơn trong quản lý dữ liệu và duy trì tính toàn vẹn nghiệp vụ.

## 5. Các vấn đề cần xem xét thêm

*   **Xử lý lỗi toàn cục (Global Error Handling):** Cần triển khai một cơ chế xử lý lỗi nhất quán trên cả Frontend và Backend để cung cấp phản hồi rõ ràng cho người dùng.
*   **Phân quyền chi tiết (Authorization):** Mặc dù BRD chỉ nhắc đến "Giáo vụ" và "Quản trị viên", cần xác định rõ các quyền cụ thể (ví dụ: Giáo vụ chỉ quản lý học sinh của mình, Quản trị viên quản lý tất cả).
*   **Tích hợp với các phân hệ khác của ONENET:** BR-HS-005 đã đề cập đến tính toàn vẹn dữ liệu khi xóa. Cần có một phân tích chi tiết hơn về các mối quan hệ nếu có các phân hệ như "Quản lý Điểm", "Quản lý Chuyên cần" để quyết định cách xử lý khi xóa học sinh (xóa mềm, cảnh báo, ngăn chặn xóa).
*   **Đa ngôn ngữ:** Nếu ONENET có yêu cầu hỗ trợ nhiều ngôn ngữ, cần xem xét kiến trúc i18n/l10n cho cả Frontend và Backend.
*   **Cache:** Đối với các danh sách hoặc dữ liệu ít thay đổi nhưng được truy cập thường xuyên, việc triển khai cơ chế caching (ví dụ: Redis) có thể cải thiện hiệu suất.

---