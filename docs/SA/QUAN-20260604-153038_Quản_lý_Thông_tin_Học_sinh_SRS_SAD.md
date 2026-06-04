Tuyệt vời! Với vai trò là Solution Architect của ONENET, tôi đã nhận được tài liệu BRD chi tiết từ Business Analyst cho tính năng "Quản lý Thông tin Học sinh". Dựa trên các yêu cầu này, tôi sẽ xây dựng Tài liệu Đặc tả Yêu cầu Phần mềm (SRS) và Tài liệu Kiến trúc Phần mềm (SAD) để cung cấp hướng dẫn rõ ràng cho đội phát triển.

---

# TÀI LIỆU ĐẶC TẢ YÊU CẦU PHẦN MỀM (SRS)

## 1. THÔNG TIN CHUNG

*   **Tên tài liệu:** Tài liệu Đặc tả Yêu cầu Phần mềm - Tính năng Quản lý Thông tin Học sinh
*   **Mã tính năng:** `QUAN-20260604-153038`
*   **Tên tính năng:** Quản lý Thông tin Học sinh
*   **Dự án:** `quanlyhocsinh`
*   **Ngày:** 2026-06-04
*   **Phiên bản:** 1.0
*   **Người soạn:** Solution Architect - ONENET
*   **Mục đích tài liệu:** `QUAN-20260604-153038-SRS01` Tài liệu này cung cấp mô tả chi tiết về các yêu cầu chức năng, phi chức năng và quy tắc nghiệp vụ cho tính năng "Quản lý Thông tin Học sinh". Nó đóng vai trò là cơ sở để đội phát triển thiết kế, triển khai và kiểm thử phần mềm, đồng thời là tài liệu tham chiếu cho các bên liên quan.

## 2. PHẠM VI TÍNH NĂNG

*   `QUAN-20260604-153038-SRS02` Tính năng "Quản lý Thông tin Học sinh" cho phép người quản trị (Admin) thực hiện các thao tác quản lý cơ bản (CRUD - Create, Read, Update, Delete) đối với thông tin học sinh trong hệ thống `quanlyhocsinh`. Bao gồm việc xem danh sách, xem chi tiết, thêm mới, chỉnh sửa, xóa học sinh, cũng như tìm kiếm, lọc và phân trang dữ liệu.

## 3. CÁC BÊN LIÊN QUAN

*   `QUAN-20260604-153038-SRS03`
    *   **Người quản trị (Admin):** Đối tượng người dùng chính tương tác trực tiếp với tính năng này để quản lý dữ liệu học sinh.
    *   **Đội phát triển (Development Team):** Đội ngũ chịu trách nhiệm thiết kế và triển khai tính năng.
    *   **Đội kiểm thử (QA Team):** Đội ngũ chịu trách nhiệm kiểm thử và đảm bảo chất lượng tính năng.
    *   **Quản lý dự án (Project Manager):** Chịu trách nhiệm giám sát tiến độ và phạm vi dự án.

## 4. YÊU CẦU NGHIỆP VỤ CHI TIẾT

### 4.1. Danh sách Yêu cầu Chức năng (Functional Requirements - FR)

*   `QUAN-20260604-153038-SRS04` Dưới đây là danh sách các yêu cầu chức năng dưới dạng User Story:

| Mã Yêu cầu | Tên Yêu cầu (BRD) | User Story (Kịch bản Người dùng) |
| :--------- | :---------------- | :------------------------------- |
| `QUAN-20260604-153038-FR01` | Hiển thị danh sách tất cả các học sinh. | **Với vai trò là Người quản trị,** tôi muốn xem danh sách tất cả các học sinh để có cái nhìn tổng quan về dữ liệu. |
| `QUAN-20260604-153038-FR02` | Xem chi tiết thông tin của một học sinh. | **Với vai trò là Người quản trị,** tôi muốn xem chi tiết thông tin của một học sinh cụ thể để nắm bắt đầy đủ hồ sơ của học sinh đó. |
| `QUAN-20260604-153038-FR03` | Thêm mới thông tin một học sinh. | **Với vai trò là Người quản trị,** tôi muốn thêm mới thông tin một học sinh vào hệ thống để ghi nhận học sinh mới. |
| `QUAN-20260604-153038-FR04` | Chỉnh sửa thông tin của một học sinh hiện có. | **Với vai trò là Người quản trị,** tôi muốn chỉnh sửa thông tin của một học sinh hiện có để cập nhật dữ liệu khi có thay đổi. |
| `QUAN-20260604-153038-FR05` | Xóa một học sinh khỏi hệ thống. | **Với vai trò là Người quản trị,** tôi muốn xóa một học sinh khỏi hệ thống khi học sinh đó không còn liên quan hoặc dữ liệu bị sai. |
| `QUAN-20260604-153038-FR06` | Ngăn chặn việc xóa học sinh nếu học sinh đó có dữ liệu liên quan. | **Với vai trò là Người quản trị,** tôi muốn hệ thống ngăn chặn việc xóa học sinh nếu có dữ liệu liên quan để đảm bảo tính toàn vẹn của dữ liệu. |
| `QUAN-20260604-153038-FR07` | Tìm kiếm và lọc học sinh. | **Với vai trò là Người quản trị,** tôi muốn tìm kiếm học sinh theo Mã Học sinh hoặc Họ và Tên, lọc theo Lớp Học và Trạng Thái để dễ dàng tìm thấy học sinh mong muốn. |
| `QUAN-20260604-153038-FR08` | Hỗ trợ phân trang cho danh sách học sinh. | **Với vai trò là Người quản trị,** tôi muốn danh sách học sinh được phân trang để dễ dàng xem và duyệt qua số lượng lớn học sinh. |

### 4.2. Mô tả chi tiết Yêu cầu Chức năng (Functional Requirements - FR) và Tiêu chí Chấp nhận (Acceptance Criteria)

#### `QUAN-20260604-153038-SRS05`: `QUAN-20260604-153038-FR01`: Hiển thị danh sách tất cả các học sinh.
*   **Mô tả:** Hệ thống phải hiển thị một bảng chứa danh sách tất cả các học sinh hiện có trong cơ sở dữ liệu.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị truy cập trang "Quản lý Học sinh".
    *   **WHEN** Trang được tải thành công.
    *   **THEN** Hệ thống hiển thị một bảng với các cột sau cho mỗi học sinh: Mã Học sinh, Họ và Tên, Ngày Sinh, Giới Tính, Lớp Học, Trạng Thái.
    *   **AND** Dữ liệu được sắp xếp mặc định theo "Mã Học sinh" tăng dần.
    *   **AND** Mỗi hàng trong bảng hiển thị dữ liệu của một học sinh.
    *   **AND** Có các nút/biểu tượng hành động (ví dụ: Xem, Sửa, Xóa) cho mỗi học sinh.

#### `QUAN-20260604-153038-SRS06`: `QUAN-20260604-153038-FR02`: Xem chi tiết thông tin của một học sinh.
*   **Mô tả:** Người quản trị có thể xem toàn bộ thông tin chi tiết của một học sinh cụ thể trong một giao diện riêng (modal hoặc trang mới).
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Xem" (hoặc nhấp vào hàng của học sinh) cho một học sinh bất kỳ.
    *   **THEN** Hệ thống hiển thị một cửa sổ/trang chi tiết chứa tất cả thông tin của học sinh đó ở chế độ chỉ đọc.
    *   **AND** Các trường thông tin bao gồm: Mã Học sinh, Họ và Tên, Ngày Sinh, Giới Tính, Địa Chỉ, Số Điện Thoại Phụ Huynh, Email Phụ Huynh, Lớp Học, Trạng Thái, Ngày Tạo, Người Tạo, Ngày Cập Nhật, Người Cập Nhật.
    *   **AND** Có nút "Đóng" hoặc "Quay lại" để trở về trang danh sách.

#### `QUAN-20260604-153038-SRS07`: `QUAN-20260604-153038-FR03`: Thêm mới thông tin một học sinh.
*   **Mô tả:** Người quản trị có thể nhập thông tin để tạo một hồ sơ học sinh mới trong hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Thêm mới học sinh".
    *   **THEN** Hệ thống hiển thị một biểu mẫu nhập liệu (có thể là modal) với các trường trống.
    *   **AND** Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" được đánh dấu là bắt buộc nhập (theo `QUAN-20260604-153038-BR02`).
    *   **AND** Sau khi người quản trị nhập đầy đủ thông tin hợp lệ và nhấp "Lưu".
    *   **THEN** Hệ thống tạo một học sinh mới với `Mã Học sinh` được tự động tạo (hoặc do người dùng nhập và kiểm tra tính duy nhất theo `QUAN-20260604-153038-BR01`).
    *   **AND** Hệ thống hiển thị thông báo thành công "Thêm học sinh thành công".
    *   **AND** Học sinh mới được thêm xuất hiện trong danh sách.
    *   **WHEN** Người quản trị nhập thông tin không hợp lệ (ví dụ: trường bắt buộc bị bỏ trống, mã học sinh trùng lặp, ngày sinh không hợp lệ).
    *   **THEN** Hệ thống hiển thị thông báo lỗi tương ứng và không cho phép lưu (theo `QUAN-20260604-153038-BR01` đến `QUAN-20260604-153038-BR06`).

#### `QUAN-20260604-153038-SRS08`: `QUAN-20260604-153038-FR04`: Chỉnh sửa thông tin của một học sinh hiện có.
*   **Mô tả:** Người quản trị có thể cập nhật các thông tin của một học sinh đã có trong hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Sửa" cho một học sinh bất kỳ.
    *   **THEN** Hệ thống hiển thị một biểu mẫu nhập liệu (có thể là modal) đã điền sẵn thông tin hiện tại của học sinh đó.
    *   **AND** Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" được đánh dấu là bắt buộc nhập (theo `QUAN-20260604-153038-BR02`).
    *   **AND** Sau khi người quản trị chỉnh sửa thông tin hợp lệ và nhấp "Lưu".
    *   **THEN** Hệ thống cập nhật thông tin của học sinh.
    *   **AND** Hệ thống hiển thị thông báo thành công "Cập nhật học sinh thành công".
    *   **AND** Thay đổi được phản ánh ngay lập tức trong danh sách.
    *   **WHEN** Người quản trị chỉnh sửa thông tin không hợp lệ (ví dụ: trường bắt buộc bị bỏ trống, mã học sinh trùng lặp nếu cho phép sửa mã, ngày sinh không hợp lệ).
    *   **THEN** Hệ thống hiển thị thông báo lỗi tương ứng và không cho phép lưu (theo `QUAN-20260604-153038-BR01` đến `QUAN-20260604-153038-BR06`).

#### `QUAN-20260604-153038-SRS09`: `QUAN-20260604-153038-FR05`: Xóa một học sinh khỏi hệ thống.
*   **Mô tả:** Người quản trị có thể loại bỏ một hồ sơ học sinh khỏi hệ thống.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhấp vào nút "Xóa" cho một học sinh.
    *   **THEN** Hệ thống hiển thị một cửa sổ xác nhận "Bạn có chắc chắn muốn xóa học sinh [Tên học sinh]?" với các lựa chọn "Xác nhận" và "Hủy".
    *   **WHEN** Người quản trị nhấp "Xác nhận" (và học sinh không có dữ liệu liên quan theo `QUAN-20260604-153038-FR06`).
    *   **THEN** Hệ thống xóa học sinh khỏi cơ sở dữ liệu.
    *   **AND** Hệ thống hiển thị thông báo thành công "Xóa học sinh thành công".
    *   **AND** Học sinh đó không còn xuất hiện trong danh sách.

#### `QUAN-20260604-153038-SRS10`: `QUAN-20260604-153038-FR06`: Hệ thống phải ngăn chặn việc xóa học sinh nếu học sinh đó có dữ liệu liên quan.
*   **Mô tả:** Để duy trì tính toàn vẹn dữ liệu, không thể xóa một học sinh nếu có các bản ghi khác trong hệ thống liên kết với học sinh đó (ví dụ: điểm số, danh sách tham gia lớp học, thông tin đóng học phí, v.v.).
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh và đã chọn xóa một học sinh có dữ liệu liên quan (ví dụ: học sinh này đã có điểm số).
    *   **WHEN** Người quản trị nhấp "Xác nhận" trong hộp thoại xóa.
    *   **THEN** Hệ thống hiển thị thông báo lỗi "Không thể xóa học sinh này vì có dữ liệu liên quan. Vui lòng xóa các dữ liệu liên quan trước hoặc chuyển trạng thái học sinh thành 'Đã chuyển trường'/'Tạm dừng' thay vì xóa."
    *   **AND** Học sinh đó vẫn còn trong danh sách và không bị xóa.

#### `QUAN-20260604-153038-SRS11`: `QUAN-20260604-153038-FR07`: Người quản trị phải có khả năng tìm kiếm học sinh theo Mã Học sinh hoặc Họ và Tên, lọc theo Lớp Học và Trạng Thái.
*   **Mô tả:** Cung cấp chức năng tìm kiếm và lọc để người quản trị có thể nhanh chóng định vị các học sinh mong muốn.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh.
    *   **WHEN** Người quản trị nhập một chuỗi vào ô tìm kiếm (dành cho "Mã Học sinh" hoặc "Họ và Tên") và/hoặc chọn giá trị từ các bộ lọc "Lớp Học" và "Trạng Thái".
    *   **THEN** Hệ thống hiển thị lại danh sách học sinh chỉ bao gồm những học sinh khớp với tiêu chí tìm kiếm và lọc.
    *   **AND** Tìm kiếm theo "Mã Học sinh" và "Họ và Tên" phải hỗ trợ tìm kiếm không phân biệt chữ hoa/thường và có thể là tìm kiếm gần đúng (chứa chuỗi con).
    *   **AND** Bộ lọc "Lớp Học" sẽ là một danh sách thả xuống chứa tất cả các lớp học hiện có.
    *   **AND** Bộ lọc "Trạng Thái" sẽ là một danh sách thả xuống chứa các giá trị được định nghĩa trước ("Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
    *   **AND** Kết quả tìm kiếm/lọc vẫn tuân thủ phân trang (`QUAN-20260604-153038-FR08`).
    *   **WHEN** Người quản trị xóa nội dung tìm kiếm hoặc đặt lại các bộ lọc.
    *   **THEN** Hệ thống hiển thị lại toàn bộ danh sách học sinh.

#### `QUAN-20260604-153038-SRS12`: `QUAN-20260604-153038-FR08`: Hệ thống phải hỗ trợ phân trang cho danh sách học sinh.
*   **Mô tả:** Khi có số lượng lớn học sinh, hệ thống cần phân chia danh sách thành các trang để cải thiện hiệu suất và trải nghiệm người dùng.
*   **Tiêu chí chấp nhận:**
    *   **GIVEN** Người quản trị đang ở trang danh sách học sinh và có nhiều hơn số lượng học sinh tối đa trên một trang.
    *   **THEN** Hệ thống hiển thị danh sách học sinh theo từng trang.
    *   **AND** Có các điều khiển phân trang (ví dụ: "Trang đầu", "Trang trước", "Trang kế tiếp", "Trang cuối", số trang, số lượng học sinh trên mỗi trang).
    *   **AND** Mặc định, mỗi trang hiển thị 10 học sinh.
    *   **AND** Người quản trị có thể thay đổi số lượng học sinh hiển thị trên mỗi trang (ví dụ: 10, 20, 50, 100).
    *   **AND** Khi người quản trị chuyển đổi trang, hệ thống tải dữ liệu cho trang mới.

### 4.3. Quy tắc Nghiệp vụ (Business Rules - BR)

*   `QUAN-20260604-153038-SRS13` Các quy tắc nghiệp vụ này định nghĩa các ràng buộc và logic cụ thể mà hệ thống phải tuân thủ.

| Mã Yêu cầu | Tên Quy tắc Nghiệp vụ (BRD) | Chi tiết triển khai | Thông báo lỗi (ví dụ) |
| :--------- | :-------------------------- | :------------------ | :-------------------- |
| `QUAN-20260604-153038-BR01` | Mã Học sinh phải là duy nhất trong toàn hệ thống. | Khi thêm mới hoặc chỉnh sửa học sinh, hệ thống phải kiểm tra xem `Mã Học sinh` đã tồn tại hay chưa. | "Mã Học sinh đã tồn tại trong hệ thống. Vui lòng nhập mã khác." |
| `QUAN-20260604-153038-BR02` | Các trường "Họ và Tên", "Ngày Sinh", "Giới Tính", "Lớp Học", "Trạng Thái" là bắt buộc nhập. | Các trường này không được phép để trống khi thêm mới hoặc chỉnh sửa. | "[Tên trường] không được để trống." |
| `QUAN-20260604-153038-BR03` | Ngày Sinh phải là một ngày hợp lệ và không được lớn hơn hoặc bằng ngày hiện tại. | Khi nhập `Ngày Sinh`, hệ thống phải kiểm tra định dạng ngày hợp lệ và đảm bảo ngày đó trong quá khứ. | "Ngày Sinh không hợp lệ." hoặc "Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại." |
| `QUAN-20260604-153038-BR04` | Số Điện Thoại Phụ Huynh (nếu nhập) phải theo định dạng số điện thoại hợp lệ. | Nếu `Số Điện Thoại Phụ Huynh` được nhập, nó phải tuân thủ định dạng số điện thoại phổ biến của Việt Nam (ví dụ: bắt đầu bằng 0, có 10 chữ số). | "Số Điện Thoại Phụ Huynh không đúng định dạng." |
| `QUAN-20260604-153038-BR05` | Email Phụ Huynh (nếu nhập) phải theo định dạng email hợp lệ. | Nếu `Email Phụ Huynh` được nhập, nó phải tuân thủ định dạng email chuẩn (ví dụ: `abc@domain.com`). | "Email Phụ Huynh không đúng định dạng." |
| `QUAN-20260604-153038-BR06` | Trạng Thái của học sinh phải là một trong các giá trị được định nghĩa trước. | Trường `Trạng Thái` phải là một danh sách thả xuống (dropdown) với các giá trị cho phép: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng". | "Trạng Thái không hợp lệ. Vui lòng chọn một trong các giá trị cho phép." |

### 4.4. Yêu cầu Phi chức năng (Non-Functional Requirements - NFR)

*   `QUAN-20260604-153038-SRS14` Các yêu cầu này mô tả các tiêu chí về chất lượng và thuộc tính của hệ thống.

*   **Hiệu năng (Performance):**
    *   **Tải danh sách học sinh:** Hệ thống phải tải và hiển thị danh sách học sinh trong vòng **không quá 3 giây** kể từ khi yêu cầu được gửi cho 1000 bản ghi dữ liệu mẫu.
    *   **Thao tác CRUD (Create, Read Detail, Update, Delete):** Mỗi thao tác thêm mới, xem chi tiết, chỉnh sửa, hoặc xóa một học sinh phải hoàn thành trong vòng **không quá 2 giây**.

*   **Bảo mật (Security):**
    *   **Xác thực và Phân quyền:**
        *   Chỉ người dùng có vai trò "Admin" (Người quản trị) mới có quyền truy cập và thực hiện các thao tác CRUD (thêm, xem, sửa, xóa) đối với thông tin học sinh.
        *   Các yêu cầu truy cập từ người dùng không được xác thực hoặc không có quyền sẽ bị từ chối với mã lỗi tương ứng (ví dụ: 401 Unauthorized, 403 Forbidden).
    *   **Bảo vệ dữ liệu:** Dữ liệu học sinh nhạy cảm (ví dụ: ngày sinh, địa chỉ) phải được bảo vệ khỏi truy cập trái phép bằng cách sử dụng các biện pháp mã hóa và kiểm soát truy cập thích hợp.

*   **Kiến trúc (Architecture):**
    *   Hệ thống phải được phát triển dựa trên kiến trúc **Clean Architecture** (Presentation -> Application -> Domain -> Infrastructure).
    *   Sử dụng mô hình **CQRS (Command Query Responsibility Segregation)** kết hợp với thư viện **MediatR** để xử lý các yêu cầu.
    *   Áp dụng **Repository Pattern** cho việc truy cập dữ liệu.
    *   Sử dụng **FluentValidation** để thực hiện xác thực nghiệp vụ (Business Rules).
    *   Sử dụng **Result Pattern** để quản lý và trả về kết quả của các thao tác nghiệp vụ, bao gồm cả thành công và thất bại với thông báo lỗi rõ ràng.

## 5. MÔ HÌNH DỮ LIỆU SƠ BỘ

*   `QUAN-20260604-153038-SRS15` Để hỗ trợ tính năng này, cần có một thực thể `HocSinh` (Student) chính và liên kết với `LopHoc` (Class).

**Thực thể: HocSinh**
*   `MaHocSinh` (string, PK, Unique, Required, max length 50) - Mã định danh duy nhất cho học sinh.
*   `HoTen` (string, Required, max length 255) - Họ và tên đầy đủ của học sinh.
*   `NgaySinh` (date, Required) - Ngày sinh của học sinh.
*   `GioiTinh` (string, Required, max length 10) - Giới tính (ví dụ: "Nam", "Nữ", "Khác").
*   `DiaChi` (string, Nullable, max length 500) - Địa chỉ hiện tại của học sinh.
*   `SoDienThoaiPH` (string, Nullable, max length 20) - Số điện thoại của phụ huynh.
*   `EmailPH` (string, Nullable, max length 255) - Email của phụ huynh.
*   `LopHocID` (Guid/int, FK to LopHoc, Required) - ID của lớp học mà học sinh đang theo học.
*   `TrangThai` (string, Required, max length 50) - Trạng thái của học sinh (ví dụ: "Đang học", "Đã tốt nghiệp", "Đã chuyển trường", "Tạm dừng").
*   `NgayTao` (datetime, Required) - Thời điểm tạo bản ghi (UTC).
*   `NguoiTao` (string, Required, max length 100) - Người tạo bản ghi.
*   `NgayCapNhat` (datetime, Nullable) - Thời điểm cập nhật bản ghi gần nhất (UTC).
*   `NguoiCapNhat` (string, Nullable, max length 100) - Người cập nhật bản ghi gần nhất.

**Thực thể: LopHoc**
*   `LopHocID` (Guid/int, PK) - ID duy nhất của lớp học.
*   `TenLop` (string, Unique, Required, max length 100) - Tên của lớp học (ví dụ: "10A1", "11B2").
*   `Khoi` (string, Required, max length 50) - Khối lớp (ví dụ: "10", "11", "12").
*   `NamHoc` (int, Required) - Năm học hiện tại của lớp.
*   ... (Các thuộc tính khác của lớp học)

**Mối quan hệ:**
*   `HocSinh` N:1 `LopHoc` (Nhiều học sinh thuộc về một lớp học).
*   `ON DELETE RESTRICT` cho mối quan hệ `HocSinh.LopHocID` với `LopHoc.LopHocID` để đảm bảo không xóa được lớp học nếu còn học sinh.

## 6. GIAO DIỆN NGƯỜI DÙNG SƠ BỘ (UI/UX - Cấu trúc)

*   `QUAN-20260604-153038-SRS16` Để trực quan hóa các yêu cầu, dưới đây là mô tả sơ bộ về các màn hình chính:

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
    *   Mã Học sinh (chỉ đọc khi chỉnh sửa nếu Mã HS là tự động/không cho phép sửa, nếu cho phép sửa cần kiểm tra duy nhất)
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

*   `QUAN-20260604-153038-SRS17`
*   **Giả định:**
    *   Hệ thống quản lý lớp học đã tồn tại và cung cấp danh sách lớp học thông qua API hoặc dịch vụ nội bộ để tích hợp.
    *   Mã Học sinh sẽ được hệ thống tạo tự động khi thêm mới và không thể chỉnh sửa sau khi tạo. (Quyết định cụ thể hơn so với BRD ban đầu để đơn giản hóa).
    *   Người quản trị có đủ kiến thức nghiệp vụ để nhập thông tin học sinh chính xác.
    *   Sẽ có một dịch vụ xác thực và phân quyền tập trung được sử dụng cho toàn bộ dự án `quanlyhocsinh`.
*   **Rủi ro:**
    *   **Rủi ro Dữ liệu liên quan (FR06):** Việc xác định và kiểm tra tất cả các mối quan hệ dữ liệu liên quan đến `HocSinh` có thể phức tạp. Cần có sự phối hợp chặt chẽ với Database Architect và các đội phụ trách các tính năng khác để định nghĩa rõ ràng các ràng buộc. Đề xuất sử dụng Foreign Key Constraints tại mức Database để đảm bảo tính toàn vẹn.
    *   **Rủi ro Hiệu năng:** Với số lượng học sinh rất lớn (ví dụ: hàng trăm nghìn trở lên), hiệu năng tìm kiếm, lọc và tải danh sách có thể bị ảnh hưởng nếu không tối ưu database và các query. Cần đánh giá kỹ lưỡng các chỉ mục (indexes) trên các cột thường xuyên được tìm kiếm/lọc (`MaHocSinh`, `HoTen`, `LopHocID`, `TrangThai`).
    *   **Rủi ro Bảo mật:** Việc phân quyền `Admin` cần được triển khai cẩn thận và kiểm thử nghiêm ngặt để tránh lỗ hổng bảo mật. Cần đảm bảo rằng chỉ các API được ủy quyền mới có thể thực hiện các thao tác quản lý.

---

# TÀI LIỆU KIẾN TRÚC PHẦN MỀM (SAD)

## 1. GIỚI THIỆU

*   **Tên tài liệu:** Tài liệu Kiến trúc Phần mềm - Tính năng Quản lý Thông tin Học sinh
*   **Mã tính năng:** `QUAN-20260604-153038`
*   **Tên tính năng:** Quản lý Thông tin Học sinh
*   **Dự án:** `quanlyhocsinh`
*   **Ngày:** 2026-06-04
*   **Phiên bản:** 1.0
*   **Người soạn:** Solution Architect - ONENET
*   **Mục đích tài liệu:** `QUAN-20260604-153038-SAD01` Tài liệu này mô tả kiến trúc tổng thể của tính năng "Quản lý Thông tin Học sinh" trong hệ thống `quanlyhocsinh`, dựa trên các yêu cầu chức năng và phi chức năng đã được đặc tả trong SRS. Nó cung cấp các quyết định kiến trúc quan trọng, các mẫu thiết kế, và các công nghệ được sử dụng để hướng dẫn đội phát triển.

## 2. MỤC TIÊU VÀ RÀNG BUỘC KIẾN TRÚC

*   `QUAN-20260604-153038-SAD02`
*   **Mục tiêu:**
    *   Đảm bảo hiệu năng cao cho các thao tác CRUD và tìm kiếm/lọc (đáp ứng NFR hiệu năng).
    *   Đảm bảo tính bảo mật và phân quyền chặt chẽ (đáp ứng NFR bảo mật).
    *   Đảm bảo khả năng mở rộng và bảo trì dễ dàng thông qua kiến trúc rõ ràng.
    *   Đảm bảo tính toàn vẹn dữ liệu.
*   **Ràng buộc:**
    *   Sử dụng .NET Core / C# cho Backend.
    *   Tuân thủ kiến trúc Clean Architecture, CQRS, Repository Pattern, FluentValidation, Result Pattern như đã định nghĩa trong NFR.
    *   Tích hợp với hệ thống xác thực và phân quyền hiện có của ONENET (nếu có, giả định là một dịch vụ OIDC/JWT).
    *   Ngân sách và thời gian phát triển.

## 3. BỐI CẢNH HỆ THỐNG CẤP CAO (System Context)

*   `QUAN-20260604-153038-SAD03`
    Tính năng "Quản lý Thông tin Học sinh" là một phần của hệ thống `quanlyhocsinh`. Nó sẽ tương tác với người dùng (Admin) thông qua giao diện web và có thể cần tích hợp với các module khác trong tương lai (ví dụ: quản lý điểm, quản lý thu chi, quản lý lớp học).

    ```
    +-------------------+    +-----------------------------+
    |   Admin User      |<-->|  Web Browser / Frontend UI  |
    +-------------------+    +-----------------------------+
                                      ^
                                      | HTTP/HTTPS (REST API)
                                      v
    +------------------------------------------------------+
    |          quanlyhocsinh System (Backend Services)     |
    |                                                      |
    |  +------------------------------------------------+  |
    |  |       Student Management API (This Feature)    |  |
    |  |  (QUAN-20260604-153038)                        |  |
    |  |  - Student Controller                          |  |
    |  |  - CQRS handlers for Student CRUD & Query      |  |
    |  |  - Student Domain Logic & Entities             |  |
    |  +------------------------------------------------+  |
    |          ^                                  ^        |
    |          | Database Interactions            |        |
    |          v                                  v        |
    |  +-----------------+                +------------------+
    |  |  Authentication |<-------------->|  Class Management|
    |  |  & Authorization|                |  Service (External)|
    |  |  Service        |                +------------------+
    |  +-----------------+                ^
    |                                     | Query Class Data
    |                                     v
    |  +-------------------+
    |  |     Database      |
    |  |  (Student, Class) |
    |  +-------------------+
    +------------------------------------------------------+
    ```

## 4. CÁC MẪU VÀ PHONG CÁCH KIẾN TRÚC

*   `QUAN-20260604-153038-SAD04`
    *   **Kiến trúc Clean Architecture:** Hệ thống sẽ được tổ chức thành các lớp (layers) rõ ràng:
        *   **Domain Layer:** Chứa các thực thể (Entities), giá trị đối tượng (Value Objects), các quy tắc nghiệp vụ cốt lõi (Domain Services, Specifications). Đây là lớp trung tâm, độc lập với các công nghệ bên ngoài.
        *   **Application Layer:** Chứa các trường hợp sử dụng (Use Cases), định nghĩa các Command và Query, các Handlers cho Command/Query, và các validator nghiệp vụ. Đây là nơi điều phối các thao tác nghiệp vụ.
        *   **Infrastructure Layer:** Chứa các triển khai cụ thể cho việc truy cập dữ liệu (Repository Implementations, ORM), giao tiếp với các dịch vụ bên ngoài, và các dịch vụ kỹ thuật khác (logging, caching).
        *   **Presentation Layer (WebAPI/UI):** Điểm vào của ứng dụng, chịu trách nhiệm xử lý các yêu cầu HTTP, ánh xạ từ DTO sang Command/Query, và trả về kết quả.

    *   **CQRS (Command Query Responsibility Segregation):**
        *   Các thao tác ghi (ghi, sửa, xóa) sẽ được xử lý bằng các `Command` (ví dụ: `CreateStudentCommand`, `UpdateStudentCommand`, `DeleteStudentCommand`).
        *   Các thao tác đọc (xem danh sách, xem chi tiết) sẽ được xử lý bằng các `Query` (ví dụ: `GetStudentsQuery`, `GetStudentByIdQuery`).
        *   Sử dụng thư viện **MediatR** để triển khai mô hình này, giúp tách biệt các yêu cầu và giảm sự phụ thuộc giữa các thành phần.

    *   **Repository Pattern:**
        *   Định nghĩa các interface `IRepository<T>` trong Domain/Application Layer để trừu tượng hóa việc truy cập dữ liệu.
        *   Triển khai cụ thể các repository trong Infrastructure Layer, sử dụng Entity Framework Core.
        *   Mục đích: Tách biệt logic nghiệp vụ khỏi chi tiết lưu trữ dữ liệu.

    *   **FluentValidation:**
        *   Sử dụng thư viện này để thực hiện xác thực các `Command` và `Query` ngay tại Application Layer trước khi xử lý nghiệp vụ, đảm bảo dữ liệu đầu vào hợp lệ theo các quy tắc nghiệp vụ (Business Rules).

    *   **Result Pattern:**
        *   Các `Command Handler` sẽ trả về một đối tượng `Result<T>` (ví dụ: `Result<StudentDto>`) thay vì trực tiếp trả về dữ liệu hoặc ném ngoại lệ.
        *   Đối tượng `Result` sẽ chứa thông tin về thành công/thất bại của thao tác, dữ liệu kết quả (nếu thành công), và danh sách lỗi (nếu thất bại). Điều này giúp chuẩn hóa việc xử lý lỗi và phản hồi.

## 5. THÀNH PHẦN KIẾN TRÚC (Component View)

*   `QUAN-20260604-153038-SAD05`

    **5.1. Presentation Layer (Frontend / Web API)**
    *   **Frontend UI:** (React/Angular/Vue.js)
        *   Tương tác với người dùng, hiển thị dữ liệu, gửi yêu cầu tới Backend API.
        *   Sử dụng Axios hoặc Fetch API để giao tiếp.
        *   Đảm bảo tuân thủ thiết kế UI/UX sơ bộ đã nêu trong SRS.
    *   **Web API:** (ASP.NET Core Web API)
        *   Điểm tiếp nhận các yêu cầu HTTP từ Frontend.
        *   Các `Controller` sẽ nhận DTO từ HTTP Request, ánh xạ chúng thành `Command` hoặc `Query`, và gửi đến MediatR.
        *   Nhận `Result` từ MediatR và chuyển đổi thành HTTP Response (JSON).
        *   Tích hợp middleware cho Authentication, Authorization, Error Handling.

    **5.2. Application Layer**
    *   Chứa các `Command` và `Query` định nghĩa các ý định của người dùng/hệ thống.
    *   Chứa các `Handler` (implement `IRequestHandler<TCommand, TResult>` hoặc `IRequestHandler<TQuery, TResult>`) để xử lý logic nghiệp vụ.
    *   `ValidationBehavior` của MediatR sẽ sử dụng `FluentValidation` để validate `Command`/`Query` trước khi gửi đến `Handler`.
    *   Sử dụng `AutoMapper` để ánh xạ giữa Domain Entities và DTOs.

    **5.3. Domain Layer**
    *   **Entities:** `HocSinh`, `LopHoc` với các thuộc tính và hành vi nghiệp vụ.
    *   **Value Objects:** Ví dụ: `FullName`, `Address` (nếu cần thiết để đóng gói logic).
    *   **Domain Services:** Chứa logic nghiệp vụ phức tạp liên quan đến nhiều thực thể hoặc cần phối hợp nhiều hành vi.
    *   **Interfaces Repository:** `IStudentRepository`, `IClassRepository` (định nghĩa các hợp đồng truy cập dữ liệu).

    **5.4. Infrastructure Layer**
    *   **Data Access:**
        *   Sử dụng **Entity Framework Core (EF Core)** làm ORM.
        *   `DbContext`: `StudentManagementDbContext` để quản lý các thực thể và phiên làm việc với database.
        *   `Repository Implementations`: Triển khai `IStudentRepository`, `IClassRepository` sử dụng `StudentManagementDbContext` và EF Core.
        *   `Migrations`: Quản lý lược đồ cơ sở dữ liệu.
    *   **External Services Integration:**
        *   Client cho dịch vụ `Class Management Service` (để lấy danh sách lớp học).
        *   Client cho `Authentication & Authorization Service` (ví dụ: Identity Server hoặc dịch vụ nội bộ).
    *   **Logging:** Sử dụng **Serilog** để ghi nhật ký, có thể tích hợp với Elastic Stack (ELK) hoặc Application Insights.
    *   **Caching:** Có thể triển khai In-memory cache hoặc Distributed Cache (Redis) cho các dữ liệu ít thay đổi như danh sách lớp học.

    **5.5. Database**
    *   Sử dụng **PostgreSQL** làm hệ quản trị cơ sở dữ liệu quan hệ.
    *   Cấu trúc bảng dựa trên mô hình dữ liệu sơ bộ trong SRS, với các chỉ mục được tối ưu cho tìm kiếm và lọc (`MaHocSinh`, `HoTen`, `LopHocID`, `TrangThai`).
    *   Thiết lập Foreign Key Constraints để đảm bảo tính toàn vẹn dữ liệu, đặc biệt cho `HocSinh.LopHocID` và các ràng buộc xóa (ON DELETE RESTRICT).

## 6. KIẾN TRÚC TRIỂN KHAI (Deployment View)

*   `QUAN-20260604-153038-SAD06`
    *   **Containerization:** Toàn bộ Backend API và Frontend UI sẽ được đóng gói thành các Docker container riêng biệt.
    *   **Orchestration:** Sử dụng **Kubernetes (K8s)** để quản lý, triển khai và tự động mở rộng các container.
    *   **Cloud Platform:** Triển khai trên **Azure** (hoặc AWS, GCP tùy theo chiến lược của ONENET) sử dụng:
        *   **Azure Kubernetes Service (AKS):** Để host các Docker containers của Backend API và Frontend.
        *   **Azure Database for PostgreSQL:** Để host cơ sở dữ liệu.
        *   **Azure Front Door / Application Gateway:** Để cân bằng tải, định tuyến và bảo vệ ứng dụng web.
        *   **Azure Key Vault:** Để lưu trữ các thông tin nhạy cảm (chuỗi kết nối DB, API keys).
        *   **Azure Monitor / Application Insights:** Để giám sát hiệu suất và thu thập log.

    ```
    +-------------------------------------------------------------+
    |                     Azure Cloud / K8s Cluster               |
    |                                                             |
    | +---------------------+      +--------------------------+ |
    | |  Azure Front Door   |<---->|  Azure App Gateway       | |
    | +---------------------+      +--------------------------+ |
    |           ^                               ^                |
    |           | Public Internet              | Internal Route |
    |           v                               v                |
    | +---------------------------------------------------------+ |
    | |                 Kubernetes Service (AKS)                | |
    | |  +-------------------------------------+                | |
    | |  |  Frontend UI Pods (Nginx + React)   |<---+           | |
    | |  +-------------------------------------+    |           | |
    | |  +-------------------------------------+    | Service   | |
    | |  |  Student Management API Pods (.NET) |<---+ Discovery | |
    | |  +-------------------------------------+    |           | |
    | +---------------------------------------------------------+ |
    |           ^                     ^                          |
    |           | Secure Connection   |                           |
    |           v                     v                          |
    | +---------------------------+  +--------------------------+ |
    | | Azure Database for        |  |  Azure Key Vault         | |
    | | PostgreSQL (Managed DB)   |  |  (Secrets Management)    | |
    | +---------------------------+  +--------------------------+ |
    |                                                             |
    +-------------------------------------------------------------+
    ```

## 7. KIẾN TRÚC DỮ LIỆU (Data Architecture)

*   `QUAN-20260604-153038-SAD07`
    *   **Database Schema:**
        *   **HocSinh Table:**
            *   `MaHocSinh` (PK, VARCHAR(50), NOT NULL, UNIQUE, Indexed)
            *   `HoTen` (VARCHAR(255), NOT NULL, Indexed for search)
            *   `NgaySinh` (DATE, NOT NULL)
            *   `GioiTinh` (VARCHAR(10), NOT NULL)
            *   `DiaChi` (VARCHAR(500))
            *   `SoDienThoaiPH` (VARCHAR(20))
            *   `EmailPH` (VARCHAR(255))
            *   `LopHocID` (FK to LopHoc.LopHocID, NOT NULL, Indexed)
            *   `TrangThai` (VARCHAR(50), NOT NULL, Indexed for filter)
            *   `NgayTao` (TIMESTAMP WITH TIME ZONE, NOT NULL)
            *   `NguoiTao` (VARCHAR(100), NOT NULL)
            *   `NgayCapNhat` (TIMESTAMP WITH TIME ZONE)
            *   `NguoiCapNhat` (VARCHAR(100))
        *   **LopHoc Table:**
            *   `LopHocID` (PK, INT/UUID, NOT NULL, UNIQUE)
            *   `TenLop` (VARCHAR(100), NOT NULL, UNIQUE)
            *   `Khoi` (VARCHAR(50), NOT NULL)
            *   `NamHoc` (INT, NOT NULL)
            *   ...
    *   **Indexing Strategy:**
        *   Tạo chỉ mục (indexes) trên các cột `MaHocSinh`, `HoTen`, `LopHocID`, `TrangThai` để tối ưu hóa hiệu suất tìm kiếm và lọc.
        *   Đảm bảo các khóa ngoại (Foreign Keys) được định nghĩa đúng đắn và có chỉ mục.
    *   **Data Integrity:** Sử dụng Foreign Key Constraints tại mức database để thực thi ràng buộc `ON DELETE RESTRICT` cho `LopHocID` trong bảng `HocSinh` nhằm ngăn chặn việc xóa một lớp học nếu vẫn còn học sinh liên quan.

## 8. CÂN NHẮC BẢO MẬT (Security Considerations)

*   `QUAN-20260604-153038-SAD08`
    *   **Authentication:** Sử dụng JSON Web Tokens (JWT) cho việc xác thực người dùng. API sẽ nhận JWT từ header của mỗi request và xác thực tính hợp lệ của token.
    *   **Authorization:** Triển khai Role-Based Access Control (RBAC). Chỉ người dùng có vai trò "Admin" mới có quyền thực hiện các thao tác CRUD trên dữ liệu học sinh. Các policy authorization sẽ được áp dụng ở Presentation Layer (Controller) và Application Layer (trước khi Command/Query Handler được gọi).
    *   **Data Protection:**
        *   Mã hóa dữ liệu nhạy cảm (nếu có, ví dụ: lưu trữ mật khẩu phụ huynh) ở trạng thái nghỉ (at rest) và trong quá trình truyền tải (in transit) bằng HTTPS.
        *   Sử dụng Parameterized Queries để chống lại các cuộc tấn công SQL Injection.
        *   Input Validation chặt chẽ ở cả Frontend và Backend để ngăn chặn XSS, CSRF và các lỗ hổng khác.
    *   **Secure Configuration:** Lưu trữ các thông tin cấu hình nhạy cảm (database connection strings, API keys) trong Azure Key Vault và truy cập chúng một cách an toàn.

## 9. CÂN NHẮC HIỆU NĂNG (Performance Considerations)

*   `QUAN-20260604-153038-SAD09`
    *   **Database Optimization:**
        *   Sử dụng chỉ mục hiệu quả cho các trường tìm kiếm (`MaHocSinh`, `HoTen`), lọc (`LopHocID`, `TrangThai`) và sắp xếp.
        *   Tối ưu hóa các truy vấn EF Core bằng cách sử dụng `AsNoTracking()` cho các truy vấn đọc để cải thiện hiệu suất.
        *   Tránh N+1 queries.
    *   **Caching:** Cân nhắc triển khai caching cho dữ liệu ít thay đổi (ví dụ: danh sách lớp học) ở Application Layer hoặc Infrastructure Layer (sử dụng Redis).
    *   **Paging & Filtering:** Đảm bảo phân trang và lọc được xử lý hiệu quả ở mức database, chỉ trả về dữ liệu cần thiết cho mỗi trang.
    *   **Asynchronous Programming:** Sử dụng `async/await` trong .NET Core để các thao tác I/O (database, gọi API ngoài) không chặn luồng chính, cải thiện khả năng mở rộng của ứng dụng.

## 10. XỬ LÝ LỖI VÀ GHI NHẬT KÝ (Error Handling and Logging)

*   `QUAN-20260604-153038-SAD10`
    *   **Error Handling:**
        *   Sử dụng `Result Pattern` để trả về kết quả rõ ràng (thành công/thất bại) cùng với thông báo lỗi chi tiết từ Application Layer lên Presentation Layer.
        *   Middleware xử lý lỗi toàn cục trong ASP.NET Core Web API để bắt các ngoại lệ không được xử lý và trả về phản hồi lỗi tiêu chuẩn (ví dụ: `ProblemDetails` theo RFC 7807).
        *   Các thông báo lỗi hiển thị cho người dùng phải thân thiện và dễ hiểu.
    *   **Logging:**
        *   Sử dụng thư viện Serilog để ghi nhật ký có cấu trúc (structured logging).
        *   Ghi lại các sự kiện quan trọng (thêm/sửa/xóa học sinh), lỗi, cảnh báo và thông tin debug.
        *   Log Collector (ví dụ: Seq, Elastic Stack) sẽ tập trung log từ các instance của ứng dụng để dễ dàng phân tích và theo dõi.

## 11. GIÁM SÁT VÀ CẢNH BÁO (Monitoring and Alerting)

*   `QUAN-20260604-153038-SAD11`
    *   **Application Performance Monitoring (APM):** Sử dụng Azure Application Insights để thu thập telemetry về hiệu suất ứng dụng, request rates, latency, error rates.
    *   **Infrastructure Monitoring:** Giám sát tài nguyên Kubernetes (CPU, Memory, Network) và Database (CPU, IOPS, Storage) thông qua Azure Monitor.
    *   **Alerting:** Thiết lập các cảnh báo dựa trên các ngưỡng quan trọng (ví dụ: thời gian phản hồi API quá cao, tỷ lệ lỗi tăng đột biến, tài nguyên CPU/Memory vượt quá giới hạn) để đội ngũ vận hành có thể phản ứng kịp thời.

---

Tôi tin rằng với SRS và SAD này, đội ngũ phát triển sẽ có một lộ trình rõ ràng để triển khai tính năng "Quản lý Thông tin Học sinh" một cách hiệu quả, đáp ứng cả yêu cầu nghiệp vụ và các tiêu chuẩn kiến trúc, chất lượng của ONENET.