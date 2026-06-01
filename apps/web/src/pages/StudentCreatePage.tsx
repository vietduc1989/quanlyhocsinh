// QUAN-20260530-2301
import { useState } from 'react';
import { Container, Title, Paper } from '@mantine/core';
import { notifications } from '@mantine/notifications';
import { StudentForm } from '../components/StudentForm';
import { createStudent } from '../api/studentApi';
import type { CreateStudentCommand } from '../types/student';
import { useNavigate } from 'react-router-dom';

export function StudentCreatePage() {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (values: CreateStudentCommand) => {
    setIsSubmitting(true);
    try {
      const response = await createStudent(values);
      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || 'Học sinh đã được thêm mới.',
          color: 'green',
        });
        navigate(`/students/${response.data?.id}`); // Redirect to detail page
      } else {
        // Handle specific validation errors from API
        response.errors?.forEach(error => {
          notifications.show({
            title: 'Lỗi tạo học sinh',
            message: `${error.field}: ${error.message}`,
            color: 'red',
          });
        });
        // Fallback for general error message
        if (!response.errors?.length) {
          notifications.show({
            title: 'Lỗi tạo học sinh',
            message: response.message || 'Có lỗi xảy ra khi thêm học sinh.',
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

  return (
    <Container size="md" py="xl">
      <Paper shadow="sm" p="lg" withBorder>
        <Title order={2} align="center" mb="lg">Thêm mới Học sinh</Title>
        <StudentForm onSubmit={handleSubmit} isSubmitting={isSubmitting} />
      </Paper>
    </Container>
  );
}