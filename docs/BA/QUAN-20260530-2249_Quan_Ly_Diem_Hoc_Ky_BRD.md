Chào bạn, với vai trò là Business Analyst cấp cao của hệ thống ONENET, tôi đã tiếp nhận yêu cầu và sẽ xây dựng tài liệu BRD chi tiết cho tính năng "Quản lý Điểm Học Kỳ" với mã tính năng `QUAN-20260530-2249`.

Dưới đây là tài liệu BRD theo chuẩn nghiệp vụ:

---

# Business Requirements Document (BRD)

## 1. Thông tin tài liệu

*   **Mã tính năng**: QUAN-20260530-2249
*   **Tên tính năng**: Quản lý Điểm Học Kỳ
*   **Dự án**: quanlyhocsinh
*   **Ngày tạo**: 2026-05-30
*   **Phiên bản**: 1.0
*   **Tác giả**: [Tên Business Analyst]
*   **Người phê duyệt**: [Chủ sản phẩm/Người quản lý dự án]

## 2. Giới thiệu

### 2.1. Mục đích tài liệu

Tài liệu này mô tả chi tiết các yêu cầu nghiệp vụ (Business Requirements) và yêu cầu chức năng (Functional Requirements) cho tính năng "Quản lý Điểm Học Kỳ" trong hệ thống `quanlyhocsinh`. Mục đích là cung cấp một bản mô tả rõ ràng, đầy đủ và dễ hiểu cho đội ngũ phát triển, kiểm thử và các bên liên quan khác, nhằm đảm bảo sản phẩm cuối cùng đáp ứng đúng nhu cầu của người dùng và mục tiêu nghiệp vụ của dự án.

### 2.2. Phạm vi tính năng

Tính năng "Quản lý Điểm Học Kỳ" tập trung vào việc số hóa quy trình nhập, xem, sửa, xóa, tìm kiếm và phân trang điểm số của học sinh theo từng môn học và học kỳ cụ thể. Tính năng này sẽ hỗ trợ giáo viên và quản trị viên trong việc quản lý dữ liệu điểm một cách hiệu quả, chính xác và minh bạch.

### 2.3. Đối tượng độc giả

*   Chủ sản phẩm (Product Owner)
*   Đội ngũ phát triển (Development Team)
*   Đội ngũ kiểm thử (QA/Tester)
*   Quản lý dự án (Project Manager)
*   Các bên liên quan khác (ví dụ: Ban giám hiệu, Giáo viên, ...)

## 3. Các bên liên quan (Stakeholders)

*   **Giáo viên**: Người trực tiếp nhập, sửa, xem điểm của học sinh thuộc lớp và môn học mình phụ trách.
*   **Quản trị viên hệ thống**: Người có quyền quản lý toàn bộ dữ liệu điểm, bao gồm phân quyền cho giáo viên, xử lý các trường hợp đặc biệt.
*   **Ban giám hiệu**: Người xem báo cáo, thống kê điểm số để đánh giá chất lượng dạy và học.
*   **Học sinh/Phụ huynh**: Người xem điểm số của bản thân/con em mình.
*   **Chủ sản phẩm**: Người đưa ra định hướng và ưu tiên cho tính năng.
*   **Đội phát triển**: Người thiết kế và xây dựng tính năng.
*   **Đội kiểm thử**: Người xác minh tính năng hoạt động đúng theo yêu cầu.

## 4. Bối cảnh nghiệp vụ (Business Context)

Hiện tại, việc quản lý điểm số học kỳ tại nhiều trường học có thể vẫn đang được thực hiện thủ công hoặc trên các hệ thống rời rạc, dẫn đến các vấn đề như:
*   Mất thời gian nhập liệu và tổng hợp.
*   Dễ xảy ra sai sót trong quá trình ghi chép và tính toán.
*   Khó khăn trong việc truy xuất thông tin, thống kê và báo cáo.
*   Thiếu tính minh bạch và khả năng truy vết thay đổi.

Tính năng "Quản lý Điểm Học Kỳ" sẽ giải quyết các vấn đề này bằng cách cung cấp một công cụ tập trung, hiệu quả để quản lý điểm số, giúp giảm thiểu sai sót, tiết kiệm thời gian, tăng cường tính minh bạch và hỗ trợ ra quyết định dựa trên dữ liệu.

## 5. Các Actor và Use Case

### 5.1. Danh sách Actor

*   **Giáo viên**: Người quản lý và nhập liệu điểm số.
*   **Quản trị viên hệ thống**: Người quản lý dữ liệu điểm và phân quyền.
*   **Học sinh/Phụ huynh**: Người xem thông tin điểm số.

### 5.2. Danh sách Use Case

| Mã Use Case               | Tên Use Case               | Actor chính         | Mô tả ngắn gọn                                          |
| :------------------------ | :------------------------- | :------------------ | :------------------------------------------------------ |
| QUAN-20260530-2249-UC01 | Quản lý Điểm Học Kỳ (CRUD) | Giáo viên, Quản trị viên hệ thống | Cho phép tạo mới, xem, sửa, xóa thông tin điểm học kỳ. |
| QUAN-20260530-2249-UC02 | Tìm kiếm Điểm Học Kỳ     | Giáo viên, Quản trị viên hệ thống, Học sinh/Phụ huynh | Cho phép tìm kiếm điểm theo các tiêu chí khác nhau.     |
| QUAN-20260530-2249-UC03 | Xem Danh sách Điểm Học Kỳ | Giáo viên, Quản trị viên hệ thống, Học sinh/Phụ huynh | Hiển thị danh sách điểm với chức năng phân trang.       |

## 6. Mô tả các Use Case

### 6.1. Use Case: QUAN-20260530-2249-UC01 - Quản lý Điểm Học Kỳ (CRUD)

*   **Mô tả**: Actor có thể tạo mới một bản ghi điểm, xem chi tiết, chỉnh sửa hoặc xóa một bản ghi điểm học kỳ đã tồn tại.
*   **Actor chính**: Giáo viên, Quản trị viên hệ thống.
*   **Điều kiện tiên quyết**:
    *   Actor đã đăng nhập vào hệ thống.
    *   Actor có quyền "Quản lý Điểm Học Kỳ".
    *   Dữ liệu về Học sinh, Môn học, Học kỳ đã có trong hệ thống.
*   **Luồng chính**:
    1.  Actor chọn chức năng "Quản lý Điểm Học Kỳ".
    2.  Hệ thống hiển thị danh sách điểm hiện có (nếu có).
    3.  **Để tạo mới**:
        *   Actor chọn "Thêm mới điểm".
        *   Hệ thống hiển thị biểu mẫu nhập liệu.
        *   Actor nhập/chọn thông tin: Học sinh, Môn học, Học kỳ, Điểm số.
        *   Actor xác nhận lưu.
        *   Hệ thống kiểm tra tính hợp lệ của dữ liệu và lưu bản ghi.
        *   Hệ thống hiển thị thông báo thành công và cập nhật danh sách.
    4.  **Để xem chi tiết**:
        *   Actor chọn một bản ghi điểm từ danh sách.
        *   Hệ thống hiển thị thông tin chi tiết của bản ghi điểm.
    5.  **Để sửa**:
        *   Actor chọn một bản ghi điểm từ danh sách và chọn "Sửa".
        *   Hệ thống hiển thị biểu mẫu với thông tin điểm đã có.
        *   Actor chỉnh sửa thông tin cần thiết (ví dụ: Điểm số).
        *   Actor xác nhận lưu.
        *   Hệ thống kiểm tra tính hợp lệ của dữ liệu và cập nhật bản ghi.
        *   Hệ thống hiển thị thông báo thành công và cập nhật danh sách.
    6.  **Để xóa**:
        *   Actor chọn một hoặc nhiều bản ghi điểm từ danh sách và chọn "Xóa".
        *   Hệ thống yêu cầu xác nhận xóa.
        *   Actor xác nhận xóa.
        *   Hệ thống xóa bản ghi điểm.
        *   Hệ thống hiển thị thông báo thành công và cập nhật danh sách.
*   **Luồng thay thế/ngoại lệ**:
    *   **QUAN-20260530-2249-UC01a (Lỗi xác thực dữ liệu)**: Nếu dữ liệu nhập vào không hợp lệ (ví dụ: điểm số ngoài khoảng cho phép, bỏ trống trường bắt buộc), hệ thống hiển thị thông báo lỗi chi tiết cho từng trường.
    *   **QUAN-20260530-2249-UC01b (Trùng lặp bản ghi)**: Nếu Actor cố gắng tạo một bản ghi điểm mà đã tồn tại điểm cho cùng một Học sinh, Môn học và Học kỳ, hệ thống hiển thị thông báo lỗi và không cho phép tạo mới.
    *   **QUAN-20260530-2249-UC01c (Không có quyền)**: Nếu Actor không có quyền thực hiện hành động (ví dụ: giáo viên không được phân công cho lớp/môn học đó), hệ thống hiển thị thông báo "Không có quyền truy cập" và ngăn chặn hành động.
    *   **QUAN-20260530-2249-UC01d (Hủy bỏ)**: Tại bất kỳ bước nào trong quá trình thêm mới, sửa, xóa, Actor có thể chọn "Hủy bỏ" để quay lại danh sách mà không lưu thay đổi.
*   **Điều kiện sau**:
    *   Một bản ghi điểm mới được tạo hoặc một bản ghi điểm hiện có được cập nhật/xóa thành công trong hệ thống.

## 7. Yêu cầu chức năng (Functional Requirements - FRs)

### 7.1. Quản lý chung

*   **QUAN-20260530-2249-FR01**: Hệ thống PHẢI cho phép người dùng xem danh sách tất cả các bản ghi điểm học kỳ.
    *   **Acceptance Criteria**:
        *   Khi người dùng truy cập vào trang "Quản lý Điểm Học Kỳ", hệ thống hiển thị bảng danh sách các điểm.
        *   Mỗi dòng trong bảng hiển thị ít nhất các thông tin: Học sinh (Tên, Mã), Môn học, Học kỳ, Điểm số.
        *   Danh sách PHẢI được hiển thị dưới dạng bảng có các cột rõ ràng.
*   **QUAN-20260530-2249-FR02**: Hệ thống PHẢI hỗ trợ phân trang (pagination) cho danh sách điểm học kỳ.
    *   **Acceptance Criteria**:
        *   Người dùng có thể cấu hình số lượng bản ghi hiển thị trên mỗi trang (ví dụ: 10, 20, 50 bản ghi).
        *   Hệ thống PHẢI hiển thị tổng số bản ghi và thông tin trang hiện tại (ví dụ: "Hiển thị 1-10 trên 100 kết quả", "Trang 1/10").
        *   Người dùng có thể di chuyển giữa các trang (trước, sau, đến trang cụ thể).

### 7.2. Thêm mới Điểm Học Kỳ (Create)

*   **QUAN-20260530-2249-FR03**: Hệ thống PHẢI cho phép người dùng có quyền thêm mới một bản ghi điểm học kỳ.
    *   **Acceptance Criteria**:
        *   Trên giao diện quản lý điểm, có nút "Thêm mới điểm".
        *   Nhấn vào nút "Thêm mới điểm" sẽ hiển thị form nhập liệu.
        *   Form nhập liệu bao gồm các trường:
            *   Học sinh (chọn từ danh sách, có thể tìm kiếm theo mã/tên).
            *   Môn học (chọn từ danh sách).
            *   Học kỳ (chọn từ danh sách).
            *   Điểm số (nhập liệu dạng số).
        *   Có nút "Lưu" và "Hủy".
*   **QUAN-20260530-2249-FR04**: Hệ thống PHẢI tự động kiểm tra và ngăn chặn việc tạo trùng lặp điểm số cho cùng một Học sinh, Môn học và Học kỳ.
    *   **Acceptance Criteria**:
        *   Nếu người dùng cố gắng thêm một bản ghi điểm mà đã có điểm cho {Học sinh X, Môn học Y, Học kỳ Z}, hệ thống PHẢI hiển thị thông báo lỗi: "Điểm cho học sinh [Tên Học sinh] môn [Tên Môn học] học kỳ [Tên Học kỳ] đã tồn tại." và không cho phép lưu.

### 7.3. Cập nhật Điểm Học Kỳ (Update)

*   **QUAN-20260530-2249-FR05**: Hệ thống PHẢI cho phép người dùng có quyền chỉnh sửa thông tin của một bản ghi điểm học kỳ đã tồn tại.
    *   **Acceptance Criteria**:
        *   Trên mỗi dòng của danh sách điểm, có nút "Sửa" hoặc biểu tượng tương ứng.
        *   Nhấn vào nút "Sửa" sẽ hiển thị form nhập liệu đã điền sẵn thông tin của bản ghi đó.
        *   Người dùng có thể chỉnh sửa trường "Điểm số". Các trường Học sinh, Môn học, Học kỳ KHÔNG ĐƯỢC phép sửa.
        *   Có nút "Lưu" và "Hủy".
        *   Sau khi lưu thành công, hệ thống hiển thị thông báo thành công và cập nhật danh sách.

### 7.4. Xóa Điểm Học Kỳ (Delete)

*   **QUAN-20260530-2249-FR06**: Hệ thống PHẢI cho phép người dùng có quyền xóa một hoặc nhiều bản ghi điểm học kỳ.
    *   **Acceptance Criteria**:
        *   Trên mỗi dòng của danh sách điểm, có nút "Xóa" hoặc biểu tượng tương ứng.
        *   Có thể chọn nhiều bản ghi để xóa đồng thời (checkbox).
        *   Khi nhấn "Xóa", hệ thống PHẢI hiển thị cửa sổ xác nhận với nội dung "Bạn có chắc chắn muốn xóa [số lượng] bản ghi điểm này không?".
        *   Nếu người dùng xác nhận, bản ghi điểm sẽ bị xóa khỏi hệ thống và danh sách được cập nhật.
        *   Hệ thống hiển thị thông báo thành công sau khi xóa.

### 7.5. Tìm kiếm Điểm Học Kỳ (Search)

*   **QUAN-20260530-2249-FR07**: Hệ thống PHẢI cho phép người dùng tìm kiếm điểm học kỳ theo các tiêu chí khác nhau.
    *   **Acceptance Criteria**:
        *   Trên giao diện quản lý điểm, có một khu vực tìm kiếm.
        *   Các tiêu chí tìm kiếm bao gồm:
            *   Tên Học sinh / Mã Học sinh (tìm kiếm tương đối).
            *   Môn học (chọn từ danh sách dropdown).
            *   Học kỳ (chọn từ danh sách dropdown).
        *   Hệ thống PHẢI hiển thị kết quả tìm kiếm theo thời gian thực (real-time) hoặc sau khi nhấn nút "Tìm kiếm".
        *   Chức năng phân trang PHẢI áp dụng cho kết quả tìm kiếm.

### 7.6. Validation (Kiểm tra dữ liệu)

*   **QUAN-20260530-2249-FR08**: Hệ thống PHẢI thực hiện kiểm tra dữ liệu đầu vào cho trường Điểm số.
    *   **Acceptance Criteria**:
        *   Điểm số PHẢI là một số thực hoặc số nguyên.
        *   Điểm số PHẢI nằm trong khoảng từ 0 đến 10 (hoặc 0 đến 100 tùy theo cấu hình hệ thống).
        *   Nếu Điểm số không hợp lệ, hệ thống hiển thị thông báo lỗi dưới trường nhập liệu (ví dụ: "Điểm số phải từ 0 đến 10").
*   **QUAN-20260530-2249-FR09**: Hệ thống PHẢI kiểm tra các trường bắt buộc không được bỏ trống.
    *   **Acceptance Criteria**:
        *   Các trường Học sinh, Môn học, Học kỳ, Điểm số PHẢI là bắt buộc.
        *   Nếu người dùng bỏ trống một trong các trường bắt buộc, hệ thống PHẢI hiển thị thông báo lỗi dưới trường nhập liệu (ví dụ: "Vui lòng chọn học sinh").

## 8. Quy tắc nghiệp vụ (Business Rules - BRs)

*   **QUAN-20260530-2249-BR01**: Một học sinh CHỈ CÓ THỂ có MỘT điểm duy nhất cho MỘT môn học trong MỘT học kỳ cụ thể.
    *   **Ví dụ**: Học sinh Nguyễn Văn A không thể có hai điểm cho môn Toán học kỳ I năm học 2026-2027. Nếu giáo viên cố gắng nhập, hệ thống sẽ báo lỗi trùng lặp.
*   **QUAN-20260530-2249-BR02**: Chỉ Giáo viên được phân công giảng dạy cho một lớp học và môn học cụ thể trong một học kỳ mới có quyền nhập/sửa/xóa điểm cho học sinh của lớp học đó.
    *   **Ví dụ**: Cô giáo Trần Thị B dạy môn Văn lớp 10A2 học kỳ I. Cô B có thể nhập điểm Văn cho học sinh 10A2. Cô B không thể nhập điểm Toán cho 10A2 hoặc điểm Văn cho 11A1.
*   **QUAN-20260530-2249-BR03**: Điểm số PHẢI được nhập dưới dạng số và nằm trong phạm vi từ 0 đến 10 (bao gồm cả giá trị thập phân một chữ số, ví dụ 7.5).
    *   **Ví dụ**: Nếu giáo viên nhập "11" hoặc "-1" hoặc "a.b" vào trường điểm, hệ thống sẽ báo lỗi. Nếu nhập "8.5", hệ thống chấp nhận.
*   **QUAN-20260530-2249-BR04**: Sau khi điểm của một học kỳ đã được "Chốt" bởi Ban Giám Hiệu, Giáo viên KHÔNG CÓ QUYỀN sửa hoặc xóa điểm của học kỳ đó nữa. Quản trị viên hệ thống có thể có quyền đặc biệt để ghi đè (override) trong trường hợp có lý do chính đáng và được ghi lại.
    *   **Ví dụ**: Sau ngày 15/01/2027, điểm học kỳ I năm học 2026-2027 đã được BGH chốt. Giáo viên cố gắng sửa điểm môn Lý của học sinh C trong học kỳ này sẽ nhận được thông báo lỗi "Điểm đã chốt, không thể sửa".

## 9. Yêu cầu phi chức năng (Non-Functional Requirements - NFRs)

*   **Hiệu năng (Performance)**:
    *   Hệ thống PHẢI hiển thị danh sách điểm (với 1000 bản ghi) trong vòng tối đa 2 giây.
    *   Các thao tác thêm/sửa/xóa điểm PHẢI được xử lý trong vòng tối đa 1 giây.
*   **Bảo mật (Security)**:
    *   Truy cập vào các chức năng quản lý điểm PHẢI được kiểm soát thông qua xác thực và phân quyền (dựa trên vai trò và môn học/lớp học được phân công).
    *   Dữ liệu điểm PHẢI được mã hóa khi truyền tải qua mạng (HTTPS).
    *   Hệ thống PHẢI có cơ chế ghi nhật ký (logging) các thao tác thêm/sửa/xóa điểm, bao gồm người thực hiện, thời gian và nội dung thay đổi.
*   **Khả năng sử dụng (Usability)**:
    *   Giao diện người dùng PHẢI thân thiện, dễ hiểu và dễ sử dụng cho giáo viên.
    *   Các thông báo lỗi và thành công PHẢI rõ ràng, dễ hiểu và có hướng dẫn nếu cần.
    *   Hệ thống PHẢI hỗ trợ điều hướng trực quan giữa các trang chức năng.
*   **Khả năng mở rộng (Scalability)**:
    *   Hệ thống PHẢI có khả năng xử lý số lượng lớn bản ghi điểm (ví dụ: hàng trăm nghìn bản ghi) mà không làm giảm hiệu năng đáng kể.
    *   Kiến trúc hệ thống PHẢI cho phép dễ dàng thêm các trường dữ liệu điểm mới hoặc các loại điểm mới trong tương lai.
*   **Khả năng tương thích (Compatibility)**:
    *   Giao diện quản lý điểm PHẢI hiển thị chính xác và hoạt động tốt trên các trình duyệt web phổ biến (Chrome, Firefox, Edge, Safari) với các phiên bản mới nhất.
    *   Giao diện PHẢI có khả năng phản hồi (responsive) để sử dụng được trên các thiết bị có kích thước màn hình khác nhau (máy tính, máy tính bảng).

## 10. Rủi ro và Ràng buộc

### 10.1. Rủi ro

*   **Rủi ro nhập liệu sai**: Giáo viên có thể nhập sai điểm số.
    *   **Giải pháp giảm thiểu**: Cung cấp xác thực mạnh mẽ, giao diện thân thiện, chức năng xem lại trước khi lưu.
*   **Rủi ro phân quyền không chính xác**: Giáo viên có thể được cấp quyền sai, dẫn đến việc truy cập hoặc chỉnh sửa điểm không được phép.
    *   **Giải pháp giảm thiểu**: Quy trình quản lý phân quyền rõ ràng, kiểm tra chặt chẽ, kiểm thử phân quyền nghiêm ngặt.
*   **Rủi ro hiệu năng**: Hệ thống có thể chậm khi xử lý lượng lớn dữ liệu hoặc nhiều người dùng truy cập cùng lúc.
    *   **Giải pháp giảm thiểu**: Tối ưu hóa cơ sở dữ liệu, sử dụng caching, cân bằng tải.
*   **Rủi ro thiếu dữ liệu gốc**: Nếu dữ liệu về Học sinh, Môn học, Học kỳ không đầy đủ hoặc không chính xác, việc nhập điểm sẽ gặp khó khăn.
    *   **Giải pháp giảm thiểu**: Đảm bảo các module quản lý dữ liệu gốc được hoàn thiện và ổn định trước.

### 10.2. Ràng buộc

*   **Thời gian và ngân sách**: Dự án có giới hạn về thời gian và nguồn lực. Các yêu cầu PHẢI được ưu tiên để phù hợp với các ràng buộc này.
*   **Hệ thống hiện có**: Tính năng PHẢI tích hợp được với các module hiện có của hệ thống `quanlyhocsinh` (ví dụ: Quản lý Học sinh, Quản lý Môn học, Quản lý Lớp học).
*   **Công nghệ**: Hệ thống PHẢI được phát triển dựa trên nền tảng công nghệ hiện tại của ONENET để đảm bảo tính nhất quán và khả năng bảo trì.
*   **Quy định giáo dục**: Tính năng PHẢI tuân thủ các quy định hiện hành của Bộ Giáo dục và Đào tạo về quản lý điểm số học sinh (nếu có yêu cầu cụ thể).

---
**Lưu ý Bảo Mật**:
*   Tài liệu này là output nghiệp vụ và TUYỆT ĐỐI KHÔNG được tạo, ghi, hoặc commit vào thư mục gốc của ONENET.AgentFactory.
*   Tất cả output PHẢI được commit vào GitHub repository của DỰ ÁN ĐÍCH thông qua API.

---