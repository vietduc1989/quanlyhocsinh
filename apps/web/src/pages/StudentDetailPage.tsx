// QUAN-20260530-2301
import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Title, Paper, Group, Text, Loader, Button, Grid, Badge } from '@mantine/core';
import { notifications } from '@mantine/notifications';
import { getStudentById } from '../api/studentApi';
import type { StudentDetailDto } from '../types/student';
import dayjs from 'dayjs';

export function StudentDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [student, setStudent] = useState<StudentDetailDto | null>(null);
  const [isLoading, setIsLoading] = useState(true);
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
          <Title order={2}>Chi tiết Học sinh: {student.hoVaTen}</Title>
          <Button onClick={() => navigate(`/students/edit/${student.id}`)}>Chỉnh sửa</Button>
        </Group>

        <Grid gutter="xl">
          <Grid.Col span={6}>
            <Text weight={500}>Mã Học sinh:</Text>
            <Text>{student.maHocSinh}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Họ và Tên:</Text>
            <Text>{student.hoVaTen}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Ngày Sinh:</Text>
            <Text>{dayjs(student.ngaySinh).format('DD/MM/YYYY')}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Giới Tính:</Text>
            <Text>{student.gioiTinh}</Text>
          </Grid.Col>
          <Grid.Col span={12}>
            <Text weight={500}>Địa chỉ:</Text>
            <Text>{student.diaChi || 'N/A'}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>SĐT Phụ Huynh:</Text>
            <Text>{student.sdtPhuHuynh || 'N/A'}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Email Phụ Huynh:</Text>
            <Text>{student.emailPhuHuynh || 'N/A'}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Lớp Học:</Text>
            <Text>{student.tenLop}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Ngày Nhập Học:</Text>
            <Text>{dayjs(student.ngayNhapHoc).format('DD/MM/YYYY')}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Trạng Thái:</Text>
            <Badge size="lg" color={student.tenTrangThai === 'Đang học' ? 'green' : 'gray'}>{student.tenTrangThai}</Badge>
          </Grid.Col>
        </Grid>

        <Title order={4} mt="xl" mb="md">Thông tin Audit</Title>
        <Grid gutter="xl">
          <Grid.Col span={6}>
            <Text weight={500}>Ngày tạo:</Text>
            <Text>{dayjs(student.createdAt).format('DD/MM/YYYY HH:mm:ss')}</Text>
          </Grid.Col>
          <Grid.Col span={6}>
            <Text weight={500}>Người tạo:</Text>
            <Text>{student.createdBy || 'N/A'}</Text>
          </Grid.Col>
          {student.updatedAt && (
            <Grid.Col span={6}>
              <Text weight={500}>Ngày cập nhật:</Text>
              <Text>{dayjs(student.updatedAt).format('DD/MM/YYYY HH:mm:ss')}</Text>
            </Grid.Col>
          )}
          {student.updatedBy && (
            <Grid.Col span={6}>
              <Text weight={500}>Người cập nhật:</Text>
              <Text>{student.updatedBy || 'N/A'}</Text>
            </Grid.Col>
          )}
        </Grid>

        <Group mt="xl">
          <Button variant="default" onClick={() => navigate('/students')}>Quay lại danh sách</Button>
        </Group>
      </Paper>
    </Container>
  );
}