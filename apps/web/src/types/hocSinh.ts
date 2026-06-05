// QUAN-20260604-153038
import { PaginatedList as BasePaginatedList } from './api';

export type GioiTinh = 'Nam' | 'Nu' | 'Khac';
export type TrangThaiHocSinh = 'DangHoc' | 'DaTotNghiep' | 'DaChuyenTruong' | 'TamDung';

export interface HocSinh {
  id: string;
  maHocSinh: string;
  hoTen: string;
  ngaySinh: string; // ISO date string (e.g., "YYYY-MM-DD")
  gioiTinh: GioiTinh;
  diaChi?: string;
  soDienThoaiPH?: string;
  emailPH?: string;
  lopHocId: string;
  tenLop: string;
  trangThai: TrangThaiHocSinh;
  createdAt: string;
  createdBy: string;
  updatedAt?: string;
  updatedBy?: string;
}

export interface HocSinhFormValues {
  id?: string; // Optional for create
  hoTen: string;
  ngaySinh: string;
  gioiTinh: GioiTinh;
  diaChi?: string;
  soDienThoaiPH?: string;
  emailPH?: string;
  lopHocId: string;
  trangThai: TrangThaiHocSinh;
}

export interface HocSinhSearchParams {
  searchTerm?: string;
  lopHocId?: string;
  trangThai?: TrangThaiHocSinh;
  pageNumber?: number;
  pageSize?: number;
}

export interface LopHocLookup {
  id: string;
  tenLop: string;
}

export interface PaginatedList<T> extends BasePaginatedList<T> {}