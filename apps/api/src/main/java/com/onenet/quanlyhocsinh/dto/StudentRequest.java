// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.dto;

import com.onenet.quanlyhocsinh.validation.ValidMaLopHoc;
import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.format.annotation.DateTimeFormat;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class StudentRequest {

    // MaHocSinh is only allowed for creation, not for update in the path variable
    // For update, it's identified by path variable, for creation, it's part of the request body
    @NotBlank(message = "Mã học sinh không được để trống")
    @Size(max = 20, message = "Mã học sinh không được vượt quá 20 ký tự")
    @Pattern(regexp = "^[a-zA-Z0-9_-]*$", message = "Mã học sinh chỉ chứa chữ cái, số, dấu gạch ngang/dưới")
    private String maHocSinh;

    @NotBlank(message = "Họ và tên không được để trống")
    @Size(max = 100, message = "Họ và tên không được vượt quá 100 ký tự")
    private String hoTen;

    @NotNull(message = "Ngày sinh không được để trống")
    @PastOrPresent(message = "Ngày sinh không được là ngày trong tương lai")
    @DateTimeFormat(iso = DateTimeFormat.ISO.DATE)
    private LocalDate ngaySinh; // YYYY-MM-DD

    @NotBlank(message = "Giới tính không được để trống")
    @Pattern(regexp = "Nam|Nữ|Khác", message = "Giới tính phải là 'Nam', 'Nữ' hoặc 'Khác'")
    private String gioiTinh;

    @Size(max = 255, message = "Địa chỉ không được vượt quá 255 ký tự")
    private String diaChi;

    @Pattern(regexp = "^(0|\\+84)([3|5|7|8|9])+([0-9]{8})$|^$", message = "Số điện thoại phụ huynh không hợp lệ (ví dụ: 0XXXXXXXXX hoặc +84XXXXXXXXX)")
    private String soDienThoaiPH;

    @Email(message = "Email phụ huynh không hợp lệ")
    @Size(max = 100, message = "Email phụ huynh không được vượt quá 100 ký tự")
    private String emailPH;

    @NotBlank(message = "Mã lớp học không được để trống")
    @ValidMaLopHoc // Custom validation to check existence in LopHoc module
    private String maLopHoc;

    @NotBlank(message = "Trạng thái không được để trống")
    @Pattern(regexp = "Đang học|Thôi học|Tạm nghỉ|Đã xóa", message = "Trạng thái phải là 'Đang học', 'Thôi học', 'Tạm nghỉ' hoặc 'Đã xóa'")
    private String trangThai;
}