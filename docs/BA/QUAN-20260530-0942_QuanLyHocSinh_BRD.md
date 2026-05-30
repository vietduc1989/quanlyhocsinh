Tuyệt vời! Với vai trò là Business Analyst của ONENET, tôi sẽ phân tích và xây dựng tài liệu yêu cầu chi tiết cho tính năng "QuanLyHocSinh" dựa trên các thông tin đã cung cấp.

---

## TÀI LIỆU PHÂN TÍCH YÊU CẦU NGHIỆP VỤ (BRD)

**Mã tài liệu:** BRD-QUAN-20260530-0942-V1.0
**Ngày ban hành:** 2026-05-30
**Phiên bản:** 1.0
**Trạng thái:** Bản nháp

---

### 1. Thông tin chung

*   **Mã tính năng:** QUAN-20260530-0942
*   **Tên tính năng:** QuanLyHocSinh
*   **Dự án:** quanlyhocsinh
*   **Ngày:** 2026-05-30
*   **Người lập:** [Tên Business Analyst] - ONENET
*   **Mô tả tóm tắt:** Tính năng này nhằm mục đích cung cấp một công cụ toàn diện cho việc quản lý thông tin học sinh trong hệ thống quanlyhocsinh, bao gồm các chức năng tạo, đọc, cập nhật, xóa (CRUD), kiểm tra tính hợp lệ dữ liệu (validation), tìm kiếm và phân trang.

### 2. Tóm tắt Điều hành (Executive Summary)

Hệ thống quanlyhocsinh cần một module quản lý học sinh mạnh mẽ để lưu trữ, truy xuất và duy trì thông tin chi tiết của tất cả học sinh một cách hiệu quả. Tính năng QuanLyHocSinh sẽ là nền tảng cho nhiều quy trình nghiệp vụ khác như quản lý điểm, quản lý lớp học, quản lý chuyên cần, v.v. Việc xây dựng một giao diện thân thiện, dễ sử dụng cùng với các chức năng cốt lõi như CRUD, tìm kiếm, phân trang và kiểm tra dữ liệu sẽ đảm bảo tính chính xác và toàn vẹn của dữ liệu học sinh, từ đó nâng cao hiệu quả vận hành của toàn hệ thống.

### 3. Mục tiêu (Objectives)

*   **Mục tiêu chính:** Cung cấp một module quản lý học sinh tập trung, cho phép người dùng thực hiện các thao tác CRUD một cách hiệu quả.
*   **Mục tiêu cụ thể:**
    *   Đảm bảo dữ liệu học sinh được lưu trữ chính xác và nhất quán.
    *   Tăng cường khả năng truy xuất thông tin học sinh nhanh chóng thông qua chức năng tìm kiếm và phân trang.
    *   Giảm thiểu lỗi nhập liệu bằng cách triển khai các quy tắc kiểm tra dữ liệu chặt chẽ.
    *   Tạo nền tảng vững chắc cho việc tích hợp với các module quản lý khác trong tương lai.

### 4. Phạm vi (Scope)

*   **Trong phạm vi:**
    *   Quản lý thông tin chi tiết của từng học sinh.
    *   Thực hiện các thao tác: Thêm mới (Create), Xem (Read), Cập nhật (Update), Xóa (Delete) thông tin học sinh.
    *   Hỗ trợ tìm kiếm học sinh theo nhiều tiêu chí.
    *   Triển khai chức năng phân trang cho danh sách học sinh.
    *   Áp dụng các quy tắc kiểm tra tính hợp lệ dữ liệu (validation) tại client-side và server-side.
*   **Ngoài phạm vi:**
    *   Quản lý lớp học (Giả định module này đã tồn tại hoặc sẽ được xây dựng riêng, thông tin Lớp học sẽ được tham chiếu).
    *   Quản lý điểm số, lịch học, chuyên cần (sẽ là các module riêng biệt sử dụng dữ liệu học sinh).
    *   Tích hợp với các hệ thống bên ngoài (e.g., hệ thống thanh toán, email tự động).
    *   Quản lý quyền và vai trò người dùng chi tiết (Giả định đã có hệ thống phân quyền cơ bản).

### 5. Đối tượng sử dụng (User Roles)

*   **Quản trị viên (Admin):** Có toàn quyền thực hiện các thao tác CRUD, tìm kiếm, xem, phân trang.
*   **Giáo viên (Teacher):** Có quyền xem danh sách học sinh, xem chi tiết học sinh, tìm kiếm và phân trang. (Quyền thêm/sửa/xóa có thể được cân nhắc dựa trên yêu cầu chi tiết hơn sau này, nhưng ban đầu chỉ nên là Admin).

### 6. Các Yêu cầu Nghiệp vụ Chi tiết (Detailed Business Requirements)

#### 6.1. Mô hình Dữ liệu (Conceptual Data Model)

**Thực thể: Học Sinh (HocSinh)**

| Trường dữ liệu     | Kiểu dữ liệu   | Mô tả                                        | Ràng buộc          | Ghi chú                                   |
| :----------------- | :------------- | :------------------------------------------- | :----------------- | :---------------------------------------- |
| `MaHocSinh`        | String (PK)    | Mã định danh duy nhất của học sinh           | Bắt buộc, Duy nhất | Tự động sinh hoặc nhập tùy cấu hình      |
| `HoTen`            | String         | Họ và tên đầy đủ của học sinh                | Bắt buộc           |                                           |
| `NgaySinh`         | Date           | Ngày sinh của học sinh                       | Bắt buộc           | Định dạng YYYY-MM-DD                      |
| `GioiTinh`         | String         | Giới tính của học sinh                       | Bắt buộc           | "Nam", "Nữ", "Khác"                       |
| `DiaChi`           | String         | Địa chỉ hiện tại của học sinh                | Tùy chọn           |                                           |
| `SoDienThoaiPH`    | String         | Số điện thoại liên hệ của phụ huynh          | Tùy chọn           | Định dạng số điện thoại Việt Nam          |
| `EmailPH`          | String         | Email liên hệ của phụ huynh                  | Tùy chọn           | Định dạng email hợp lệ                    |
| `MaLopHoc`         | String (FK)    | Mã lớp học mà học sinh đang theo học         | Bắt buộc           | Tham chiếu đến thực thể LớpHoc            |
| `TrangThai`        | String         | Trạng thái học sinh (Đang học, Thôi học...) | Bắt buộc           | Mặc định: "Đang học"                      |
| `NgayTao`          | DateTime       | Ngày học sinh được thêm vào hệ thống         | Tự động            |                                           |
| `NguoiTao`         | String         | Người dùng đã tạo bản ghi này                | Tự động            |                                           |
| `NgayCapNhatCuoi`  | DateTime       | Ngày bản ghi được cập nhật lần cuối          | Tự động            |                                           |
| `NguoiCapNhatCuoi` | String         | Người dùng đã cập nhật bản ghi này           | Tự động            |                                           |

#### 6.2. Các Yêu cầu Chức năng (Functional Requirements) - User Stories

##### 6.2.1. QLHS.FR.001 - Quản lý Học Sinh (CRUD)

**QLHS.US.001: Xem danh sách học sinh**
*   **Với vai trò là:** Quản trị viên hoặc Giáo viên
*   **Tôi muốn:** Xem danh sách tất cả học sinh trong hệ thống
*   **Để:** Có cái nhìn tổng quan và dễ dàng truy cập thông tin học sinh.
*   **Tiêu chí chấp nhận (Acceptance Criteria):**
    *   Hệ thống hiển thị một bảng chứa danh sách học sinh.
    *   Bảng hiển thị các cột thông tin cơ bản: Mã học sinh, Họ và tên, Ngày sinh, Giới tính, Lớp học, Trạng thái.
    *   Danh sách được sắp xếp mặc định theo tên hoặc mã học sinh.
    *   Có liên kết hoặc nút để xem chi tiết, chỉnh sửa, xóa (chỉ Admin).
    *   Danh sách phải hỗ trợ phân trang (xem QLHS.US.006).
    *   Danh sách phải hỗ trợ tìm kiếm (xem QLHS.US.005).

**QLHS.US.002: Thêm mới học sinh**
*   **Với vai trò là:** Quản trị viên
*   **Tôi muốn:** Thêm một học sinh mới vào hệ thống
*   **Để:** Ghi nhận thông tin của học sinh mới.
*   **Tiêu chí chấp nhận:**
    *   Hệ thống cung cấp một form nhập liệu đầy đủ các trường thông tin của học sinh.
    *   Form có các nút "Lưu" và "Hủy".
    *   Sau khi lưu thành công, học sinh mới sẽ xuất hiện trong danh sách và hệ thống hiển thị thông báo thành công.
    *   Nếu có lỗi, hệ thống hiển thị thông báo lỗi và giữ lại dữ liệu đã nhập (trừ mật khẩu nếu có).
    *   Dữ liệu nhập phải được kiểm tra tính hợp lệ (xem QLHS.US.007).

**QLHS.US.003: Xem chi tiết học sinh**
*   **Với vai trò là:** Quản trị viên hoặc Giáo viên
*   **Tôi muốn:** Xem thông tin chi tiết của một học sinh cụ thể
*   **Để:** Kiểm tra toàn bộ dữ liệu của học sinh đó.
*   **Tiêu chí chấp nhận:**
    *   Khi người dùng nhấp vào tên/mã học sinh hoặc nút "Xem chi tiết" trong danh sách, hệ thống hiển thị một trang/pop-up chứa tất cả các trường thông tin của học sinh đó.
    *   Thông tin hiển thị ở chế độ chỉ đọc.
    *   Có nút "Quay lại" hoặc "Đóng".

**QLHS.US.004: Cập nhật thông tin học sinh**
*   **Với vai trò là:** Quản trị viên
*   **Tôi muốn:** Cập nhật thông tin của một học sinh đã có
*   **Để:** Đảm bảo dữ liệu luôn chính xác và mới nhất.
*   **Tiêu chí chấp nhận:**
    *   Khi người dùng nhấp vào nút "Chỉnh sửa" trong danh sách hoặc trang chi tiết, hệ thống hiển thị một form nhập liệu được điền sẵn thông tin hiện tại của học sinh.
    *   Form có các nút "Lưu thay đổi" và "Hủy".
    *   Sau khi lưu thành công, thông tin của học sinh được cập nhật và hệ thống hiển thị thông báo thành công.
    *   Nếu có lỗi, hệ thống hiển thị thông báo lỗi và giữ lại dữ liệu đã nhập.
    *   Dữ liệu nhập phải được kiểm tra tính hợp lệ (xem QLHS.US.007).
    *   Trường `MaHocSinh` không được phép chỉnh sửa.

**QLHS.US.005: Xóa học sinh**
*   **Với vai trò là:** Quản trị viên
*   **Tôi muốn:** Xóa một học sinh khỏi hệ thống
*   **Để:** Loại bỏ các hồ sơ không còn cần thiết.
*   **Tiêu chí chấp nhận:**
    *   Khi người dùng nhấp vào nút "Xóa" trong danh sách, hệ thống hiển thị một hộp thoại xác nhận (ví dụ: "Bạn có chắc chắn muốn xóa học sinh [Tên học sinh] không?").
    *   Nếu người dùng xác nhận, học sinh sẽ bị xóa khỏi hệ thống. (Ưu tiên **Soft Delete** - thay đổi `TrangThai` thành "Thôi học" hoặc "Đã xóa" thay vì xóa vĩnh viễn khỏi DB để bảo toàn lịch sử dữ liệu).
    *   Hệ thống hiển thị thông báo thành công hoặc lỗi.
    *   Học sinh đã xóa (soft delete) sẽ không xuất hiện trong danh sách mặc định (trừ khi có chức năng lọc riêng cho trạng thái này).

##### 6.2.2. QLHS.FR.002 - Tìm kiếm (Search)

**QLHS.US.006: Tìm kiếm học sinh**
*   **Với vai trò là:** Quản trị viên hoặc Giáo viên
*   **Tôi muốn:** Tìm kiếm học sinh theo các tiêu chí khác nhau
*   **Để:** Nhanh chóng định vị học sinh cụ thể trong danh sách lớn.
*   **Tiêu chí chấp nhận:**
    *   Hệ thống cung cấp một ô tìm kiếm chung (global search) hoặc các trường tìm kiếm riêng biệt.
    *   Tìm kiếm hỗ trợ các tiêu chí sau:
        *   **Mã học sinh:** Tìm kiếm chính xác hoặc một phần.
        *   **Họ và tên:** Tìm kiếm một phần, không phân biệt chữ hoa/thường.
        *   **Lớp học:** Tìm kiếm chính xác hoặc một phần của tên lớp.
    *   Kết quả tìm kiếm được hiển thị trong bảng danh sách, cùng với phân trang.
    *   Nếu không tìm thấy kết quả, hệ thống hiển thị thông báo "Không tìm thấy học sinh nào phù hợp."

##### 6.2.3. QLHS.FR.003 - Phân trang (Pagination)

**QLHS.US.007: Phân trang danh sách học sinh**
*   **Với vai trò là:** Quản trị viên hoặc Giáo viên
*   **Tôi muốn:** Duyệt qua danh sách học sinh theo từng trang
*   **Để:** Xử lý các danh sách lớn một cách hiệu quả và cải thiện hiệu suất tải trang.
*   **Tiêu chí chấp nhận:**
    *   Danh sách học sinh được chia thành các trang.
    *   Người dùng có thể chọn số lượng học sinh hiển thị trên mỗi trang (ví dụ: 10, 25, 50, 100).
    *   Có các điều khiển điều hướng trang (ví dụ: "Trang trước", "Trang sau", số trang cụ thể).
    *   Hệ thống hiển thị thông tin về tổng số học sinh và số lượng học sinh trên trang hiện tại (ví dụ: "Hiển thị 1-10 trên tổng số 100 học sinh").

##### 6.2.4. QLHS.FR.004 - Kiểm tra tính hợp lệ dữ liệu (Validation)

**QLHS.US.008: Kiểm tra tính hợp lệ dữ liệu khi nhập/cập nhật**
*   **Với vai trò là:** Người dùng nhập liệu
*   **Tôi muốn:** Hệ thống kiểm tra dữ liệu tôi nhập vào là hợp lệ
*   **Để:** Ngăn chặn lỗi và đảm bảo tính toàn vẹn của dữ liệu.
*   **Tiêu chí chấp nhận:**
    *   **MaHocSinh:**
        *   Bắt buộc.
        *   Duy nhất trong hệ thống.
        *   Độ dài tối đa 20 ký tự (ví dụ).
        *   Chỉ chứa chữ cái, số, dấu gạch ngang/dưới (ví dụ).
    *   **HoTen:**
        *   Bắt buộc.
        *   Độ dài tối đa 100 ký tự (ví dụ).
    *   **NgaySinh:**
        *   Bắt buộc.
        *   Phải là ngày hợp lệ theo định dạng YYYY-MM-DD.
        *   Không được là ngày trong tương lai.
        *   Tuổi học sinh phải nằm trong khoảng hợp lý (ví dụ: từ 5 đến 20 tuổi).
    *   **GioiTinh:**
        *   Bắt buộc.
        *   Phải là một trong các giá trị cho phép (Nam, Nữ, Khác).
    *   **DiaChi:**
        *   Độ dài tối đa 255 ký tự (ví dụ).
    *   **SoDienThoaiPH:**
        *   Tùy chọn. Nếu nhập, phải theo định dạng số điện thoại Việt Nam hợp lệ (ví dụ: 10 chữ số, bắt đầu bằng 0).
    *   **EmailPH:**
        *   Tùy chọn. Nếu nhập, phải theo định dạng email hợp lệ.
    *   **MaLopHoc:**
        *   Bắt buộc.
        *   Phải tồn tại trong danh sách lớp học (tích hợp/lookup với module Lớp học).
    *   **TrangThai:**
        *   Bắt buộc.
        *   Phải là một trong các giá trị cho phép (Đang học, Thôi học, Tạm nghỉ, v.v.).
    *   Thông báo lỗi rõ ràng được hiển thị bên cạnh trường nhập liệu không hợp lệ.

### 7. Yêu cầu Phi chức năng (Non-Functional Requirements)

*   **Hiệu suất (Performance):**
    *   Thời gian tải danh sách học sinh không quá 3 giây với 1000 bản ghi.
    *   Thời gian thực hiện thao tác CRUD không quá 2 giây.
*   **Bảo mật (Security):**
    *   Chỉ người dùng có quyền tương ứng mới có thể truy cập các chức năng (Quản trị viên có toàn quyền, Giáo viên chỉ xem).
    *   Dữ liệu nhạy cảm của học sinh (nếu có) phải được mã hóa khi lưu trữ và truyền tải.
*   **Khả năng sử dụng (Usability):**
    *   Giao diện người dùng trực quan, dễ hiểu và dễ sử dụng.
    *   Đảm bảo tính nhất quán về UI/UX với các module khác của hệ thống.
*   **Độ tin cậy (Reliability):**
    *   Hệ thống phải hoạt động ổn định, không gặp lỗi thường xuyên khi thực hiện các tác vụ quản lý học sinh.
    *   Có cơ chế phục hồi dữ liệu trong trường hợp lỗi hệ thống.
*   **Khả năng mở rộng (Scalability):**
    *   Hệ thống có khả năng mở rộng để xử lý số lượng học sinh lớn trong tương lai (ví dụ: hàng chục nghìn học sinh) mà không ảnh hưởng đáng kể đến hiệu suất.
*   **Tính bảo trì (Maintainability):**
    *   Mã nguồn phải được tổ chức tốt, dễ đọc và dễ bảo trì, cập nhật.

### 8. Các Giả định và Ràng buộc (Assumptions & Constraints)

*   **Giả định:**
    *   Hệ thống xác thực và ủy quyền người dùng (Authentication & Authorization) đã được triển khai.
    *   Module quản lý Lớp học (LopHoc) đã tồn tại hoặc sẽ được phát triển song song, cung cấp dữ liệu lớp học để tham chiếu.
    *   Hạ tầng kỹ thuật (server, database) đủ mạnh để đáp ứng yêu cầu hiệu suất.
*   **Ràng buộc:**
    *   Phải sử dụng công nghệ và framework đã được ONENET phê duyệt cho dự án.
    *   Tuân thủ các quy định về bảo vệ dữ liệu cá nhân nếu có.
    *   Thời gian triển khai dự án theo lịch đã định.

### 9. Yêu cầu về Giao diện người dùng (UI/UX - High-level)

*   **Trang Danh sách Học sinh:**
    *   Tiêu đề rõ ràng: "Quản lý Học Sinh".
    *   Nút "Thêm mới Học Sinh" nổi bật.
    *   Ô tìm kiếm và bộ lọc (theo Lớp học, Trạng thái) rõ ràng.
    *   Bảng hiển thị dữ liệu với các cột đã nêu, có thể sắp xếp theo từng cột.
    *   Cột "Thao tác" chứa các nút "Xem chi tiết", "Chỉnh sửa", "Xóa".
    *   Thanh phân trang ở phía dưới bảng.
*   **Form Thêm mới/Chỉnh sửa Học sinh:**
    *   Tiêu đề rõ ràng: "Thêm mới Học Sinh" hoặc "Chỉnh sửa Học Sinh".
    *   Các trường nhập liệu được sắp xếp hợp lý, có nhãn rõ ràng.
    *   Dropdown/Combobox cho các trường có giá trị cố định (Giới tính, Lớp học, Trạng thái).
    *   Date picker cho trường Ngày sinh.
    *   Thông báo lỗi hiển thị ngay dưới trường nhập liệu bị lỗi.
    *   Nút "Lưu" và "Hủy" ở cuối form.

### 10. Kế hoạch Triển khai Giai đoạn tiếp theo

1.  **Họp xem xét tài liệu (BRD Review Meeting):** Tổ chức cuộc họp với các bên liên quan (Product Owner, Development Team Lead, QA Lead) để xem xét, thảo luận và thống nhất về các yêu cầu.
2.  **Thiết kế Giao diện (UI/UX Design):** Dựa trên BRD này, đội ngũ UI/UX sẽ bắt đầu phác thảo wireframe và mockup chi tiết.
3.  **Thiết kế Kỹ thuật (Technical Design):** Đội ngũ phát triển sẽ phân tích BRD và thiết kế kiến trúc kỹ thuật, lựa chọn công nghệ và lên kế hoạch triển khai.
4.  **Phát triển (Development):** Triển khai mã nguồn theo thiết kế kỹ thuật.
5.  **Kiểm thử (Testing):** Đội ngũ QA sẽ kiểm thử tính năng dựa trên các yêu cầu nghiệp vụ và tiêu chí chấp nhận.
6.  **Triển khai (Deployment):** Đưa tính năng vào môi trường thử nghiệm/sản phẩm.

---

**[Ký tên]**
**Business Analyst - ONENET**