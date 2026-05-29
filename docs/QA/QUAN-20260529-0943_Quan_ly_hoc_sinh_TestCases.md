Chào bạn, tôi là QA của ONENET. Dưới đây là bộ Test Cases chi tiết (bao gồm Happy cases và Edge cases) cho tính năng **Quản lý học sinh** (Mã tính năng: `QUAN-20260529-0943`).

Do tài liệu BRD hiện tại là N/A, tôi đã thiết lập các **Quy tắc nghiệp vụ giả định tiêu chuẩn (Assumed Business Rules)** dưới đây để làm cơ sở viết test cases chi tiết và thực tế nhất:
1. *Mã học sinh:* Tự động sinh bởi hệ thống hoặc nhập tay (độ dài 5-10 ký tự, không khoảng trắng, không dấu).
2. *Họ và tên:* Bắt buộc, từ 2 - 50 ký tự, không chứa số/ký tự đặc biệt.
3. *Tuổi học sinh:* Từ 6 đến 18 tuổi (tính theo năm sinh hiện tại).
4. *Số điện thoại phụ huynh:* 10 chữ số, bắt đầu bằng đầu số hợp lệ của Việt Nam (03, 05, 07, 08, 09).

---

### PHẦN I: DANH SÁCH TEST CASES (TỔNG HỢP)

| STT | Mã Test Case | Phân loại | Tên Test Case |
| :--- | :--- | :--- | :--- |
| **I** | **CHỨC NĂNG: THÊM MỚI HỌC SINH (CREATE)** | | |
| 1 | `QUAN-20260529-0943-TC01` | Happy Case | Thêm mới học sinh thành công với đầy đủ thông tin hợp lệ |
| 2 | `QUAN-20260529-0943-TC02` | Happy Case | Thêm mới học sinh thành công chỉ với các trường bắt buộc |
| 3 | `QUAN-20260529-0943-TC03` | Edge Case | Thêm mới thất bại khi để trống các trường bắt buộc |
| 4 | `QUAN-20260529-0943-TC04` | Edge Case | Validate ngày sinh: Học sinh dưới 6 tuổi hoặc trên 18 tuổi |
| 5 | `QUAN-20260529-0943-TC05` | Edge Case | Validate số điện thoại phụ huynh sai định dạng |
| 6 | `QUAN-20260529-0943-TC06` | Edge Case | Trùng mã học sinh (nếu hệ thống cho phép nhập tay) |
| **II** | **CHỨC NĂNG: TÌM KIẾM & PHÂN TRANG (READ)** | | |
| 7 | `QUAN-20260529-0943-TC07` | Happy Case | Tìm kiếm học sinh theo Mã học sinh/Họ tên chính xác |
| 8 | `QUAN-20260529-0943-TC08` | Edge Case | Tìm kiếm với từ khóa chứa ký tự đặc biệt hoặc SQL Injection |
| 9 | `QUAN-20260529-0943-TC09` | Happy Case | Phân trang và chuyển trang danh sách học sinh |
| **III**| **CHỨC NĂNG: CẬP NHẬT THÔNG TIN (UPDATE)** | | |
| 10 | `QUAN-20260529-0943-TC10` | Happy Case | Cập nhật thông tin học sinh thành công |
| 11 | `QUAN-20260529-0943-TC11` | Edge Case | Cập nhật thông tin để trống trường bắt buộc |
| **IV** | **CHỨC NĂNG: XÓA HỌC SINH (DELETE)** | | |
| 12 | `QUAN-20260529-0943-TC12` | Happy Case | Xóa học sinh có xác nhận hủy bỏ/đồng ý |
| 13 | `QUAN-20260529-0943-TC13` | Edge Case | Xóa học sinh đang có dữ liệu ràng buộc (VD: Điểm số, Vi phạm) |
| **V** | **CHỨC NĂNG: IMPORT/EXPORT (NÂNG CAO)** | | |
| 14 | `QUAN-20260529-0943-TC14` | Happy Case | Xuất file Excel danh sách học sinh thành công |
| 15 | `QUAN-20260529-0943-TC15` | Edge Case | Import file Excel danh sách học sinh chứa dữ liệu lỗi |

---

### PHẦN II: CHI TIẾT CÁC TEST CASES

#### I. CHỨC NĂNG: THÊM MỚI HỌC SINH (CREATE)

##### `QUAN-20260529-0943-TC01`: Thêm mới học sinh thành công với đầy đủ thông tin hợp lệ (Happy Case)
*   **Mô tả:** Kiểm tra chức năng thêm mới học sinh hoạt động đúng khi người dùng nhập toàn bộ các thông tin hợp lệ.
*   **Các bước thực hiện:**
    1. Truy cập màn hình "Quản lý học sinh".
    2. Click nút "Thêm mới".
    3. Nhập đầy đủ thông tin:
        *   Mã HS: `HS00001`
        *   Họ và tên: `Nguyễn Văn A`
        *   Ngày sinh: `15/05/2012` (14 tuổi - hợp lệ)
        *   Giới tính: Chọn `Nam`
        *   Lớp: Chọn lớp từ dropdown (VD: `8A1`)
        *   SĐT Phụ huynh: `0912345678`
        *   Địa chỉ: `Số 1, Đường Trần Hưng Đạo, Hà Nội`
    4. Click nút "Lưu".
*   **Kết quả mong đợi:**
    *   Hệ thống hiển thị thông báo: "Thêm mới học sinh thành công!"
    *   Học sinh `Nguyễn Văn A` hiển thị ở đầu danh sách học sinh.
    *   Dữ liệu trong database được lưu chính xác, không bị lỗi font tiếng Việt.

##### `QUAN-20260529-0943-TC02`: Thêm mới học sinh thành công chỉ với các trường bắt buộc (Happy Case)
*   **Mô tả:** Kiểm tra việc lưu thông tin khi bỏ qua các trường không bắt buộc (SĐT, Địa chỉ).
*   **Các bước thực hiện:**
    1. Truy cập màn hình "Quản lý học sinh" -> Click "Thêm mới".
    2. Nhập các trường bắt buộc:
        *   Mã HS: `HS00002`
        *   Họ tên: `Trần Thị B`
        *   Ngày sinh: `10/10/2015`
        *   Giới tính: `Nữ`
        *   Lớp: `5A`
    3. Để trống: SĐT Phụ huynh, Địa chỉ.
    4. Click "Lưu".
*   **Kết quả mong đợi:** Thêm mới thành công. Bản ghi mới lưu SĐT và Địa chỉ là `Null` hoặc trống.

##### `QUAN-20260529-0943-TC03`: Thêm mới thất bại khi để trống các trường bắt buộc (Edge Case)
*   **Mô tả:** Kiểm tra validation khi người dùng không nhập các thông tin bắt buộc.
*   **Các bước thực hiện:**
    1. Truy cập màn hình "Thêm mới học sinh".
    2. Để trống tất cả các trường.
    3. Click "Lưu".
*   **Kết quả mong đợi:**
    *   Hệ thống không cho phép lưu.
    *   Các trường bắt buộc (Mã HS, Họ tên, Ngày sinh, Lớp) hiển thị viền đỏ và thông báo lỗi dưới từng trường: "Trường này không được để trống".

##### `QUAN-20260529-0943-TC04`: Validate ngày sinh: Học sinh dưới 6 tuổi hoặc trên 18 tuổi (Edge Case)
*   **Mô tả:** Kiểm tra tính hợp lệ của độ tuổi (Giới hạn từ 6 - 18 tuổi).
*   **Các bước thực hiện:**
    1. Click "Thêm mới".
    2. Nhập họ tên hợp lệ.
    3. Nhập ngày sinh:
        *   *Case A (Dưới 6 tuổi):* Nhập năm sinh hiện tại trừ đi 3 (Ví dụ năm nay 2026, nhập `01/01/2023`).
        *   *Case B (Trên 18 tuổi):* Nhập năm sinh hiện tại trừ đi 25 (Ví dụ nhập `01/01/2000`).
        *   *Case C (Ngày ở tương lai):* Nhập `30/12/2028`.
    4. Click "Lưu".
*   **Kết quả mong đợi:** Hệ thống báo lỗi tại trường Ngày sinh: "Tuổi học sinh phải từ 6 đến 18 tuổi" hoặc "Ngày sinh không hợp lệ".

##### `QUAN-20260529-0943-TC05`: Validate số điện thoại phụ huynh sai định dạng (Edge Case)
*   **Mô tả:** Đảm bảo số điện thoại nhập vào phải đúng định dạng số điện thoại Việt Nam.
*   **Các bước thực hiện:**
    1. Thêm mới học sinh, nhập các thông tin khác hợp lệ.
    2. Tại trường SĐT Phụ huynh, nhập thử các case:
        *   *Case A:* `012345678` (Chỉ có 9 số)
        *   *Case B:* `09123456789` (11 số)
        *   *Case C:* `abc1234567` (Chứa chữ)
        *   *Case D:* `1900100800` (Đầu số không hợp lệ)
    3. Click "Lưu".
*   **Kết quả mong đợi:** Hệ thống báo lỗi tại trường SĐT phụ huynh: "Số điện thoại không đúng định dạng (10 chữ số, bắt đầu bằng đầu số hợp lệ)".

##### `QUAN-20260529-0943-TC06`: Trùng mã học sinh (Edge Case)
*   **Mô tả:** Đảm bảo tính duy nhất của Mã học sinh trong hệ thống.
*   **Các bước thực hiện:**
    1. Giả sử trong hệ thống đã có mã học sinh `HS00001`.
    2. Tạo mới học sinh khác và nhập tay mã học sinh là `HS00001`.
    3. Điền đầy đủ các thông tin hợp lệ khác.
    4. Click "Lưu".
*   **Kết quả mong đợi:** Hệ thống báo lỗi: "Mã học sinh đã tồn tại trong hệ thống".

---

#### II. CHỨC NĂNG: TÌM KIẾM & PHÂN TRANG (READ)

##### `QUAN-20260529-0943-TC07`: Tìm kiếm học sinh theo Mã học sinh/Họ tên (Happy Case)
*   **Mô tả:** Kiểm tra tính chính xác của bộ lọc tìm kiếm.
*   **Các bước thực hiện:**
    1. Tại ô tìm kiếm, nhập mã chính xác: `HS00001` -> Nhấn Enter hoặc click nút "Tìm kiếm".
    2. Xóa ô tìm kiếm, nhập tên không dấu/có dấu viết thường: `nguyen van a` -> Tìm kiếm.
*   **Kết quả mong đợi:**
    1. Hệ thống hiển thị đúng duy nhất học sinh có mã `HS00001`.
    2. Hệ thống tìm kiếm không phân biệt chữ hoa/thường, không phân biệt dấu (nếu hệ thống hỗ trợ) và trả về kết quả chứa từ khóa `Nguyễn Văn A`.

##### `QUAN-20260529-0943-TC08`: Tìm kiếm với ký tự đặc biệt hoặc SQL Injection (Edge Case)
*   **Mô tả:** Kiểm tra độ bảo mật và xử lý chuỗi của ô tìm kiếm.
*   **Các bước thực hiện:**
    1. Nhập vào ô tìm kiếm: `' OR 1=1 --` hoặc `<script>alert(1)</script>` hoặc `$%&*@#`.
    2. Nhấn nút tìm kiếm.
*   **Kết quả mong đợi:** Hệ thống không bị crash, không lỗi SQL, hiển thị thông báo: "Không tìm thấy kết quả phù hợp".

##### `QUAN-20260529-0943-TC09`: Phân trang danh sách học sinh (Happy Case)
*   **Mô tả:** Xác nhận tính năng phân trang hoạt động tốt khi số lượng học sinh vượt quá giới hạn hiển thị một trang (Mặc định: 10 học sinh/trang).
*   **Các bước thực hiện:**
    1. Kiểm tra tổng số học sinh hiện có (Ví dụ: có 25 học sinh).
    2. Quan sát trang 1 (hiển thị 10 học sinh đầu tiên).
    3. Click nút chuyển sang "Trang 2" -> "Trang 3".
*   **Kết quả mong đợi:**
    *   Trang 2 hiển thị học sinh từ thứ 11 đến 20.
    *   Trang 3 hiển thị 5 học sinh còn lại.
    *   Các nút "Trước", "Sau", "Đầu", "Cuối" hoạt động mượt mà.

---

#### III. CHỨC NĂNG: CẬP NHẬT THÔNG TIN (UPDATE)

##### `QUAN-20260529-0943-TC10`: Cập nhật thông tin học sinh thành công (Happy Case)
*   **Mô tả:** Chỉnh sửa thông tin học sinh hiện tại và lưu lại.
*   **Các bước thực hiện:**
    1. Tại dòng học sinh `Nguyễn Văn A`, click biểu tượng "Sửa" (Edit).
    2. Hệ thống load đúng dữ liệu cũ lên form.
    3. Thay đổi lớp từ `8A1` sang `8A2`, cập nhật SĐT phụ huynh thành `0988888888`.
    4. Click "Lưu".
*   **Kết quả mong đợi:**
    *   Thông báo: "Cập nhật thông tin học sinh thành công!"
    *   Trên danh sách, thông tin lớp và SĐT của học sinh này đã được thay đổi mới.

##### `QUAN-20260529-0943-TC11`: Cập nhật thông tin để trống trường bắt buộc (Edge Case)
*   **Mô tả:** Đảm bảo validation vẫn hoạt động khi sửa thông tin.
*   **Các bước thực hiện:**
    1. Chọn một học sinh -> Click "Sửa".
    2. Xóa sạch dữ liệu ở trường "Họ và tên".
    3. Click "Lưu".
*   **Kết quả mong đợi:** Hệ thống chặn không cho lưu và báo lỗi: "Họ và tên không được để trống". Dữ liệu cũ của học sinh trong DB không bị thay đổi.

---

#### IV. CHỨC NĂNG: XÓA HỌC SINH (DELETE)

##### `QUAN-20260529-0943-TC12`: Xóa học sinh có xác nhận hủy bỏ/đồng ý (Happy Case)
*   **Mô tả:** Đảm bảo quy trình xóa an toàn, tránh người dùng click nhầm.
*   **Các bước thực hiện:**
    1. Click biểu tượng "Xóa" tại học sinh `Nguyễn Văn A`.
    2. Hệ thống hiển thị Pop-up xác nhận: "Bạn có chắc chắn muốn xóa học sinh này không?".
    3. Click "Hủy" (Cancel) -> Kiểm tra danh sách.
    4. Click lại "Xóa" -> Chọn "Đồng ý" (Confirm).
*   **Kết quả mong đợi:**
    *   Khi chọn "Hủy": Học sinh không bị xóa.
    *   Khi chọn "Đồng ý": Hệ thống hiển thị thông báo "Xóa thành công", học sinh biến mất khỏi danh sách.

##### `QUAN-20260529-0943-TC13`: Xóa học sinh đang có dữ liệu ràng buộc (Edge Case)
*   **Mô tả:** Đảm bảo tính toàn vẹn dữ liệu (Data Integrity). Không cho phép xóa học sinh đã có điểm số học tập hoặc lịch sử đóng học phí.
*   **Các bước thực hiện:**
    1. Chọn học sinh `Trần Văn B` (học sinh này đã có bảng điểm học kỳ 1 trong database).
    2. Click "Xóa" -> Click "Đồng ý".
*   **Kết quả mong đợi:** Hệ thống không cho xóa và hiển thị cảnh báo: "Không thể xóa học sinh này do đã tồn tại dữ liệu học tập/điểm số liên quan."

---

#### V. CHỨC NĂNG: IMPORT/EXPORT

##### `QUAN-20260529-0943-TC14`: Xuất file Excel danh sách học sinh (Happy Case)
*   **Mô tả:** Kiểm tra tính năng Export danh sách học sinh ra file excel.
*   **Các bước thực hiện:**
    1. Click nút "Xuất Excel" (Export).
*   **Kết quả mong đợi:**
    *   Tải xuống file dạng `.xlsx` thành công.
    *   Mở file kiểm tra: Số lượng dòng trùng khớp với danh sách trên web, hiển thị đầy đủ các cột thông tin, không bị lỗi hiển thị tiếng Việt (UTF-8).

##### `QUAN-20260529-0943-TC15`: Import file Excel chứa dữ liệu lỗi (Edge Case)
*   **Mô tả:** Kiểm tra độ tin cậy của luồng Import dữ liệu từ file Excel.
*   **Các bước thực hiện:**
    1. Tạo 1 file excel theo template mẫu nhưng cố tình nhập sai dữ liệu:
        *   Dòng 1: Để trống Họ tên.
        *   Dòng 2: Ngày sinh nhập `01/01/2026` (Mới sinh).
        *   Dòng 3: SĐT phụ huynh chứa chữ cái.
    2. Click "Import" -> Chọn file excel này -> Click "Tải lên".
*   **Kết quả mong đợi:**
    *   Hệ thống không import các dòng lỗi vào DB.
    *   Hiển thị thông báo Import thất bại kèm báo cáo chi tiết dòng bị lỗi: "Dòng 1: Họ tên trống; Dòng 2: Tuổi không hợp lệ; Dòng 3: SĐT sai định dạng".