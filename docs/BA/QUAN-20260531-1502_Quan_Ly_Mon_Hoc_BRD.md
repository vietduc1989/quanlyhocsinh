# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-1502` |
| **Tên tính năng** | Quan Ly Mon Hoc |
| **Dự án** | quanlyhocsinh |
| **Phiên bản** | 1.0 |
| **Ngày tạo** | 2026-05-31 |
| **Tác giả** | BA Agent (ONENET AgentFactory) |

## Tài liệu liên quan

| Mã tính năng | Loại tài liệu | Trạng thái |
|-------------|---------------|------------|
| `QUAN-20260531-1502` | **BRD** (tài liệu này) | ✅ Hiện tại |
| `QUAN-20260531-1502` | SRS/SAD | ⏳ Chờ BA duyệt |
| `QUAN-20260531-1502` | DEV (mã nguồn) | ⏳ Chờ SRS duyệt |
| `QUAN-20260531-1502` | TEST (test cases) | ⏳ Chờ DEV duyệt |

---

## 1. Mục tiêu

### 1.1 Mục tiêu chính
Cung cấp khả năng quản lý thông tin các môn học một cách hiệu quả, chính xác và có hệ thống trong ứng dụng quản lý học sinh, phục vụ công tác quản lý đào tạo.

### 1.2 Mục tiêu phụ
- Nâng cao tính toàn vẹn và nhất quán của dữ liệu môn học.
- Hỗ trợ cán bộ quản lý đào tạo dễ dàng tạo, xem, cập nhật và xóa thông tin môn học.
- Tối ưu hóa quá trình tra cứu thông tin môn học thông qua chức năng tìm kiếm và phân trang.
- Đảm bảo dữ liệu môn học được kiểm tra tính hợp lệ trước khi lưu trữ.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới thông tin môn học (bao gồm mã môn học, tên môn học, số tín chỉ, mô tả).
- Xem danh sách các môn học, bao gồm khả năng phân trang.
- Tìm kiếm môn học theo mã môn học và tên môn học.
- Xem chi tiết thông tin của một môn học cụ thể.
- Cập nhật thông tin của một môn học hiện có.
- Xóa một môn học khỏi hệ thống.
- Kiểm tra tính hợp lệ (validation) của dữ liệu nhập liệu cho các trường thông tin môn học.

### 2.2 Out of Scope (Ngoài phạm vi)
- Import/Export dữ liệu môn học từ/ra file (Excel, CSV, v.v.).
- Liên kết môn học với giáo viên, lớp học hoặc chương trình đào tạo (sẽ là các tính năng riêng).
- Thống kê, báo cáo chuyên sâu liên quan đến môn học.
- Quản lý các phiên bản môn học hoặc lịch sử thay đổi quá phức tạp.

### 2.3 Giả định (Assumptions)
- Người dùng truy cập tính năng này đã được xác thực và có quyền hạn phù hợp (ví dụ: quản trị viên, cán bộ quản lý đào tạo).
- Hệ thống đã có sẵn các thành phần giao diện người dùng cơ bản (ví dụ: form nhập liệu, bảng hiển thị dữ liệu).
- Mã môn học là duy nhất trong toàn hệ thống.
- Người dùng có kết nối mạng ổn định để tương tác với hệ thống.

### 2.4 Phụ thuộc (Dependencies)
- Module xác thực và phân quyền người dùng (Authentication & Authorization) đã được triển khai và hoạt động ổn định.
- Cơ sở dữ liệu đã được khởi tạo và sẵn sàng để lưu trữ dữ liệu môn học.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | Cán bộ Quản lý Đào tạo | Cán bộ hành chính / Quản lý | Toàn quyền CRUD môn học, xem danh sách, tìm kiếm, phân trang | Đảm bảo danh mục môn học chính xác, đầy đủ; dễ dàng cập nhật theo chương trình đào tạo mới. |
| 2 | Quản trị viên hệ thống | Quản lý hệ thống | Toàn quyền CRUD môn học, xem danh sách, tìm kiếm, phân trang | Giám sát và duy trì tính toàn vẹn dữ liệu hệ thống, hỗ trợ người dùng khi cần. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết mô tả quy trình quản lý môn học tổng quát.

```mermaid
flowchart TD
    A[Người dùng đăng nhập] --> B{Có quyền quản lý môn học?}
    B -- Có --> C[Truy cập trang Quản lý Môn học]
    C --> D[Hiển thị danh sách môn học (có phân trang)]
    D --> E{Người dùng muốn thực hiện hành động gì?}

    E -- Tìm kiếm --> F[Nhập tiêu chí tìm kiếm]
    F --> G[Hiển thị kết quả tìm kiếm]
    G --> D

    E -- Thêm mới môn học --> H[Hiển thị form Thêm mới]
    H --> I[Nhập thông tin môn học]
    I --> J{Thông tin hợp lệ?}
    J -- Có --> K[Lưu môn học vào hệ thống]
    K -- Thành công --> L[Hiển thị thông báo thành công]
    K -- Lỗi --> M[Hiển thị thông báo lỗi hệ thống]
    J -- Không --> N[Hiển thị lỗi validation trên form]
    L --> D
    M --> H
    N --> H

    E -- Xem chi tiết/Sửa môn học --> O[Chọn môn học từ danh sách]
    O --> P[Hiển thị form Chi tiết/Sửa với dữ liệu có sẵn]
    P --> Q{Người dùng muốn sửa?}
    Q -- Có --> R[Thay đổi thông tin môn học]
    R --> S{Thông tin hợp lệ?}
    S -- Có --> T[Cập nhật môn học vào hệ thống]
    T -- Thành công --> U[Hiển thị thông báo thành công]
    T -- Lỗi --> V[Hiển thị thông báo lỗi hệ thống]
    S -- Không --> W[Hiển thị lỗi validation trên form]
    U --> D
    V --> P
    W --> P
    Q -- Không --> D

    E -- Xóa môn học --> X[Chọn môn học từ danh sách]
    X --> Y[Yêu cầu xác nhận xóa]
    Y -- Xác nhận --> Z[Thực hiện xóa môn học]
    Z -- Thành công --> AA[Hiển thị thông báo thành công]
    Z -- Lỗi --> BB[Hiển thị thông báo lỗi]
    AA --> D
    BB --> D

    B -- Không --> CC[Hiển thị thông báo lỗi quyền]
    CC --> End([Kết thúc])
    D --> End([Kết thúc])
```

---

## 5. Functional Requirements (FR)

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-1502-FR01` | Tạo mới Môn học | As a Cán bộ Quản lý Đào tạo, I want to tạo mới một môn học so that thông tin môn học được lưu trữ vào hệ thống. | Must |
| `QUAN-20260531-1502-FR02` | Xem danh sách Môn học | As a Cán bộ Quản lý Đào tạo, I want to xem danh sách các môn học (kèm phân trang, tìm kiếm) so that tôi có thể dễ dàng tra cứu và quản lý. | Must |
| `QUAN-20260531-1502-FR03` | Xem chi tiết Môn học | As a Cán bộ Quản lý Đào tạo, I want to xem chi tiết thông tin của một môn học cụ thể so that tôi có thể nắm rõ tất cả các thuộc tính của nó. | Must |
| `QUAN-20260531-1502-FR04` | Cập nhật Môn học | As a Cán bộ Quản lý Đào tạo, I want to cập nhật thông tin của một môn học hiện có so that dữ liệu được duy trì chính xác và cập nhật. | Must |
| `QUAN-20260531-1502-FR05` | Xóa Môn học | As a Cán bộ Quản lý Đào tạo, I want to xóa một môn học không còn cần thiết so that hệ thống loại bỏ dữ liệu không sử dụng. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian phản hồi**:
    - Khi tải danh sách môn học với 1.000 bản ghi: < 2 giây.
    - Khi thực hiện các thao tác Tạo, Cập nhật, Xóa một môn học: < 1 giây.
- **Khả năng chịu tải**: Hệ thống phải hỗ trợ ít nhất 100 người dùng đồng thời truy cập và thực hiện các thao tác quản lý môn học mà không bị suy giảm hiệu suất đáng kể.

### 6.2 Security
- **Phân quyền**: Chỉ những người dùng có vai trò "Cán bộ Quản lý Đào tạo" hoặc "Quản trị viên hệ thống" mới có quyền truy cập và thực hiện các thao tác CRUD đối với môn học.
- **Xác thực**: Tất cả các yêu cầu truy cập và thao tác trên dữ liệu môn học phải được xác thực hợp lệ.
- **Mã hóa dữ liệu**: Dữ liệu môn học phải được mã hóa khi truyền tải giữa client và server (sử dụng HTTPS).
- **Phòng chống lỗ hổng**: Hệ thống phải có khả năng phòng chống các lỗ hổng bảo mật phổ biến như SQL Injection, XSS, CSRF.

### 6.3 Availability
- **Thời gian hoạt động (Uptime)**: Tính năng Quản lý Môn học phải hoạt động liên tục 24/7 với thời gian uptime tối thiểu 99.5%.
- **Khả năng phục hồi**: Trong trường hợp xảy ra lỗi hệ thống, tính năng phải có khả năng phục hồi hoạt động trong vòng 30 phút mà không làm mất dữ liệu đã được lưu thành công.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-1502-BR01` | Mã môn học là duy nhất | **Mô tả**: Mỗi môn học phải có một mã duy nhất trong toàn hệ thống. Hệ thống sẽ báo lỗi nếu người dùng nhập mã đã tồn tại khi tạo mới hoặc cập nhật. <br> **VD**: Nếu đã có môn học với `Mã môn học = "CS101"`, người dùng không thể tạo thêm môn học khác với cùng mã này. Khi cập nhật, nếu đổi mã thành "CS101" mà mã này đã tồn tại cho môn học khác, hệ thống sẽ báo lỗi. |
| `QUAN-20260531-1502-BR02` | Tên môn học là bắt buộc | **Mô tả**: Tên môn học không được để trống khi tạo mới hoặc cập nhật. <br> **VD**: Nếu người dùng cố gắng lưu một môn học mà trường "Tên môn học" trống, hệ thống sẽ hiển thị thông báo lỗi yêu cầu nhập tên. |
| `QUAN-20260531-1502-BR03` | Số tín chỉ phải là số nguyên dương | **Mô tả**: Số tín chỉ của môn học phải là một số nguyên lớn hơn hoặc bằng 1. <br> **VD**: Nếu người dùng nhập `Số tín chỉ = 0`, `-2`, `3.5` hoặc bỏ trống, hệ thống sẽ hiển thị thông báo lỗi yêu cầu nhập số nguyên dương. |
| `QUAN-20260531-1502-BR04` | Độ dài tối đa của Tên môn học | **Mô tả**: Tên môn học không được vượt quá 255 ký tự. <br> **VD**: Nếu người dùng nhập tên môn học dài hơn 255 ký tự, hệ thống sẽ hiển thị thông báo lỗi. |

---

## 8. Integration Requirements

- N/A (Tính năng Quản lý Môn học hoạt động độc lập trong phạm vi dự án quanlyhocsinh và không yêu cầu tích hợp với các hệ thống bên thứ ba trong giai đoạn hiện tại).

---

## 9. Audit Trail

- Mọi thao tác tạo mới, cập nhật và xóa môn học phải được ghi lại vào nhật ký hệ thống (audit log).
- Thông tin cần ghi lại cho mỗi thao tác:
    - ID của người dùng thực hiện thao tác.
    - Thời gian thực hiện thao tác (timestamp).
    - Loại thao tác (Tạo mới, Cập nhật, Xóa).
    - ID của môn học bị ảnh hưởng.
    - Dữ liệu cũ của môn học (chỉ đối với thao tác Cập nhật).
    - Dữ liệu mới của môn học (đối với Tạo mới và Cập nhật).
    - Địa chỉ IP của người dùng thực hiện.

---

## 10. KPI & Metrics theo dõi

- **Số lượng môn học được tạo/cập nhật/xóa**: Tổng số lượng thao tác CRUD thành công mỗi tuần/tháng.
- **Tỷ lệ lỗi khi thực hiện thao tác**: Phần trăm các thao tác CRUD thất bại do lỗi hệ thống hoặc validation.
- **Thời gian phản hồi trung bình**: Thời gian trung bình để hệ thống phản hồi khi người dùng thực hiện các thao tác chính (tạo, cập nhật, tải danh sách).
- **Tần suất sử dụng tính năng**: Số lượng lượt xem trang quản lý môn học hoặc số lượng người dùng độc lập truy cập tính năng mỗi ngày/tuần.

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1502-FR01` | Tạo mới môn học thành công | Người dùng là Cán bộ QLĐT, đang ở trang "Thêm mới môn học".<br>Đã nhập đầy đủ và hợp lệ các thông tin: <br>- Mã môn học: "MKT101" <br>- Tên môn học: "Marketing Cơ bản" <br>- Số tín chỉ: "3" <br>- Mô tả: "Giới thiệu về Marketing" | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Thêm môn học thành công".<br>Môn học "Marketing Cơ bản" xuất hiện trong danh sách môn học.<br>Dữ liệu được lưu trữ chính xác vào cơ sở dữ liệu. |
| `QUAN-20260531-1502-FR02` | Xem danh sách môn học và phân trang | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Hệ thống có > 100 môn học đã lưu. | Hệ thống tải trang "Quản lý môn học". | Hệ thống hiển thị danh sách môn học, chỉ 20 môn học đầu tiên trên trang 1.<br>Có các điều khiển phân trang (trang 1, 2, 3..., tiếp theo, trước đó). |
| `QUAN-20260531-1502-FR02` | Tìm kiếm môn học theo tên | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Danh sách có môn học "Toán cao cấp". | Người dùng nhập "Toán" vào ô tìm kiếm và nhấn nút "Tìm". | Hệ thống hiển thị danh sách chỉ chứa các môn học có tên chứa từ "Toán", ví dụ: "Toán cao cấp", "Toán rời rạc".<br>Danh sách kết quả tìm kiếm được phân trang (nếu có nhiều kết quả). |
| `QUAN-20260531-1502-FR03` | Xem chi tiết môn học thành công | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Đã có môn học "MKT101 - Marketing Cơ bản" trong danh sách. | Người dùng click vào nút "Xem chi tiết" (hoặc tên môn học) của môn "Marketing Cơ bản". | Hệ thống hiển thị một trang/pop-up chứa đầy đủ các thông tin chi tiết của môn "Marketing Cơ bản" (Mã, Tên, Số tín chỉ, Mô tả). |
| `QUAN-20260531-1502-FR04` | Cập nhật môn học thành công | Người dùng là Cán bộ QLĐT, đang ở trang "Chỉnh sửa môn học" cho "MKT101 - Marketing Cơ bản".<br>Đã thay đổi "Số tín chỉ" từ "3" thành "4" và "Mô tả" thành "Giới thiệu Marketing tổng quát". | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Cập nhật môn học thành công".<br>Thông tin môn học "MKT101" trong danh sách và chi tiết được cập nhật đúng với dữ liệu mới. |
| `QUAN-20260531-1502-FR05` | Xóa môn học thành công | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Đã có môn học "ENG201 - Tiếng Anh 2" trong danh sách và không có liên kết dữ liệu nào. | Người dùng click vào nút "Xóa" của môn "Tiếng Anh 2" và xác nhận hành động xóa. | Hệ thống hiển thị thông báo "Xóa môn học thành công".<br>Môn học "Tiếng Anh 2" không còn xuất hiện trong danh sách môn học. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1502-FR01` | Tạo mới môn học với Mã môn học đã tồn tại | Người dùng là Cán bộ QLĐT, đang ở trang "Thêm mới môn học".<br>Môn học "MKT101" đã tồn tại trong hệ thống.<br>Người dùng nhập: <br>- Mã môn học: "MKT101" <br>- Tên môn học: "Marketing Nâng cao" <br>- Số tín chỉ: "3" | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Mã môn học 'MKT101' đã tồn tại." ở trường Mã môn học. |
| `QUAN-20260531-1502-FR01` | Tạo mới môn học bỏ trống Tên môn học | Người dùng là Cán bộ QLĐT, đang ở trang "Thêm mới môn học".<br>Đã nhập: <br>- Mã môn học: "ENG101" <br>- Tên môn học: (Để trống) <br>- Số tín chỉ: "3" | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Tên môn học không được để trống." ở trường Tên môn học. |
| `QUAN-20260531-1502-FR02` | Tìm kiếm môn học không có kết quả | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Danh sách môn học không có bất kỳ môn học nào chứa từ khóa "Vật lý hạt nhân". | Người dùng nhập "Vật lý hạt nhân" vào ô tìm kiếm và nhấn nút "Tìm". | Hệ thống hiển thị thông báo "Không tìm thấy môn học nào phù hợp với tiêu chí tìm kiếm." |
| `QUAN-20260531-1502-FR03` | Xem chi tiết môn học không tồn tại | Người dùng là Cán bộ QLĐT.<br>Môn học có ID = 999 không tồn tại trong hệ thống. | Người dùng cố gắng truy cập trang chi tiết môn học với ID = 999 (ví dụ qua URL trực tiếp). | Hệ thống hiển thị thông báo lỗi "Môn học không tồn tại." hoặc chuyển hướng về trang danh sách với thông báo lỗi. |
| `QUAN-20260531-1502-FR04` | Cập nhật môn học với Số tín chỉ không hợp lệ | Người dùng là Cán bộ QLĐT, đang ở trang "Chỉnh sửa môn học" cho "MKT101 - Marketing Cơ bản".<br>Người dùng thay đổi "Số tín chỉ" từ "3" thành "0". | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi "Số tín chỉ phải là số nguyên dương." ở trường Số tín chỉ. |
| `QUAN-20260531-1502-FR05` | Xóa môn học không tồn tại | Người dùng là Cán bộ QLĐT, đang ở trang "Quản lý môn học".<br>Môn học có ID = 888 không tồn tại trong hệ thống. | Người dùng cố gắng gửi yêu cầu xóa môn học với ID = 888. | Hệ thống hiển thị thông báo lỗi "Môn học cần xóa không tồn tại." |

---

_Mã tính năng `QUAN-20260531-1502` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._