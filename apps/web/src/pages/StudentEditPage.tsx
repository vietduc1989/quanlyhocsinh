// QUAN-20260530-2301
import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Title, Paper, Loader, Button, Group, Text } from '@mantine/core';
import { notifications } from '@mantine/notifications';
import { StudentForm } from '../components/StudentForm';
import { getStudentById, updateStudent } from '../api/studentApi';
import type { UpdateStudentCommand, StudentDetailDto } from '../types/student';

export function StudentEditPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [student, setStudent] = useState<StudentDetailDto | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchStudent = async () => {
      if (!id) {
        setError('ID học sinh không hợp lệ.');
        setIsLoading(false);
        return;
      }
      setIsLoading(true);
      try {
        const response = await getStudentById(id);
        if (response.success && response.data) {
          setStudent(response.data);
        } else {
          notifications.show({
            title: 'Lỗi tải chi tiết học sinh',
            message: response.message || 'Có lỗi xảy ra khi tải dữ liệu.',
            color: 'red',
          });
          setError(response.message || 'Failed to fetch student details.');
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
    };

    fetchStudent();
  }, [id]);

  const handleSubmit = async (values: UpdateStudentCommand) => {
    if (!id) return; // Should not happen if component loaded correctly
    setIsSubmitting(true);
    try {
      const response = await updateStudent({ ...values, id });
      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || 'Thông tin học sinh đã được cập nhật.',
          color: 'green',
        });
        navigate(`/students/${id}`); // Redirect to detail page
      } else {
        // Handle specific validation errors from API
        response.errors?.forEach(error => {
          notifications.show({
            title: 'Lỗi cập nhật học sinh',
            message: `${error.field}: ${error.message}`,
            color: 'red',
          });
        });
        // Fallback for general error message
        if (!response.errors?.length) {
          notifications.show({
            title: 'Lỗi cập nhật học sinh',
            message: response.message || 'Có lỗi xảy ra khi cập nhật học sinh.',
            color: 'red',
          });
        }
      }
    } catch (error: any) {
      notifications.show({
        title: 'Lỗi API',
        message: error.response?.data?.message || error.message || 'Không thể kết nối tới máy chủ.',
        color: 'red',
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  if (isLoading) {
    return (
      <Container size="md" py="xl" style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: 'calc(100vh - 100px)' }}>
        <Loader />
      </Container>
    );
  }

  if (error) {
    return (
      <Container size="md" py="xl">
        <Paper shadow="sm" p="lg" withBorder>
          <Title order={3} color="red">Lỗi</Title>
          <Text color="red">{error}</Text>
          <Button mt="md" onClick={() => navigate('/students')}>Quay lại danh sách</Button>
        </Paper>
      </Container>
    );
  }

  if (!student) {
    return (
      <Container size="md" py="xl">
        <Paper shadow="sm" p="lg" withBorder>
          <Title order={3}>Không tìm thấy học sinh</Title>
          <Text>Học sinh bạn đang tìm kiếm không tồn tại hoặc đã bị xóa.</Text>
          <Button mt="md" onClick={() => navigate('/students')}>Quay lại danh sách</Button>
        </Paper>
      </Container>
    );
  }

  return (
    <Container size="md" py="xl">
      <Paper shadow="sm" p="lg" withBorder>
        <Group position="apart" mb="lg">
          <Title order={2}>Chỉnh sửa Học sinh: {student.hoVaTen}</Title>
          <Button variant="default" onClick={() => navigate(`/students/${id}`)}>Hủy</Button>
        </Group>
        <StudentForm initialValues={student} onSubmit={handleSubmit} isSubmitting={isSubmitting} isEditMode />
      </Paper>
    </Container>
  );
}