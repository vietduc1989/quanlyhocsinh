/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Frontend (API Client)
 */

import axios from 'axios';
import { Student, StudentsResponse } from '../types/student';

const API_BASE_URL = '/api/v1/students';

export const studentApi = {
  getStudents: async (searchQuery: string, page: number): Promise<StudentsResponse> => {
    const response = await axios.get<StudentsResponse>(API_BASE_URL, {
      params: { searchQuery, page, pageSize: 20 },
    });
    return response.data;
  },

  createStudent: async (student: Omit<Student, 'id' | 'studentCode'>): Promise<any> => {
    const response = await axios.post(API_BASE_URL, student);
    return response.data;
  },

  updateStudent: async (id: string, student: Omit<Student, 'id' | 'studentCode'>): Promise<any> => {
    const response = await axios.put(`${API_BASE_URL}/${id}`, student);
    return response.data;
  },

  deleteStudent: async (id: string): Promise<any> => {
    const response = await axios.delete(`${API_BASE_URL}/${id}`);
    return response.data;
  },

  exportStudentsUrl: (searchQuery: string) => {
    return `${API_BASE_URL}/export?searchQuery=${encodeURIComponent(searchQuery)}`;
  },

  importStudents: async (file: File): Promise<any> => {
    const formData = new FormData();
    formData.append('file', file);
    const response = await axios.post(`${API_BASE_URL}/import`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
      responseType: 'blob', // Để nhận file lỗi nếu import lỗi
    });
    return response;
  }
};