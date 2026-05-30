// QUAN-20260530-0942
export interface Student {
    maHocSinh: string;
    hoTen: string;
    ngaySinh: string; // YYYY-MM-DD format
    gioiTinh: 'Nam' | 'Nữ' | 'Khác';
    diaChi?: string;
    soDienThoaiPH?: string;
    emailPH?: string;
    maLopHoc: string;
    trangThai: 'Đang học' | 'Thôi học' | 'Tạm nghỉ' | 'Đã xóa';
    ngayTao: string; // ISO 8601 string (OffsetDateTime)
    nguoiTao: string;
    ngayCapNhatCuoi?: string; // ISO 8601 string (OffsetDateTime)
    nguoiCapNhatCuoi?: string;
}

// DTO for creating/updating a student
export interface StudentRequest {
    maHocSinh?: string; // Optional for update, required for create
    hoTen: string;
    ngaySinh: string;
    gioiTinh: 'Nam' | 'Nữ' | 'Khác';
    diaChi?: string;
    soDienThoaiPH?: string;
    emailPH?: string;
    maLopHoc: string;
    trangThai: 'Đang học' | 'Thôi học' | 'Tạm nghỉ' | 'Đã xóa';
}

// Generic pagination response structure
export interface PaginationResponse<T> {
    content: T[];
    pageNo: number;
    pageSize: number;
    totalElements: number;
    totalPages: number;
    last: boolean;
}