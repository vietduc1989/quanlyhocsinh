# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-154643` |
| **Tên tính năng** | Quan Ly Mon Hoc |
| **Dự án** | quanlyhocsinh |
| **Phiên bản** | 1.0 |
| **Ngày tạo** | 2026-05-31 |
| **Tác giả** | BA Agent (ONENET AgentFactory) |

## Tài liệu liên quan

| Mã tính năng | Loại tài liệu | Trạng thái |
|-------------|---------------|------------|
| `QUAN-20260531-154643` | **BRD** (tài liệu này) | ✅ Hiện tại |
| `QUAN-20260531-154643` | SRS/SAD | ⏳ Chờ BA duyệt |
| `QUAN-20260531-154643` | DEV (mã nguồn) | ⏳ Chờ SRS duyệt |
| `QUAN-20260531-154643` | TEST (test cases) | ⏳ Chờ DEV duyệt |

---

## 1. Mục tiêu

### 1.1 Mục tiêu chính
- Cung cấp một giao diện và các chức năng để người dùng có thẩm quyền (Admin, Chuyên viên phòng Đào tạo) có thể quản lý (tạo, xem, sửa, xóa) thông tin môn học một cách hiệu quả và chính xác.
- Đảm bảo tính toàn vẹn và nhất quán của dữ liệu môn học trong hệ thống.

### 1.2 Mục tiêu phụ
- Tăng cường độ chính xác của dữ liệu phục vụ cho việc lập báo cáo, xếp thời khóa biểu và các quy trình đào tạo khác liên quan đến môn học.
- Cải thiện trải nghiệm người dùng bằng cách cung cấp một công cụ quản lý môn học thân thiện, dễ sử dụng.
- Đặt nền tảng cho việc tích hợp trong tương lai với các module khác của hệ thống như quản lý đăng ký học, phân công giảng viên.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới thông tin môn học với các trường: Mã môn học, Tên môn học, Mô tả, Số tín chỉ, Trạng thái (Hoạt động/Không hoạt động).
- Xem danh sách môn học, bao gồm khả năng phân trang và sắp xếp cơ bản.
- Xem chi tiết thông tin của một môn học cụ thể.
- Cập nhật thông tin cho một môn học hiện có.
- Xóa môn học (thực hiện xóa mềm, tức là thay đổi trạng thái thành "Không hoạt động").
- Tìm kiếm môn học theo Mã môn học và Tên môn học.
- Xác thực dữ liệu đầu vào cho tất cả các trường thông tin môn học.
- Ghi nhận lịch sử thao tác (Audit Trail) cho các thay đổi quan trọng.

### 2.2 Out of Scope (Ngoài phạm vi)
- Quản lý học liệu, tài liệu đính kèm hoặc tài nguyên liên quan đến từng môn học.
- Chức năng phân công giáo viên giảng dạy cho các môn học.
- Tích hợp với hệ thống xếp thời khóa biểu tự động hoặc hệ thống quản lý lớp học.
- Quản lý điểm số, kết quả học tập của sinh viên theo môn học.
- Chức năng nhập/xuất (Import/Export) dữ liệu môn học hàng loạt.

### 2.3 Giả định (Assumptions)
- Hệ thống `quanlyhocsinh` hiện tại đã có module quản lý người dùng và phân quyền cơ bản hoạt động ổn định.
- Người dùng có vai trò "Admin" hoặc "Chuyên viên phòng Đào tạo" đã được cấp quyền truy cập đầy đủ vào tính năng này.
- Số lượng bản ghi môn học dự kiến không quá lớn trong giai đoạn đầu, không gây ảnh hưởng nghiêm trọng đến hiệu năng tìm kiếm và phân trang mặc định.
- Hệ thống hỗ trợ đầy đủ việc lưu trữ và hiển thị dữ liệu có dấu tiếng Việt.

### 2.4 Phụ thuộc (Dependencies)
- Module quản lý người dùng và phân quyền của hệ thống `quanlyhocsinh` để xác thực và ủy quyền truy cập.
- Cơ sở dữ liệu và các dịch vụ hạ tầng back-end cơ bản đã sẵn sàng và hoạt động.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | **Quản trị viên hệ thống** | Admin | Toàn quyền CRUD môn học, cấu hình hệ thống | Đảm bảo dữ liệu môn học luôn chính xác, hỗ trợ vận hành và quản lý tổng thể hệ thống. |
| 2 | **Chuyên viên phòng Đào tạo** | Quản lý nghiệp vụ | Toàn quyền CRUD môn học, tra cứu, báo cáo | Quản lý danh mục môn học, phục vụ công tác xếp lớp, lên kế hoạch đào tạo, và quản lý chương trình học. |
| 3 | **Giáo viên** | Người dùng cuối | Xem danh sách và chi tiết môn học | Tra cứu thông tin môn học mình giảng dạy hoặc các môn học liên quan để nắm thông tin. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết. Mọi luồng chính (Happy Path) và luồng rẽ nhánh (Edge Cases) cần được thể hiện.

```mermaid
flowchart TD
    A[Bắt đầu] --> B{Người dùng đăng nhập?}
    B -- Có --> C[Kiểm tra quyền truy cập "Quản lý Môn học"]
    B -- Không --> D[Chuyển hướng về trang đăng nhập]
    D --> B

    C -- Không đủ quyền --> E[Hiển thị thông báo lỗi quyền / Chuyển hướng]
    C -- Đủ quyền --> F[Màn hình Danh sách Môn học (có Paging, Search)]

    F --> G{Chọn hành động?}
    G -- Xem chi tiết môn học --> H[Màn hình Chi tiết Môn học]
    G -- Thêm mới môn học --> I[Form Thêm mới Môn học]
    G -- Sửa môn học --> J[Form Cập nhật Môn học]
    G -- Xóa môn học --> K{Xác nhận xóa?}
    G -- Sử dụng Search/Paging --> F

    I --> I1{Dữ liệu hợp lệ theo Business Rules?}
    I1 -- Có --> L[Lưu Môn học vào CSDL (Trạng thái: Hoạt động)]
    I1 -- Không --> I2[Hiển thị lỗi Validation trên Form]
    L --> M[Hiển thị thông báo "Tạo mới thành công"]
    M --> F

    J --> J1{Dữ liệu hợp lệ theo Business Rules?}
    J1 -- Có --> N[Cập nhật Môn học vào CSDL]
    J1 -- Không --> J2[Hiển thị lỗi Validation trên Form]
    N --> O[Hiển thị thông báo "Cập nhật thành công"]
    O --> H

    K -- Có --> P[Cập nhật Trạng thái Môn học thành "Không hoạt động" (Xóa mềm)]
    K -- Không --> F
    P --> Q[Hiển thị thông báo "Xóa thành công"]
    Q --> F

    H --> G
    I2 --> I
    J2 --> J
    E --> Z([Kết thúc])
    M --> Z
    O --> Z
    Q --> Z
    F --> Z
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-154643-FR01` | Tạo mới Môn học | As a Chuyên viên phòng Đào tạo, I want to create a new subject with its details (code, name, description, credits, status) so that I can add new courses to the system. | Must |
| `QUAN-20260531-154643-FR02` | Xem danh sách Môn học | As a Giáo viên, I want to view a paginated list of all active subjects so that I can quickly browse and locate relevant courses. | Must |
| `QUAN-20260531-154643-FR03` | Xem chi tiết Môn học | As a Chuyên viên phòng Đào tạo, I want to view the full detailed information of a specific subject so that I can review its attributes before making changes. | Must |
| `QUAN-20260531-154643-FR04` | Cập nhật Môn học | As an Admin, I want to modify the details of an existing subject so that I can correct errors or update outdated information. | Must |
| `QUAN-20260531-154643-FR05` | Xóa Môn học | As a Chuyên viên phòng Đào tạo, I want to logically delete a subject by setting its status to inactive so that it no longer appears in active lists but can be restored if needed. | Must |
| `QUAN-20260531-154643-FR06` | Tìm kiếm Môn học | As a Chuyên viên phòng Đào tạo, I want to search for subjects using keywords in their code or name so that I can quickly find specific subjects within a large list. | Must |
| `QUAN-20260531-154643-FR07` | Phân trang danh sách Môn học | As a Chuyên viên phòng Đào tạo, I want to view the list of subjects broken down into pages so that I can navigate through a large number of subjects efficiently without overwhelming the interface. | Must |
| `QUAN-20260531-154643-FR08` | Validate dữ liệu Môn học | As a System, I want to validate all input data for subject details against predefined business rules so that data integrity and consistency are maintained. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- Thời gian tải trang danh sách môn học (với 1.000 bản ghi, hiển thị 20 bản ghi/trang) không được vượt quá 2 giây.
- Thời gian phản hồi cho các thao tác CRUD (Tạo mới, Xem chi tiết, Cập nhật, Xóa) không được vượt quá 1 giây trong điều kiện tải bình thường.
- Hệ thống phải có khả năng xử lý đồng thời ít nhất 50 người dùng truy cập và thao tác trên tính năng quản lý môn học mà không làm giảm hiệu năng đáng kể (thời gian phản hồi tăng không quá 20%).

### 6.2 Security
- Tất cả các thao tác CRUD trên module quản lý môn học phải yêu cầu xác thực người dùng và kiểm tra quyền hạn (chỉ các vai trò được phép mới có thể thực hiện).
- Dữ liệu môn học phải được bảo vệ khỏi việc truy cập hoặc sửa đổi trái phép.
- Hệ thống phải được bảo mật chống lại các lỗ hổng phổ biến như SQL Injection và Cross-Site Scripting (XSS) trong quá trình nhập liệu.
- Mọi thông tin nhạy cảm (nếu có) phải được mã hóa khi truyền tải và lưu trữ.

### 6.3 Availability
- Tính năng quản lý môn học phải sẵn sàng hoạt động 99.5% thời gian trong giờ làm việc (từ 8h sáng đến 5h chiều, Thứ Hai đến Thứ Sáu).
- Trong trường hợp có sự cố, hệ thống cần hiển thị thông báo lỗi rõ ràng và thân thiện với người dùng, đồng thời ghi lại chi tiết lỗi vào hệ thống log để hỗ trợ khắc phục.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-154643-BR01` | Mã môn học là duy nhất | Mỗi môn học phải có một Mã môn học duy nhất trong toàn hệ thống để định danh. <br> **VD:** Nếu đã tồn tại môn học có Mã là "MATH101", người dùng không thể tạo thêm môn học mới với Mã cũng là "MATH101". |
| `QUAN-20260531-154643-BR02` | Tên môn học không được trùng | Tên môn học không được phép trùng lặp để tránh nhầm lẫn giữa các môn học. <br> **VD:** Không thể có hai môn học cùng tên "Toán Cao Cấp 1" (ngay cả khi Mã môn học khác nhau). |
| `QUAN-20260531-154643-BR03` | Số tín chỉ phải là số nguyên dương | Số tín chỉ của môn học phải là một số nguyên lớn hơn 0 và nhỏ hơn hoặc bằng 10. <br> **VD:** Đầu vào `3` là hợp lệ. Đầu vào `0`, `-1`, `3.5`, `11` là không hợp lệ. |
| `QUAN-20260531-154643-BR04` | Xóa mềm môn học | Khi một môn học được chọn để "xóa", hệ thống sẽ chỉ đánh dấu môn học đó là không hoạt động (inactive) thay vì xóa vĩnh viễn khỏi cơ sở dữ liệu. <br> **VD:** Môn học có `Trạng thái = Hoạt động` khi xóa sẽ chuyển thành `Trạng thái = Không hoạt động`. Môn học này sẽ không xuất hiện trong danh sách mặc định nhưng có thể được khôi phục bởi Admin. |
| `QUAN-20260531-154643-BR05` | Giới hạn độ dài ký tự | Các trường văn bản có giới hạn độ dài ký tự cụ thể để đảm bảo dữ liệu gọn gàng và tránh tràn bộ nhớ. <br> **VD:** Mã môn học: tối đa 20 ký tự; Tên môn học: tối đa 100 ký tự; Mô tả: tối đa 500 ký tự. |
| `QUAN-20260531-154643-BR06` | Các trường bắt buộc | Các trường Mã môn học, Tên môn học và Số tín chỉ là bắt buộc phải nhập khi tạo mới hoặc cập nhật. <br> **VD:** Nếu người dùng để trống trường "Tên môn học" khi tạo mới, hệ thống sẽ báo lỗi validation và không cho phép lưu. |

---

## 8. Integration Requirements

- Hiện tại, không có yêu cầu tích hợp với các hệ thống bên thứ 3 trong phạm vi của tính năng này.

---

## 9. Audit Trail

- Mọi thao tác tạo mới, cập nhật, và xóa mềm môn học đều phải được ghi lại vào hệ thống nhật ký (Audit Log).
- Thông tin ghi log cần bao gồm: ID người dùng thực hiện thao tác, thời gian thực hiện, loại hành động (Create/Update/Soft Delete), ID của môn học bị tác động, và chi tiết thay đổi (giá trị trước và sau) đối với các trường thông tin chính như Mã môn học, Tên môn học, Số tín chỉ, Trạng thái.

---

## 10. KPI & Metrics theo dõi

- **Số lượng môn học được tạo mới/cập nhật thành công**: Đo lường hoạt động và mức độ sử dụng tính năng.
- **Thời gian phản hồi trung bình của các thao tác CRUD**: Đánh giá hiệu suất của hệ thống.
- **Tỷ lệ lỗi khi nhập liệu (Validation Errors)**: Cho biết chất lượng dữ liệu đầu vào và mức độ thân thiện của giao diện.
- **Số lượt tìm kiếm thành công và tỷ lệ tìm kiếm không có kết quả**: Đánh giá hiệu quả của chức năng tìm kiếm.
- **Số lượt truy cập trang quản lý môn học**: Tổng quan về mức độ sử dụng module này.

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-154643-FR01` | Tạo mới môn học thành công | 1. Người dùng đã đăng nhập với vai trò "Admin".<br>2. Form tạo môn học được điền đầy đủ và hợp lệ (Mã: `CS101`, Tên: `Lập trình cơ bản`, Số tín chỉ: `3`, Mô tả: `Giới thiệu về lập trình`, Trạng thái: `Hoạt động`). | Người dùng bấm nút "Lưu". | 1. Hệ thống hiển thị thông báo "Tạo môn học thành công".<br>2. Môn học "CS101 - Lập trình cơ bản" xuất hiện trong danh sách môn học.<br>3. Dữ liệu môn học được lưu vào cơ sở dữ liệu với trạng thái "Hoạt động". |
| `QUAN-20260531-154643-FR02` | Xem danh sách môn học thành công | 1. Người dùng đã đăng nhập với vai trò "Chuyên viên phòng Đào tạo".<br>2. Có ít nhất 30 môn học đang hoạt động trong hệ thống. | Người dùng truy cập trang "Quản lý Môn học". | 1. Hệ thống hiển thị danh sách các môn học đang hoạt động.<br>2. Danh sách được phân trang, mỗi trang hiển thị 20 môn học đầu tiên.<br>3. Các cột thông tin cơ bản (Mã, Tên, Số tín chỉ, Trạng thái) được hiển thị rõ ràng. |
| `QUAN-20260531-154643-FR03` | Xem chi tiết môn học thành công | 1. Người dùng đã đăng nhập.<br>2. Trên trang danh sách môn học, có một môn học "Toán Cao Cấp 1" với mã "TCC101". | Người dùng bấm vào liên kết/biểu tượng xem chi tiết của môn học "TCC101". | 1. Hệ thống chuyển hướng đến trang chi tiết môn học.<br>2. Trang hiển thị đầy đủ thông tin của môn học "TCC101" bao gồm Mã, Tên, Mô tả, Số tín chỉ, Trạng thái. |
| `QUAN-20260531-154643-FR04` | Cập nhật môn học thành công | 1. Người dùng đã đăng nhập với vai trò "Admin".<br>2. Có môn học "Vật Lý Đại Cương" (VLDC101) với Mô tả là "Môn học cơ bản".<br>3. Người dùng mở form cập nhật môn học VLDC101, thay đổi "Mô tả" thành "Môn học về các định luật vật lý cơ bản". | Người dùng bấm nút "Cập nhật". | 1. Hệ thống hiển thị thông báo "Cập nhật môn học thành công".<br>2. Thông tin "Mô tả" của môn học "VLDC101" được cập nhật trên giao diện và trong cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR05` | Xóa mềm môn học thành công | 1. Người dùng đã đăng nhập với vai trò "Chuyên viên phòng Đào tạo".<br>2. Có môn học "Tin học căn bản" (THCB101) đang ở trạng thái "Hoạt động". | Người dùng bấm nút "Xóa" cho môn THCB101 và xác nhận xóa trong hộp thoại xác nhận. | 1. Hệ thống hiển thị thông báo "Xóa môn học thành công".<br>2. Môn học THCB101 không còn hiển thị trong danh sách mặc định các môn học "Hoạt động".<br>3. Trạng thái của môn học THCB101 trong CSDL được cập nhật thành "Không hoạt động". |
| `QUAN-20260531-154643-FR06` | Tìm kiếm môn học theo tên | 1. Người dùng đã đăng nhập.<br>2. Danh sách môn học có "Toán Cao Cấp 1" và "Toán Cao Cấp 2". | Người dùng nhập từ khóa "Cao Cấp" vào ô tìm kiếm và bấm "Tìm". | 1. Hệ thống hiển thị danh sách chỉ bao gồm "Toán Cao Cấp 1" và "Toán Cao Cấp 2".<br>2. Các môn học khác không chứa từ khóa "Cao Cấp" sẽ không hiển thị. |
| `QUAN-20260531-154643-FR07` | Chuyển trang danh sách | 1. Người dùng đã đăng nhập.<br>2. Danh sách có 30 môn học, mỗi trang hiển thị 20 môn.<br>3. Người dùng đang ở trang 1. | Người dùng bấm vào nút "Trang 2" trên điều hướng phân trang. | 1. Hệ thống hiển thị 10 môn học còn lại của trang 2.<br>2. Nút "Trang 2" được làm nổi bật, nút "Trang 1" không còn nổi bật. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-154643-FR01` | Tạo mới với mã môn học trùng | 1. Người dùng đã đăng nhập.<br>2. Mã môn học "ENG101" đã tồn tại trong hệ thống.<br>3. Người dùng điền form tạo môn học mới với Mã: `ENG101`, Tên: `Tiếng Anh A`, Số tín chỉ: `3`. | Người dùng bấm nút "Lưu". | 1. Hệ thống hiển thị thông báo lỗi "Mã môn học đã tồn tại, vui lòng nhập mã khác".<br>2. Môn học mới không được tạo.<br>3. Dữ liệu không được lưu vào cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR01` | Tạo mới với tên môn học trùng | 1. Người dùng đã đăng nhập.<br>2. Tên môn học "Vật Lý Đại Cương" đã tồn tại (với mã khác, ví dụ: `VLDC101`).<br>3. Người dùng điền form tạo môn học mới với Mã: `PHYS101`, Tên: `Vật Lý Đại Cương`, Số tín chỉ: `3`. | Người dùng bấm nút "Lưu". | 1. Hệ thống hiển thị thông báo lỗi "Tên môn học đã tồn tại, vui lòng nhập tên khác".<br>2. Môn học mới không được tạo.<br>3. Dữ liệu không được lưu vào cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR08` | Bỏ trống trường bắt buộc | 1. Người dùng đã đăng nhập.<br>2. Người dùng mở form tạo môn học và không nhập giá trị cho trường "Tên môn học". | Người dùng bấm nút "Lưu". | 1. Hệ thống hiển thị thông báo lỗi validation "Tên môn học không được để trống" ngay bên dưới trường đó.<br>2. Môn học không được tạo.<br>3. Dữ liệu không được lưu vào cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR08` | Nhập số tín chỉ không hợp lệ | 1. Người dùng đã đăng nhập.<br>2. Người dùng mở form tạo môn học và nhập "0" vào trường "Số tín chỉ". | Người dùng bấm nút "Lưu". | 1. Hệ thống hiển thị thông báo lỗi validation "Số tín chỉ phải là số nguyên dương" ngay bên dưới trường đó.<br>2. Môn học không được tạo.<br>3. Dữ liệu không được lưu vào cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR04` | Cập nhật tên môn học trùng | 1. Người dùng đã đăng nhập với vai trò "Admin".<br>2. Có môn học "Đại Số Tuyến Tính" (DSTT201) và "Giải Tích 1" (GT101).<br>3. Người dùng mở form cập nhật môn học "Đại Số Tuyến Tính", thay đổi "Tên môn học" thành "Giải Tích 1". | Người dùng bấm nút "Cập nhật". | 1. Hệ thống hiển thị thông báo lỗi "Tên môn học đã tồn tại, vui lòng nhập tên khác".<br>2. Thông tin môn học "Đại Số Tuyến Tính" không được cập nhật. |
| `QUAN-20260531-154643-FR05` | Hủy xóa môn học | 1. Người dùng đã đăng nhập.<br>2. Có môn học "Hóa học" (HH101) đang hoạt động. | Người dùng bấm nút "Xóa" cho môn HH101, sau đó bấm "Hủy" trong hộp thoại xác nhận. | 1. Hệ thống không hiển thị thông báo xóa thành công.<br>2. Môn học HH101 vẫn hiển thị trong danh sách và trạng thái không đổi.<br>3. Trạng thái của môn học HH101 trong CSDL không thay đổi. |
| `QUAN-20260531-154643-FR06` | Tìm kiếm không có kết quả | 1. Người dùng đã đăng nhập.<br>2. Danh sách môn học không chứa bất kỳ môn nào có từ khóa "Thiết kế đồ họa". | Người dùng nhập "Thiết kế đồ họa" vào ô tìm kiếm và bấm "Tìm". | 1. Hệ thống hiển thị thông báo "Không tìm thấy môn học nào phù hợp với từ khóa bạn tìm kiếm".<br>2. Danh sách môn học hiển thị rỗng hoặc không có kết quả. |

---

_Mã tính năng `QUAN-20260531-154643` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._