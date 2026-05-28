# BRD – Tính năng Đăng ký Học sinh mới

**Hệ thống:** Hệ thống Quản lý Học sinh trường học (ONENET-SMS)
**Phân hệ:** Quản lý Hồ sơ Học sinh (Student Profile Management)
**Phiên bản:** v1.0
**Ngày:** 24/10/2023

---

## 1. Mục tiêu
### Mục tiêu chính
*   Số hóa và chuẩn hóa quy trình tiếp nhận, đăng ký học sinh mới vào nhà trường.
*   Loại bỏ việc nhập liệu thủ công bằng giấy tờ, giảm thiểu tối đa sai sót thông tin.
*   Tự động hóa việc sinh mã học sinh và hỗ trợ xếp lớp nhanh chóng dựa trên dữ liệu chỉ tiêu của nhà trường.
*   Tạo cơ sở dữ liệu tập trung, sẵn sàng kết nối với các phân hệ khác như Tài chính (Học phí), Đào tạo (Điểm danh, Sổ liên lạc).

---

## 2. Phạm vi
### 2.1 In Scope
*   **Giao diện đăng ký:** Thiết kế form nhập liệu trực quan cho nhân viên học vụ/tuyển sinh.
*   **Thông tin cá nhân:** Thu thập thông tin chi tiết của học sinh (Họ tên, Ngày sinh, Giới tính, Quê quán, Địa chỉ, Ảnh chân dung, diện chính sách...).
*   **Thông tin phụ huynh:** Thu thập thông tin của ít nhất một người giám hộ (Họ tên, Số điện thoại, Email, Mối quan hệ).
*   **Phân lớp học:** Cho phép chọn Năm học, Khối và Lớp học cụ thể cho học sinh ngay khi đăng ký.
*   **Lưu trữ hồ sơ số:** Cho phép đính kèm các tài liệu scan (Giấy khai sinh, Học bạ cấp dưới, Giấy tờ ưu tiên).
*   **Sinh mã tự động:** Hệ thống tự động sinh Mã học sinh (Student ID) duy nhất theo quy chuẩn định sẵn ngay sau khi lưu hồ sơ thành công.

### 2.2 Out of Scope
*   **Đăng ký trực tuyến dành cho phụ huynh:** Tính năng này chỉ phục vụ nội bộ (Nhân viên Tuyển sinh/Admin của trường nhập liệu).
*   **Đóng phí nhập học:** Quy trình thu học phí và lệ phí nhập học sẽ do Phân hệ Tài chính xử lý.
*   **Xếp lớp tự động số lượng lớn (Mass Auto-assignment):** Chỉ thực hiện phân lớp thủ công từng học sinh khi đăng ký. Tính năng xếp lớp hàng loạt bằng thuật toán sẽ phát triển ở phase sau.

---

## 3. Nhóm người dùng
*   **Cán bộ Tuyển sinh (Admissions Officer):** Người trực tiếp tiếp nhận hồ sơ giấy từ phụ huynh và nhập liệu vào hệ thống.
*   **Quản trị viên Học vụ (Academic Administrator):** Người kiểm tra, phê duyệt hồ sơ, thực hiện điều chỉnh lớp học hoặc thông tin học sinh khi có sai sót.
*   **Ban Giám Hiệu (School Board):** Người xem báo cáo số lượng học sinh mới đăng ký thành công (Quyền xem dữ liệu - Read-only).

---

## 4. User Flow nghiệp vụ

1.  **Bắt đầu:** Cán bộ tuyển sinh đăng nhập hệ thống ONENET-SMS, truy cập phân hệ *Quản lý Hồ sơ*, chọn tính năng *Đăng ký Học sinh mới*.
2.  **Bước 1: Nhập thông tin học sinh:** Hệ thống hiển thị form nhập thông tin cá nhân. Người dùng điền đầy đủ thông tin bắt buộc và tải lên ảnh chân dung.
3.  **Bước 2: Nhập thông tin phụ huynh/người giám hộ:** Người dùng nhập thông tin liên hệ của Cha, Mẹ hoặc Người giám hộ hợp pháp.
4.  **Bước 3: Chọn lớp học:** Người dùng chọn Năm học -> Khối -> Chọn Lớp học còn chỉ tiêu từ danh sách thả xuống (Dropdown).
5.  **Bước 4: Đính kèm tài liệu:** Upload các bản scan giấy khai sinh, học bạ (nếu có).
6.  **Bước 5: Kiểm tra và Lưu:** Người dùng nhấn nút "Lưu hồ sơ".
7.  **Hệ thống xử lý:**
    *   Hệ thống kiểm tra tính hợp lệ của dữ liệu (Validate dữ liệu trống, định dạng SĐT, Email, tuổi học sinh...).
    *   Nếu dữ liệu hợp lệ: Hệ thống tự động sinh Mã học sinh, lưu thông tin vào CSDL, cập nhật sĩ số lớp học và hiển thị thông báo "Đăng ký thành công".
    *   Nếu dữ liệu không hợp lệ: Hiển thị thông báo lỗi chi tiết tại các trường thông tin sai lệch để người dùng sửa đổi.
8.  **Kết thúc:** Hồ sơ học sinh được tạo mới với trạng thái "Đang học" (hoặc "Chờ nhập học").

---

## 5. Functional Requirements

| ID | Tên chức năng | Mô tả chi tiết | Loại yêu cầu |
| :--- | :--- | :--- | :--- |
| **FR-01** | Nhập Thông tin Cá nhân | Cho phép người dùng nhập các thông tin: Họ và tên (chuyển đổi viết hoa tự động), Ngày sinh (Date picker), Giới tính (Radio button), Dân tộc, Quốc tịch, Địa chỉ thường trú, Địa chỉ tạm trú. Cho phép tải lên ảnh thẻ định dạng .jpg, .png (tối đa 2MB). | Bắt buộc |
| **FR-02** | Nhập Thông tin Phụ huynh | Cho phép nhập thông tin của Cha, Mẹ, hoặc Người giám hộ bao gồm: Họ tên, Số điện thoại (10 số), Email, Nghề nghiệp. Bắt buộc phải nhập thông tin của ít nhất 1 người giám hộ trực tiếp. | Bắt buộc |
| **FR-03** | Phân lớp học trực tiếp | Hệ thống hiển thị danh sách lớp học dựa trên Năm học và Khối đã chọn. Hiển thị sĩ số hiện tại của lớp (Ví dụ: 35/40) để người dùng cân nhắc xếp lớp. | Bắt buộc |
| **FR-04** | Đính kèm Hồ sơ đính kèm | Cho phép tải lên các tệp tài liệu hỗ trợ (Giấy khai sinh, học bạ cũ...). Định dạng hỗ trợ: PDF, JPG, PNG. Dung lượng tối đa: 5MB/file. | Cải tiến |
| **FR-05** | Tự động sinh Mã học sinh | Khi nhấn lưu thành công, hệ thống tự động sinh mã học sinh theo định dạng: `HS + [2 số cuối của năm nhập học] + [5 số tự tăng]`. Ví dụ: `HS2300001`. | Bắt buộc |
| **FR-06** | Validate dữ liệu thời gian thực | - Kiểm tra định dạng Email hợp lệ.<br>- Kiểm tra số điện thoại phụ huynh phải đúng 10 chữ số.<br>- Kiểm tra ngày sinh của học sinh phải phù hợp với độ tuổi quy định của Khối lớp đăng ký. | Bắt buộc |
| **FR-07** | Xem trước và In Phiếu đăng ký | Cho phép người dùng xem lại toàn bộ thông tin đã nhập dưới dạng biểu mẫu chuẩn của trường và in ra file PDF hoặc máy in vật lý để phụ huynh ký xác nhận. | Cải tiến |

---

## 6. Non-functional Requirements

### Performance
*   **Thời gian phản hồi:** Thời gian lưu dữ liệu và sinh mã học sinh không quá 2 giây kể từ khi nhấn nút "Lưu".
*   **Tải trang:** Giao diện Form đăng ký phải tải hoàn tất trong vòng dưới 1.5 giây trong điều kiện mạng bình thường.
*   **Tải đồng thời:** Hệ thống hỗ trợ tối thiểu 200 người dùng thực hiện đăng ký học sinh cùng một thời điểm vào mùa tuyển sinh cao điểm mà không bị nghẽn.

### Security
*   **Phân quyền (RBAC):** Chỉ có tài khoản thuộc nhóm "Cán bộ Tuyển sinh", "Quản trị Học vụ" mới có quyền Thêm mới/Chỉnh sửa hồ sơ. Tài khoản "Ban Giám Hiệu" chỉ có quyền Xem.
*   **Mã hóa dữ liệu:** Các thông tin nhạy cảm như Số điện thoại, Email của phụ huynh phải được mã hóa khi lưu trữ trong cơ sở dữ liệu để tránh rò rỉ thông tin cá nhân.
*   **Xác thực:** Mọi yêu cầu ghi nhận dữ liệu phải đi kèm Access Token hợp lệ của phiên đăng nhập.

### Availability
*   **Độ sẵn sàng:** Hệ thống hoạt động liên tục 24/7 với tỷ lệ uptime tối thiểu là 99.9%.
*   **Sao lưu dữ liệu:** Dữ liệu hồ sơ học sinh phải được sao lưu tự động (Backup) định kỳ hàng ngày vào lúc 23h00.

---

## 7. Business Rules

| ID | Quy tắc nghiệp vụ | Mô tả quy tắc |
| :--- | :--- | :--- |
| **BR-01** | Giới hạn độ tuổi tuyển sinh | Học sinh đăng ký vào Khối 1 phải đủ 6 tuổi tính theo năm dương lịch của năm tuyển sinh (Ví dụ: Tuyển sinh năm 2023 thì học sinh phải sinh năm 2017). |
| **BR-02** | Giới hạn Sĩ số tối đa | Không cho phép phân học sinh vào lớp đã đạt sĩ số tối đa theo quy định của nhà trường (Mặc định là 40 học sinh/lớp, có thể cấu hình lại bởi Quản trị viên). |
| **BR-03** | Duy nhất của Mã học sinh | Mỗi học sinh chỉ có một Mã duy nhất trên toàn hệ thống và không được phép thay đổi mã này dưới bất kỳ hình thức nào sau khi đã khởi tạo thành công. |
| **BR-04** | Ràng buộc liên hệ | Một số điện thoại phụ huynh không được đăng ký quá 3 học sinh trên hệ thống (trừ trường hợp anh chị em ruột được cấu hình đặc biệt). |

---

## 8. Integration Requirements
*   **Tích hợp Phân hệ Tài chính (Billing/Finance Module):** Ngay sau khi học sinh được đăng ký thành công trên hệ thống, thông tin Học sinh và Lớp học sẽ được đồng bộ sang Phân hệ Tài chính để tự động khởi tạo danh mục các khoản phí cần đóng (Học phí, Phí bán trú, Đồng phục, v.v.) theo đúng Khối/Lớp của học sinh đó.
*   **Tích hợp Dịch vụ SMS/Email Gateway:** Gửi tin nhắn SMS/Email tự động thông báo "Đăng ký nhập học thành công" kèm theo Mã học sinh và thông tin lớp học đến số điện thoại/email của phụ huynh ngay khi hồ sơ được lưu thành công.

---

## 9. Audit Trail
Hệ thống bắt buộc phải ghi lại nhật ký (Log) đối với tất cả các hành động tác động đến hồ sơ học sinh bao gồm:
*   **Ai thực hiện:** Tên tài khoản (Username) và Địa chỉ IP của người dùng.
*   **Thời gian:** Ngày, giờ, phút, giây chính xác phát sinh hành động.
*   **Hành động:** Tạo mới hồ sơ (Create), Cập nhật thông tin (Update), Xóa/Hủy hồ sơ (Delete).
*   **Chi tiết thay đổi:** Ghi nhận giá trị cũ (Old Value) và giá trị mới (New Value) đối với các trường thông tin được cập nhật để phục vụ công tác đối soát khi xảy ra tranh chấp dữ liệu.

---

## 10. KPI theo dõi
*   **Thời gian hoàn thành 1 hồ sơ:** Thời gian trung bình để cán bộ tuyển sinh nhập liệu hoàn thành 1 hồ sơ học sinh mới (Mục tiêu: < 3 phút/hồ sơ).
*   **Tỷ lệ sai sót thông tin đầu vào:** Số lượng hồ sơ cần chỉnh sửa lại thông tin sau khi đã lưu thành công (Mục tiêu: < 1%).
*   **Tỷ lệ phân lớp thành công:** Tỷ lệ học sinh mới có lớp học ngay khi hoàn thành đăng ký hồ sơ (Mục tiêu: 100%).