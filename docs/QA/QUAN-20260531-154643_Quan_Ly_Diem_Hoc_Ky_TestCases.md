Tuyệt vời! Với vai trò là Kỹ sư QA/Tester cấp cao của ONENET, tôi sẽ tạo tài liệu Test Specification chi tiết cho tính năng "Quản lý Điểm Học kỳ" (QUAN-20260531-154643), tuân thủ các quy tắc và template đã cung cấp, đồng thời bao phủ các yêu cầu và thay đổi trong DEV PR Diff.

---

# TEST SPECIFICATION - QUẢN LÝ ĐIỂM HỌC KỲ

## 1. GIỚI THIỆU

Tài liệu này trình bày Test Specification cho tính năng "Quản lý Điểm Học kỳ" (QUAN-20260531-154643). Mục đích của tài liệu là mô tả chi tiết các trường hợp kiểm thử (Test Cases) nhằm đảm bảo hệ thống đáp ứng đầy đủ các yêu cầu nghiệp vụ (BRD), yêu cầu hệ thống (SRS/SAD) và tuân thủ các quy định kỹ thuật được định nghĩa trong `docs/rules/Technical_Guideline.md`, cũng như các thay đổi được triển khai trong DEV PR Diff (PR# 48).

**Mã tính năng:** QUAN-20260531-154643
**Tên tính năng:** Quan Ly Diem Hoc Ky
**BRD PR#:** 40
**SRS PR#:** 43
**DEV PR#:** 48

## 2. CÁC ĐỐI TƯỢNG KIỂM THỬ (TEST ITEMS)

*   API Endpoint: `/api/v1/Scores`
*   Các Command Handlers: `CreateScoreCommandHandler`, `UpdateScoreCommandHandler`, `DeleteScoreCommandHandler`
*   Các Query Handlers: `GetScoresQueryHandler`, `GetScoreByIdQueryHandler`
*   Các Validators: `CreateScoreCommandValidator`, `UpdateScoreCommandValidator`, `GetScoresQueryValidator`
*   Domain Entities: `Score`
*   Value Objects: `ScoreValue`
*   Services: `IScorePermissionService`, `IAuditLogService`, `ICurrentUser`
*   Repositories: `IScoreRepository`, `IStudentRepository`, `ISubjectRepository`, `ISemesterRepository`, `IAuditLogRepository`
*   Exception Handling: `ConflictException`, `ForbiddenException`, `NotFoundException`, `CustomException`, `GlobalExceptionMiddleware`
*   Database Schema: Bảng `scores` và các liên kết tới `students`, `subjects`, `semesters`.

## 3. CÁC TÍNH NĂNG CẦN KIỂM THỬ (FEATURES TO BE TESTED)

Dựa trên DEV PR Diff và các chuẩn mực phát triển phần mềm, các tính năng sau đây sẽ được kiểm thử:

### 3.1. Các Yêu cầu Chức năng (Functional Requirements - FR)

*   **FR01: Tạo Điểm Học kỳ (Create Score):** Cho phép người dùng (Admin/Teacher) tạo một bản ghi điểm mới cho một học sinh, môn học và học kỳ cụ thể. (CREATE ScoreCommand)
*   **FR02: Xem Chi tiết Điểm Học kỳ (Get Score Details):** Cho phép người dùng (Admin/Teacher) xem thông tin chi tiết của một bản ghi điểm dựa trên ID. (GET by ID ScoreQuery)
*   **FR03: Cập nhật Điểm Học kỳ (Update Score):** Cho phép người dùng (Admin/Teacher) cập nhật giá trị điểm của một bản ghi điểm hiện có. (UPDATE ScoreCommand)
*   **FR04: Xóa Điểm Học kỳ (Soft Delete Score):** Cho phép người dùng (Admin/Teacher) xóa mềm (đánh dấu `IsDeleted = true`) một bản ghi điểm hiện có. (DELETE ScoreCommand)
*   **FR05: Lấy Danh sách Điểm Học kỳ (Paginated List):** Cho phép người dùng (Admin/Teacher) truy xuất danh sách các bản ghi điểm có phân trang. (GET list ScoreQuery)
*   **FR06: Tìm kiếm và Lọc Điểm Học kỳ (Search & Filter):** Hỗ trợ tìm kiếm danh sách điểm theo từ khóa (tên/mã học sinh, tên môn học) và lọc theo Học kỳ, Môn học, Học sinh. (GET list ScoreQuery)
*   **FR07: Sắp xếp Danh sách Điểm Học kỳ (Sort List):** Hỗ trợ sắp xếp danh sách điểm theo tên học sinh, giá trị điểm, hoặc ngày tạo (tăng/giảm dần). (GET list ScoreQuery)

### 3.2. Các Yêu cầu Nghiệp vụ/Phi Chức năng quan trọng (Business Rules / Key Non-Functional Requirements - BR/NFR)

*   **BR01: Quy tắc Giá trị Điểm:** Giá trị điểm phải nằm trong khoảng từ 0.0 đến 10.0 và chỉ được phép tối đa 2 chữ số thập phân. (`ScoreValue` VO, FluentValidation).
*   **BR02: Duy nhất Bản ghi Điểm:** Mỗi học sinh chỉ có thể có một bản ghi điểm duy nhất cho một môn học trong một học kỳ cụ thể. (Unique index trên `(StudentId, SubjectId, SemesterId)` và kiểm tra trong `CreateScoreCommandHandler`).
*   **BR03: Kiểm tra Trường Bắt buộc:** Các trường `StudentId`, `SubjectId`, `SemesterId` và `Value` là bắt buộc khi tạo/cập nhật điểm. (FluentValidation).
*   **BR04: Phân quyền Truy cập:**
    *   Tất cả các thao tác quản lý điểm yêu cầu người dùng phải được xác thực (`IsAuthenticated`).
    *   Quản trị viên (Admin) có toàn quyền tạo, xem, sửa, xóa điểm.
    *   Giáo viên (Teacher) chỉ có quyền tạo, xem, sửa, xóa điểm của các học sinh thuộc lớp mình chủ nhiệm hoặc các môn học mình giảng dạy. *(Lưu ý: Dựa trên `ScorePermissionService` hiện tại, logic phân quyền chi tiết cho giáo viên phụ thuộc vào dữ liệu gán giáo viên-học sinh/môn học/học kỳ, hiện chưa được cung cấp đầy đủ. Các test case sẽ kiểm tra quyền của Admin và trường hợp Giáo viên không có quyền.)*
*   **BR05: Soft Delete (Xóa mềm):** Khi một bản ghi điểm được xóa, nó sẽ được đánh dấu `IsDeleted = true` và không được trả về trong các truy vấn mặc định.
*   **BR06: Audit Trail:** Mọi thao tác tạo, sửa, xóa bản ghi điểm phải được ghi lại đầy đủ vào hệ thống Audit Log (ID thực thể, loại thực thể, hành động, giá trị cũ/mới, người thực hiện, thời gian).
*   **BR07: Xử lý ngoại lệ chuẩn hóa:** Hệ thống phải trả về các lỗi chuẩn hóa (400 Bad Request, 403 Forbidden, 404 Not Found, 409 Conflict, 500 Internal Server Error) với cấu trúc `ApiResponse` và thông tin chi tiết lỗi phù hợp. (GlobalExceptionMiddleware, Custom Exceptions).
*   **BR08: Tuân thủ Technical Guideline:**
    *   Sử dụng Value Object `ScoreValue` để đóng gói logic nghiệp vụ giá trị điểm.
    *   Tên bảng và cột phải theo chuẩn `snake_case`.
    *   Các trường audit `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`, `IsDeleted` được quản lý bởi `BaseEntity` và `AppDbContext`.
    *   Global query filter cho `IsDeleted` được áp dụng cho `BaseEntity`.

## 4. CÁC TÍNH NĂNG KHÔNG CẦN KIỂM THỬ (FEATURES NOT TO BE TESTED)

*   Giao diện người dùng (UI) và trải nghiệm người dùng (UX) của các chức năng quản lý điểm.
*   Kiểm thử hiệu năng, stress test, load test ở quy mô lớn.
*   Kiểm thử khả năng phục hồi sau thảm họa (Disaster Recovery).
*   Kiểm thử tích hợp sâu với các hệ thống quản lý học sinh/môn học/học kỳ bên ngoài (chỉ kiểm tra việc hệ thống có thể truy vấn các placeholder entities `Student`, `Subject`, `Semester` theo ID).
*   Kiểm thử các chi tiết triển khai hạ tầng (ví dụ: Nginx, Kubernetes, CDN).
*   Kiểm thử cấu hình CORS chi tiết vượt ra ngoài việc xác nhận API có thể được gọi từ `AllowedOrigins`.
*   Kiểm thử chi tiết logic phân quyền của giáo viên (`IScorePermissionService`) nếu nó yêu cầu các bảng dữ liệu Assignment phức tạp không được cung cấp trong context hiện tại. (Giả định rằng `ScorePermissionService` sẽ trả về `false` cho giáo viên nếu không có dữ liệu assignment cụ thể để quản lý điểm đó).

## 5. PHƯƠNG PHÁP KIỂM THỬ (APPROACH)

*   **Kiểm thử Đơn vị (Unit Testing):** Được giả định đã thực hiện bởi DEV cho các thành phần nhỏ (Value Objects, Validators, Command/Query handlers riêng lẻ).
*   **Kiểm thử Tích hợp (Integration Testing):** Tập trung vào việc kiểm tra sự tương tác giữa các lớp Application, Domain, Infrastructure và Persistence (Repositories, DbContext).
*   **Kiểm thử API (API Testing):** Sử dụng các công cụ như Postman/Newman hoặc test framework để gửi request HTTP và xác thực response (status code, body, headers).
*   **Kiểm thử Chức năng (Functional Testing):** Đảm bảo tất cả các yêu cầu chức năng (FR) được đáp ứng.
*   **Kiểm thử Phi Chức năng (Non-Functional Testing):** Tập trung vào các yêu cầu nghiệp vụ quan trọng (BR), bảo mật (Security), xử lý lỗi (Error Handling), và boundary conditions.
*   **Kiểm thử Dữ liệu (Data-driven Testing):** Sử dụng các bộ dữ liệu khác nhau (happy path, edge cases, negative cases) để đảm bảo tính ổn định và chính xác.
*   **BDD (Given-When-Then):** Các bước kiểm thử sẽ được viết theo format BDD để tăng tính rõ ràng và dễ hiểu.

## 6. TIÊU CHÍ ĐẬU/RỚT (ITEM PASS/FAIL CRITERIA)

Một test case được coi là **ĐẬU** nếu:
*   Tất cả các bước kiểm thử được thực hiện thành công.
*   Kết quả thực tế khớp hoàn toàn với Kết quả mong muốn.
*   API trả về HTTP Status Code chính xác.
*   Cấu trúc phản hồi JSON (`ApiResponse`) là hợp lệ và chứa dữ liệu chính xác.
*   Dữ liệu trong cơ sở dữ liệu được tạo, cập nhật hoặc xóa (mềm) một cách chính xác.
*   Audit log được ghi nhận đúng theo yêu cầu.
*   Không có lỗi ngoại lệ không mong muốn xảy ra.

Một test case được coi là **RỚT** nếu:
*   Bất kỳ bước kiểm thử nào không thể thực hiện được hoặc bị lỗi.
*   Kết quả thực tế khác với Kết quả mong muốn.
*   API trả về HTTP Status Code không chính xác.
*   Cấu trúc phản hồi JSON không hợp lệ hoặc thiếu dữ liệu.
*   Dữ liệu trong cơ sở dữ liệu không nhất quán hoặc không đúng.
*   Audit log bị thiếu hoặc sai thông tin.
*   Bất kỳ lỗi ngoại lệ không được xử lý nào xảy ra.

## 7. TIÊU CHÍ ĐÌNH CHỈ VÀ YÊU CẦU TIẾP TỤC (SUSPENSION CRITERIA AND RESUMPTION REQUIREMENTS)

**Tiêu chí đình chỉ:**
*   Hơn 10% test case quan trọng (P1/P2) bị lỗi trong một chu kỳ chạy kiểm thử.
*   Các lỗi nghiêm trọng ngăn cản việc kiểm thử các luồng chức năng chính.
*   Môi trường kiểm thử không ổn định hoặc không khả dụng.
*   Phát hiện lỗi bảo mật nghiêm trọng.

**Yêu cầu tiếp tục:**
*   Các lỗi gây đình chỉ đã được sửa và xác nhận bởi DEV.
*   Môi trường kiểm thử được khôi phục về trạng thái ổn định.
*   Các bản build mới được cung cấp và cài đặt thành công.

## 8. YÊU CẦU VỀ MÔI TRƯỜNG (ENVIRONMENTAL REQUIREMENTS)

*   **Môi trường:** Dev/Staging Environment, được triển khai hoàn chỉnh với ONENET.API và Database.
*   **Cơ sở dữ liệu:** PostgreSQL, chứa dữ liệu khởi tạo (master data cho Students, Subjects, Semesters) và dữ liệu test.
*   **Công cụ:**
    *   API Client (Postman, Insomnia) để gửi yêu cầu HTTP.
    *   Công cụ quản lý Database (pgAdmin, DBeaver) để xác thực dữ liệu.
    *   Công cụ xem log (Serilog output, Kibana/Splunk) để kiểm tra log và audit trail.
    *   JWT Token generation tool (e.g., jwt.io hoặc local script) để tạo token cho các vai trò Admin, Teacher, Unauthenticated user.

## 9. DỮ LIỆU KIỂM THỬ (TEST DATA)

**Người dùng:**
*   **Admin User:**
    *   `UserId`: `admin_id_guid`
    *   `UserName`: `admin.user`
    *   `Roles`: `["Admin"]`
    *   `Token`: Token JWT hợp lệ cho Admin
*   **Teacher User:**
    *   `UserId`: `teacher_id_guid`
    *   `UserName`: `teacher.user`
    *   `Roles`: `["Teacher"]`
    *   `Token`: Token JWT hợp lệ cho Teacher
*   **Normal User (Không có quyền quản lý điểm):**
    *   `UserId`: `normal_user_id_guid`
    *   `UserName`: `normal.user`
    *   `Roles`: `["Student"]` hoặc không có vai trò liên quan
    *   `Token`: Token JWT hợp lệ cho Normal User
*   **Unauthenticated User:** Không có Token JWT.

**Dữ liệu Tham chiếu (External Entities) - Cần được tạo sẵn trong DB:**
*   **Students:**
    *   `Student_A`: `Id = GUID_STUDENT_A`, `Code = "ST001"`, `FullName = "Nguyen Van A"`
    *   `Student_B`: `Id = GUID_STUDENT_B`, `Code = "ST002"`, `FullName = "Tran Thi B"`
    *   `Student_C`: `Id = GUID_STUDENT_C`, `Code = "ST003"`, `FullName = "Le Van C"`
*   **Subjects:**
    *   `Subject_Math`: `Id = GUID_SUBJECT_MATH`, `Name = "Toan"`
    *   `Subject_Physics`: `Id = GUID_SUBJECT_PHYSICS`, `Name = "Vat Ly"`
    *   `Subject_Chemistry`: `Id = GUID_SUBJECT_CHEMISTRY`, `Name = "Hoa Hoc"`
*   **Semesters:**
    *   `Semester_2024_1`: `Id = GUID_SEMESTER_2024_1`, `Name = "Hoc Ky I (2024-2025)"`, `SchoolYear = "2024-2025"`
    *   `Semester_2024_2`: `Id = GUID_SEMESTER_2024_2`, `Name = "Hoc Ky II (2024-2025)"`, `SchoolYear = "2024-2025"`

**Dữ liệu Điểm (Scores) - Cần được tạo hoặc sẽ được tạo trong quá trình test:**
*   `SCORE_ID_EXISTS_1`: `Student_A`, `Subject_Math`, `Semester_2024_1`, `Value = 8.5`
*   `SCORE_ID_EXISTS_2`: `Student_B`, `Subject_Physics`, `Semester_2024_1`, `Value = 7.0`
*   `SCORE_ID_FOR_UPDATE`: `Student_C`, `Subject_Chemistry`, `Semester_2024_1`, `Value = 6.5`
*   `SCORE_ID_FOR_DELETE`: `Student_A`, `Subject_Physics`, `Semester_2024_1`, `Value = 9.0`
*   `SCORE_ID_DELETED`: `Student_B`, `Subject_Chemistry`, `Semester_2024_2`, `Value = 5.0` (đã được tạo và soft-delete trước đó)

**Dữ liệu không hợp lệ/Biên:**
*   `GUID_INVALID`: Một GUID không tồn tại (ví dụ: `00000000-0000-0000-0000-000000000000`).
*   `GUID_NON_EXISTENT`: Một GUID có cấu trúc hợp lệ nhưng không tương ứng với bất kỳ bản ghi nào trong DB.
*   `SCORE_VALUE_INVALID_LOW`: `-0.1`
*   `SCORE_VALUE_INVALID_HIGH`: `10.1`
*   `SCORE_VALUE_INVALID_DECIMAL`: `8.123` (3 chữ số thập phân)
*   `SCORE_VALUE_VALID_MIN`: `0.0`
*   `SCORE_VALUE_VALID_MAX`: `10.0`
*   `SCORE_VALUE_VALID_2_DECIMAL`: `7.25`

## 10. TEST CASES

### 10.1. Functional Tests (`QUAN-20260531-154643-TCXX`)

#### QUAN-20260531-154643-TC01: Tạo điểm thành công bởi Admin (Happy Case)
*   **Mục đích:** Xác minh Admin có thể tạo điểm mới hợp lệ.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration
*   **Tham chiếu:** FR01, BR01, BR03, BR04, BR06
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   `GUID_STUDENT_A`, `GUID_SUBJECT_MATH`, `GUID_SEMESTER_2024_2` tồn tại.
    *   Chưa có điểm cho `Student_A`, `Subject_Math`, `Semester_2024_2`.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Các thông tin `StudentId`, `SubjectId`, `SemesterId` hợp lệ và duy nhất, `Value` là `8.5`
    3.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body:
        ```json
        {
          "studentId": "GUID_STUDENT_A",
          "subjectId": "GUID_SUBJECT_MATH",
          "semesterId": "GUID_SEMESTER_2024_2",
          "value": 8.5
        }
        ```
    4.  **Then** Hệ thống trả về HTTP Status Code `201 Created`
    5.  **And** Phản hồi chứa `ApiResponse.Success` với `Data` là `ID` của điểm mới tạo.
    6.  **And** Tôi có thể tìm thấy bản ghi điểm mới trong database với các giá trị chính xác và `IsDeleted = false`.
    7.  **And** Trường `CreatedBy` là `admin.user`, `CreatedAt` là thời gian hiện tại.
    8.  **And** Một bản ghi audit log với `Action = "Create"` được tạo với các giá trị mới.

#### QUAN-20260531-154643-TC02: Tạo điểm thất bại do trùng lặp (Negative Case)
*   **Mục đích:** Xác minh hệ thống không cho phép tạo điểm trùng lặp theo BR02.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration, Boundary
*   **Tham chiếu:** FR01, BR02, BR03, BR07
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Điểm với `Student_A`, `Subject_Math`, `Semester_2024_1`, `Value = 8.5` (`SCORE_ID_EXISTS_1`) đã tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Bản ghi điểm cho `Student_A`, `Subject_Math`, `Semester_2024_1` đã tồn tại
    3.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body tạo điểm cho cùng `Student_A`, `Subject_Math`, `Semester_2024_1` và `Value = 9.0`
    4.  **Then** Hệ thống trả về HTTP Status Code `409 Conflict`
    5.  **And** Phản hồi chứa `ApiResponse.Error` với thông báo "A score for this student, subject, and semester already exists."
    6.  **And** Không có bản ghi điểm mới nào được thêm vào database.
    7.  **And** Không có bản ghi audit log mới nào được tạo.

#### QUAN-20260531-154643-TC03: Cập nhật giá trị điểm thành công (Happy Case)
*   **Mục đích:** Xác minh Admin có thể cập nhật giá trị điểm hợp lệ.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration
*   **Tham chiếu:** FR03, BR01, BR03, BR04, BR06
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Điểm với `Student_C`, `Subject_Chemistry`, `Semester_2024_1`, `Value = 6.5` (`SCORE_ID_FOR_UPDATE`) đã tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Bản ghi điểm với `ID = SCORE_ID_FOR_UPDATE` và `Value = 6.5` tồn tại
    3.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với body:
        ```json
        {
          "id": "SCORE_ID_FOR_UPDATE",
          "value": 7.75
        }
        ```
    4.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    5.  **And** Phản hồi chứa `ApiResponse.Success` với thông báo "Score updated successfully.".
    6.  **And** Bản ghi điểm trong database có `Value` được cập nhật thành `7.75`, `UpdatedBy` là `admin.user`, `UpdatedAt` là thời gian hiện tại.
    7.  **And** Một bản ghi audit log với `Action = "Update"` được tạo, hiển thị `oldValues` là `6.5` và `newValues` là `7.75`.

#### QUAN-20260531-154643-TC04: Xóa mềm bản ghi điểm thành công (Happy Case)
*   **Mục đích:** Xác minh Admin có thể xóa mềm bản ghi điểm và nó không hiển thị trong truy vấn.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration
*   **Tham chiếu:** FR04, BR04, BR05, BR06
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Điểm với `Student_A`, `Subject_Physics`, `Semester_2024_1`, `Value = 9.0` (`SCORE_ID_FOR_DELETE`) đã tồn tại và `IsDeleted = false`.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Bản ghi điểm với `ID = SCORE_ID_FOR_DELETE` tồn tại và `IsDeleted = false`
    3.  **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_FOR_DELETE`
    4.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    5.  **And** Phản hồi chứa `ApiResponse.Success` với thông báo "Score deleted successfully.".
    6.  **And** Bản ghi điểm trong database với `ID = SCORE_ID_FOR_DELETE` có `IsDeleted = true`, `UpdatedBy` là `admin.user`, `UpdatedAt` là thời gian hiện tại.
    7.  **And** Khi tôi gửi yêu cầu GET `/api/v1/Scores/SCORE_ID_FOR_DELETE`, hệ thống trả về HTTP Status Code `404 Not Found`.
    8.  **And** Một bản ghi audit log với `Action = "Delete"` được tạo, hiển thị `oldValues` của bản ghi.

#### QUAN-20260531-154643-TC05: Lấy danh sách điểm có phân trang và lọc thành công (Happy Case)
*   **Mục đích:** Xác minh Admin có thể lấy danh sách điểm, sử dụng phân trang và lọc theo yêu cầu FR05, FR06.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration
*   **Tham chiếu:** FR05, FR06, BR04, BR05, BR07
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Nhiều bản ghi điểm hợp lệ đã tồn tại (ví dụ: `SCORE_ID_EXISTS_1`, `SCORE_ID_EXISTS_2`, điểm từ TC01, TC03).
    *   `SCORE_ID_DELETED` tồn tại nhưng đã soft-delete.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Có ít nhất 5 bản ghi điểm hợp lệ (chưa xóa mềm) trong hệ thống
    3.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores` với query params:
        `pageNumber=1`, `pageSize=2`, `semesterId=GUID_SEMESTER_2024_1`, `searchKeyword=Nguyen`
    4.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    5.  **And** Phản hồi chứa `ApiResponse.Success` với `Data` là `PaginatedList<ScoreListItemDto>`.
    6.  **And** `PaginatedList.Items` chứa 2 bản ghi điểm.
    7.  **And** Tất cả các bản ghi trong `Items` đều thuộc `Semester_2024_1` và có `StudentFullName` hoặc `StudentCode` chứa "Nguyen".
    8.  **And** Bản ghi `SCORE_ID_DELETED` không có trong danh sách kết quả.
    9.  **And** Các trường `StudentCode`, `StudentFullName`, `SubjectName`, `SemesterName`, `SchoolYear` được populated đúng.

#### QUAN-20260531-154643-TC06: Lấy chi tiết điểm thành công (Happy Case)
*   **Mục đích:** Xác minh Admin có thể lấy chi tiết điểm và dữ liệu liên quan được populate đúng.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API, Integration
*   **Tham chiếu:** FR02, BR04, BR07, BR08
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Điểm với `Student_A`, `Subject_Math`, `Semester_2024_1`, `Value = 8.5` (`SCORE_ID_EXISTS_1`) đã tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Bản ghi điểm với `ID = SCORE_ID_EXISTS_1` tồn tại và `IsDeleted = false`
    3.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_EXISTS_1`
    4.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    5.  **And** Phản hồi chứa `ApiResponse.Success` với `Data` là `ScoreDto`.
    6.  **And** `ScoreDto.Id` là `SCORE_ID_EXISTS_1`.
    7.  **And** `ScoreDto.Value` là `8.5`.
    8.  **And** `ScoreDto.StudentCode` là `ST001`, `ScoreDto.StudentFullName` là `Nguyen Van A`.
    9.  **And** `ScoreDto.SubjectName` là `Toan`.
    10. **And** `ScoreDto.SemesterName` là `Hoc Ky I (2024-2025)`, `ScoreDto.SchoolYear` là `2024-2025`.
    11. **And** `CreatedBy`, `CreatedDate`, `LastModifiedBy`, `LastModifiedDate` được populate đúng.

#### QUAN-20260531-154643-TC07: Sắp xếp danh sách điểm theo các tiêu chí (Happy Case)
*   **Mục đích:** Xác minh hệ thống hỗ trợ sắp xếp danh sách điểm theo FR07.
*   **Độ ưu tiên:** P2 (Medium)
*   **Loại test:** Functional, API
*   **Tham chiếu:** FR07, BR04
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   Nhiều bản ghi điểm hợp lệ với các `Value` và `CreatedAt` khác nhau, và các `StudentFullName` khác nhau.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Có các bản ghi điểm với các giá trị khác nhau
    3.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?sortBy=studentName&sortOrder=desc`
    4.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    5.  **And** Danh sách điểm trả về được sắp xếp giảm dần theo `StudentFullName`.
    6.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?sortBy=value&sortOrder=asc`
    7.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    8.  **And** Danh sách điểm trả về được sắp xếp tăng dần theo `Value`.
    9.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?sortBy=createdDate&sortOrder=desc`
    10. **Then** Hệ thống trả về HTTP Status Code `200 OK`
    11. **And** Danh sách điểm trả về được sắp xếp giảm dần theo `CreatedDate`.

### 10.2. API Tests (`QUAN-20260531-154643-APIXX`)

#### QUAN-20260531-154643-API01: Kiểm tra cấu trúc phản hồi API (GET /api/v1/Scores)
*   **Mục đích:** Đảm bảo tất cả các API endpoint trả về cấu trúc `ApiResponse` chuẩn.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** API
*   **Tham chiếu:** BR07
*   **Test Data Pre-conditions:** Admin User token. Ít nhất một bản ghi điểm tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores`
    3.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    4.  **And** Phản hồi JSON có cấu trúc `{ "succeeded": true, "message": "...", "statusCode": 200, "data": { ... } }`
    5.  **And** `data` là một đối tượng `PaginatedList<ScoreListItemDto>`.

#### QUAN-20260531-154643-API02: Kiểm tra cấu trúc phản hồi API (POST /api/v1/Scores)
*   **Mục đích:** Đảm bảo `POST` API endpoint trả về cấu trúc `ApiResponse` chuẩn.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** API
*   **Tham chiếu:** BR07
*   **Test Data Pre-conditions:** Admin User token. `GUID_STUDENT_B`, `GUID_SUBJECT_MATH`, `GUID_SEMESTER_2024_2` tồn tại và duy nhất.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Các thông tin `StudentId`, `SubjectId`, `SemesterId` hợp lệ và duy nhất, `Value` là `8.0`
    3.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body hợp lệ
    4.  **Then** Hệ thống trả về HTTP Status Code `201 Created`
    5.  **And** Phản hồi JSON có cấu trúc `{ "succeeded": true, "message": "...", "statusCode": 201, "data": "..." }`
    6.  **And** `data` là một `Guid` đại diện cho ID của điểm mới.
    7.  **And** Header `Location` tồn tại và trỏ đến URI của tài nguyên mới.

#### QUAN-20260531-154643-API03: ID trong URL và body không khớp khi Update (Negative Case)
*   **Mục đích:** Xác minh API xử lý lỗi khi ID trong URL và body của PUT request không khớp.
*   **Độ ưu tiên:** P2 (Medium)
*   **Loại test:** API, Functional
*   **Tham chiếu:** FR03, BR07
*   **Test Data Pre-conditions:** Admin User token. `SCORE_ID_FOR_UPDATE` tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **And** Bản ghi điểm với `ID = SCORE_ID_FOR_UPDATE` tồn tại
    3.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/GUID_NON_EXISTENT` với body chứa `id = SCORE_ID_FOR_UPDATE` và `value = 7.0`
    4.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    5.  **And** Phản hồi chứa `ApiResponse.Error` với thông báo "ID in URL does not match ID in request body."

### 10.3. Security Tests (`QUAN-20260531-154643-SECXX`)

#### QUAN-20260531-154643-SEC01: Truy cập API khi chưa xác thực (Unauthenticated Access)
*   **Mục đích:** Xác minh người dùng chưa xác thực không thể truy cập bất kỳ endpoint nào.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Security, API
*   **Tham chiếu:** BR04, BR07
*   **Test Data Pre-conditions:** Không có token JWT.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng chưa xác thực (không có token JWT)
    2.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body hợp lệ
    3.  **Then** Hệ thống trả về HTTP Status Code `401 Unauthorized`
    4.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores`
    5.  **Then** Hệ thống trả về HTTP Status Code `401 Unauthorized`
    6.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_EXISTS_1`
    7.  **Then** Hệ thống trả về HTTP Status Code `401 Unauthorized`
    8.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với body hợp lệ
    9.  **Then** Hệ thống trả về HTTP Status Code `401 Unauthorized`
    10. **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_FOR_DELETE`
    11. **Then** Hệ thống trả về HTTP Status Code `401 Unauthorized`

#### QUAN-20260531-154643-SEC02: Truy cập API bởi người dùng không có quyền (Unauthorized User - Normal User)
*   **Mục đích:** Xác minh người dùng đã xác thực nhưng không có vai trò Admin hoặc Teacher không thể truy cập các endpoint quản lý điểm.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Security, API
*   **Tham chiếu:** BR04, BR07
*   **Test Data Pre-conditions:** Normal User token (vai trò "Student" hoặc không có vai trò liên quan).
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng đã xác thực với vai trò "Student"
    2.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body hợp lệ
    3.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    4.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores`
    5.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    6.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_EXISTS_1`
    7.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    8.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với body hợp lệ
    9.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    10. **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_FOR_DELETE`
    11. **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`

#### QUAN-20260531-154643-SEC03: Giáo viên cố gắng thao tác điểm không có quyền (Teacher Unauthorized Access)
*   **Mục đích:** Xác minh giáo viên không thể thao tác các bản ghi điểm mà họ không được phân quyền quản lý (theo logic placeholder của `ScorePermissionService`).
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Security, Functional
*   **Tham chiếu:** BR04, BR07
*   **Test Data Pre-conditions:**
    *   Teacher User token.
    *   `SCORE_ID_EXISTS_1` (Student_A, Subject_Math, Semester_2024_1) tồn tại. Giả định rằng `Teacher User` *không* được gán để quản lý điểm này trong `ScorePermissionService`.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Teacher đã xác thực
    2.  **And** Bản ghi điểm `SCORE_ID_EXISTS_1` tồn tại, nhưng tôi không có quyền quản lý điểm này (theo `IScorePermissionService` trả về `false`)
    3.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_EXISTS_1`
    4.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    5.  **And** Phản hồi chứa `ApiResponse.Error` với thông báo "You do not have permission to view this score.".
    6.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_EXISTS_1` để cập nhật `Value` thành `8.0`
    7.  **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    8.  **And** Phản hồi chứa `ApiResponse.Error` với thông báo "You do not have permission to update this score.".
    9.  **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_EXISTS_1`
    10. **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    11. **And** Phản hồi chứa `ApiResponse.Error` với thông báo "You do not have permission to delete this score.".
    12. **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` để tạo một điểm mới mà tôi không có quyền tạo (Student_A, Subject_Chemistry, Semester_2024_2, Value=7.0)
    13. **Then** Hệ thống trả về HTTP Status Code `403 Forbidden`
    14. **And** Phản hồi chứa `ApiResponse.Error` với thông báo "You do not have permission to create scores for this student, subject, or semester.".
    15. **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores` (danh sách)
    16. **Then** Hệ thống trả về HTTP Status Code `403 Forbidden` (do `GetScoresQueryHandler` hiện tại có thể hạn chế nếu không phải Admin và không có filter cụ thể hoặc permissionService chưa được triển khai đầy đủ cho list query)
    *(Ghi chú: Test case này dựa trên giả định `ScorePermissionService` sẽ trả về `false` cho giáo viên nếu không có logic gán quyền cụ thể được định nghĩa)*

#### QUAN-20260531-154643-SEC04: Giáo viên thao tác điểm có quyền (Teacher Authorized Access - Happy Path)
*   **Mục đích:** Xác minh giáo viên có thể thao tác các bản ghi điểm mà họ có quyền quản lý.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Security, Functional
*   **Tham chiếu:** FR01, FR02, FR03, FR04, BR04, BR06
*   **Test Data Pre-conditions:**
    *   Teacher User token.
    *   `GUID_STUDENT_B`, `GUID_SUBJECT_MATH`, `GUID_SEMESTER_2024_2` tồn tại.
    *   Giả định rằng `ScorePermissionService.CanManageScoreAsync` sẽ trả về `true` cho các `studentId`, `subjectId`, `semesterId` này khi gọi bởi `Teacher User`.
    *   Chưa có điểm cho `Student_B`, `Subject_Math`, `Semester_2024_2`.
    *   Điểm `SCORE_ID_FOR_UPDATE` thuộc về Teacher (giả định `Student_C`, `Subject_Chemistry`, `Semester_2024_1` được Teacher quản lý)
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Teacher đã xác thực
    2.  **And** `Student_B`, `Subject_Math`, `Semester_2024_2` thuộc về quyền quản lý của tôi (theo `IScorePermissionService`)
    3.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với body hợp lệ cho `Student_B`, `Subject_Math`, `Semester_2024_2`, `Value = 7.5`
    4.  **Then** Hệ thống trả về HTTP Status Code `201 Created`
    5.  **And** Tôi có thể tìm thấy bản ghi điểm mới trong database và audit log được tạo.
    6.  **Given** Tôi là người dùng Teacher đã xác thực
    7.  **And** Bản ghi điểm `SCORE_ID_FOR_UPDATE` thuộc về quyền quản lý của tôi
    8.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE`
    9.  **Then** Hệ thống trả về HTTP Status Code `200 OK`
    10. **And** Tôi nhận được chi tiết điểm.
    11. **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` để cập nhật `Value` thành `8.25`
    12. **Then** Hệ thống trả về HTTP Status Code `200 OK`
    13. **And** Bản ghi điểm được cập nhật và audit log được tạo.
    14. **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_FOR_DELETE` (giả sử điểm này cũng thuộc quyền quản lý của Teacher)
    15. **Then** Hệ thống trả về HTTP Status Code `200 OK`
    16. **And** Bản ghi điểm được soft-delete và audit log được tạo.
    17. **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores` với các bộ lọc `studentId=GUID_STUDENT_B`, `subjectId=GUID_SUBJECT_MATH`, `semesterId=GUID_SEMESTER_2024_2` (điểm tôi có quyền quản lý)
    18. **Then** Hệ thống trả về HTTP Status Code `200 OK`
    19. **And** Tôi nhận được danh sách điểm hợp lệ và có quyền truy cập.

### 10.4. Boundary/Validation Tests (`QUAN-20260531-154643-BVXX`)

#### QUAN-20260531-154643-BV01: Kiểm tra giá trị điểm (Score Value) - Biên và Không hợp lệ
*   **Mục đích:** Xác minh BR01 (`ScoreValue`) được thực thi chính xác trong cả tạo và cập nhật.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Boundary, Validation, Functional, API
*   **Tham chiếu:** BR01, BR03, BR07
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   `GUID_STUDENT_A`, `GUID_SUBJECT_MATH`, `GUID_SEMESTER_2024_2` tồn tại.
    *   `SCORE_ID_FOR_UPDATE` tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value = SCORE_VALUE_INVALID_LOW` (`-0.1`)
    3.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    4.  **And** Phản hồi chứa thông báo lỗi "Score value must be between 0.0 and 10.0 with at most 2 decimal places.".
    5.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value = SCORE_VALUE_INVALID_HIGH` (`10.1`)
    6.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    7.  **And** Phản hồi chứa thông báo lỗi "Score value must be between 0.0 and 10.0 with at most 2 decimal places.".
    8.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value = SCORE_VALUE_INVALID_DECIMAL` (`8.123`)
    9.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    10. **And** Phản hồi chứa thông báo lỗi "Score value must be between 0.0 and 10.0 with at most 2 decimal places.".
    11. **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value = SCORE_VALUE_VALID_MIN` (`0.0`)
    12. **Then** Hệ thống trả về HTTP Status Code `201 Created` (Tạo thành công)
    13. **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value = SCORE_VALUE_VALID_MAX` (`10.0`)
    14. **Then** Hệ thống trả về HTTP Status Code `201 Created` (Tạo thành công)
    15. **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với `Value = SCORE_VALUE_INVALID_LOW` (`-0.1`)
    16. **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    17. **And** Phản hồi chứa thông báo lỗi "Score value must be between 0.0 and 10.0 with at most 2 decimal places.".
    18. **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với `Value = SCORE_VALUE_VALID_2_DECIMAL` (`7.25`)
    19. **Then** Hệ thống trả về HTTP Status Code `200 OK` (Cập nhật thành công)

#### QUAN-20260531-154643-BV02: Kiểm tra các trường bắt buộc (Required Fields)
*   **Mục đích:** Xác minh BR03 được thực thi cho cả tạo và cập nhật.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Validation, Functional, API
*   **Tham chiếu:** BR03, BR07
*   **Test Data Pre-conditions:** Admin User token. `SCORE_ID_FOR_UPDATE` tồn tại.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `StudentId` là `null` hoặc `empty GUID`
    3.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    4.  **And** Phản hồi chứa thông báo lỗi "Student ID is required.".
    5.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `SubjectId` là `null` hoặc `empty GUID`
    6.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    7.  **And** Phản hồi chứa thông báo lỗi "Subject ID is required.".
    8.  **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `SemesterId` là `null` hoặc `empty GUID`
    9.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    10. **And** Phản hồi chứa thông báo lỗi "Semester ID is required.".
    11. **When** Tôi gửi yêu cầu POST đến `/api/v1/Scores` với `Value` là `null`
    12. **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    13. **And** Phản hồi chứa thông báo lỗi "Score value is required.".
    14. **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_FOR_UPDATE` với `Value` là `null`
    15. **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    16. **And** Phản hồi chứa thông báo lỗi "Score value is required.".

#### QUAN-20260531-154643-BV03: Kiểm tra Pagination và Sort params
*   **Mục đích:** Xác minh validator cho `GetScoresQuery` hoạt động đúng.
*   **Độ ưu tiên:** P2 (Medium)
*   **Loại test:** Validation, API
*   **Tham chiếu:** FR05, FR07, BR07
*   **Test Data Pre-conditions:** Admin User token.
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?pageNumber=0`
    3.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    4.  **And** Phản hồi chứa thông báo lỗi "Page number must be at least 1.".
    5.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?pageSize=0`
    6.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    7.  **And** Phản hồi chứa thông báo lỗi "Page size must be at least 1.".
    8.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?pageSize=101`
    9.  **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    10. **And** Phản hồi chứa thông báo lỗi "Page size cannot exceed 100.".
    11. **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?sortOrder=invalid`
    12. **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    13. **And** Phản hồi chứa thông báo lỗi "Sort order must be 'asc' or 'desc'.".
    14. **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores?sortBy=invalidField`
    15. **Then** Hệ thống trả về HTTP Status Code `400 Bad Request`
    16. **And** Phản hồi chứa thông báo lỗi "SortBy field is not supported.".

#### QUAN-20260531-154643-BV04: Truy vấn bản ghi điểm không tồn tại hoặc đã bị xóa mềm
*   **Mục đích:** Xác minh hệ thống xử lý đúng khi truy vấn, cập nhật hoặc xóa bản ghi không tồn tại hoặc đã xóa mềm.
*   **Độ ưu tiên:** P1 (High)
*   **Loại test:** Functional, API
*   **Tham chiếu:** FR02, FR03, FR04, BR05, BR07
*   **Test Data Pre-conditions:**
    *   Admin User token.
    *   `GUID_NON_EXISTENT`: Một GUID không tồn tại trong DB.
    *   `SCORE_ID_DELETED`: Một bản ghi điểm đã được soft-delete (`IsDeleted = true`).
*   **Test Steps (BDD):**
    1.  **Given** Tôi là người dùng Admin đã xác thực
    2.  **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/GUID_NON_EXISTENT`
    3.  **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    4.  **And** Phản hồi chứa thông báo lỗi "Score with ID {GUID_NON_EXISTENT} not found.".
    5.  **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/GUID_NON_EXISTENT` với body hợp lệ
    6.  **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    7.  **And** Phản hồi chứa thông báo lỗi "Score with ID {GUID_NON_EXISTENT} not found.".
    8.  **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/GUID_NON_EXISTENT`
    9.  **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    10. **And** Phản hồi chứa thông báo lỗi "Score with ID {GUID_NON_EXISTENT} not found.".
    11. **When** Tôi gửi yêu cầu GET đến `/api/v1/Scores/SCORE_ID_DELETED`
    12. **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    13. **And** Phản hồi chứa thông báo lỗi "Score with ID {SCORE_ID_DELETED} not found.".
    14. **When** Tôi gửi yêu cầu PUT đến `/api/v1/Scores/SCORE_ID_DELETED` với body hợp lệ
    15. **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    16. **And** Phản hồi chứa thông báo lỗi "Score with ID {SCORE_ID_DELETED} not found.".
    17. **When** Tôi gửi yêu cầu DELETE đến `/api/v1/Scores/SCORE_ID_DELETED`
    18. **Then** Hệ thống trả về HTTP Status Code `404 Not Found`
    19. **And** Phản hồi chứa thông báo lỗi "Score with ID {SCORE_ID_DELETED} not found.".

## 11. MA TRẬN TRUY VẾT (TRACEABILITY MATRIX)

| ID Test Case | Loại Test | FR được bao phủ | BR/NFR được bao phủ | Ghi chú |
| :------------------------------- | :-------- | :-------------------- | :------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------- |
| `QUAN-20260531-154643-TC01` | Functional | FR01 | BR01, BR03, BR04, BR06, BR08 | Tạo điểm thành công bởi Admin. |
| `QUAN-20260531-154643-TC02` | Functional | FR01 | BR02, BR03, BR07, BR08 | Tạo điểm trùng lặp. |
| `QUAN-20260531-154643-TC03` | Functional | FR03 | BR01, BR03, BR04, BR06, BR08 | Cập nhật điểm thành công bởi Admin. |
| `QUAN-20260531-154643-TC04` | Functional | FR04 | BR04, BR05, BR06, BR08 | Xóa mềm điểm thành công bởi Admin. |
| `QUAN-20260531-154643-TC05` | Functional | FR05, FR06 | BR04, BR05, BR07 | Lấy danh sách điểm có phân trang và lọc. |
| `QUAN-20260531-154643-TC06` | Functional | FR02 | BR04, BR07, BR08 | Lấy chi tiết điểm thành công. |
| `QUAN-20260531-154643-TC07` | Functional | FR07 | BR04 | Sắp xếp danh sách điểm theo các tiêu chí. |
| `QUAN-20260531-154643-API01` | API | N/A | BR07 | Kiểm tra cấu trúc phản hồi API (GET list). |
| `QUAN-20260531-154643-API02` | API | N/A | BR07 | Kiểm tra cấu trúc phản hồi API (POST). |
| `QUAN-20260531-154643-API03` | API | FR03 | BR07 | ID trong URL và body không khớp khi Update. |
| `QUAN-20260531-154643-SEC01` | Security | FR01, FR02, FR03, FR04, FR05 | BR04, BR07 | Truy cập API khi chưa xác thực. |
| `QUAN-20260531-154643-SEC02` | Security | FR01, FR02, FR03, FR04, FR05 | BR04, BR07 | Truy cập API bởi người dùng không có quyền (Normal User). |
| `QUAN-20260531-154643-SEC03` | Security | FR01, FR02, FR03, FR04, FR05 | BR04, BR07 | Giáo viên cố gắng thao tác điểm không có quyền (placeholder logic). |
| `QUAN-20260531-154643-SEC04` | Security | FR01, FR02, FR03, FR04, FR05 | BR04, BR06 | Giáo viên thao tác điểm có quyền (giả định có dữ liệu phân quyền). |
| `QUAN-20260531-154643-BV01` | Boundary/Validation | FR01, FR03 | BR01, BR03, BR07, BR08 | Kiểm tra giá trị điểm (biên và không hợp lệ). |
| `QUAN-20260531-154643-BV02` | Boundary/Validation | FR01, FR03 | BR03, BR07 | Kiểm tra các trường bắt buộc. |
| `QUAN-20260531-154643-BV03` | Boundary/Validation | FR05, FR07 | BR07 | Kiểm tra các tham số phân trang và sắp xếp không hợp lệ. |
| `QUAN-20260531-154643-BV04` | Boundary/Validation | FR02, FR03, FR04 | BR05, BR07 | Truy vấn bản ghi điểm không tồn tại hoặc đã xóa mềm. |

---

**Cảnh báo bảo mật:** Tôi không tạo, ghi file vào thư mục gốc của ONENET.AgentFactory trong quá trình thực hiện nhiệm vụ này.