// QUAN-20260530-2301

export interface ApiError {
  field?: string;
  message: string;
  code?: string;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string | null;
  data: T | null;
  errors: ApiError[];
}

export interface PagedResult<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface LookupItem {
  id: string;
  name: string;
}

// Student DTO for list view
export interface Student {
  id: string;
  maHocSinh: string;
  hoVaTen: string;
  lopHoc: string; // TenLop
  trangThai: string; // TenTrangThai
}

// Student DTO for detail view
export interface StudentDetail {
  id: string;
  maHocSinh: string;
  hoVaTen: string;
  ngaySinh: string; // ISO 8601 string
  gioiTinh: 'Nam' | 'Nu' | 'Khac'; // Enum converted to string
  diaChi: string | null;
  sdtPhuHuynh: string | null;
  emailPhuHuynh: string | null;
  lopId: string;
  tenLop: string;
  ngayNhapHoc: string; // ISO 8601 string
  trangThaiId: string;
  tenTrangThai: string;
  ngayTao: string; // ISO 8601 string
  nguoiTao: string | null;
  ngayCapNhat: string | null; // ISO 8601 string
  nguoiCapNhat: string | null;
}

// Payload for creating a new student
export interface CreateStudentPayload {
  maHocSinh: string;
  hoVaTen: string;
  ngaySinh: string; // ISO 8601 string
  gioiTinh: 'Nam' | 'Nu' | 'Khac';
  diaChi: string | null;
  sdtPhuHuynh: string | null;
  emailPhuHuynh: string | null;
  lopId: string;
  ngayNhapHoc: string; // ISO 8601 string
  trangThaiId: string;
}

// Payload for updating an existing student
export interface UpdateStudentPayload {
  id: string; // Include ID for consistency with API, though it's also in URL
  maHocSinh?: string;
  hoVaTen?: string;
  ngaySinh?: string; // ISO 8601 string
  gioiTinh?: 'Nam' | 'Nu' | 'Khac';
  diaChi?: string | null;
  sdtPhuHuynh?: string | null;
  emailPhuHuynh?: string | null;
  lopId?: string;
  ngayNhapHoc?: string; // ISO 8601 string
  trangThaiId?: string;
}
