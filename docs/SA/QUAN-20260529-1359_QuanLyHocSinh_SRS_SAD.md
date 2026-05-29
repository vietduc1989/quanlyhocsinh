# Software Requirements Specification (SRS)
## Tính năng: QuanLyHocSinh

**Phiên bản:** v1.0.0  
**Ngày:** 2026-05-29  
**Chuẩn tài liệu:** ONENET Software Specification Standard (IEEE Std 830-1998 compliant)

---

# 1. Introduction

## 1.1 Purpose
Tài liệu này đặc tả các yêu cầu phần mềm (SRS) và thiết kế kiến trúc hệ thống (SAD) cho tính năng **QuanLyHocSinh** (mã tính năng: `QUAN-20260529-1359`) thuộc dự án `quanlyhocsinh`. Tài liệu hướng đến việc xây dựng hệ thống quản lý học sinh toàn diện, tích hợp module AI hỗ trợ tự động hóa nhập liệu và đánh giá học tập, đồng thời thiết kế giải pháp kỹ thuật kiên cố để xử lý triệt để lỗi giới hạn băng thông và quá tải hệ thống từ các dịch vụ AI bên ngoài (lỗi HTTP Status Code 429 - Too Many Requests) được ghi nhận trong BRD PR#15.

## 1.2 Scope
Phạm vi hệ thống bao gồm:
- **Quản lý thông tin học sinh**: Thực hiện các tác vụ CRUD (Thêm, Xóa, Sửa, Xem) hồ sơ học sinh, lớp học và kết quả học tập.
- **Tích hợp Module AI**: Hỗ trợ phân tích học bạ, nhận diện hồ sơ tự động từ tệp PDF/ảnh thông qua mô hình AI.
- **Xử lý sự cố Rate Limit (429)**: Triển khai Middleware, Message Queue và cơ chế Retry với Exponential Backoff để đảm bảo hệ thống tự phục hồi khi các AI Model API bị giới hạn tốc độ.

## 1.3 Intended Audience
Tài liệu này được biên soạn cho các đối tượng sau:
- **Đội ngũ Phát triển (Backend & Frontend Developers)**: Để hiện thực hóa các chức năng và kiến trúc kỹ thuật.
- **Đội ngũ Kiểm thử (QA/QC)**: Để làm căn cứ thiết kế Test Cases và thực hiện kiểm thử tự động, kiểm thử chịu tải.
- **Quản trị viên hệ thống (DevOps)**: Để thiết lập hạ tầng, cấu hình Rate Limiting và hệ thống giám sát.
- **Product Owner / Business Analyst (BA)**: Để đối chiếu và nghiệm thu sản phẩm cuối cùng.

---

# 2. Overall Description

## 2.1 Product Perspective
Hệ thống `quanlyhocsinh` được xây dựng theo kiến trúc Monolithic vững chắc, triển khai trên nền tảng .NET 10 kết hợp với cơ sở dữ liệu PostgreSQL mạnh mẽ. Trong hệ thống này, phân hệ `QuanLyHocSinh` đóng vai trò là hạt nhân trung tâm, kết nối trực tiếp với các module quản lý giáo viên, lớp học và phân hệ phân tích dữ liệu AI hỗ trợ giảng dạy. Hệ thống giao tiếp với các mô hình AI bên ngoài qua các RESTful API và có cơ chế hàng đợi xử lý bất đồng bộ để chống lỗi nghẽn mạng (Rate Limit 429).

## 2.2 User Roles
Hệ thống phân quyền cho 3 nhóm người dùng chính tương tác với tính năng này:
1. **Quản trị viên (Admin)**: Có toàn quyền cấu hình hệ thống, quản lý tài khoản, thiết lập ngưỡng Rate Limit và cấu hình khóa API kết nối dịch vụ AI.
2. **Giáo vụ (Registrar)**: Thực hiện quản lý hồ sơ học sinh, thực hiện import hồ sơ hàng loạt và sử dụng tính năng quét AI tự động nhập liệu.
3. **Giáo viên (Teacher)**: Tra cứu danh sách học sinh, cập nhật điểm số, nhận xét học sinh thông qua gợi ý của trợ lý AI.

## 2.3 Technology Stack

### Backend
- **.NET 10**: Phiên bản LTS mới nhất của Microsoft, đảm bảo hiệu năng cao và bảo mật tối đa.
- **ASP.NET Web API**: Xây dựng hệ thống RESTful API chuẩn hóa, bảo mật bằng JWT.
- **MVC Pattern**: Tổ chức mã nguồn phân tách rõ ràng giữa Model, View và Controller.
- **Entity Framework Core (ORM)**: Truy xuất và ánh xạ cơ sở dữ liệu PostgreSQL một cách an toàn, tối ưu hiệu năng truy vấn thông qua LINQ.
- **Polly library**: Sử dụng để triển khai các mẫu thiết kế phục hồi hệ thống như Retry, Circuit Breaker cho lỗi 429.

### Frontend
- **ReactJS**: Thư viện UI xây dựng giao diện người dùng SPA (Single Page Application) mượt mà và tối ưu hóa hiệu suất render.
- **Mantine UI**: Thư viện React component hiện đại, cung cấp bộ UI elements đồng bộ, tương thích tốt với nhiều thiết bị.

### Database
- **PostgreSQL**: Hệ quản trị cơ sở dữ liệu quan hệ nguồn mở mạnh mẽ, hỗ trợ lưu trữ dữ liệu JSONB phục vụ các kết quả phân tích phức tạp từ AI.

### Architecture
- **Monolithic Architecture**: Kiến trúc nguyên khối tinh gọn, giảm thiểu độ trễ mạng nội bộ, dễ dàng phân phối và quản lý mã nguồn tập trung.
- **RESTful API**: Chuẩn giao tiếp không trạng thái (stateless), định dạng dữ liệu JSON.

---

# 3. Functional Requirements

### QUAN-20260529-1359-SRS01: Quản lý Hồ sơ Học sinh (CRUD)
- **Mô tả**: Cho phép nhân viên giáo vụ thực hiện thêm mới, cập nhật, xóa và tìm kiếm hồ sơ cá nhân học sinh.
- **Luồng xử lý chính**:
  1. Người dùng vào màn hình "Danh sách học sinh", hệ thống gửi yêu cầu API `GET /api/v1/students` hiển thị dữ liệu dạng bảng.
  2. Để thêm mới, chọn "Thêm học sinh", nhập các thông tin bắt buộc (Họ tên, Ngày sinh, Lớp học, Email phụ huynh).
  3. Hệ thống kiểm tra tính hợp lệ dữ liệu bằng Fluent Validation ở Backend.
  4. Lưu dữ liệu trực tiếp vào database PostgreSQL thông qua EF Core.
- **Luồng ngoại lệ**:
  - `QUAN-20260529-1359-SRS01.E1`: Trùng mã học sinh -> Hệ thống báo lỗi 409 Conflict.
  - `QUAN-20260529-1359-SRS01.E2`: Dữ liệu đầu vào không hợp lệ -> Hệ thống báo lỗi 400 Bad Request kèm chi tiết lỗi của từng trường.

### QUAN-20260529-1359-SRS02: Quản lý Phân lớp và Chuyển lớp
- **Mô tả**: Giáo vụ thực hiện xếp lớp học sinh vào đầu năm học hoặc chuyển lớp giữa kỳ.
- **Luồng xử lý chính**:
  1. Giáo vụ chọn học sinh từ danh sách, nhấn "Phân lớp".
  2. Hệ thống hiển thị danh sách lớp học còn chỗ trống dựa trên sĩ số tối đa quy định.
  3. Giáo vụ xác nhận chuyển lớp, hệ thống thực hiện cập nhật khóa ngoại `ClassId` của bảng `Students` trong một Database Transaction duy nhất để đảm bảo tính nhất quán của dữ liệu.

### QUAN-20260529-1359-SRS03: Nhập liệu hồ sơ tự động qua AI và cơ chế hàng đợi xử lý bất đồng bộ
- **Mô tả**: Giáo vụ tải lên tệp hồ sơ gốc (PDF hoặc ảnh scan), hệ thống gửi yêu cầu phân tích thông tin đến mô hình AI để tự động điền các thông tin của học sinh vào biểu mẫu.
- **Luồng xử lý chính**:
  1. Người dùng tải tệp tin lên thông qua giao diện kéo thả của Mantine UI.
  2. Backend nhận file và đẩy một tác vụ xử lý (Background Job) vào hàng đợi nội bộ sử dụng `Hangfire` hoặc `Channel` của .NET.
  3. Hệ thống trả về mã tác vụ (Job ID) ngay lập tức cho Frontend với mã trạng thái `202 Accepted`.
  4. Ở tiến trình chạy ngầm (Background Worker), hệ thống thực hiện gọi API của AI Model để nhận diện văn bản (OCR) và bóc tách dữ liệu JSON.
  5. Sau khi hoàn thành, trạng thái tác vụ được cập nhật vào DB, Frontend sử dụng kết nối SignalR hoặc cơ chế Polling để cập nhật kết quả tự động lên màn hình của giáo vụ.

### QUAN-20260529-1359-SRS04: Xử lý ngoại lệ giới hạn tốc độ AI (Rate Limit 429 Exception Handler)
- **Mô tả**: Xử lý kịch bản lỗi khi dịch vụ AI phản hồi mã lỗi `429 (Too Many Requests)` nhằm chống sập hệ thống và mang lại trải nghiệm mượt mà cho người dùng.
- **Luồng xử lý chính**:
  1. Khi tiến trình ngầm (Background Worker) gọi API AI và nhận về mã trạng thái `429 (Too Many Requests)` kèm theo HTTP Header `Retry-After`.
  2. **Bộ lọc lỗi tự động (Polly Middleware)** bắt được ngoại lệ này.
  3. Áp dụng giải pháp **Retry Policy với Exponential Backoff & Jitter**:
     - *Lần thử 1*: Đợi `Retry-After` (giây) hoặc mặc định 2 giây.
     - *Lần thử 2*: Đợi `4 giây + Jitter ngẫu nhiên`.
     - *Lần thử 3*: Đợi `8 giây + Jitter ngẫu nhiên`.
  4. Nếu sau 3 lần thử lại vẫn thất bại, kích hoạt **Circuit Breaker** để chuyển trạng thái tác vụ xử lý AI sang hàng đợi "Chờ xử lý lại" (DLQ - Dead Letter Queue).
  5. Hệ thống gửi thông báo lên giao diện Frontend thông qua Notification Toast của Mantine UI: *"Hệ thống AI hiện tại đang bận xử lý. Yêu cầu nhập liệu của bạn đã được đưa vào hàng đợi thông minh và sẽ tự động hoàn thành trong vòng ít phút nữa."* để người dùng không phải thao tác lại từ đầu.

---

# 4. External Interface Requirements

## 4.1 User Interface
- Giao diện được thiết kế Responsive tương thích 100% trên cả Desktop (độ phân giải từ 1024px trở lên) và Mobile (từ 375px trở lên) bằng Mantine UI.
- Màn hình chính bao gồm:
  - **Dashboard**: Biểu đồ sĩ số, phân phối điểm bằng Mantine Charts.
  - **Bảng danh sách học sinh**: Hỗ trợ phân trang, tìm kiếm thời gian thực (real-time search with debounce 300ms) và bộ lọc đa tiêu chí (Lớp, Niên khóa, Học lực).
  - **Màn hình Upload hồ sơ AI**: Hộp kéo thả file (Dropzone) hiển thị tiến trình (Progress Bar) trực quan. Khi xảy ra lỗi 429, hệ thống hiển thị cảnh báo dạng Alert màu vàng cam lịch sự với nút "Thử lại thủ công" hoặc cho phép chuyển sang nhập bằng tay nếu cần gấp.

## 4.2 API Interface
Hệ thống cung cấp các Endpoint chuẩn RESTful:

| Method | Endpoint | Description | Payload mẫu | Phản hồi lỗi 429 |
| :--- | :--- | :--- | :--- | :--- |
| **POST** | `/api/v1/students` | Tạo mới hồ sơ học sinh | `{"fullName": "Nguyen Van A", "dob": "2010-05-15", "classId": 12}` | `400 Bad Request`, `409 Conflict` |
| **GET** | `/api/v1/students/{id}`| Lấy chi tiết học sinh | N/A | `404 Not Found` |
| **POST** | `/api/v1/students/import-ai`| Gửi yêu cầu OCR học bạ bằng AI | FormData (Multipart file upload) | `202 Accepted` (Hệ thống tiếp nhận vào hàng đợi xử lý bất đồng bộ) |

**Chi tiết xử lý phản hồi API 429 từ AI Third-Party:**
Khi AI Model API trả về:
```http
HTTP/1.1 429 Too Many Requests
Retry-After: 30
Content-Type: application/json

{
  "error": "Rate limit exceeded. Please try again later.",
  "code": 429
}
```
Hệ thống Backend của ONENET sẽ chuyển đổi nội dung này thành một payload thông báo thân thiện dạng JSON trước khi gửi lại cho Client nếu là cuộc gọi đồng bộ:
```json
{
  "success": false,
  "code": "AI_LIMIT_EXCEEDED",
  "message": "Hệ thống AI đang quá tải. Tiến trình của bạn đã được chuyển sang chế độ xếp hàng tự động.",
  "retryAfterSeconds": 30
}
```

## 4.3 Database Interface
Hệ thống sử dụng EF Core để tương tác với PostgreSQL. Các kết nối cơ sở dữ liệu được tối ưu bằng việc sử dụng Connection Pooling thông qua cấu hình `AddDbContextPool` trong file `Program.cs`. 

Sơ đồ thực thể cơ bản gồm:
- **Table `Students`**: Lưu thông tin học sinh (`Id`, `FullName`, `Dob`, `Email`, `ClassId`, `CreatedAt`, `UpdatedAt`).
- **Table `Classes`**: Lưu thông tin lớp học (`Id`, `ClassName`, `Grade`, `MaxStudents`).
- **Table `AIProcessingQueue`**: Lưu vết trạng thái xử lý AI (`Id`, `StudentId`, `Status` [Pending, Processing, Completed, Failed], `RetryCount`, `ErrorMessage`).

---

# 5. Non-functional Requirements

## Performance
- Thời gian phản hồi của các API thông thường (CRUD) phải nhỏ hơn 200ms với mức tải 500 CCU (Concurrent Users).
- Thời gian tìm kiếm và lọc danh sách học sinh trên cơ sở dữ liệu có quy mô 100,000 dòng ghi nhận không được vượt quá 500ms (sử dụng tối ưu Indexes trên PostgreSQL).

## Security
- Mã hóa toàn bộ mật khẩu người dùng bằng giải pháp băm BCrypt.
- Giao thức truyền tải bắt buộc sử dụng HTTPS (TLS 1.3).
- Toàn bộ các API đều được bảo vệ bởi JSON Web Token (JWT) có thời hạn hiệu lực ngắn (15 phút) đi kèm cơ chế Refresh Token lưu giữ an toàn trong HTTP-Only Cookie chống các cuộc tấn công XSS và CSRF.

## Availability
- Uptime >= 99.9%
- Hệ thống hỗ trợ tính năng Active-Passive Clustering cho cơ sở dữ liệu để tự động chuyển vùng dữ liệu (failover) khi gặp sự cố phần cứng.

## Scalability
- Horizontal scaling supported: Do backend thiết kế theo mô hình RESTful API không trạng thái (Stateless), ứng dụng có thể nhân bản lên nhiều thực thể (Containers) sau một Load Balancer (Nginx/HAProxy) mà không ảnh hưởng tới logic nghiệp vụ.

## Maintainability
- Modular source code: Mã nguồn tuân thủ nguyên lý thiết kế Clean Architecture, phân tách module quản lý học sinh tách biệt hoàn toàn với module thông báo và thanh toán.
- ORM data access: Sử dụng Entity Framework Core phiên bản tối ưu, nghiêm cấm viết SQL thuần không an toàn trừ trường hợp tối ưu hóa truy vấn đặc biệt đã thông qua phê duyệt của Solution Architect.
- Reusable UI components: Các thành phần giao diện như Bảng (Table), Hộp thoại (Modal), Thông báo lỗi (Alerts) phải được đóng gói thành các component React dùng chung trong toàn bộ dự án `quanlyhocsinh`.

---

# 6. Data Requirements

Dữ liệu hệ thống bắt buộc phải thỏa mãn các miền ràng buộc dữ liệu toàn vẹn sau:
- **Học sinh (Student)**:
  - `FullName`: Kiểu chuỗi ký tự unicode, không chứa ký tự đặc biệt hoặc số, độ dài từ 2 đến 100 ký tự.
  - `Dob` (Ngày sinh): Phải nhỏ hơn ngày hiện tại và tuổi học sinh phải thuộc giới hạn từ 6 đến 18 tuổi tại thời điểm nhập học.
  - `Email`: Định dạng chuẩn RFC 5322.
- **Dữ liệu Hàng đợi AI (AIProcessingQueue)**:
  - `RetryCount` mặc định khởi tạo là `0` và giới hạn tối đa là `3`.
  - `Payload` lưu dưới định dạng cột `JSONB` trong PostgreSQL để lưu vết cấu trúc dữ liệu không đồng nhất trả về từ các tệp OCR khác nhau.

---

# 7. Business Rules

1. **Quy tắc tuổi nhập học**: Chỉ những học sinh đạt chuẩn độ tuổi từ 6 đến 18 tuổi mới được phép tạo mới hồ sơ trên hệ thống.
2. **Quy tắc sĩ số**: Không được phân học sinh vào lớp học đã vượt quá sĩ số tối đa cấu hình trong hệ thống (mặc định là 40 học sinh/lớp).
3. **Quy tắc xử lý Rate Limit 429**:
   - Khi hệ thống nhận về lỗi 429 từ nhà cung cấp dịch vụ AI, hệ thống không được dừng ứng dụng đột ngột.
   - Luôn phải ưu tiên đưa nhiệm vụ đó vào hàng đợi ngầm để giải phóng luồng chính của giáo vụ, cho phép họ tiếp tục thao tác với các hồ sơ khác.
   - Thời gian chờ thử lại tối đa cho một tiến trình nền không vượt quá 120 giây. Quá thời gian này, tác vụ sẽ chuyển sang trạng thái "Failed - Cần kiểm tra thủ công" và thông báo cho người dùng qua thanh thông báo hệ thống.

---

# 8. Acceptance Criteria

### QUAN-20260529-1359-AC01: Kiểm tra CRUD hồ sơ học sinh
- **Điều kiện đầu vào**: Người dùng đã đăng nhập tài khoản Giáo vụ hợp lệ.
- **Kịch bản kiểm thử**:
  - Tạo mới học sinh với dữ liệu hợp lệ đầy đủ -> Tạo thành công, xuất hiện bản ghi mới trong DB, trả về HTTP 201 Created.
  - Sửa thông tin ngày sinh sai định dạng (ví dụ nhập năm sinh 2050) -> Hệ thống báo lỗi Validation, không lưu vào DB, trả về HTTP 400 Bad Request.

### QUAN-20260529-1359-AC02: Kiểm tra xử lý lỗi Rate Limit 429 khi gọi API AI
- **Điều kiện đầu vào**: Tải lên 10 file học bạ cùng lúc để kích hoạt giới hạn tốc độ (Rate Limit) giả lập của mô hình AI.
- **Kịch bản kiểm thử**:
  - Khi API của bên thứ 3 trả về mã lỗi 429.
  - Hệ thống tự động bắt mã lỗi này và kích hoạt Polly Policy để đợi 30 giây (hoặc theo Header `Retry-After`) trước khi tự động gọi lại.
  - Trong quá trình hệ thống đang thử lại dưới nền, màn hình giao diện không bị đóng băng, người dùng vẫn có thể thực hiện thao tác duyệt danh sách hoặc chỉnh sửa thông tin học sinh khác.
  - Sau khi kết thúc xử lý ngầm thành công, dữ liệu học sinh được tự động cập nhật lên màn hình của người dùng qua thông báo Notification Toast thời gian thực mà không cần tải lại trang.

---

# 9. Assumptions & Dependencies

- **Giả định (Assumptions)**:
  - Giả định rằng máy chủ cơ sở dữ liệu PostgreSQL luôn hoạt động ổn định và đường truyền internet từ hệ thống ONENET đến các API dịch vụ AI đạt độ trễ thấp dưới 150ms.
- **Phụ thuộc (Dependencies)**:
  - Phụ thuộc hoàn toàn vào API của mô hình AI bên thứ ba. Nếu API của dịch vụ AI này thay đổi cấu trúc dữ liệu trả về hoặc sập hoàn toàn (Lỗi 500, 503), tính năng quét tự động bằng AI sẽ bị tạm dừng hoạt động, nhưng hệ thống vẫn đảm bảo các tác vụ CRUD thủ công hoạt động độc lập bình thường.

---

# 10. Appendix

- **Từ viết tắt**:
  - **CRUD**: Create, Read, Update, Delete (Thêm, Đọc, Sửa, Xóa).
  - **ORM**: Object-Relational Mapping (Ánh xạ quan hệ đối tượng).
  - **API**: Application Programming Interface (Giao diện lập trình ứng dụng).
  - **DLQ**: Dead Letter Queue (Hàng đợi chứa các tác vụ hỏng).
  - **JWT**: JSON Web Token.
- **Tài liệu tham khảo**:
  - Quy chuẩn thiết kế giao diện UI/UX của ONENET 2026.
  - Đặc tả kỹ thuật Polly .NET Resilience Framework.