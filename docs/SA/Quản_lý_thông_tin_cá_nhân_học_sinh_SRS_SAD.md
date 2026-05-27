Chào bạn,

Dựa trên Tài liệu Phân tích Nghiệp vụ (BRD) đã cung cấp cho tính năng "Quản lý thông tin cá nhân học sinh" của hệ thống ONENET, tôi đã tiến hành thiết kế tài liệu Đặc tả Yêu cầu Phần mềm (SRS) và Kiến trúc Hệ thống (SAD) tuân thủ Tech Stack bắt buộc (PostgreSQL, .NET 10 Clean Architecture, React/Mantine UI).

Với vai trò là Solution Architect, tôi đã thực hiện kiểm tra schema hiện tại một cách giả định (do không có công cụ thực tế để truy vấn schema ONENET đang có) để đảm bảo không có thiết kế trùng lặp hoặc mâu thuẫn chính. Giả định rằng bảng `Students` với cấu trúc như dưới đây là mới hoặc không trùng lặp hoàn toàn với một bảng đã có sẵn cùng mục đích và các ràng buộc duy nhất. Nếu có một bảng `Students` đã tồn tại, chúng ta sẽ cần phải đánh giá lại để mở rộng hoặc điều chỉnh bảng đó thay vì tạo mới.

---

# TÀI LIỆU TỔNG HỢP SRS VÀ SAD CHO HỆ THỐNG ONENET

**Tính năng:** Quản lý thông tin cá nhân học sinh
**Ngày tạo:** 19/05/2024
**Người thiết kế:** [Tên Solution Architect - Tức là tôi]

---

## PHẦN I: ĐẶC TẢ YÊU CẦU PHẦN MỀM (SRS)

### 1. Giới thiệu

#### 1.1. Mục đích
Tài liệu này đặc tả các yêu cầu chức năng và phi chức năng cho tính năng "Quản lý thông tin cá nhân học sinh" trong hệ thống ONENET. Mục đích là cung cấp một cơ sở rõ ràng cho việc phát triển, kiểm thử và triển khai tính năng này, đảm bảo đáp ứng đầy đủ các yêu cầu nghiệp vụ đã được xác định trong BRD.

#### 1.2. Phạm vi sản phẩm
Tính năng này sẽ cho phép "Người quản lý học sinh" thực hiện các thao tác thêm mới, xem, sửa, xóa, tìm kiếm và lọc thông tin cá nhân của học sinh trong hệ thống.

#### 1.3. Định nghĩa và Viết tắt
*   **ONENET:** Tên hệ thống.
*   **BRD:** Business Requirements Document (Tài liệu Phân tích Nghiệp vụ).
*   **SRS:** Software Requirements Specification (Đặc tả Yêu cầu Phần mềm).
*   **SAD:** Software Architecture Document (Tài liệu Kiến trúc Hệ thống).
*   **BA:** Business Analyst.
*   **SA:** Solution Architect.
*   **UI:** User Interface (Giao diện người dùng).
*   **UX:** User Experience (Trải nghiệm người dùng).
*   **API:** Application Programming Interface.
*   **DB:** Database (Cơ sở dữ liệu).
*   **CRUD:** Create, Read, Update, Delete.

#### 1.4. Tài liệu tham khảo
*   Tài liệu Phân tích Yêu cầu Nghiệp vụ (BRD) - Tính năng Quản lý thông tin cá nhân học sinh, ngày 18/05/2024.

### 2. Mô tả tổng quan

#### 2.1. Quan điểm sản phẩm
Tính năng "Quản lý thông tin cá nhân học sinh" là một phần của hệ thống quản lý ONENET lớn hơn, cung cấp khả năng quản lý dữ liệu học sinh tập trung. Nó sẽ tương tác với người dùng thông qua giao diện web.

#### 2.2. Chức năng sản phẩm
Các chức năng chính bao gồm:
*   Thêm mới thông tin học sinh vào hệ thống.
*   Sửa đổi thông tin chi tiết của học sinh hiện có.
*   Xóa thông tin học sinh khỏi hệ thống.
*   Xem danh sách tất cả học sinh.
*   Tìm kiếm học sinh theo Mã học sinh hoặc Họ và tên.
*   Lọc học sinh theo Giới tính hoặc Lớp học.
*   Hỗ trợ phân trang và sắp xếp danh sách học sinh.

#### 2.3. Các lớp người dùng và đặc điểm
*   **Người quản lý học sinh:** Là người dùng có thẩm quyền được phép thực hiện tất cả các thao tác CRUD, tìm kiếm và lọc trên thông tin học sinh. Người này cần một giao diện trực quan, hiệu quả để quản lý lượng lớn dữ liệu học sinh.

#### 2.4. Môi trường vận hành
Hệ thống sẽ được triển khai trên môi trường web, có thể truy cập qua trình duyệt web phổ biến. Backend chạy trên .NET 10 và sử dụng PostgreSQL làm cơ sở dữ liệu.

#### 2.5. Các ràng buộc và phụ thuộc
*   **Công nghệ bắt buộc (Tech Stack):**
    *   **Cơ sở dữ liệu:** PostgreSQL.
    *   **Backend:** .NET 10 (Clean Architecture).
    *   **Frontend:** React (Mantine UI).
*   Phụ thuộc vào các dịch vụ hạ tầng cơ bản (mạng, máy chủ).
*   Yêu cầu về hiệu suất và bảo mật.

### 3. Yêu cầu cụ thể

#### 3.1. Yêu cầu chức năng (Functional Requirements)

**FR.001: Thêm học sinh mới**
*   **FR.001.1 - Hiển thị biểu mẫu nhập liệu:**
    *   Hệ thống phải hiển thị một biểu mẫu nhập liệu với các trường sau:
        *   **Họ và tên:** (Bắt buộc, kiểu chuỗi, tối đa 100 ký tự, không chứa ký tự đặc biệt ngoài khoảng trắng và dấu nháy đơn).
        *   **Ngày sinh:** (Bắt buộc, kiểu ngày, định dạng DD/MM/YYYY, phải là ngày trong quá khứ hoặc hiện tại).
        *   **Giới tính:** (Bắt buộc, chọn từ danh sách: Nam, Nữ, Khác).
        *   **Địa chỉ:** (Bắt buộc, kiểu chuỗi, tối đa 255 ký tự).
        *   **Số điện thoại phụ huynh:** (Bắt buộc, kiểu chuỗi số, định dạng số điện thoại Việt Nam hợp lệ (10 hoặc 11 chữ số), chỉ chứa các ký tự số).
        *   **Lớp học:** (Bắt buộc, kiểu chuỗi, tối đa 20 ký tự).
        *   **Mã học sinh:** (Bắt buộc, kiểu chuỗi, duy nhất, tối đa 20 ký tự, chỉ chứa chữ cái, số, gạch ngang '-', gạch dưới '_', không chứa khoảng trắng).
*   **FR.001.2 - Lưu thông tin học sinh thành công:**
    *   Khi tất cả thông tin hợp lệ được điền và người dùng nhấn "Lưu", hệ thống phải:
        *   Lưu thông tin học sinh mới vào cơ sở dữ liệu.
        *   Hiển thị thông báo "Thêm học sinh thành công."
        *   Chuyển hướng người dùng về trang danh sách học sinh hoặc hiển thị chi tiết học sinh vừa thêm.
*   **FR.001.3 - Xử lý nhập liệu thiếu/sai định dạng:**
    *   Khi người dùng nhấn "Lưu" với dữ liệu thiếu hoặc sai định dạng, hệ thống phải:
        *   Hiển thị thông báo lỗi rõ ràng bên cạnh từng trường bị lỗi.
        *   Không cho phép lưu thông tin.
*   **FR.001.4 - Xử lý Mã học sinh trùng lặp:**
    *   Khi người dùng nhấn "Lưu" với "Mã học sinh" đã tồn tại, hệ thống phải:
        *   Hiển thị thông báo lỗi "Mã học sinh [Mã học sinh đã nhập] đã tồn tại. Vui lòng chọn Mã học sinh khác."
        *   Không cho phép lưu thông tin.

**FR.002: Sửa thông tin học sinh**
*   **FR.002.1 - Hiển thị biểu mẫu chỉnh sửa với dữ liệu có sẵn:**
    *   Khi người dùng chọn chức năng "Sửa" cho một học sinh, hệ thống phải hiển thị một biểu mẫu với tất cả thông tin hiện tại của học sinh đó được điền sẵn.
    *   Trường "Mã học sinh" phải ở chế độ chỉ đọc (read-only).
    *   Các trường khác có thể chỉnh sửa.
*   **FR.002.2 - Cập nhật thông tin học sinh thành công:**
    *   Khi người dùng chỉnh sửa thông tin hợp lệ và nhấn "Cập nhật", hệ thống phải:
        *   Cập nhật thông tin học sinh trong cơ sở dữ liệu.
        *   Hiển thị thông báo "Cập nhật thông tin học sinh thành công."
        *   Chuyển hướng người dùng về trang danh sách học sinh hoặc hiển thị chi tiết học sinh vừa cập nhật.
*   **FR.002.3 - Xử lý nhập liệu thiếu/sai định dạng khi chỉnh sửa:**
    *   Tương tự FR.001.3, hệ thống phải hiển thị lỗi và không cho phép cập nhật nếu dữ liệu thiếu hoặc sai định dạng.

**FR.003: Xóa thông tin học sinh**
*   **FR.003.1 - Yêu cầu xác nhận trước khi xóa:**
    *   Khi người dùng chọn chức năng "Xóa", hệ thống phải hiển thị một hộp thoại xác nhận với nội dung "Bạn có chắc chắn muốn xóa học sinh [Tên Học Sinh] (Mã: [Mã Học Sinh]) không? Thao tác này không thể hoàn tác.".
*   **FR.003.2 - Xóa học sinh thành công:**
    *   Khi người dùng xác nhận xóa, hệ thống phải:
        *   Xóa thông tin học sinh đó khỏi cơ sở dữ liệu.
        *   Hiển thị thông báo "Xóa học sinh thành công."
        *   Đảm bảo học sinh đã xóa không còn xuất hiện trong danh sách.
*   **FR.003.3 - Hủy bỏ thao tác xóa:**
    *   Khi người dùng hủy bỏ thao tác xóa, hệ thống phải:
        *   Giữ nguyên thông tin học sinh trong hệ thống.
        *   Đóng hộp thoại xác nhận.
*   **FR.003.4 - Xử lý trường hợp không tìm thấy học sinh để xóa:**
    *   Nếu học sinh không tồn tại khi yêu cầu xóa, hệ thống phải hiển thị thông báo lỗi "Không tìm thấy học sinh để xóa."

**FR.004: Xem danh sách học sinh với tìm kiếm/lọc**
*   **FR.004.1 - Hiển thị danh sách học sinh mặc định:**
    *   Hệ thống phải hiển thị một bảng chứa danh sách tất cả học sinh hiện có khi truy cập chức năng.
    *   Mỗi hàng phải hiển thị: Mã học sinh, Họ và tên, Ngày sinh, Giới tính, Lớp học, Địa chỉ, Số điện thoại phụ huynh.
    *   Danh sách phải được sắp xếp mặc định theo Họ và tên (A-Z) hoặc Mã học sinh (tăng dần).
*   **FR.004.2 - Chức năng tìm kiếm theo từ khóa:**
    *   Hệ thống phải cung cấp trường tìm kiếm cho "Họ và tên" và "Mã học sinh".
    *   Khi nhập từ khóa, hệ thống phải lọc và hiển thị ngay lập tức (hoặc sau khi nhấn tìm kiếm) những học sinh có Họ và tên hoặc Mã học sinh chứa từ khóa (không phân biệt chữ hoa, chữ thường).
    *   Khi xóa từ khóa, danh sách phải trở về trạng thái trước đó (toàn bộ hoặc theo lọc).
*   **FR.004.3 - Chức năng lọc theo tiêu chí:**
    *   Hệ thống phải cung cấp bộ lọc cho "Giới tính" (Nam, Nữ, Khác) và "Lớp học" (danh sách các lớp học hiện có).
    *   Khi chọn giá trị lọc, hệ thống phải cập nhật danh sách học sinh phù hợp.
    *   Tùy chọn "Tất cả" hoặc bỏ chọn phải loại bỏ tiêu chí lọc.
*   **FR.004.4 - Kết hợp tìm kiếm và lọc:**
    *   Hệ thống phải cập nhật danh sách học sinh để hiển thị những học sinh thỏa mãn tất cả các tiêu chí tìm kiếm và lọc đang được áp dụng.
*   **FR.004.5 - Hỗ trợ phân trang và sắp xếp:**
    *   Hệ thống phải hỗ trợ phân trang (pagination) cho danh sách học sinh.
    *   Hệ thống phải cung cấp tùy chọn sắp xếp danh sách theo Họ và tên hoặc Mã học sinh (tăng dần/giảm dần).

#### 3.2. Yêu cầu giao diện bên ngoài

*   **Giao diện người dùng (UI):**
    *   Phải được xây dựng bằng React và Mantine UI.
    *   Cung cấp các biểu mẫu nhập liệu, bảng hiển thị dữ liệu, nút điều khiển, hộp thoại xác nhận, trường tìm kiếm và bộ lọc trực quan, dễ sử dụng.
    *   Thông báo phản hồi (thành công/lỗi) phải rõ ràng, hiển thị đúng vị trí.
*   **Giao diện phần mềm:**
    *   Backend sẽ cung cấp một RESTful API cho frontend để thực hiện các thao tác CRUD, tìm kiếm và lọc.
    *   Backend sẽ tương tác với cơ sở dữ liệu PostgreSQL thông qua ORM (Entity Framework Core).

#### 3.3. Yêu cầu phi chức năng (Non-Functional Requirements)

*   **Hiệu suất:**
    *   Thời gian tải danh sách học sinh (mặc định hoặc sau tìm kiếm/lọc) không quá 2 giây.
    *   Thời gian phản hồi cho các thao tác thêm, sửa, xóa học sinh không quá 1 giây.
    *   Hệ thống có khả năng xử lý đồng thời 50 người dùng mà không giảm hiệu suất đáng kể.
*   **Bảo mật:**
    *   Chỉ "Người quản lý học sinh" mới có quyền truy cập và thực hiện các thao tác quản lý học sinh.
    *   Dữ liệu nhạy cảm phải được bảo vệ (ví dụ: không hiển thị toàn bộ số điện thoại nếu có yêu cầu bảo mật cao hơn, mặc dù BRD không đề cập).
    *   Hệ thống phải chống lại các lỗ hổng bảo mật web phổ biến (XSS, SQL Injection, CSRF).
    *   Tất cả dữ liệu đầu vào phải được xác thực ở cả frontend và backend.
*   **Độ tin cậy:**
    *   Hệ thống phải hoạt động ổn định, có khả năng phục hồi khi gặp lỗi.
    *   Dữ liệu phải được sao lưu định kỳ.
*   **Tính khả dụng (Usability):**
    *   Giao diện người dùng phải trực quan, dễ học và dễ sử dụng cho "Người quản lý học sinh".
    *   Các thông báo lỗi và thông báo thành công phải rõ ràng, dễ hiểu và cung cấp phản hồi kịp thời.
    *   Tuân thủ các nguyên tắc thiết kế UI/UX tốt nhất (sử dụng Mantine UI để hỗ trợ).
*   **Khả năng bảo trì (Maintainability):**
    *   Mã nguồn phải rõ ràng, được tổ chức tốt theo Clean Architecture.
    *   Sử dụng các tiêu chuẩn mã hóa và quy ước đặt tên nhất quán.
    *   Có tài liệu kỹ thuật đầy đủ.
    *   Hệ thống phải dễ dàng mở rộng và sửa đổi để đáp ứng các yêu cầu tương lai.
*   **Khả năng kiểm thử (Testability):**
    *   Hệ thống phải được thiết kế để dễ dàng viết các bài kiểm thử đơn vị (Unit Test), kiểm thử tích hợp (Integration Test) và kiểm thử đầu cuối (End-to-End Test).

---

## PHẦN II: KIẾN TRÚC HỆ THỐNG (SAD)

### 1. Giới thiệu

#### 1.1. Mục đích
Tài liệu này mô tả kiến trúc phần mềm cho tính năng "Quản lý thông tin cá nhân học sinh" của hệ thống ONENET, tập trung vào việc tuân thủ Clean Architecture với .NET 10, React/Mantine UI và PostgreSQL.

#### 1.2. Phạm vi
Phạm vi của tài liệu kiến trúc này bao gồm các thành phần chính của hệ thống, luồng dữ liệu, thiết kế cơ sở dữ liệu và các khía cạnh phi chức năng.

#### 1.3. Các bên liên quan
Nhóm phát triển, kiểm thử, quản trị hệ thống, và các kiến trúc sư khác trong dự án ONENET.

#### 1.4. Công nghệ bắt buộc (Tech Stack)
*   **Cơ sở dữ liệu:** PostgreSQL
*   **Backend:** .NET 10 (Clean Architecture)
*   **Frontend:** React (Mantine UI)

### 2. Mục tiêu và Ràng buộc Kiến trúc

#### 2.1. Mục tiêu Kiến trúc
*   **Modularity và Separation of Concerns:** Tách biệt rõ ràng các lớp (Domain, Application, Infrastructure, Presentation) để dễ dàng quản lý, phát triển và bảo trì.
*   **Maintainability:** Dễ dàng sửa đổi và mở rộng mà không ảnh hưởng đến toàn bộ hệ thống.
*   **Testability:** Khuyến khích kiểm thử đơn vị và tích hợp.
*   **Performance:** Đảm bảo hiệu suất tốt cho các thao tác CRUD và tìm kiếm/lọc.
*   **Scalability:** Có khả năng mở rộng theo chiều ngang (horizontal scaling) cho các thành phần backend.
*   **Security:** Áp dụng các biện pháp bảo mật chặt chẽ ở mọi lớp.
*   **Adherence to Clean Architecture:** Đảm bảo các phụ thuộc hướng vào bên trong (dependencies point inwards).

#### 2.2. Ràng buộc Kiến trúc
*   Bắt buộc phải sử dụng Tech Stack đã quy định.
*   Tuân thủ các quy tắc và tiêu chuẩn coding của ONENET (nếu có).

### 3. Ngữ cảnh hệ thống

Hệ thống Quản lý thông tin học sinh là một module trong hệ sinh thái ONENET. Nó tương tác với người dùng qua giao diện web và có thể được tích hợp với các module khác của ONENET trong tương lai (ví dụ: quản lý điểm, quản lý lớp học).

```
+-------------------+     HTTP/HTTPS     +---------------------+     SQL/Npgsql     +------------------+
|    Người dùng     |<------------------>|    Frontend (React) |<------------------->|   Backend (.NET) |<----------------->|  Cơ sở dữ liệu   |
| (Student Manager) |                    |     Mantine UI      |                     | Clean Architecture |                   |   (PostgreSQL)   |
+-------------------+                    +---------------------+                     +---------------------+                   +------------------+
```

### 4. Các góc nhìn kiến trúc

#### 4.1. Logical View (Clean Architecture)

Kiến trúc backend sẽ tuân thủ mô hình Clean Architecture, được chia thành các lớp chính:

*   **Domain Layer (Lớp Miền):**
    *   **Mục đích:** Chứa các thực thể cốt lõi của nghiệp vụ (Domain Entities), các quy tắc nghiệp vụ (Domain Services) và các giá trị (Value Objects). Lớp này hoàn toàn độc lập với các lớp khác, không có phụ thuộc bên ngoài.
    *   **Thực thể chính:** `Student` (Học sinh).
    *   **Thành phần:**
        *   `Student.cs`: Định nghĩa thực thể Học sinh với các thuộc tính và hành vi nghiệp vụ.
        *   Các `enum` hoặc `Value Objects` liên quan (ví dụ: `Gender` nếu được định nghĩa là một Value Object).
*   **Application Layer (Lớp Ứng dụng):**
    *   **Mục đích:** Chứa logic nghiệp vụ cấp cao (Application Services), các DTO (Data Transfer Objects), các Command/Query và các Interface cho các dịch vụ hạ tầng (e.g., repository interfaces). Lớp này phụ thuộc vào Domain Layer.
    *   **Thành phần:**
        *   `IStudentRepository.cs`: Interface cho repository quản lý học sinh.
        *   `StudentService.cs` (hoặc `StudentApplicationService`): Chứa logic nghiệp vụ chính (thêm, sửa, xóa, lấy danh sách học sinh). Sẽ nhận vào các `Command` và trả về `DTOs`.
        *   `StudentDto.cs`: DTO để trả về thông tin học sinh cho Frontend.
        *   `CreateStudentCommand.cs`, `UpdateStudentCommand.cs`, `DeleteStudentCommand.cs`: Các đối tượng chứa dữ liệu yêu cầu từ người dùng.
        *   `GetStudentsQuery.cs`, `StudentQueryParams.cs`: Các đối tượng chứa tiêu chí tìm kiếm/lọc/phân trang.
        *   `IValidator<T>.cs`: Interface cho việc xác thực dữ liệu (có thể sử dụng thư viện FluentValidation).
*   **Infrastructure Layer (Lớp Hạ tầng):**
    *   **Mục đích:** Chứa các triển khai cụ thể của các interface được định nghĩa trong Application Layer (ví dụ: triển khai repository, cấu hình cơ sở dữ liệu, các dịch vụ gửi email...). Lớp này phụ thuộc vào Application Layer và Domain Layer.
    *   **Thành phần:**
        *   `StudentRepository.cs`: Triển khai `IStudentRepository` sử dụng Entity Framework Core và PostgreSQL.
        *   `AppDbContext.cs`: DbContext của Entity Framework Core.
        *   Cấu hình Npgsql cho PostgreSQL.
        *   `LoggingService.cs`: Triển khai dịch vụ ghi log (ví dụ: sử dụng Serilog).
        *   `ValidationBehaviors.cs`: Middleware hoặc pipeline behavior để thực hiện validation trước khi xử lý command/query.
*   **Presentation Layer (Lớp Trình bày - API & UI):**
    *   **Mục đích:** Cung cấp giao diện người dùng và các API endpoints. Lớp này phụ thuộc vào Application Layer (để gọi các Application Services) và Infrastructure Layer (nếu cần các dịch vụ hạ tầng trực tiếp).
    *   **Backend (Web API - .NET 10):**
        *   `StudentsController.cs`: RESTful API Controller, xử lý các HTTP requests (GET, POST, PUT, DELETE) từ Frontend. Gọi các Application Services để xử lý nghiệp vụ.
        *   Cấu hình Dependency Injection, Authentication/Authorization.
    *   **Frontend (React - Mantine UI):**
        *   Các Components React sử dụng Mantine UI:
            *   `StudentList.tsx`: Hiển thị danh sách học sinh, chứa bảng, tìm kiếm, lọc, phân trang.
            *   `StudentForm.tsx`: Biểu mẫu thêm/sửa học sinh.
            *   `ConfirmDeleteModal.tsx`: Hộp thoại xác nhận xóa.
            *   Các Services để gọi API backend.
            *   State management (ví dụ: React Query, Redux Toolkit, hoặc Context API).

**Tương tác tổng thể (Luồng yêu cầu):**
1.  **Frontend (React):** Người dùng thực hiện thao tác (ví dụ: nhấn nút "Thêm học sinh").
2.  **API (StudentsController):** Frontend gửi yêu cầu HTTP (POST /api/students) đến API. Controller nhận `CreateStudentCommand`.
3.  **Application Layer (StudentService):** Controller gọi `StudentService` (hoặc gửi Command qua Mediator pattern) để xử lý. `StudentService` thực hiện validation, tương tác với `IStudentRepository`.
4.  **Domain Layer (Student Entity):** `StudentService` tạo ra hoặc sửa đổi các đối tượng `Student` trong Domain Layer, áp dụng các quy tắc nghiệp vụ.
5.  **Infrastructure Layer (StudentRepository):** `StudentService` sử dụng `StudentRepository` (triển khai `IStudentRepository`) để lưu trữ/truy vấn dữ liệu từ PostgreSQL thông qua Entity Framework Core.
6.  **Cơ sở dữ liệu (PostgreSQL):** Dữ liệu được đọc/ghi vào bảng `Students`.
7.  **Phản hồi:** Kết quả được trả về từ DB -> Infrastructure -> Application -> API -> Frontend để hiển thị cho người dùng.

#### 4.2. Data View (PostgreSQL Schema)

*   **Kiểm tra Schema hiện tại:** Giả định rằng trong hệ thống ONENET, chưa có một bảng quản lý thông tin học sinh nào có ràng buộc duy nhất `StudentId` hoặc các trường dữ liệu tương tự gây trùng lặp mục đích chính. Nếu có, cần đánh giá để tái sử dụng hoặc mở rộng. Trong trường hợp này, chúng ta sẽ thiết kế một bảng mới.

*   **Bảng chính:** `Students`
    *   **Mục đích:** Lưu trữ thông tin cá nhân của mỗi học sinh.
    *   **Cấu trúc (DDL):**
        ```sql
        CREATE TABLE Students (
            Id UUID PRIMARY KEY DEFAULT gen_random_uuid(), -- Khóa chính nội bộ, sử dụng UUID để dễ mở rộng và phân tán
            StudentId VARCHAR(20) UNIQUE NOT NULL,         -- Mã học sinh (duy nhất, không trùng lặp)
            FullName VARCHAR(100) NOT NULL,                -- Họ và tên
            DateOfBirth DATE NOT NULL,                     -- Ngày sinh (phải là ngày trong quá khứ hoặc hiện tại, kiểm tra ở app layer hoặc CHECK constraint)
            Gender VARCHAR(10) NOT NULL,                   -- Giới tính (Nam, Nữ, Khác - kiểm tra ở app layer)
            Address VARCHAR(255) NOT NULL,                 -- Địa chỉ
            ParentPhoneNumber VARCHAR(15) NOT NULL,        -- Số điện thoại phụ huynh (có thể chấp nhận 10 hoặc 11 số, kiểm tra ở app layer)
            ClassName VARCHAR(20) NOT NULL,                -- Lớp học
            CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL, -- Thời điểm tạo bản ghi (audit)
            UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL  -- Thời điểm cập nhật bản ghi cuối cùng (audit)
            -- Có thể thêm các CHECK constraint nếu cần validation chặt chẽ ở DB level,
            -- ví dụ: CHECK (DateOfBirth <= CURRENT_DATE), CHECK (Gender IN ('Nam', 'Nữ', 'Khác'))
        );
        ```
    *   **Chỉ mục (Indexes):**
        *   `idx_students_student_id` (trên `StudentId`): Đã được tạo tự động với `UNIQUE NOT NULL`.
        *   `idx_students_full_name` (trên `FullName`): Để tối ưu hóa tìm kiếm và sắp xếp theo tên.
        *   `idx_students_class_name` (trên `ClassName`): Để tối ưu hóa lọc theo lớp học.
        ```sql
        CREATE INDEX idx_students_full_name ON Students (FullName);
        CREATE INDEX idx_students_class_name ON Students (ClassName);
        ```

#### 4.3. Process View (Lược đồ xử lý)

*   **Thêm học sinh:**
    1.  Frontend gửi `POST /api/students` với dữ liệu `CreateStudentCommand`.
    2.  API Controller xác thực dữ liệu (ví dụ: FluentValidation).
    3.  Controller gọi `StudentService.CreateStudent(command)`.
    4.  `StudentService` kiểm tra `StudentId` duy nhất.
    5.  `StudentService` tạo đối tượng `Student` domain, lưu qua `IStudentRepository.Add(student)`.
    6.  `StudentRepository` (EF Core) thêm vào DB, gọi `_dbContext.SaveChanges()`.
    7.  Trả về `StudentDto` và thông báo thành công.
*   **Sửa học sinh:**
    1.  Frontend gửi `PUT /api/students/{id}` với dữ liệu `UpdateStudentCommand`.
    2.  API Controller xác thực dữ liệu.
    3.  Controller gọi `StudentService.UpdateStudent(id, command)`.
    4.  `StudentService` tìm `Student` theo `id` qua `IStudentRepository.GetById(id)`.
    5.  `StudentService` cập nhật thuộc tính của đối tượng `Student` domain, lưu qua `IStudentRepository.Update(student)`.
    6.  `StudentRepository` (EF Core) cập nhật vào DB, gọi `_dbContext.SaveChanges()`.
    7.  Trả về `StudentDto` và thông báo thành công.
*   **Xóa học sinh:**
    1.  Frontend gửi `DELETE /api/students/{id}`.
    2.  API Controller gọi `StudentService.DeleteStudent(id)`.
    3.  `StudentService` tìm `Student` theo `id`, xóa qua `IStudentRepository.Delete(student)`.
    4.  `StudentRepository` (EF Core) xóa khỏi DB, gọi `_dbContext.SaveChanges()`.
    5.  Trả về thông báo thành công.
*   **Xem danh sách, tìm kiếm, lọc, phân trang:**
    1.  Frontend gửi `GET /api/students` với các tham số query (search, filter, page, pageSize, sortBy, sortOrder).
    2.  API Controller gọi `StudentService.GetStudents(queryParams)`.
    3.  `StudentService` sử dụng `IStudentRepository.GetAll(queryParams)` để truy vấn dữ liệu.
    4.  `StudentRepository` xây dựng truy vấn LINQ/SQL với EF Core, áp dụng các điều kiện tìm kiếm, lọc, phân trang, sắp xếp.
    5.  Trả về danh sách `StudentDto` và thông tin phân trang.

#### 4.4. Deployment View (Lược đồ triển khai)

Hệ thống sẽ được triển khai theo mô hình ba tầng điển hình:

```
+-------------------------------------------------------------+
|                          CLIENT LAYER                       |
|                   (Web Browser - React App)                 |
+-------------------------------------------------------------+
       | HTTP/HTTPS
       v
+-------------------------------------------------------------+
|                      APPLICATION LAYER                      |
|                                                             |
|   +-------------------+     +-------------------------+     |
|   |   Load Balancer   |<----|  .NET 10 Web API (Kestrel)|    |
|   |   (e.g., Nginx)   |     |    (Multiple instances)   |    |
|   +-------------------+     +-------------------------+     |
|             ^                                               |
|             |                                               |
|             +-----------------------------------------------+
|                                                             |
+-------------------------------------------------------------+
       | SQL (Npgsql)
       v
+-------------------------------------------------------------+
|                         DATABASE LAYER                      |
|                  (PostgreSQL Server Cluster)                |
+-------------------------------------------------------------+
```

*   **Client Layer:** Người dùng truy cập ứng dụng React thông qua trình duyệt web.
*   **Application Layer:**
    *   React frontend được build thành các tệp tĩnh (HTML, CSS, JS) và được phục vụ bởi một web server (ví dụ: Nginx, IIS, hoặc Azure App Service).
    *   Backend .NET 10 Web API được triển khai trên các máy chủ ứng dụng (ví dụ: Docker containers, Azure App Service, Kubernetes).
    *   Một bộ cân bằng tải (Load Balancer) sẽ phân phối lưu lượng truy cập giữa các instance của Web API để đảm bảo khả năng mở rộng và chịu lỗi.
*   **Database Layer:** Cơ sở dữ liệu PostgreSQL được triển khai trên một máy chủ hoặc cụm máy chủ chuyên dụng, có thể là dịch vụ được quản lý (ví dụ: Azure Database for PostgreSQL) để đảm bảo độ tin cậy và hiệu suất.

### 5. Các vấn đề xuyên suốt (Cross-Cutting Concerns)

*   **Xác thực và Ủy quyền (Authentication & Authorization):**
    *   Backend API sẽ yêu cầu xác thực người dùng (ví dụ: JWT Bearer Token) và kiểm tra quyền (`[Authorize(Roles = "StudentManager")]`) cho tất cả các endpoint liên quan đến quản lý học sinh.
*   **Xử lý lỗi và Ghi log (Error Handling & Logging):**
    *   Sử dụng middleware trong .NET để xử lý lỗi toàn cục, trả về các lỗi nhất quán cho frontend (ví dụ: HTTP 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found, 500 Internal Server Error).
    *   Sử dụng thư viện ghi log (ví dụ: Serilog) để ghi lại các sự kiện quan trọng, lỗi và thông tin debug vào các hệ thống log tập trung (ví dụ: ELK Stack, Azure Monitor).
*   **Xác thực dữ liệu (Data Validation):**
    *   Thực hiện xác thực dữ liệu ở cả frontend (Mantine form validation) và backend (sử dụng FluentValidation trong Application Layer hoặc Model Validation trong Controller).
*   **Cấu hình (Configuration Management):**
    *   Sử dụng `appsettings.json` trong .NET cho các cấu hình ứng dụng, và biến môi trường cho các thông tin nhạy cảm (chuỗi kết nối DB, khóa API) khi triển khai.
*   **Giám sát (Monitoring):**
    *   Triển khai các công cụ giám sát hiệu suất ứng dụng (APM) để theo dõi hiệu suất backend và database.
    *   Giám sát lỗi và cảnh báo cho các sự cố hệ thống.
*   **Quản lý phiên bản (Version Control):**
    *   Sử dụng Git (ví dụ: với GitHub, GitLab, Azure DevOps Repos) để quản lý mã nguồn.

---

Hy vọng tài liệu này cung cấp một cái nhìn toàn diện và rõ ràng về các yêu cầu và thiết kế kiến trúc cho tính năng Quản lý thông tin cá nhân học sinh trong hệ thống ONENET.