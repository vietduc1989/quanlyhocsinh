# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-1452` |
| **Tên tính năng** | Quan Ly Mon Hoc |
| **Dự án** | quanlyhocsinh |
| **Phiên bản** | 1.0 |
| **Ngày tạo** | 2026-05-31 |
| **Tác giả** | BA Agent (ONENET AgentFactory) |

## Tài liệu liên quan

| Mã tính năng | Loại tài liệu | Trạng thái |
|-------------|---------------|------------|
| `QUAN-20260531-1452` | **BRD** (tài liệu này) | ✅ Hiện tại |
| `QUAN-20260531-1452` | SRS/SAD | ⏳ Chờ BA duyệt |
| `QUAN-20260531-1452` | DEV (mã nguồn) | ⏳ Chờ SRS duyệt |
| `QUAN-20260531-1452` | TEST (test cases) | ⏳ Chờ DEV duyệt |

---

## 1. Mục tiêu

### 1.1 Mục tiêu chính
Cung cấp một hệ thống quản lý môn học hiệu quả, giúp cán bộ quản lý có thể dễ dàng tạo, xem, cập nhật, xóa, tìm kiếm và phân trang thông tin các môn học trong hệ thống `quanlyhocsinh`.

### 1.2 Mục tiêu phụ
- Đảm bảo tính nhất quán và chính xác của dữ liệu môn học.
- Nâng cao hiệu suất công việc cho người dùng bằng cách tự động hóa các thao tác quản lý môn học.
- Hỗ trợ việc tích hợp thông tin môn học vào các module khác của hệ thống (ví dụ: quản lý lớp học, quản lý thời khóa biểu).
- Cung cấp khả năng truy xuất thông tin môn học nhanh chóng thông qua chức năng tìm kiếm và phân trang.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới thông tin môn học với các trường dữ liệu cần thiết.
- Xem danh sách tất cả các môn học hiện có trong hệ thống.
- Xem chi tiết thông tin của một môn học cụ thể.
- Cập nhật thông tin của một môn học đã tồn tại.
- Xóa một môn học khỏi hệ thống.
- Tìm kiếm môn học theo các tiêu chí như mã môn học, tên môn học.
- Phân trang kết quả danh sách môn học.
- Kiểm tra tính hợp lệ của dữ liệu đầu vào (validation) khi tạo mới và cập nhật.

### 2.2 Out of Scope (Ngoài phạm vi)
- Gán môn học cho giáo viên hoặc lớp học.
- Quản lý học liệu, tài liệu đính kèm cho môn học.
- Xuất/nhập khẩu (Export/Import) danh sách môn học từ/đến các định dạng file khác (ví dụ: Excel, PDF).
- Phân quyền chi tiết cho từng hành động trên môn học (ngoại trừ quyền Admin tổng quát).
- Lịch sử thay đổi chi tiết từng trường dữ liệu của môn học.

### 2.3 Giả định (Assumptions)
- Người dùng có quyền truy cập module "Quản lý môn học" đã được xác thực và phân quyền (ví dụ: vai trò Quản trị viên, Cán bộ quản lý).
- Hệ thống cơ sở dữ liệu đã sẵn sàng để lưu trữ thông tin môn học.
- Môi trường mạng ổn định và người dùng sử dụng trình duyệt web tiêu chuẩn.
- Mã môn học là duy nhất trong hệ thống.

### 2.4 Phụ thuộc (Dependencies)
- **Module Xác thực & Phân quyền:** Cần có hệ thống xác thực người dùng và phân quyền để đảm bảo chỉ những người dùng có vai trò phù hợp mới có thể truy cập và thực hiện các thao tác quản lý môn học.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | **Cán bộ quản lý / Quản trị viên** | Quản lý hệ thống, quản lý dữ liệu nghiệp vụ | Toàn quyền CRUD, tìm kiếm, phân trang trên dữ liệu môn học | Quản lý thông tin môn học một cách hiệu quả để hỗ trợ công tác giảng dạy và học tập; đảm bảo dữ liệu chính xác và cập nhật. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết. Mọi luồng chính (Happy Path) và luồng rẽ nhánh (Edge Cases) cần được thể hiện.

```mermaid
flowchart TD
    Start([Bắt đầu]) --> Login[Đăng nhập hệ thống]
    Login -- Thành công --> Dashboard[Dashboard]
    Dashboard --> Navigate[Chọn "Quản lý Môn học"]
    Navigate --> ListSubjects[Hiển thị Danh sách Môn học (có phân trang)]

    ListSubjects -- "Xem chi tiết" --> ViewSubjectDetail[Xem Chi tiết Môn học]
    ListSubjects -- "Tìm kiếm" --> SearchSubjects[Thực hiện Tìm kiếm]
    SearchSubjects -- Kết quả --> ListSubjects
    SearchSubjects -- Không có kết quả --> NoResult[Hiển thị "Không có dữ liệu"]
    ViewSubjectDetail --> ListSubjects
    
    ListSubjects -- "Tạo mới" --> CreateForm[Mở Form Tạo mới Môn học]
    CreateForm -- "Điền thông tin hợp lệ" --> SubmitCreate[Bấm "Lưu"]
    SubmitCreate -- "Thành công" --> SaveSuccess[Thông báo thành công]
    SaveSuccess --> ListSubjects
    SubmitCreate -- "Validation lỗi" --> CreateFormError[Hiển thị lỗi Validation trên Form]
    CreateForm -- "Hủy" --> ListSubjects

    ListSubjects -- "Chỉnh sửa" --> EditForm[Mở Form Chỉnh sửa Môn học (điền sẵn data)]
    EditForm -- "Điền thông tin hợp lệ" --> SubmitEdit[Bấm "Cập nhật"]
    SubmitEdit -- "Thành công" --> UpdateSuccess[Thông báo thành công]
    UpdateSuccess --> ListSubjects
    SubmitEdit -- "Validation lỗi" --> EditFormError[Hiển thị lỗi Validation trên Form]
    EditForm -- "Hủy" --> ListSubjects

    ListSubjects -- "Xóa" --> ConfirmDelete[Xác nhận Xóa Môn học?]
    ConfirmDelete -- "Xác nhận" --> PerformDelete[Thực hiện Xóa]
    PerformDelete -- "Thành công" --> DeleteSuccess[Thông báo thành công]
    DeleteSuccess --> ListSubjects
    PerformDelete -- "Lỗi (đang sử dụng)" --> DeleteError[Thông báo lỗi: "Môn học đang được sử dụng"]
    DeleteError --> ListSubjects
    ConfirmDelete -- "Hủy" --> ListSubjects
    
    NoResult --> ListSubjects
    
    ListSubjects --> End([Kết thúc])
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-1452-FR01` | Tạo mới Môn học | As a Cán bộ quản lý, I want to tạo mới một môn học với đầy đủ thông tin so that tôi có thể thêm môn học vào hệ thống để quản lý. | Must |
| `QUAN-20260531-1452-FR02` | Xem danh sách Môn học | As a Cán bộ quản lý, I want to xem danh sách các môn học hiện có so that tôi có cái nhìn tổng quan về các môn học và có thể thực hiện các thao tác tiếp theo. | Must |
| `QUAN-20260531-1452-FR03` | Xem chi tiết Môn học | As a Cán bộ quản lý, I want to xem chi tiết thông tin của một môn học cụ thể so that tôi có thể kiểm tra và xác nhận các dữ liệu liên quan. | Must |
| `QUAN-20260531-1452-FR04` | Cập nhật Môn học | As a Cán bộ quản lý, I want to cập nhật thông tin của một môn học đã tồn tại so that tôi có thể chỉnh sửa các sai sót hoặc thay đổi dữ liệu khi cần thiết. | Must |
| `QUAN-20260531-1452-FR05` | Xóa Môn học | As a Cán bộ quản lý, I want to xóa một môn học khỏi hệ thống so that tôi có thể loại bỏ các môn học không còn sử dụng hoặc đã nhập sai. | Must |
| `QUAN-20260531-1452-FR06` | Tìm kiếm Môn học | As a Cán bộ quản lý, I want to tìm kiếm môn học theo mã hoặc tên so that tôi có thể nhanh chóng định vị môn học cần quản lý. | Must |
| `QUAN-20260531-1452-FR07` | Phân trang danh sách Môn học | As a Cán bộ quản lý, I want to xem danh sách môn học được phân trang so that tôi có thể duyệt qua lượng lớn dữ liệu một cách hiệu quả mà không bị quá tải. | Must |
| `QUAN-20260531-1452-FR08` | Kiểm tra hợp lệ dữ liệu | As a Cán bộ quản lý, I want to nhận được thông báo lỗi khi nhập dữ liệu không hợp lệ so that tôi có thể sửa lỗi và đảm bảo tính chính xác của dữ liệu môn học. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian phản hồi:**
    - Trang danh sách môn học (với 1000 bản ghi) phải tải trong vòng 3 giây.
    - Các thao tác tạo mới, cập nhật, xóa môn học phải hoàn tất trong vòng 1 giây (không bao gồm thời gian tải trang).
    - Chức năng tìm kiếm phải trả về kết quả trong vòng 1 giây.
- **Khả năng chịu tải:** Hệ thống phải hỗ trợ ít nhất 50 người dùng đồng thời truy cập và thực hiện các thao tác quản lý môn học mà không ảnh hưởng đáng kể đến hiệu suất.

### 6.2 Security
- **Xác thực & Phân quyền:** Chỉ những người dùng đã đăng nhập với vai trò "Cán bộ quản lý" hoặc "Quản trị viên" mới có thể truy cập và sử dụng tính năng quản lý môn học. Các quyền CRUD cần được kiểm tra chặt chẽ dựa trên vai trò.
- **Bảo mật dữ liệu:** Dữ liệu môn học phải được bảo vệ khỏi truy cập trái phép. Truyền dữ liệu giữa client và server phải sử dụng giao thức HTTPS.
- **Ngăn chặn tấn công:** Hệ thống phải có cơ chế ngăn chặn các loại tấn công phổ biến như SQL Injection, XSS, CSRF thông qua việc sử dụng các framework bảo mật và mã hóa đầu vào người dùng.

### 6.3 Availability
- **Thời gian hoạt động (Uptime):** Tính năng quản lý môn học phải khả dụng 99.5% thời gian trong tháng (ngoại trừ thời gian bảo trì định kỳ).

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-1452-BR01` | Tên môn học không được để trống | **Mô tả:** Trường "Tên Môn học" là bắt buộc khi tạo mới hoặc cập nhật. <br> **VD Lỗi:** Input `Tên môn học = ""`, Output: "Tên môn học không được để trống." |
| `QUAN-20260531-1452-BR02` | Mã môn học không được để trống | **Mô tả:** Trường "Mã Môn học" là bắt buộc khi tạo mới hoặc cập nhật. <br> **VD Lỗi:** Input `Mã môn học = ""`, Output: "Mã môn học không được để trống." |
| `QUAN-20260531-1452-BR03` | Tên môn học phải là duy nhất | **Mô tả:** Không được phép có hai môn học có cùng tên. <br> **VD Lỗi:** Môn "Toán" đã tồn tại. Input `Tên môn học = "Toán"`, Output: "Tên môn học đã tồn tại trong hệ thống." |
| `QUAN-20260531-1452-BR04` | Mã môn học phải là duy nhất | **Mô tả:** Không được phép có hai môn học có cùng mã. <br> **VD Lỗi:** Môn "T01" đã tồn tại. Input `Mã môn học = "T01"`, Output: "Mã môn học đã tồn tại trong hệ thống." |
| `QUAN-20260531-1452-BR05` | Độ dài tối đa của Tên môn học | **Mô tả:** Tên môn học không được vượt quá 255 ký tự. <br> **VD Lỗi:** Input `Tên môn học = "A... (256 ký tự)"`, Output: "Tên môn học không được vượt quá 255 ký tự." |
| `QUAN-20260531-1452-BR06` | Độ dài tối đa của Mã môn học | **Mô tả:** Mã môn học không được vượt quá 50 ký tự. <br> **VD Lỗi:** Input `Mã môn học = "ABC... (51 ký tự)"`, Output: "Mã môn học không được vượt quá 50 ký tự." |
| `QUAN-20260531-1452-BR07` | Không thể xóa môn học đang được sử dụng | **Mô tả:** Một môn học không thể bị xóa nếu nó đang được gán cho bất kỳ lớp học hoặc chương trình học nào. <br> **VD Lỗi:** Môn học "Văn học" đang được gán cho lớp "10A1". Người dùng cố gắng xóa môn "Văn học". Output: "Môn học này đang được sử dụng và không thể xóa." |
| `QUAN-20260531-1452-BR08` | Giá trị mặc định của Trạng thái | **Mô tả:** Khi tạo mới, trạng thái mặc định của môn học là "Hoạt động". <br> **VD:** Khi tạo môn học mới "Lịch sử", trường trạng thái sẽ tự động được gán là "Hoạt động" nếu người dùng không chọn giá trị khác (nếu có tùy chọn). |

---

## 8. Integration Requirements

- Không có yêu cầu tích hợp với hệ thống bên thứ 3 cụ thể cho tính năng quản lý môn học trong giai đoạn này. Tính năng này sẽ tích hợp nội bộ với các module khác của hệ thống `quanlyhocsinh` khi các module đó yêu cầu dữ liệu môn học.

---

## 9. Audit Trail

- Hệ thống phải ghi lại nhật ký (log) cho tất cả các thao tác tạo mới, cập nhật và xóa môn học.
- Nhật ký cần bao gồm:
    - Thời gian thực hiện thao tác.
    - ID của người dùng thực hiện thao tác.
    - Loại thao tác (CREATE, UPDATE, DELETE).
    - ID của môn học bị tác động.
    - Nội dung thay đổi (đặc biệt cho thao tác UPDATE, cần ghi lại các trường dữ liệu bị thay đổi).

---

## 10. KPI & Metrics theo dõi

- **Số lượng môn học được tạo mới:** Đo lường tần suất thêm môn học vào hệ thống.
- **Tỷ lệ môn học được cập nhật:** Đo lường tần suất chỉnh sửa thông tin môn học.
- **Số lượt tìm kiếm thành công:** Đánh giá hiệu quả và mức độ sử dụng của chức năng tìm kiếm.
- **Thời gian phản hồi trung bình:** Theo dõi hiệu suất của các chức năng CRUD, tìm kiếm, và tải danh sách.
- **Tỷ lệ lỗi khi nhập liệu:** Đo lường số lần người dùng gặp lỗi validation, phản ánh chất lượng dữ liệu đầu vào.

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1452-FR01` | Tạo mới môn học thành công | Đã đăng nhập với vai trò Cán bộ quản lý. Trên form tạo mới môn học, các trường "Mã môn học" (M001) và "Tên môn học" (Toán học) được điền hợp lệ và chưa tồn tại. | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo "Tạo môn học thành công." và môn học mới được thêm vào danh sách. |
| `QUAN-20260531-1452-FR02` | Xem danh sách môn học | Đã đăng nhập với vai trò Cán bộ quản lý. Có ít nhất 3 môn học đã tồn tại trong hệ thống. | Người dùng truy cập trang "Quản lý Môn học". | Hệ thống hiển thị danh sách các môn học, mỗi môn học có Mã, Tên và các thao tác (Xem, Sửa, Xóa). |
| `QUAN-20260531-1452-FR03` | Xem chi tiết môn học | Đã đăng nhập với vai trò Cán bộ quản lý. Có một môn học (Mã: L001, Tên: Ngữ văn) tồn tại trong hệ thống. | Người dùng bấm vào nút "Xem chi tiết" của môn "Ngữ văn" trên danh sách. | Hệ thống hiển thị trang chi tiết môn học với đầy đủ thông tin của môn "Ngữ văn". |
| `QUAN-20260531-1452-FR04` | Cập nhật môn học thành công | Đã đăng nhập với vai trò Cán bộ quản lý. Môn học "Toán học" (Mã: M001) đang tồn tại. Trên form chỉnh sửa, "Tên môn học" được đổi thành "Toán học nâng cao". | Người dùng bấm nút "Cập nhật". | Hệ thống hiển thị thông báo "Cập nhật môn học thành công." và thông tin môn học "M001" trong danh sách được cập nhật thành "Toán học nâng cao". |
| `QUAN-20260531-1452-FR05` | Xóa môn học thành công | Đã đăng nhập với vai trò Cán bộ quản lý. Có một môn học (Mã: H001, Tên: Lịch sử) không liên kết với bất kỳ lớp học nào. | Người dùng bấm nút "Xóa" của môn "Lịch sử" và xác nhận thao tác. | Hệ thống hiển thị thông báo "Xóa môn học thành công." và môn "Lịch sử" không còn xuất hiện trong danh sách. |
| `QUAN-20260531-1452-FR06` | Tìm kiếm môn học theo tên | Đã đăng nhập với vai trò Cán bộ quản lý. Có các môn học "Vật lý", "Hóa học", "Sinh học" trong hệ thống. | Người dùng nhập "Lý" vào ô tìm kiếm và bấm nút "Tìm kiếm". | Hệ thống hiển thị danh sách chỉ chứa môn học "Vật lý". |
| `QUAN-20260531-1452-FR07` | Phân trang danh sách môn học | Đã đăng nhập với vai trò Cán bộ quản lý. Có 25 môn học trong hệ thống và cấu hình mặc định hiển thị 10 bản ghi/trang. | Người dùng truy cập trang "Quản lý Môn học". | Hệ thống hiển thị 10 môn học đầu tiên và các tùy chọn phân trang (ví dụ: Trang 1/3, nút chuyển trang). |
| `QUAN-20260531-1452-FR08` | Hiển thị thông báo validation | Đã đăng nhập với vai trò Cán bộ quản lý. Trên form tạo mới môn học, trường "Tên môn học" bị bỏ trống. | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Tên môn học không được để trống." ngay dưới trường nhập liệu. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1452-FR01` | Tạo mới môn học với tên trùng | Đã đăng nhập, quyền Cán bộ quản lý. Môn học "Địa lý" đã tồn tại. Trên form tạo mới, điền "Mã môn học" (Đ002), "Tên môn học" (Địa lý). | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Tên môn học đã tồn tại trong hệ thống." |
| `QUAN-20260531-1452-FR01` | Tạo mới môn học với mã trùng | Đã đăng nhập, quyền Cán bộ quản lý. Môn học có Mã "A001" đã tồn tại. Trên form tạo mới, điền "Mã môn học" (A001), "Tên môn học" (Âm nhạc). | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Mã môn học đã tồn tại trong hệ thống." |
| `QUAN-20260531-1452-FR04` | Cập nhật môn học với tên trùng | Đã đăng nhập, quyền Cán bộ quản lý. Môn học "Vật lý" và "Hóa học" đang tồn tại. Người dùng chỉnh sửa môn "Vật lý", đổi tên thành "Hóa học". | Người dùng bấm nút "Cập nhật". | Hệ thống hiển thị thông báo lỗi "Tên môn học đã tồn tại trong hệ thống." |
| `QUAN-20260531-1452-FR05` | Xóa môn học đang được sử dụng | Đã đăng nhập với vai trò Cán bộ quản lý. Môn học "Ngữ văn" (Mã: L001) đang được gán cho lớp "11A3". | Người dùng bấm nút "Xóa" của môn "Ngữ văn" và xác nhận. | Hệ thống hiển thị thông báo lỗi "Môn học này đang được sử dụng và không thể xóa." và môn học vẫn còn trong danh sách. |
| `QUAN-20260531-1452-FR06` | Tìm kiếm không có kết quả | Đã đăng nhập với vai trò Cán bộ quản lý. Không có môn học nào tên "Tiếng Lào" trong hệ thống. | Người dùng nhập "Tiếng Lào" vào ô tìm kiếm và bấm nút "Tìm kiếm". | Hệ thống hiển thị thông báo "Không tìm thấy môn học nào phù hợp với từ khóa." hoặc một giao diện trống với thông báo tương tự. |
| `QUAN-20260531-1452-FR07` | Chuyển trang cuối khi không đủ dữ liệu | Đã đăng nhập với vai trò Cán bộ quản lý. Có 15 môn học trong hệ thống, cấu hình hiển thị 10 bản ghi/trang. | Người dùng đang ở trang 2 và bấm nút "Trang tiếp theo". | Nút "Trang tiếp theo" bị vô hiệu hóa hoặc không có tác dụng, và người dùng vẫn ở trang 2. |
| `QUAN-20260531-1452-FR08` | Nhập dữ liệu vượt quá độ dài tối đa | Đã đăng nhập với vai trò Cán bộ quản lý. Trên form tạo mới môn học, trường "Tên môn học" điền chuỗi dài 260 ký tự. | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Tên môn học không được vượt quá 255 ký tự." |

---

_Mã tính năng `QUAN-20260531-1452` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._