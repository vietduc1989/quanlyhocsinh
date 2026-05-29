# BRD – Quản lý điểm thi cuối kỳ (QUAN-20260529-0952)

**Hệ thống:** Hệ thống quản lý học sinh (quanlyhocsinh)  
**Phân hệ:** Phân hệ Quản lý Học tập & Điểm số  
**Phiên bản:** v1.0  
**Ngày:** 2026-05-29  

---

## 1. Mục tiêu
### Mục tiêu chính
*   Số hóa và tự động hóa toàn bộ quy trình nhập, sửa, tính toán, xếp loại và phê duyệt điểm thi cuối kỳ của học sinh tại các trường học.
*   Đảm bảo tính chính xác, bảo mật và tính toàn vẹn của dữ liệu điểm số thông qua cơ chế phê duyệt (Approve) của TechLead và lưu vết lịch sử (Audit Trail).
*   Cung cấp công cụ thống kê trực quan và xuất báo cáo phục vụ công tác quản lý của Ban giám hiệu.

---

## 2. Phạm vi
### 2.1 In Scope
*   Chức năng nhập điểm thi cuối kỳ theo từng lớp học và từng môn học cụ thể.
*   Chức năng chỉnh sửa điểm thi đã nhập kèm theo yêu cầu bắt buộc ghi nhận lịch sử thay đổi (Audit Trail).
*   Chức năng xem bảng điểm cá nhân (dành cho Học sinh/Phụ huynh) và xem bảng điểm của toàn lớp theo môn học (dành cho Giáo viên/TechLead).
*   Hệ thống tự động tính toán Điểm trung bình môn và Điểm trung bình học kỳ theo công thức quy định.
*   Hệ thống tự động xếp loại học lực của học sinh dựa trên Điểm trung bình học kỳ.
*   Chức năng kiểm tra tính hợp lệ của dữ liệu đầu vào (Validation): Điểm số từ 0 - 10, tối đa 1 chữ số thập phân.
*   Chức năng xuất bảng điểm của lớp/môn học ra định dạng file PDF.
*   Chức năng thống kê số lượng và tỷ lệ học sinh đạt loại Giỏi, Khá, Trung bình, Yếu theo từng lớp học.
*   Chức năng phê duyệt điểm của TechLead và cơ chế tự động khóa điểm (không cho phép chỉnh sửa) sau khi đã được duyệt.

### 2.2 Out of Scope
*   Quản lý điểm kiểm tra miệng, kiểm tra 15 phút, kiểm tra 1 tiết (chỉ tập trung vào điểm thi cuối kỳ và điểm trung bình cuối cùng).
*   Quy trình tiếp nhận và xử lý đơn phúc khảo điểm thi của học sinh.
*   Gửi thông báo điểm tự động qua SMS hoặc OTT cho Phụ huynh học sinh (tính năng này sẽ phát triển ở phase sau).

---

## 3. Nhóm người dùng
*   **Giáo viên bộ môn (Teacher):** Người trực tiếp nhập, sửa điểm thi cuối kỳ của lớp/môn mình phụ trách; xem bảng điểm lớp và gửi yêu cầu phê duyệt điểm lên TechLead.
*   **TechLead (Tổ trưởng chuyên môn/Quản trị viên):** Người kiểm tra, phê duyệt bảng điểm của các môn học và thực hiện khóa điểm. Có quyền mở khóa điểm trong trường hợp đặc biệt.
*   **Học sinh / Phụ huynh (Student/Parent):** Người tra cứu và xem bảng điểm cá nhân của học sinh đó.
*   **Ban giám hiệu (School Board):** Xem bảng điểm toàn trường, xem báo cáo thống kê tỷ lệ học lực và xuất file PDF để lưu trữ.

---

## 4. User Flow nghiệp vụ

```
[Giáo viên] ──> Chọn Lớp & Môn học ──> Nhập/Sửa điểm ──> [Hệ thống Validate]
                                                               │
     ┌───────────────── Điểm KHÔNG hợp lệ (Báo lỗi) <──────────┤ (Điểm từ 0-10, 1 số thập phân)
     │                                                         │
     │                                                         ▼ Điểm hợp lệ
     │                                                 [Lưu tạm bảng điểm]
     │                                                         │
     │                                                         ▼
     │                                                 Tự động tính ĐTB & Xếp loại
     │                                                         │
     │                                                         ▼
     │                                                 [Gửi yêu cầu phê duyệt]
     │                                                         │
     │                                                         ▼
[TechLead] <────────────────────────────────────────── Xem bảng điểm chờ duyệt
     │
     ├─ [Từ chối] ──> Mở lại quyền sửa cho Giáo viên (Yêu cầu nhập lại)
     │
     └─ [Phê duyệt] ──> [Hệ thống tự động khóa điểm] ──> (Không thể chỉnh sửa nữa)
                               │
                               ├─> [Học sinh/Phụ huynh] ──> Xem điểm cá nhân
                               │
                               └─> [Ban giám hiệu] ──> Xem Thống kê & Xuất PDF
```

---

## 5. Functional Requirements

| ID | Tên chức năng | Mô tả chi tiết | Người dùng |
| :--- | :--- | :--- | :--- |
| **FR-01** | Nhập điểm thi | Cho phép giáo viên chọn Lớp, chọn Môn học và hiển thị danh sách học sinh để nhập điểm thi cuối kỳ trực tiếp trên giao diện lưới (grid). | Giáo viên |
| **FR-02** | Sửa điểm & Ghi log | Cho phép sửa điểm thi của học sinh khi bảng điểm chưa bị khóa. Hệ thống bắt buộc người sửa nhập lý do và tự động ghi nhận vào log (người sửa, thời gian, giá trị cũ, giá trị mới). | Giáo viên |
| **FR-03** | Tra cứu điểm cá nhân | Cho phép học sinh/phụ huynh đăng nhập và xem bảng điểm cá nhân gồm: điểm thi cuối kỳ các môn, điểm trung bình môn, điểm trung bình học kỳ và xếp loại. | Học sinh, Phụ huynh |
| **FR-04** | Xem bảng điểm lớp | Hiển thị bảng điểm đầy đủ của toàn bộ học sinh trong lớp theo môn học được chọn, bao gồm điểm thi cuối kỳ, ĐTB môn và xếp loại tương ứng. | Giáo viên, TechLead, BGH |
| **FR-05** | Tự động tính điểm | Hệ thống tự động tính toán:<br>1. Điểm trung bình môn (ĐTB môn).<br>2. Điểm trung bình học kỳ (ĐTB học kỳ = Trung bình cộng của các ĐTB môn). | Hệ thống |
| **FR-06** | Tự động xếp loại | Tự động xếp loại học lực học sinh dựa vào ĐTB học kỳ:<br>- **Giỏi:** ĐTB học kỳ >= 8.0<br>- **Khá:** 6.5 <= ĐTB học kỳ < 8.0<br>- **Trung bình (TB):** 5.0 <= ĐTB học kỳ < 6.5<br>- **Yếu:** ĐTB học kỳ < 5.0 | Hệ thống |
| **FR-07** | Kiểm tra dữ liệu (Validate) | Ràng buộc dữ liệu khi nhập điểm: Chỉ chấp nhận số thập phân từ `0.0` đến `10.0`. Chặn và báo lỗi ngay trên màn hình nếu nhập sai định dạng hoặc quá 1 chữ số thập phân. | Hệ thống |
| **FR-08** | Xuất PDF | Hỗ trợ kết xuất bảng điểm của lớp theo môn học ra file định dạng PDF theo chuẩn biểu mẫu của trường học để ký duyệt và lưu trữ giấy. | Giáo viên, TechLead, BGH |
| **FR-09** | Thống kê học lực | Hiển thị biểu đồ và bảng số liệu thống kê số lượng & tỷ lệ (%) học sinh đạt loại Giỏi, Khá, TB, Yếu theo từng lớp học để BGH theo dõi chất lượng. | Ban giám hiệu, TechLead |
| **FR-10** | Phê duyệt & Khóa điểm | TechLead thực hiện duyệt bảng điểm. Sau khi click "Duyệt", hệ thống chuyển trạng thái bảng điểm sang "Đã khóa". Giáo viên không thể sửa điểm nữa trừ khi TechLead thực hiện "Mở khóa". | TechLead |

---

## 6. Non-functional Requirements

### Performance
*   Thời gian phản hồi khi lưu dữ liệu điểm của một lớp học (sĩ số tối đa 50 học sinh) không quá 2 giây.
*   Hệ thống tính toán Điểm trung bình và Xếp loại học lực của một lớp học hoàn tất dưới 1 giây sau khi bấm lưu.
*   Thời gian tạo và tải xuống file PDF bảng điểm không quá 3 giây.

### Security
*   Phân quyền truy cập nghiêm ngặt (Role-based Access Control - RBAC):
    *   Giáo viên chỉ được nhập/sửa điểm của lớp/môn mình được phân công giảng dạy.
    *   Học sinh chỉ được phép xem điểm của chính mình, không có quyền xem điểm học sinh khác.
    *   Chỉ TechLead mới có quyền duyệt và khóa/mở khóa điểm.
*   Mọi kết nối truyền tải dữ liệu điểm số phải được mã hóa qua giao thức HTTPS.

### Availability
*   Hệ thống đảm bảo tính sẵn sàng hoạt động (Uptime) tối thiểu 99.9% trong thời gian diễn ra kỳ thi và nhập điểm cuối kỳ.
*   Hệ thống có khả năng tự động sao lưu dữ liệu điểm (Backup) hàng ngày vào lúc 23:00.

---

## 7. Business Rules

*   **BR-1 (Ràng buộc nhập điểm):** Điểm thi cuối kỳ nhập vào phải là số thực nằm trong khoảng `[0.0, 10.0]` và chỉ được phép có tối đa 1 chữ số thập phân (Ví dụ: 8.5 là hợp lệ; 8.55 hoặc 11.0 là không hợp lệ).
*   **BR-2 (Quy tắc xếp loại học lực):**
    *   Điểm trung bình học kỳ (ĐTB HK) >= 8.0 $\rightarrow$ Xếp loại: **Giỏi**
    *   6.5 <= ĐTB HK < 8.0 $\rightarrow$ Xếp loại: **Khá**
    *   5.0 <= ĐTB HK < 6.5 $\rightarrow$ Xếp loại: **Trung bình**
    *   ĐTB HK < 5.0 $\rightarrow$ Xếp loại: **Yếu**
*   **BR-3 (Quy tắc Khóa điểm):** 
    *   Trạng thái bảng điểm gồm: `Mới tạo` $\rightarrow$ `Chờ duyệt` $\rightarrow$ `Đã duyệt & Khóa`.
    *   Khi bảng điểm ở trạng thái `Đã duyệt & Khóa`, chức năng "Chỉnh sửa điểm" của Giáo viên đối với lớp/môn đó sẽ bị vô hiệu hóa hoàn toàn (Read-only).
    *   Nếu phát hiện sai sót sau khi khóa, chỉ TechLead mới có quyền thực hiện hành động "Mở khóa điểm" (Unlock), yêu cầu ghi rõ lý do mở khóa trước khi hệ thống chuyển trạng thái về `Mới tạo` để Giáo viên chỉnh sửa lại.

---

## 8. Integration Requirements

*   **Tích hợp Phân hệ Quản lý Lớp học:** Đồng bộ danh sách lớp học, thông tin học sinh và danh sách giáo viên chủ nhiệm/giáo viên bộ môn theo thời gian thực.
*   **Tích hợp Phân hệ Quản lý Môn học:** Lấy dữ liệu danh mục môn học (Mã môn, Tên môn, Số tín chỉ/Số tiết) để làm cơ sở tính toán Điểm trung bình học kỳ (nếu có hệ số môn học).

---

## 9. Audit Trail

Hệ thống bắt buộc ghi nhận lại toàn bộ lịch sử chỉnh sửa điểm thi vào cơ sở dữ liệu nhật ký hệ thống (Audit Log) để phục vụ công tác thanh tra. Cấu trúc log ghi nhận gồm các thông tin:

*   **Thời gian:** Ngày giờ thực hiện thay đổi (dd/mm/yyyy hh:mm:ss).
*   **Tài khoản thực hiện:** Username/ID của người thực hiện chỉnh sửa (Giáo viên hoặc TechLead).
*   **Học sinh được sửa điểm:** ID học sinh, Tên học sinh.
*   **Môn học & Lớp học:** Tên môn học, Tên lớp học.
*   **Thông tin thay đổi:**
    *   Điểm thi cũ (Old Value).
    *   Điểm thi mới (New Value).
*   **Lý do chỉnh sửa:** Nội dung lý do bắt buộc do người sửa nhập vào (Ví dụ: "Nhập nhầm điểm", "Chấm phúc khảo tăng điểm").

---

## 10. KPI theo dõi

*   **Tỷ lệ nhập điểm đúng hạn:** % Giáo viên hoàn thành nhập điểm và gửi phê duyệt đúng thời hạn quy định của nhà trường (Target: >= 98%).
*   **Thời gian phê duyệt bảng điểm:** Thời gian trung bình từ lúc Giáo viên gửi yêu cầu phê duyệt đến khi TechLead thực hiện duyệt và khóa điểm (Target: <= 24 giờ làm việc).
*   **Tỷ lệ lỗi tính toán điểm:** Số lượng trường hợp học sinh bị tính sai ĐTB môn hoặc xếp loại học lực sai quy chuẩn (Target: 0%).
*   **Tần suất yêu cầu sửa điểm sau khóa:** Số lần TechLead phải mở khóa bảng điểm để sửa lại do sai sót của giáo viên (Target: <= 2% tổng số bảng điểm của toàn trường).