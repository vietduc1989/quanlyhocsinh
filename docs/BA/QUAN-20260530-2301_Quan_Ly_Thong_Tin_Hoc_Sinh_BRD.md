Chào bạn,

Với vai trò là Business Analyst cấp cao của hệ thống ONENET, tôi đã tiếp nhận yêu cầu và tiến hành phân tích nghiệp vụ cho tính năng "Quản lý Thông tin Học sinh" thuộc dự án `quanlyhocsinh`. Dưới đây là tài liệu BRD chi tiết.

---

# Business Requirements Document (BRD)

## Tiêu đề tài liệu & Thông tin chung

*   **Tên tài liệu:** Tài liệu Yêu cầu Nghiệp vụ - Tính năng Quản lý Thông tin Học sinh
*   **Mã tính năng:** `QUAN-20260530-2301`
*   **Tên tính năng:** Quản lý Thông tin Học sinh
*   **Dự án:** `quanlyhocsinh`
*   **Ngày:** 2026-05-30
*   **Phiên bản:** 1.0
*   **Người soạn:** [Tên Business Analyst]

## Mục lục

1.  Giới thiệu
    1.1. Mục đích tài liệu
    1.2. Phạm vi
    1.3. Tổng quan hệ thống
2.  Các bên liên quan & Người dùng (Actors)
3.  Yêu cầu Nghiệp vụ
    3.1. Mục tiêu Nghiệp vụ
    3.2. Yêu cầu Chức năng (Functional Requirements - FR)
    3.3. Quy tắc Nghiệp vụ (Business Rules - BR)
    3.4. Yêu cầu Phi chức năng (Non-Functional Requirements - NFR)
4.  Các trường hợp sử dụng (Use Cases)
    4.1. Use Case Diagram (Tổng quan)
    4.2. Chi tiết Use Case: Quản lý Thông tin Học sinh
5.  Từ điển dữ liệu (Data Dictionary)
6.  Ràng buộc & Giả định
7.  Tiêu chí chấp nhận tổng thể

---

## 1. Giới thiệu

### 1.1. Mục đích tài liệu

Tài liệu này nhằm mục đích mô tả chi tiết các yêu cầu nghiệp vụ cho tính năng "Quản lý Thông tin Học sinh" trong hệ thống `quanlyhocsinh`. Nó cung cấp một cái nhìn tổng quan về các chức năng cần thiết, quy tắc nghiệp vụ, và các tiêu chí chấp nhận, phục vụ làm cơ sở cho quá trình thiết kế, phát triển và kiểm thử phần mềm.

### 1.2. Phạm vi

Phạm vi của tính năng này bao gồm việc cho phép người dùng có thẩm quyền thực hiện các thao tác CRUD (Tạo, Đọc, Cập nhật, Xóa) đối với thông tin học sinh, cùng với các chức năng hỗ trợ như tìm kiếm, lọc và phân trang dữ liệu.

### 1.3. Tổng quan hệ thống

Hệ thống `quanlyhocsinh` là một nền tảng được thiết kế để hỗ trợ các trường học, trung tâm giáo dục trong việc quản lý thông tin học sinh, giáo viên, lịch học, điểm số, và các hoạt động khác liên quan đến quá trình đào tạo. Tính năng "Quản lý Thông tin Học sinh" là một thành phần cốt lõi, cung cấp khả năng duy trì và tra cứu dữ liệu cơ bản về học sinh.

## 2. Các bên liên quan & Người dùng (Actors)

*   **Người quản trị (Administrator)**: Là người dùng có quyền cao nhất trong hệ thống, có thể thực hiện tất cả các thao tác CRUD, tìm kiếm, phân trang đối với thông tin học sinh.
*   **Giáo viên (Teacher)**: (Có thể có, tùy thuộc vào phân quyền cụ thể, nhưng với yêu cầu ban đầu, chủ yếu là Administrator) Có thể xem thông tin học sinh được phân công.
*   **Hệ thống (System)**: Tương tác để lưu trữ và truy xuất dữ liệu.

## 3. Yêu cầu Nghiệp vụ

### 3.1. Mục tiêu Nghiệp vụ

*   Cung cấp một giao diện trực quan và dễ sử dụng cho việc quản lý thông tin học sinh.
*   Đảm bảo tính toàn vẹn và chính xác của dữ liệu học sinh thông qua các quy tắc nghiệp vụ và kiểm tra hợp lệ.
*   Nâng cao hiệu quả trong việc tìm kiếm và truy xuất thông tin học sinh.
*   Hỗ trợ quản lý số lượng lớn dữ liệu học sinh một cách hiệu quả thông qua phân trang.

### 3.2. Yêu cầu Chức năng (Functional Requirements - FR)

#### 3.2.1. Quản lý danh sách Học sinh

*   **`QUAN-20260530-2301-FR01`**: Hệ thống phải hiển thị danh sách tất cả các học sinh.
    *   **Acceptance Criteria**:
        *   Màn hình "Quản lý Học sinh" hiển thị bảng danh sách học sinh.
        *   Mỗi dòng trong bảng hiển thị các thông tin tóm tắt như: Mã Học sinh, Họ và Tên, Lớp Học, Trạng Thái.
        *   Danh sách học sinh phải được sắp xếp mặc định theo tên hoặc mã học sinh tăng dần.
*   **`QUAN-20260530-2301-FR02`**: Người quản trị phải có khả năng xem chi tiết thông tin của một học sinh.
    *   **Acceptance Criteria**:
        *   Khi người quản trị chọn một học sinh từ danh sách (ví dụ: nhấp vào tên, nút "Xem chi tiết"), hệ thống sẽ hiển thị một màn hình/popup chứa tất cả thông tin chi tiết của học sinh đó.

#### 3.2.2. Thêm mới Học sinh (Create)

*   **`QUAN-20260530-2301-FR03`**: Người quản trị phải có khả năng thêm mới thông tin một học sinh vào hệ thống.
    *   **Acceptance Criteria**:
        *   Hệ thống cung cấp một form nhập liệu đầy đủ các thông tin cần thiết của học sinh.
        *   Người quản trị có thể nhập thông tin và lưu lại.
        *   Sau khi lưu thành công, học sinh mới được thêm vào danh sách và hiển thị trên giao diện quản lý.
        *   Hệ thống hiển thị thông báo "Thêm học sinh thành công" khi thao tác thành công.

#### 3.2.3. Cập nhật thông tin Học sinh (Update)

*   **`QUAN-20260530-2301-FR04`**: Người quản trị phải có khả năng chỉnh sửa thông tin của một học sinh hiện có.
    *   **Acceptance Criteria**:
        *   Từ màn hình danh sách hoặc màn hình xem chi tiết, người quản trị có thể chọn chức năng "Chỉnh sửa" cho một học sinh.
        *   Hệ thống hiển thị form chỉnh sửa với các thông tin hiện tại của học sinh được điền sẵn.
        *   Người quản trị có thể thay đổi các trường thông tin và lưu lại.
        *   Sau khi lưu thành công, thông tin của học sinh được cập nhật trong hệ thống và hiển thị trên giao diện.
        *   Hệ thống hiển thị thông báo "Cập nhật học sinh thành công" khi thao tác thành công.

#### 3.2.4. Xóa Học sinh (Delete)

*   **`QUAN-20260530-2301-FR05`**: Người quản trị phải có khả năng xóa một học sinh khỏi hệ thống.
    *   **Acceptance Criteria**:
        *   Người quản trị có thể chọn một hoặc nhiều học sinh từ danh sách.
        *   Khi chọn chức năng "Xóa", hệ thống hiển thị hộp thoại xác nhận trước khi thực hiện xóa.
        *   Nếu xác nhận, học sinh được chọn sẽ bị xóa vĩnh viễn khỏi hệ thống.
        *   Hệ thống hiển thị thông báo "Xóa học sinh thành công" khi thao tác thành công.
*   **`QUAN-20260530-2301-FR06`**: Hệ thống phải ngăn chặn việc xóa học sinh nếu học sinh đó có dữ liệu liên quan (ví dụ: điểm số, lịch sử học tập) mà không có cơ chế xử lý dữ liệu liên quan.
    *   **Acceptance Criteria**:
        *   Nếu học sinh có dữ liệu liên quan, hệ thống phải hiển thị thông báo lỗi "Không thể xóa học sinh vì có dữ liệu liên quan. Vui lòng xử lý dữ liệu liên quan trước." và không thực hiện xóa.
        *   (Ghi chú: Cần phân tích sâu hơn về chính sách xử lý dữ liệu liên quan: xóa cascade, đánh dấu không hoạt động, v.v. Trong phạm vi BRD này, chỉ dừng lại ở cảnh báo và ngăn chặn xóa).

#### 3.2.5. Tìm kiếm và Lọc (Search & Filter)

*   **`QUAN-20260530-2301-FR07`**: Người quản trị phải có khả năng tìm kiếm học sinh theo các tiêu chí khác nhau.
    *   **Acceptance Criteria**:
        *   Hệ thống cung cấp trường tìm kiếm chung (global search) cho phép tìm kiếm theo "Mã Học sinh" hoặc "Họ và Tên".
        *   Hệ thống cung cấp các tùy chọn lọc nâng cao theo "Lớp Học" và "Trạng Thái".
        *   Kết quả tìm kiếm/lọc phải hiển thị danh sách học sinh phù hợp với tiêu chí đã nhập.
        *   Tìm kiếm phải không phân biệt chữ hoa/thường.

#### 3.2.6. Phân trang (Pagination)

*   **`QUAN-20260530-2301-FR08`**: Hệ thống phải hỗ trợ phân trang cho danh sách học sinh.
    *   **Acceptance Criteria**:
        *   Danh sách học sinh được hiển thị theo từng trang, với số lượng học sinh cố định trên mỗi trang (ví dụ: 10, 20, 50 học sinh/trang).
        *   Hệ thống cung cấp các điều khiển phân trang (ví dụ: nút "Trang trước", "Trang sau", số trang cụ thể) để di chuyển giữa các trang.
        *   Thông tin về tổng số học sinh và số trang hiện tại phải được hiển thị rõ ràng.

### 3.3. Quy tắc Nghiệp vụ (Business Rules - BR)

*   **`QUAN-20260530-2301-BR01`**: Mã Học sinh phải là duy nhất trong toàn hệ thống.
    *   **Ví dụ**:
        *   Nếu người quản trị cố gắng thêm một học sinh với Mã Học sinh "HS001" mà đã có học sinh này tồn tại, hệ thống sẽ hiển thị lỗi "Mã Học sinh 'HS001' đã tồn tại. Vui lòng chọn mã khác."
*   **`QUAN-20260530-2301-BR02`**: Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" là bắt buộc nhập.
    *   **Ví dụ**:
        *   Nếu người quản trị cố gắng lưu một học sinh mới mà không điền "Họ và Tên", hệ thống sẽ hiển thị thông báo lỗi "Họ và Tên không được để trống."
*   **`QUAN-20260530-2301-BR03`**: Ngày Sinh phải là một ngày hợp lệ và không được lớn hơn hoặc bằng ngày hiện tại.
    *   **Ví dụ**:
        *   Nếu người quản trị nhập "Ngày Sinh" là "31/02/2005", hệ thống sẽ báo lỗi "Ngày Sinh không hợp lệ."
        *   Nếu người quản trị nhập "Ngày Sinh" là "2026-05-30" (ngày hiện tại hoặc tương lai), hệ thống sẽ báo lỗi "Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại."
*   **`QUAN-20260530-2301-BR04`**: Số Điện Thoại Phụ Huynh (nếu nhập) phải theo định dạng số điện thoại hợp lệ (ví dụ: chỉ chứa số, có thể có dấu '+' ở đầu, độ dài hợp lý).
    *   **Ví dụ**:
        *   Nếu người quản trị nhập "Số Điện Thoại Phụ Huynh" là "abc12345", hệ thống sẽ báo lỗi "Số Điện Thoại Phụ Huynh không đúng định dạng."
*   **`QUAN-20260530-2301-BR05`**: Email Phụ Huynh (nếu nhập) phải theo định dạng email hợp lệ.
    *   **Ví dụ**:
        *   Nếu người quản trị nhập "Email Phụ Huynh" là "john.doe@invalid", hệ thống sẽ báo lỗi "Email Phụ Huynh không đúng định dạng."
*   **`QUAN-20260530-2301-BR06`**: Trạng Thái của học sinh phải là một trong các giá trị được định nghĩa trước (ví dụ: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
    *   **Ví dụ**:
        *   Nếu người quản trị cố gắng nhập trạng thái là "Không tồn tại", hệ thống sẽ báo lỗi "Trạng Thái không hợp lệ. Vui lòng chọn từ danh sách."

### 3.4. Yêu cầu Phi chức năng (Non-Functional Requirements - NFR)

*   **Hiệu năng**:
    *   Thời gian tải danh sách học sinh (tối đa 1000 bản ghi) không quá 3 giây.
    *   Thời gian thực hiện thao tác CRUD (thêm/sửa/xóa) không quá 2 giây.
*   **Bảo mật**:
    *   Chỉ người dùng có quyền quản trị mới có thể thực hiện các thao tác CRUD.
    *   Dữ liệu học sinh phải được bảo vệ khỏi các truy cập trái phép.
*   **Khả năng sử dụng (Usability)**:
    *   Giao diện người dùng phải trực quan, dễ sử dụng, phù hợp với tiêu chuẩn thiết kế UI/UX của ONENET.
    *   Các thông báo lỗi và thành công phải rõ ràng, dễ hiểu.
*   **Khả năng mở rộng (Scalability)**:
    *   Hệ thống phải có khả năng xử lý việc tăng trưởng dữ liệu học sinh lên đến hàng chục nghìn bản ghi mà không ảnh hưởng đáng kể đến hiệu suất.

## 4. Các trường hợp sử dụng (Use Cases)

### 4.1. Use Case Diagram (Tổng quan)

```
+-------------------+       +------------------------------------+
| Administrator     |       | <<System>>                         |
|                   |------>| Quản lý Thông tin Học sinh         |
|                   |       |                                    |
+-------------------+       +------------------------------------+
       ^                              |              |
       |                              |              |
       |                              |              |
       |                   +----------v--------------v----------+
       |                   |           +------------+           |
       |                   |           |   Xem DS   |           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Xem Chi tiết|           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Thêm HS    |           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Sửa HS     |           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Xóa HS     |           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Tìm kiếm/Lọc|           |
       |                   |           +------------+           |
       |                   |                 |                  |
       |                   |           +-----v------+           |
       |                   |           | Phân trang |           |
       |                   |           +------------+           |
       +-------------------v------------------------------------+
```

### 4.2. Chi tiết Use Case: Quản lý Thông tin Học sinh

*   **Tên Use Case:** Quản lý Thông tin Học sinh
*   **Actor chính:** Người quản trị (Administrator)
*   **Mục tiêu:** Cho phép Người quản trị thực hiện các thao tác thêm, xem, sửa, xóa, tìm kiếm và phân trang thông tin học sinh một cách hiệu quả.

#### 4.2.1. Luồng chính (Basic Flow)

1.  Người quản trị đăng nhập vào hệ thống và truy cập chức năng "Quản lý Học sinh".
2.  Hệ thống hiển thị danh sách học sinh hiện có (theo `QUAN-20260530-2301-FR01`), bao gồm các thông tin tóm tắt và tùy chọn phân trang (`QUAN-20260530-2301-FR08`).
3.  Người quản trị có thể thực hiện một trong các hành động sau:
    *   **Thêm mới học sinh:**
        *   Chọn nút "Thêm mới".
        *   Hệ thống hiển thị form nhập liệu (đáp ứng `QUAN-20260530-2301-FR03`).
        *   Người quản trị nhập các thông tin cần thiết và chọn "Lưu".
        *   Hệ thống kiểm tra các ràng buộc nghiệp vụ (theo `QUAN-20260530-2301-BR01` đến `QUAN-20260530-2301-BR06`).
        *   Hệ thống lưu thông tin và cập nhật danh sách.
        *   Hệ thống hiển thị thông báo thành công.
    *   **Xem chi tiết học sinh:**
        *   Chọn một học sinh từ danh sách.
        *   Hệ thống hiển thị thông tin chi tiết của học sinh đó (đáp ứng `QUAN-20260530-2301-FR02`).
    *   **Chỉnh sửa học sinh:**
        *   Chọn một học sinh từ danh sách hoặc màn hình chi tiết, sau đó chọn nút "Chỉnh sửa".
        *   Hệ thống hiển thị form chỉnh sửa với dữ liệu hiện có (đáp ứng `QUAN-20260530-2301-FR04`).
        *   Người quản trị thay đổi thông tin và chọn "Lưu".
        *   Hệ thống kiểm tra các ràng buộc nghiệp vụ (theo `QUAN-20260530-2301-BR01` đến `QUAN-20260530-2301-BR06`).
        *   Hệ thống cập nhật thông tin và hiển thị thông báo thành công.
    *   **Xóa học sinh:**
        *   Chọn một hoặc nhiều học sinh từ danh sách, sau đó chọn nút "Xóa".
        *   Hệ thống yêu cầu xác nhận.
        *   Nếu Người quản trị xác nhận, hệ thống kiểm tra các ràng buộc (ví dụ: dữ liệu liên quan - `QUAN-20260530-2301-FR06`).
        *   Hệ thống xóa học sinh và cập nhật danh sách (đáp ứng `QUAN-20260530-2301-FR05`).
        *   Hệ thống hiển thị thông báo thành công.
    *   **Tìm kiếm/Lọc học sinh:**
        *   Nhập từ khóa vào ô tìm kiếm hoặc chọn tiêu chí lọc.
        *   Hệ thống hiển thị danh sách học sinh phù hợp với tiêu chí (đáp ứng `QUAN-20260530-2301-FR07`).
        *   Người quản trị có thể tiếp tục thực hiện các thao tác khác trên kết quả tìm kiếm/lọc.
4.  Người quản trị hoàn thành phiên làm việc.

#### 4.2.2. Luồng ngoại lệ (Alternate Flows)

*   **AE1: Dữ liệu nhập không hợp lệ khi thêm/sửa:**
    *   Nếu các quy tắc nghiệp vụ (`QUAN-20260530-2301-BR01` đến `QUAN-20260530-2301-BR06`) bị vi phạm, hệ thống hiển thị thông báo lỗi tương ứng và không lưu/cập nhật dữ liệu. Người quản trị phải sửa lỗi để tiếp tục.
*   **AE2: Không tìm thấy học sinh để chỉnh sửa/xóa:**
    *   Nếu học sinh được chọn để chỉnh sửa hoặc xóa không còn tồn tại trong hệ thống (có thể do đã bị xóa bởi người dùng khác), hệ thống hiển thị thông báo "Học sinh không tồn tại" và quay lại màn hình danh sách.
*   **AE3: Xóa học sinh có dữ liệu liên quan:**
    *   Nếu Người quản trị cố gắng xóa một học sinh có dữ liệu liên quan, hệ thống sẽ thực hiện theo `QUAN-20260530-2301-FR06` (hiển thị thông báo lỗi và không xóa).

#### 4.2.3. Điều kiện tiên quyết (Preconditions)

*   Người quản trị đã đăng nhập thành công vào hệ thống.
*   Người quản trị có quyền truy cập và quản lý thông tin học sinh.

#### 4.2.4. Điều kiện hậu kỳ (Postconditions)

*   **Thành công:** Thông tin học sinh được thêm/cập nhật/xóa thành công trong hệ thống, và danh sách học sinh được hiển thị chính xác.
*   **Thất bại:** Thông tin học sinh không thay đổi, và hệ thống hiển thị thông báo lỗi phù hợp.

## 5. Từ điển dữ liệu (Data Dictionary)

| Trường dữ liệu          | Mô tả                                      | Kiểu dữ liệu | Ràng buộc         | Ghi chú                                   |
| :---------------------- | :----------------------------------------- | :----------- | :---------------- | :---------------------------------------- |
| `MaHocSinh`             | Mã định danh duy nhất của học sinh       | Chuỗi (String)| Bắt buộc, Duy nhất | VD: HS2024001, tự sinh hoặc nhập tay     |
| `HoVaTen`               | Họ và tên đầy đủ của học sinh            | Chuỗi (String)| Bắt buộc, min: 3, max: 100|                                           |
| `NgaySinh`              | Ngày sinh của học sinh                   | Ngày (Date)  | Bắt buộc, < Ngày hiện tại |                                           |
| `GioiTinh`              | Giới tính của học sinh                   | Chuỗi (String)| Bắt buộc          | VD: "Nam", "Nữ", "Khác"                   |
| `DiaChi`                | Địa chỉ hiện tại của học sinh            | Chuỗi (String)| Tùy chọn, max: 255|                                           |
| `SDTPhuHuynh`           | Số điện thoại liên hệ của phụ huynh      | Chuỗi (String)| Tùy chọn, Định dạng SĐT|                                           |
| `EmailPhuHuynh`         | Địa chỉ email liên hệ của phụ huynh      | Chuỗi (String)| Tùy chọn, Định dạng Email|                                        |
| `LopHoc`                | Lớp học hiện tại của học sinh            | Chuỗi (String)| Bắt buộc, min: 2, max: 20| Có thể là ID nếu có module quản lý lớp học riêng|
| `NgayNhapHoc`           | Ngày học sinh chính thức nhập học        | Ngày (Date)  | Bắt buộc, <= Ngày hiện tại|                                           |
| `TrangThai`             | Trạng thái học tập của học sinh          | Chuỗi (String)| Bắt buộc          | VD: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng" |
| `NgayTao`               | Thời điểm bản ghi được tạo                | Ngày giờ (DateTime)| Hệ thống tự sinh |                                           |
| `NguoiTao`              | ID người dùng tạo bản ghi                | Chuỗi (String)| Hệ thống tự sinh |                                           |
| `NgayCapNhat`           | Thời điểm bản ghi được cập nhật lần cuối | Ngày giờ (DateTime)| Hệ thống tự sinh |                                           |
| `NguoiCapNhat`          | ID người dùng cập nhật bản ghi           | Chuỗi (String)| Hệ thống tự sinh |                                           |

## 6. Ràng buộc & Giả định

*   **Ràng buộc:**
    *   Hệ thống phải được xây dựng trên nền tảng công nghệ hiện có của ONENET (nếu có).
    *   Tính năng phải tuân thủ các quy định về bảo mật dữ liệu cá nhân (ví dụ: GDPR, Luật An ninh mạng).
    *   Thiết kế giao diện phải tuân thủ các hướng dẫn về UI/UX của ONENET.
*   **Giả định:**
    *   Người quản trị có đủ kiến thức và kỹ năng để sử dụng các chức năng quản lý học sinh.
    *   Hệ thống cơ sở dữ liệu đã có sẵn và có khả năng lưu trữ dữ liệu học sinh.
    *   Các danh mục như "Lớp Học" (nếu là danh mục động) hoặc "Trạng Thái" sẽ được cung cấp hoặc được định nghĩa trong phạm vi tính năng này.

## 7. Tiêu chí chấp nhận tổng thể

*   Tất cả các Functional Requirements (`QUAN-20260530-2301-FRxx`) phải được triển khai đầy đủ và hoạt động chính xác theo Acceptance Criteria đã định.
*   Tất cả các Business Rules (`QUAN-20260530-2301-BRxx`) phải được áp dụng và kiểm tra hợp lệ.
*   Hệ thống phải đáp ứng các yêu cầu phi chức năng về hiệu năng, bảo mật và khả năng sử dụng.
*   Giao diện người dùng phải thân thiện, trực quan và dễ thao tác cho Người quản trị.
*   Tài liệu hướng dẫn sử dụng (User Manual) và tài liệu kỹ thuật (Technical Design Document) phải được cập nhật tương ứng với tính năng mới này.

---

Đây là bản BRD chi tiết cho tính năng "Quản lý Thông tin Học sinh" dựa trên các yêu cầu đã được cung cấp. Vui lòng xem xét và phản hồi để chúng ta có thể điều chỉnh nếu cần thiết.