import { useQuery } from '@tanstack/react-query';
import { getStudents } from '../api/studentApi';
import type { PagedResult, Student } from '../types/common';

interface UseStudentsOptions {
  pageNumber?: number;
  pageSize?: number;
  searchQuery?: string;
  lopId?: string;
  trangThaiId?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

export function useStudents(options?: UseStudentsOptions) {
  return useQuery<PagedResult<Student>, Error>({
    queryKey: ['students', options],
    queryFn: async () => {
      const response = await getStudents(options);
      if (!response.success || !response.data) {
        throw new Error(response.message || 'Failed to fetch students');
      }
      return response.data;
    },
    staleTime: 1000 * 30, // 30 seconds
    keepPreviousData: true, // Keep data while fetching new page/filters
  });
}
