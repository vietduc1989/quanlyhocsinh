# Software Requirements Specification (SRS)
## Tính năng: Quản lý học sinh

**Phiên bản:** v1.0  
**Ngày:** 2026-05-29  
**Chuẩn tài liệu:** IEEE Std 830-1998 / ONENET Solution Architecture Standard  

---

# 1. Introduction

## 1.1 Purpose
Tài liệu Đặc tả Yêu cầu Phần mềm (SRS) và Thiết kế Kiến trúc (SAD) này được xây dựng bởi Solution Architect của ONENET nhằm mục đích mô tả chi tiết các yêu cầu chức năng (Functional), phi chức năng (Non-functional), các quy tắc nghiệp vụ (Business Rules), thiết kế cơ sở dữ liệu, giao diện API và kiến trúc hệ thống cho phân hệ **Quản lý học sinh** thuộc dự án **quanlyhocsinh** (Mã tính năng: `QUAN-20260529-0943`).

Tài liệu này đóng vai trò là kim chỉ nam kỹ thuật cho đội ngũ phát triển (Developers), đội ngũ kiểm thử (QA/QC), quản lý dự án (PM) và các bên liên quan tại ONENET để hiện thực hóa sản phẩm một cách đồng nhất, chính xác và bảo mật.

## 1.2 Scope
### 1.2.1 Phạm vi áp dụng (In Scope)
*   **Quản lý thông tin hồ sơ học sinh (CRUD):** 
    *   Hỗ trợ tạo mới, chỉnh sửa thông tin cá nhân của học sinh.
    *   Thực hiện cơ chế xóa mềm (Soft Delete) bảo toàn lịch sử dữ liệu.
*   **Hiển thị và phân trang:** Hiển thị danh sách học sinh theo dạng bảng dữ liệu lưới, phân trang chuẩn 20 bản ghi/trang.
*   **Tìm kiếm & Lọc:** Tìm kiếm nhanh linh hoạt theo họ tên (tìm kiếm gần đúng) và mã học sinh (tìm kiếm chính xác).
*   **Nhập/Xuất dữ liệu hàng loạt (Bulk Processing):**
    *   Import danh sách học sinh từ file Excel mẫu `.xlsx` đi kèm cơ chế kiểm tra lỗi trực tiếp trên từng dòng và trả về file báo cáo lỗi chi tiết.
    *   Export danh sách học sinh hiện tại hoặc kết quả tìm kiếm ra file Excel `.xlsx`.
*   **Cơ chế định danh tự động:** Hệ thống tự động sinh Mã học sinh duy nhất theo định dạng quy chuẩn bảo đảm không trùng lặp 100%.
*   **Nhật ký vận hành & Bảo mật:** Kiểm soát quyền hạn truy cập (RBAC) và ghi nhận Audit Trail chi tiết cho mọi hành động thay đổi dữ liệu (C, U, D, Import, Export).

### 1.2.2 Ngoài phạm vi áp dụng (Out of Scope)
*   Nghiệp vụ quản lý lớp học, sơ đồ lớp học, phân chia giáo viên chủ nhiệm, chuyển lớp/chuyển trường.
*   Nghiệp vụ quản lý học bạ điện tử, bảng điểm, xếp loại học lực, hạnh kiểm và rèn luyện.
*   Nghiệp vụ kế toán, quản lý học phí, thu chi, hóa đơn và tích hợp cổng thanh toán.
*   Cổng thông tin tương tác phụ huynh - học sinh (Portal/Mobile App).

## 1.3 Intended Audience
*   **Ban giám hiệu / Khách hàng:** Đánh giá tính phù hợp của giải pháp đối với nhu cầu thực tế.
*   **Project Manager / Scrum Master:** Lập kế hoạch, theo dõi tiến độ phát triển và bàn giao.
*   **Backend & Frontend Developers:** Hiểu rõ cấu trúc dữ liệu, luồng nghiệp vụ và đặc tả API để lập trình.
*   **QA / QC Engineers:** Xây dựng Test Plan, Test Cases, thực hiện kiểm thử tự động (Automation Test) và kiểm thử thủ công (Manual Test).
*   **DevOps / System Admin:** Triển khai hạ tầng, thiết lập CI/CD, giám sát hệ thống và phân quyền truy cập.

---

# 2. Overall Description

## 2.1 Product Perspective
Phân hệ **Quản lý học sinh** được thiết kế như một module lõi nằm trong hệ sinh thái quản lý giáo dục tổng thể của ONENET. Module này đóng vai trò cung cấp dữ liệu định danh học sinh gốc (Master Data) cho các phân hệ tương lai (Điểm số, Điểm danh, Học phí) thông qua hệ thống RESTful API kết nối nội bộ an toàn.

```
       +-------------------------------------------------------+
       |                  ONENET Ecosystem                     |
       |                                                       |
       |  +------------------+           +------------------+  |
       |  |  Phân hệ Học phí |           | Phân hệ Điểm số  |  |
       |  +--------+---------+           +--------+---------+  |
       |           |                              |            |
       |           |       [RESTful API JSON]     |            |
       |           +--------------+---------------+            |
       |                          |                            |
       |                          v                            |
       |         +----------------------------------+          |
       |         | QUAN-20260529-0943: QL Học sinh  |          |
       |         +----------------------------------+          |
       +-------------------------------------------------------+
```

## 2.2 User Roles
Hệ thống phân quyền chi tiết dựa trên vai trò (RBAC) như sau:

| Vai trò người dùng | Quyền hạn & Thao tác được phép |
| :--- | :--- |
| **Giáo vụ / Nhân viên tuyển sinh** | Toàn quyền thao tác trên phân hệ: Xem, Thêm mới, Sửa, Xóa mềm, Import Excel, Export Excel, Tải file mẫu. |
| **Ban giám hiệu / Admin hệ thống** | Quyền xem danh sách, tìm kiếm học sinh, xuất báo cáo Excel, cấu hình hệ thống và truy cập màn hình Nhật ký hệ thống (Audit Trail). Không trực tiếp thực hiện CRUD thông thường. |
| **Giáo viên chủ nhiệm** | Chỉ có quyền Xem danh sách và Tìm kiếm thông tin học sinh thuộc khối/lớp được chỉ định phân quyền. Không có quyền sửa đổi dữ liệu, import hoặc xem nhật ký hệ thống toàn cục. |

## 2.3 Technology Stack

### Backend
- **Framework:** .NET 10 (C# 14)
- **Web Framework:** ASP.NET Core Web API
- **Design Pattern:** MVC Pattern kết hợp CQRS (Command Query Responsibility Segregation) nội bộ để tối ưu hóa hiệu năng đọc/ghi dữ liệu.
- **Data Access:** Entity Framework Core (ORM) áp dụng kỹ thuật Code First, Migration tự động.

### Frontend
- **Framework:** ReactJS (v18+) sử dụng TypeScript.
- **UI Component Library:** Mantine UI (v7+) hỗ trợ tối ưu hóa khả năng tùy chỉnh giao diện và responsive tốt trên mọi thiết bị.
- **State Management:** React Query (TanStack Query) cho việc caching và quản lý dữ liệu bất đồng bộ từ API.

### Database
- **Engine:** PostgreSQL 16
- **Kỹ thuật tối ưu:** B-Tree Index trên các cột tìm kiếm thường xuyên (`student_code`, `full_name`), JSONB cho cột chứa vết thay đổi dữ liệu trong bảng log (`old_values`, `new_values`).

### Architecture
- **Kiến trúc:** Monolithic Architecture (Kiến trúc nguyên khối được Module hóa rõ ràng về mặt Logical NameSpaces).
- **Giao thức kết nối:** RESTful API chuẩn hóa dữ liệu đầu ra dưới dạng JSON.
- **Mã hóa truyền tải:** HTTPS TLS 1.3 bảo mật kênh truyền.

---

# 3. Functional Requirements

Dưới đây là bảng đặc tả các yêu cầu chức năng của hệ thống Quản lý học sinh:

| ID chức năng | Tên chức năng | Mô tả chi tiết yêu cầu kỹ thuật | Trạng thái |
| :--- | :--- | :--- | :--- |
| **QUAN-20260529-0943-SRS01** | Thêm mới học sinh trực tiếp | Cho phép người dùng (Giáo vụ) nhập trực tiếp thông tin học sinh qua giao diện Form. Hệ thống validate dữ liệu theo thời gian thực (Real-time). Khi lưu thành công, hệ thống tự động sinh Mã học sinh duy nhất theo định dạng `HS-YYYYMMDD-XXXX` và lưu vết Audit Log. | Bắt buộc |
| **QUAN-20260529-0943-SRS02** | Cập nhật hồ sơ học sinh | Cho phép chỉnh sửa thông tin học sinh hiện tại. Trường "Mã học sinh" (`student_code`) ở trạng thái Read-only, tuyệt đối không được sửa đổi. Mọi thay đổi phải ghi nhận lại dữ liệu cũ và dữ liệu mới vào bảng Audit Trail. | Bắt buộc |
| **QUAN-20260529-0943-SRS03** | Xóa mềm học sinh | Chức năng xóa học sinh tại giao diện danh sách. Hệ thống hiển thị Popup xác nhận. Khi đồng ý, hệ thống cập nhật trường `is_deleted = true`, ẩn học sinh khỏi giao diện hiển thị thông thường nhưng giữ nguyên dữ liệu trong Database. | Bắt buộc |
| **QUAN-20260529-0943-SRS04** | Tìm kiếm học sinh | Cung cấp ô tìm kiếm đa năng: tìm kiếm gần đúng không phân biệt hoa thường theo `full_name` và tìm kiếm chính xác theo `student_code`. | Bắt buộc |
| **QUAN-20260529-0943-SRS05** | Phân trang danh sách | Áp dụng phân trang cứng 20 dòng trên mỗi trang ở phía Server (Server-side pagination) để tối ưu hóa hiệu năng truy vấn dữ liệu lớn. | Bắt buộc |
| **QUAN-20260529-0943-SRS06** | Import danh sách từ Excel | Cho phép tải lên file Excel `.xlsx`. Hệ thống kiểm tra hợp lệ dữ liệu của từng dòng. Nếu hợp lệ, lưu vào DB và sinh mã tự động. Nếu có lỗi, rollback toàn bộ giao dịch và trả về file Excel chứa cột chỉ rõ nguyên nhân lỗi ở dòng nào. | Bắt buộc |
| **QUAN-20260529-0943-SRS07** | Export danh sách ra Excel | Cho phép xuất dữ liệu danh sách học sinh hiện hành ra file Excel `.xlsx` dựa theo bộ lọc và từ khóa tìm kiếm hiện tại của người dùng. | Bắt buộc |
| **QUAN-20260529-0943-SRS08** | Ghi nhật ký hệ thống (Audit Trail) | Tự động ghi nhận log cho tất cả các hành động CREATE, UPDATE, DELETE, IMPORT, EXPORT của người dùng vào cơ sở dữ liệu để phục vụ đối soát an ninh. | Bắt buộc |

---

# 4. External Interface Requirements

## 4.1 User Interface
Giao diện người dùng được thiết kế dựa trên thư viện **Mantine UI**, đảm bảo tính trực quan, nhất quán và thân thiện trên cả màn hình Desktop và Tablet.

### 4.1.1 Màn hình Danh sách học sinh (Main Dashboard Grid)
*   **Thành phần phía trên:** Thanh tiêu đề "Quản lý Hồ sơ Học sinh", nút "Thêm mới học sinh" (nổi bật), nút "Import Excel", nút "Export Excel" và liên kết "Tải file mẫu Excel".
*   **Thành phần bộ lọc:** Thanh tìm kiếm đa năng (Search Input) hỗ trợ nhập Họ tên hoặc Mã học sinh kèm nút "Tìm kiếm" và "Làm mới".
*   **Bảng hiển thị (Grid/Table):** Gồm các cột: `STT`, `Mã học sinh`, `Họ và tên`, `Ngày sinh`, `Giới tính`, `SĐT Phụ huynh`, `Email`, `Hành động` (Sửa, Xóa).
*   **Phân trang:** Nằm dưới cùng góc phải của bảng, hiển thị rõ số trang hiện tại, nút điều hướng qua lại `|<`, `<`, `>`, `>|`.

### 4.1.2 Form Thêm mới / Cập nhật học sinh (Modal Form)
*   Sử dụng Mantine Modal nằm giữa màn hình.
*   Các trường thông tin được nhóm logic:
    *   *Thông tin cá nhân:* Họ và tên (Mantine TextInput), Ngày sinh (Mantine DateInput - giới hạn tối đa là ngày hiện tại), Giới tính (Mantine RadioGroup hoặc Select: Nam, Nữ, Khác).
    *   *Thông tin liên hệ:* Địa chỉ (Mantine Textarea), SĐT Phụ huynh (Mantine TextInput), Email (Mantine TextInput).
*   Chỉ thị trường bắt buộc bằng dấu sao đỏ `*`.
*   Nút hành động: "Lưu thông tin" (Màu xanh thương hiệu ONENET) và "Hủy bỏ" (Màu xám).

---

## 4.2 API Interface

Tất cả các API đều yêu cầu Header:
*   `Authorization: Bearer <JWT_Token>`
*   `Content-Type: application/json`

### 4.2.1 [POST] Thêm mới học sinh
*   **Endpoint:** `/api/v1/students`
*   **Request Body:**
```json
{
  "fullName": "Nguyễn Văn A",
  "dateOfBirth": "2018-05-15",
  "gender": "Nam",
  "address": "Số 12 Chùa Bộc, Đống Đa, Hà Nội",
  "parentPhone": "0987654321",
  "email": "nguyenvana@gmail.com"
}
```
*   **Response (201 Created):**
```json
{
  "success": true,
  "message": "Thêm mới học sinh thành công.",
  "data": {
    "studentId": "d3b07384-d113-49cd-a5d6-8cf924971c22",
    "studentCode": "HS-20260529-0001",
    "fullName": "Nguyễn Văn A",
    "dateOfBirth": "2018-05-15",
    "gender": "Nam",
    "address": "Số 12 Chùa Bộc, Đống Đa, Hà Nội",
    "parentPhone": "0987654321",
    "email": "nguyenvana@gmail.com",
    "createdAt": "2026-05-29T10:15:30.452Z"
  }
}
```

### 4.2.2 [PUT] Chỉnh sửa học sinh
*   **Endpoint:** `/api/v1/students/{id}`
*   **Request Body:**
```json
{
  "fullName": "Nguyễn Văn A",
  "dateOfBirth": "2018-05-15",
  "gender": "Nam",
  "address": "Số 25 Tây Sơn, Đống Đa, Hà Nội",
  "parentPhone": "0987654321",
  "email": "nguyenvana@gmail.com"
}
```
*   **Response (200 OK):**
```json
{
  "success": true,
  "message": "Cập nhật thông tin học sinh thành công.",
  "data": {
    "studentId": "d3b07384-d113-49cd-a5d6-8cf924971c22",
    "studentCode": "HS-20260529-0001",
    "fullName": "Nguyễn Văn A",
    "dateOfBirth": "2018-05-15",
    "gender": "Nam",
    "address": "Số 25 Tây Sơn, Đống Đa, Hà Nội",
    "parentPhone": "0987654321",
    "email": "nguyenvana@gmail.com",
    "updatedAt": "2026-05-29T10:30:12.118Z"
  }
}
```

### 4.2.3 [DELETE] Xóa mềm học sinh
*   **Endpoint:** `/api/v1/students/{id}`
*   **Response (200 OK):**
```json
{
  "success": true,
  "message": "Xóa thông tin học sinh thành công (Soft Delete)."
}
```

### 4.2.4 [GET] Tra cứu danh sách & Tìm kiếm (Có phân trang)
*   **Endpoint:** `/api/v1/students?page=1&pageSize=20&searchQuery=Nguyễn`
*   **Response (200 OK):**
```json
{
  "success": true,
  "data": [
    {
      "studentId": "d3b07384-d113-49cd-a5d6-8cf924971c22",
      "studentCode": "HS-20260529-0001",
      "fullName": "Nguyễn Văn A",
      "dateOfBirth": "2018-05-15",
      "gender": "Nam",
      "parentPhone": "0987654321",
      "email": "nguyenvana@gmail.com"
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalRecords": 154,
    "totalPages": 8
  }
}
```

### 4.2.5 [POST] Import học sinh từ Excel
*   **Endpoint:** `/api/v1/students/import`
*   **Content-Type:** `multipart/form-data`
*   **Payload:** File Excel đính kèm (Key: `file`).
*   **Response thành công (200 OK):**
```json
{
  "success": true,
  "message": "Import thành công 150/150 bản ghi học sinh."
}
```
*   **Response thất bại do có lỗi dữ liệu (400 Bad Request):**
```json
{
  "success": false,
  "message": "Phát hiện dữ liệu không hợp lệ. Vui lòng tải file đính kèm lỗi bên dưới để sửa đổi.",
  "errorReportUrl": "https://s3.onenet.vn/temp-reports/error-import-20260529.xlsx"
}
```

---

## 4.3 Database Interface

Hệ thống sử dụng cơ sở dữ liệu quan hệ PostgreSQL 16. Dưới đây là sơ đồ thực thể vật lý (Physical Schema) cho hai bảng dữ liệu lõi:

### 4.3.1 Bảng `students` (Lưu trữ thông tin học sinh)
```sql
CREATE TABLE students (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    student_code VARCHAR(20) NOT NULL UNIQUE,
    full_name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL,
    gender VARCHAR(10) NOT NULL,
    address VARCHAR(255),
    parent_phone VARCHAR(15) NOT NULL,
    email VARCHAR(100),
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Tạo Index để tăng tốc truy vấn tìm kiếm danh sách và phân trang
CREATE INDEX idx_students_search ON students(full_name, student_code) WHERE is_deleted = FALSE;
```

### 4.3.2 Bảng `audit_logs` (Ghi nhận nhật ký thay đổi hệ thống)
```sql
CREATE TABLE audit_logs (
    log_id VARCHAR(50) PRIMARY KEY,
    username VARCHAR(100) NOT NULL,
    action VARCHAR(20) NOT NULL, -- CREATE, UPDATE, DELETE, IMPORT, EXPORT
    object_affected VARCHAR(50) NOT NULL, -- Mã học sinh bị tác động
    timestamp TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    ip_address VARCHAR(45) NOT NULL,
    old_values JSONB, -- Lưu trữ dữ liệu trước khi thay đổi
    new_values JSONB  -- Lưu trữ dữ liệu sau khi thay đổi
);

-- Tạo Index trên cột timestamp để tăng hiệu năng truy vấn log
CREATE INDEX idx_audit_logs_timestamp ON audit_logs(timestamp DESC);
```

---

# 5. Non-functional Requirements

## Performance
*   **Thời gian phản hồi trang (Page Load & Search Latency):** Thời gian trả về kết quả truy vấn tìm kiếm và phân trang danh sách học sinh trên màn hình UI phải `< 1.0 giây` với quy mô dữ liệu thử nghiệm đạt 100,000 bản ghi (vượt mong đợi BRD là 1.5s).
*   **Thời gian xử lý Import hàng loạt:** Quá trình đọc, xác thực và ghi xuống DB cho file Excel chứa dưới 2,000 dòng dữ liệu phải kết thúc trong vòng `< 5 giây`.

## Security
*   **Giao thức truyền tải:** Toàn bộ kết nối giữa Client và Server bắt buộc mã hóa qua giao thức an toàn **HTTPS (TLS 1.3)**.
*   **Cơ chế xác thực & Phân quyền:** Sử dụng JWT (JSON Web Token) để xác thực người dùng. Phân quyền chặt chẽ theo vai trò (RBAC) ở cả tầng Client (ẩn/hiển thị nút bấm) và tầng Server API (Middleware kiểm tra Token và Quyền trước khi xử lý).
*   **Phòng chống tấn công an ninh mạng:**
    *   Sử dụng EF Core LINQ để ngăn chặn triệt để lỗ hổng **SQL Injection**.
    *   Thực hiện lọc dữ liệu đầu vào (Input Sanitization) để ngăn chặn tấn công **XSS (Cross-Site Scripting)**.

## Availability
- Uptime >= 99.9% (Ngoại trừ lịch bảo trì định kỳ được thông báo trước tối thiểu 24 giờ).
- Hệ thống áp dụng cơ chế tự động khởi động lại container (Auto-restart) thông qua Docker/Kubernetes khi xảy ra lỗi đột ngột.

## Scalability
- Horizontal scaling supported: Do backend được xây dựng hoàn toàn không lưu trạng thái (Stateless API), cho phép dễ dàng nhân bản (Scale-out) thành nhiều thực thể (Instances) chạy song song đằng sau Load Balancer (Nginx/HAProxy) khi tải tăng cao đột biến.

## Maintainability
- Modular source code: Cấu trúc mã nguồn Backend được phân tách rõ ràng theo cấu trúc phân tầng (Clean/Layered Architecture: API, Application, Domain, Infrastructure).
- ORM data access: Toàn bộ quá trình tương tác database được quản trị thông qua Entity Framework Core giúp dễ dàng nâng cấp hệ quản trị cơ sở dữ liệu nếu cần.
- Reusable UI components: Tận dụng cơ chế thiết kế component nguyên tử (Atomic Design) trong ReactJS và Mantine UI giúp tối ưu hóa khả năng tái sử dụng mã nguồn giao diện.

---

# 6. Data Requirements

### 6.1 Data Dictionary & Validation Rules

| Tên trường dữ liệu | Kiểu dữ liệu | Ràng buộc nghiệp vụ | Mô tả & Quy tắc kiểm tra (Validation Rules) |
| :--- | :--- | :--- | :--- |
| **Họ tên** (`full_name`) | `VARCHAR(100)` | Bắt buộc (Not Null) | Không được bỏ trống. Chỉ chấp nhận các chữ cái tiếng Việt, tiếng Anh và khoảng trắng. Không chứa số hoặc ký tự đặc biệt. |
| **Ngày sinh** (`date_of_birth`) | `DATE` | Bắt buộc (Not Null) | Phải nhỏ hơn ngày hiện tại của hệ thống (`date_of_birth < current_date`). |
| **Giới tính** (`gender`) | `VARCHAR(10)` | Bắt buộc (Not Null) | Chỉ chấp nhận các giá trị định nghĩa trước: `Nam`, `Nữ`, `Khác`. |
| **Địa chỉ** (`address`) | `VARCHAR(255)` | Tùy chọn (Nullable) | Cho phép nhập địa chỉ thường trú hoặc tạm trú của học sinh. |
| **SĐT phụ huynh** (`parent_phone`) | `VARCHAR(15)` | Bắt buộc (Not Null) | Bắt buộc 10 chữ số, bắt đầu bằng các đầu số di động hợp lệ tại Việt Nam: `03`, `05`, `07`, `08`, `09`. |
| **Email** (`email`) | `VARCHAR(100)` | Tùy chọn (Nullable) | Nếu nhập, phải tuân thủ nghiêm ngặt chuẩn RFC 5322 (ví dụ: `pattern@domain.com`). |

---

# 7. Business Rules

### BR-01: Quy tắc định dạng Mã học sinh (Unique ID)
*   Hệ thống tự động sinh Mã học sinh duy nhất không trùng lặp cho mỗi học sinh khi lưu hồ sơ thành công theo định dạng cố định: `HS-YYYYMMDD-XXXX`
    *   `HS`: Tiền tố ký hiệu học sinh.
    *   `YYYYMMDD`: Ngày, tháng, năm tạo bản ghi.
    *   `XXXX`: Số thứ tự tự động tăng liên tục từ `0001` đến `9999` trong ngày và tự động reset về `0001` khi bước sang ngày mới.
*   *Giải pháp tránh xung đột đồng thời (Concurrency Handling):* Để đảm bảo tỷ lệ lỗi trùng lặp đạt **0% (Zero Duplication Rate)** khi có nhiều yêu cầu tạo mới đồng thời, hệ thống sử dụng một Transaction cô lập mức cao (Serializable) kết hợp Sequence riêng biệt trong PostgreSQL thiết lập theo chu kỳ ngày.

### BR-02: Quy tắc Xóa mềm (Soft Delete)
*   Khi có yêu cầu xóa, hệ thống chỉ cập nhật cờ dữ liệu `is_deleted = true`.
*   Tất cả các câu lệnh truy vấn hiển thị danh sách, tìm kiếm, export danh sách học sinh mặc định phải thêm điều kiện `is_deleted = false`.
*   Dữ liệu lịch sử, nhật ký thao tác (Audit Log) liên quan tới học sinh bị xóa mềm vẫn được bảo toàn tuyệt đối để đảm bảo tính toàn vẹn thông tin và đối soát lịch sử.

### BR-03: Quy tắc Giao dịch của tiến trình Import Excel
*   Quy trình Import file Excel được thực thi dưới dạng một Database Transaction nguyên tử (All-or-Nothing). 
*   Nếu phát hiện bất kỳ một lỗi logic hoặc lỗi định dạng dữ liệu nào ở bất kỳ dòng nào trong file Excel, toàn bộ phiên làm việc sẽ bị **Rollback** để đảm bảo dữ liệu trong database không bị sai lệch cục bộ. Hệ thống sẽ kết xuất một file báo cáo lỗi chi tiết, phản hồi trực tiếp cho người dùng.

---

# 8. Acceptance Criteria

Hệ thống được coi là hoàn thành và đủ điều kiện bàn giao khi đáp ứng các tiêu chuẩn nghiệm thu sau:

| Mã tiêu chuẩn | Tiêu chí nghiệm thu cụ thể | Phương pháp kiểm chứng |
| :--- | :--- | :--- |
| **AC-01** | Tạo mới thành công học sinh với đầy đủ các trường dữ liệu hợp lệ và tự động sinh mã theo đúng cấu trúc `HS-YYYYMMDD-XXXX`. | Kiểm thử thủ công (Manual Test) + Kiểm tra Database trực tiếp. |
| **AC-02** | Ngăn chặn lưu thành công nếu bỏ trống Họ tên, Ngày sinh, SĐT phụ huynh hoặc nhập sai định dạng SĐT, Email, Ngày sinh tương lai. | Thử nghiệm nhập dữ liệu lỗi trên Form và kiểm tra thông báo lỗi trên UI. |
| **AC-03** | Thực hiện hành động xóa học sinh, kiểm tra trên giao diện không còn hiển thị học sinh đó nhưng trong Database bản ghi vẫn tồn tại và trường `is_deleted` chuyển thành `true`. | Kiểm tra giao diện Danh sách học sinh và truy vấn SQL DB. |
| **AC-04** | Tìm kiếm học sinh trả kết quả đúng theo Họ tên gần đúng hoặc chính xác Mã học sinh. Thời gian trả kết quả tìm kiếm hiển thị trên UI phải dưới 1 giây. | Đo lường hiệu năng bằng Chrome DevTools Lighthouse / Network tab. |
| **AC-05** | Nhập file Excel mẫu chuẩn hóa gồm 2000 dòng dữ liệu hợp lệ. Hệ thống xử lý thành công, không có bản ghi lỗi, thời gian Import dưới 5 giây. | Đo lường hiệu năng xử lý API trên Server với file dữ liệu mẫu thực tế. |
| **AC-06** | Mọi hoạt động thêm, sửa, xóa đều được lưu vết trong bảng `audit_logs` đầy đủ thông tin: User, Action, Object, Timestamp, IP, Old Values, New Values. | Thực hiện thao tác CRUD và kiểm tra bảng `audit_logs` trong Database. |

---

# 9. Assumptions & Dependencies

*   **Giả định (Assumptions):**
    *   Hệ thống máy chủ File Server hoặc dịch vụ AWS S3 lưu trữ nội bộ của ONENET hoạt động ổn định và sẵn sàng kết nối qua SDK/API để phục vụ tác vụ tải/lưu file Excel mẫu và lưu file báo cáo lỗi.
    *   Người dùng hệ thống đã được đào tạo cơ bản về các định dạng dữ liệu chuẩn (Excel `.xlsx`) trước khi sử dụng chức năng Import dữ liệu.
*   **Phụ thuộc (Dependencies):**
    *   Phân hệ phụ thuộc vào tính ổn định của dịch vụ xác thực tập trung (Identity Service) của ONENET để nhận diện ID người dùng và cấp mã JWT hợp lệ cho việc lưu Audit Trail.
    *   Thư viện Mantine UI không có thay đổi mang tính phá vỡ cấu trúc (Breaking Changes) trong suốt quá trình phát triển sản phẩm.

---

# 10. Appendix

### Thuật ngữ và Từ viết tắt
*   **CRUD:** Create (Thêm), Read (Xem), Update (Sửa), Delete (Xóa).
*   **RBAC (Role-Based Access Control):** Cơ chế quản lý truy cập và phân quyền dựa trên vai trò của người dùng trong hệ thống.
*   **Audit Trail:** Nhật ký kiểm vết hệ thống, ghi nhận chi tiết lịch sử tác động dữ liệu của người dùng.
*   **Soft Delete:** Xóa mềm - cập nhật trạng thái bản ghi thay vì xóa hoàn toàn khỏi cơ sở dữ liệu.
*   **ORM (Object-Relational Mapping):** Kỹ thuật ánh xạ cơ sở dữ liệu quan hệ sang các đối tượng lập trình hướng đối tượng.
*   **JWT (JSON Web Token):** Chuẩn mã hóa chuỗi token dùng để xác thực an toàn giữa Client và Server.