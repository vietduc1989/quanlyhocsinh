// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;
import java.time.OffsetDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class StudentResponse {
    private String maHocSinh;
    private String hoTen;
    private LocalDate ngaySinh;
    private String gioiTinh;
    private String diaChi;
    private String soDienThoaiPH;
    private String emailPH;
    private String maLopHoc;
    private String trangThai;
    private OffsetDateTime ngayTao;
    private String nguoiTao;
    private OffsetDateTime ngayCapNhatCuoi;
    private String nguoiCapNhatCuoi;
}