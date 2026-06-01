Tuyệt vời! Với vai trò là Kỹ sư QA/Tester cấp cao của ONENET, tôi sẽ tạo tài liệu Test Specification chi tiết cho tính năng "Quản Lý Môn Học" (`QUAN-20260531-154643`) dựa trên các thông tin đã cho và tuân thủ các quy tắc, template của ONENET.

---

## Test Specification for Feature: Quan Ly Mon Hoc

**Test Specification ID:** TS-QUAN-20260531-154643
**Project:** ONENET
**Feature ID:** QUAN-20260531-154643
**Feature Name:** Quản Lý Môn Học
**Version:** 1.0
**Date:** 2024-05-31
**Author:** ONENET QA Team

---

### 1. Introduction

Tài liệu này mô tả kế hoạch kiểm thử và các trường hợp kiểm thử chi tiết cho tính năng "Quản Lý Môn Học" trong hệ thống ONENET. Tính năng này cho phép người dùng có quyền thực hiện các thao tác CRUD (Create, Read, Update, Delete) đối với thông tin môn học, bao gồm quản lý mã môn học, tên, mô tả, số tín chỉ và trạng thái hoạt động.

### 2. Purpose

Mục đích của tài liệu này là để:
*   Đảm bảo tính năng "Quản Lý Môn Học" hoạt động đúng theo yêu cầu nghiệp vụ (BRD) và yêu cầu hệ thống (SRS/SAD).
*   Xác định rõ ràng các kịch bản kiểm thử (test cases) bao gồm Happy Path, Edge Cases, và Negative Cases.
*   Đảm bảo chất lượng, tính ổn định, bảo mật và hiệu suất của tính năng trước khi triển khai.
*   Cung cấp cơ sở để theo dõi và báo cáo tiến độ kiểm thử.
*   Kiểm tra việc tuân thủ các quy tắc và hướng dẫn kỹ thuật của ONENET.

### 3. Scope

Phạm vi kiểm thử bao gồm các chức năng chính sau:
*   **Tạo mới Môn Học (Create Subject):** Tạo môn học với các thuộc tính: Mã, Tên, Mô tả, Số tín chỉ, Trạng thái hoạt động. Đảm bảo tính duy nhất của Mã và Tên.
*   **Xem danh sách Môn Học (Get Subjects List):** Lấy danh sách môn học có phân trang, tìm kiếm theo Mã/Tên, và sắp xếp. Hỗ trợ hiển thị môn học không hoạt động cho người dùng có quyền.
*   **Xem chi tiết Môn Học (Get Subject by ID):** Lấy thông tin chi tiết của một môn học theo ID.
*   **Cập nhật Môn Học (Update Subject):** Cập nhật thông tin của một môn học đã tồn tại. Đảm bảo tính duy nhất của Mã và Tên sau khi cập nhật (nếu Mã/Tên được thay đổi).
*   **Xóa Môn Học (Soft Delete Subject):** Thực hiện xóa mềm một môn học bằng cách đặt trạng thái `IsActive` và `IsDeleted` về `false`/`true`.
*   **Xác thực dữ liệu (Validation):** Đảm bảo các trường dữ liệu đầu vào tuân thủ các quy tắc về định dạng, độ dài, khoảng giá trị.
*   **Phân quyền (Authorization):** Đảm bảo chỉ những người dùng có vai trò phù hợp mới có thể truy cập và thực hiện các chức năng.
*   **Ghi log Audit (Audit Logging):** Xác minh các thao tác tạo, cập nhật, xóa mềm được ghi lại đầy đủ trong hệ thống audit log.
*   **Xử lý ngoại lệ (Exception Handling):** Kiểm tra các trường hợp lỗi được xử lý và trả về phản hồi thích hợp (ví dụ: NotFoundException, ValidationException).

### 4. References

*   **BRD PR#:** 41
*   **SRS PR#:** 42
*   **DEV PR#:** 49
*   Technical Guideline: `docs/rules/Technical_Guideline.md`
*   Template TestCase: `docs/templates/TestCase_Template.md`
*   API Documentation (implied from DEV PR Diff - `SubjectsController.cs`, `ApiResponse.cs`)

### 5. Environment

*   **Development Environment:** Local development setup, CI/CD environment.
*   **Database:** PostgreSQL.
*   **Backend:** .NET 8 Web API.
*   **Authentication:** JWT Bearer Token.
*   **Logging:** Serilog.

### 6. Test Data Pre-conditions

Để thực hiện kiểm thử, cần chuẩn bị các dữ liệu sau trong hệ thống:

**6.1. Users (Người dùng)**

| Username | Password | Roles | JWT Token (Example) | Description |
| :------- | :------- | :---- | :------------------ | :---------- |
| `admin_user` | `P@ssw0rd1!` | `Admin` | `{admin_token}` | Có toàn quyền quản lý môn học. |
| `edu_staff` | `P@ssw0rd1!` | `ChuyenVienDaoTao` | `{edu_staff_token}` | Có toàn quyền quản lý môn học. |
| `teacher_user` | `P@ssw0rd1!` | `GiaoVien` | `{teacher_token}` | Chỉ có quyền xem danh sách và chi tiết môn học. |
| `normal_user` | `P@ssw0rd1!` | `Student` | `{normal_token}` | Không có quyền quản lý môn học. |
| `no_token` | N/A | N/A | `No Token` | Người dùng chưa xác thực. |

**6.2. Subjects (Môn học)**

| ID (GUID) | Code | Name | Description | Credits | IsActive | IsDeleted | CreatedBy |
| :-------- | :--- | :--- | :---------- | :------ | :------- | :-------- | :-------- |
| `{guid_s1}` | `CS101` | `Lap trinh co ban` | `Mon hoc nhap mon` | 3 | `true` | `false` | `admin_user` |
| `{guid_s2}` | `MA201` | `Dai so tuyen tinh` | `Mon hoc nang cao` | 4 | `true` | `false` | `edu_staff` |
| `{guid_s3}` | `PH301` | `Vat ly dai cuong` | `Mon hoc co ban` | 2 | `false` | `false` | `admin_user` |
| `{guid_s4}` | `BIO101` | `Sinh hoc dai cuong` | `Mon hoc khoa hoc` | 3 | `true` | `false` | `admin_user` |
| `{guid_s_update}` | `TEST_UPD` | `Mon hoc cap nhat` | `Mo ta` | 3 | `true` | `false` | `admin_user` |
| `{guid_s_delete}` | `TEST_DEL` | `Mon hoc xoa mem` | `Mo ta` | 2 | `true` | `false` | `admin_user` |
| `{guid_s_deleted}` | `DEL999` | `Mon da xoa` | `Mo ta` | 1 | `false` | `true` | `admin_user` |

**6.3. Audit Log (Hệ thống Audit Log)**
*   Đảm bảo hệ thống Audit Log được cấu hình và hoạt động.
*   Kiểm tra các bản ghi được tạo ra với `ActionType`, `EntityName`, `EntityId`, `UserName`, và `Changes` chính xác.

---

### 7. Functional Test Cases

#### 7.1. Create Subject (Tạo mới Môn Học) - POST /api/v1/Subjects

**FR/BR/SRS Ref:** BRD PR#41, SRS PR#42, DEV PR#49 (`CreateSubjectCommand.cs`, `CreateSubjectCommandHandler.cs`, `CreateSubjectCommandValidator.cs`, `SubjectsController.cs`)

**QUAN-20260531-154643-TC01: Happy Path - Create a new subject with all valid details (Admin Role)**
*   **Description:** Verify that an Admin user can successfully create a new subject with valid data.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Subject data: `Code="MATH101"`, `Name="Toan Cao Cap A1"`, `Description="Mon hoc dai cuong ve toan"`, `Credits=3`, `IsActive=true`
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a POST request to `/api/v1/Subjects` with the following valid JSON body:
        ```json
        {
          "code": "MATH101",
          "name": "Toan Cao Cap A1",
          "description": "Mon hoc dai cuong ve toan",
          "credits": 3,
          "isActive": true
        }
        ```
    3.  **Then** the response status code should be `201 Created`.
    4.  **And** the response body should contain `Success: true`, `Message: "Môn học được tạo thành công."`, and a valid `Data` (GUID of the new subject).
    5.  **And** I can retrieve the created subject by its ID using GET `/api/v1/Subjects/{id}`.
    6.  **And** the retrieved subject details match the created data.
    7.  **And** an audit log entry should be created with `ActionType="Create"`, `EntityName="Subject"`, `EntityId={new_subject_id}`, `UserName="admin_user"`, and `Changes` reflecting the created data.

**QUAN-20260531-154643-TC02: Edge Case - Create a new subject with minimal valid details (ChuyenVienDaoTao Role)**
*   **Description:** Verify that a ChuyenVienDaoTao user can successfully create a new subject with only required fields. `IsActive` should default to `true`.
*   **Test Data Pre-conditions:**
    *   User: `edu_staff` (`{edu_staff_token}`)
    *   Subject data: `Code="ENGL201"`, `Name="Tieng Anh chuyen nganh 1"`, `Credits=2` (Description is null, IsActive defaults to true)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "ChuyenVienDaoTao" role (`{edu_staff_token}`).
    2.  **When** I send a POST request to `/api/v1/Subjects` with the following valid JSON body:
        ```json
        {
          "code": "ENGL201",
          "name": "Tieng Anh chuyen nganh 1",
          "credits": 2
        }
        ```
    3.  **Then** the response status code should be `201 Created`.
    4.  **And** the response body should contain `Success: true`, `Message: "Môn học được tạo thành công."`, and a valid `Data` (GUID of the new subject).
    5.  **And** I can retrieve the created subject by its ID using GET `/api/v1/Subjects/{id}`.
    6.  **And** the retrieved subject's `IsActive` field should be `true` and `Description` should be `null`.
    7.  **And** an audit log entry should be created with `ActionType="Create"`, `EntityName="Subject"`, `EntityId={new_subject_id}`, `UserName="edu_staff"`, and `Changes` reflecting the created data.

**QUAN-20260531-154643-TC03: Negative Case - Create subject with duplicate Code**
*   **Description:** Verify that creating a subject with an already existing code fails.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Existing subject: `CS101` (`{guid_s1}`)
    *   New subject data: `Code="CS101"`, `Name="Lap trinh web"`, `Credits=3`
*   **Test Steps (BDD):**
    1.  **Given** an existing subject with Code `CS101` is present in the system.
    2.  **And** I am an authenticated user with "Admin" role (`{admin_token}`).
    3.  **When** I send a POST request to `/api/v1/Subjects` with a JSON body containing `Code="CS101"` and a unique `Name`.
    4.  **Then** the response status code should be `400 Bad Request`.
    5.  **And** the response body should contain `Success: false`, `Message: "Một hoặc nhiều lỗi xác thực đã xảy ra."`, and `Errors` containing an error for `code` field with message "Mã môn học 'CS101' đã tồn tại."

#### 7.2. Get Subjects List (Xem danh sách Môn Học) - GET /api/v1/Subjects

**FR/BR/SRS Ref:** BRD PR#41, SRS PR#42, DEV PR#49 (`GetSubjectsQuery.cs`, `GetSubjectsQueryHandler.cs`, `GetSubjectsQueryValidator.cs`, `SubjectsController.cs`, `PaginatedList.cs`)

**QUAN-20260531-154643-TC04: Happy Path - Get paginated list of active subjects (Teacher Role)**
*   **Description:** Verify that a Teacher user can retrieve a paginated list of only active subjects by default.
*   **Test Data Pre-conditions:**
    *   User: `teacher_user` (`{teacher_token}`)
    *   Subjects: `CS101` (active), `MA201` (active), `PH301` (inactive), `BIO101` (active)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "GiaoVien" role (`{teacher_token}`).
    2.  **When** I send a GET request to `/api/v1/Subjects` with `PageIndex=1` and `PageSize=10` (without `IncludeInactive=true`).
    3.  **Then** the response status code should be `200 OK`.
    4.  **And** the response body should contain `Success: true`, `Data` with a `PaginatedList<SubjectDto>`.
    5.  **And** the `Items` in the `Data` list should only contain subjects where `IsActive` is `true` (e.g., `CS101`, `MA201`, `BIO101`), `PH301` should not be included.
    6.  **And** the `TotalCount` and `TotalPages` should reflect only the active subjects.

**QUAN-20260531-154643-TC05: Edge Case - Get paginated list including inactive subjects (Admin Role, Search & Sort)**
*   **Description:** Verify that an Admin user can retrieve a paginated list including inactive subjects, with search and sorting applied.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Subjects: `CS101` (active), `MA201` (active), `PH301` (inactive), `BIO101` (active), `DEL999` (deleted/inactive)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a GET request to `/api/v1/Subjects` with `PageIndex=1`, `PageSize=10`, `SearchQuery="Mon hoc"`, `SortBy="name"`, `SortOrder="desc"`, and `IncludeInactive=true`.
    3.  **Then** the response status code should be `200 OK`.
    4.  **And** the response body should contain `Success: true`, `Data` with a `PaginatedList<SubjectDto>`.
    5.  **And** the `Items` in the `Data` list should include all subjects (active, inactive, soft-deleted) that match the search query (e.g., `PH301`, `BIO101` if their names contain "Mon hoc").
    6.  **And** the list should be sorted by `Name` in descending order.
    7.  **And** the `TotalCount` and `TotalPages` should reflect all matching subjects, active and inactive.

**QUAN-20260531-154643-TC06: Negative Case - Get list with invalid sort field**
*   **Description:** Verify that requesting a list with an invalid sort field returns a bad request.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a GET request to `/api/v1/Subjects` with `SortBy="invalidField"`.
    3.  **Then** the response status code should be `400 Bad Request`.
    4.  **And** the response body should contain `Success: false` and `Errors` indicating an invalid `sortBy` field.

#### 7.3. Get Subject By ID (Xem chi tiết Môn Học) - GET /api/v1/Subjects/{id}

**FR/BR/SRS Ref:** BRD PR#41, SRS PR#42, DEV PR#49 (`GetSubjectByIdQuery.cs`, `GetSubjectByIdQueryHandler.cs`, `SubjectsController.cs`)

**QUAN-20260531-154643-TC07: Happy Path - Get details of an active subject (GiaoVien Role)**
*   **Description:** Verify that a Teacher user can retrieve details of an existing active subject.
*   **Test Data Pre-conditions:**
    *   User: `teacher_user` (`{teacher_token}`)
    *   Existing active subject: `CS101` (`{guid_s1}`)
*   **Test Steps (BDD):**
    1.  **Given** an active subject with ID `{guid_s1}` exists in the system.
    2.  **And** I am an authenticated user with "GiaoVien" role (`{teacher_token}`).
    3.  **When** I send a GET request to `/api/v1/Subjects/{guid_s1}`.
    4.  **Then** the response status code should be `200 OK`.
    5.  **And** the response body should contain `Success: true` and `Data` matching the details of subject `CS101`, including `Code`, `Name`, `Description`, `Credits`, `IsActive`, `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`.

**QUAN-20260531-154643-TC08: Negative Case - Get details of a non-existent subject**
*   **Description:** Verify that requesting details for a non-existent subject returns a 404 Not Found error.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Non-existent ID: `00000000-0000-0000-0000-000000000000`
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a GET request to `/api/v1/Subjects/00000000-0000-0000-0000-000000000000`.
    3.  **Then** the response status code should be `404 Not Found`.
    4.  **And** the response body should contain `Success: false` and `Message: "Môn học với ID '00000000-0000-0000-0000-000000000000' không tìm thấy."`

#### 7.4. Update Subject (Cập nhật Môn Học) - PUT /api/v1/Subjects/{id}

**FR/BR/SRS Ref:** BRD PR#41, SRS PR#42, DEV PR#49 (`UpdateSubjectCommand.cs`, `UpdateSubjectCommandHandler.cs`, `UpdateSubjectCommandValidator.cs`, `SubjectsController.cs`)

**QUAN-20260531-154643-TC09: Happy Path - Update all fields of an existing subject (Admin Role)**
*   **Description:** Verify that an Admin user can successfully update all modifiable fields of an existing subject.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Existing subject: `TEST_UPD` (`{guid_s_update}`)
    *   Update data: `Code="UPD101"`, `Name="Mon hoc da cap nhat"`, `Description="Mo ta moi"`, `Credits=4`, `IsActive=false`
*   **Test Steps (BDD):**
    1.  **Given** an existing subject with ID `{guid_s_update}` and current details.
    2.  **And** I am an authenticated user with "Admin" role (`{admin_token}`).
    3.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with the following JSON body:
        ```json
        {
          "id": "{guid_s_update}",
          "code": "UPD101",
          "name": "Mon hoc da cap nhat",
          "description": "Mo ta moi",
          "credits": 4,
          "isActive": false
        }
        ```
    4.  **Then** the response status code should be `200 OK`.
    5.  **And** the response body should contain `Success: true` and `Message: "Môn học được cập nhật thành công."`.
    6.  **And** I can retrieve the updated subject by its ID using GET `/api/v1/Subjects/{guid_s_update}`.
    7.  **And** the retrieved subject details reflect the updated data (Code, Name, Description, Credits, IsActive).
    8.  **And** the `UpdatedAt` field should be updated, and `UpdatedBy` should be `admin_user`.
    9.  **And** an audit log entry should be created with `ActionType="Update"`, `EntityName="Subject"`, `EntityId={guid_s_update}`, `UserName="admin_user"`, and `Changes` reflecting both old and new values.

**QUAN-20260531-154643-TC10: Edge Case - Update subject, only partial fields (ChuyenVienDaoTao Role)**
*   **Description:** Verify that a ChuyenVienDaoTao user can update only a subset of fields of an existing subject, leaving others unchanged.
*   **Test Data Pre-conditions:**
    *   User: `edu_staff` (`{edu_staff_token}`)
    *   Existing subject: `BIO101` (`{guid_s4}`) - currently active, credits 3, description "Mon hoc khoa hoc"
    *   Update data: `Name="Sinh hoc dai cuong (Cap nhat)"`, `Credits=5` (Code, Description, IsActive remain)
*   **Test Steps (BDD):**
    1.  **Given** an existing subject with ID `{guid_s4}` and current details: `Code="BIO101"`, `Name="Sinh hoc dai cuong"`, `Credits=3`, `IsActive=true`.
    2.  **And** I am an authenticated user with "ChuyenVienDaoTao" role (`{edu_staff_token}`).
    3.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s4}` with the following JSON body:
        ```json
        {
          "id": "{guid_s4}",
          "code": "BIO101", // Code remains unchanged
          "name": "Sinh hoc dai cuong (Cap nhat)",
          "description": "Mon hoc khoa hoc", // Description remains unchanged
          "credits": 5,
          "isActive": true // IsActive remains unchanged
        }
        ```
    4.  **Then** the response status code should be `200 OK`.
    5.  **And** the retrieved subject details reflect the updated `Name` and `Credits`, while `Code`, `Description`, `IsActive` remain as they were before the update.
    6.  **And** `UpdatedBy` should be `edu_staff`.
    7.  **And** an audit log entry should be created with `ActionType="Update"`, `EntityName="Subject"`, `EntityId={guid_s4}`, `UserName="edu_staff"`, and `Changes` reflecting both old and new values.

**QUAN-20260531-154643-TC11: Negative Case - Update subject with duplicate Code (of another subject)**
*   **Description:** Verify that updating a subject's code to an already existing code of *another* subject fails.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Subject to update: `TEST_UPD` (`{guid_s_update}`) - Code `TEST_UPD`
    *   Another existing subject: `CS101` (`{guid_s1}`) - Code `CS101`
    *   Update data: Set `TEST_UPD`'s code to `CS101`
*   **Test Steps (BDD):**
    1.  **Given** an existing subject with ID `{guid_s_update}` and Code `TEST_UPD`.
    2.  **And** another existing subject with Code `CS101`.
    3.  **And** I am an authenticated user with "Admin" role (`{admin_token}`).
    4.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with a JSON body containing `id={guid_s_update}` and `code="CS101"`.
    5.  **Then** the response status code should be `400 Bad Request`.
    6.  **And** the response body should contain `Success: false` and `Errors` indicating an error for `code` field with message "Mã môn học 'CS101' đã tồn tại.".
    7.  **And** the subject `{guid_s_update}` should remain unchanged.

**QUAN-20260531-154643-TC12: Negative Case - Update subject with ID mismatch in URL and body**
*   **Description:** Verify that updating a subject fails if the ID in the URL does not match the ID in the request body.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Subject to update (URL): `{guid_s_update}`
    *   Subject ID in body: `{guid_s1}` (different ID)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with a JSON body containing `id="{guid_s1}"` and valid update data.
    3.  **Then** the response status code should be `400 Bad Request`.
    4.  **And** the response body should contain `Success: false` and `Message: "ID trong URL không khớp với ID trong nội dung yêu cầu."`.

#### 7.5. Soft Delete Subject (Xóa mềm Môn Học) - DELETE /api/v1/Subjects/{id}

**FR/BR/SRS Ref:** BRD PR#41, SRS PR#42, DEV PR#49 (`DeleteSubjectCommand.cs`, `DeleteSubjectCommandHandler.cs`, `SubjectsController.cs`)

**QUAN-20260531-154643-TC13: Happy Path - Soft delete an existing subject (Admin Role)**
*   **Description:** Verify that an Admin user can successfully soft delete an existing subject.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Existing subject: `TEST_DEL` (`{guid_s_delete}`) - currently active, `IsDeleted=false`
*   **Test Steps (BDD):**
    1.  **Given** an existing subject with ID `{guid_s_delete}` that is `IsActive=true` and `IsDeleted=false`.
    2.  **And** I am an authenticated user with "Admin" role (`{admin_token}`).
    3.  **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}`.
    4.  **Then** the response status code should be `200 OK`.
    5.  **And** the response body should contain `Success: true` and `Message: "Môn học được xóa thành công."`.
    6.  **And** when retrieving the subject `{guid_s_delete}` via `GetSubjectById`, its `IsActive` should be `false` and `IsDeleted` should be `true`.
    7.  **And** when listing subjects (GET `/api/v1/Subjects`) without `IncludeInactive=true`, subject `{guid_s_delete}` should *not* be present.
    8.  **And** when listing subjects (GET `/api/v1/Subjects`) with `IncludeInactive=true` (by Admin/ChuyenVienDaoTao), subject `{guid_s_delete}` *should* be present with `IsActive=false`.
    9.  **And** an audit log entry should be created with `ActionType="SoftDelete"`, `EntityName="Subject"`, `EntityId={guid_s_delete}`, `UserName="admin_user"`, and `Changes` reflecting `OldIsActive=true`, `NewIsActive=false`, `IsDeleted=true`.

**QUAN-20260531-154643-TC14: Negative Case - Soft delete a non-existent subject**
*   **Description:** Verify that soft deleting a non-existent subject returns a 404 Not Found error.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Non-existent ID: `00000000-0000-0000-0000-000000000000`
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a DELETE request to `/api/v1/Subjects/00000000-0000-0000-0000-000000000000`.
    3.  **Then** the response status code should be `404 Not Found`.
    4.  **And** the response body should contain `Success: false` and `Message: "Entity \"Subject\" (00000000-0000-0000-0000-000000000000) was not found."`.

---

### 8. API Test Cases

(These API tests reiterate and provide more technical detail for some functional cases, specifically focusing on the HTTP aspects and expected JSON structures. Many functional tests already cover API interactions, so this section will focus on distinct API-level validations like header, error format, etc.)

#### 8.1. API Structure and Error Handling

**QUAN-20260531-154643-API01: API Response Format - Success**
*   **Description:** Verify all successful API responses conform to `ApiResponse<T>` structure.
*   **Test Data Pre-conditions:** Any valid API request (e.g., `QUAN-20260531-154643-TC01`).
*   **Test Steps (BDD):**
    1.  **Given** a successful API call to any subject endpoint (e.g., `POST /api/v1/Subjects`).
    2.  **When** the response is received.
    3.  **Then** the response JSON structure should have `success: true`, `message` (optional), and `data` (optional, for non-void responses).
    4.  **And** `errors` field should be `null` or absent.

**QUAN-20260531-154643-API02: API Response Format - Validation Error**
*   **Description:** Verify validation errors conform to `ApiResponse` structure with `Errors` array.
*   **Test Data Pre-conditions:** Any request triggering validation error (e.g., `QUAN-20260531-154643-TC03`).
*   **Test Steps (BDD):**
    1.  **Given** an API call that triggers a `ValidationException` (e.g., `POST /api/v1/Subjects` with duplicate code).
    2.  **When** the response is received.
    3.  **Then** the response status code should be `400 Bad Request`.
    4.  **And** the response JSON structure should have `success: false`, `message: "Một hoặc nhiều lỗi xác thực đã xảy ra."`.
    5.  **And** `errors` field should be an array of `ApiError` objects, each having `field` and `message` properties.

**QUAN-20260531-154643-API03: API Response Format - Not Found Error**
*   **Description:** Verify not found errors conform to `ApiResponse` structure.
*   **Test Data Pre-conditions:** Any request targeting a non-existent resource (e.g., `QUAN-20260531-154643-TC08`).
*   **Test Steps (BDD):**
    1.  **Given** an API call that triggers a `NotFoundException` (e.g., `GET /api/v1/Subjects/{non_existent_id}`).
    2.  **When** the response is received.
    3.  **Then** the response status code should be `404 Not Found`.
    4.  **And** the response JSON structure should have `success: false` and `message` describing the error.
    5.  **And** `errors` field should be `null` or absent.

#### 8.2. Audit Logging Verification

**QUAN-20260531-154643-API04: Audit Log - Create Operation**
*   **Description:** Verify audit log details for a subject creation.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   New subject data: `Code="AUDIT01"`, `Name="Mon Audit Create"`, `Credits=3`, `IsActive=true`
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I successfully create a new subject (e.g., `POST /api/v1/Subjects`).
    3.  **Then** an entry should be recorded in the `audit_log` table.
    4.  **And** the entry's `user_name` should be `admin_user`.
    5.  **And** the `action_type` should be "Create".
    6.  **And** the `entity_name` should be "Subject".
    7.  **And** the `entity_id` should match the newly created subject's ID.
    8.  **And** the `changes` (JSONB) field should contain a serialized object similar to `{"Code":"AUDIT01","Name":"Mon Audit Create","Credits":3,"IsActive":true}`.

**QUAN-20260531-154643-API05: Audit Log - Update Operation**
*   **Description:** Verify audit log details for a subject update.
*   **Test Data Pre-conditions:**
    *   User: `edu_staff` (`{edu_staff_token}`)
    *   Existing subject: `{guid_s_update}` (`Code="UPD101"`, `Name="Mon hoc da cap nhat"`, `Credits=4`, `IsActive=false`)
    *   Update data: `Name="Mon hoc da cap nhat lan 2"`, `Credits=5`, `IsActive=true`
*   **Test Steps (BDD):**
    1.  **Given** an existing subject `{guid_s_update}` with initial values.
    2.  **And** I am an authenticated user with "ChuyenVienDaoTao" role (`{edu_staff_token}`).
    3.  **When** I successfully update the subject `{guid_s_update}` (e.g., `PUT /api/v1/Subjects/{guid_s_update}`).
    4.  **Then** an entry should be recorded in the `audit_log` table.
    5.  **And** the entry's `user_name` should be `edu_staff`.
    6.  **And** the `action_type` should be "Update".
    7.  **And** the `entity_id` should match `{guid_s_update}`.
    8.  **And** the `changes` (JSONB) field should contain a structure like `{"OldValues":{...old_subject_data...},"NewValues":{...new_subject_data...}}`, accurately reflecting the changes made.

**QUAN-20260531-154643-API06: Audit Log - Soft Delete Operation**
*   **Description:** Verify audit log details for a subject soft deletion.
*   **Test Data Pre-conditions:**
    *   User: `admin_user` (`{admin_token}`)
    *   Existing subject: `{guid_s_delete}` (`Code="TEST_DEL"`, `IsActive=true`, `IsDeleted=false`)
*   **Test Steps (BDD):**
    1.  **Given** an existing subject `{guid_s_delete}` that is active and not deleted.
    2.  **And** I am an authenticated user with "Admin" role (`{admin_token}`).
    3.  **When** I successfully soft delete the subject `{guid_s_delete}` (e.g., `DELETE /api/v1/Subjects/{guid_s_delete}`).
    4.  **Then** an entry should be recorded in the `audit_log` table.
    5.  **And** the entry's `user_name` should be `admin_user`.
    6.  **And** the `action_type` should be "SoftDelete".
    7.  **And** the `entity_id` should match `{guid_s_delete}`.
    8.  **And** the `changes` (JSONB) field should contain a serialized object similar to `{"OldIsActive":true,"NewIsActive":false,"IsDeleted":true}`.

---

### 9. Security Test Cases

**FR/BR/SRS Ref:** SRS PR#42 (Authorization policies in `Program.cs`), DEV PR#49 (`SubjectsController.cs` - `[Authorize]` attributes)

**QUAN-20260531-154643-SEC01: Authorization - Create Subject (POST)**
*   **Description:** Verify role-based access for creating a subject.
*   **Test Data Pre-conditions:** Valid subject creation data.
*   **Test Steps (BDD):**
    1.  **Given** a valid subject creation request.
    2.  **When** I send the request with `{admin_token}`.
    3.  **Then** the response status code should be `201 Created`.
    4.  **When** I send the request with `{edu_staff_token}`.
    5.  **Then** the response status code should be `201 Created`.
    6.  **When** I send the request with `{teacher_token}`.
    7.  **Then** the response status code should be `403 Forbidden`.
    8.  **When** I send the request with `{normal_token}`.
    9.  **Then** the response status code should be `403 Forbidden`.
    10. **When** I send the request with `No Token`.
    11. **Then** the response status code should be `401 Unauthorized`.

**QUAN-20260531-154643-SEC02: Authorization - Get Subjects List (GET)**
*   **Description:** Verify role-based access for retrieving the list of subjects.
*   **Test Data Pre-conditions:** Existing active and inactive subjects.
*   **Test Steps (BDD):**
    1.  **Given** a valid request to get subjects list.
    2.  **When** I send the request with `{admin_token}`.
    3.  **Then** the response status code should be `200 OK`.
    4.  **When** I send the request with `{edu_staff_token}`.
    5.  **Then** the response status code should be `200 OK`.
    6.  **When** I send the request with `{teacher_token}`.
    7.  **Then** the response status code should be `200 OK`.
    8.  **When** I send the request with `{normal_token}`.
    9.  **Then** the response status code should be `403 Forbidden`.
    10. **When** I send the request with `No Token`.
    11. **Then** the response status code should be `401 Unauthorized`.

**QUAN-20260531-154643-SEC03: Authorization - Get Subject By ID (GET)**
*   **Description:** Verify role-based access for retrieving a specific subject's details.
*   **Test Data Pre-conditions:** Existing subject ID (`{guid_s1}`).
*   **Test Steps (BDD):**
    1.  **Given** an existing subject ID `{guid_s1}`.
    2.  **When** I send a GET request to `/api/v1/Subjects/{guid_s1}` with `{admin_token}`.
    3.  **Then** the response status code should be `200 OK`.
    4.  **When** I send a GET request to `/api/v1/Subjects/{guid_s1}` with `{edu_staff_token}`.
    5.  **Then** the response status code should be `200 OK`.
    6.  **When** I send a GET request to `/api/v1/Subjects/{guid_s1}` with `{teacher_token}`.
    7.  **Then** the response status code should be `200 OK`.
    8.  **When** I send a GET request to `/api/v1/Subjects/{guid_s1}` with `{normal_token}`.
    9.  **Then** the response status code should be `403 Forbidden`.
    10. **When** I send a GET request to `/api/v1/Subjects/{guid_s1}` with `No Token`.
    11. **Then** the response status code should be `401 Unauthorized`.

**QUAN-20260531-154643-SEC04: Authorization - Update Subject (PUT)**
*   **Description:** Verify role-based access for updating a subject.
*   **Test Data Pre-conditions:** Existing subject `{guid_s_update}` and valid update data.
*   **Test Steps (BDD):**
    1.  **Given** an existing subject `{guid_s_update}` and valid update data.
    2.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with `{admin_token}`.
    3.  **Then** the response status code should be `200 OK`.
    4.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with `{edu_staff_token}`.
    5.  **Then** the response status code should be `200 OK`.
    6.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with `{teacher_token}`.
    7.  **Then** the response status code should be `403 Forbidden`.
    8.  **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with `{normal_token}`.
    9.  **Then** the response status code should be `403 Forbidden`.
    10. **When** I send a PUT request to `/api/v1/Subjects/{guid_s_update}` with `No Token`.
    11. **Then** the response status code should be `401 Unauthorized`.

**QUAN-20260531-154643-SEC05: Authorization - Soft Delete Subject (DELETE)**
*   **Description:** Verify role-based access for soft deleting a subject.
*   **Test Data Pre-conditions:** Existing subject `{guid_s_delete}`.
*   **Test Steps (BDD):**
    1.  **Given** an existing subject ID `{guid_s_delete}`.
    2.  **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}` with `{admin_token}`.
    3.  **Then** the response status code should be `200 OK`.
    4.  **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}` with `{edu_staff_token}`.
    5.  **Then** the response status code should be `200 OK`.
    6.  **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}` with `{teacher_token}`.
    7.  **Then** the response status code should be `403 Forbidden`.
    8.  **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}` with `{normal_token}`.
    9.  **Then** the response status code should be `403 Forbidden`.
    10. **When** I send a DELETE request to `/api/v1/Subjects/{guid_s_delete}` with `No Token`.
    11. **Then** the response status code should be `401 Unauthorized`.

---

### 10. Boundary/Validation Test Cases

**FR/BR/SRS Ref:** SRS PR#42, DEV PR#49 (`CreateSubjectCommandValidator.cs`, `UpdateSubjectCommandValidator.cs`, `GetSubjectsQueryValidator.cs`)

**QUAN-20260531-154643-BV01: Validation - Create/Update Subject (Code field)**
*   **Description:** Verify validation rules for the `Code` field.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** a valid subject creation/update request.
    2.  **When** the `Code` field is empty (`""`).
    3.  **Then** the response status code should be `400 Bad Request` with error "Mã môn học không được để trống."
    4.  **When** the `Code` field exceeds 20 characters (e.g., `"TOO_LONG_CODE_1234567890"`).
    5.  **Then** the response status code should be `400 Bad Request` with error "Mã môn học không được vượt quá 20 ký tự."

**QUAN-20260531-154643-BV02: Validation - Create/Update Subject (Name field)**
*   **Description:** Verify validation rules for the `Name` field.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** a valid subject creation/update request.
    2.  **When** the `Name` field is empty (`""`).
    3.  **Then** the response status code should be `400 Bad Request` with error "Tên môn học không được để trống."
    4.  **When** the `Name` field exceeds 100 characters.
    5.  **Then** the response status code should be `400 Bad Request` with error "Tên môn học không được vượt quá 100 ký tự."

**QUAN-20260531-154643-BV03: Validation - Create/Update Subject (Description field)**
*   **Description:** Verify validation rules for the `Description` field.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** a valid subject creation/update request.
    2.  **When** the `Description` field exceeds 500 characters.
    3.  **Then** the response status code should be `400 Bad Request` with error "Mô tả không được vượt quá 500 ký tự."
    4.  **When** the `Description` field is `null` or empty (valid).
    5.  **Then** the request should be successful (`201 Created` or `200 OK`).

**QUAN-20260531-154643-BV04: Validation - Create/Update Subject (Credits field)**
*   **Description:** Verify validation rules for the `Credits` field.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** a valid subject creation/update request.
    2.  **When** the `Credits` field is `0`.
    3.  **Then** the response status code should be `400 Bad Request` with error "Số tín chỉ phải là số nguyên dương từ 1 đến 10."
    4.  **When** the `Credits` field is `11`.
    5.  **Then** the response status code should be `400 Bad Request` with error "Số tín chỉ phải là số nguyên dương từ 1 đến 10."
    6.  **When** the `Credits` field is `1` (min valid).
    7.  **Then** the request should be successful (`201 Created` or `200 OK`).
    8.  **When** the `Credits` field is `10` (max valid).
    9.  **Then** the request should be successful (`201 Created` or `200 OK`).

**QUAN-20260531-154643-BV05: Validation - Get Subjects Query (Pagination & Sorting)**
*   **Description:** Verify validation rules for `PageIndex`, `PageSize`, `SortBy`, `SortOrder` in Get Subjects Query.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** a request to GET `/api/v1/Subjects`.
    2.  **When** `PageIndex` is `0`.
    3.  **Then** the response status code should be `400 Bad Request` with error "Số trang (pageIndex) phải lớn hơn hoặc bằng 1."
    4.  **When** `PageSize` is `0`.
    5.  **Then** the response status code should be `400 Bad Request` with error "Kích thước trang (pageSize) phải nằm trong khoảng từ 1 đến 100."
    6.  **When** `PageSize` is `101`.
    7.  **Then** the response status code should be `400 Bad Request` with error "Kích thước trang (pageSize) phải nằm trong khoảng từ 1 đến 100."
    8.  **When** `SortBy` is `invalid_field`.
    9.  **Then** the response status code should be `400 Bad Request` with error "Trường sắp xếp (sortBy) không hợp lệ."
    10. **When** `SortOrder` is `invalid_order`.
    11. **Then** the response status code should be `400 Bad Request` with error "Thứ tự sắp xếp (sortOrder) không hợp lệ."

**QUAN-20260531-154643-BV06: Validation - Invalid GUID Format for ID (Get, Update, Delete)**
*   **Description:** Verify that endpoints expecting a GUID for ID handle invalid formats correctly.
*   **Test Data Pre-conditions:** User: `admin_user` (`{admin_token}`)
*   **Test Steps (BDD):**
    1.  **Given** I am an authenticated user with "Admin" role (`{admin_token}`).
    2.  **When** I send a GET request to `/api/v1/Subjects/invalid-guid-format`.
    3.  **Then** the response status code should be `400 Bad Request` (from model binding/routing).
    4.  **When** I send a PUT request to `/api/v1/Subjects/invalid-guid-format` with a valid body containing a valid GUID for `id`.
    5.  **Then** the response status code should be `400 Bad Request`.
    6.  **When** I send a DELETE request to `/api/v1/Subjects/invalid-guid-format`.
    7.  **Then** the response status code should be `400 Bad Request`.

---

### 11. Traceability Matrix

| Requirement ID (BR/SRS) | Description | Test Case ID(s) | Type | Status |
| :---------------------- | :---------- | :-------------- | :--- | :----- |
| FR-SUBJECT-001 | Cho phép tạo mới môn học với Code, Name, Description, Credits, IsActive. | QUAN-20260531-154643-TC01, QUAN-20260531-154643-TC02 | F | New |
| FR-SUBJECT-002 | Mã môn học là duy nhất. | QUAN-20260531-154643-TC03, QUAN-20260531-154643-TC11, QUAN-20260531-154643-BV01 | F, BV | New |
| FR-SUBJECT-003 | Tên môn học là duy nhất. | QUAN-20260531-154643-TC03 (implied), QUAN-20260531-154643-TC11 (implied), QUAN-20260531-154643-BV02 | F, BV | New |
| FR-SUBJECT-004 | Mã môn học tối đa 20 ký tự. | QUAN-20260531-154643-BV01 | BV | New |
| FR-SUBJECT-005 | Tên môn học tối đa 100 ký tự. | QUAN-20260531-154643-BV02 | BV | New |
| FR-SUBJECT-006 | Mô tả môn học tối đa 500 ký tự. | QUAN-20260531-154643-BV03 | BV | New |
| FR-SUBJECT-007 | Số tín chỉ từ 1 đến 10. | QUAN-20260531-154643-BV04 | BV | New |
| FR-SUBJECT-008 | Mặc định `IsActive=true` khi tạo mới. | QUAN-20260531-154643-TC02 | F | New |
| FR-SUBJECT-009 | Cho phép lấy danh sách môn học có phân trang, tìm kiếm, sắp xếp. | QUAN-20260531-154643-TC04, QUAN-20260531-154643-TC05 | F | New |
| FR-SUBJECT-010 | Mặc định chỉ hiển thị môn học `IsActive=true` cho người dùng không có quyền quản trị/chuyên viên. | QUAN-20260531-154643-TC04, QUAN-20260531-154643-SEC02 | F, SEC | New |
| FR-SUBJECT-011 | Admin/ChuyenVienDaoTao có thể xem môn học không hoạt động (`IncludeInactive=true`). | QUAN-20260531-154643-TC05 | F | New |
| FR-SUBJECT-012 | Hỗ trợ tìm kiếm theo Code hoặc Name. | QUAN-20260531-154643-TC05 | F | New |
| FR-SUBJECT-013 | Hỗ trợ sắp xếp theo Code, Name, Credits, CreatedAt (asc/desc). | QUAN-20260531-154643-TC05, QUAN-20260531-154643-TC06, QUAN-20260531-154643-BV05 | F, BV | New |
| FR-SUBJECT-014 | `PageIndex` >= 1, `PageSize` từ 1 đến 100. | QUAN-20260531-154643-BV05 | BV | New |
| FR-SUBJECT-015 | Cho phép lấy thông tin chi tiết môn học theo ID. | QUAN-20260531-154643-TC07 | F | New |
| FR-SUBJECT-016 | Xử lý khi không tìm thấy môn học theo ID (404 Not Found). | QUAN-20260531-154643-TC08, QUAN-20260531-154643-TC14, QUAN-20260531-154643-API03 | F, API | New |
| FR-SUBJECT-017 | Cho phép cập nhật thông tin môn học. | QUAN-20260531-154643-TC09, QUAN-20260531-154643-TC10 | F | New |
| FR-SUBJECT-018 | Xử lý trường hợp ID trong URL không khớp ID trong request body. | QUAN-20260531-154643-TC12 | F | New |
| FR-SUBJECT-019 | Cho phép xóa mềm môn học (`IsActive=false`, `IsDeleted=true`). | QUAN-20260531-154643-TC13 | F | New |
| FR-AUTH-001 | Tạo, Cập nhật, Xóa môn học yêu cầu vai trò "Admin" hoặc "ChuyenVienDaoTao". | QUAN-20260531-154643-SEC01, QUAN-20260531-154643-SEC04, QUAN-20260531-154643-SEC05 | SEC | New |
| FR-AUTH-002 | Xem danh sách/chi tiết môn học yêu cầu vai trò "Admin", "ChuyenVienDaoTao" hoặc "GiaoVien". | QUAN-20260531-154643-SEC02, QUAN-20260531-154643-SEC03 | SEC | New |
| FR-AUTH-003 | Người dùng không xác thực bị từ chối truy cập (401 Unauthorized). | QUAN-20260531-154643-SEC01, QUAN-20260531-154643-SEC02, QUAN-20260531-154643-SEC03, QUAN-20260531-154643-SEC04, QUAN-20260531-154643-SEC05 | SEC | New |
| FR-AUDIT-001 | Ghi audit log cho các thao tác Tạo, Cập nhật, Xóa mềm môn học. | QUAN-20260531-154643-TC01, QUAN-20260531-154643-TC02, QUAN-20260531-154643-TC09, QUAN-20260531-154643-TC10, QUAN-20260531-154643-TC13, QUAN-20260531-154643-API04, QUAN-20260531-154643-API05, QUAN-20260531-154643-API06 | API | New |
| SR-API-001 | Tất cả phản hồi API theo cấu trúc `ApiResponse`. | QUAN-20260531-154643-API01, QUAN-20260531-154643-API02, QUAN-20260531-154643-API03 | API | New |
| SR-API-002 | Xử lý lỗi xác thực chung (`ValidationException`) trả về 400. | QUAN-20260531-154643-TC03, QUAN-20260531-154643-TC06, QUAN-20260531-154643-TC11, QUAN-20260531-154643-BV01, QUAN-20260531-154643-BV02, QUAN-20260531-154643-BV03, QUAN-20260531-154643-BV04, QUAN-20260531-154643-BV05, QUAN-20260531-154643-API02 | F, API, BV | New |
| SR-API-003 | Xử lý lỗi không tìm thấy (`NotFoundException`) trả về 404. | QUAN-20260531-154643-TC08, QUAN-20260531-154643-TC14, QUAN-20260531-154643-API03 | F, API | New |
| SR-DB-001 | Cơ chế Soft Delete thông qua `IsDeleted` và Global Query Filter. | QUAN-20260531-154643-TC13 | F | New |
| SR-DB-002 | `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy` được tự động cập nhật. | QUAN-20260531-154643-TC01, QUAN-20260531-154643-TC09 | F | New |
| SR-DB-003 | `ICurrentUser` sử dụng `UserName` để ghi `CreatedBy`/`UpdatedBy`. | QUAN-20260531-154643-TC01, QUAN-20260531-154643-TC09 | F | New |
| SR-DB-004 | Optimistic concurrency (`xmin`) được cấu hình cho Subject. | N/A (Internal, không kiểm thử trực tiếp qua API, nhưng cần kiểm tra trong DB schema) | N/A | New |

---