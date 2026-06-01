<!-- QUAN-20260530-2301 -->
import { useQuery } from '@tanstack/react-query';
import { getStudentById } from '../api/studentApi';
import type { StudentDetail } from '../types/common';

export function useStudentDetail(studentId: string | undefined) {
  return useQuery<StudentDetail, Error>({
    queryKey: ['studentDetail', studentId],
    queryFn: async () => {
      if (!studentId) {
        throw new Error('Student ID is required to fetch details.');
      }
      const response = await getStudentById(studentId);
      if (!response.success || !response.data) {
        throw new Error(response.message || 'Failed to fetch student details');
      }
      return response.data;
    },
    enabled: !!studentId, // Only run the query if studentId is available
    staleTime: 1000 * 60 * 5, // 5 minutes
    refetchOnWindowFocus: false,
  });
}