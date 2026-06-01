// QUAN-20260530-2301
export interface StudentListDto {
  id: string;
  maHocSinh: string;
  hoVaTen: string;
  tenLop: string;
  tenTrangThai: string;
}

export interface StudentDetailDto {
  id: string;
  maHocSinh: string;
  hoVaTen: string;
  ngaySinh: string; // ISO 8601 string
  gioiTinh: string;
  diaChi?: string;
  sdtPhuHuynh?: string;
  emailPhuHuynh?: string;
  lopId: string;
  tenLop: string;
  ngayNhapHoc: string; // ISO 8601 string
  trangThaiId: string;
  tenTrangThai: string;
  createdAt: string;
  createdBy?: string;
  updatedAt?: string;
  updatedBy?: string;
}

export interface CreateStudentCommand {
  maHocSinh: string;
  hoVaTen: string;
  ngaySinh: string; // ISO 8601 string (date only, e.g., "2009-03-20")
  gioiTinh: string;
  diaChi?: string;
  sdtPhuHuynh?: string;
  emailPhuHuynh?: string;
  lopId: string;
  ngayNhapHoc: string; // ISO 8601 string (date only, e.g., "2024-09-05")
  trangThaiId: string;
}

export interface UpdateStudentCommand {
  id: string;
  maHocSinh?: string;
  hoVaTen?: string;
  ngaySinh?: string;
  gioiTinh?: string;
  diaChi?: string;
  sdtPhuHuynh?: string;
  emailPhuHuynh?: string;
  lopId?: string;
  ngayNhapHoc?: string;
  trangThaiId?: string;
}

export interface StudentListFilter {
  pageNumber?: number;
  pageSize?: number;
  searchQuery?: string;
  lopId?: string;
  trangThaiId?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

export interface LopDto {
  id: string;
  tenLop: string;
  moTa?: string;
}

export interface TrangThaiHocSinhDto {
  id: string;
  maTrangThai: string;
  tenTrangThai: string;
  moTa?: string;
}