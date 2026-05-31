# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-154643` |
| **Tên tính năng** | Quan Ly Diem Hoc Ky |
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
Cung cấp một hệ thống cho phép giáo viên và quản trị viên nhà trường quản lý (tạo, xem, cập nhật, xóa) điểm học kỳ của học sinh một cách chính xác, hiệu quả và có tổ chức.

### 1.2 Mục tiêu phụ
- Đảm bảo tính chính xác và toàn vẹn của dữ liệu điểm học kỳ.
- Nâng cao hiệu suất và giảm thiểu sai sót trong quy trình nhập và quản lý điểm.
- Hỗ trợ việc truy xuất thông tin điểm số nhanh chóng và linh hoạt.
- Cung cấp nền tảng cho việc phát triển các chức năng liên quan đến báo cáo và phân tích kết quả học tập trong tương lai.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới bản ghi điểm học kỳ cho từng học sinh theo môn học và học kỳ.
- Xem danh sách điểm học kỳ đã nhập, bao gồm thông tin học sinh, môn học, học kỳ và điểm số.
- Xem chi tiết thông tin của một bản ghi điểm học kỳ cụ thể.
- Chỉnh sửa thông tin điểm học kỳ hiện có.
- Xóa bản ghi điểm học kỳ.
- Xác thực dữ liệu đầu vào cho các trường thông tin điểm (ví dụ: định dạng, giá trị).
- Tìm kiếm điểm học kỳ theo các tiêu chí như tên học sinh, mã học sinh, tên môn học, hoặc học kỳ.
- Phân trang danh sách điểm học kỳ để dễ dàng quản lý dữ liệu lớn.

### 2.2 Out of Scope (Ngoài phạm vi)
- Tự động tính toán điểm trung bình môn, điểm trung bình học kỳ, hoặc điểm tổng kết năm học.
- Chức năng in hoặc xuất báo cáo bảng điểm, học bạ.
- Tích hợp với các hệ thống bên ngoài (ví dụ: cổng thông tin phụ huynh, hệ thống gửi thông báo).
- Quản lý các thành phần điểm chi tiết (điểm miệng, điểm 15 phút, điểm giữa kỳ, ...), chỉ tập trung vào điểm học kỳ cuối cùng.
- Chức năng nhập/xuất dữ liệu điểm hàng loạt từ/ra file (Excel, CSV).

### 2.3 Giả định (Assumptions)
- Dữ liệu về học sinh, môn học, lớp học và học kỳ đã tồn tại và được quản lý trong hệ thống ONENET.
- Người dùng có quyền truy cập tính năng Quản lý Điểm Học Kỳ đã được xác thực và phân quyền phù hợp.
- Điểm học kỳ được nhập là điểm cuối cùng của môn học trong học kỳ, không phải điểm thành phần.

### 2.4 Phụ thuộc (Dependencies)
- **Module Quản lý Học sinh:** Cung cấp thông tin chi tiết về học sinh.
- **Module Quản lý Môn học:** Cung cấp danh sách và thông tin về các môn học.
- **Module Quản lý Học kỳ:** Cung cấp danh sách và thông tin về các học kỳ.
- **Hệ thống Xác thực và Phân quyền:** Đảm bảo người dùng có quyền hợp lệ để thực hiện các thao tác quản lý điểm.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | **Giáo viên** | Giáo viên Bộ môn/Chủ nhiệm | Tạo, xem, cập nhật, xóa điểm của học sinh trong các lớp và môn học được phân công. | Nhập điểm cho học sinh một cách nhanh chóng, chính xác; dễ dàng theo dõi và điều chỉnh điểm khi cần. |
| 2 | **Quản trị viên Nhà trường** | Quản lý hệ thống/Trợ lý giáo vụ | Toàn quyền tạo, xem, cập nhật, xóa điểm của tất cả học sinh trong trường. | Đảm bảo tính toàn vẹn và chính xác của dữ liệu điểm trên toàn hệ thống; hỗ trợ giáo viên khi có vấn đề. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết. Mọi luồng chính (Happy Path) và luồng rẽ nhánh (Edge Cases) cần được thể hiện.

```mermaid
flowchart TD
    A[Người dùng đăng nhập] --> B{Có quyền quản lý điểm?}
    B -- Có --> C[Truy cập trang Quản lý Điểm Học Kỳ]
    B -- Không --> D[Hiển thị thông báo lỗi/Chuyển hướng về trang chủ]
    
    C --> E[Hiển thị danh sách điểm học kỳ (có phân trang)]
    
    E -- Click "Tạo mới" --> F{Form tạo mới điểm}
    F --> G[Điền thông tin: Học sinh, Môn học, Học kỳ, Điểm số]
    G -- Nhấn "Lưu" --> H{Dữ liệu hợp lệ?}
    H -- Có --> I[Lưu thành công, hiển thị thông báo, quay về danh sách]
    H -- Không --> J[Hiển thị lỗi validation trên form]
    J --> G

    E -- Click "Xem chi tiết" --> K[Hiển thị thông tin chi tiết của điểm]
    K --> E

    E -- Click "Chỉnh sửa" --> L{Form chỉnh sửa điểm (tải dữ liệu hiện có)}
    L --> M[Thay đổi thông tin: Điểm số]
    M -- Nhấn "Lưu" --> N{Dữ liệu hợp lệ?}
    N -- Có --> O[Cập nhật thành công, hiển thị thông báo, quay về danh sách]
    N -- Không --> J
    J --> M

    E -- Click "Xóa" --> P{Xác nhận xóa bản ghi?}
    P -- Đồng ý --> Q[Xóa thành công, hiển thị thông báo, cập nhật danh sách]
    P -- Hủy --> E

    E -- Nhập từ khóa tìm kiếm --> R[Kết quả tìm kiếm phù hợp]
    E -- Thay đổi trang/Số bản ghi --> E
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|-----------|
| `QUAN-20260531-154643-FR01` | Tạo mới Điểm Học Kỳ | As a Giáo viên, I want to tạo mới bản ghi điểm học kỳ cho học sinh so that điểm được ghi nhận vào hệ thống. | Must |
| `QUAN-20260531-154643-FR02` | Xem danh sách Điểm Học Kỳ | As a Giáo viên, I want to xem danh sách các điểm học kỳ đã được nhập so that tôi có cái nhìn tổng quan về kết quả học tập. | Must |
| `QUAN-20260531-154643-FR03` | Xem chi tiết Điểm Học Kỳ | As a Giáo viên, I want to xem chi tiết một bản ghi điểm học kỳ so that tôi có thể kiểm tra lại thông tin cụ thể. | Must |
| `QUAN-20260531-154643-FR04` | Cập nhật Điểm Học Kỳ | As a Giáo viên, I want to chỉnh sửa thông tin điểm học kỳ của học sinh so that tôi có thể điều chỉnh khi có sai sót. | Must |
| `QUAN-20260531-154643-FR05` | Xóa Điểm Học Kỳ | As a Giáo viên, I want to xóa một bản ghi điểm học kỳ so that tôi có thể loại bỏ dữ liệu sai hoặc không cần thiết. | Must |
| `QUAN-20260531-154643-FR06` | Tìm kiếm Điểm Học Kỳ | As a Giáo viên, I want to tìm kiếm điểm học kỳ theo nhiều tiêu chí (tên học sinh, môn học, học kỳ) so that tôi có thể nhanh chóng tìm thấy thông tin cần thiết. | Must |
| `QUAN-20260531-154643-FR07` | Phân trang danh sách Điểm | As a Giáo viên, I want to xem danh sách điểm được phân trang và điều chỉnh số lượng bản ghi trên mỗi trang so that tôi có thể dễ dàng duyệt qua lượng dữ liệu lớn. | Must |
| `QUAN-20260531-154643-FR08` | Xác thực dữ liệu đầu vào Điểm | As a Giáo viên, I want to được hệ thống xác thực dữ liệu khi nhập/cập nhật điểm so that tôi đảm bảo điểm được ghi nhận là hợp lệ. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian tải trang:** Trang danh sách điểm học kỳ (với tối đa 500 bản ghi) phải được tải và hiển thị hoàn chỉnh trong vòng 3 giây.
- **Thời gian phản hồi CRUD:** Các thao tác tạo mới, cập nhật, xóa điểm phải hoàn thành và hiển thị kết quả trong vòng 2 giây.
- **Thời gian tìm kiếm:** Chức năng tìm kiếm phải trả về kết quả trong vòng 3 giây, ngay cả khi dữ liệu có đến 10.000 bản ghi điểm.

### 6.2 Security
- **Xác thực và Phân quyền:** Chỉ người dùng đã đăng nhập và có quyền (Giáo viên, Quản trị viên Nhà trường) mới được phép truy cập và thực hiện các thao tác quản lý điểm. Giáo viên chỉ được thao tác với điểm của học sinh và môn học mà mình phụ trách.
- **Bảo vệ dữ liệu:** Dữ liệu điểm số phải được bảo vệ khỏi các lỗ hổng bảo mật phổ biến như SQL Injection, Cross-Site Scripting (XSS).
- **Mã hóa truyền tải:** Tất cả dữ liệu truyền giữa client và server phải được mã hóa sử dụng HTTPS.

### 6.3 Availability
- Hệ thống phải có khả năng hoạt động liên tục 24/7 với thời gian hoạt động (uptime) không dưới 99.5% mỗi tháng.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-154643-BR01` | **Giá trị điểm hợp lệ** | Điểm học kỳ phải là một số thực nằm trong khoảng từ 0.0 đến 10.0 (bao gồm cả 0.0 và 10.0) và có thể có tối đa 2 chữ số thập phân. <br> **VD hợp lệ:** 7.5, 8.00, 9, 10.0, 0. <br> **VD không hợp lệ:** -1, 10.1, 8.123, "Điểm giỏi". |
| `QUAN-20260531-154643-BR02` | **Tính duy nhất của bản ghi điểm** | Mỗi học sinh chỉ có thể có một bản ghi điểm duy nhất cho một môn học trong một học kỳ cụ thể. Hệ thống không cho phép tạo hai bản ghi điểm trùng lặp. <br> **VD:** Học sinh Nguyễn Văn A, Môn Toán, Học kỳ 1, Năm học 2026-2027 => chỉ được có 1 bản ghi điểm. Nếu cố gắng tạo thêm, hệ thống phải báo lỗi. |
| `QUAN-20260531-154643-BR03` | **Các trường bắt buộc** | Các trường "Học sinh", "Môn học", "Học kỳ" và "Điểm số" là các trường bắt buộc phải có giá trị khi tạo mới hoặc cập nhật điểm học kỳ. <br> **VD:** Khi tạo mới, nếu không chọn Học sinh, hệ thống sẽ báo lỗi "Học sinh không được để trống". |
| `QUAN-20260531-154643-BR04` | **Quyền hạn quản lý điểm** | Giáo viên chỉ có quyền tạo, xem, sửa, xóa điểm của các học sinh thuộc lớp mình chủ nhiệm hoặc các môn học mình giảng dạy. Quản trị viên nhà trường có toàn quyền trên tất cả các bản ghi điểm. <br> **VD:** Giáo viên A chỉ dạy Toán lớp 10A1. Giáo viên A có thể sửa điểm Toán của học sinh lớp 10A1, nhưng không thể sửa điểm Lý của học sinh lớp 10A1 hoặc điểm Toán của học sinh lớp 10A2. |

---

## 8. Integration Requirements

- Không có yêu cầu tích hợp với hệ thống bên thứ 3 trong giai đoạn hiện tại.
- Tính năng này sẽ sử dụng dữ liệu từ các module nội bộ của hệ thống ONENET như Quản lý Học sinh, Quản lý Môn học, Quản lý Lớp học, và Quản lý Học kỳ.

---

## 9. Audit Trail

- Hệ thống phải ghi lại nhật ký cho tất cả các thao tác quan trọng liên quan đến quản lý điểm học kỳ.
- Nhật ký cần bao gồm:
    - **Người thực hiện:** ID người dùng và tên người dùng.
    - **Thời gian thực hiện:** Thời điểm thao tác diễn ra.
    - **Loại thao tác:** Tạo mới, chỉnh sửa, xóa.
    - **Thông tin thay đổi:** Đối với thao tác chỉnh sửa, cần ghi lại giá trị trước và sau khi thay đổi của các trường bị ảnh hưởng. Đối với xóa, ghi lại dữ liệu bản ghi đã bị xóa.
    - **Mã định danh bản ghi:** ID của bản ghi điểm học kỳ bị tác động.

---

## 10. KPI & Metrics theo dõi

- **Tỷ lệ hoàn thành nhập điểm đúng hạn:** Phần trăm các lớp/môn học có điểm được nhập đầy đủ trước thời hạn quy định (>95%).
- **Chất lượng dữ liệu điểm:** Số lượng lỗi hoặc yêu cầu chỉnh sửa điểm sau khi nhập/công bố (<1% tổng số bản ghi).
- **Hiệu suất người dùng:** Thời gian trung bình để hoàn thành việc nhập điểm cho một lớp học (tối ưu hóa để giảm thời gian).
- **Tỷ lệ sử dụng chức năng:** Số lượt truy cập và tương tác với các chức năng tạo/sửa/xóa/tìm kiếm điểm hàng ngày/tuần.
- **Thời gian phản hồi trung bình:** Thời gian phản hồi trung bình cho các thao tác CRUD và tìm kiếm (<3 giây).

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-154643-FR01` | **Tạo mới điểm học kỳ thành công** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Form tạo mới điểm được điền đầy đủ các trường bắt buộc (Học sinh, Môn học, Học kỳ, Điểm số) với dữ liệu hợp lệ (ví dụ: Học sinh: Nguyễn Văn A, Môn: Toán, Học kỳ: HK1-2026, Điểm: 8.5). | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Tạo điểm thành công". <br> Bản ghi điểm mới xuất hiện trong danh sách điểm. <br> Dữ liệu điểm được lưu trữ chính xác trong cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR02` | **Xem danh sách điểm học kỳ** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Có ít nhất 5 bản ghi điểm học kỳ đã tồn tại trong hệ thống. | Người dùng truy cập trang "Quản lý Điểm Học Kỳ". | Hệ thống hiển thị danh sách tất cả các bản ghi điểm (hoặc các bản ghi mà người dùng có quyền xem). <br> Mỗi bản ghi hiển thị đầy đủ các thông tin chính (Học sinh, Môn học, Học kỳ, Điểm số). <br> Danh sách được hiển thị trong một bảng với các cột rõ ràng. |
| `QUAN-20260531-154643-FR03` | **Xem chi tiết điểm học kỳ** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Có một bản ghi điểm học kỳ hiện có trong danh sách. | Người dùng nhấn vào nút "Xem chi tiết" hoặc tương đương của một bản ghi điểm cụ thể. | Hệ thống hiển thị một trang hoặc cửa sổ popup chứa tất cả các thông tin chi tiết của bản ghi điểm đó. |
| `QUAN-20260531-154643-FR04` | **Cập nhật điểm học kỳ thành công** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Có một bản ghi điểm học kỳ hiện có (ví dụ: Học sinh: Nguyễn Văn A, Môn: Toán, Học kỳ: HK1-2026, Điểm: 8.5). <br> Người dùng truy cập form chỉnh sửa và thay đổi Điểm số thành một giá trị hợp lệ khác (ví dụ: 9.0). | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Cập nhật điểm thành công". <br> Bản ghi điểm trong danh sách và cơ sở dữ liệu được cập nhật với Điểm số mới. |
| `QUAN-20260531-154643-FR05` | **Xóa điểm học kỳ thành công** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Có một bản ghi điểm học kỳ hiện có trong danh sách. | Người dùng nhấn nút "Xóa" của một bản ghi điểm và xác nhận xóa trong hộp thoại. | Hệ thống hiển thị thông báo "Xóa điểm thành công". <br> Bản ghi điểm đó không còn xuất hiện trong danh sách và đã bị xóa khỏi cơ sở dữ liệu. |
| `QUAN-20260531-154643-FR06` | **Tìm kiếm điểm học kỳ theo tên học sinh** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Có các bản ghi điểm, trong đó có học sinh "Nguyễn Văn B". | Người dùng nhập "Nguyễn Văn B" vào ô tìm kiếm và nhấn nút "Tìm kiếm". | Hệ thống hiển thị danh sách các bản ghi điểm chỉ của học sinh "Nguyễn Văn B". |
| `QUAN-20260531-154643-FR07` | **Chuyển trang danh sách điểm** | Một Giáo viên/Quản trị viên đã đăng nhập và có quyền. <br> Danh sách điểm có tổng cộng 30 bản ghi và cấu hình hiển thị 10 bản ghi/trang (đang ở trang 1). | Người dùng nhấn vào nút "Trang 2" trên thanh phân trang. | Hệ thống tải và hiển thị 10 bản ghi điểm tiếp theo (từ bản ghi thứ 11 đến 20). |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-154643-FR01` | **Bỏ trống trường bắt buộc khi tạo mới** | Một Giáo viên đã đăng nhập. <br> Form tạo mới điểm thiếu thông tin "Học sinh" (hoặc Môn học/Học kỳ/Điểm số). | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi validation tại trường bị bỏ trống (ví dụ: "Học sinh không được để trống"). <br> Dữ liệu không được lưu vào hệ thống. |
| `QUAN-20260531-154643-FR01` | **Nhập giá trị điểm không hợp lệ** | Một Giáo viên đã đăng nhập. <br> Form tạo mới điểm được điền đầy đủ, nhưng trường "Điểm số" có giá trị ngoài phạm vi [0.0, 10.0] (ví dụ: 10.5 hoặc -1). | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi validation tại trường "Điểm số" (ví dụ: "Điểm số phải từ 0.0 đến 10.0"). <br> Dữ liệu không được lưu vào hệ thống. |
| `QUAN-20260531-154643-FR01` | **Tạo mới điểm trùng lặp** | Một Giáo viên đã đăng nhập. <br> Đã tồn tại bản ghi điểm cho Học sinh: Nguyễn Văn B, Môn: Lý, Học kỳ: HK2-2026. <br> Người dùng cố gắng tạo một bản ghi điểm mới với thông tin Học sinh, Môn học, Học kỳ giống hệt. | Người dùng nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi (ví dụ: "Điểm cho học sinh này, môn học này và học kỳ này đã tồn tại"). <br> Dữ liệu không được lưu vào hệ thống. |
| `QUAN-20260531-154643-FR04` | **Giáo viên sửa điểm ngoài quyền hạn** | Một Giáo viên A đã đăng nhập và chỉ được phân quyền quản lý điểm môn Toán lớp 10A1. <br> Có bản ghi điểm môn Lý lớp 10A1. | Giáo viên A cố gắng truy cập và chỉnh sửa điểm môn Lý lớp 10A1. | Hệ thống hiển thị thông báo lỗi "Bạn không có quyền thực hiện thao tác này" hoặc chuyển hướng về trang danh sách. |
| `QUAN-20260531-154643-FR06` | **Không tìm thấy kết quả tìm kiếm** | Một Giáo viên đã đăng nhập. <br> Không có bản ghi điểm nào phù hợp với từ khóa tìm kiếm. | Người dùng nhập "Học sinh không tồn tại" vào ô tìm kiếm và nhấn nút "Tìm kiếm". | Hệ thống hiển thị thông báo "Không tìm thấy kết quả nào" hoặc một danh sách trống. |

---

_Mã tính năng `QUAN-20260531-154643` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._