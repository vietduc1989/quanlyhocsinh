# Test Specification: Quản lý Thông tin Học sinh

**Feature ID:** QUAN-20260604-153038
**Feature Name:** Quản lý Thông tin Học sinh
**BRD PR#:** 65
**SRS PR#:** 66
**DEV PR#:** 67

## 1. Test Data Pre-conditions

To ensure comprehensive testing, the following data must be prepared in the database before executing test cases.

**1.1. User Accounts:**
*   **Admin User:**
    *   Username: `admin_user`
    *   Password: `password123`
    *   Roles: `Admin`
    *   JWT Token: `[Generated_Admin_JWT_Token]` (to be obtained via login API)
*   **Non-Admin User:** (If authorization matrix includes non-admin roles)
    *   Username: `teacher_user`
    *   Password: `password123`
    *   Roles: `Teacher`
    *   JWT Token: `[Generated_Teacher_JWT_Token]` (to be obtained via login API)

**1.2. LopHoc (Classes):**
*   **Lop A:**
    *   `Id`: `UUID_LOP_A`
    *   `TenLop`: "10A1"
    *   `Khoi`: "10"
    *   `NamHoc`: 2024
*   **Lop B:**
    *   `Id`: `UUID_LOP_B`
    *   `TenLop`: "11B2"
    *   `Khoi`: "11"
    *   `NamHoc`: 2024
*   **Lop C:**
    *   `Id`: `UUID_LOP_C`
    *   `TenLop`: "12C3"
    *   `Khoi`: "12"
    *   `NamHoc`: 2024
*   **Lop Khong Ton Tai (Non-existent class ID):** `00000000-0000-0000-0000-000000000000` (or any non-existent valid GUID format)

**1.3. HocSinh (Students):**

*   **HocSinh 1 (Active, Lop A):**
    *   `Id`: `UUID_HS_01`
    *   `HoTen`: "Nguyen Van A"
    *   `NgaySinh`: "2008-01-15"
    *   `GioiTinh`: "Nam"
    *   `DiaChi`: "123 Duong ABC, Quan 1, TP HCM"
    *   `SoDienThoaiPH`: "0901234567"
    *   `EmailPH`: "ph.nguyenvana@example.com"
    *   `LopHocId`: `UUID_LOP_A`
    *   `TrangThai`: "DangHoc"
    *   `MaHocSinh`: (Auto-generated, e.g., "HS-xxxxxx")
    *   `IsDeleted`: false

*   **HocSinh 2 (Active, Lop B):**
    *   `Id`: `UUID_HS_02`
    *   `HoTen`: "Tran Thi B"
    *   `NgaySinh`: "2007-03-20"
    *   `GioiTinh`: "Nu"
    *   `LopHocId`: `UUID_LOP_B`
    *   `TrangThai`: "DangHoc"
    *   `MaHocSinh`: (Auto-generated, e.g., "HS-yyyyyy")
    *   `IsDeleted`: false

*   **HocSinh 3 (Graduated, Lop A):**
    *   `Id`: `UUID_HS_03`
    *   `HoTen`: "Le Van C"
    *   `NgaySinh`: "2006-05-10"
    *   `GioiTinh`: "Nam"
    *   `LopHocId`: `UUID_LOP_A`
    *   `TrangThai`: "DaTotNghiep"
    *   `MaHocSinh`: (Auto-generated, e.g., "HS-zzzzzz")
    *   `IsDeleted`: false

*   **HocSinh 4 (Related data, Lop B):**
    *   `Id`: `UUID_HS_04`
    *   `HoTen`: "Pham Thi D"
    *   `NgaySinh`: "2007-09-01"
    *   `GioiTinh`: "Nu"
    *   `LopHocId`: `UUID_LOP_B`
    *   `TrangThai`: "DangHoc"
    *   `MaHocSinh`: (Auto-generated, e.g., "HS-aaaaaa")
    *   `IsDeleted`: false
    *   **Related HocSinhDiem:**
        *   `Id`: `UUID_DIEM_01`
        *   `HocSinhId`: `UUID_HS_04`
        *   `MonHoc`: "Toan"
        *   `DiemSo`: 8.5
        *   `HocKy`: 1
        *   `NamHoc`: 2024
        *   `IsDeleted`: false

*   **HocSinh 5 (Soft Deleted, Lop C):**
    *   `Id`: `UUID_HS_05`
    *   `HoTen`: "Hoang Minh E"
    *   `NgaySinh`: "2005-11-25"
    *   `GioiTinh`: "Nam"
    *   `LopHocId`: `UUID_LOP_C`
    *   `TrangThai`: "DaChuyenTruong"
    *   `MaHocSinh`: (Auto-generated, e.g., "HS-bbbbbb")
    *   `IsDeleted`: true

**1.4. Non-existent HocSinh ID:** `00000000-0000-0000-0000-000000000000` (or any non-existent valid GUID)

---

## 2. Test Cases

### 2.1. Functional Tests

**QUAN-20260604-153038-TC01: Create HocSinh - Happy Path (FR03, BR02, BR03, BR04, BR05, BR06)**

*   **Description:** Verify that a new student can be successfully created with all valid required and optional fields.
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint for creating a student `/api/hocsinh`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a POST request is sent with the following body:
        ```json
        {
          "hoTen": "Nguyen Thi Thuy",
          "ngaySinh": "2009-02-28",
          "gioiTinh": "Nu",
          "diaChi": "456 Duong XYZ, Quan 3, TP HCM",
          "soDienThoaiPH": "0987654321",
          "emailPH": "ph.nguyenthuy@example.com",
          "lopHocId": "UUID_LOP_A",
          "trangThai": "DangHoc"
        }
        ```
    4.  **Then** the response status code should be `201 Created`
    5.  **And** the response body should contain `Success: true`
    6.  **And** the `data` in the response should contain the newly created `HocSinhDto` with:
        *   `id` is a valid GUID
        *   `maHocSinh` is auto-generated and unique (e.g., "HS-xxxxxx")
        *   All provided fields match the request
        *   `createdAt` is recent, `createdBy` is "AdminUser"
    7.  **And** a subsequent GET request to `/api/hocsinh/{new_hoc_sinh_id}` should return the created student.

**QUAN-20260604-153038-TC02: Get HocSinh By ID - Happy Path (FR02)**

*   **Description:** Verify that a specific student's details can be retrieved using their ID.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HS_01` exists in the database.
*   **Test Steps:**
    1.  **Given** the API endpoint for retrieving a student by ID `/api/hocsinh/{UUID_HS_01}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a GET request is sent
    4.  **Then** the response status code should be `200 OK`
    5.  **And** the response body should contain `Success: true`
    6.  **And** the `data` in the response should match the details of "Nguyen Van A" with `id = UUID_HS_01`, `tenLop` as "10A1", `gioiTinh` and `trangThai` as string representations.

**QUAN-20260604-153038-TC03: Update HocSinh - Happy Path (FR04, BR02, BR03, BR04, BR05, BR06)**

*   **Description:** Verify that an existing student's information can be successfully updated (excluding `MaHocSinh`).
*   **Pre-conditions:** Admin user is authenticated. `UUID_HS_01` exists and is active.
*   **Test Steps:**
    1.  **Given** the API endpoint for updating a student `/api/hocsinh/{UUID_HS_01}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a PUT request is sent with the following body to update name, address, phone, and status:
        ```json
        {
          "id": "UUID_HS_01",
          "hoTen": "Nguyen Van A Updated",
          "ngaySinh": "2008-01-15",
          "gioiTinh": "Nam",
          "diaChi": "789 Duong PQR, Quan 5, TP HCM",
          "soDienThoaiPH": "0912345678",
          "emailPH": "ph.nguyenvana.updated@example.com",
          "lopHocId": "UUID_LOP_B", // Change class
          "trangThai": "TamDung" // Change status
        }
        ```
    4.  **Then** the response status code should be `200 OK`
    5.  **And** the response body should contain `Success: true`
    6.  **And** the `data` in the response should contain the updated `HocSinhDto` with:
        *   `hoTen`, `diaChi`, `soDienThoaiPH`, `emailPH`, `lopHocId`, `trangThai` updated
        *   `maHocSinh` remaining unchanged (BR01)
        *   `updatedAt` is recent, `updatedBy` is "AdminUser"
    7.  **And** a subsequent GET request to `/api/hocsinh/{UUID_HS_01}` should reflect the updated information.

**QUAN-20260604-153038-TC04: Delete HocSinh - Happy Path (FR05)**

*   **Description:** Verify that a student without active related data can be soft-deleted.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HS_02` exists and has no `HocSinhDiem` entries.
*   **Test Steps:**
    1.  **Given** the API endpoint for deleting a student `/api/hocsinh/{UUID_HS_02}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a DELETE request is sent
    4.  **Then** the response status code should be `200 OK`
    5.  **And** the response body should contain `Success: true` and a success message.
    6.  **And** a subsequent GET request to `/api/hocsinh/{UUID_HS_02}` (using a mechanism to bypass global filter if possible, or direct database check) should show `IsDeleted = true`.
    7.  **And** a subsequent GET request to `/api/hocsinh` (list endpoint) should NOT include `UUID_HS_02` in the results, demonstrating the global soft-delete filter.

**QUAN-20260604-153038-TC05: Get All HocSinhs - Happy Path (FR07, FR08)**

*   **Description:** Verify that a paginated list of active students can be retrieved without any search/filter criteria.
*   **Pre-conditions:** Admin user is authenticated. Multiple active students exist (`UUID_HS_01`, `UUID_HS_02`, `UUID_HS_04`).
*   **Test Steps:**
    1.  **Given** the API endpoint for listing students `/api/hocsinh`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a GET request is sent with `pageNumber=1` and `pageSize=2`
    4.  **Then** the response status code should be `200 OK`
    5.  **And** the response body should contain `Success: true`
    6.  **And** the `data` in the response should be a `PaginatedList<HocSinhDto>` with:
        *   `pageNumber = 1`, `pageSize = 2` (or the requested page size)
        *   `totalCount` reflecting the total number of *active* students (e.g., 3 if 3 active students exist initially)
        *   `totalPages` calculated correctly
        *   `items` containing 2 student records, sorted by `MaHocSinh` (FR01).
        *   None of the soft-deleted students (e.g., `UUID_HS_05`) should be present.

**QUAN-20260604-153038-TC06: Get LopHocs for Lookup - Happy Path (FR07 implied)**

*   **Description:** Verify that a list of LopHocs can be retrieved for use in filter/dropdowns.
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A`, `UUID_LOP_B`, `UUID_LOP_C` exist.
*   **Test Steps:**
    1.  **Given** the API endpoint for LopHoc lookup `/api/hocsinh/lop-hocs-lookup`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a GET request is sent
    4.  **Then** the response status code should be `200 OK`
    5.  **And** the response body should contain `Success: true`
    6.  **And** the `data` in the response should be an `IReadOnlyList<LopHocLookupDto>` containing:
        *   `UUID_LOP_A` with `TenLop: "10A1"`
        *   `UUID_LOP_B` with `TenLop: "11B2"`
        *   `UUID_LOP_C` with `TenLop: "12C3"`
        *   Sorted by `TenLop`.

### 2.2. API Tests

**QUAN-20260604-153038-API01: API Endpoint Availability and Response Format**

*   **Description:** Verify that all API endpoints are accessible and return `ApiResponse<T>` as expected.
*   **Pre-conditions:** Admin user is authenticated.
*   **Test Steps:**
    1.  **Given** the base API URL `/api/hocsinh`
    2.  **When** sending a GET request to `/api/hocsinh`
    3.  **Then** the response should be a JSON object conforming to `ApiResponse<PaginatedList<HocSinhDto>>`.
    4.  **When** sending a GET request to `/api/hocsinh/{UUID_HS_01}`
    5.  **Then** the response should be a JSON object conforming to `ApiResponse<HocSinhDto>`.
    6.  **When** sending a POST request to `/api/hocsinh` (with valid data, similar to TC01)
    7.  **Then** the response should be a JSON object conforming to `ApiResponse<HocSinhDto>`.
    8.  **When** sending a PUT request to `/api/hocsinh/{UUID_HS_01}` (with valid data, similar to TC03)
    9.  **Then** the response should be a JSON object conforming to `ApiResponse<HocSinhDto>`.
    10. **When** sending a DELETE request to `/api/hocsinh/{UUID_HS_02}` (ensure no related data first)
    11. **Then** the response should be a JSON object conforming to `ApiResponse<object>` (or `ApiResponse<null>`).
    12. **When** sending a GET request to `/api/hocsinh/lop-hocs-lookup`
    13. **Then** the response should be a JSON object conforming to `ApiResponse<IReadOnlyList<LopHocLookupDto>>`.

### 2.3. Security Tests

**QUAN-20260604-153038-SEC01: Authorization - Admin Role Required (NFR: Security)**

*   **Description:** Verify that all CRUD operations on `HocSinh` require an 'Admin' role.
*   **Pre-conditions:** Non-admin user (`teacher_user`) is authenticated. Admin user token also available.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh`
    2.  **And** a valid JWT token for a Non-Admin user (e.g., `teacher_user`)
    3.  **When** a POST request (Create) is sent with valid student data
    4.  **Then** the response status code should be `403 Forbidden`
    5.  **When** a PUT request (Update) is sent to `/api/hocsinh/{UUID_HS_01}` with valid student data
    6.  **Then** the response status code should be `403 Forbidden`
    7.  **When** a DELETE request is sent to `/api/hocsinh/{UUID_HS_02}`
    8.  **Then** the response status code should be `403 Forbidden`
    9.  **When** a GET request to `/api/hocsinh` (list) is sent
    10. **Then** the response status code should be `403 Forbidden`
    11. **When** a GET request to `/api/hocsinh/{UUID_HS_01}` (detail) is sent
    12. **Then** the response status code should be `403 Forbidden`

**QUAN-20260604-153038-SEC02: Authentication - Unauthorized Access**

*   **Description:** Verify that unauthenticated requests are rejected.
*   **Pre-conditions:** None.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh`
    2.  **When** a POST request (Create) is sent with valid student data and NO JWT token
    3.  **Then** the response status code should be `401 Unauthorized`
    4.  **When** a GET request to `/api/hocsinh` is sent with NO JWT token
    5.  **Then** the response status code should be `401 Unauthorized`

### 2.4. Boundary/Validation Tests

**QUAN-20260604-153038-BV01: Create HocSinh - Required Fields Validation (BR02)**

*   **Description:** Verify that creation fails if any required field is missing or empty.
*   **Pre-conditions:** Admin user is authenticated.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `hoTen` as empty string
    3.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Họ và Tên không được để trống."
    4.  **When** a POST request is sent with `ngaySinh` missing
    5.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Ngày Sinh không được để trống."
    6.  **When** a POST request is sent with `gioiTinh` missing/invalid enum (e.g., "InvalidGender")
    7.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Giới Tính không hợp lệ."
    8.  **When** a POST request is sent with `lopHocId` missing
    9.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Lớp Học không được để trống."
    10. **When** a POST request is sent with `trangThai` missing/invalid enum (e.g., "InvalidStatus")
    11. **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Trạng Thái không hợp lệ."

**QUAN-20260604-153038-BV02: Create/Update HocSinh - NgaySinh Validation (BR03)**

*   **Description:** Verify `NgaySinh` validation (valid date, not in future).
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `ngaySinh`: "invalid-date-format"
    3.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Ngày Sinh không hợp lệ."
    4.  **When** a POST request is sent with `ngaySinh`: `(today + 1 day)` (e.g., "2025-06-05" if today is "2025-06-04")
    5.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại."
    6.  **Given** the API endpoint `/api/hocsinh/{UUID_HS_01}` and an Admin JWT token
    7.  **When** a PUT request is sent with `ngaySinh`: `(today + 1 day)`
    8.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại."

**QUAN-20260604-153038-BV03: Create/Update HocSinh - SoDienThoaiPH Validation (BR04)**

*   **Description:** Verify `SoDienThoaiPH` format validation.
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `soDienThoaiPH`: "123" (too short)
    3.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Số Điện Thoại Phụ Huynh không đúng định dạng."
    4.  **When** a POST request is sent with `soDienThoaiPH`: "090123456789" (too long)
    5.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Số Điện Thoại Phụ Huynh không đúng định dạng."
    6.  **When** a POST request is sent with `soDienThoaiPH`: "notaphone"
    7.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Số Điện Thoại Phụ Huynh không đúng định dạng."
    8.  **When** a POST request is sent with `soDienThoaiPH`: `null` or empty string
    9.  **Then** the response status code should be `201 Created` (optional field).

**QUAN-20260604-153038-BV04: Create/Update HocSinh - EmailPH Validation (BR05)**

*   **Description:** Verify `EmailPH` format validation.
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `emailPH`: "invalid-email"
    3.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Email Phụ Huynh không đúng định dạng."
    4.  **When** a POST request is sent with `emailPH`: "test@."
    5.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Email Phụ Huynh không đúng định dạng."
    6.  **When** a POST request is sent with `emailPH`: `null` or empty string
    7.  **Then** the response status code should be `201 Created` (optional field).

**QUAN-20260604-153038-BV05: Create/Update HocSinh - LopHocId and TrangThai Validation (BR02, BR06)**

*   **Description:** Verify that `LopHocId` must exist and `TrangThai` must be a valid predefined enum value.
*   **Pre-conditions:** Admin user is authenticated.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `lopHocId`: `UUID_LOP_KHONG_TON_TAI`
    3.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Lớp Học không tồn tại."
    4.  **When** a POST request is sent with `trangThai`: "NonExistentStatus"
    5.  **Then** the response status code should be `400 Bad Request` and `Errors` should contain "Trạng Thái không hợp lệ."
    6.  **When** a POST request is sent with `trangThai`: "DaChuyenTruong" (valid enum value, BR06)
    7.  **Then** the response status code should be `201 Created`.

**QUAN-20260604-153038-BV06: Delete HocSinh - With Related Data (FR06)**

*   **Description:** Verify that a student with active related data (`HocSinhDiem`) cannot be soft-deleted.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HS_04` exists and has an associated `HocSinhDiem` record that is not soft-deleted.
*   **Test Steps:**
    1.  **Given** the API endpoint for deleting a student `/api/hocsinh/{UUID_HS_04}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a DELETE request is sent
    4.  **Then** the response status code should be `400 Bad Request`
    5.  **And** the response body should contain `Success: false` and `Errors` should contain "Không thể xóa học sinh này vì có dữ liệu liên quan. Vui lòng xóa các dữ liệu liên quan trước hoặc chuyển trạng thái học sinh thành 'Đã chuyển trường'/'Tạm dừng' thay vì xóa."
    6.  **And** a subsequent GET request to `/api/hocsinh/{UUID_HS_04}` should show `IsDeleted = false` (student remains active).

**QUAN-20260604-153038-BV07: Get HocSinh by ID - Not Found**

*   **Description:** Verify retrieving a non-existent student returns 404.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HOC_SINH_KHONG_TON_TAI` is a non-existent GUID.
*   **Test Steps:**
    1.  **Given** the API endpoint for retrieving a student by ID `/api/hocsinh/{UUID_HOC_SINH_KHONG_TON_TAI}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a GET request is sent
    4.  **Then** the response status code should be `404 Not Found`
    5.  **And** the response body should contain `Success: false` and `Errors` should contain "Học sinh không tìm thấy."

**QUAN-20260604-153038-BV08: Update HocSinh - Not Found (FR04)**

*   **Description:** Verify updating a non-existent student returns an appropriate error.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HOC_SINH_KHONG_TON_TAI` is a non-existent GUID.
*   **Test Steps:**
    1.  **Given** the API endpoint for updating a student `/api/hocsinh/{UUID_HOC_SINH_KHONG_TON_TAI}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a PUT request is sent with a valid update body but non-existent ID
    4.  **Then** the response status code should be `400 Bad Request`
    5.  **And** the response body should contain `Success: false` and `Errors` should contain "Học sinh không tìm thấy."

**QUAN-20260604-153038-BV09: Update HocSinh - ID Mismatch (NFR: API Design)**

*   **Description:** Verify that the `id` in the URL path must match the `id` in the request body for PUT requests.
*   **Pre-conditions:** Admin user is authenticated. `UUID_HS_01` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint for updating a student `/api/hocsinh/{UUID_HS_01}`
    2.  **And** a valid JWT token for an Admin user is provided
    3.  **When** a PUT request is sent where `id` in body is `UUID_HS_02` (mismatch)
        ```json
        {
          "id": "UUID_HS_02", // Mismatch
          "hoTen": "Nguyen Van A Mismatched",
          "ngaySinh": "2008-01-15",
          "gioiTinh": "Nam",
          "lopHocId": "UUID_LOP_A",
          "trangThai": "DangHoc"
        }
        ```
    4.  **Then** the response status code should be `400 Bad Request`
    5.  **And** the response body should contain `Success: false` and `Errors` should contain "ID không khớp."

**QUAN-20260604-153038-BV10: List HocSinhs - Search and Filter Combinations (FR07)**

*   **Description:** Verify combined search and filter functionality.
*   **Pre-conditions:** Admin user is authenticated. Multiple students exist with various attributes.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a GET request is sent with `searchTerm = "Nguyen"` and `lopHocId = UUID_LOP_A`
    3.  **Then** the response should contain only "Nguyen Van A" (or "Nguyen Thi Thuy" if created in TC01). `totalCount` should reflect this.
    4.  **When** a GET request is sent with `searchTerm = "Tran"` and `trangThai = "DangHoc"`
    5.  **Then** the response should contain only "Tran Thi B". `totalCount` should reflect this.
    6.  **When** a GET request is sent with `searchTerm = "Le"` and `lopHocId = UUID_LOP_A` and `trangThai = "DaTotNghiep"`
    7.  **Then** the response should contain only "Le Van C". `totalCount` should reflect this.
    8.  **When** a GET request is sent with `searchTerm = "NonExistent"`
    9.  **Then** the response `items` list should be empty and `totalCount` should be 0.

**QUAN-20260604-153038-BV11: List HocSinhs - Pagination Boundaries (FR08)**

*   **Description:** Verify pagination behavior at boundaries and with varying page sizes.
*   **Pre-conditions:** Admin user is authenticated. At least 5 active students exist.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a GET request is sent with `pageNumber = 1` and `pageSize = 1`
    3.  **Then** `items` should contain 1 student. `pageNumber` should be 1. `hasPreviousPage` should be false. `hasNextPage` should be true (if total > 1).
    4.  **When** a GET request is sent with `pageNumber = totalPages` (e.g., if total 5, pageSize 2, totalPages 3, request page 3) and `pageSize = 2`
    5.  **Then** `items` should contain the remaining students (e.g., 1 student). `pageNumber` should be `totalPages`. `hasPreviousPage` should be true. `hasNextPage` should be false.
    6.  **When** a GET request is sent with `pageNumber = 999` (page far beyond total pages)
    7.  **Then** `items` should be an empty list, `pageNumber` should match requested, `totalCount` and `totalPages` should be correct.
    8.  **When** a GET request is sent with `pageSize = 0` or `pageSize = -1` (invalid)
    9.  **Then** the response status code should be `400 Bad Request` or default to a valid page size (depending on implementation, 400 is preferred for invalid input).

**QUAN-20260604-153038-BV12: Auto-generated MaHocSinh (BR01)**

*   **Description:** Verify that `MaHocSinh` is auto-generated and cannot be provided/changed by the user.
*   **Pre-conditions:** Admin user is authenticated. `UUID_LOP_A` exists.
*   **Test Steps:**
    1.  **Given** the API endpoint `/api/hocsinh` and an Admin JWT token
    2.  **When** a POST request is sent with `maHocSinh`: "MANUAL_CODE_123" included in the body
    3.  **Then** the response status code should be `201 Created`
    4.  **And** the `maHocSinh` in the response should *not* be "MANUAL_CODE_123", but an auto-generated one (e.g., "HS-xxxxxx"). This indicates the provided `maHocSinh` was ignored.
    5.  **Given** the API endpoint `/api/hocsinh/{UUID_HS_01}` and an Admin JWT token
    6.  **When** a PUT request is sent to change `MaHocSinh` of `UUID_HS_01` (include `maHocSinh: "NEW_CODE"` in body)
    7.  **Then** the response status code should be `200 OK`
    8.  **And** the `maHocSinh` in the response for `UUID_HS_01` should remain its original auto-generated value, not "NEW_CODE". This indicates it was ignored.

---

## 3. Traceability Matrix

| Requirement ID | Test Case ID(s)                                                                                                              | Type          | Description                                                                                                                                                                                                                                                                                                                                                                   |
| :------------- | :--------------------------------------------------------------------------------------------------------------------------- | :------------ | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **FR01**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-TC05, QUAN-20260604-153038-BV12                      | Functional    | `MaHocSinh` is auto-generated, unique, and immutable. Default sorting by `MaHocSinh` in list.                                                                                                                                                                                                                                                                                       |
| **FR02**       | QUAN-20260604-153038-TC02, QUAN-20260604-153038-BV07                                                                           | Functional    | View detailed student information by ID. Handle not found cases.                                                                                                                                                                                                                                                                                                              |
| **FR03**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-BV01, QUAN-20260604-153038-BV02, QUAN-20260604-153038-BV03, QUAN-20260604-153038-BV04, QUAN-20260604-153038-BV05 | Functional    | Create new student with valid data and validate required fields and formats.                                                                                                                                                                                                                                                                                                  |
| **FR04**       | QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV08, QUAN-20260604-153038-BV09                                                | Functional    | Update existing student information (except `MaHocSinh`). Handle not found and ID mismatch.                                                                                                                                                                                                                                                                                         |
| **FR05**       | QUAN-20260604-153038-TC04                                                                                                      | Functional    | Soft delete student (mark `IsDeleted=true`) and ensure it's hidden from default list views.                                                                                                                                                                                                                                                                                           |
| **FR06**       | QUAN-20260604-153038-BV06                                                                                                      | Functional    | Prevent soft-deletion of students with active related data (`HocSinhDiem`).                                                                                                                                                                                                                                                                                                           |
| **FR07**       | QUAN-20260604-153038-TC05, QUAN-20260604-153038-TC06, QUAN-20260604-153038-BV10                                                | Functional    | Search students by `MaHocSinh` or `HoTen`, filter by `LopHocId` and `TrangThai`.                                                                                                                                                                                                                                                                                                      |
| **FR08**       | QUAN-20260604-153038-TC05, QUAN-20260604-153038-BV11                                                                           | Functional    | Implement pagination for student lists, including boundary conditions.                                                                                                                                                                                                                                                                                                                |
| **BR01**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV12                                                | Business Rule | `MaHocSinh` is auto-generated (HS-{GUID part}), unique, and not editable after creation.                                                                                                                                                                                                                                                                                            |
| **BR02**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV01, QUAN-20260604-153038-BV05                      | Business Rule | Fields `HoTen`, `NgaySinh`, `GioiTinh`, `LopHocId`, `TrangThai` are required and must be valid.                                                                                                                                                                                                                                                                                         |
| **BR03**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV02                                                | Business Rule | `NgaySinh` must be a valid date and not in the future.                                                                                                                                                                                                                                                                                                                                |
| **BR04**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV03                                                | Business Rule | `SoDienThoaiPH` must follow Vietnamese phone number format if provided.                                                                                                                                                                                                                                                                                                             |
| **BR05**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV04                                                | Business Rule | `EmailPH` must follow a valid email format if provided.                                                                                                                                                                                                                                                                                                                               |
| **BR06**       | QUAN-20260604-153038-TC01, QUAN-20260604-153038-TC03, QUAN-20260604-153038-BV05                                                | Business Rule | `TrangThaiHocSinh` must be one of predefined values ('DangHoc', 'DaTotNghiep', 'DaChuyenTruong', 'TamDung').                                                                                                                                                                                                                                                                            |
| **SRS/API**    | QUAN-20260604-153038-API01                                                                                                      | API           | Verify all API endpoints are available and return expected `ApiResponse` structure.                                                                                                                                                                                                                                                                                                   |
| **SRS/Security** | QUAN-20260604-153038-SEC01, QUAN-20260604-153038-SEC02                                                                           | Security      | Ensure only 'Admin' role can perform CRUD operations and unauthenticated requests are rejected (NFR).                                                                                                                                                                                                                                                                                 |
| **SRS/Database** | QUAN-20260604-153038-TC04, QUAN-20260604-153038-BV06                                                                           | Technical     | Global query filter for `IsDeleted=false` works, `ON DELETE RESTRICT` for `LopHocId` (implicit for soft delete), naming conventions. (Other naming conventions implicitly covered by code review, not functional test).                                                                                                                                                            |
| **SRS/Validation** | QUAN-20260604-153038-BV01, QUAN-20260604-153038-BV02, QUAN-20260604-153038-BV03, QUAN-20260604-153038-BV04, QUAN-20260604-153038-BV05, QUAN-20260604-153038-BV06, QUAN-20260604-153038-BV07, QUAN-20260604-153038-BV08, QUAN-20260604-153038-BV09, QUAN-20260604-153038-BV11 | Boundary/Validation | Comprehensive validation of all input fields, boundary conditions for pagination, and specific business rules. Handling of non-existent IDs and ID mismatches.                                                                                                                                                                                                          |