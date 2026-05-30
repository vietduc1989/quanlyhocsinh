Chào bạn,

Với vai trò là BA cấp cao của hệ thống ONENET, tôi đã tiếp nhận yêu cầu và tiến hành phân tích nghiệp vụ cho tính năng "Quản lý Lớp học" (QUAN-20260530-2302). Dưới đây là tài liệu BRD chi tiết theo cấu trúc chuẩn và tuân thủ các quy định đã đặt ra.

---

# Business Requirements Document (BRD)

**Mã tính năng:** QUAN-20260530-2302
**Tên tính năng:** Quản lý Lớp học
**Dự án:** quanlyhocsinh
**Ngày tạo:** 2026-05-30
**Phiên bản:** 1.0
**Trạng thái:** Hoàn thành

---

## 1. Giới thiệu (Introduction)

### 1.1 Mục đích (Purpose)
Tài liệu này mô tả chi tiết các yêu cầu nghiệp vụ cho tính năng "Quản lý Lớp học" trong hệ thống `quanlyhocsinh`. Mục đích là cung cấp một bản mô tả rõ ràng, đầy đủ và nhất quán về các chức năng cần thiết, quy tắc nghiệp vụ, và tiêu chí chấp nhận, nhằm làm cơ sở cho quá trình phát triển và kiểm thử phần mềm.

### 1.2 Phạm vi (Scope)
Tính năng "Quản lý Lớp học" sẽ bao gồm các khả năng quản lý thông tin của các lớp học trong nhà trường, cụ thể là:
*   Xem danh sách các lớp học.
*   Tìm kiếm và phân trang danh sách lớp học.
*   Thêm mới một lớp học.
*   Cập nhật thông tin của một lớp học hiện có.
*   Xóa một lớp học.
*   Thực hiện các validation cần thiết trên dữ liệu lớp học.

Phạm vi **không** bao gồm: quản lý thời khóa biểu, quản lý điểm số học sinh theo lớp, hoặc phân công học sinh vào lớp (chỉ xem sĩ số hiện tại).

### 1.3 Thuật ngữ & Viết tắt (Glossary)
| Thuật ngữ / Viết tắt | Diễn giải |
| :------------------ | :-------- |
| **BRD**             | Business Requirements Document - Tài liệu yêu cầu nghiệp vụ |
| **FR**              | Functional Requirement - Yêu cầu chức năng |
| **BR**              | Business Rule - Quy tắc nghiệp vụ |
| **CRUD**            | Create, Read, Update, Delete - Các thao tác cơ bản trên dữ liệu |
| **Lớp học**         | Đơn vị tổ chức học tập trong nhà trường, bao gồm các học sinh và có thể có giáo viên chủ nhiệm. |
| **Mã lớp**          | Mã định danh duy nhất cho mỗi lớp học (ví dụ: 10A1, 11B2). |
| **Tên lớp**         | Tên gọi đầy đủ của lớp học (ví dụ: Lớp 10A1, Lớp Khoa học Tự nhiên 12). |
| **Niên khóa**       | Khoảng thời gian một năm học (ví dụ: 2023-2024). |
| **Sĩ số**           | Số lượng học sinh hiện tại trong một lớp học. |
| **GVCN**            | Giáo viên chủ nhiệm. |
| **Validation**      | Quá trình kiểm tra tính hợp lệ của dữ liệu đầu vào. |
| **Phân trang**      | Chia danh sách dữ liệu thành các trang nhỏ để dễ quản lý và tải nhanh hơn. |

---

## 2. Bối cảnh nghiệp vụ (Business Context)

### 2.1 Nhu cầu nghiệp vụ / Vấn đề (Business Need / Problem Statement)
Hiện tại, hệ thống `quanlyhocsinh` chưa có một module chuyên biệt và hiệu quả để quản lý thông tin các lớp học. Việc thiếu vắng chức năng này dẫn đến:
*   Khó khăn trong việc tổ chức và sắp xếp học sinh vào các lớp.
*   Thiếu thông tin tổng quan về các lớp học hiện có, sĩ số và giáo viên chủ nhiệm.
*   Dữ liệu lớp học có thể bị phân mảnh hoặc không nhất quán nếu quản lý thủ công bên ngoài hệ thống.
*   Gây trở ngại cho các module khác của hệ thống cần tham chiếu thông tin lớp học (ví dụ: quản lý điểm, quản lý học sinh).

### 2.2 Mục tiêu nghiệp vụ (Business Goals)
*   Cung cấp một phương tiện tập trung để quản lý tất cả thông tin liên quan đến lớp học.
*   Đảm bảo tính chính xác và nhất quán của dữ liệu lớp học thông qua các quy tắc validation chặt chẽ.
*   Nâng cao hiệu quả quản lý hành chính cho nhà trường bằng cách tự động hóa các thao tác CRUD.
*   Cải thiện trải nghiệm người dùng bằng cách cung cấp giao diện trực quan và chức năng tìm kiếm, phân trang linh hoạt.
*   Tạo nền tảng dữ liệu vững chắc cho việc phát triển các tính năng khác trong tương lai (ví dụ: quản lý thời khóa biểu, phân công học sinh).

---

## 3. Các bên liên quan (Stakeholders)

*   **Quản trị viên hệ thống:** Người trực tiếp sử dụng tính năng để cấu hình và quản lý dữ liệu lớp học.
*   **Ban giám hiệu:** Người cần xem tổng quan về tình hình các lớp học.
*   **Giáo viên:** Người có thể được gán làm giáo viên chủ nhiệm và cần truy cập thông tin lớp mình phụ trách (khả năng xem).
*   **Nhóm phát triển & Kiểm thử:** Nhóm sẽ xây dựng và đảm bảo chất lượng của tính năng này.

---

## 4. Yêu cầu chức năng (Functional Requirements - FR)

**Actor chính:** Quản trị viên hệ thống

### 4.1. Quản lý chung danh sách Lớp học

**QUAN-20260530-2302-FR01: Xem danh sách Lớp học**
*   **Mô tả:** Hệ thống phải cho phép người dùng xem danh sách tất cả các lớp học hiện có trong hệ thống.
*   **Acceptance Criteria:**
    *   [AC-FR01.1] Hệ thống hiển thị danh sách lớp học dưới dạng bảng, bao gồm các cột thông tin: "Mã lớp", "Tên lớp", "Niên khóa", "Sĩ số (hiện tại)", "Giáo viên chủ nhiệm".
    *   [AC-FR01.2] Sĩ số (hiện tại) được hiển thị là tổng số học sinh đang được gán vào lớp học đó.
    *   [AC-FR01.3] Danh sách lớp học mặc định được sắp xếp theo "Mã lớp" tăng dần.
    *   [AC-FR01.4] Mỗi dòng trong danh sách phải có các hành động để thực hiện "Xem chi tiết", "Chỉnh sửa", "Xóa" lớp học tương ứng.

**QUAN-20260530-2302-FR02: Phân trang danh sách Lớp học**
*   **Mô tả:** Hệ thống phải hỗ trợ phân trang cho danh sách lớp học khi số lượng lớp vượt quá một ngưỡng nhất định.
*   **Acceptance Criteria:**
    *   [AC-FR02.1] Người dùng có thể chọn số lượng bản ghi hiển thị trên mỗi trang (ví dụ: 10, 25, 50, 100).
    *   [AC-FR02.2] Hệ thống hiển thị thông tin về tổng số lớp học, số trang hiện tại và tổng số trang.
    *   [AC-FR02.3] Người dùng có thể điều hướng qua lại giữa các trang (trang đầu, trang cuối, trang trước, trang sau, nhập số trang).

**QUAN-20260530-2302-FR03: Tìm kiếm Lớp học**
*   **Mô tả:** Hệ thống phải cho phép người dùng tìm kiếm lớp học dựa trên các tiêu chí nhất định.
*   **Acceptance Criteria:**
    *   [AC-FR03.1] Người dùng có thể nhập từ khóa tìm kiếm vào một trường tìm kiếm chung hoặc các trường tìm kiếm cụ thể.
    *   [AC-FR03.2] Hệ thống hỗ trợ tìm kiếm theo "Mã lớp", "Tên lớp", "Niên khóa", "Tên giáo viên chủ nhiệm".
    *   [AC-FR03.3] Kết quả tìm kiếm phải hiển thị danh sách các lớp học phù hợp với tiêu chí và được phân trang theo yêu cầu QUAN-20260530-2302-FR02.
    *   [AC-FR03.4] Chức năng tìm kiếm không phân biệt chữ hoa, chữ thường.

### 4.2. Thêm mới Lớp học

**QUAN-20260530-2302-FR04: Tạo Lớp học mới**
*   **Mô tả:** Hệ thống phải cho phép người dùng tạo một lớp học mới.
*   **Acceptance Criteria:**
    *   [AC-FR04.1] Hệ thống cung cấp một giao diện (form) để người dùng nhập các thông tin sau: Mã lớp, Tên lớp, Niên khóa, Giáo viên chủ nhiệm (chọn từ danh sách giáo viên hiện có trong hệ thống).
    *   [AC-FR04.2] Tất cả các quy tắc nghiệp vụ (BR) liên quan đến validation phải được áp dụng khi người dùng gửi form.
    *   [AC-FR04.3] Sau khi tạo thành công, lớp học mới sẽ xuất hiện trong danh sách lớp học và hệ thống hiển thị thông báo thành công.
    *   [AC-FR04.4] Nếu có lỗi validation, hệ thống phải hiển thị thông báo lỗi rõ ràng cho từng trường bị lỗi.

### 4.3. Cập nhật Lớp học

**QUAN-20260530-2302-FR05: Cập nhật thông tin Lớp học**
*   **Mô tả:** Hệ thống phải cho phép người dùng chỉnh sửa thông tin của một lớp học hiện có.
*   **Acceptance Criteria:**
    *   [AC-FR05.1] Khi chọn chức năng "Chỉnh sửa", hệ thống hiển thị giao diện chỉnh sửa với thông tin hiện tại của lớp học được điền sẵn vào các trường tương ứng.
    *   [AC-FR05.2] Người dùng có thể chỉnh sửa "Tên lớp", "Niên khóa", "Giáo viên chủ nhiệm".
    *   [AC-FR05.3] Trường "Mã lớp" không được phép chỉnh sửa sau khi lớp học đã được tạo.
    *   [AC-FR05.4] Tất cả các quy tắc nghiệp vụ (BR) liên quan đến validation phải được áp dụng khi người dùng gửi form cập nhật.
    *   [AC-FR05.5] Sau khi cập nhật thành công, thông tin mới của lớp học phải được phản ánh trong hệ thống và hiển thị thông báo thành công.
    *   [AC-FR05.6] Nếu có lỗi validation, hệ thống phải hiển thị thông báo lỗi rõ ràng cho từng trường bị lỗi.

### 4.4. Xóa Lớp học

**QUAN-20260530-2302-FR06: Xóa Lớp học**
*   **Mô tả:** Hệ thống phải cho phép người dùng xóa một lớp học khỏi danh sách.
*   **Acceptance Criteria:**
    *   [AC-FR06.1] Khi người dùng chọn chức năng "Xóa", hệ thống phải hiển thị một hộp thoại xác nhận với nội dung rõ ràng (ví dụ: "Bạn có chắc chắn muốn xóa lớp học [Tên lớp] không?").
    *   [AC-FR06.2] Nếu lớp học không có bất kỳ liên kết dữ liệu nào với học sinh hoặc các module khác, hệ thống sẽ thực hiện xóa vĩnh viễn và hiển thị thông báo thành công.
    *   [AC-FR06.3] Sau khi xóa thành công, lớp học không còn xuất hiện trong danh sách.

**QUAN-20260530-2302-FR07: Ngăn chặn xóa Lớp học có liên kết dữ liệu**
*   **Mô tả:** Hệ thống phải ngăn chặn việc xóa một lớp học nếu nó có liên kết dữ liệu quan trọng với học sinh hoặc các thực thể khác.
*   **Acceptance Criteria:**
    *   [AC-FR07.1] Nếu lớp học có ít nhất một học sinh đang theo học (đang được gán vào lớp đó), hệ thống sẽ từ chối thao tác xóa và hiển thị thông báo lỗi rõ ràng (ví dụ: "Không thể xóa lớp học '10A1' vì hiện có 25 học sinh đang theo học. Vui lòng chuyển hoặc xóa học sinh trước khi thực hiện.").
    *   [AC-FR07.2] Nếu lớp học được gán làm chủ nhiệm cho một giáo viên đang hoạt động, hệ thống sẽ từ chối thao tác xóa và hiển thị thông báo lỗi (ví dụ: "Không thể xóa lớp học '12C3' vì đang được phân công cho Giáo viên Nguyễn Văn A. Vui lòng thay đổi Giáo viên chủ nhiệm trước khi xóa.").

---

## 5. Quy tắc nghiệp vụ (Business Rules - BR)

**QUAN-20260530-2302-BR01: Quy tắc về Mã lớp**
*   **Mô tả:** Mã lớp là trường bắt buộc, phải là duy nhất trong hệ thống và có độ dài tối đa 20 ký tự.
*   **Ví dụ:**
    *   Hợp lệ: `10A1`, `11B2`, `L12C`.
    *   Không hợp lệ:
        *   Để trống: `""` (Lỗi: "Mã lớp không được để trống.")
        *   Trùng lặp: Nhập `10A1` khi `10A1` đã tồn tại (Lỗi: "Mã lớp '10A1' đã tồn tại.")
        *   Vượt quá ký tự: `LOP-10A1-K2023-2024-HK1` (25 ký tự) (Lỗi: "Mã lớp không được vượt quá 20 ký tự.")

**QUAN-20260530-2302-BR02: Quy tắc về Tên lớp**
*   **Mô tả:** Tên lớp là trường bắt buộc và có độ dài tối đa 50 ký tự.
*   **Ví dụ:**
    *   Hợp lệ: `Lớp 10A1`, `Lớp Khoa học Tự nhiên 12`.
    *   Không hợp lệ:
        *   Để trống: `""` (Lỗi: "Tên lớp không được để trống.")
        *   Vượt quá ký tự: `Lớp Chuyên Toán 12 - Năm học 2023-2024 - Khóa học đặc biệt` (60 ký tự) (Lỗi: "Tên lớp không được vượt quá 50 ký tự.")

**QUAN-20260530-2302-BR03: Quy tắc về Niên khóa**
*   **Mô tả:** Niên khóa là trường bắt buộc và phải có định dạng năm hợp lệ (ví dụ: YYYY hoặc YYYY-YYYY). Độ dài tối đa là 9 ký tự.
*   **Ví dụ:**
    *   Hợp lệ: `2023-2024`, `2026`.
    *   Không hợp lệ:
        *   Để trống: `""` (Lỗi: "Niên khóa không được để trống.")
        *   Sai định dạng: `Năm học mới`, `2023/2024` (Lỗi: "Niên khóa không đúng định dạng YYYY hoặc YYYY-YYYY.")
        *   Vượt quá ký tự: `2023-2024A` (10 ký tự) (Lỗi: "Niên khóa không được vượt quá 9 ký tự.")

**QUAN-20260530-2302-BR04: Quy tắc về Giáo viên chủ nhiệm**
*   **Mô tả:** Giáo viên chủ nhiệm là trường tùy chọn. Nếu được chọn, giá trị phải là ID của một giáo viên hiện có trong hệ thống. Một giáo viên có thể chủ nhiệm nhiều lớp (nếu quy định của trường cho phép, mặc định cho phép).
*   **Ví dụ:**
    *   Hợp lệ: Chọn "Nguyễn Thị B" từ danh sách gợi ý. Để trống trường này.
    *   Không hợp lệ:
        *   Nhập tên giáo viên không tồn tại trong danh sách (Lỗi: "Giáo viên chủ nhiệm không hợp lệ.")

---

## 6. Yêu cầu phi chức năng (Non-Functional Requirements - NFR)

*   **Hiệu suất (Performance):**
    *   Hệ thống phải tải danh sách lớp học (tối đa 1000 bản ghi) trong vòng không quá 3 giây.
    *   Các thao tác thêm mới, cập nhật, xóa lớp học phải hoàn thành trong vòng không quá 2 giây.
*   **Khả năng sử dụng (Usability):**
    *   Giao diện quản lý lớp học phải trực quan, dễ hiểu và dễ sử dụng cho người dùng phổ thông, yêu cầu ít hoặc không cần đào tạo.
    *   Các thông báo lỗi và thành công phải rõ ràng, dễ hiểu và hiển thị kịp thời.
*   **Bảo mật (Security):**
    *   Chỉ người dùng có vai trò "Quản trị viên hệ thống" mới có quyền truy cập và thực hiện các thao tác CRUD trên module "Quản lý Lớp học". Các vai trò khác (ví dụ: "Giáo viên") có thể chỉ có quyền xem.
*   **Tính toàn vẹn dữ liệu (Data Integrity):**
    *   Hệ thống phải đảm bảo tính toàn vẹn dữ liệu giữa Lớp học và Học sinh/Giáo viên thông qua các ràng buộc khóa ngoại và các quy tắc nghiệp vụ đã định.

---

## 7. Ràng buộc (Constraints)

*   **Tích hợp:** Tính năng này phải tích hợp với module quản lý "Học sinh" để cập nhật sĩ số và module "Giáo viên" để gán giáo viên chủ nhiệm.
*   **Công nghệ:** Phải được phát triển trên nền tảng công nghệ hiện có của hệ thống `quanlyhocsinh`.
*   **Dữ liệu:** Dữ liệu giáo viên và học sinh phải được quản lý thông qua các module riêng biệt và có sẵn để tham chiếu.

---

## 8. Giả định (Assumptions)

*   Module quản lý "Giáo viên" đã tồn tại và cung cấp danh sách giáo viên hoạt động để gán làm giáo viên chủ nhiệm.
*   Module quản lý "Học sinh" đã tồn tại và cho phép gán học sinh vào một lớp học, từ đó hệ thống có thể tính toán sĩ số lớp.
*   Mã lớp là một trường do người dùng nhập thủ công (không phải tự động sinh).

---

## 9. Tiêu chí chấp nhận tổng thể (Overall Acceptance Criteria)

*   Người dùng có vai trò "Quản trị viên hệ thống" có thể thực hiện tất cả các thao tác CRUD (thêm, xem, sửa, xóa) đối với lớp học một cách thành công.
*   Các chức năng tìm kiếm và phân trang hoạt động chính xác, hiệu quả và hiển thị dữ liệu đúng theo yêu cầu.
*   Tất cả các quy tắc nghiệp vụ (BR) được áp dụng và thực thi đúng đắn, đảm bảo tính toàn vẹn và hợp lệ của dữ liệu lớp học.
*   Hệ thống xử lý các trường hợp xóa lớp học có liên kết dữ liệu một cách an toàn, ngăn ngừa mất mát dữ liệu không mong muốn.
*   Giao diện người dùng thân thiện, trực quan và cung cấp phản hồi rõ ràng cho mọi thao tác.

---
**Lưu ý:** TUYỆT ĐỐI không được tạo, ghi, hoặc commit file vào thư mục gốc của ONENET.AgentFactory. Tất cả output được gửi qua API.