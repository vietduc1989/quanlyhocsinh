# Business Requirements Document (BRD)

| Metadata | Giá trị |
|----------|---------|
| **Mã tính năng** | `QUAN-20260531-150408` |
| **Tên tính năng** | Quản Lý Môn Học |
| **Dự án** | quanlyhocsinh |
| **Phiên bản** | 1.0 |
| **Ngày tạo** | 2026-05-31 |
| **Tác giả** | BA Agent (ONENET AgentFactory) |

## Tài liệu liên quan

| Mã tính năng | Loại tài liệu | Trạng thái |
|-------------|---------------|------------|
| `QUAN-20260531-150408` | **BRD** (tài liệu này) | ✅ Hiện tại |
| `QUAN-20260531-150408` | SRS/SAD | ⏳ Chờ BA duyệt |
| `QUAN-20260531-150408` | DEV (mã nguồn) | ⏳ Chờ SRS duyệt |
| `QUAN-20260531-150408` | TEST (test cases) | ⏳ Chờ DEV duyệt |

---

## 1. Mục tiêu

### 1.1 Mục tiêu chính
Cung cấp một hệ thống cho phép Quản trị viên và Cán bộ phòng Đào tạo quản lý các thông tin về môn học một cách hiệu quả, bao gồm việc tạo mới, xem, chỉnh sửa, và xóa các môn học, đồng thời hỗ trợ tìm kiếm và phân trang dữ liệu.

### 1.2 Mục tiêu phụ
- Đảm bảo tính nhất quán và chính xác của dữ liệu môn học trong toàn hệ thống.
- Giảm thiểu thời gian và công sức quản lý thủ công các môn học.
- Nâng cao trải nghiệm người dùng thông qua giao diện quản lý trực quan và dễ sử dụng.
- Đặt nền tảng cho các tính năng liên quan như xếp lớp, phân công giảng dạy, và quản lý kết quả học tập.

---

## 2. Phạm vi, Giả định và Phụ thuộc

### 2.1 In Scope (Trong phạm vi)
- Tạo mới thông tin môn học (Mã môn học, Tên môn học, Số tín chỉ, Mô tả).
- Xem danh sách các môn học hiện có, bao gồm phân trang và hiển thị số lượng bản ghi.
- Cập nhật thông tin chi tiết của môn học.
- Xóa một hoặc nhiều môn học.
- Tìm kiếm môn học theo Mã môn học hoặc Tên môn học.
- Kiểm tra tính hợp lệ của dữ liệu đầu vào (validation) cho các trường thông tin môn học.

### 2.2 Out of Scope (Ngoài phạm vi)
- Quản lý tài liệu học tập của môn học.
- Phân công giáo viên cho môn học.
- Đăng ký môn học cho sinh viên.
- Quản lý lịch học của môn học.
- Xuất/Nhập dữ liệu môn học từ/đến các định dạng khác (ví dụ: Excel, CSV).

### 2.3 Giả định (Assumptions)
- Người dùng có vai trò và quyền hạn phù hợp để truy cập và thực hiện các thao tác quản lý môn học.
- Hệ thống cơ bản (đăng nhập, quản lý người dùng) đã được triển khai và hoạt động ổn định.
- Mã môn học và Tên môn học là duy nhất trong toàn hệ thống.
- Các trường thông tin về môn học được hiển thị rõ ràng trên giao diện.

### 2.4 Phụ thuộc (Dependencies)
- Module Xác thực & Phân quyền (Authentication & Authorization) để đảm bảo chỉ những người dùng có thẩm quyền mới có thể truy cập và thao tác.
- Module Giao diện người dùng (UI/UX Framework) để xây dựng giao diện quản lý môn học.

---

## 3. Nhóm người dùng (User Personas)

| # | Persona | Vai trò | Quyền hạn chính | Mục tiêu sử dụng |
|---|---------|-------|-----------------|------------------|
| 1 | Quản trị viên hệ thống | Admin | Toàn quyền CRUD môn học, quản lý người dùng | Đảm bảo dữ liệu môn học được quản lý chính xác, hỗ trợ các phòng ban khác. |
| 2 | Cán bộ Phòng Đào tạo | Giáo vụ | Xem, tạo, sửa, xóa thông tin môn học | Quản lý danh mục môn học phục vụ cho việc xếp lớp, đăng ký học, quản lý chương trình đào tạo. |

---

## 4. User Flow nghiệp vụ (Mermaid Diagram)

> Sơ đồ luồng nghiệp vụ chi tiết. Mọi luồng chính (Happy Path) và luồng rẽ nhánh (Edge Cases) cần được thể hiện.

```mermaid
flowchart TD
    A[Bắt đầu] --> B{Đăng nhập thành công?};
    B -- Có --> C[Truy cập chức năng Quản lý Môn học];
    B -- Không --> D[Hiển thị lỗi đăng nhập];

    C --> E[Hiển thị Danh sách Môn học (có phân trang & tìm kiếm)];

    E --> F{Hành động của người dùng?};

    F -- Tìm kiếm --> G[Nhập từ khóa tìm kiếm];
    G --> H[Hiển thị kết quả tìm kiếm];
    H --> E;

    F -- Phân trang --> I[Chọn trang kế tiếp/trước đó];
    I --> J[Tải và hiển thị dữ liệu trang mới];
    J --> E;

    F -- Thêm mới --> K[Click nút "Thêm mới"];
    K --> L[Hiển thị form Tạo mới Môn học];
    L --> M{Nhập thông tin hợp lệ?};
    M -- Có --> N[Lưu Môn học vào hệ thống];
    N --> O[Thông báo "Thêm mới thành công"];
    O --> E;
    M -- Không --> P[Hiển thị lỗi Validation trên Form];
    P --> L;

    F -- Chỉnh sửa --> Q[Chọn Môn học cần sửa];
    Q --> R[Hiển thị form Chỉnh sửa Môn học (preload dữ liệu)];
    R --> S{Cập nhật thông tin hợp lệ?};
    S -- Có --> T[Lưu thay đổi vào hệ thống];
    T --> U[Thông báo "Cập nhật thành công"];
    U --> E;
    S -- Không --> V[Hiển thị lỗi Validation trên Form];
    V --> R;

    F -- Xóa --> W[Chọn Môn học cần xóa];
    W --> X{Xác nhận xóa?};
    X -- Có --> Y[Xóa Môn học khỏi hệ thống];
    Y --> Z[Thông báo "Xóa thành công"];
    Z --> E;
    X -- Không --> E;

    E --> End([Kết thúc]);
```

---

## 5. Functional Requirements (FR)

> Chuyển đổi yêu cầu thành định dạng User Story: `As a [persona], I want to [action] so that [benefit]`

| Mã FR | Tên FR | User Story | Độ ưu tiên |
|--------|-----|------------|-----------|
| `QUAN-20260531-150408-FR01` | Xem danh sách Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to xem danh sách tất cả các môn học hiện có, so that I can có cái nhìn tổng quan và lựa chọn môn học để thao tác. | Must |
| `QUAN-20260531-150408-FR02` | Tạo mới Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to tạo mới một môn học với các thông tin cơ bản, so that I can bổ sung các môn học mới vào hệ thống. | Must |
| `QUAN-20260531-150408-FR03` | Cập nhật thông tin Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to chỉnh sửa thông tin chi tiết của một môn học đã có, so that I can duy trì tính chính xác và cập nhật của dữ liệu môn học. | Must |
| `QUAN-20260531-150408-FR04` | Xóa Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to xóa một môn học không còn cần thiết, so that I can loại bỏ dữ liệu không hợp lệ hoặc lỗi thời khỏi hệ thống. | Must |
| `QUAN-20260531-150408-FR05` | Tìm kiếm Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to tìm kiếm môn học theo Mã hoặc Tên, so that I can nhanh chóng tìm thấy môn học mong muốn trong danh sách dài. | Must |
| `QUAN-20260531-150408-FR06` | Phân trang danh sách Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to xem danh sách môn học theo từng trang, so that I can dễ dàng duyệt qua một lượng lớn dữ liệu mà không bị quá tải giao diện. | Must |
| `QUAN-20260531-150408-FR07` | Xác thực dữ liệu Môn học | As a Quản trị viên/Cán bộ Phòng Đào tạo, I want to được hệ thống kiểm tra tính hợp lệ của dữ liệu khi tạo/cập nhật môn học, so that I can đảm bảo dữ liệu nhập vào là chính xác và tuân thủ quy tắc nghiệp vụ. | Must |

---

## 6. Non-functional Requirements (NFR)

### 6.1 Performance
- **Thời gian phản hồi:**
    - Tải danh sách môn học (với phân trang và tìm kiếm): Dưới 2 giây cho tối đa 1000 bản ghi.
    - Thao tác Thêm/Sửa/Xóa một môn học: Dưới 1 giây.
- **Số lượng người dùng đồng thời:** Hệ thống phải hỗ trợ ít nhất 50 người dùng đồng thời thao tác trên chức năng Quản lý Môn học mà không ảnh hưởng đáng kể đến hiệu suất.

### 6.2 Security
- **Xác thực & Phân quyền:** Chỉ những người dùng có quyền "Quản trị viên hệ thống" hoặc "Cán bộ Phòng Đào tạo" mới có thể truy cập và thao tác trên chức năng này.
- **Bảo vệ dữ liệu:** Dữ liệu đầu vào phải được làm sạch (input sanitization) để ngăn chặn các cuộc tấn công injection (ví dụ: SQL Injection, XSS).
- **Ghi nhật ký:** Mọi thao tác tạo, sửa, xóa môn học phải được ghi lại trong nhật ký hệ thống (Audit Trail) để phục vụ mục đích kiểm tra và truy vết.

### 6.3 Availability
- Hệ thống cần có thời gian hoạt động (uptime) tối thiểu 99.5% trong giờ làm việc.

---

## 7. Business Rules (BR)

| Mã BR | Quy tắc | Mô tả & Ví dụ |
|--------|---------|---------------|
| `QUAN-20260531-150408-BR01` | Mã môn học là duy nhất | Mã môn học phải là duy nhất trong toàn hệ thống. Nếu có môn học khác đã tồn tại với cùng mã, hệ thống phải báo lỗi. <br> VD: <br> - **Đầu vào hợp lệ:** Mã: "IT101", Tên: "Nhập môn Lập trình" <br> - **Đầu vào không hợp lệ:** Khi tạo môn học mới với Mã: "IT101", nếu đã có môn học "IT101 - Nhập môn Công nghệ thông tin" tồn tại, hệ thống báo lỗi: "Mã môn học đã tồn tại." |
| `QUAN-20260531-150408-BR02` | Tên môn học là duy nhất | Tên môn học phải là duy nhất trong toàn hệ thống (không phân biệt chữ hoa/thường). <br> VD: <br> - **Đầu vào hợp lệ:** Tên: "Toán cao cấp A", Mã: "MA101" <br> - **Đầu vào không hợp lệ:** Khi tạo môn học mới với Tên: "Toán cao cấp A", nếu đã có môn học "MA102 - Toán Cao Cấp A" tồn tại, hệ thống báo lỗi: "Tên môn học đã tồn tại." |
| `QUAN-20260531-150408-BR03` | Số tín chỉ là số nguyên dương | Số tín chỉ phải là một số nguyên dương (>0). <br> VD: <br> - **Đầu vào hợp lệ:** Số tín chỉ: 3 <br> - **Đầu vào không hợp lệ:** Số tín chỉ: 0, -1, 2.5, "abc". Hệ thống báo lỗi: "Số tín chỉ phải là số nguyên dương." |
| `QUAN-20260531-150408-BR04` | Không được xóa môn học đã liên kết | Môn học không được phép xóa nếu nó đang được liên kết với bất kỳ khóa học, lớp học hoặc sinh viên nào đã được ghi nhận trong hệ thống. <br> VD: <br> - **Đầu vào hợp lệ:** Xóa môn học "Lịch sử Đảng" (MA123) chưa có liên kết. <br> - **Đầu vào không hợp lệ:** Xóa môn học "Cơ sở dữ liệu" (CSD201) khi môn học này đang là một phần của chương trình đào tạo "Kỹ thuật phần mềm Khóa 2023" hoặc có sinh viên đã đăng ký. Hệ thống báo lỗi: "Không thể xóa môn học vì có dữ liệu liên quan." |

---

## 8. Integration Requirements

- Không có yêu cầu tích hợp với các hệ thống bên thứ ba trong phạm vi tính năng Quản lý Môn học này.

---

## 9. Audit Trail

- Hệ thống phải ghi lại nhật ký (log) cho các thao tác quan trọng:
    - Tạo mới một môn học: Ghi lại thông tin người tạo, thời gian tạo, và dữ liệu môn học được tạo.
    - Cập nhật thông tin một môn học: Ghi lại thông tin người cập nhật, thời gian cập nhật, mã môn học, và chi tiết các trường dữ liệu đã thay đổi (trước và sau).
    - Xóa một môn học: Ghi lại thông tin người xóa, thời gian xóa, và mã/tên môn học đã bị xóa.
- Nhật ký cần được lưu trữ an toàn và có thể truy vấn bởi Quản trị viên hệ thống.

---

## 10. KPI & Metrics theo dõi

- **Số lượng môn học:** Tổng số môn học hiện có trong hệ thống.
- **Tần suất thao tác:** Số lượng thao tác tạo, sửa, xóa môn học trung bình mỗi ngày/tuần.
- **Thời gian phản hồi:** Thời gian phản hồi trung bình của các chức năng xem, tạo, sửa, xóa môn học.
- **Tỷ lệ lỗi:** Tỷ lệ các thao tác quản lý môn học gặp lỗi (ví dụ: lỗi validation, lỗi hệ thống).

---

## 11. Acceptance Criteria (Tiêu chí chấp nhận - BDD)

> Viết tiêu chí chấp nhận cho từng FR theo chuẩn BDD (Given / When / Then).

### Luồng chính (Happy Path)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-150408-FR01` | Xem danh sách thành công | Người dùng đã đăng nhập với vai trò "Cán bộ Phòng Đào tạo" và có quyền truy cập chức năng Quản lý Môn học. | Người dùng điều hướng đến trang "Quản lý Môn học". | Hệ thống hiển thị danh sách các môn học hiện có, bao gồm Mã, Tên, Số tín chỉ, Mô tả (nếu có), và các nút chức năng (Sửa, Xóa). |
| `QUAN-20260531-150408-1` | Tạo mới thành công | Người dùng đã đăng nhập với vai trò "Quản trị viên hệ thống". <br> Người dùng điền đầy đủ và hợp lệ các thông tin: <br> - Mã môn học: "CSD101" <br> - Tên môn học: "Cấu trúc dữ liệu" <br> - Số tín chỉ: 3 <br> - Mô tả: "Môn học về các cấu trúc dữ liệu cơ bản." | Người dùng bấm nút "Lưu" (hoặc "Tạo mới"). | Hệ thống hiển thị thông báo "Tạo mới môn học thành công", và môn học "CSD101 - Cấu trúc dữ liệu" xuất hiện trong danh sách môn học. |
| `QUAN-20260531-150408-FR03` | Cập nhật thành công | Môn học "IT101 - Nhập môn Lập trình" (3 tín chỉ) đã tồn tại trong hệ thống. <br> Người dùng đã đăng nhập với vai trò "Cán bộ Phòng Đào tạo". <br> Người dùng chọn chức năng "Sửa" cho môn học "IT101" và cập nhật: <br> - Tên môn học: "Nhập môn Lập trình cơ bản" <br> - Số tín chỉ: 2 | Người dùng bấm nút "Lưu" (hoặc "Cập nhật"). | Hệ thống hiển thị thông báo "Cập nhật môn học thành công", và thông tin môn học "IT101" được hiển thị là "Nhập môn Lập trình cơ bản" với 2 tín chỉ trong danh sách. |
| `QUAN-20260531-150408-FR04` | Xóa thành công | Môn học "LICHSD01 - Lịch sử Đảng Cộng sản Việt Nam" đã tồn tại và chưa có bất kỳ liên kết nào với khóa học/lớp học/sinh viên. <br> Người dùng đã đăng nhập với vai trò "Quản trị viên hệ thống". <br> Người dùng chọn chức năng "Xóa" cho môn học "LICHSD01". | Người dùng xác nhận thao tác xóa. | Hệ thống hiển thị thông báo "Xóa môn học thành công", và môn học "LICHSD01" không còn xuất hiện trong danh sách. |
| `QUAN-20260531-150408-FR05` | Tìm kiếm theo Tên Môn học | Danh sách môn học có chứa "Kỹ thuật phần mềm" và "Phân tích thiết kế hệ thống". <br> Người dùng đã đăng nhập với vai trò "Cán bộ Phòng Đào tạo". | Người dùng nhập "Kỹ thuật" vào ô tìm kiếm và bấm nút "Tìm". | Hệ thống hiển thị danh sách chỉ bao gồm môn học "Kỹ thuật phần mềm" và các môn học khác có chứa từ "Kỹ thuật" trong tên, và không hiển thị "Phân tích thiết kế hệ thống". |
| `QUAN-20260531-150408-FR06` | Phân trang hiển thị đúng | Có tổng cộng 50 môn học trong hệ thống, cấu hình hiển thị 10 môn học mỗi trang. <br> Người dùng đã đăng nhập. | Người dùng truy cập trang "Quản lý Môn học". | Hệ thống hiển thị 10 môn học đầu tiên và các điều khiển phân trang (ví dụ: Trang 1/5, nút "Trang sau"). Khi người dùng bấm "Trang sau", hệ thống hiển thị 10 môn học tiếp theo. |

### Luồng lỗi (Unhappy Path / Edge Cases)

| Mã FR | Scenario | Given (Đầu vào/Điều kiện) | When (Hành động) | Then (Kết quả mong đợi) |
|-------|----------|---------------------------|------------------|-------------------------|
| `QUAN-20260531-150408-FR02` | Bỏ trống Mã môn học | Người dùng đã đăng nhập. <br> Người dùng điền: <br> - Tên môn học: "Cấu trúc dữ liệu" <br> - Số tín chỉ: 3 <br> - Mã môn học: (bỏ trống) | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi validation đỏ dưới trường "Mã môn học": "Mã môn học không được để trống." |
| `QUAN-20260531-150408-FR02` | Mã môn học bị trùng | Môn học "IT101 - Nhập môn Lập trình" đã tồn tại. <br> Người dùng đã đăng nhập. <br> Người dùng điền thông tin tạo mới: <br> - Mã môn học: "IT101" <br> - Tên môn học: "Lập trình nâng cao" <br> - Số tín chỉ: 3 | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi: "Mã môn học 'IT101' đã tồn tại. Vui lòng nhập mã khác." |
| `QUAN-20260531-150408-FR03` | Cập nhật Tên môn học bị trùng | Môn học "MA101 - Đại số tuyến tính" và "MA102 - Giải tích 1" đã tồn tại. <br> Người dùng chọn sửa môn học "MA101" và thay đổi: <br> - Tên môn học: "Giải tích 1" | Người dùng bấm nút "Lưu". | Hệ thống hiển thị thông báo lỗi: "Tên môn học 'Giải tích 1' đã tồn tại. Vui lòng nhập tên khác." |
| `QUAN-20260531-150408-FR04` | Xóa môn học có liên kết | Môn học "CSD201 - Cơ sở dữ liệu" đang được liên kết với một khóa học hoặc lớp học có sinh viên. <br> Người dùng đã đăng nhập với vai trò "Quản trị viên hệ thống". | Người dùng chọn chức năng "Xóa" cho môn học "CSD201" và xác nhận. | Hệ thống hiển thị thông báo lỗi: "Không thể xóa môn học 'CSD201' vì đang có dữ liệu liên quan." |
| `QUAN-20260531-150408-FR05` | Tìm kiếm không có kết quả | Danh sách môn học không có môn nào chứa từ khóa "Vật lý lượng tử". <br> Người dùng đã đăng nhập. | Người dùng nhập "Vật lý lượng tử" vào ô tìm kiếm và bấm nút "Tìm". | Hệ thống hiển thị thông báo "Không tìm thấy môn học nào phù hợp với từ khóa 'Vật lý lượng tử'." và danh sách môn học trống. |

---

_Mã tính năng `QUAN-20260531-150408` — Tài liệu BRD được sinh tự động bởi ONENET AgentFactory._