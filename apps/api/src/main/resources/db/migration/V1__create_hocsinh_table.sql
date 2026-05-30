-- QUAN-20260530-0942
CREATE TABLE hoc_sinh (
    ma_hoc_sinh VARCHAR(20) PRIMARY KEY NOT NULL UNIQUE,
    ho_ten VARCHAR(100) NOT NULL,
    ngay_sinh DATE NOT NULL,
    gioi_tinh VARCHAR(10) NOT NULL CHECK (gioi_tinh IN ('Nam', 'Nữ', 'Khác')),
    dia_chi VARCHAR(255),
    so_dien_thoai_ph VARCHAR(15),
    email_ph VARCHAR(100),
    ma_lop_hoc VARCHAR(20) NOT NULL, -- FK, assuming lop_hoc table exists in a related DB or microservice context
    trang_thai VARCHAR(20) NOT NULL DEFAULT 'Đang học' CHECK (trang_thai IN ('Đang học', 'Thôi học', 'Tạm nghỉ', 'Đã xóa')),
    ngay_tao TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    nguoi_tao VARCHAR(50) NOT NULL,
    ngay_cap_nhat_cuoi TIMESTAMP WITH TIME ZONE,
    nguoi_cap_nhat_cuoi VARCHAR(50)
);

CREATE UNIQUE INDEX idx_hocsinh_ma_hoc_sinh ON hoc_sinh (ma_hoc_sinh);
CREATE INDEX idx_hocsinh_ho_ten ON hoc_sinh (ho_ten);
CREATE INDEX idx_hocsinh_ma_lop_hoc ON hoc_sinh (ma_lop_hoc);
CREATE INDEX idx_hocsinh_trang_thai ON hoc_sinh (trang_thai);

-- Note: The FK constraint to lop_hoc table might be managed externally if LopHoc is a separate microservice.
-- If LopHoc table is in the same database:
-- ALTER TABLE hoc_sinh
-- ADD CONSTRAINT fk_ma_lop_hoc
-- FOREIGN KEY (ma_lop_hoc) REFERENCES lop_hoc (ma_lop_hoc);