Với vai trò là Kỹ sư QA/Tester cấp cao của ONENET, tôi sẽ xây dựng Tài liệu Kịch bản Kiểm thử (Test Specification) chi tiết cho tính năng "Quản lý Lớp học" (QUAN-20260530-2302) dựa trên thông tin BRD, SRS/SAD và DEV PR Diff đã cung cấp.

---

## 1. Test Specification - Quản lý Lớp học (Class Management)

**Mã tính năng:** QUAN-20260530-2302
**Tên tính năng:** Quản lý Lớp học
**BRD PR#:** 27
**SRS PR#:** 44
**DEV PR#:** 47

### 1.1. Introduction

Tài liệu này mô tả các kịch bản kiểm thử (Test Cases) cho tính năng Quản lý Lớp học trong hệ thống ONENET. Mục tiêu là đảm bảo rằng tất cả các yêu cầu về chức năng, API, bảo mật, và kiểm tra biên/dữ liệu đã được đáp ứng theo BRD, SRS, và các thay đổi trong DEV PR Diff. Tài liệu này tuân thủ các hướng dẫn kỹ thuật và mẫu Test Case của ONENET.

### 1.2. Scope

Kiểm thử sẽ bao gồm các chức năng CRUD (Create, Read, Update, Delete) cho đối tượng Lớp học (Class), bao gồm:
*   Tạo lớp học mới.
*   Xem thông tin chi tiết của một lớp học.
*   Xem danh sách các lớp học (có phân trang và bộ lọc).
*   Cập nhật thông tin lớp học hiện có.
*   Xóa lớp học.
*   Kiểm tra các ràng buộc nghiệp vụ liên quan đến giáo viên chủ nhiệm và học sinh.
*   Kiểm tra các ràng buộc dữ liệu (validation) cho các trường thông tin lớp học.
*   Kiểm tra cơ chế optimistic concurrency cho việc cập nhật.
*   Kiểm tra phân quyền người dùng (Admin, Teacher, Manager).
*   Kiểm tra xử lý ngoại lệ và phản hồi API.

### 1.3. Test Environment

*   **Hệ điều hành:** Linux/Windows Server (môi trường triển khai API), Windows/macOS (môi trường chạy Postman/Browser cho UI).
*   **Database:** PostgreSQL.
*   **API Client:** Postman, Swagger UI, hoặc công cụ tương tự.
*   **Môi trường:** Development/Staging.
*   **Authentication/Authorization:** JWT Token.

### 1.4. Test Data Pre-conditions

Để thực hiện kiểm thử, các dữ liệu sau cần được chuẩn bị trong cơ sở dữ liệu:

**Users:**
*   **Admin User:** `admin_id` (Guid), Token với vai trò "Admin".
*   **Teacher User:** `teacher_user_id` (Guid), Token với vai trò "Teacher".
*   **Manager User:** `manager_user_id` (Guid), Token với vai trò "Manager".
*   **Unauthorized User:** `unauthorized_user_id` (Guid), Token với vai trò không có quyền hoặc không có token.

**Teachers:**
*   **Existing Teacher 1:**
    *   ID: `teacher1_id` (Guid)
    *   FullName: "Nguyễn Văn A"
*   **Existing Teacher 2:**
    *   ID: `teacher2_id` (Guid)
    *   FullName: "Trần Thị B"
*   **Non-existent Teacher ID:** `non_existent_teacher_id` (Guid, vd: `Guid.NewGuid()` mới).

**Classes:**
*   **Class without Teacher & Students:**
    *   ID: `class_no_teacher_no_students_id` (Guid)
    *   ClassCode: "C001"
    *   ClassName: "Lớp A1"
    *   SchoolYear: "2023-2024"
    *   HomeroomTeacherId: `null`
    *   Version: `version_c001` (byte[])
*   **Class with Teacher, no Students:**
    *   ID: `class_with_teacher_no_students_id` (Guid)
    *   ClassCode: "C002"
    *   ClassName: "Lớp B2"
    *   SchoolYear: "2023"
    *   HomeroomTeacherId: `teacher1_id`
    *   Version: `version_c002` (byte[])
*   **Class with Students, no Teacher:**
    *   ID: `class_with_students_no_teacher_id` (Guid)
    *   ClassCode: "C003"
    *   ClassName: "Lớp C3"
    *   SchoolYear: "2024-2025"
    *   HomeroomTeacherId: `null`
    *   Version: `version_c003` (byte[])
    *   Students: 3 (assoc. with `student1_id`, `student2_id`, `student3_id`)
*   **Class with Teacher and Students:**
    *   ID: `class_with_teacher_and_students_id` (Guid)
    *   ClassCode: "C004"
    *   ClassName: "Lớp D4"
    *   SchoolYear: "2024"
    *   HomeroomTeacherId: `teacher2_id`
    *   Version: `version_c004` (byte[])
    *   Students: 5 (assoc. with `student4_id`...`student8_id`)
*   **Class for Concurrency Test:**
    *   ID: `class_for_concurrency_id` (Guid)
    *   ClassCode: "C005"
    *   ClassName: "Lớp E5"
    *   SchoolYear: "2025-2026"
    *   HomeroomTeacherId: `null`
    *   Version: `initial_version_c005` (byte[])
*   **Class for Search/Filter Test:** Nhiều lớp với nhiều niên khóa, mã, tên, giáo viên chủ nhiệm khác nhau.
*   **Non-existent Class ID:** `non_existent_class_id` (Guid, vd: `Guid.NewGuid()` mới).

**Students:**
*   **Existing Student 1:** `student1_id` (Guid), assigned to `class_with_students_no_teacher_id`.
*   **Existing Student 2:** `student2_id` (Guid), assigned to `class_with_students_no_teacher_id`.
*   **Existing Student 3:** `student3_id` (Guid), assigned to `class_with_students_no_teacher_id`.
*   *(Tương tự cho `class_with_teacher_and_students_id`)*

### 1.5. Test Cases

#### 1.5.1. Functional Tests

**QUAN-20260530-2302-TC01: Tạo Lớp học mới thành công (không có GVCN)**
*   **Test Case ID:** QUAN-20260530-2302-TC01
*   **Description:** Kiểm tra tạo lớp học mới với các thông tin hợp lệ và không chỉ định giáo viên chủ nhiệm.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR01 (Create Class), BR-QUAN-2302-01 (Valid Class Data)
*   **Pre-conditions:** Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với body:
        ```json
        {
          "classCode": "MATH001",
          "className": "Lớp Toán 1",
          "schoolYear": "2023-2024",
          "homeroomTeacherId": null
        }
        ```
    3.  **Then** Hệ thống trả về trạng thái HTTP 201 Created.
    4.  **And** Response body chứa `ClassIdDto` với `Id` của lớp học vừa tạo.
    5.  **And** Dữ liệu lớp học được lưu thành công trong DB với `ClassCode="MATH001"`, `ClassName="Lớp Toán 1"`, `SchoolYear="2023-2024"`, `HomeroomTeacherId=null`.

**QUAN-20260530-2302-TC02: Tạo Lớp học mới thành công (có GVCN hợp lệ)**
*   **Test Case ID:** QUAN-20260530-2302-TC02
*   **Description:** Kiểm tra tạo lớp học mới với các thông tin hợp lệ và chỉ định một giáo viên chủ nhiệm hiện có.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR01 (Create Class), BR-QUAN-2302-01 (Valid Class Data), BR-QUAN-2302-02 (Valid Homeroom Teacher)
*   **Pre-conditions:**
    *   Người dùng `admin_id` có quyền "Admin".
    *   Giáo viên `teacher1_id` tồn tại trong DB.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với body:
        ```json
        {
          "classCode": "LIT002",
          "className": "Lớp Văn 2",
          "schoolYear": "2024",
          "homeroomTeacherId": "teacher1_id"
        }
        ```
    3.  **Then** Hệ thống trả về trạng thái HTTP 201 Created.
    4.  **And** Response body chứa `ClassIdDto` với `Id` của lớp học vừa tạo.
    5.  **And** Dữ liệu lớp học được lưu thành công trong DB với `ClassCode="LIT002"`, `ClassName="Lớp Văn 2"`, `SchoolYear="2024"`, `HomeroomTeacherId="teacher1_id"`.

**QUAN-20260530-2302-TC03: Xem chi tiết Lớp học hiện có**
*   **Test Case ID:** QUAN-20260530-2302-TC03
*   **Description:** Kiểm tra việc lấy thông tin chi tiết của một lớp học hiện có.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR02 (Get Class Details), BR-QUAN-2302-03 (Class Exists)
*   **Pre-conditions:**
    *   Lớp học `class_with_teacher_and_students_id` tồn tại trong DB, với GVCN `teacher2_id` và 5 học sinh.
    *   Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_with_teacher_and_students_id}`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    4.  **And** Response body chứa `ClassDto` với các thông tin chi tiết:
        *   `Id`: `class_with_teacher_and_students_id`
        *   `ClassCode`: "C004"
        *   `ClassName`: "Lớp D4"
        *   `SchoolYear`: "2024"
        *   `CurrentStudentCount`: 5
        *   `HomeroomTeacherId`: `teacher2_id`
        *   `HomeroomTeacherName`: "Trần Thị B"

**QUAN-20260530-2302-TC04: Cập nhật thông tin Lớp học thành công (thay đổi tên, niên khóa)**
*   **Test Case ID:** QUAN-20260530-2302-TC04
*   **Description:** Kiểm tra cập nhật tên và niên khóa của một lớp học mà không thay đổi GVCN.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR03 (Update Class), BR-QUAN-2302-01 (Valid Class Data)
*   **Pre-conditions:**
    *   Lớp học `class_no_teacher_no_students_id` tồn tại trong DB với `Version = version_c001`.
    *   Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **And** Lấy được `version_c001` của lớp học `class_no_teacher_no_students_id`.
    3.  **When** Gửi yêu cầu PUT tới `/api/v1/Classes/{class_no_teacher_no_students_id}` với body:
        ```json
        {
          "id": "class_no_teacher_no_students_id",
          "className": "Lớp A1 Mới",
          "schoolYear": "2024-2025",
          "homeroomTeacherId": null,
          "version": "version_c001"
        }
        ```
    4.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    5.  **And** Response body chứa thông báo thành công.
    6.  **And** Lớp học trong DB với ID `class_no_teacher_no_students_id` có `ClassName="Lớp A1 Mới"`, `SchoolYear="2024-2025"`, `HomeroomTeacherId=null` và `UpdatedAt` được cập nhật, `Version` mới.

**QUAN-20260530-2302-TC05: Cập nhật thông tin Lớp học thành công (gán GVCN)**
*   **Test Case ID:** QUAN-20260530-2302-TC05
*   **Description:** Kiểm tra cập nhật lớp học bằng cách gán một giáo viên chủ nhiệm hợp lệ.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR03 (Update Class), BR-QUAN-2302-02 (Valid Homeroom Teacher)
*   **Pre-conditions:**
    *   Lớp học `class_no_teacher_no_students_id` tồn tại trong DB với `HomeroomTeacherId=null` và `Version = updated_version_c001` (từ TC04).
    *   Giáo viên `teacher2_id` tồn tại trong DB.
    *   Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **And** Lấy được `updated_version_c001` của lớp học `class_no_teacher_no_students_id`.
    3.  **When** Gửi yêu cầu PUT tới `/api/v1/Classes/{class_no_teacher_no_students_id}` với body:
        ```json
        {
          "id": "class_no_teacher_no_students_id",
          "className": "Lớp A1 Mới", // Giữ nguyên tên
          "schoolYear": "2024-2025", // Giữ nguyên niên khóa
          "homeroomTeacherId": "teacher2_id",
          "version": "updated_version_c001"
        }
        ```
    4.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    5.  **And** Response body chứa thông báo thành công.
    6.  **And** Lớp học trong DB với ID `class_no_teacher_no_students_id` có `HomeroomTeacherId="teacher2_id"` và `UpdatedAt` được cập nhật, `Version` mới.

**QUAN-20260530-2302-TC06: Xóa Lớp học thành công (không có GVCN và Học sinh)**
*   **Test Case ID:** QUAN-20260530-2302-TC06
*   **Description:** Kiểm tra xóa một lớp học không có giáo viên chủ nhiệm và không có học sinh.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR04 (Delete Class), BR-QUAN-2302-04 (No Students), BR-QUAN-2302-05 (No Homeroom Teacher)
*   **Pre-conditions:**
    *   Lớp học `class_no_teacher_no_students_id` tồn tại trong DB, không có GVCN và không có học sinh.
    *   Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập với vai trò "Admin".
    2.  **And** Lớp học `class_no_teacher_no_students_id` không có GVCN và không có học sinh.
    3.  **When** Gửi yêu cầu DELETE tới `/api/v1/Classes/{class_no_teacher_no_students_id}`.
    4.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    5.  **And** Response body chứa thông báo thành công.
    6.  **And** Lớp học trong DB với ID `class_no_teacher_no_students_id` được đánh dấu `IsDeleted=true`.

**QUAN-20260530-2302-TC07: Lấy danh sách Lớp học (phân trang và bộ lọc)**
*   **Test Case ID:** QUAN-20260530-2302-TC07
*   **Description:** Kiểm tra lấy danh sách lớp học với phân trang và các bộ lọc hợp lệ.
*   **Category:** Functional, Happy Path
*   **REFERENCES:** SRS-QUAN-2302-FR05 (Get Classes List), BR-QUAN-2302-06 (Pagination & Filtering)
*   **Pre-conditions:**
    *   Tồn tại nhiều lớp học với các `SchoolYear`, `ClassCode`, `HomeroomTeacherId` khác nhau.
    *   Người dùng `teacher_user_id` có quyền "Teacher".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `teacher_user_id` đã đăng nhập với vai trò "Teacher".
    2.  **And** Có ít nhất 10 lớp học trong DB, trong đó có 3 lớp có `SchoolYear="2024-2025"` và tên GVCN chứa "Trần".
    3.  **When** Gửi yêu cầu GET tới `/api/v1/Classes?PageNumber=1&PageSize=5&SchoolYear=2024-2025&HomeroomTeacherName=Trần`.
    4.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    5.  **And** Response body chứa `PaginatedList<ClassDto>` với:
        *   `PageNumber`: 1
        *   `PageSize`: 5
        *   `TotalCount`: Số lượng lớp học khớp với bộ lọc (ví dụ: 3)
        *   `Items`: Một danh sách các `ClassDto` chỉ chứa các lớp có `SchoolYear="2024-2025"` và tên GVCN chứa "Trần", tối đa 5 lớp, được sắp xếp theo `SchoolYear` rồi `ClassCode`.

#### 1.5.2. API Tests

**QUAN-20260530-2302-API01: Kiểm tra cấu trúc phản hồi khi tạo lớp thành công**
*   **Test Case ID:** QUAN-20260530-2302-API01
*   **Description:** Đảm bảo cấu trúc JSON của phản hồi khi tạo lớp thành công đúng chuẩn `ApiResponse` và `ClassIdDto`.
*   **Category:** API, Structure
*   **REFERENCES:** SRS-QUAN-2302-FR01, DEV-QUAN-2302-01 (CreateClassCommand, ClassIdDto)
*   **Pre-conditions:** Người dùng `admin_id` có quyền "Admin".
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu POST tạo lớp học mới với dữ liệu hợp lệ (ClassCode: "API001", ClassName: "API Test", SchoolYear: "2023").
    3.  **Then** Hệ thống trả về trạng thái HTTP 201 Created.
    4.  **And** Response body phải có cấu trúc:
        ```json
        {
          "success": true,
          "data": {
            "id": "guid-cua-lop-hoc-vua-tao"
          },
          "message": "Thành công.",
          "errors": null
        }
        ```
    5.  **And** `data.id` phải là một GUID hợp lệ.

**QUAN-20260530-2302-API02: Kiểm tra cấu trúc phản hồi khi lấy chi tiết lớp học**
*   **Test Case ID:** QUAN-20260530-2302-API02
*   **Description:** Đảm bảo cấu trúc JSON của phản hồi khi lấy chi tiết lớp học đúng chuẩn `ApiResponse` và `ClassDto`.
*   **Category:** API, Structure
*   **REFERENCES:** SRS-QUAN-2302-FR02, DEV-QUAN-2302-02 (GetClassByIdQuery, ClassDto)
*   **Pre-conditions:**
    *   Lớp học `class_with_teacher_and_students_id` tồn tại.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_with_teacher_and_students_id}`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    4.  **And** Response body phải có cấu trúc:
        ```json
        {
          "success": true,
          "data": {
            "id": "guid-cua-lop-hoc",
            "classCode": "C004",
            "className": "Lớp D4",
            "schoolYear": "2024",
            "currentStudentCount": 5,
            "homeroomTeacherId": "guid-cua-gvcn",
            "homeroomTeacherName": "Tên GVCN"
          },
          "message": null, // Hoặc "Thành công." tùy implement
          "errors": null
        }
        ```
    5.  **And** Các trường `id`, `homeroomTeacherId` phải là GUID hợp lệ.

**QUAN-20260530-2302-API03: Kiểm tra xử lý ngoại lệ BusinessRuleException (409 Conflict)**
*   **Test Case ID:** QUAN-20260530-2302-API03
*   **Description:** Xác minh API trả về trạng thái 409 Conflict và cấu trúc lỗi đúng khi xảy ra `BusinessRuleException` (ví dụ: xóa lớp có học sinh).
*   **Category:** API, Error Handling
*   **REFERENCES:** SRS-QUAN-2302-FR04, DEV-QUAN-2302-03 (DeleteClassCommandHandler)
*   **Pre-conditions:**
    *   Lớp học `class_with_students_no_teacher_id` tồn tại và có học sinh.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **And** Lớp học `class_with_students_no_teacher_id` hiện có 3 học sinh.
    3.  **When** Gửi yêu cầu DELETE tới `/api/v1/Classes/{class_with_students_no_teacher_id}`.
    4.  **Then** Hệ thống trả về trạng thái HTTP 409 Conflict.
    5.  **And** Response body phải có cấu trúc lỗi:
        ```json
        {
          "success": false,
          "data": null,
          "message": "Không thể xóa lớp học 'C003' vì hiện có 3 học sinh đang theo học. Vui lòng chuyển hoặc xóa học sinh trước khi thực hiện.",
          "errors": null
        }
        ```

#### 1.5.3. Security Tests

**QUAN-20260530-2302-SEC01: Phân quyền tạo lớp học (Admin Only)**
*   **Test Case ID:** QUAN-20260530-2302-SEC01
*   **Description:** Kiểm tra chỉ có người dùng với vai trò "Admin" mới được phép tạo lớp học.
*   **Category:** Security, Authorization
*   **REFERENCES:** SRS-QUAN-2302-SEC01, DEV-QUAN-2302-04 (ClassesController Authorize Admin)
*   **Pre-conditions:**
    *   Người dùng `teacher_user_id` (vai trò Teacher).
    *   Người dùng `manager_user_id` (vai trò Manager).
    *   Người dùng không có token hợp lệ.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `teacher_user_id` đã đăng nhập với vai trò "Teacher".
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với dữ liệu tạo lớp hợp lệ.
    3.  **Then** Hệ thống trả về trạng thái HTTP 403 Forbidden.
    ---
    4.  **Given** Người dùng `manager_user_id` đã đăng nhập với vai trò "Manager".
    5.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với dữ liệu tạo lớp hợp lệ.
    6.  **Then** Hệ thống trả về trạng thái HTTP 403 Forbidden.
    ---
    7.  **Given** Người dùng không có token hợp lệ.
    8.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với dữ liệu tạo lớp hợp lệ.
    9.  **Then** Hệ thống trả về trạng thái HTTP 401 Unauthorized.

**QUAN-20260530-2302-SEC02: Phân quyền xem chi tiết/danh sách lớp học (Admin, Teacher, Manager)**
*   **Test Case ID:** QUAN-20260530-2302-SEC02
*   **Description:** Kiểm tra các vai trò "Admin", "Teacher", "Manager" được phép xem thông tin lớp học.
*   **Category:** Security, Authorization
*   **REFERENCES:** SRS-QUAN-2302-SEC02, DEV-QUAN-2302-05 (ClassesController Authorize Admin,Teacher,Manager)
*   **Pre-conditions:**
    *   Lớp học `class_no_teacher_no_students_id` tồn tại.
    *   Người dùng `admin_id`, `teacher_user_id`, `manager_user_id` đã đăng nhập.
    *   Người dùng không có token hợp lệ.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_no_teacher_no_students_id}`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    ---
    4.  **Given** Người dùng `teacher_user_id` đã đăng nhập.
    5.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_no_teacher_no_students_id}`.
    6.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    ---
    7.  **Given** Người dùng `manager_user_id` đã đăng nhập.
    8.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_no_teacher_no_students_id}`.
    9.  **Then** Hệ thống trả về trạng thái HTTP 200 OK.
    ---
    10. **Given** Người dùng không có token hợp lệ.
    11. **When** Gửi yêu cầu GET tới `/api/v1/Classes/{class_no_teacher_no_students_id}`.
    12. **Then** Hệ thống trả về trạng thái HTTP 401 Unauthorized.

#### 1.5.4. Boundary / Validation Tests

**QUAN-20260530-2302-BV01: Tạo Lớp học - Mã lớp đã tồn tại**
*   **Test Case ID:** QUAN-20260530-2302-BV01
*   **Description:** Kiểm tra việc tạo lớp học với `ClassCode` đã tồn tại.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR01, DEV-QUAN-2302-06 (CreateClassCommandValidator - BeUniqueClassCode)
*   **Pre-conditions:**
    *   Lớp học `class_no_teacher_no_students_id` có `ClassCode="C001"` tồn tại.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với body:
        ```json
        {
          "classCode": "C001", // Mã lớp đã tồn tại
          "className": "Lớp trùng mã",
          "schoolYear": "2023-2024",
          "homeroomTeacherId": null
        }
        ```
    3.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    4.  **And** Response body chứa lỗi validation với `message="Mã lớp 'C001' đã tồn tại."` hoặc tương tự.

**QUAN-20260530-2302-BV02: Tạo/Cập nhật Lớp học - Niên khóa không hợp lệ**
*   **Test Case ID:** QUAN-20260530-2302-BV02
*   **Description:** Kiểm tra các định dạng `SchoolYear` không hợp lệ.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR01, SRS-QUAN-2302-FR03, DEV-QUAN-2302-07 (CreateClassCommandValidator/UpdateClassCommandValidator - BeValidSchoolYearFormat)
*   **Pre-conditions:** Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với `schoolYear="2023-2025"` (không phải 2023-2024).
    3.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    4.  **And** Response body chứa lỗi validation với `message="Niên khóa không đúng định dạng YYYY hoặc YYYY-YYYY."`.
    ---
    5.  **Given** Người dùng `admin_id` đã đăng nhập.
    6.  **And** Lớp học `class_no_teacher_no_students_id` tồn tại với `Version = current_version`.
    7.  **When** Gửi yêu cầu PUT tới `/api/v1/Classes/{class_no_teacher_no_students_id}` với `schoolYear="2022-2022"`.
    8.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    9.  **And** Response body chứa lỗi validation tương tự.

**QUAN-20260530-2302-BV03: Tạo/Cập nhật Lớp học - GVCN không tồn tại**
*   **Test Case ID:** QUAN-20260530-2302-BV03
*   **Description:** Kiểm tra việc gán một `HomeroomTeacherId` không tồn tại.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR01, SRS-QUAN-2302-FR03, DEV-QUAN-2302-08 (CreateClassCommandValidator/UpdateClassCommandValidator - BeExistingTeacher)
*   **Pre-conditions:** Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu POST tới `/api/v1/Classes` với `homeroomTeacherId="non_existent_teacher_id"`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    4.  **And** Response body chứa lỗi validation với `message="Giáo viên chủ nhiệm không hợp lệ."`.
    ---
    5.  **Given** Người dùng `admin_id` đã đăng nhập.
    6.  **And** Lớp học `class_no_teacher_no_students_id` tồn tại với `Version = current_version`.
    7.  **When** Gửi yêu cầu PUT tới `/api/v1/Classes/{class_no_teacher_no_students_id}` với `homeroomTeacherId="non_existent_teacher_id"`.
    8.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    9.  **And** Response body chứa lỗi validation tương tự.

**QUAN-20260530-2302-BV04: Cập nhật Lớp học - Optimistic Concurrency (DbUpdateConcurrencyException)**
*   **Test Case ID:** QUAN-20260530-2302-BV04
*   **Description:** Kiểm tra xử lý trường hợp optimistic concurrency khi cập nhật lớp học.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR03, DEV-QUAN-2302-09 (UpdateClassCommandHandler - DbUpdateConcurrencyException)
*   **Pre-conditions:**
    *   Lớp học `class_for_concurrency_id` tồn tại với `Version = initial_version_c005`.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **And** Lấy `initial_version_c005` của `class_for_concurrency_id`.
    3.  **And** Một người dùng khác đã *thành công* cập nhật `class_for_concurrency_id`, làm thay đổi `Version` trong DB thành `new_version_c005`.
    4.  **When** Người dùng `admin_id` gửi yêu cầu PUT tới `/api/v1/Classes/{class_for_concurrency_id}` với `Version = initial_version_c005` (phiên bản cũ).
    5.  **Then** Hệ thống trả về trạng thái HTTP 409 Conflict.
    6.  **And** Response body chứa `message="Lớp học đã được người dùng khác cập nhật. Vui lòng thử lại."`.
    7.  **And** Dữ liệu lớp học trong DB không bị thay đổi bởi yêu cầu của `admin_id`.

**QUAN-20260530-2302-BV05: Xóa Lớp học - Lớp đang có học sinh**
*   **Test Case ID:** QUAN-20260530-2302-BV05
*   **Description:** Kiểm tra không thể xóa lớp học nếu lớp đó đang có học sinh.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR04, BR-QUAN-2302-04, DEV-QUAN-2302-10 (DeleteClassCommandHandler - GetStudentCountInClassAsync)
*   **Pre-conditions:**
    *   Lớp học `class_with_students_no_teacher_id` tồn tại và có ít nhất 1 học sinh.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **And** Lớp học `class_with_students_no_teacher_id` hiện có 3 học sinh.
    3.  **When** Gửi yêu cầu DELETE tới `/api/v1/Classes/{class_with_students_no_teacher_id}`.
    4.  **Then** Hệ thống trả về trạng thái HTTP 409 Conflict.
    5.  **And** Response body chứa `message="Không thể xóa lớp học 'C003' vì hiện có 3 học sinh đang theo học. Vui lòng chuyển hoặc xóa học sinh trước khi thực hiện."`.
    6.  **And** Lớp học `class_with_students_no_teacher_id` vẫn tồn tại và không bị đánh dấu `IsDeleted`.

**QUAN-20260530-2302-BV06: Xóa Lớp học - Lớp đang có GVCN**
*   **Test Case ID:** QUAN-20260530-2302-BV06
*   **Description:** Kiểm tra không thể xóa lớp học nếu lớp đó đang có giáo viên chủ nhiệm.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR04, BR-QUAN-2302-05, DEV-QUAN-2302-11 (DeleteClassCommandHandler - HomeroomTeacherId.HasValue)
*   **Pre-conditions:**
    *   Lớp học `class_with_teacher_no_students_id` tồn tại và có GVCN `teacher1_id`.
    *   Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **And** Lớp học `class_with_teacher_no_students_id` có GVCN `teacher1_id` ("Nguyễn Văn A").
    3.  **When** Gửi yêu cầu DELETE tới `/api/v1/Classes/{class_with_teacher_no_students_id}`.
    4.  **Then** Hệ thống trả về trạng thái HTTP 409 Conflict.
    5.  **And** Response body chứa `message="Không thể xóa lớp học 'C002' vì đang được phân công cho Giáo viên Nguyễn Văn A. Vui lòng thay đổi Giáo viên chủ nhiệm trước khi xóa."`.
    6.  **And** Lớp học `class_with_teacher_no_students_id` vẫn tồn tại và không bị đánh dấu `IsDeleted`.

**QUAN-20260530-2302-BV07: Lấy danh sách Lớp học - Kích thước trang không hợp lệ**
*   **Test Case ID:** QUAN-20260530-2302-BV07
*   **Description:** Kiểm tra trường hợp `PageSize` không nằm trong danh sách cho phép (10, 25, 50, 100).
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR05, DEV-QUAN-2302-12 (GetClassesQueryValidator - _allowedPageSizes)
*   **Pre-conditions:** Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu GET tới `/api/v1/Classes?PageNumber=1&PageSize=20`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 400 Bad Request.
    4.  **And** Response body chứa lỗi validation với `message="Kích thước trang phải là một trong các giá trị: 10, 25, 50, 100."`.

**QUAN-20260530-2302-BV08: NotFoundException (404 Not Found)**
*   **Test Case ID:** QUAN-20260530-2302-BV08
*   **Description:** Kiểm tra API trả về 404 Not Found khi ID lớp học không tồn tại.
*   **Category:** Boundary/Validation, Negative
*   **REFERENCES:** SRS-QUAN-2302-FR02, SRS-QUAN-2302-FR03, SRS-QUAN-2302-FR04, DEV-QUAN-2302-13 (NotFoundException)
*   **Pre-conditions:** Người dùng `admin_id` đã đăng nhập.
*   **Test Steps (BDD):**
    1.  **Given** Người dùng `admin_id` đã đăng nhập.
    2.  **When** Gửi yêu cầu GET tới `/api/v1/Classes/{non_existent_class_id}`.
    3.  **Then** Hệ thống trả về trạng thái HTTP 404 Not Found.
    4.  **And** Response body chứa `message="Lớp học với ID 'non_existent_class_id' không tìm thấy."`.
    ---
    5.  **Given** Người dùng `admin_id` đã đăng nhập.
    6.  **When** Gửi yêu cầu PUT tới `/api/v1/Classes/{non_existent_class_id}` với dữ liệu hợp lệ và một `Version` bất kỳ.
    7.  **Then** Hệ thống trả về trạng thái HTTP 404 Not Found.
    8.  **And** Response body chứa `message="Lớp học với ID 'non_existent_class_id' không tìm thấy."`.
    ---
    9.  **Given** Người dùng `admin_id` đã đăng nhập.
    10. **When** Gửi yêu cầu DELETE tới `/api/v1/Classes/{non_existent_class_id}`.
    11. **Then** Hệ thống trả về trạng thái HTTP 404 Not Found.
    12. **And** Response body chứa `message="Lớp học với ID 'non_existent_class_id' không tìm thấy."`.

#### 1.5.5. Other Tests (Placeholder - Not fully detailed as per main request focus)

**Performance Tests:**
*   QUAN-20260530-2302-PERF01: Kiểm tra hiệu suất API GetClasses khi có lượng lớn dữ liệu (10,000+ lớp học) và các bộ lọc phức tạp.
*   QUAN-20260530-2302-PERF02: Kiểm tra thời gian phản hồi của các API CRUD dưới tải đồng thời cao.

**Usability Tests:**
*   QUAN-20260530-2302-USAB01: Đảm bảo các thông báo lỗi hiển thị cho người dùng thân thiện và dễ hiểu.
*   QUAN-20260530-2302-USAB02: Kiểm tra luồng tạo/sửa/xóa lớp học trên giao diện người dùng (nếu có).

**Compatibility Tests:**
*   QUAN-20260530-2302-COMP01: Kiểm tra API hoạt động đúng trên các trình duyệt và thiết bị khác nhau (nếu có UI).

### 1.6. Traceability Matrix

| Requirement ID (Inferred) | Description                                         | Test Cases Covered                                                                                                                                                                                                  |
| :------------------------ | :-------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| SRS-QUAN-2302-FR01        | Tạo Lớp học mới                                     | QUAN-20260530-2302-TC01, QUAN-20260530-2302-TC02, QUAN-20260530-2302-API01, QUAN-20260530-2302-SEC01, QUAN-20260530-2302-BV01, QUAN-20260530-2302-BV02, QUAN-20260530-2302-BV03                                       |
| SRS-QUAN-2302-FR02        | Xem chi tiết Lớp học theo ID                        | QUAN-20260530-2302-TC03, QUAN-20260530-2302-API02, QUAN-20260530-2302-SEC02, QUAN-20260530-2302-BV08                                                                                                                     |
| SRS-QUAN-2302-FR03        | Cập nhật thông tin Lớp học                          | QUAN-20260530-2302-TC04, QUAN-20260530-2302-TC05, QUAN-20260530-2302-SEC01, QUAN-20260530-2302-BV02, QUAN-20260530-2302-BV03, QUAN-20260530-2302-BV04, QUAN-20260530-2302-BV08                                       |
| SRS-QUAN-2302-FR04        | Xóa Lớp học                                         | QUAN-20260530-2302-TC06, QUAN-20260530-2302-API03, QUAN-20260530-2302-SEC01, QUAN-20260530-2302-BV05, QUAN-20260530-2302-BV06, QUAN-20260530-2302-BV08                                                           |
| SRS-QUAN-2302-FR05        | Xem danh sách Lớp học (phân trang, lọc)             | QUAN-20260530-2302-TC07, QUAN-20260530-2302-SEC02, QUAN-20260530-2302-BV07                                                                                                                                           |
| BR-QUAN-2302-01           | Dữ liệu Lớp học hợp lệ (mã, tên, niên khóa)         | QUAN-20260530-2302-TC01, QUAN-20260530-2302-TC02, QUAN-20260530-2302-TC04, QUAN-20260530-2302-BV01, QUAN-20260530-2302-BV02                                                                                        |
| BR-QUAN-2302-02           | GVCN phải tồn tại nếu được chỉ định                 | QUAN-20260530-2302-TC02, QUAN-20260530-2302-TC05, QUAN-20260530-2302-BV03                                                                                                                                           |
| BR-QUAN-2302-03           | Lớp học phải tồn tại                                | QUAN-20260530-2302-TC03, QUAN-20260530-2302-BV08                                                                                                                                                                      |
| BR-QUAN-2302-04           | Không xóa lớp có học sinh                          | QUAN-20260530-2302-API03, QUAN-20260530-2302-BV05                                                                                                                                                                      |
| BR-QUAN-2302-05           | Không xóa lớp có GVCN                              | QUAN-20260530-2302-BV06                                                                                                                                                                                               |
| BR-QUAN-2302-06           | Phân trang và bộ lọc hoạt động đúng                | QUAN-20260530-2302-TC07, QUAN-20260530-2302-BV07                                                                                                                                                                      |
| BR-QUAN-2302-07           | Xử lý Optimistic Concurrency                       | QUAN-20260530-2302-BV04                                                                                                                                                                                               |
| SRS-QUAN-2302-SEC01       | Phân quyền Create/Update/Delete (Admin)            | QUAN-20260530-2302-SEC01                                                                                                                                                                                            |
| SRS-QUAN-2302-SEC02       | Phân quyền Get (Admin, Teacher, Manager)           | QUAN-20260530-2302-SEC02                                                                                                                                                                                            |
| DEV-QUAN-2302-01          | CreateClassCommand & ClassIdDto structure          | QUAN-20260530-2302-TC01, QUAN-20260530-2302-API01                                                                                                                                                                     |
| DEV-QUAN-2302-02          | GetClassByIdQuery & ClassDto structure             | QUAN-20260530-2302-TC03, QUAN-20260530-2302-API02                                                                                                                                                                     |
| DEV-QUAN-2302-03          | DeleteClassCommandHandler logic                    | QUAN-20260530-2302-TC06, QUAN-20260530-2302-API03, QUAN-20260530-2302-BV05, QUAN-20260530-2302-BV06                                                                                                                 |
| DEV-QUAN-2302-04          | ClassesController [Authorize(Roles = "Admin")]     | QUAN-20260530-2302-SEC01                                                                                                                                                                                            |
| DEV-QUAN-2302-05          | ClassesController [Authorize(Roles = "Admin,Teacher,Manager")] | QUAN-20260530-2302-SEC02                                                                                                                                                                                            |
| DEV-QUAN-2302-06          | CreateClassCommandValidator - Unique ClassCode     | QUAN-20260530-2302-BV01                                                                                                                                                                                            |
| DEV-QUAN-2302-07          | Create/UpdateClassCommandValidator - SchoolYear Format | QUAN-20260530-2302-BV02                                                                                                                                                                                            |
| DEV-QUAN-2302-08          | Create/UpdateClassCommandValidator - Existing Teacher | QUAN-20260530-2302-BV03                                                                                                                                                                                            |
| DEV-QUAN-2302-09          | UpdateClassCommandHandler - DbUpdateConcurrencyException | QUAN-20260530-2302-BV04                                                                                                                                                                                            |
| DEV-QUAN-2302-10          | DeleteClassCommandHandler - GetStudentCountInClassAsync | QUAN-20260530-2302-BV05                                                                                                                                                                                            |
| DEV-QUAN-2302-11          | DeleteClassCommandHandler - HomeroomTeacherId.HasValue | QUAN-20260530-2302-BV06                                                                                                                                                                                            |
| DEV-QUAN-2302-12          | GetClassesQueryValidator - PageSize allowed values | QUAN-20260530-2302-BV07                                                                                                                                                                                            |
| DEV-QUAN-2302-13          | GlobalExceptionMiddleware - NotFoundException      | QUAN-20260530-2302-BV08                                                                                                                                                                                            |
| DEV-QUAN-2302-14          | GlobalExceptionMiddleware - BusinessRuleException  | QUAN-20260530-2302-API03, QUAN-20260530-2302-BV05, QUAN-20260530-2302-BV06                                                                                                                                        |
| DEV-QUAN-2302-15          | GlobalExceptionMiddleware - DbUpdateConcurrencyException | QUAN-20260530-2302-BV04                                                                                                                                                                                            |
| DEV-QUAN-2302-16          | GlobalExceptionMiddleware - UnauthorizedAccessException | QUAN-20260530-2302-SEC01, QUAN-20260530-2302-SEC02                                                                                                                                                                  |
| DEV-QUAN-2302-17          | GlobalExceptionMiddleware - ValidationException    | QUAN-20260530-2302-BV01, QUAN-20260530-2302-BV02, QUAN-20260530-2302-BV03, QUAN-20260530-2302-BV07                                                                                                                 |

---