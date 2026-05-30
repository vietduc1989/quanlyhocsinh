# Tài liệu Yêu cầu Nghiệp vụ (BRD) - Quản lý Lớp Học

**Mã tính năng:** `QUAN-20260530-2249`
**Tên tính năng:** Quản lý Lớp Học
**Dự án:** `quanlyhocsinh`
**Ngày:** `2026-05-30`

---

## 1. Giới thiệu

### 1.1. Mục đích

Tài liệu này mô tả chi tiết các yêu cầu nghiệp vụ cho tính năng "Quản lý Lớp Học" trong hệ thống `quanlyhocsinh`. Mục đích là cung cấp một bản mô tả rõ ràng, đầy đủ và nhất quán về các chức năng và quy tắc nghiệp vụ cần được phát triển, làm cơ sở cho các giai đoạn thiết kế, phát triển và kiểm thử hệ thống.

### 1.2. Phạm vi

Tính năng "Quản lý Lớp Học" bao gồm các chức năng cốt lõi cho phép người dùng có quyền quản trị hệ thống thực hiện các thao tác thêm mới, xem, cập nhật, xóa (CRUD) thông tin lớp học. Ngoài ra, tính năng này còn hỗ trợ tìm kiếm, phân trang và kiểm tra tính hợp lệ của dữ liệu (validation) để đảm bảo tính toàn vẹn và dễ sử dụng.

### 1.3. Đối tượng độc giả

*   Nhóm phát triển phần mềm (Developers)
*   Kiểm thử viên (QA/Testers)
*   Quản lý dự án (Project Managers)
*   Chuyên viên phân tích nghiệp vụ (Business Analysts)
*   Các bên liên quan (Stakeholders)

## 2. Bối cảnh Nghiệp vụ

Hệ thống `quanlyhocsinh` hiện tại cần một module mạnh mẽ để quản lý thông tin các lớp học một cách hiệu quả. Việc quản lý lớp học là nền tảng để tổ chức học sinh, phân công giáo viên, quản lý thời khóa biểu và theo dõi kết quả học tập. Tính năng này sẽ giúp người quản lý hệ thống dễ dàng tạo, chỉnh sửa, và tổ chức cấu trúc lớp học, đảm bảo dữ liệu luôn chính xác và cập nhật, từ đó nâng cao hiệu quả vận hành tổng thể của hệ thống.

## 3. Các Bên Liên quan (Actors)

*   **Quản trị viên hệ thống (System Administrator):** Người dùng có toàn quyền truy cập và thực hiện tất cả các thao tác CRUD đối với thông tin lớp học.
*   **Người quản lý (Manager):** (Nếu có vai trò phân cấp) Người dùng có thể xem danh sách lớp học, tìm kiếm nhưng bị hạn chế một số thao tác CRUD nhất định (ví dụ: không được xóa).

*(Trong phạm vi tài liệu này, chúng ta sẽ tập trung vào vai trò Quản trị viên hệ thống với toàn quyền thao tác.)*

## 4. Các Trường hợp Sử dụng (Use Cases)

### 4.1. UC1: Quản lý Danh sách Lớp Học

*   **Tên Use Case:** Quản lý Danh sách Lớp Học
*   **Actor:** Quản trị viên hệ thống
*   **Mô tả:** Cho phép Quản trị viên hệ thống xem danh sách tất cả các lớp học hiện có trong hệ thống, tìm kiếm lớp học theo tiêu chí và duyệt qua các trang kết quả.
*   **Luồng chính (Main Flow):**
    1.  Quản trị viên truy cập vào màn hình "Quản lý Lớp Học".
    2.  Hệ thống hiển thị danh sách các lớp học hiện có.
    3.  Quản trị viên có thể nhập từ khóa vào ô tìm kiếm (ví dụ: mã lớp, tên lớp, năm học).
    4.  Quản trị viên nhấn nút "Tìm kiếm" hoặc hệ thống tự động lọc.
    5.  Hệ thống hiển thị danh sách lớp học phù hợp với tiêu chí tìm kiếm.
    6.  Nếu danh sách quá dài, hệ thống sẽ hiển thị phân trang. Quản trị viên có thể chuyển đổi giữa các trang.
    7.  Quản trị viên có thể nhấn vào một lớp học trong danh sách để xem chi tiết.
*   **Luồng ngoại lệ (Exception Flows):**
    *   **UC1-EX-01 - Không tìm thấy kết quả:** Nếu không có lớp học nào phù hợp với tiêu chí tìm kiếm, hệ thống hiển thị thông báo "Không tìm thấy lớp học nào."
    *   **UC1-EX-02 - Lỗi hệ thống:** Nếu có lỗi xảy ra khi tải dữ liệu, hệ thống hiển thị thông báo lỗi và yêu cầu thử lại.
*   **Điều kiện tiên quyết:** Quản trị viên đã đăng nhập và có quyền truy cập tính năng "Quản lý Lớp Học".
*   **Kết quả sau thực hiện:** Quản trị viên có thể xem, tìm kiếm và phân trang danh sách lớp học.

### 4.2. UC2: Thêm Mới Lớp Học

*   **Tên Use Case:** Thêm Mới Lớp Học
*   **Actor:** Quản trị viên hệ thống
*   **Mô tả:** Cho phép Quản trị viên hệ thống thêm một lớp học mới vào hệ thống với các thông tin cơ bản.
*   **Luồng chính (Main Flow):**
    1.  Quản trị viên truy cập màn hình "Quản lý Lớp Học" (UC1).
    2.  Quản trị viên nhấn nút "Thêm Mới" hoặc biểu tượng tương ứng.
    3.  Hệ thống hiển thị màn hình nhập liệu "Thêm Lớp Học Mới".
    4.  Quản trị viên nhập các thông tin cần thiết: Mã lớp, Tên lớp, Năm học, Sĩ số tối đa, Ghi chú (tùy chọn).
    5.  Quản trị viên nhấn nút "Lưu".
    6.  Hệ thống kiểm tra tính hợp lệ của dữ liệu.
    7.  Nếu dữ liệu hợp lệ, hệ thống lưu lớp học mới vào cơ sở dữ liệu.
    8.  Hệ thống hiển thị thông báo "Thêm lớp học thành công." và điều hướng về màn hình danh sách (UC1) hoặc màn hình chi tiết lớp học vừa tạo.
*   **Luồng ngoại lệ (Exception Flows):**
    *   **UC2-EX-01 - Dữ liệu không hợp lệ:** Nếu dữ liệu nhập vào không hợp lệ (ví dụ: thiếu trường bắt buộc, sai định dạng), hệ thống hiển thị thông báo lỗi chi tiết cho từng trường và không lưu dữ liệu.
    *   **UC2-EX-02 - Mã lớp đã tồn tại:** Nếu Mã lớp đã tồn tại, hệ thống hiển thị thông báo lỗi "Mã lớp đã tồn tại. Vui lòng nhập mã khác."
    *   **UC2-EX-03 - Lỗi hệ thống:** Nếu có lỗi xảy ra trong quá trình lưu, hệ thống hiển thị thông báo lỗi chung và không lưu dữ liệu.
*   **Điều kiện tiên quyết:** Quản trị viên đã đăng nhập và có quyền thêm lớp học.
*   **Kết quả sau thực hiện:** Một lớp học mới được tạo trong hệ thống.

### 4.3. UC3: Xem Chi tiết Lớp Học

*   **Tên Use Case:** Xem Chi tiết Lớp Học
*   **Actor:** Quản trị viên hệ thống
*   **Mô tả:** Cho phép Quản trị viên hệ thống xem thông tin chi tiết của một lớp học đã tồn tại.
*   **Luồng chính (Main Flow):**
    1.  Quản trị viên truy cập màn hình "Quản lý Lớp Học" (UC1).
    2.  Quản trị viên nhấn vào tên hoặc biểu tượng "Xem chi tiết" của một lớp học cụ thể trong danh sách.
    3.  Hệ thống hiển thị màn hình "Chi tiết Lớp Học" với tất cả thông tin của lớp đó.
    4.  Quản trị viên có thể nhấn nút "Sửa" để cập nhật hoặc "Quay lại" để về danh sách.
*   **Luồng ngoại lệ (Exception Flows):**
    *   **UC3-EX-01 - Không tìm thấy lớp học:** Nếu lớp học không tồn tại (ví dụ: đã bị xóa bởi người dùng khác), hệ thống hiển thị thông báo "Không tìm thấy lớp học."
    *   **UC3-EX-02 - Lỗi hệ thống:** Nếu có lỗi xảy ra khi tải dữ liệu, hệ thống hiển thị thông báo lỗi và yêu cầu thử lại.
*   **Điều kiện tiên quyết:** Quản trị viên đã đăng nhập và lớp học cần xem chi tiết đã tồn tại.
*   **Kết quả sau thực hiện:** Thông tin chi tiết của lớp học được hiển thị.

### 4.4. UC4: Cập nhật Lớp Học

*   **Tên Use Case:** Cập nhật Lớp Học
*   **Actor:** Quản trị viên hệ thống
*   **Mô tả:** Cho phép Quản trị viên hệ thống chỉnh sửa thông tin của một lớp học đã tồn tại.
*   **Luồng chính (Main Flow):**
    1.  Quản trị viên truy cập màn hình "Chi tiết Lớp Học" (UC3).
    2.  Quản trị viên nhấn nút "Sửa" hoặc biểu tượng tương ứng.
    3.  Hệ thống hiển thị màn hình chỉnh sửa với các thông tin hiện tại của lớp học được điền sẵn.
    4.  Quản trị viên chỉnh sửa các thông tin mong muốn (ví dụ: Tên lớp, Sĩ số tối đa, Ghi chú). Mã lớp và Năm học có thể bị khóa hoặc có quy tắc riêng để thay đổi.
    5.  Quản trị viên nhấn nút "Lưu".
    6.  Hệ thống kiểm tra tính hợp lệ của dữ liệu đã chỉnh sửa.
    7.  Nếu dữ liệu hợp lệ, hệ thống cập nhật thông tin lớp học vào cơ sở dữ liệu.
    8.  Hệ thống hiển thị thông báo "Cập nhật lớp học thành công." và điều hướng về màn hình chi tiết lớp học.
*   **Luồng ngoại lệ (Exception Flows):**
    *   **UC4-EX-01 - Dữ liệu không hợp lệ:** Tương tự UC2-EX-01.
    *   **UC4-EX-02 - Lớp học không tồn tại:** Tương tự UC3-EX-01.
    *   **UC4-EX-03 - Lỗi hệ thống:** Tương tự UC2-EX-03.
*   **Điều kiện tiên quyết:** Quản trị viên đã đăng nhập và có quyền chỉnh sửa lớp học. Lớp học cần chỉnh sửa đã tồn tại.
*   **Kết quả sau thực hiện:** Thông tin của lớp học được cập nhật trong hệ thống.

### 4.5. UC5: Xóa Lớp Học

*   **Tên Use Case:** Xóa Lớp Học
*   **Actor:** Quản trị viên hệ thống
*   **Mô tả:** Cho phép Quản trị viên hệ thống xóa một lớp học không còn cần thiết khỏi hệ thống.
*   **Luồng chính (Main Flow):**
    1.  Quản trị viên truy cập màn hình "Quản lý Lớp Học" (UC1).
    2.  Quản trị viên nhấn nút "Xóa" hoặc biểu tượng tương ứng bên cạnh lớp học muốn xóa.
    3.  Hệ thống hiển thị hộp thoại xác nhận "Bạn có chắc chắn muốn xóa lớp học [Tên lớp] không? Thao tác này không thể hoàn tác."
    4.  Quản trị viên nhấn "Xác nhận" (hoặc "Đồng ý").
    5.  Hệ thống kiểm tra các ràng buộc nghiệp vụ (ví dụ: lớp học có học sinh không).
    6.  Nếu không có ràng buộc nào bị vi phạm, hệ thống xóa lớp học khỏi cơ sở dữ liệu.
    7.  Hệ thống hiển thị thông báo "Xóa lớp học thành công." và cập nhật lại danh sách lớp học.
*   **Luồng ngoại lệ (Exception Flows):**
    *   **UC5-EX-01 - Hủy bỏ thao tác:** Quản trị viên nhấn "Hủy" (hoặc "Không đồng ý") trong hộp thoại xác nhận. Hệ thống đóng hộp thoại và không thực hiện xóa.
    *   **UC5-EX-02 - Vi phạm ràng buộc:** Nếu lớp học có các ràng buộc không cho phép xóa (ví dụ: đã có học sinh, đã có thời khóa biểu), hệ thống hiển thị thông báo lỗi "Không thể xóa lớp học này vì [Lý do cụ thể]."
    *   **UC5-EX-03 - Lớp học không tồn tại:** Tương tự UC3-EX-01.
    *   **UC5-EX-04 - Lỗi hệ thống:** Tương tự UC2-EX-03.
*   **Điều kiện tiên quyết:** Quản trị viên đã đăng nhập và có quyền xóa lớp học. Lớp học cần xóa đã tồn tại và không vi phạm các ràng buộc xóa.
*   **Kết quả sau thực hiện:** Lớp học được xóa khỏi hệ thống hoặc thông báo lỗi được hiển thị.

## 5. Yêu cầu Chức năng (Functional Requirements - FRs)

### 5.1. Quản lý Danh sách (CRUD: Read, Search, Pagination)

*   **QUAN-20260530-2249-FR01: Hiển thị danh sách lớp học**
    *   **Mô tả:** Hệ thống phải hiển thị danh sách tất cả các lớp học hiện có, bao gồm các thông tin chính như Mã lớp, Tên lớp, Năm học, Sĩ số hiện tại/tối đa.
    *   **Acceptance Criteria:**
        *   Khi truy cập màn hình "Quản lý Lớp Học", danh sách các lớp học phải được tải và hiển thị tự động.
        *   Mỗi dòng trong danh sách phải hiển thị Mã lớp, Tên lớp, Năm học và Sĩ số (ví dụ: 25/40).
        *   Danh sách phải được sắp xếp mặc định theo Năm học giảm dần, sau đó theo Tên lớp tăng dần.
        *   Danh sách phải có các cột cho phép thao tác (Xem, Sửa, Xóa).
*   **QUAN-20260530-2249-FR02: Chức năng tìm kiếm lớp học**
    *   **Mô tả:** Hệ thống phải cung cấp khả năng tìm kiếm lớp học dựa trên các tiêu chí như Mã lớp, Tên lớp, hoặc Năm học.
    *   **Acceptance Criteria:**
        *   Người dùng có thể nhập một từ khóa vào ô tìm kiếm.
        *   Hệ thống phải lọc danh sách lớp học theo từ khóa đã nhập, hiển thị các lớp có chứa từ khóa đó ở bất kỳ trường nào (Mã lớp, Tên lớp, Năm học).
        *   Tìm kiếm phải không phân biệt chữ hoa/thường.
        *   Kết quả tìm kiếm phải được cập nhật ngay lập tức (live search) hoặc sau khi người dùng nhấn nút "Tìm kiếm".
*   **QUAN-20260530-2249-FR03: Chức năng phân trang danh sách lớp học**
    *   **Mô tả:** Hệ thống phải hỗ trợ phân trang cho danh sách lớp học khi số lượng lớp vượt quá giới hạn hiển thị trên một trang.
    *   **Acceptance Criteria:**
        *   Người dùng có thể chọn số lượng mục hiển thị trên mỗi trang (ví dụ: 10, 20, 50). Mặc định là 10.
        *   Hệ thống phải hiển thị các điều khiển phân trang (trang trước, trang kế tiếp, số trang, trang đầu, trang cuối).
        *   Khi người dùng chuyển trang, hệ thống phải tải và hiển thị dữ liệu của trang đó.
        *   Thông tin về tổng số lớp học và số lượng lớp học trên trang hiện tại phải được hiển thị (ví dụ: "Hiển thị 1-10 trên tổng số 100 lớp").

### 5.2. Thêm Mới Lớp Học (CRUD: Create)

*   **QUAN-20260530-2249-FR04: Thêm mới thông tin lớp học**
    *   **Mô tả:** Hệ thống phải cung cấp một giao diện cho phép Quản trị viên nhập thông tin để tạo lớp học mới.
    *   **Acceptance Criteria:**
        *   Màn hình "Thêm Lớp Học Mới" phải có các trường nhập liệu: Mã lớp, Tên lớp, Năm học, Sĩ số tối đa, Ghi chú.
        *   Trường Mã lớp phải là duy nhất (kiểm tra real-time hoặc khi lưu).
        *   Mã lớp, Tên lớp, Năm học, Sĩ số tối đa là các trường bắt buộc.
        *   Năm học phải là số nguyên dương và có định dạng năm (YYYY), ví dụ: 2023, 2024.
        *   Sĩ số tối đa phải là số nguyên dương.
        *   Sau khi lưu thành công, hệ thống phải hiển thị thông báo "Thêm lớp học thành công." và điều hướng về trang danh sách hoặc trang chi tiết lớp vừa tạo.

### 5.3. Xem Chi tiết Lớp Học (CRUD: Read)

*   **QUAN-20260530-2249-FR05: Xem chi tiết thông tin lớp học**
    *   **Mô tả:** Hệ thống phải hiển thị đầy đủ thông tin chi tiết của một lớp học khi người dùng yêu cầu.
    *   **Acceptance Criteria:**
        *   Màn hình "Chi tiết Lớp Học" phải hiển thị tất cả các trường thông tin của lớp học: Mã lớp, Tên lớp, Năm học, Sĩ số tối đa, Sĩ số hiện tại, Ghi chú.
        *   Thông tin phải được hiển thị rõ ràng, dễ đọc.
        *   Màn hình chi tiết phải có các nút "Sửa" và "Quay lại".

### 5.4. Cập nhật Lớp Học (CRUD: Update)

*   **QUAN-20260530-2249-FR06: Chỉnh sửa thông tin lớp học**
    *   **Mô tả:** Hệ thống phải cho phép Quản trị viên chỉnh sửa thông tin của một lớp học đã tồn tại.
    *   **Acceptance Criteria:**
        *   Khi người dùng nhấn nút "Sửa" từ danh sách hoặc màn hình chi tiết, hệ thống phải hiển thị màn hình chỉnh sửa với dữ liệu hiện tại của lớp được điền sẵn.
        *   Các trường Tên lớp, Sĩ số tối đa, Ghi chú phải có thể chỉnh sửa.
        *   Trường Mã lớp và Năm học PHẢI không thể chỉnh sửa sau khi tạo. (Hoặc có quy trình đặc biệt nếu cần thay đổi, nhưng mặc định là không).
        *   Các validation tương tự như `QUAN-20260530-2249-FR04` phải được áp dụng khi lưu.
        *   Nếu Sĩ số tối đa được giảm, hệ thống phải đảm bảo Sĩ số tối đa mới không nhỏ hơn Sĩ số hiện tại của lớp.
        *   Sau khi lưu thành công, hệ thống phải hiển thị thông báo "Cập nhật lớp học thành công." và điều hướng về trang chi tiết lớp học.

### 5.5. Xóa Lớp Học (CRUD: Delete)

*   **QUAN-20260530-2249-FR07: Xóa lớp học**
    *   **Mô tả:** Hệ thống phải cho phép Quản trị viên xóa một lớp học khỏi hệ thống.
    *   **Acceptance Criteria:**
        *   Khi người dùng nhấn nút "Xóa", hệ thống phải hiển thị hộp thoại xác nhận trước khi thực hiện xóa.
        *   Nếu người dùng xác nhận, hệ thống phải kiểm tra các ràng buộc nghiệp vụ (xem phần Business Rules).
        *   Nếu không có ràng buộc nào bị vi phạm, lớp học phải được xóa vĩnh viễn khỏi cơ sở dữ liệu.
        *   Sau khi xóa thành công, hệ thống phải hiển thị thông báo "Xóa lớp học thành công." và cập nhật lại danh sách lớp học.

### 5.6. Validation

*   **QUAN-20260530-2249-FR08: Kiểm tra dữ liệu đầu vào**
    *   **Mô tả:** Hệ thống phải thực hiện kiểm tra tính hợp lệ của dữ liệu đầu vào cho các trường thông tin lớp học.
    *   **Acceptance Criteria:**
        *   Trường Mã lớp: Bắt buộc, tối đa 20 ký tự, không chứa ký tự đặc biệt (chỉ chữ, số, gạch nối, gạch dưới).
        *   Trường Tên lớp: Bắt buộc, tối đa 50 ký tự.
        *   Trường Năm học: Bắt buộc, là số nguyên dương, có định dạng YYYY (ví dụ: từ 1900 đến năm hiện tại + 5).
        *   Trường Sĩ số tối đa: Bắt buộc, là số nguyên dương, nằm trong khoảng từ 10 đến 60 (hoặc giới hạn khác theo quy định).
        *   Trường Ghi chú: Tùy chọn, tối đa 255 ký tự.
        *   Khi có lỗi validation, hệ thống phải hiển thị thông báo lỗi rõ ràng bên dưới hoặc cạnh trường nhập liệu tương ứng.

## 6. Quy tắc Nghiệp vụ (Business Rules - BRs)

*   **QUAN-20260530-2249-BR01: Mã lớp duy nhất**
    *   **Mô tả:** Mỗi Mã lớp phải là duy nhất trong toàn bộ hệ thống.
    *   **Ví dụ:**
        *   Quản trị viên tạo lớp với Mã lớp "10A1". Lớp này đã tồn tại.
        *   Hệ thống phải hiển thị thông báo lỗi: "Mã lớp '10A1' đã tồn tại. Vui lòng chọn mã khác."
*   **QUAN-20260530-2249-BR02: Tên lớp duy nhất trong cùng Năm học**
    *   **Mô tả:** Tên lớp không được trùng nhau trong cùng một Năm học.
    *   **Ví dụ:**
        *   Trong Năm học 2025, đã có lớp "10A".
        *   Quản trị viên cố gắng tạo một lớp khác với Tên lớp là "10A" cho Năm học 2025.
        *   Hệ thống phải hiển thị thông báo lỗi: "Tên lớp '10A' đã tồn tại trong Năm học 2025. Vui lòng chọn tên khác."
*   **QUAN-20260530-2249-BR03: Sĩ số tối đa phải lớn hơn hoặc bằng sĩ số hiện tại**
    *   **Mô tả:** Khi cập nhật thông tin lớp học, Sĩ số tối đa mới không được nhỏ hơn Sĩ số hiện tại của lớp.
    *   **Ví dụ:**
        *   Lớp 10A có Sĩ số hiện tại là 35 học sinh, Sĩ số tối đa là 40.
        *   Quản trị viên cố gắng sửa Sĩ số tối đa xuống còn 30.
        *   Hệ thống phải hiển thị thông báo lỗi: "Sĩ số tối đa không được nhỏ hơn sĩ số hiện tại của lớp (35)."
*   **QUAN-20260530-2249-BR04: Không thể xóa lớp học có học sinh**
    *   **Mô tả:** Một lớp học không thể bị xóa nếu hiện tại có ít nhất một học sinh đang thuộc lớp đó.
    *   **Ví dụ:**
        *   Lớp 11B đang có 30 học sinh.
        *   Quản trị viên cố gắng xóa lớp 11B.
        *   Hệ thống phải hiển thị thông báo lỗi: "Không thể xóa lớp 11B vì lớp này đang có học sinh. Vui lòng chuyển hoặc xóa học sinh trước."
*   **QUAN-20260530-2249-BR05: Không thể xóa lớp học đã được gán vào thời khóa biểu hoặc sự kiện**
    *   **Mô tả:** Một lớp học không thể bị xóa nếu nó đã được gán vào bất kỳ thời khóa biểu, lịch thi, hoặc sự kiện nào trong hệ thống.
    *   **Ví dụ:**
        *   Lớp 12C đã có thời khóa biểu cho học kỳ hiện tại.
        *   Quản trị viên cố gắng xóa lớp 12C.
        *   Hệ thống phải hiển thị thông báo lỗi: "Không thể xóa lớp 12C vì lớp này đang có thời khóa biểu. Vui lòng hủy thời khóa biểu liên quan trước."

## 7. Yêu cầu Phi chức năng (Non-Functional Requirements - NFRs)

*   **Hiệu năng:**
    *   Hệ thống phải tải danh sách lớp học (tối đa 1000 bản ghi) trong vòng 3 giây.
    *   Các thao tác thêm/sửa/xóa lớp học phải hoàn thành trong vòng 1 giây.
*   **Khả năng sử dụng:**
    *   Giao diện người dùng phải trực quan, dễ hiểu và dễ sử dụng.
    *   Các thông báo lỗi và thành công phải rõ ràng, thân thiện với người dùng.
*   **Bảo mật:**
    *   Chỉ Quản trị viên hệ thống mới có quyền thực hiện các thao tác thêm, sửa, xóa lớp học.
    *   Dữ liệu nhạy cảm phải được bảo vệ khỏi truy cập trái phép.
*   **Khả năng mở rộng:**
    *   Hệ thống phải có khả năng mở rộng để hỗ trợ số lượng lớp học lớn hơn trong tương lai mà không ảnh hưởng đáng kể đến hiệu suất.

## 8. Ràng buộc (Constraints)

*   **Tích hợp:** Tính năng này cần tích hợp với module Quản lý Học Sinh (để kiểm tra sĩ số, học sinh thuộc lớp nào) và module Thời Khóa Biểu (để kiểm tra lớp đã được gán hay chưa).
*   **Công nghệ:** Hệ thống phải tuân thủ kiến trúc và công nghệ hiện có của hệ thống `ONENET`.
*   **Ngôn ngữ:** Giao diện và thông báo phải bằng tiếng Việt.

## 9. Giả định (Assumptions)

*   Đã có module quản lý học sinh cơ bản để liên kết học sinh với lớp học.
*   Người dùng có quyền "Quản trị viên hệ thống" đã được xác định và quản lý trong hệ thống phân quyền.
*   Sĩ số hiện tại của lớp được tính toán tự động dựa trên số lượng học sinh được gán vào lớp đó.

## 10. Tiêu chí Chấp nhận Tổng thể (Overall Acceptance Criteria)

*   Tất cả các yêu cầu chức năng (`QUAN-20260530-2249-FRxx`) và quy tắc nghiệp vụ (`QUAN-20260530-2249-BRxx`) phải được triển khai và hoạt động chính xác.
*   Các yêu cầu phi chức năng quan trọng (hiệu năng, bảo mật, khả năng sử dụng) phải được đáp ứng.
*   Hệ thống phải hoạt động ổn định và không có lỗi nghiêm trọng.
*   Người dùng có thể dễ dàng quản lý lớp học từ đầu đến cuối mà không gặp trở ngại đáng kể.

## 11. Rủi ro Tiềm ẩn (Potential Risks)

*   **Rủi ro về dữ liệu:** Các vấn đề về đồng bộ dữ liệu sĩ số học sinh nếu có sự chậm trễ hoặc lỗi trong module quản lý học sinh.
*   **Rủi ro về hiệu năng:** Hiệu năng tìm kiếm và tải danh sách có thể bị ảnh hưởng nếu số lượng lớp học tăng lên rất lớn mà không có tối ưu hóa cơ sở dữ liệu.
*   **Rủi ro về tích hợp:** Các module liên quan (Học sinh, Thời khóa biểu) có thể chưa sẵn sàng hoặc thay đổi interface, gây ảnh hưởng đến tính năng Quản lý Lớp Học.
*   **Rủi ro về quy định nghiệp vụ:** Các quy định về sĩ số tối đa, năm học có thể thay đổi trong tương lai, yêu cầu cập nhật hệ thống.

---