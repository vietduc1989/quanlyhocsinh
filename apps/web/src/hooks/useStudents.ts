// QUAN-20260530-2301
import { useState, useEffect, useCallback } from 'react';
import { notifications } from '@mantine/notifications';
import { getStudents } from '../api/studentApi';
import type { PaginatedResponse } from '../types/ApiResponse';
import type { StudentListDto, StudentListFilter } from '../types/student';

export function useStudents(initialFilters?: StudentListFilter) {
  const [students, setStudents] = useState<StudentListDto[]>([]);
  const [pagination, setPagination] = useState<Omit<PaginatedResponse<any>, 'items'>>({
    pageNumber: 1,
    pageSize: 10,
    totalCount: 0,
    totalPages: 0,
    hasNextPage: false,
    hasPreviousPage: false,
  });
  const [filters, setFilters] = useState<StudentListFilter>(initialFilters || {
    pageNumber: 1,
    pageSize: 10,
    sortBy: 'hoVaTen',
    sortOrder: 'asc',
  });
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchStudents = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await getStudents(filters);
      if (response.success && response.data) {
        setStudents(response.data.items);
        setPagination({
          pageNumber: response.data.pageNumber,
          pageSize: response.data.pageSize,
          totalCount: response.data.totalCount,
          totalPages: response.data.totalPages,
          hasNextPage: response.data.hasNextPage,
          hasPreviousPage: response.data.hasPreviousPage,
        });
      } else {
        notifications.show({
          title: 'Lỗi tải danh sách học sinh',
          message: response.message || 'Có lỗi xảy ra khi tải dữ liệu.',
          color: 'red',
        });
        setError(response.message || 'Failed to fetch students.');
      }
    } catch (err: any) {
      notifications.show({
        title: 'Lỗi API',
        message: err.response?.data?.message || err.message || 'Không thể kết nối tới máy chủ.',
        color: 'red',
      });
      setError(err.response?.data?.message || err.message || 'An unexpected error occurred.');
    } finally {
      setIsLoading(false);
    }
  }, [filters]);

  useEffect(() => {
    fetchStudents();
  }, [fetchStudents]);

  const handleFilterChange = (newFilters: Partial<StudentListFilter>) => {
    setFilters((prev) => ({ ...prev, ...newFilters, pageNumber: 1 })); // Reset to first page on filter change
  };

  const handlePageChange = (newPage: number) => {
    if (newPage > 0 && newPage <= pagination.totalPages) {
      setFilters((prev) => ({ ...prev, pageNumber: newPage }));
    }
  };

  const handlePageSizeChange = (newSize: number) => {
    setFilters((prev) => ({ ...prev, pageSize: newSize, pageNumber: 1 }));
  };

  const handleSortChange = (newSortBy: string | null, newSortOrder: 'asc' | 'desc') => {
    setFilters((prev) => ({ ...prev, sortBy: newSortBy || prev.sortBy, sortOrder: newSortOrder }));
  };

  return {
    students,
    pagination,
    filters,
    isLoading,
    error,
    fetchStudents,
    handleFilterChange,
    handlePageChange,
    handlePageSizeChange,
    handleSortChange,
  };
}