# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-1452` |
| **Tên tính năng** | Quan Ly Diem Hoc Ky |
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
Cung cấp một tính năng toàn diện cho phép người dùng có quyền quản lý điểm học kỳ của học sinh, bao gồm các thao tác thêm mới, xem, sửa, xóa, tìm kiếm, lọc và phân trang dữ liệu điểm, nhằm đảm bảo tính chính xác và hiệu quả trong công tác quản lý học vụ.

### 1.2 Mục tiêu phụ
- Đảm bảo tính nhất quán và toàn vẹn của dữ liệu điểm trong hệ thống.
- Giảm thiểu sai sót thủ công trong quá trình nhập liệu và quản lý điểm.
- Hỗ trợ công tác tra cứu và tổng hợp thông tin điểm học kỳ một cách nhanh chóng.
- Cải thiện trải nghiệm người dùng với giao diện trực quan và các chức năng tìm kiếm, phân trang tiện lợi.
- Đảm bảo an toàn thông tin bằng cách áp dụng cơ chế phân quyền chặt chẽ.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Chức năng thêm mới điểm học kỳ cho từng học sinh theo môn học và học kỳ.
- Chức năng xem danh sách điểm học kỳ với thông tin chi tiết (học sinh, môn học, lớp, giáo viên phụ trách, điểm số, học kỳ, năm học).
- Chức năng cập nhật thông tin điểm học kỳ đã có.
- Chức năng xóa bản ghi điểm học kỳ.
- Chức năng tìm kiếm điểm học kỳ theo các tiêu chí (tên học sinh, mã học sinh, tên môn học, lớp, học kỳ, năm học).
- Chức năng lọc danh sách điểm theo lớp, môn học, học kỳ, năm học.
- Chức năng phân trang và sắp xếp danh sách điểm.
- Các quy tắc kiểm tra hợp lệ dữ liệu đầu vào (validation) cho điểm số và các trường liên quan.
- Cơ chế phân quyền truy cập và thao tác đối với dữ liệu điểm học kỳ.

### 2.2 Out of Scope (Ngoài phạm vi)
- Tính năng tự động tính toán điểm trung bình môn, điểm trung bình học kỳ, hoặc điểm tổng kết.
- Tích hợp với các hệ thống quản lý học tập (LMS) hoặc hệ thống bên ngoài khác.
- Chức năng import/export dữ liệu điểm hàng loạt từ file.
- Giao diện xem điểm dành cho học sinh hoặc phụ huynh (chỉ tập trung vào giao diện quản lý).
- Chức năng xét duyệt điểm hoặc khóa sổ điểm.

### 2.3 Giả định (Assumptions)
- Dữ liệu cơ bản về Học sinh, Lớp học, Môn học, Giáo viên và Phân công giảng dạy đã tồn tại và được quản lý trong hệ thống `quanlyhocsinh`.
- Người dùng có quyền truy cập vào hệ thống đã được xác thực thông qua hệ thống quản lý tài khoản người dùng của ONENET.
- Các mã học kỳ và năm học đã được định nghĩa trong hệ thống.
- Người dùng có kết nối internet ổn định để truy cập hệ thống.

### 2.4 Phụ thuộc (Dependencies)
- Module Quản lý Học sinh: Cần dữ liệu danh sách học sinh.
- Module Quản lý Môn học: Cần dữ liệu danh sách môn học.
- Module Quản lý Lớp học: Cần dữ liệu danh sách lớp học.
- Module Quản lý Người dùng/Phân quyền: Cần thông tin về vai trò và quyền hạn của người dùng để áp dụng phân quyền truy cập.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | **Admin Hệ thống** | Quản trị viên | Toàn quyền CRUD trên tất cả dữ liệu điểm học kỳ. | Quản lý tổng thể, khắc phục sự cố, cấu hình ban đầu. |
| 2 | **Giáo viên Bộ môn** | Giáo viên giảng dạy | Thêm, sửa, xem, xóa điểm của các môn học mình được phân công giảng dạy. | Nhập và cập nhật điểm cho học sinh thuộc môn mình phụ trách. |
| 3 | **Giáo viên Chủ nhiệm** | Giáo viên phụ trách lớp | Xem, sửa điểm của học sinh thuộc lớp mình chủ nhiệm. | Theo dõi và điều chỉnh điểm của học sinh trong lớp, hỗ trợ học sinh. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết mô tả quá trình quản lý điểm học kỳ. Bao gồm luồng chính (Happy Path) và các luồng rẽ nhánh (Edge Cases) như lỗi validation.

```mermaid
graph TD
    A[Người dùng đăng nhập thành công] --> B{Quyền hạn quản lý điểm?}
    B -- Không có --> P[Hiển thị "Không có quyền truy cập"]
    B -- Có --> C[Truy cập trang "Quản lý Điểm Học Kỳ"]

    C --> D[Hiển thị Danh sách Điểm (có phân trang & sắp xếp)]
    D --> E{Thao tác mong muốn?}

    E -- Tìm kiếm/Lọc --> F[Nhập tiêu chí (Tên HS, Môn, Lớp, HK, NH)]
    F --> G[Hệ thống lọc/tìm kiếm]
    G --> D

    E -- Thêm mới --> H[Nhấn nút "Thêm mới Điểm"]
    H --> I[Hiển thị form Thêm mới Điểm]
    I --> J[Nhập thông tin Điểm (HS, Môn, Lớp, Điểm, HK, NH)]
    J --> K{Dữ liệu hợp lệ theo BRs & FRs?}
    K -- Không hợp lệ --> L[Hiển thị lỗi Validation trên form]
    L --> J
    K -- Hợp lệ --> M[Nhấn "Lưu"]
    M --> N[Lưu Điểm vào hệ thống & Ghi Audit Log]
    N --> O[Hiển thị thông báo thành công]
    O --> D

    E -- Sửa --> Q[Chọn Điểm cần sửa từ danh sách]
    Q --> R[Nhấn nút "Sửa"]
    R --> S[Hiển thị form Chỉnh sửa Điểm (pre-fill dữ liệu cũ)]
    S --> T[Cập nhật thông tin Điểm]
    T --> U{Dữ liệu hợp lệ theo BRs & FRs?}
    U -- Không hợp lệ --> V[Hiển thị lỗi Validation trên form]
    V --> T
    U -- Hợp lệ --> W[Nhấn "Cập nhật"]
    W --> X[Cập nhật Điểm vào hệ thống & Ghi Audit Log]
    X --> Y[Hiển thị thông báo thành công]
    Y --> D

    E -- Xóa --> Z[Chọn Điểm cần xóa từ danh sách]
    Z --> AA[Nhấn nút "Xóa"]
    AA --> BB[Hiển thị hộp thoại xác nhận xóa]
    BB -- Hủy bỏ --> D
    BB -- Xác nhận --> CC[Xóa Điểm khỏi hệ thống & Ghi Audit Log]
    CC --> DD[Hiển thị thông báo thành công]
    DD --> D

    N, X, CC --> End([Kết thúc thao tác])
    P --> End
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-1452-FR01` | Quản lý Danh sách Điểm | As an **Admin/Giáo viên Bộ môn/Giáo viên Chủ nhiệm**, I want to **xem danh sách điểm học kỳ** so that **tôi có cái nhìn tổng quan về tình hình học tập của học sinh**. | Must |
| `QUAN-20260531-1452-FR02` | Thêm mới Điểm Học Kỳ | As an **Admin/Giáo viên Bộ môn**, I want to **thêm mới một bản ghi điểm học kỳ** so that **tôi có thể nhập điểm cho học sinh một cách dễ dàng và nhanh chóng**. | Must |
| `QUAN-20260531-1452-FR03` | Cập nhật Điểm Học Kỳ | As an **Admin/Giáo viên Bộ môn/Giáo viên Chủ nhiệm**, I want to **cập nhật thông tin điểm học kỳ của học sinh** so that **tôi có thể chỉnh sửa các sai sót hoặc điểm đã được duyệt lại**. | Must |
| `QUAN-20260531-1452-FR04` | Xóa Điểm Học Kỳ | As an **Admin/Giáo viên Bộ môn**, I want to **xóa một bản ghi điểm học kỳ** so that **tôi có thể loại bỏ các thông tin điểm không hợp lệ hoặc nhập sai**. | Must |
| `QUAN-20260531-1452-FR05` | Tìm kiếm và Lọc Điểm | As an **Admin/Giáo viên Bộ môn/Giáo viên Chủ nhiệm**, I want to **tìm kiếm và lọc danh sách điểm** so that **tôi có thể nhanh chóng định vị các bản ghi điểm cụ thể**. | Must |
| `QUAN-20260531-1452-FR06` | Kiểm tra hợp lệ dữ liệu | As a **người dùng**, I want the system to **kiểm tra tính hợp lệ của dữ liệu nhập** so that **tôi đảm bảo điểm được lưu đúng định dạng và quy tắc**. | Must |
| `QUAN-20260531-1452-FR07` | Phân quyền truy cập | As an **Admin**, I want the system to **áp dụng các quy tắc phân quyền truy cập và thao tác** so that **chỉ những người dùng được cấp phép mới có thể quản lý điểm và dữ liệu được bảo mật**. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian phản hồi:** Thời gian tải trang hiển thị danh sách điểm (với tối đa 100 bản ghi) không quá 2 giây. Thời gian xử lý các thao tác thêm mới, cập nhật, xóa điểm không quá 1 giây.
- **Khả năng chịu tải:** Hệ thống phải hỗ trợ tối thiểu 50 người dùng đồng thời thực hiện các thao tác quản lý điểm mà không ảnh hưởng đáng kể đến hiệu suất.

### 6.2 Security
- **Xác thực và ủy quyền:** Mọi thao tác quản lý điểm đều yêu cầu người dùng đã được xác thực và có quyền hạn phù hợp (theo FR07).
- **Phân quyền:** Các quyền truy cập và thao tác trên dữ liệu điểm phải được định nghĩa rõ ràng cho từng vai trò người dùng (Admin, Giáo viên Bộ môn, Giáo viên Chủ nhiệm).
- **Bảo mật dữ liệu:** Dữ liệu điểm học kỳ phải được bảo vệ khỏi truy cập trái phép, sửa đổi hoặc xóa bỏ không mong muốn. Mọi thông tin nhạy cảm phải được mã hóa khi truyền tải.

### 6.3 Availability
- Hệ thống quản lý điểm học kỳ phải khả dụng 99.5% thời gian trong các giờ làm việc (từ 7h00 đến 17h00 các ngày trong tuần).

### 6.4 Usability
- Giao diện người dùng phải trực quan, dễ hiểu và dễ sử dụng cho các vai trò người dùng mục tiêu.
- Các thông báo lỗi và thông báo thành công phải rõ ràng, cung cấp thông tin hữu ích cho người dùng.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-1452-BR01` | Phạm vi điểm số hợp lệ | Điểm số phải là một số thực (decimal) trong khoảng từ 0.0 đến 10.0 (hoặc từ 0 đến 100 nếu áp dụng thang điểm 100). VD: Nếu nhập 10.5 hoặc -1, hệ thống phải báo lỗi. Nếu nhập 7.5, hệ thống chấp nhận. |
| `QUAN-20260531-1452-BR02` | Duy nhất bản ghi điểm | Mỗi học sinh chỉ có thể có một điểm duy nhất cho một môn học cụ thể trong một học kỳ và năm học nhất định. VD: Không thể thêm điểm môn Toán cho HS Nguyễn Văn A trong HK1-2026 nếu điểm đó đã tồn tại. |
| `QUAN-20260531-1452-BR03` | Trường bắt buộc | Các trường Học sinh, Môn học, Lớp, Điểm số, Học kỳ, Năm học là các trường bắt buộc phải có dữ liệu khi thêm mới hoặc cập nhật. VD: Nếu bỏ trống trường "Học kỳ", hệ thống phải báo lỗi. |
| `QUAN-20260531-1452-BR04` | Quyền cập nhật điểm | Giáo viên Bộ môn chỉ có thể thêm/sửa/xóa điểm của các môn học mà họ được phân công giảng dạy. Giáo viên Chủ nhiệm chỉ có thể xem và sửa điểm của học sinh thuộc lớp mình chủ nhiệm. Admin có toàn quyền. VD: GV A được phân công môn Toán lớp 10A, không thể nhập điểm môn Lý lớp 10B. |
| `QUAN-20260531-1452-BR05` | Học sinh/Môn học/Lớp tồn tại | Học sinh, Môn học, Lớp được chọn phải là các thông tin đã có trong hệ thống. VD: Nếu nhập ID học sinh không tồn tại, hệ thống phải báo lỗi. |

---

## 8. Integration Requirements

- Không có tích hợp với hệ thống bên thứ ba nào được yêu cầu trong phạm vi tính năng này.
- Hệ thống sẽ tích hợp ngầm với các module nội bộ khác như Quản lý Học sinh, Quản lý Môn học, Quản lý Lớp học và Module Quản lý Người dùng/Phân quyền để lấy dữ liệu tham chiếu và xác thực quyền hạn.

---

## 9. Audit Trail

- Hệ thống phải ghi lại lịch sử các thao tác quan trọng đối với dữ liệu điểm học kỳ, bao gồm:
    - **Thêm mới:** Ghi lại ID bản ghi, người thực hiện, thời gian, và các giá trị điểm được thêm.
    - **Cập nhật:** Ghi lại ID bản ghi, người thực hiện, thời gian, trường bị thay đổi, giá trị cũ và giá trị mới.
    - **Xóa:** Ghi lại ID bản ghi, người thực hiện, thời gian, và các giá trị điểm đã bị xóa.
- Các log này cần được lưu trữ an toàn và có thể tra cứu bởi Admin.

---

## 10. KPI & Metrics theo dõi

- **Số lượng bản ghi điểm được thêm/cập nhật thành công mỗi ngày/tuần.**
- **Tỷ lệ lỗi validation khi người dùng nhập liệu điểm.**
- **Thời gian trung bình để thực hiện một thao tác CRUD điểm.**
- **Số lượt tìm kiếm/lọc điểm thành công mỗi ngày.**
- **Số lượng người dùng hoạt động trên tính năng quản lý điểm mỗi ngày.**

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1452-FR01` | Xem danh sách điểm | Người dùng (Admin/GVBM/GVCN) đã đăng nhập và có quyền. Trang Quản lý Điểm có dữ liệu. | Truy cập trang Quản lý Điểm Học Kỳ. | Hệ thống hiển thị danh sách điểm bao gồm Học sinh, Môn, Lớp, Điểm, Học kỳ, Năm học, và các nút thao tác tương ứng với quyền. |
| `QUAN-20260531-1452-FR01` | Phân trang danh sách | Danh sách điểm có nhiều hơn số lượng bản ghi hiển thị trên 1 trang (ví dụ: >20). | Người dùng cuộn hoặc nhấp vào nút phân trang. | Hệ thống hiển thị các bản ghi tiếp theo của danh sách, số trang hiện tại được cập nhật. |
| `QUAN-20260531-1452-FR02` | Thêm mới điểm thành công | Người dùng (Admin/GVBM) đã đăng nhập, có quyền thêm điểm cho môn học/lớp tương ứng. Các trường trong form hợp lệ (HS, Môn, Lớp, Điểm: 8.5, HK, NH). | Nhấn nút "Thêm mới", điền đầy đủ thông tin hợp lệ vào form và nhấn "Lưu". | Hệ thống hiển thị thông báo "Thêm điểm thành công", bản ghi điểm mới xuất hiện trong danh sách, và một log audit được tạo. |
| `QUAN-20260531-1452-FR03` | Cập nhật điểm thành công | Người dùng (Admin/GVBM/GVCN) đã đăng nhập, có quyền sửa điểm cho môn học/lớp tương ứng. Có bản ghi điểm hiện có (ví dụ: HS A, Toán, Lớp B, Điểm: 7.0, HK1, 2026). | Chọn bản ghi điểm cần sửa, thay đổi Điểm từ 7.0 thành 8.0, và nhấn "Cập nhật". | Hệ thống hiển thị thông báo "Cập nhật điểm thành công", điểm của bản ghi đó được thay đổi thành 8.0 trong danh sách, và một log audit được tạo. |
| `QUAN-20260531-1452-FR04` | Xóa điểm thành công | Người dùng (Admin/GVBM) đã đăng nhập, có quyền xóa điểm. Có bản ghi điểm hiện có trong danh sách. | Chọn bản ghi điểm cần xóa, nhấn "Xóa", và xác nhận trong hộp thoại. | Hệ thống hiển thị thông báo "Xóa điểm thành công", bản ghi điểm đó không còn xuất hiện trong danh sách, và một log audit được tạo. |
| `QUAN-20260531-1452-FR05` | Tìm kiếm theo tên học sinh | Danh sách điểm có chứa học sinh "Nguyễn Văn A". | Nhập "Nguyễn Văn A" vào ô tìm kiếm và nhấn "Tìm kiếm". | Hệ thống hiển thị các bản ghi điểm có tên học sinh là "Nguyễn Văn A". |
| `QUAN-20260531-1452-FR05` | Lọc theo lớp và học kỳ | Danh sách điểm có nhiều bản ghi thuộc các lớp và học kỳ khác nhau. | Chọn "Lớp 10A" và "Học kỳ 1" từ các bộ lọc và nhấn "Áp dụng". | Hệ thống hiển thị chỉ các bản ghi điểm của học sinh thuộc Lớp 10A trong Học kỳ 1. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1452-FR02` | Bỏ trống trường bắt buộc | Người dùng (Admin/GVBM) đã đăng nhập, có quyền. Form thêm mới đang mở. | Bỏ trống trường "Học kỳ" và nhấn "Lưu". | Hệ thống hiển thị thông báo lỗi "Học kỳ không được bỏ trống" (hoặc tương tự) màu đỏ dưới trường bị thiếu. |
| `QUAN-20260531-1452-FR02` | Điểm ngoài phạm vi | Người dùng (Admin/GVBM) đã đăng nhập, có quyền. Form thêm mới đang mở. | Nhập điểm là "11.0" hoặc "-0.5" vào trường "Điểm số" và nhấn "Lưu". | Hệ thống hiển thị thông báo lỗi "Điểm số phải nằm trong khoảng từ 0.0 đến 10.0" (hoặc tương tự). |
| `QUAN-20260531-1452-FR02` | Thêm điểm trùng lặp | Người dùng (Admin/GVBM) đã đăng nhập, có quyền. Đã có điểm cho HS A, Môn Toán, HK1, 2026. | Cố gắng thêm điểm mới cho cùng HS A, Môn Toán, HK1, 2026. | Hệ thống hiển thị thông báo lỗi "Điểm cho học sinh này, môn học này trong học kỳ này đã tồn tại". |
| `QUAN-20260531-1452-FR03` | Cập nhật điểm ngoài phạm vi | Người dùng (Admin/GVBM/GVCN) đã đăng nhập, có quyền. Form sửa điểm đang mở. | Thay đổi điểm từ 7.0 thành "101" hoặc "-5" và nhấn "Cập nhật". | Hệ thống hiển thị thông báo lỗi "Điểm số phải nằm trong khoảng từ 0.0 đến 10.0" (hoặc tương tự). |
| `QUAN-20260531-1452-FR04` | Hủy thao tác xóa | Người dùng (Admin/GVBM) đã đăng nhập, có quyền. Chọn một bản ghi để xóa. | Nhấn "Xóa", sau đó trong hộp thoại xác nhận, chọn "Hủy bỏ". | Hộp thoại xác nhận đóng lại, bản ghi điểm vẫn tồn tại trong danh sách, không có thay đổi nào được thực hiện. |
| `QUAN-20260531-1452-FR05` | Tìm kiếm không có kết quả | Danh sách điểm không chứa học sinh có tên "Nguyễn Văn X". | Nhập "Nguyễn Văn X" vào ô tìm kiếm và nhấn "Tìm kiếm". | Hệ thống hiển thị thông báo "Không tìm thấy kết quả phù hợp" và danh sách rỗng. |
| `QUAN-20260531-1452-FR07` | Truy cập trái phép | Người dùng không có quyền (ví dụ: một học sinh) hoặc GVBM cố gắng truy cập điểm của môn không được phân công. | Cố gắng truy cập trang Quản lý Điểm hoặc thực hiện thao tác Thêm/Sửa/Xóa. | Hệ thống hiển thị thông báo "Bạn không có quyền truy cập chức năng này" hoặc "Bạn không có quyền thực hiện thao tác này" và chặn hành động. |

---

_Mã tính năng `QUAN-20260531-1452` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._