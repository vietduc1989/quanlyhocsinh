<!-- QUAN-20260530-2301 -->
import { useParams } from 'react-router-dom';
import { Container, Title, Text, Card, Group, Badge, Loader, Center } from '@mantine/core';
import { useStudentDetail } from '../../hooks/useStudentDetail';
import dayjs from 'dayjs';

export function StudentDetailPage() {
  const { id } = useParams<{ id: string }>();
  const { data: student, isLoading, isError, error } = useStudentDetail(id);

  if (isLoading) {
    return (
      <Center style={{ height: 'calc(100vh - 60px)' }}>
        <Loader size="lg" />
      </Center>
    );
  }

  if (isError) {
    return (
      <Container>
        <Title order={3} color="red">Lỗi tải dữ liệu</Title>
        <Text color="red">{error?.message || 'Không thể tải chi tiết học sinh.'}</Text>
      </Container>
    );
  }

  if (!student) {
    return (
      <Container>
        <Title order={3}>Không tìm thấy học sinh</Title>
        <Text>Học sinh với ID "{id}" không tồn tại hoặc đã bị xóa.</Text>
      </Container>
    );
  }

  return (
    <Container size="md" my="md">
      <Title order={2} mb="md">Chi tiết Học sinh: {student.hoVaTen}</Title>

      <Card shadow="sm" padding="lg" radius="md" withBorder>
        <Group justify="space-between" mb="xs">
          <Text fw={700}>Mã Học sinh: {student.maHocSinh}</Text>
          <Badge variant="light" color="blue" size="lg">{student.tenTrangThai}</Badge>
        </Group>

        <Text size="sm" c="dimmed">
          <Text span fw={500}>Họ và Tên:</Text> {student.hoVaTen}
        </Text>
        <Text size="sm" c="dimmed">
          <Text span fw={500}>Ngày Sinh:</Text> {dayjs(student.ngaySinh).format('DD/MM/YYYY')}
        </Text>
        <Text size="sm" c="dimmed">
          <Text span fw={500}>Giới Tính:</Text> {student.gioiTinh}
        </Text>
        <Text size="sm" c="dimmed">
          <Text span fw={500}>Lớp Học:</Text> {student.tenLop}
        </Text>
        <Text size="sm" c="dimmed">
          <Text span fw={500}>Ngày Nhập Học:</Text> {dayjs(student.ngayNhapHoc).format('DD/MM/YYYY')}
        </Text>

        {student.diaChi && (
          <Text size="sm" c="dimmed">
            <Text span fw={500}>Địa Chỉ:</Text> {student.diaChi}
          </Text>
        )}
        {student.sdtPhuHuynh && (
          <Text size="sm" c="dimmed">
            <Text span fw={500}>SĐT Phụ Huynh:</Text> {student.sdtPhuHuynh}
          </Text>
        )}
        {student.emailPhuHuynh && (
          <Text size="sm" c="dimmed">
            <Text span fw={500}>Email Phụ Huynh:</Text> {student.emailPhuHuynh}
          </Text>
        )}

        <Text size="xs" c="dimmed" mt="md">
          Ngày tạo: {dayjs(student.ngayTao).format('DD/MM/YYYY HH:mm')} bởi {student.nguoiTao || 'N/A'}
        </Text>
        {student.ngayCapNhat && (
          <Text size="xs" c="dimmed">
            Cập nhật cuối: {dayjs(student.ngayCapNhat).format('DD/MM/YYYY HH:mm')} bởi {student.nguoiCapNhat || 'N/A'}
          </Text>
        )}
      </Card>
    </Container>
  );
}