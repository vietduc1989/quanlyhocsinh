Chào bạn, tôi là **Solution Architect của ONENET**. Dưới đây là bộ tài liệu kỹ thuật hoàn chỉnh bao gồm **Tài liệu Đặc tả Yêu cầu Phần mềm (SRS - Software Requirements Specification)** và **Tài liệu Thiết kế Kiến trúc Phần mềm (SAD - Software Architecture Document)** được thiết kế tối ưu cho Phân hệ Quản lý Hồ sơ Học sinh thuộc Hệ thống ONENET-SMS, dựa trên tài liệu BRD v1.0 đã được cung cấp.

---

# PHẦN 1: SOFTWARE REQUIREMENTS SPECIFICATION (SRS)
## Hệ thống Quản lý Học sinh ONENET-SMS | Phân hệ: Quản lý Hồ sơ Học sinh

### 1. Giới thiệu (Introduction)
#### 1.1 Mục đích
Tài liệu SRS này mô tả chi tiết các yêu cầu chức năng, phi chức năng, nghiệp vụ và giao diện kỹ thuật cho tính năng **Đăng ký Học sinh mới** thuộc Phân hệ Quản lý Hồ sơ Học sinh (ONENET-SMS). Đối tượng sử dụng tài liệu này bao gồm Đội ngũ Phát triển Phần mềm (Developers), Đội ngũ Kiểm thử (QA/QC), Đội ngũ Triển khai và các Bên liên quan tại ONENET.

#### 1.2 Thuật ngữ và Viết tắt
*   **ONENET-SMS**: Student Management System - Hệ thống Quản lý Trường học ONENET.
*   **Student ID (Mã học sinh)**: Mã định danh duy nhất của học sinh trên hệ thống.
*   **RBAC**: Role-Based Access Control (Kiểm soát truy cập dựa trên vai trò).
*   **SĐT**: Số điện thoại.
*   **CSDL / DB**: Cơ sở dữ liệu / Database.

---

### 2. Mô tả Tổng quan (Overall Description)
#### 2.1 Kiến trúc Tổng quan Chức năng
Tính năng "Đăng ký Học sinh mới" là cổng tiếp nhận dữ liệu đầu vào quan trọng nhất của phân hệ Hồ sơ. Nó tương tác trực tiếp với CSDL trung tâm và kích hoạt các luồng xử lý phi đồng bộ (Asynchronous) sang các phân hệ Tài chính và các dịch vụ truyền thông (SMS/Email).

#### 2.2 Đặc tính Người dùng (User Personas)
*   **Cán bộ Tuyển sinh**: Kỹ năng tin học văn phòng cơ bản, thao tác nhanh, yêu cầu giao diện rõ ràng, hỗ trợ phím tắt và tự động hóa tối đa (ví dụ: tự động viết hoa tên, tự động điền địa chỉ).
*   **Quản trị viên Học vụ**: Thành thạo hệ thống, có quyền sửa đổi dữ liệu sai lệch, cấu hình sĩ số lớp học và phê duyệt danh sách lớp.
*   **Ban Giám Hiệu**: Chỉ đọc báo cáo, theo dõi tiến độ tuyển sinh qua Dashboard.

#### 2.3 Giới hạn và Ràng buộc thiết kế
*   Hệ thống chạy trên nền tảng Web-based, hỗ trợ tốt nhất trên Chrome, Edge, Safari phiên bản mới nhất.
*   Không thiết kế giao diện mobile cho chức năng nhập liệu này (do đặc thù nghiệp vụ nhập liệu màn hình lớn tại văn phòng tuyển sinh).

---

### 3. Yêu cầu Chức năng chi tiết (Functional Requirements)

#### FR-01: Nhập Thông tin Cá nhân Học sinh
*   **Mô tả**: Cho phép nhập liệu toàn bộ thông tin cá nhân bắt buộc và tải lên ảnh chân dung của học sinh.
*   **Chi tiết trường dữ liệu & Validation**:
    *   *Họ và tên*: Chuỗi chữ, tự động viết hoa toàn bộ chữ cái (ví dụ: "nguyễn văn a" -> "NGUYỄN VĂN A"). Bắt buộc.
    *   *Ngày sinh*: Date picker. Bắt buộc. Phải thỏa mãn quy tắc giới hạn độ tuổi theo Khối (Xem BR-01).
    *   *Giới tính*: Radio button (Nam / Nữ / Khác). Bắt buộc.
    *   *Dân tộc*, *Quốc tịch*: Thả chọn (Dropdown). Mặc định là "Kinh" và "Việt Nam". Bắt buộc.
    *   *Địa chỉ thường trú & Tạm trú*: Cho phép chọn Tỉnh/Thành phố -> Quận/Huyện -> Phường/Xã từ dropdown liên kết cấp bậc. Có checkbox "Địa chỉ tạm trú giống địa chỉ thường trú" để tự động sao chép thông tin.
    *   *Ảnh chân dung*: Kéo thả hoặc chọn file. Định dạng `.jpg`, `.png`, dung lượng tối đa 2MB. Tự động crop ảnh về tỷ lệ 3:4.

#### FR-02: Nhập Thông tin Phụ huynh / Người giám hộ
*   **Mô tả**: Tiếp nhận thông tin của tối thiểu 01 và tối đa 03 người giám hộ trực tiếp.
*   **Chi tiết trường dữ liệu**:
    *   *Mối quan hệ*: Dropdown (Cha, Mẹ, Người giám hộ khác).
    *   *Họ và tên*: Chữ viết hoa. Bắt buộc.
    *   *Số điện thoại*: Chuỗi số, bắt buộc đúng 10 chữ số, đầu số Việt Nam hợp lệ (09, 08, 03, 05, 07). Bắt buộc cho người giám hộ chính. Kiểm tra quy tắc BR-04.
    *   *Email*: Định dạng email chuẩn (`^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$`).
    *   *Nghề nghiệp*: Trường nhập liệu tự do (Tùy chọn).

#### FR-03: Phân lớp học trực tiếp
*   **Mô tả**: Lựa chọn lớp học thực tế cho học sinh ngay khi làm thủ tục.
*   **Luồng xử lý**:
    1.  Người dùng chọn **Năm học** (Mặc định năm hiện tại).
    2.  Hệ thống hiển thị danh sách **Khối** tương thích với độ tuổi của học sinh vừa nhập.
    3.  Người dùng chọn **Khối** -> Hệ thống tải danh sách **Lớp học** thuộc Khối đó.
    4.  Mỗi lớp hiển thị kèm chỉ số sĩ số thực tế dưới dạng: `Tên Lớp (Sĩ số hiện tại / Sĩ số tối đa)` (Ví dụ: `1A1 (38/40)`).
    5.  Nếu lớp đã đầy (38/38 hoặc 40/40), hệ thống sẽ disable dòng lớp đó hoặc hiển thị cảnh báo đỏ và không cho phép chọn (Tuân thủ BR-02).

#### FR-04: Quản lý Hồ sơ Đính kèm (Tài liệu scan)
*   **Mô tả**: Cho phép upload đính kèm các tài liệu pháp lý phục vụ lưu trữ số.
*   **Chi tiết**:
    *   Các danh mục tài liệu gồm: Giấy khai sinh (Bắt buộc), Học bạ cấp dưới (Tùy chọn), Giấy chứng nhận ưu tiên (Tùy chọn).
    *   Định dạng hỗ trợ: `PDF`, `JPG`, `JPEG`, `PNG`.
    *   Kích thước file tối đa: 5MB/file.
    *   Tự động quét virus trước khi lưu trữ vào Object Storage.

#### FR-05: Tự động Sinh Mã học sinh (Student ID)
*   **Mô tả**: Sau khi bấm "Lưu", hệ thống tự động sinh Mã học sinh duy nhất.
*   **Công thức**: `HS` + `YY` (2 chữ số cuối năm tuyển sinh hiện tại) + `XXXXX` (5 chữ số tự tăng bắt đầu từ 00001).
    *   *Ví dụ*: Năm tuyển sinh là 2023, học sinh đầu tiên sẽ là `HS2300001`.
*   **Ràng buộc kỹ thuật**: Cơ chế sinh mã phải bảo đảm không bị trùng lặp trong môi trường phân tán (sử dụng Database Sequence hoặc Redis Distributed Lock).

#### FR-06: Validate Dữ liệu thời gian thực (Real-time Validation)
*   **Mô tả**: Đưa ra cảnh báo lỗi ngay tại trường thông tin mà không cần đợi bấm nút "Lưu".
*   **Các kịch bản kiểm tra**:
    *   *Tuổi học sinh*: Nếu học sinh đăng ký Khối 1 nhưng năm sinh không thỏa mãn BR-01, hệ thống hiển thị thông báo lỗi màu đỏ ngay dưới ô Ngày sinh: `"Học sinh đăng ký Khối 1 phải đủ 6 tuổi tính đến năm tuyển sinh [Năm]"`.
    *   *Định dạng SĐT*: Hiện lỗi nếu nhập chữ hoặc thiếu/thừa số.
    *   *Kiểm tra trùng lặp SĐT phụ huynh (BR-04)*: Nếu SĐT đã đăng ký cho 3 học sinh khác nhau trong hệ thống, yêu cầu hiển thị cảnh báo và yêu cầu tích chọn "Xác nhận diện Anh/Chị/Em ruột" để tiếp tục.

#### FR-07: Xem trước và In Phiếu đăng ký
*   **Mô tả**: Tạo file biểu mẫu in ấn nhanh sau khi hồ sơ được lưu.
*   **Yêu cầu**: Hiển thị popup chứa bản xem trước (Preview) định dạng PDF đúng quy chuẩn Bộ GD&ĐT bao gồm đầy đủ thông tin vừa nhập, Mã học sinh vừa sinh, và vị trí chữ ký của Phụ huynh và Người tiếp nhận. Hỗ trợ nút "In nhanh" kết nối máy in nội bộ hoặc "Tải xuống PDF".

---

### 4. Yêu cầu Phi chức năng (Non-functional Requirements)

#### 4.1 Hiệu năng (Performance)
*   Thời gian xử lý API lưu hồ sơ và sinh mã: $\le 1.5\text{ giây}$ (điều kiện kết nối mạng tiêu chuẩn).
*   Giao diện nhập liệu render hoàn tất dưới $1\text{ giây}$.
*   Hỗ trợ tối thiểu 200 CCU (Concurrent Users) thực hiện nhấn "Lưu" đồng thời vào giờ cao điểm mà không tăng thời gian phản hồi quá $3\text{ giây}$.

#### 4.2 Bảo mật (Security)
*   **Phân quyền (RBAC)**:
    *   `Admissions_Officer` (Cán bộ tuyển sinh): Có quyền CREATE, READ, UPDATE hồ sơ do mình tạo hoặc được phân công. Không được DELETE.
    *   `Academic_Administrator` (Quản trị học vụ): Có toàn quyền CRUD, có quyền cấu hình lại lớp học, ghi đè sĩ số tối đa.
    *   `School_Board` (Ban giám hiệu): Chỉ có quyền READ toàn bộ danh sách và báo cáo biểu đồ.
*   **Mã hóa dữ liệu nhạy cảm**: Số điện thoại, Email, Địa chỉ chi tiết và số CCCD (nếu có) của phụ huynh và học sinh phải được mã hóa bằng thuật toán đối xứng **AES-256** trước khi lưu trữ vào Database.
*   **An toàn API**: Mọi API truyền nhận dữ liệu phải sử dụng giao thức HTTPS (TLS 1.3), xác thực qua JWT Token nằm trong Header của mỗi Request.

#### 4.3 Độ sẵn sàng & Khôi phục (Availability & Disaster Recovery)
*   Hệ thống cam kết độ sẵn sàng uptime $99.9\%$.
*   Dữ liệu được backup tự động hàng ngày lúc 23h00 (Daily Incremental Backup) và backup toàn phần vào Chủ nhật hàng tuần (Weekly Full Backup). Bản backup được lưu trữ tại một phân vùng vật lý độc lập (Off-site storage).

---

# PHẦN 2: SOFTWARE ARCHITECTURE DOCUMENT (SAD)
## Hệ thống Quản lý Học sinh ONENET-SMS | Phân hệ: Quản lý Hồ sơ Học sinh

### 1. Giới thiệu & Mục tiêu Kiến trúc
Tài liệu SAD này phác thảo thiết kế kiến trúc hệ thống phục vụ tính năng **Đăng ký Học sinh mới**. Mục tiêu kiến trúc là xây dựng một hệ thống có tính chịu tải cao, bảo mật thông tin tuyệt đối, kiến trúc mô-đun hóa dễ bảo trì, dễ tích hợp với các phân hệ khác (Tài chính, Thông báo) thông qua cơ chế hướng sự kiện (Event-Driven Architecture).

---

### 2. Kiến trúc Công nghệ (Technology Stack)

| Thành phần | Công nghệ lựa chọn | Lý do lựa chọn |
| :--- | :--- | :--- |
| **Frontend Framework** | React.js 18 (TypeScript) + TailwindCSS | Tối ưu hóa render giao diện, cơ chế Virtual DOM giúp form nhập liệu lớn mượt mà, cấu trúc Component dễ tái sử dụng. |
| **Backend Framework** | Java Spring Boot 3.2.x / JDK 17 | Đảm bảo tính ổn định cao trong xử lý giao dịch tài chính/học vụ, khả năng quản lý luồng tốt, thư viện Spring Security hỗ trợ bảo mật mạnh mẽ. |
| **Database** | PostgreSQL 15 | Cơ sở dữ liệu quan hệ mạnh mẽ, hỗ trợ lưu trữ JSONB tốt (lưu lịch sử log), hỗ trợ ACID hoàn hảo bảo đảm tính toàn vẹn dữ liệu sĩ số lớp học. |
| **In-Memory Cache** | Redis | Sử dụng để quản lý Session, lưu trữ tạm thời sĩ số thực tế của lớp học nhằm tránh nghẽn luồng đọc ghi DB khi có hàng trăm yêu cầu đăng ký cùng lúc. |
| **Message Broker** | RabbitMQ | Xử lý hàng đợi tin nhắn phi đồng bộ để tích hợp gửi SMS/Email và đồng bộ dữ liệu sang Phân hệ Tài chính. |
| **File Storage** | MinIO (On-Premises S3-compatible) | Lưu trữ các tệp scan đính kèm an toàn, bảo mật cao, dễ cấu hình trong hạ tầng nội bộ của trường học. |

---

### 3. Thiết kế Cơ sở Dữ liệu (Database Schema)

Dưới đây là sơ đồ thực thể mối quan hệ dữ liệu cốt lõi (Entity Relationship Diagram - ERD) thiết kế riêng cho phân hệ này:

```
+----------------------------------------------------+       +--------------------------------------------------+
|                  STUDENTS                          |       |                  GUARDIANS                       |
+----------------------------------------------------+       +--------------------------------------------------+
| PK | id                 | UUID                     |       | PK | id               | UUID                     |
| UK | student_code       | VARCHAR(10)              |       |    | full_name        | VARCHAR(100)             |
|    | first_name         | VARCHAR(50)              |       | UK | phone            | VARCHAR(15) (Encrypted)  |
|    | last_name          | VARCHAR(50)              |       |    | email            | VARCHAR(100) (Encrypted) |
|    | date_of_birth      | DATE                     |       |    | occupation       | VARCHAR(100)             |
|    | gender             | VARCHAR(10)              |       +--------------------------------------------------+
|    | nationality        | VARCHAR(50)              |                                | 1
|    | ethnic             | VARCHAR(50)              |                                |
|    | permanent_address  | TEXT                     |                                |
|    | temporary_address  | TEXT                     |                                | 1..N
|    | avatar_url         | VARCHAR(255)             |       +--------------------------------------------------+
|    | status             | VARCHAR(20)              |       |             STUDENT_GUARDIANS                    |
|    | created_at         | TIMESTAMP                |       +--------------------------------------------------+
|    | updated_at         | TIMESTAMP                |       | FK | student_id       | UUID                     |
+----------------------------------------------------+       | FK | guardian_id      | UUID                     |
                         | 1                                 |    | relationship     | VARCHAR(20)              |
                         |                                   |    | is_primary       | BOOLEAN                  |
                         | 1..N                              +--------------------------------------------------+
+----------------------------------------------------+                                | N
|          STUDENT_CLASS_ENROLLMENTS                 |                                |
+----------------------------------------------------+                                |
| PK | id                 | UUID                     |                                |
| FK | student_id         | UUID                     |<--------------------------------+
| FK | class_id           | UUID                     |
|    | school_year        | VARCHAR(9)               |
|    | enrollment_date    | DATE                     |
|    | status             | VARCHAR(20)              |
+----------------------------------------------------+
                         | N
                         |
                         | 1
+----------------------------------------------------+
|                   CLASSES                          |
+----------------------------------------------------+
| PK | id                 | UUID                     |
|    | class_name         | VARCHAR(20)              |
|    | grade_level        | INT                      |
|    | max_capacity       | INT                      |
|    | current_capacity   | INT                      |
|    | version            | INT (Optimistic Lock)    |
+----------------------------------------------------+
```

---

### 4. Biểu đồ Sequence xử lý Nghiệp vụ (Sequence Diagram)
Dưới đây là quy trình xử lý của hệ thống khi Cán bộ tuyển sinh thực hiện hành động **"Lưu hồ sơ Đăng ký"**:

```
[Admissions_Officer]      [Frontend (React)]     [Backend API (Spring Boot)]   [Redis Cache]   [PostgreSQL DB]   [RabbitMQ Broker]
        |                        |                           |               |                |                  |
        |--- 1. Click "Lưu" ---->|                           |               |                |                  |
        |                        |--- 2. Validate Client --->|               |                |                  |
        |                        |       (Format SĐT, Email) |               |                |                  |
        |                        |                           |               |                |                  |
        |                        |------ 3. POST /student -->|               |                |                  |
        |                        |                           |-- 4. Check -->|                |                  |
        |                        |                           |   Class Cap   |                |                  |
        |                        |                           |<-- 5. OK -----|                |                  |
        |                        |                           |                                |                  |
        |                        |                           |------------ 6. Start Transaction ------------->|
        |                        |                           |                                |                  |
        |                        |                           |-- 7. Lock Class (version) ---->|                  |
        |                        |                           |-- 8. Write Student & Guardian->|                  |
        |                        |                           |-- 9. Gen Student Code -------->|                  |
        |                        |                           |-- 10. Update class capacity -->|                  |
        |                        |                           |                                |                  |
        |                        |                           |<----------- 11. Commit Transaction ------------|
        |                        |                           |                                |                  |
        |                        |                           |-- 12. Update Cache (Capacity)->|                  |
        |                        |                           |                                |                  |
        |                        |                           |------------- 13. Publish Event (Asyn) ----------->|
        |                        |                           |                                |                  | [Publish to queues:
        |                        |                           |                                |                  |  - sync-billing-queue
        |                        |                           |                                |                  |  - send-sms-welcome-queue]
        |                        |<-- 14. Response 201 ------|                                |                  |
        |                        |   (Created + StudentCode) |                                |                  |
        |<-- 15. Show Success ---|                           |                                |                  |
        |    & Open Print Preview|                           |                                |                  |
```

---

### 5. Giải pháp Kiến trúc cho các Ràng buộc & Quy tắc Nghiệp vụ

#### 5.1 Xử lý Đồng thời và Chống Vượt Sĩ số lớp học (BR-02)
*   **Vấn đề**: Khi lớp học chỉ còn 1 chỉ tiêu trống, nhưng có 2 Cán bộ tuyển sinh cùng bấm nút "Lưu" vào lớp đó tại cùng một thời điểm Miligiây.
*   **Giải pháp**: Sử dụng cơ chế **Optimistic Locking (Khóa lạc quan)** trên bảng `CLASSES` thông qua trường `version`.
    *   Mỗi khi có cập nhật sĩ số, câu lệnh SQL dạng: `UPDATE classes SET current_capacity = current_capacity + 1, version = version + 1 WHERE id = :classId AND version = :currentVersion AND current_capacity < max_capacity;`
    *   Nếu một giao dịch thực hiện trước thành công, trường `version` tăng lên. Giao dịch thứ hai chạy sau sẽ không khớp `version` nữa, DB sẽ throw `OptimisticLockingFailureException`. Hệ thống sẽ bắt lỗi này và phản hồi lỗi "Lớp đã đạt sĩ số tối đa" về giao diện người dùng để chọn lớp khác, đảm bảo tính nhất quán tuyệt đối của dữ liệu.

#### 5.2 Cơ chế sinh Mã học sinh Duy nhất không Trùng lặp (BR-03 / FR-05)
*   **Vấn đề**: Trong hệ thống phân tán đa máy chủ, nếu lấy mã lớn nhất trong DB rồi cộng 1, hai máy chủ có thể sinh trùng một mã.
*   **Giải pháp**: Sử dụng **Database Sequence** chuyên dụng được khởi tạo trong PostgreSQL cho từng năm học.
    *   Tạo sequence: `CREATE SEQUENCE IF NOT EXISTS student_id_seq_2023 START WITH 1;`
    *   Khi có yêu cầu, Backend chạy truy vấn: `SELECT nextval('student_id_seq_2023')` để lấy số tự tăng một cách thread-safe. Tiếp đó format chuỗi ở Java: `String.format("HS23%05d", sequenceValue)` đảm bảo tốc độ sinh cực nhanh và không bao giờ trùng lặp.

#### 5.3 Kiến trúc Đồng bộ tích hợp phi đồng bộ (Integration Architecture)
Để giảm tải cho tiến trình đăng ký chính (Main Thread), việc đồng bộ dữ liệu sang Phân hệ Tài chính và gửi tin nhắn được xử lý bằng **Kiến trúc hướng sự kiện (Event-Driven)**:
1.  Ngay sau khi giao dịch Lưu học sinh thành công (Commit DB), Spring Boot sẽ phát đi một Event tên là `StudentRegisteredEvent` vào RabbitMQ Exchange.
2.  **Phân hệ Tài chính (Billing Module)** sẽ lắng nghe từ queue `billing-sync-queue`: Nhận thông tin Học sinh mới -> Kiểm tra Khối/Lớp -> Tự động sinh danh mục hóa đơn học phí tương ứng trong DB Tài chính.
3.  **Hệ thống Truyền thông (Notification Module)** lắng nghe từ queue `sms-notification-queue`: Gọi API của SMS Gateway hoặc SMTP Server để bắn tin nhắn/email cảm ơn đến phụ huynh.

---

### 6. Thiết kế Module Ghi nhật ký hệ thống (Audit Trail)
Để phục vụ việc bảo mật dữ liệu và đối soát (theo mục 9 của BRD), hệ thống sử dụng một Spring Boot Aspect (`@Aspect`) để chặn các hành động thay đổi dữ liệu (AOP - Aspect Oriented Programming).

#### Cấu trúc bảng Lưu trữ Log (`audit_logs`):
*   `id` (UUID - Khóa chính)
*   `username` (VARCHAR - Tài khoản thực hiện)
*   `ip_address` (VARCHAR - IP nguồn phát sinh yêu cầu)
*   `action` (VARCHAR - `CREATE`, `UPDATE`, `DELETE`)
*   `table_name` (VARCHAR - Tên bảng bị tác động, ví dụ: `students`)
*   `record_id` (UUID - ID bản ghi bị tác động)
*   `old_values` (JSONB - Lưu toàn bộ giá trị cũ trước khi sửa đổi)
*   `new_values` (JSONB - Lưu toàn bộ giá trị mới sau khi sửa đổi)
*   `timestamp` (TIMESTAMP WITH TIME ZONE - Thời gian chính xác)

#### Ví dụ về log ghi nhận khi Cập nhật Số điện thoại Phụ huynh:
*   `action`: `"UPDATE"`
*   `old_values`: `{"phone": "0912345678", "occupation": "Kỹ sư"}`
*   `new_values`: `{"phone": "0988888888", "occupation": "Kỹ sư"}`

---

### 7. Thiết kế Giao diện Người dùng mẫu (Form UI Draft)
Dưới đây là sơ đồ bố cục giao diện nhập liệu trực quan được thiết kế tối ưu cho Cán bộ Tuyển sinh thao tác nhanh:

```
+---------------------------------------------------------------------------------------------------------+
|  ONENET-SMS - ĐĂNG KÝ HỌC SINH MỚI                                                                      |
+---------------------------------------------------------------------------------------------------------+
| [ BƯỚC 1: THÔNG TIN HỌC SINH ]                                                                           |
|  Họ và Tên (*):    [ NGUYỄN VĂN AN                  ] (Tự động chuyển viết hoa)                         |
|  Ngày sinh (*):    [ 15/05/2017 ] [Calendar]  Giới tính (*): (o) Nam  ( ) Nữ  ( ) Khác                  |
|  Dân tộc:          [ Kinh             ][v]    Quốc tịch:     [ Việt Nam         ][v]                    |
|  Thường trú (*):   [ Tỉnh/Thành ][v] -> [ Quận/Huyện ][v] -> [ Phường/Xã ][v]                            |
|                    [ Số nhà 12, Đường Lê Lợi...                                                      ]  |
|  Tạm trú:          [*] Giống địa chỉ thường trú                                                        |
|  Ảnh chân dung (*):[ [Kéo thả ảnh tại đây hoặc Duyệt file] - Hỗ trợ JPG, PNG < 2MB ]                     |
+---------------------------------------------------------------------------------------------------------+
| [ BƯỚC 2: THÔNG TIN PHỤ HUYNH ]                                                                         |
|  Mối quan hệ (*):  [ Cha    ][v]              Họ và tên (*): [ NGUYỄN VĂN BÌNH                       ]  |
|  Số điện thoại (*):[ 0912345678 ]             Email:         [ binhnv@gmail.com                      ]  |
|  Nghề nghiệp:      [ Kinh doanh               ]                                                         |
|  [+] Thêm thông tin người bảo hộ (Mẹ/Người giám hộ khác)                                                |
+---------------------------------------------------------------------------------------------------------+
| [ BƯỚC 3: PHÂN LỚP HỌC TRỰC TIẾP ]                                                                      |
|  Năm học:          [ 2023-2024  ][v]          Khối học:      [ Khối 1 ][v]                              |
|  Lớp học đăng ký:  [ Lớp 1A1 (38/40)   ][v] (Hệ thống kiểm tra sĩ số thời gian thực)                      |
+---------------------------------------------------------------------------------------------------------+
| [ BƯỚC 4: HỒ SƠ ĐÍNH KÈM ]                                                                              |
|  - Giấy khai sinh (*):  [ Chọn file PDF, JPG... ]                                                       |
|  - Giấy tờ ưu tiên:     [ Chọn file...          ]                                                       |
+---------------------------------------------------------------------------------------------------------+
|                                                      [ HỦY ĐĂNG KÝ ]     [ LƯU HỒ SƠ & IN PHIẾU ]       |
+---------------------------------------------------------------------------------------------------------+
```

---

Trên đây là toàn bộ tài liệu **SRS** và **SAD** hoàn chỉnh dành cho tính năng Đăng ký Học sinh mới của ONENET-SMS. Tài liệu này đã sẵn sàng để chuyển giao cho các đội ngũ phát triển, kiểm thử và thiết kế giao diện triển khai thực tế.