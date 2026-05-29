/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Frontend (TypeScript Types)
 */

export interface Student {
  id: string;
  studentCode: string;
  fullName: string;
  dateOfBirth: string; // ISO format or YYYY-MM-DD
  gender: 'Nam' | 'Nữ' | 'Khác';
  address?: string;
  parentPhone: string;
  email?: string;
  createdAt?: string;
}

export interface Pagination {
  currentPage: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
}

export interface StudentsResponse {
  success: boolean;
  data: Student[];
  pagination: Pagination;
}