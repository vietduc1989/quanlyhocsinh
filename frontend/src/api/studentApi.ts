import axios from 'axios';

const API_BASE_URL = '/api/students';

export interface Student {
  id?: string;
  studentCode: string;
  fullName: string;
  dateOfBirth: string;
  gender: string;
  address?: string;
  phoneNumber?: string;
  email?: string;
  classId: string;
  className?: string;
  parentName?: string;
  parentPhoneNumber?: string;
  status?: number;
  statusText?: string;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface GetStudentsParams {
  searchTerm?: string;
  classId?: string;
  pageNumber: number;
  pageSize: number;
}

export const studentApi = {
  getStudents: async (params: GetStudentsParams): Promise<PagedResult<Student>> => {
    const response = await axios.get<PagedResult<Student>>(API_BASE_URL, { params });
    return response.data;
  },

  getStudentById: async (id: string): Promise<Student> => {
    const response = await axios.get<Student>(`${API_BASE_URL}/${id}`);
    return response.data;
  },

  createStudent: async (student: Student): Promise<string> => {
    const response = await axios.post<string>(API_BASE_URL, student);
    return response.data;
  },

  updateStudent: async (id: string, student: Student): Promise<void> => {
    await axios.put(`${API_BASE_URL}/${id}`, student);
  },

  deleteStudent: async (id: string): Promise<void> => {
    await axios.delete(`${API_BASE_URL}/${id}`);
  }
};