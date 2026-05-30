// QUAN-20260530-0942
package com.onenet.quanlyhocsinh.entity;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.annotations.Where;
import org.springframework.data.annotation.CreatedBy;
import org.springframework.data.annotation.CreatedDate;
import org.springframework.data.annotation.LastModifiedBy;
import org.springframework.data.annotation.LastModifiedDate;
import org.springframework.data.jpa.domain.support.AuditingEntityListener;

import java.time.LocalDate;
import java.time.OffsetDateTime;

@Entity
@Table(name = "hoc_sinh")
@Data
@NoArgsConstructor
@AllArgsConstructor
@EntityListeners(AuditingEntityListener.class)
@Where(clause = "trang_thai != 'Đã xóa'") // Default filter for soft-deleted students
public class HocSinh {

    @Id
    @Column(name = "ma_hoc_sinh", length = 20, unique = true, nullable = false)
    private String maHocSinh;

    @Column(name = "ho_ten", length = 100, nullable = false)
    private String hoTen;

    @Column(name = "ngay_sinh", nullable = false)
    private LocalDate ngaySinh;

    @Column(name = "gioi_tinh", length = 10, nullable = false)
    private String gioiTinh; // Enum: Nam, Nữ, Khác

    @Column(name = "dia_chi", length = 255)
    private String diaChi;

    @Column(name = "so_dien_thoai_ph", length = 15)
    private String soDienThoaiPH;

    @Column(name = "email_ph", length = 100)
    private String emailPH;

    @Column(name = "ma_lop_hoc", length = 20, nullable = false)
    private String maLopHoc; // Foreign key reference to LopHoc (handled by service integration)

    @Column(name = "trang_thai", length = 20, nullable = false)
    private String trangThai; // Enum: Đang học, Thôi học, Tạm nghỉ, Đã xóa

    @CreatedDate
    @Column(name = "ngay_tao", nullable = false, updatable = false)
    private OffsetDateTime ngayTao;

    @CreatedBy
    @Column(name = "nguoi_tao", length = 50, nullable = false, updatable = false)
    private String nguoiTao;

    @LastModifiedDate
    @Column(name = "ngay_cap_nhat_cuoi")
    private OffsetDateTime ngayCapNhatCuoi;

    @LastModifiedBy
    @Column(name = "nguoi_cap_nhat_cuoi", length = 50)
    private String nguoiCapNhatCuoi;
}