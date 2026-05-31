# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-1502` |
| **Tên tính năng** | Quan Ly Diem Hoc Ky |
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
Cung cấp một tính năng cho phép quản lý điểm học kỳ của học sinh một cách hiệu quả và chính xác, bao gồm các nghiệp vụ tạo mới, xem, cập nhật, xóa, tìm kiếm và phân trang dữ liệu điểm.

### 1.2 Mục tiêu phụ
- Nâng cao tính chính xác và đồng bộ của dữ liệu điểm học kỳ trong hệ thống.
- Giảm thiểu thời gian và công sức thủ công trong việc nhập liệu và quản lý điểm.
- Hỗ trợ giáo viên và cán bộ quản lý dễ dàng truy xuất thông tin điểm của học sinh.
- Cải thiện trải nghiệm người dùng thông qua giao diện trực quan và các chức năng tìm kiếm, phân trang tiện lợi.
- Đảm bảo tuân thủ các quy định về quản lý điểm của nhà trường.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới điểm học kỳ cho từng học sinh theo môn học và học kỳ.
- Xem danh sách điểm học kỳ theo các tiêu chí (lớp, môn học, học kỳ).
- Cập nhật thông tin điểm học kỳ hiện có.
- Xóa điểm học kỳ đã nhập (chỉ trong trường hợp được phép).
- Tìm kiếm điểm học kỳ theo tên học sinh, mã học sinh, tên môn học, lớp học hoặc học kỳ.
- Phân trang danh sách điểm học kỳ để quản lý dữ liệu lớn.
- Kiểm tra tính hợp lệ của dữ liệu đầu vào (validation).
- Phân quyền truy cập và thao tác trên dữ liệu điểm.

### 2.2 Out of Scope (Ngoài phạm vi)
- Tính toán điểm trung bình môn, điểm trung bình học kỳ, xếp loại học lực.
- Tính năng phê duyệt/xác nhận điểm từ nhiều cấp độ.
- Tích hợp với các hệ thống chấm điểm tự động bên ngoài.
- Xuất/nhập khẩu dữ liệu điểm từ các định dạng file khác (Excel, CSV) trong giai đoạn hiện tại.
- Lịch sử thay đổi điểm (audit trail chi tiết đến từng trường dữ liệu).

### 2.3 Giả định (Assumptions)
- Hệ thống quản lý học sinh (quanlyhocsinh) đã có sẵn các dữ liệu cơ bản về Học sinh, Lớp học, Môn học, và Giáo viên.
- Thông tin về học kỳ hiện tại và các học kỳ đã qua đã được định nghĩa trong hệ thống.
- Người dùng có quyền truy cập hệ thống đã được xác thực và phân quyền phù hợp.
- Định dạng và thang điểm (ví dụ: từ 0 đến 10, hoặc A, B, C) đã được thống nhất và cấu hình trong hệ thống hoặc được hiểu ngầm.
- Các quy tắc nghiệp vụ về điểm (ví dụ: chỉ số nguyên, số thập phân) sẽ được xác định rõ.

### 2.4 Phụ thuộc (Dependencies)
- **Module Quản lý Học sinh:** Cần thông tin về danh sách học sinh (Mã học sinh, Tên học sinh, Lớp học) để liên kết điểm.
- **Module Quản lý Môn học:** Cần thông tin về danh sách môn học (Mã môn học, Tên môn học) để nhập điểm cho từng môn.
- **Module Quản lý Lớp học:** Cần thông tin về lớp học để lọc và phân loại điểm.
- **Module Quản lý Giáo viên:** Cần thông tin về giáo viên để phân quyền nhập điểm theo môn học hoặc lớp.
- **Hệ thống xác thực và phân quyền người dùng:** Đảm bảo chỉ người dùng có thẩm quyền mới có thể truy cập và thao tác với dữ liệu điểm.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | **Giáo viên** | Giáo viên Bộ môn | Xem, Tạo, Cập nhật điểm cho môn học mình phụ trách. | Nhập và chỉnh sửa điểm học kỳ cho học sinh của các lớp mình giảng dạy, đảm bảo tính chính xác và kịp thời. |
| 2 | **Cán bộ quản lý** | Quản trị viên/Giáo vụ | Xem, Tạo, Cập nhật, Xóa điểm cho tất cả học sinh, tất cả môn học. | Giám sát, kiểm tra, điều chỉnh điểm học kỳ của toàn bộ học sinh khi cần thiết, hỗ trợ quản lý dữ liệu điểm tổng thể. |
| 3 | **Học sinh (đọc)** | Học sinh | Chỉ xem điểm của bản thân. | Xem điểm học kỳ của các môn học mình đã tham gia để theo dõi kết quả học tập. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết. Mọi luồng chính (Happy Path) và luồng rẽ nhánh (Edge Cases) cần được thể hiện.

```mermaid
flowchart TD
    A[Start: Đăng nhập vào hệ thống] --> B{Người dùng có quyền quản lý điểm?}
    B -- Có --> C[Truy cập tính năng Quản lý Điểm Học Kỳ]
    B -- Không --> A_Err[Hiển thị thông báo: Không có quyền]
    
    C --> D{Người dùng muốn làm gì?}
    D -- Xem danh sách --> E[Hiển thị danh sách điểm (có phân trang, tìm kiếm)]
    D -- Tạo mới điểm --> F[Hiển thị form nhập điểm mới]
    D -- Cập nhật điểm --> G[Chọn 1 mục điểm từ danh sách]
    D -- Xóa điểm --> H[Chọn 1 mục điểm từ danh sách]

    E --> K{Thao tác trên danh sách?}
    K -- Tìm kiếm/Lọc --> E
    K -- Chọn sửa --> G
    K -- Chọn xóa --> H
    K -- Quay lại --> C
    
    F --> F1[Nhập thông tin điểm (Mã HS, Môn học, Học kỳ, Điểm)]
    F1 --> F2{Thông tin hợp lệ?}
    F2 -- Có --> F3[Lưu điểm vào hệ thống]
    F2 -- Không --> F_Err[Hiển thị lỗi validation trên form]
    F3 --> F_Success[Hiển thị thông báo "Tạo mới thành công"]
    F_Success --> E
    F_Err --> F1

    G --> G1[Hiển thị form cập nhật điểm (với dữ liệu hiện có)]
    G1 --> G2[Chỉnh sửa thông tin điểm]
    G2 --> G3{Thông tin hợp lệ?}
    G3 -- Có --> G4[Cập nhật điểm vào hệ thống]
    G3 -- Không --> G_Err[Hiển thị lỗi validation trên form]
    G4 --> G_Success[Hiển thị thông báo "Cập nhật thành công"]
    G_Success --> E
    G_Err --> G2

    H --> H1[Hiển thị hộp thoại xác nhận xóa]
    H1 -- Xác nhận xóa --> H2[Xóa điểm khỏi hệ thống]
    H1 -- Hủy --> E
    H2 --> H_Success[Hiển thị thông báo "Xóa thành công"]
    H2 --> H_Err[Hiển thị thông báo "Không thể xóa" (ví dụ: đã khóa điểm)]
    H_Success --> E
    H_Err --> E
    
    A_Err --> End([End])
    E --> End_Op[End]
    F_Success --> End_Op
    G_Success --> End_Op
    H_Success --> End_Op
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-1502-FR01` | Quản lý thông tin điểm học kỳ | As a Giáo viên/Cán bộ quản lý, I want to thêm mới, xem, cập nhật và xóa thông tin điểm học kỳ của học sinh so that tôi có thể quản lý dữ liệu điểm một cách đầy đủ và chính xác. | Must |
| `QUAN-20260531-1502-FR02` | Tìm kiếm và lọc điểm học kỳ | As a Giáo viên/Cán bộ quản lý, I want to tìm kiếm và lọc danh sách điểm học kỳ theo các tiêu chí như học sinh, môn học, lớp học và học kỳ so that tôi có thể dễ dàng tìm thấy thông tin cần thiết. | Must |
| `QUAN-20260531-1502-FR03` | Phân trang danh sách điểm | As a Giáo viên/Cán bộ quản lý, I want to xem danh sách điểm được phân trang so that tôi có thể duyệt qua lượng lớn dữ liệu một cách hiệu quả mà không bị quá tải. | Must |
| `QUAN-20260531-1502-FR04` | Kiểm tra tính hợp lệ của điểm | As a Giáo viên/Cán bộ quản lý, I want the system to kiểm tra tính hợp lệ của dữ liệu điểm khi nhập hoặc cập nhật so that điểm được lưu trữ luôn đúng định dạng và trong phạm vi cho phép. | Must |
| `QUAN-20260531-1502-FR05` | Phân quyền thao tác điểm | As a Cán bộ quản lý, I want the system to phân quyền cho phép Giáo viên chỉ thao tác trên điểm của môn học mình phụ trách và Cán bộ quản lý có toàn quyền so that dữ liệu điểm được bảo mật và chỉ những người có thẩm quyền mới có thể chỉnh sửa. | Must |
| `QUAN-20260531-1502-FR06` | Xem điểm học kỳ của bản thân | As a Học sinh, I want to xem điểm học kỳ của các môn học tôi đã tham gia so that tôi có thể theo dõi kết quả học tập của mình. | Should |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian phản hồi:** Hệ thống phải phản hồi các thao tác CRUD (tạo, cập nhật, xóa) trong vòng 3 giây. Các thao tác tìm kiếm và hiển thị danh sách (có phân trang) phải trong vòng 5 giây, ngay cả với danh sách hơn 10,000 bản ghi điểm.
- **Số lượng người dùng đồng thời:** Hệ thống phải hỗ trợ ít nhất 100 người dùng đồng thời truy cập và thực hiện các thao tác quản lý điểm mà không làm giảm hiệu suất đáng kể.

### 6.2 Security
- **Xác thực:** Mọi người dùng truy cập tính năng phải được xác thực qua hệ thống đăng nhập chung của ONENET.
- **Phân quyền:** Chỉ người dùng có vai trò "Giáo viên" hoặc "Cán bộ quản lý" mới có quyền truy cập tính năng. "Giáo viên" chỉ được thao tác trên điểm của các môn học mình được phân công. "Cán bộ quản lý" có toàn quyền thao tác trên tất cả dữ liệu điểm. "Học sinh" chỉ có quyền xem điểm của bản thân.
- **Bảo mật dữ liệu:** Dữ liệu điểm học kỳ phải được bảo vệ khỏi truy cập trái phép, sửa đổi không mong muốn hoặc tiết lộ thông tin. Tất cả các giao tiếp giữa client và server phải sử dụng HTTPS.
- **Kiểm tra đầu vào:** Hệ thống phải thực hiện kiểm tra đầu vào nghiêm ngặt để ngăn chặn các cuộc tấn công phổ biến như SQL Injection, Cross-Site Scripting (XSS).

### 6.3 Availability
- **Thời gian uptime:** Hệ thống phải có thời gian uptime (khả dụng) tối thiểu 99.5% trong giờ hành chính.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-1502-BR01` | Định dạng và khoảng giá trị điểm | Điểm phải là một số thực không âm, nằm trong khoảng từ 0 đến 10. Điểm có thể có tối đa 2 chữ số thập phân. <br> VD: **Đầu vào hợp lệ:** 7.5, 9.0, 10, 0.5. **Đầu vào không hợp lệ:** -1, 10.5, "A+", 7.555. |
| `QUAN-20260531-1502-BR02` | Cấm nhập điểm trùng lặp | Một học sinh không thể có nhiều hơn một điểm cho cùng một môn học trong cùng một học kỳ. <br> VD: **Đầu vào không hợp lệ:** Học sinh Nguyễn Văn A, Môn Toán, Học kỳ I, Điểm 7.0; Học sinh Nguyễn Văn A, Môn Toán, Học kỳ I, Điểm 8.0 (không cho phép tạo mục điểm thứ hai). |
| `QUAN-20260531-1502-BR03` | Quyền tạo/cập nhật điểm của Giáo viên | Một giáo viên chỉ được tạo hoặc cập nhật điểm cho các môn học mà họ được phân công giảng dạy trong học kỳ đó và cho các học sinh thuộc lớp học do họ phụ trách. <br> VD: **Cho phép:** Giáo viên A được phân công môn Toán lớp 10A, có thể nhập điểm môn Toán cho học sinh lớp 10A. **Không cho phép:** Giáo viên A không thể nhập điểm môn Lý cho học sinh lớp 10B. |
| `QUAN-20260531-1502-BR04` | Cấm xóa điểm sau khi đã khóa sổ | Điểm học kỳ không thể bị xóa nếu học kỳ đó đã được "khóa sổ" bởi Cán bộ quản lý (tức là đã kết thúc học kỳ và dữ liệu điểm đã được chốt). <br> VD: **Cho phép:** Xóa điểm môn Văn cho HS Nguyễn Thị B ở Học kỳ II năm học 2025-2026 khi học kỳ này chưa khóa sổ. **Không cho phép:** Xóa điểm môn Anh cho HS Trần Văn C ở Học kỳ I năm học 2025-2026 khi học kỳ này đã khóa sổ. |
| `QUAN-20260531-1502-BR05` | Học sinh phải tồn tại | Khi nhập điểm, học sinh được chọn phải tồn tại trong hệ thống quản lý học sinh. <br> VD: **Đầu vào hợp lệ:** Chọn học sinh có mã "HS001". **Đầu vào không hợp lệ:** Nhập thủ công mã học sinh "HS999" không tồn tại, hệ thống phải báo lỗi. |
| `QUAN-20260531-1502-BR06` | Môn học phải tồn tại | Khi nhập điểm, môn học được chọn phải tồn tại trong hệ thống quản lý môn học. <br> VD: **Đầu vào hợp lệ:** Chọn môn học "Toán". **Đầu vào không hợp lệ:** Nhập thủ công tên môn học "Mỹ thuật" không tồn tại, hệ thống phải báo lỗi. |

---

## 8. Integration Requirements

- **Tích hợp với Module Quản lý Học sinh:** Truy vấn dữ liệu học sinh (ID, Tên, Lớp) để hiển thị và liên kết điểm.
- **Tích hợp với Module Quản lý Môn học:** Truy vấn dữ liệu môn học (ID, Tên) để hiển thị và liên kết điểm.
- **Tích hợp với Module Quản lý Lớp học:** Truy vấn dữ liệu lớp học (ID, Tên) để lọc và hiển thị thông tin điểm.
- **Tích hợp với Module Quản lý Giáo viên:** Truy vấn thông tin phân công giảng dạy của giáo viên để áp dụng phân quyền nhập liệu.
- **Tích hợp với Hệ thống Xác thực & Phân quyền:** Sử dụng API/service hiện có để kiểm tra quyền của người dùng trước khi cho phép thực hiện các thao tác.

---

## 9. Audit Trail

- Hệ thống phải ghi lại các thao tác quan trọng:
    - **Tạo mới điểm:** Ghi lại ID điểm, ID học sinh, ID môn học, học kỳ, điểm số, người tạo, thời gian tạo.
    - **Cập nhật điểm:** Ghi lại ID điểm, người cập nhật, thời gian cập nhật, và giá trị điểm trước/sau khi thay đổi (chỉ cho trường "Điểm số").
    - **Xóa điểm:** Ghi lại ID điểm, ID học sinh, ID môn học, học kỳ, điểm số, người xóa, thời gian xóa.
- Các log này cần được lưu trữ an toàn và có thể truy vấn bởi Cán bộ quản lý để phục vụ mục đích kiểm tra và đối chiếu.

---

## 10. KPI & Metrics theo dõi

- **Tỷ lệ lỗi nhập liệu điểm:** Số lỗi nhập liệu trên tổng số lần nhập liệu. Mục tiêu: < 0.5%.
- **Thời gian trung bình để nhập/cập nhật điểm:** Đo lường thời gian cần thiết để hoàn thành một tác vụ nhập/cập nhật điểm. Mục tiêu: Giảm 20% so với phương pháp thủ công.
- **Số lượt truy cập tính năng:** Số lần người dùng truy cập trang quản lý điểm.
- **Số lượng bản ghi điểm được tạo/cập nhật/xóa thành công:** Tổng số giao dịch thành công.
- **Mức độ hài lòng của người dùng:** Khảo sát người dùng (Giáo viên, Cán bộ quản lý) về tính dễ sử dụng và hiệu quả của tính năng. Mục tiêu: > 80% hài lòng.

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1502-FR01` | Tạo mới điểm thành công | Giáo viên đã đăng nhập và được phân quyền nhập điểm môn Toán cho lớp 10A.<br>Form nhập điểm trống và hợp lệ.<br>Dữ liệu: Mã HS="HS001", Môn học="Toán", Học kỳ="I", Điểm="8.5". | Người dùng nhập đầy đủ thông tin vào form và nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Tạo mới điểm thành công".<br>Điểm "8.5" của HS001 môn Toán học kỳ I được lưu vào cơ sở dữ liệu.<br>Dữ liệu điểm mới xuất hiện trong danh sách. |
| `QUAN-20260531-1502-FR01` | Cập nhật điểm thành công | Cán bộ quản lý đã đăng nhập.<br>Điểm của HS002 môn Lý học kỳ II hiện là "6.0".<br>Người dùng chọn mục điểm này và mở form cập nhật. | Người dùng thay đổi Điểm thành "7.5" và nhấn nút "Lưu". | Hệ thống hiển thị thông báo "Cập nhật điểm thành công".<br>Điểm của HS002 môn Lý học kỳ II trong cơ sở dữ liệu được cập nhật thành "7.5".<br>Giá trị hiển thị trên danh sách được thay đổi. |
| `QUAN-20260531-1502-FR01` | Xóa điểm thành công | Cán bộ quản lý đã đăng nhập.<br>Điểm của HS003 môn Văn học kỳ I đang tồn tại và học kỳ chưa khóa sổ.<br>Người dùng chọn mục điểm này. | Người dùng nhấn nút "Xóa" và xác nhận trong hộp thoại. | Hệ thống hiển thị thông báo "Xóa điểm thành công".<br>Dữ liệu điểm của HS003 môn Văn học kỳ I bị xóa khỏi cơ sở dữ liệu.<br>Mục điểm không còn xuất hiện trong danh sách. |
| `QUAN-20260531-1502-FR02` | Tìm kiếm điểm theo học sinh | Cán bộ quản lý đã đăng nhập.<br>Danh sách điểm có các học sinh Nguyễn Văn A, Trần Thị B, Lê Văn C.<br>Trường tìm kiếm "Học sinh" đang trống. | Người dùng nhập "Nguyễn" vào trường tìm kiếm "Học sinh" và nhấn "Tìm kiếm". | Danh sách điểm chỉ hiển thị các bản ghi có tên học sinh chứa "Nguyễn" (ví dụ: Nguyễn Văn A). |
| `QUAN-20260531-1502-FR03` | Phân trang danh sách điểm | Giáo viên đã đăng nhập.<br>Có 50 bản ghi điểm phù hợp với tiêu chí lọc.<br>Cài đặt hiển thị 10 bản ghi/trang. | Người dùng truy cập danh sách điểm. | Hệ thống hiển thị 10 bản ghi đầu tiên.<br>Các điều khiển phân trang (trang 1/5, nút Previous/Next) được hiển thị chính xác. |
| `QUAN-20260531-1502-FR06` | Học sinh xem điểm của bản thân | Học sinh đã đăng nhập với tài khoản của mình.<br>Thông tin điểm của học sinh đó đã có trong hệ thống. | Học sinh truy cập tính năng "Xem điểm học kỳ". | Hệ thống hiển thị danh sách tất cả các điểm của học sinh đó theo từng môn học và học kỳ.<br>Không có nút chức năng để tạo/sửa/xóa điểm. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-1502-FR04` | Nhập điểm ngoài khoảng hợp lệ | Giáo viên đã đăng nhập.<br>Form nhập điểm trống.<br>Dữ liệu: Mã HS="HS001", Môn học="Toán", Học kỳ="I", Điểm="11.0". | Người dùng nhập điểm "11.0" và nhấn nút "Lưu". | Hệ thống hiển thị thông báo lỗi validation "Điểm phải nằm trong khoảng từ 0 đến 10" dưới trường Điểm. |
| `QUAN-20260531-1502-BR02` | Tạo mới điểm trùng lặp | Giáo viên đã đăng nhập.<br>Điểm của HS001 môn Toán học kỳ I đã tồn tại với giá trị "7.0".<br>Người dùng mở form nhập điểm mới. | Người dùng nhập Mã HS="HS001", Môn học="Toán", Học kỳ="I", Điểm="8.0" và nhấn "Lưu". | Hệ thống hiển thị thông báo lỗi "Điểm cho học sinh này, môn học này, học kỳ này đã tồn tại". |
| `QUAN-20260531-1502-BR03` | Giáo viên nhập điểm không thuộc quyền | Giáo viên B đã đăng nhập.<br>Giáo viên B được phân công dạy môn Văn, không dạy môn Hóa.<br>Người dùng mở form nhập điểm mới. | Người dùng chọn Mã HS="HS004", Môn học="Hóa", Học kỳ="II", Điểm="7.0" và nhấn "Lưu". | Hệ thống hiển thị thông báo lỗi "Bạn không có quyền nhập điểm cho môn học này". |
| `QUAN-20260531-1502-BR04` | Xóa điểm của học kỳ đã khóa sổ | Cán bộ quản lý đã đăng nhập.<br>Điểm của HS005 môn Lý học kỳ II năm 2024-2025 đã tồn tại và học kỳ này đã được đánh dấu là "khóa sổ".<br>Người dùng chọn mục điểm này. | Người dùng nhấn nút "Xóa" và xác nhận trong hộp thoại. | Hệ thống hiển thị thông báo lỗi "Không thể xóa điểm vì học kỳ đã khóa sổ".<br>Dữ liệu điểm vẫn còn trong cơ sở dữ liệu. |
| `QUAN-20260531-1502-FR05` | Học sinh truy cập tính năng quản lý | Học sinh đã đăng nhập.<br>Tính năng "Quản lý Điểm Học Kỳ" nằm trong menu. | Học sinh cố gắng truy cập tính năng "Quản lý Điểm Học Kỳ" (thay vì "Xem điểm của tôi"). | Hệ thống hiển thị thông báo lỗi "Bạn không có quyền truy cập chức năng này" hoặc chuyển hướng về trang xem điểm của học sinh. |

---

_Mã tính năng `QUAN-20260531-1502` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._