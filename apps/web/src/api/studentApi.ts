// QUAN-20260530-0942
import apiClient from './apiClient';
import { Student, StudentRequest, PaginationResponse } from '../types/student';

const STUDENT_API_PREFIX = '/students';

export const getStudents = async (
  page: number = 0,
  size: number = 10,
  sortBy: string = 'maHocSinh',
  sortDir: 'asc' | 'desc' = 'asc',
  keyword: string = '',
  maLopHoc: string = ''
): Promise<PaginationResponse<Student>> => {
  const params = {
    page,
    size,
    sortBy,
    sortDir,
    keyword,
    maLopHoc,
  };

  const response = await apiClient.get<PaginationResponse<Student>>(STUDENT_API_PREFIX, { params });
  return response.data;
};

export const getStudentById = async (maHocSinh: string): Promise<Student> => {
  const response = await apiClient.get<Student>(`${STUDENT_API_PREFIX}/${maHocSinh}`);
  return response.data;
};

export const createStudent = async (student: StudentRequest): Promise<Student> => {
  const response = await apiClient.post<Student>(STUDENT_API_PREFIX, student);
  return response.data;
};

export const updateStudent = async (maHocSinh: string, student: StudentRequest): Promise<Student> => {
  const response = await apiClient.put<Student>(`${STUDENT_API_PREFIX}/${maHocSinh}`, student);
  return response.data;
};

export const deleteStudent = async (maHocSinh: string): Promise<void> => {
  await apiClient.delete(`${STUDENT_API_PREFIX}/${maHocSinh}`);
};