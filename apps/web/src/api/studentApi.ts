import { axiosInstance } from './axiosInstance';
import type {
  ApiResponse,
  PagedResult,
  Student,
  StudentDetail,
  CreateStudentPayload,
  UpdateStudentPayload,
  LookupItem,
  ApiError
} from '../types/common';

// QUAN-20260530-2301-SRS01, QUAN-20260530-2301-SRS07, QUAN-20260530-2301-SRS08
interface GetStudentsParams {
  pageNumber?: number;
  pageSize?: number;
  searchQuery?: string;
  lopId?: string;
  trangThaiId?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

export const getStudents = async (params?: GetStudentsParams): Promise<ApiResponse<PagedResult<Student>>> => {
  const response = await axiosInstance.get<ApiResponse<PagedResult<Student>>>('/students', { params });
  return response.data;
};

// QUAN-20260530-2301-SRS02
export const getStudentById = async (id: string): Promise<ApiResponse<StudentDetail>> => {
  const response = await axiosInstance.get<ApiResponse<StudentDetail>>(`/students/${id}`);
  return response.data;
};

// QUAN-20260530-2301-SRS03
export const createStudent = async (payload: CreateStudentPayload): Promise<ApiResponse<{ id: string }>> => {
  const response = await axiosInstance.post<ApiResponse<{ id: string }>>('/students', payload);
  return response.data;
};

// QUAN-20260530-2301-SRS04
export const updateStudent = async (id: string, payload: UpdateStudentPayload): Promise<ApiResponse<null>> => {
  const response = await axiosInstance.put<ApiResponse<null>>(`/students/${id}`, payload);
  return response.data;
};

// QUAN-20260530-2301-SRS05, QUAN-20260530-2301-SRS06
export const deleteStudent = async (id: string): Promise<ApiResponse<null>> => {
  const response = await axiosInstance.delete<ApiResponse<null>>(`/students/${id}`);
  return response.data;
};

// Assuming you also need to fetch lookup data for Lop and TrangThaiHocSinh
export const getLops = async (): Promise<ApiResponse<LookupItem[]>> => {
  // This endpoint is assumed to exist for fetching lookup data.
  // The backend setup has configured Lop and TrangThaiHocSinh entities.
  // A controller and handlers for this would be in another feature or common module.
  // For now, we'll assume a dummy endpoint or create a placeholder.
  // Example: '/api/v1/lops'
  const response = await axiosInstance.get<ApiResponse<LookupItem[]>>('/lops');
  return response.data;
};

export const getTrangThais = async (): Promise<ApiResponse<LookupItem[]>> => {
  // Example: '/api/v1/trangthaisinhvien'
  const response = await axiosInstance.get<ApiResponse<LookupItem[]>>('/trangthaisinhvien');
  return response.data;
};
