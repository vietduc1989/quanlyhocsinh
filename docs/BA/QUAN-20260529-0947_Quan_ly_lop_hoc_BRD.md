# BRD – QUAN-20260529-0947: Quản lý lớp học

**Hệ thống:** quanlyhocsinh  
**Phân hệ:** Quản lý Lớp học và Học sinh  
**Phiên bản:** V1.0  
**Ngày:** 2026-05-29  

---

## 1. Mục tiêu
### Mục tiêu chính
Xây dựng phân hệ Quản lý lớp học toàn diện thuộc hệ thống `quanlyhocsinh` của ONENET nhằm tự động hóa quy trình khởi tạo lớp học, phân bổ và chuyển lớp cho học sinh. Đảm bảo tính chính xác về mặt nghiệp vụ (giới hạn sĩ số tối đa, mã lớp duy nhất, không trùng lặp lớp học của học sinh tại một thời điểm) và hỗ trợ công tác thống kê, báo cáo số lượng học sinh theo lớp, theo khối cho Ban Giám Hiệu và bộ phận Học vụ.

---

## 2. Phạm vi
### 2.1 In Scope
*   **Quản lý thông tin lớp học:** Thêm mới, sửa thông tin lớp học (tên lớp, niên khóa, khối, giáo viên chủ nhiệm).
*   **Cơ chế định danh:** Tự động sinh và kiểm tra tính duy nhất của Mã lớp học (Ví dụ: LH-10A1-2026).
*   **Xóa lớp học:** Áp dụng cơ chế xóa mềm (Soft delete) và chỉ cho phép xóa khi lớp học không còn học sinh.
*   **Quản lý học sinh trong lớp:** 
    *   Phân bổ học sinh mới vào lớp học.
    *   Chuyển đổi học sinh giữa các lớp học hiện có.
    *   Ràng buộc sĩ số tối đa 45 học sinh/lớp.
    *   Ràng buộc một học sinh chỉ thuộc duy nhất một lớp tại một thời điểm.
*   **Tra cứu và Thống kê:**
    *   Xem danh sách học sinh theo từng lớp.
    *   Thống kê tổng số học sinh theo từng lớp, theo từng khối học.

### 2.2 Out of Scope
*   Quản lý thông tin chi tiết hồ sơ lý lịch của giáo viên (chỉ liên kết thông tin cơ bản để làm Giáo viên chủ nhiệm).
*   Quản lý điểm số, học bạ, kết quả rèn luyện của học sinh.
*   Quản lý thời khóa biểu, lịch học và phòng học vật lý.

---

## 3. Nhóm người dùng
*   **Quản trị viên hệ thống (Admin) / Nhân viên Học vụ:** Người có toàn quyền thực hiện các thao tác cấu hình lớp học, thêm, sửa, xóa, phân bổ và chuyển lớp cho học sinh.
*   **Ban Giám Hiệu (BGH):** Xem danh sách học sinh, theo dõi số liệu thống kê tổng số học sinh theo lớp và theo khối để phục vụ công tác quản lý vĩ mô.
*   **Giáo viên Chủ nhiệm (GVCN):** Xem danh sách học sinh thuộc lớp mình được phân công chủ nhiệm.

---

## 4. User Flow nghiệp vụ

### Flow 1: Khởi tạo lớp học mới
1. Người dùng (Học vụ) chọn chức năng "Thêm mới lớp học".
2. Nhập các thông tin: Tên lớp, Niên khóa, Khối, chọn Giáo viên chủ nhiệm.
3. Hệ thống kiểm tra tính hợp lệ và tự động sinh Mã lớp (LH-[TênLớp]-[NiênKhóa]).
4. Hệ thống kiểm tra tính duy nhất của Mã lớp:
    * *Nếu đã tồn tại:* Thông báo lỗi trùng lặp mã lớp.
    * *Nếu hợp lệ:* Lưu thông tin lớp học vào hệ thống ở trạng thái hoạt động.

### Flow 2: Phân bổ và Chuyển lớp cho học sinh
1. Người dùng chọn chức năng "Phân bổ học sinh" hoặc "Chuyển lớp".
2. Chọn học sinh cần phân bổ/chuyển lớp và chọn Lớp học đích.
3. Hệ thống thực hiện kiểm tra các điều kiện nghiệp vụ:
    * Kiểm tra sĩ số hiện tại của lớp đích (Sĩ số hiện tại < 45).
    * Kiểm tra học sinh tại thời điểm hiện tại đã có lớp khác hay chưa (Đảm bảo 1 học sinh chỉ có 1 lớp).
4. Phê duyệt điều kiện:
    * *Nếu không thỏa mãn (Sĩ số đã đạt 45 hoặc lỗi ràng buộc):* Hệ thống hiển thị cảnh báo từ chối thao tác.
    * *Nếu thỏa mãn:* Cập nhật lớp học mới cho học sinh (Trường hợp chuyển lớp, hệ thống tự động rút học sinh khỏi lớp cũ và cập nhật sĩ số cả 2 lớp).

### Flow 3: Xóa lớp học (Soft delete)
1. Người dùng chọn lớp học cần xóa và nhấn "Xóa".
2. Hệ thống kiểm tra sĩ số của lớp học hiện tại:
    * *Nếu Sĩ số > 0 (vẫn còn học sinh trong lớp):* Hệ thống chặn thao tác và đưa ra thông báo: "Không thể xóa lớp học do vẫn còn học sinh trong lớp".
    * *Nếu Sĩ số = 0:* Hệ thống thực hiện Soft Delete (đánh dấu trạng thái lớp là 'Đã xóa', không xóa vật lý khỏi cơ sở dữ liệu để đảm bảo tính toàn vẹn dữ liệu lịch sử).

---

## 5. Functional Requirements

| ID | Tên yêu cầu | Mô tả chi tiết |
| :--- | :--- | :--- |
| **FR-01** | Thêm mới lớp học | Cho phép nhập: Tên lớp, Niên khóa, Khối, Giáo viên chủ nhiệm. Hệ thống tự động sinh Mã lớp duy nhất dựa trên quy tắc (Ví dụ: LH-10A1-2026). |
| **FR-02** | Sửa thông tin lớp học | Cho phép sửa thông tin: Tên lớp, Giáo viên chủ nhiệm, Niên khóa, Khối. Không cho phép sửa Mã lớp học sau khi đã tạo thành công. |
| **FR-03** | Xóa lớp học (Soft Delete) | Hệ thống chỉ cho phép xóa lớp học khi sĩ số lớp đó bằng 0. Khi xóa, chỉ chuyển trạng thái hoạt động sang "Đã xóa" (Soft Delete) chứ không xóa cứng trong DB. |
| **FR-04** | Phân bổ học sinh vào lớp | Cho phép chọn một hoặc nhiều học sinh chưa có lớp để gán vào một lớp cụ thể. Hệ thống tự động cập nhật sĩ số lớp. |
| **FR-05** | Chuyển học sinh giữa các lớp | Cho phép chuyển học sinh từ lớp cũ sang lớp mới. Hệ thống tự động giảm sĩ số lớp cũ và tăng sĩ số lớp mới. |
| **FR-06** | Xem danh sách học sinh theo lớp | Giao diện hiển thị danh sách tất cả học sinh thuộc một lớp được chọn, kèm theo sĩ số hiện tại của lớp đó. |
| **FR-07** | Thống kê số lượng học sinh | Cung cấp màn hình dashboard/báo cáo hiển thị: <br>- Tổng số học sinh theo từng lớp học. <br>- Tổng số học sinh theo từng khối học. |

---

## 6. Non-functional Requirements

### Performance
* Thời gian phản hồi cho các thao tác truy vấn danh sách học sinh theo lớp và tải trang thống kê phải nhỏ hơn 1.5 giây.
* Hệ thống phải xử lý đồng thời ít nhất 200 yêu cầu phân bổ/chuyển lớp học sinh trong cùng một thời điểm mà không xảy ra tình trạng tắc nghẽn dữ liệu (Lock/Deadlock database).

### Security
* Hệ thống phân quyền truy cập chặt chẽ (Role-Based Access Control):
    * Chỉ Admin/Học vụ được quyền Thêm, Sửa, Xóa, Phân bổ và Chuyển lớp.
    * Giáo viên chủ nhiệm chỉ được xem danh sách lớp của mình.
* Toàn bộ dữ liệu truyền tải giữa Client và Server phải được mã hóa qua giao thức bảo mật HTTPS.

### Availability
* Hệ thống hoạt động liên tục với độ sẵn sàng tối thiểu 99.9% (Uptime).
* Có cơ chế tự động sao lưu (Auto-backup) cơ sở dữ liệu hàng ngày vào khung giờ thấp điểm (02:00 AM).

---

## 7. Business Rules

| Mã BR | Tên quy định nghiệp vụ | Chi tiết quy định |
| :--- | :--- | :--- |
| **BR-01** | Mã lớp học duy nhất | Mỗi lớp học được tạo ra phải có một Mã lớp học duy nhất trên toàn hệ thống (Ví dụ: LH-10A1-2026). Hệ thống không chấp nhận trùng lặp mã này dưới mọi hình thức. |
| **BR-02** | Giới hạn sĩ số tối đa | Sĩ số tối đa cho phép của một lớp học là **45 học sinh**. Hệ thống sẽ khóa chức năng thêm/chuyển học sinh vào lớp học đó khi sĩ số đã đạt ngưỡng giới hạn 45. |
| **BR-03** | Điều kiện xóa lớp học | Chỉ cho phép thực hiện hành động xóa (Soft Delete) đối với lớp học có **sĩ số hiện tại bằng 0**. Nếu lớp học còn học sinh, thao tác xóa sẽ bị hệ thống từ chối. |
| **BR-04** | Tính duy nhất của lớp học học sinh | Tại một thời điểm xác định, một học sinh chỉ được phép ghi danh vào **duy nhất 1 lớp học** đang hoạt động. |

---

## 8. Integration Requirements
*   **Tích hợp Phân hệ Quản lý Học sinh:** Liên kết dữ liệu để lấy danh sách học sinh chưa được phân lớp hoặc để thực hiện chuyển thông tin lớp của học sinh.
*   **Tích hợp Phân hệ Quản lý Giáo viên:** Liên kết danh sách nhân sự giáo viên đang hoạt động để làm dữ liệu nguồn chọn Giáo viên chủ nhiệm cho lớp học.

---

## 9. Audit Trail
Hệ thống bắt buộc phải ghi lại Nhật ký hoạt động (Log) chi tiết cho các thao tác thay đổi dữ liệu trọng yếu bao gồm:
*   **Thao tác:** Tạo lớp mới, Sửa thông tin lớp, Xóa lớp, Phân bổ học sinh, Chuyển lớp cho học sinh.
*   **Thông tin ghi nhận:** 
    *   Tài khoản thực hiện (User ID).
    *   Thời gian thực hiện (Timestamp).
    *   Giá trị trước khi thay đổi (Old Value) và Giá trị sau khi thay đổi (New Value) đối với các hành động Sửa/Chuyển lớp.
    *   Địa chỉ IP thực hiện thao tác.

---

## 10. KPI theo dõi
*   **Tỷ lệ sai số sĩ số:** 0% (Không có bất kỳ lớp học nào vượt quá sĩ số 45 học sinh).
*   **Tỷ lệ trùng lặp lớp học:** 0% (Không có học sinh nào bị ghi nhận thuộc 2 lớp trở lên trong cùng một thời điểm).
*   **Thời gian hoàn thành thao tác nghiệp vụ:** Thời gian trung bình để Nhân viên Học vụ hoàn tất việc tạo mới một lớp học hoặc chuyển lớp cho một học sinh < 30 giây.