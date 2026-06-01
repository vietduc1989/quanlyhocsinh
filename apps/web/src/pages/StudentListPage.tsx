// QUAN-20260530-2301
import { useState } from 'react';
import { Container, Title, Paper, Pagination, Group, Button, Box } from '@mantine/core';
import { IconPlus } from '@tabler/icons-react';
import { useStudents } from '../hooks/useStudents';
import { StudentTable } from '../components/StudentTable';
import { StudentFilterForm } from '../components/StudentFilterForm';
import { deleteStudent } from '../api/studentApi';
import { notifications } from '@mantine/notifications';
import { useNavigate } from 'react-router-dom';

export function StudentListPage() {
  const navigate = useNavigate();
  const {
    students,
    pagination,
    filters,
    isLoading,
    error,
    fetchStudents,
    handleFilterChange,
    handlePageChange,
    handleSortChange,
  } = useStudents();

  const [isDeleting, setIsDeleting] = useState(false);

  const handleDelete = async (id: string) => {
    if (!confirm('Bạn có chắc chắn muốn xóa học sinh này?')) {
      return;
    }

    setIsDeleting(true);
    try {
      const response = await deleteStudent(id);
      if (response.success) {
        notifications.show({
          title: 'Xóa thành công',
          message: response.message || 'Học sinh đã được xóa.',
          color: 'green',
        });
        fetchStudents(); // Refresh list after deletion
      } else {
        notifications.show({
          title: 'Lỗi xóa học sinh',
          message: response.message || 'Có lỗi xảy ra khi xóa học sinh.',
          color: 'red',
        });
        response.errors?.forEach(err => {
          notifications.show({
            title: 'Lỗi xóa học sinh',
            message: err.message,
            color: 'red',
          });
        });
      }
    } catch (err: any) {
      notifications.show({
        title: 'Lỗi API',
        message: err.response?.data?.message || err.message || 'Không thể kết nối tới máy chủ.',
        color: 'red',
      });
    } finally {
      setIsDeleting(false);
    }
  };

  const handleSearchAndFilter = () => {
    fetchStudents();
  };

  const handleClearFilters = () => {
    // Reset filters to initial state for useStudents hook
    // and then manually trigger fetchStudents if needed or let useEffect handle it.
    handleFilterChange({
      searchQuery: undefined,
      lopId: undefined,
      trangThaiId: undefined,
      pageNumber: 1,
      sortBy: 'hoVaTen', // Revert to default sort
      sortOrder: 'asc', // Revert to default order
    });
    fetchStudents(); // Explicitly fetch after clearing filters
  };

  return (
    <Container size="xl" py="xl">
      <Paper shadow="sm" p="lg" withBorder>
        <Group position="apart" mb="lg">
          <Title order={2}>Quản lý Học sinh</Title>
          <Button leftIcon={<IconPlus size="1rem" />} onClick={() => navigate('/students/create')}>
            Thêm mới
          </Button>
        </Group>

        <Box mb="xl">
          <StudentFilterForm
            filters={filters}
            onFilterChange={handleFilterChange}
            onSearch={handleSearchAndFilter}
            onClear={handleClearFilters}
          />
        </Box>

        {isLoading ? (
          <Text align="center">Đang tải dữ liệu...</Text>
        ) : error ? (
          <Text align="center" color="red">Lỗi: {error}</Text>
        ) : (
          <>
            <StudentTable
              data={students}
              onDelete={handleDelete}
              onSortChange={handleSortChange}
              currentSortBy={filters.sortBy}
              currentSortOrder={filters.sortOrder}
            />

            <Group position="apart" mt="xl">
              <Text size="sm">
                Hiển thị {((pagination.pageNumber - 1) * pagination.pageSize) + 1} -{' '}
                {Math.min(pagination.pageNumber * pagination.pageSize, pagination.totalCount)} trên {pagination.totalCount} bản ghi
              </Text>
              <Pagination
                value={pagination.pageNumber}
                onChange={handlePageChange}
                total={pagination.totalPages}
                siblings={1}
                boundaries={1}
              />
            </Group>
          </>
        )}
      </Paper>
    </Container>
  );
}