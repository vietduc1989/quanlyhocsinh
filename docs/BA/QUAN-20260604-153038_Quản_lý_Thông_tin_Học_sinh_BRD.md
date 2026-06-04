Tuyệt vời! Với vai trò là Business Analyst của ONENET, tôi sẽ phân tích các yêu cầu đã cho và xây dựng một tài liệu chi tiết, rõ ràng để đảm bảo đội phát triển có đủ thông tin để triển khai tính năng "Quản lý Thông tin Học sinh" một cách hiệu quả nhất.

Dưới đây là Tài liệu Đặc tả Yêu cầu Phần mềm (SRS) hoặc Bổ sung BRD chi tiết hơn, dựa trên thông tin bạn đã cung cấp:

---

# TÀI LIỆU ĐẶC TẢ YÊU CẦU PHẦN MỀM (SRS)

## 1. THÔNG TIN CHUNG

*   **Tên tài liệu:** Tài liệu Đặc tả Yêu cầu Phần mềm - Tính năng Quản lý Thông tin Học sinh
*   **Mã tính năng:** `QUAN-20260604-153038` (Sử dụng mã tính năng mới nhất từ thông tin khởi tạo)
*   **Tên tính năng:** Quản lý Thông tin Học sinh
*   **Dự án:** `quanlyhocsinh`
*   **Ngày:** 2026-06-04
*   **Phiên bản:** 1.0
*   **Người soạn:** Business Analyst - ONENET
*   **Mục đích tài liệu:** Tài liệu này cung cấp mô tả chi tiết về các yêu cầu chức năng, phi chức năng và quy tắc nghiệp vụ cho tính năng "Quản lý Thông tin Học sinh". Nó đóng vai trò là cơ sở để đội phát triển thiết kế, triển khai và kiểm thử phần mềm, đồng thời là tài liệu tham chiếu cho các bên liên quan.

## 2. PHẠM VI TÍNH NĂNG

Tính năng "Quản lý Thông tin Học sinh" cho phép người quản trị (Admin) thực hiện các thao tác quản lý cơ bản (CRUD - Create, Read, Update, Delete) đối với thông tin học sinh trong hệ thống `quanlyhocsinh`. Bao gồm việc xem danh sách, xem chi tiết, thêm mới, chỉnh sửa, xóa học sinh, cũng như tìm kiếm, lọc và phân trang dữ liệu.

## 3. CÁC BÊN LIÊN QUAN

*   **Người quản trị (Admin):** Đối tượng người dùng chính tương tác trực tiếp với tính năng này để quản lý dữ liệu học sinh.
*   **Đội phát triển (Development Team):** Đội ngũ chịu trách nhiệm thiết kế và triển khai tính năng.
*   **Đội kiểm thử (QA Team):** Đội ngũ chịu trách nhiệm kiểm thử và đảm bảo chất lượng tính năng.
*   **Quản lý dự án (Project Manager):** Chịu trách nhiệm giám sát tiến độ và phạm vi dự án.

## 4. YÊU CẦU NGHIỆP VỤ CHI TIẾT

### 4.1. Danh sách Yêu cầu Chức năng (Functional Requirements - FR)

Dựa trên các yêu cầu chức năng đã cung cấp, tôi sẽ chi tiết hóa thành các User Story và Acceptance Criteria.

| Mã Yêu cầu | Tên Yêu cầu (BRD) | User Story (Kịch bản Người dùng) |
| :--------- | :---------------- | :------------------------------- |
| `QUAN-20260530-2301-FR01` | Hiển thị danh sách tất cả các học sinh. | **Với vai trò là Người quản trị,** tôi muốn xem danh sách tất cả các học sinh để có cái nhìn tổng quan về dữ liệu. |
| `QUAN-20260530-2301-FR02` | Xem chi tiết thông tin của một học sinh. | **Với vai trò là Người quản trị,** tôi muốn xem chi tiết thông tin của một học sinh cụ thể để nắm bắt đầy đủ hồ sơ của học sinh đó. |
| `QUAN-20260530-2301-FR03` | Thêm mới thông tin một học sinh. | **Với vai trò là Người quản trị,** tôi muốn thêm mới thông tin một học sinh vào hệ thống để ghi nhận học sinh mới. |
| `QUAN-20260530-2301-FR04` | Chỉnh sửa thông tin của một học sinh hiện có. | **Với vai trò là Người quản trị,** tôi muốn chỉnh sửa thông tin của một học sinh hiện có để cập nhật dữ liệu khi có thay đổi. |
| `QUAN-20260530-2301-FR05` | Xóa một học sinh khỏi hệ thống. | **Với vai trò là Người quản trị,** tôi muốn xóa một học sinh khỏi hệ thống khi học sinh đó không còn liên quan hoặc dữ liệu bị sai. |
| `QUAN-20260530-2301-FR06` | Ngăn chặn việc xóa học sinh nếu học sinh đó có dữ liệu liên quan. | **Với vai trò là Người quản trị,** tôi muốn hệ thống ngăn chặn việc xóa học sinh nếu có dữ liệu liên quan để đảm bảo tính toàn vẹn của dữ liệu. |
| `QUAN-20260530-2301-FR07` | Tìm kiếm và lọc học sinh. | **Với vai trò là Người quản trị,** tôi muốn tìm kiếm học sinh theo Mã Học sinh hoặc Họ và Tên, lọc theo Lớp Học và Trạng Thái để dễ dàng tìm thấy học sinh mong muốn. |
| `QUAN-20260530-2301-FR08` | Hỗ trợ phân trang cho danh sách học sinh. | **Với vai trò là Người quản trị,** tôi muốn danh sách học sinh được phân trang để dễ dàng xem và duyệt qua số lượng lớn học sinh. |

---

### 4.2. Mô tả chi tiết Yêu cầu Chức năng (Functional Requirements - FR) và Tiêu chí Chấp nhận (Acceptance Criteria)

#### `QUAN-20260530-2301-FR01`: Hiển thị danh sách tất cả các học sinh.
*   **Mô tả:** Hệ thống phải hiển thị một bảng chứa danh sách tất cả các học sinh hiện có trong cơ sở dữ liệu.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị truy cập trang "Quản lý Học sinh".
    *   **WHEN** Trang được tải thành công.
    *   **THEN** Hệ thống hiển thị một bảng với các cột sau cho mỗi học sinh: Mã Học sinh, Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái.
    *   **AND** Dữ liệu được sắp xếp mặc định theo "Mã Học sinh" tăng dần (hoặc theo một tiêu chí mặc định khác sẽ được thỏa thuận).
    *   **AND** Mỗi hàng trong bảng hiển thị dữ liệu của một học sinh.
    *   **AND** Có các nút/biểu tượng hành động (ví dụ: Xem, Sửa, Xóa) cho mỗi học sinh.

#### `QUAN-20260530-2301-FR02`: Xem chi tiết thông tin của một học sinh.
*   **Mô tả:** Người quản trị có thể xem toàn bộ thông tin chi tiết của một học sinh cụ thể trong một giao diện riêng (modal hoặc trang mới).
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Xem" (hoặc nhấp vào hàng của học sinh) cho một học sinh bất kỳ.
    *   **THEN** Hệ thống hiển thị một cửa sổ/trang chi tiết chứa tất cả thông tin của học sinh đó ở chế độ chỉ đọc.
    *   **AND** Các trường thông tin bao gồm: Mã Học sinh, Họ và Tên, Ngày Sinh, Giới Tính, Địa Chỉ, Số Điện Thoại Phụ Huynh, Email Phụ Huynh, Lớp Học, Trạng Thái, (có thể thêm ngày tạo, người tạo, ngày sửa, người sửa).
    *   **AND** Có nút "Đóng" hoặc "Quay lại" để trở về trang danh sách.

#### `QUAN-20260530-2301-FR03`: Thêm mới thông tin một học sinh.
*   **Mô tả:** Người quản trị có thể nhập thông tin để tạo một hồ sơ học sinh mới trong hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Thêm mới học sinh".
    *   **THEN** Hệ thống hiển thị một biểu mẫu nhập liệu (có thể là modal) với các trường trống.
    *   **AND** Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" được đánh dấu là bắt buộc nhập (theo `QUAN-20260530-2301-BR02`).
    *   **AND** Sau khi người quản trị nhập đầy đủ thông tin hợp lệ và nhấp "Lưu".
    *   **THEN** Hệ thống tạo một học sinh mới với `Mã Học sinh` được tự động tạo (hoặc do người dùng nhập và kiểm tra tính duy nhất theo `QUAN-20260530-2301-BR01`).
    *   **AND** Hệ thống hiển thị thông báo thành công "Thêm học sinh thành công".
    *   **AND** Học sinh mới được thêm xuất hiện trong danh sách.
    *   **WHEN** Người quản trị nhập thông tin không hợp lệ (ví dụ: trường bắt buộc bị bỏ trống, mã học sinh trùng lặp, ngày sinh không hợp lệ).
    *   **THEN** Hệ thống hiển thị thông báo lỗi tương ứng và không cho phép lưu (theo `QUAN-20260530-2301-BR01`, `QUAN-20260530-2301-BR02`, `QUAN-20260530-2301-BR03`, `QUAN-20260530-2301-BR04`, `QUAN-20260530-2301-BR05`, `QUAN-20260530-2301-BR06`).

#### `QUAN-20260530-2301-FR04`: Chỉnh sửa thông tin của một học sinh hiện có.
*   **Mô tả:** Người quản trị có thể cập nhật các thông tin của một học sinh đã có trong hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Sửa" cho một học sinh bất kỳ.
    *   **THEN** Hệ thống hiển thị một biểu mẫu nhập liệu (có thể là modal) đã điền sẵn thông tin hiện tại của học sinh đó.
    *   **AND** Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" được đánh dấu là bắt buộc nhập (theo `QUAN-20260530-2301-BR02`).
    *   **AND** Sau khi người quản trị chỉnh sửa thông tin hợp lệ và nhấp "Lưu".
    *   **THEN** Hệ thống cập nhật thông tin của học sinh.
    *   **AND** Hệ thống hiển thị thông báo thành công "Cập nhật học sinh thành công".
    *   **AND** Thay đổi được phản ánh ngay lập tức trong danh sách.
    *   **WHEN** Người quản trị chỉnh sửa thông tin không hợp lệ (ví dụ: trường bắt buộc bị bỏ trống, mã học sinh trùng lặp nếu cho phép sửa mã, ngày sinh không hợp lệ).
    *   **THEN** Hệ thống hiển thị thông báo lỗi tương ứng và không cho phép lưu (theo `QUAN-20260530-2301-BR01`, `QUAN-20260530-2301-BR02`, `QUAN-20260530-2301-BR03`, `QUAN-20260530-2301-BR04`, `QUAN-20260530-2301-BR05`, `QUAN-20260530-2301-BR06`).

#### `QUAN-20260530-2301-FR05`: Xóa một học sinh khỏi hệ thống.
*   **Mô tả:** Người quản trị có thể loại bỏ một hồ sơ học sinh khỏi hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Xóa" cho một học sinh.
    *   **THEN** Hệ thống hiển thị một cửa sổ xác nhận "Bạn có chắc chắn muốn xóa học sinh [Tên học sinh]?" với các lựa chọn "Xác nhận" và "Hủy".
    *   **WHEN** Người quản trị nhấp "Xác nhận" (và học sinh không có dữ liệu liên quan theo `QUAN-20260530-2301-FR06`).
    *   **THEN** Hệ thống xóa học sinh khỏi cơ sở dữ liệu.
    *   **AND** Hệ thống hiển thị thông báo thành công "Xóa học sinh thành công".
    *   **AND** Học sinh đó không còn xuất hiện trong danh sách.

#### `QUAN-20260530-2301-FR06`: Hệ thống phải ngăn chặn việc xóa học sinh nếu học sinh đó có dữ liệu liên quan.
*   **Mô tả:** Để duy trì tính toàn vẹn dữ liệu, không thể xóa một học sinh nếu có các bản ghi khác trong hệ thống liên kết với học sinh đó (ví dụ: điểm số, danh sách tham gia lớp học, thông tin đóng học phí, v.v.).
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh và đã chọn xóa một học sinh có dữ liệu liên quan (ví dụ: học sinh này đã có điểm số).
    *   **WHEN** Người quản trị nhấp "Xác nhận" trong hộp thoại xóa.
    *   **THEN** Hệ thống hiển thị thông báo lỗi "Không thể xóa học sinh này vì có dữ liệu liên quan. Vui lòng xóa các dữ liệu liên quan trước hoặc chuyển trạng thái học sinh thành 'Đã chuyển trường'/'Tạm dừng' thay vì xóa."
    *   **AND** Học sinh đó vẫn còn trong danh sách và không bị xóa.

#### `QUAN-20260530-2301-FR07`: Người quản trị phải có khả năng tìm kiếm học sinh theo Mã Học sinh hoặc Họ và Tên, lọc theo Lớp Học và Trạng Thái.
*   **Mô tả:** Cung cấp chức năng tìm kiếm và lọc để người quản trị có thể nhanh chóng định vị các học sinh mong muốn.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhập một chuỗi vào ô tìm kiếm (dành cho "Mã Học sinh" hoặc "Họ và Tên") và/hoặc chọn giá trị từ các bộ lọc "Lớp Học" và "Trạng Thái".
    *   **THEN** Hệ thống hiển thị lại danh sách học sinh chỉ bao gồm những học sinh khớp với tiêu chí tìm kiếm và lọc.
    *   **AND** Tìm kiếm theo "Mã Học sinh" và "Họ và Tên" phải hỗ trợ tìm kiếm không phân biệt chữ hoa/thường và có thể là tìm kiếm gần đúng (chứa chuỗi con).
    *   **AND** Bộ lọc "Lớp Học" sẽ là một danh sách thả xuống chứa tất cả các lớp học hiện có.
    *   **AND** Bộ lọc "Trạng Thái" sẽ là một danh sách thả xuống chứa các giá trị được định nghĩa trước ("Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
    *   **AND** Kết quả tìm kiếm/lọc vẫn tuân thủ phân trang (`QUAN-20260530-2301-FR08`).
    *   **WHEN** Người quản trị xóa nội dung tìm kiếm hoặc đặt lại các bộ lọc.
    *   **THEN** Hệ thống hiển thị lại toàn bộ danh sách học sinh.

#### `QUAN-20260530-2301-FR08`: Hệ thống phải hỗ trợ phân trang cho danh sách học sinh.
*   **Mô tả:** Khi có số lượng lớn học sinh, hệ thống cần phân chia danh sách thành các trang để cải thiện hiệu suất và trải nghiệm người dùng.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh và có nhiều hơn số lượng học sinh tối đa trên một trang.
    *   **THEN** Hệ thống hiển thị danh sách học sinh theo từng trang.
    *   **AND** Có các điều khiển phân trang (ví dụ: "Trang đầu", "Trang trước", "Trang kế tiếp", "Trang cuối", số trang, số lượng học sinh trên mỗi trang).
    *   **AND** Mặc định, mỗi trang hiển thị 10 (hoặc 20, cần xác định rõ) học sinh.
    *   **AND** Người quản trị có thể thay đổi số lượng học sinh hiển thị trên mỗi trang (ví dụ: 10, 20, 50, 100).
    *   **AND** Khi người quản trị chuyển đổi trang, hệ thống tải dữ liệu cho trang mới.

---

### 4.3. Quy tắc Nghiệp vụ (Business Rules - BR)

Các quy tắc nghiệp vụ này định nghĩa các ràng buộc và logic cụ thể mà hệ thống phải tuân thủ.

| Mã Yêu cầu | Tên Quy tắc Nghiệp vụ (BRD) | Chi tiết triển khai | Thông báo lỗi (ví dụ) |
| :--------- | :-------------------------- | :------------------ | :-------------------- |
| `QUAN-20260530-2301-BR01` | Mã Học sinh phải là duy nhất trong toàn hệ thống. | Khi thêm mới hoặc chỉnh sửa học sinh, hệ thống phải kiểm tra xem `Mã Học sinh` đã tồn tại hay chưa. | "Mã Học sinh đã tồn tại trong hệ thống. Vui lòng nhập mã khác." |
| `QUAN-20260530-2301-BR02` | Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" là bắt buộc nhập. | Các trường này không được phép để trống khi thêm mới hoặc chỉnh sửa. | "[Tên trường] không được để trống." |
| `QUAN-20260530-2301-BR03` | Ngày Sinh phải là một ngày hợp lệ và không được lớn hơn hoặc bằng ngày hiện tại. | Khi nhập `Ngày Sinh`, hệ thống phải kiểm tra định dạng ngày hợp lệ và đảm bảo ngày đó trong quá khứ. | "Ngày Sinh không hợp lệ." hoặc "Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại." |
| `QUAN-20260530-2301-BR04` | Số Điện Thoại Phụ Huynh (nếu nhập) phải theo định dạng số điện thoại hợp lệ. | Nếu `Số Điện Thoại Phụ Huynh` được nhập, nó phải tuân thủ định dạng số điện thoại phổ biến của Việt Nam (ví dụ: bắt đầu bằng 0, có 10 chữ số). | "Số Điện Thoại Phụ Huynh không đúng định dạng." |
| `QUAN-20260530-2301-BR05` | Email Phụ Huynh (nếu nhập) phải theo định dạng email hợp lệ. | Nếu `Email Phụ Huynh` được nhập, nó phải tuân thủ định dạng email chuẩn (ví dụ: `abc@domain.com`). | "Email Phụ Huynh không đúng định dạng." |
| `QUAN-20260530-2301-BR06` | Trạng Thái của học sinh phải là một trong các giá trị được định nghĩa trước. | Trường `Trạng Thái` phải là một danh sách thả xuống (dropdown) với các giá trị cho phép: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng". | "Trạng Thái không hợp lệ. Vui lòng chọn một trong các giá trị cho phép." |

---

### 4.4. Yêu cầu Phi chức năng (Non-Functional Requirements - NFR)

Các yêu cầu này mô tả các tiêu chí về chất lượng và thuộc tính của hệ thống.

*   **Hiệu năng (Performance):**
    *   **Tải danh sách học sinh:** Hệ thống phải tải và hiển thị danh sách học sinh trong vòng **không quá 3 giây** kể từ khi yêu cầu được gửi cho 1000 bản ghi dữ liệu mẫu.
    *   **Thao tác CRUD (Create, Read Detail, Update, Delete):** Mỗi thao tác thêm mới, xem chi tiết, chỉnh sửa, hoặc xóa một học sinh phải hoàn thành trong vòng **không quá 2 giây**.

*   **Bảo mật (Security):**
    *   **Xác thực và Phân quyền:**
        *   Chỉ người dùng có vai trò "Admin" (Người quản trị) mới có quyền truy cập và thực hiện các thao tác CRUD (thêm, xem, sửa, xóa) đối với thông tin học sinh.
        *   Các yêu cầu truy cập từ người dùng không được xác thực hoặc không có quyền sẽ bị từ chối với mã lỗi tương ứng (ví dụ: 401 Unauthorized, 403 Forbidden).
    *   **Bảo vệ dữ liệu:** Dữ liệu học sinh nhạy cảm (ví dụ: ngày sinh, địa chỉ) phải được bảo vệ khỏi truy cập trái phép.

*   **Kiến trúc (Architecture):**
    *   Hệ thống phải được phát triển dựa trên kiến trúc **Clean Architecture** (Domain -> Application -> Infrastructure -> WebAPI).
    *   Sử dụng mô hình **CQRS (Command Query Responsibility Segregation)** kết hợp với thư viện **MediatR** để xử lý các yêu cầu.
    *   Áp dụng **Repository Pattern** cho việc truy cập dữ liệu.
    *   Sử dụng **FluentValidation** để thực hiện xác thực nghiệp vụ (Business Rules).
    *   Sử dụng **Result Pattern** để quản lý và trả về kết quả của các thao tác nghiệp vụ, bao gồm cả thành công và thất bại với thông báo lỗi rõ ràng.

## 5. MÔ HÌNH DỮ LIỆU SƠ BỘ

Để hỗ trợ tính năng này, cần có một thực thể `HocSinh` (Student) chính và có thể liên kết với `LopHoc` (Class).

**Thực thể: HocSinh**
*   `MaHocSinh` (string, PK, Unique, Required) - Mã định danh duy nhất cho học sinh.
*   `HoTen` (string, Required) - Họ và tên đầy đủ của học sinh.
*   `NgaySinh` (date, Required) - Ngày sinh của học sinh.
*   `GioiTinh` (string, Required) - Giới tính (ví dụ: "Nam", "Nữ", "Khác").
*   `DiaChi` (string, Nullable) - Địa chỉ hiện tại của học sinh.
*   `SoDienThoaiPH` (string, Nullable) - Số điện thoại của phụ huynh.
*   `EmailPH` (string, Nullable) - Email của phụ huynh.
*   `LopHocID` (Guid/int, FK to LopHoc, Required) - ID của lớp học mà học sinh đang theo học.
*   `TrangThai` (string, Required) - Trạng thái của học sinh (ví dụ: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
*   `NgayTao` (datetime) - Thời điểm tạo bản ghi.
*   `NguoiTao` (string) - Người tạo bản ghi.
*   `NgayCapNhat` (datetime, Nullable) - Thời điểm cập nhật bản ghi gần nhất.
*   `NguoiCapNhat` (string, Nullable) - Người cập nhật bản ghi gần nhất.

**Thực thể: LopHoc**
*   `LopHocID` (Guid/int, PK) - ID duy nhất của lớp học.
*   `TenLop` (string, Unique, Required) - Tên của lớp học (ví dụ: "10A1", "11B2").
*   ... (Các thuộc tính khác của lớp học)

**Mối quan hệ:**
*   `HocSinh` N:1 `LopHoc` (Nhiều học sinh thuộc về một lớp học).

## 6. GIAO DIỆN NGƯỜI DÙNG SƠ BỘ (UI/UX - Cấu trúc)

Để trực quan hóa các yêu cầu, dưới đây là mô tả sơ bộ về các màn hình chính:

### 6.1. Màn hình Danh sách Học sinh (`/students`)
*   **Header:** "Quản lý Học sinh"
*   **Nút "Thêm mới Học sinh":** Dẫn đến form thêm mới.
*   **Khu vực Tìm kiếm & Lọc:**
    *   **Ô nhập liệu tìm kiếm:** Cho phép nhập "Mã Học sinh" hoặc "Họ và Tên".
    *   **Dropdown "Lớp Học":** Danh sách các lớp học để lọc.
    *   **Dropdown "Trạng Thái":** Danh sách các trạng thái học sinh (Đang học, Đã tốt nghiệp...).
    *   **Nút "Tìm kiếm" / "Reset":** Thực hiện tìm kiếm hoặc đặt lại bộ lọc.
*   **Bảng hiển thị danh sách:**
    *   Các cột: STT, Mã Học sinh, Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái, Thao tác.
    *   Cột "Thao tác" bao gồm các nút/biểu tượng:
        *   **Xem chi tiết:** Mở modal/trang xem chi tiết.
        *   **Sửa:** Mở modal/trang chỉnh sửa.
        *   **Xóa:** Hiện pop-up xác nhận xóa.
*   **Khu vực phân trang:** Hiển thị thông tin tổng số bản ghi, số trang, các nút điều hướng trang.

### 6.2. Màn hình/Modal Thêm mới/Chỉnh sửa Học sinh (`/students/add`, `/students/edit/{id}`)
*   **Header:** "Thêm mới Học sinh" hoặc "Chỉnh sửa Học sinh"
*   **Form nhập liệu với các trường:**
    *   Mã Học sinh (chỉ đọc khi chỉnh sửa nếu Mã HS là tự động/không cho phép sửa)
    *   Họ và Tên (bắt buộc)
    *   Ngày Sinh (Date picker, bắt buộc)
    *   Giới Tính (Dropdown/Radio, bắt buộc)
    *   Địa Chỉ
    *   Số Điện Thoại Phụ Huynh
    *   Email Phụ Huynh
    *   Lớp Học (Dropdown, bắt buộc)
    *   Trạng Thái (Dropdown, bắt buộc)
*   **Các nút hành động:** "Lưu" và "Hủy".
*   **Thông báo lỗi:** Hiển thị rõ ràng dưới mỗi trường nhập liệu không hợp lệ hoặc thông báo lỗi chung ở đầu form.

### 6.3. Màn hình/Modal Xem chi tiết Học sinh (`/students/detail/{id}`)
*   **Header:** "Thông tin chi tiết Học sinh"
*   **Hiển thị tất cả các thông tin của học sinh ở chế độ chỉ đọc.**
*   **Nút "Đóng" hoặc "Quay lại".**

## 7. CÁC GIẢ ĐỊNH VÀ RỦI RO

*   **Giả định:**
    *   Hệ thống quản lý lớp học đã tồn tại và cung cấp danh sách lớp học cho tính năng này.
    *   Mã Học sinh có thể được tạo tự động bởi hệ thống hoặc nhập thủ công bởi Admin (cần xác nhận rõ ràng). *Trong tài liệu này tôi đang giả định có thể nhập hoặc là tự động sinh.*
    *   Người quản trị có đủ kiến thức nghiệp vụ để nhập thông tin học sinh chính xác.
*   **Rủi ro:**
    *   **Rủi ro Dữ liệu liên quan (FR06):** Nếu cấu trúc dữ liệu liên quan phức tạp hoặc không rõ ràng, việc triển khai `FR06` có thể mất thời gian và dễ phát sinh lỗi. Cần xác định rõ các thực thể nào liên quan trực tiếp đến `HocSinh` để ngăn chặn xóa.
    *   **Rủi ro Hiệu năng:** Với số lượng học sinh rất lớn (ví dụ: hàng trăm nghìn), hiệu năng tìm kiếm, lọc và tải danh sách có thể bị ảnh hưởng nếu không tối ưu database và các query.
    *   **Rủi ro Bảo mật:** Việc phân quyền `Admin` cần được triển khai cẩn thận để tránh lỗ hổng bảo mật.

---

Đây là tài liệu chi tiết hóa các yêu cầu của bạn, được trình bày một cách có cấu trúc và rõ ràng như một Business Analyst của ONENET sẽ làm. Nó cung cấp đủ thông tin cho các bên liên quan để hiểu, phát triển và kiểm thử tính năng "Quản lý Thông tin Học sinh".