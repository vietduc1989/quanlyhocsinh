// QUAN-20260530-2301
import axios from 'axios';
import type { ApiResponse, PaginatedResponse } from '../types/ApiResponse';
import type {
  StudentListDto,
  StudentDetailDto,
  CreateStudentCommand,
  UpdateStudentCommand,
  StudentListFilter,
  LopDto,
  TrangThaiHocSinhDto,
} from '../types/student';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api/v1';

const studentApi = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor for JWT token (placeholder)
studentApi.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwt_token'); // Get token from local storage or context
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export const getStudents = async (
  filters: StudentListFilter
): Promise<ApiResponse<PaginatedResponse<StudentListDto>>> => {
  const response = await studentApi.get<ApiResponse<PaginatedResponse<StudentListDto>>>('/students', {
    params: filters,
  });
  return response.data;
};

export const getStudentById = async (id: string): Promise<ApiResponse<StudentDetailDto>> => {
  const response = await studentApi.get<ApiResponse<StudentDetailDto>>(`/students/${id}`);
  return response.data;
};

export const createStudent = async (data: CreateStudentCommand): Promise<ApiResponse<{ id: string }>> => {
  const response = await studentApi.post<ApiResponse<{ id: string }>>('/students', data);
  return response.data;
};

export const updateStudent = async (data: UpdateStudentCommand): Promise<ApiResponse> => {
  const response = await studentApi.put<ApiResponse>(`/students/${data.id}`, data);
  return response.data;
};

export const deleteStudent = async (id: string): Promise<ApiResponse> => {
  const response = await studentApi.delete<ApiResponse>(`/students/${id}`);
  return response.data;
};

export const getLops = async (): Promise<ApiResponse<LopDto[]>> => {
  // Assuming a /lops endpoint exists for lookup tables
  const response = await studentApi.get<ApiResponse<LopDto[]>>('/lops');
  return response.data;
};

export const getTrangThaiHocSinhs = async (): Promise<ApiResponse<TrangThaiHocSinhDto[]>> => {
  // Assuming a /trangthaihocsinh endpoint exists for lookup tables
  const response = await studentApi.get<ApiResponse<TrangThaiHocSinhDto[]>>('/trangthaihocsinh');
  return response.data;
};