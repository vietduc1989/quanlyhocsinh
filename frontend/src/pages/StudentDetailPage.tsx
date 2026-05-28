import React from 'react';
import { Card, Group, Stack, Title, Text, Button, Badge, Grid, LoadingOverlay } from '@mantine/core';
import { useQuery } from '@tanstack/react-query';
import { studentApi } from '../api/studentApi';

interface Props {
  studentId: string;
  onBack: () => void;
}

export const StudentDetailPage: React.FC<Props> = ({ studentId, onBack }) => {
  const { data: student, isLoading } = useQuery({
    queryKey: ['student', studentId],
    queryFn: () => studentApi.getStudentById(studentId)
  });

  if (isLoading) return <LoadingOverlay visible />;

  return (
    <Card withBorder shadow="md" radius="md" p="xl" style={{ maxWidth: 800, margin: '20px auto' }}>
      <Group justify="space-between" mb="lg">
        <Title order={3}>Hồ sơ chi tiết Học sinh</Title>
        <Button variant="subtle" onClick={onBack}>Quay lại danh sách</Button>
      </Group>

      {student && (
        <Stack gap="md">
          <Grid>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Mã Học sinh:</Text>
              <Text>{student.studentCode}</Text>
            </Grid.Col>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Trạng thái:</Text>
              <Badge color={student.status === 1 ? 'green' : 'red'}>
                {student.statusText}
              </Badge>
            </Grid.Col>
          </Grid>

          <Grid>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Họ và tên:</Text>
              <Text>{student.fullName}</Text>
            </Grid.Col>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Ngày sinh:</Text>
              <Text>{new Date(student.dateOfBirth).toLocaleDateString('vi-VN')}</Text>
            </Grid.Col>
          </Grid>

          <Grid>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Giới tính:</Text>
              <Text>{student.gender}</Text>
            </Grid.Col>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Lớp học hiện tại:</Text>
              <Text>{student.className}</Text>
            </Grid.Col>
          </Grid>

          <Grid>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Số điện thoại:</Text>
              <Text>{student.phoneNumber || 'Không có'}</Text>
            </Grid.Col>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Email:</Text>
              <Text>{student.email || 'Không có'}</Text>
            </Grid.Col>
          </Grid>

          <Text style={{ fontWeight: 'bold' }}>Địa chỉ thường trú:</Text>
          <Text>{student.address || 'Chưa cập nhật'}</Text>

          <Title order={4} mt="lg">Thông tin người giám hộ/Phụ huynh</Title>
          <Grid>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Họ và tên:</Text>
              <Text>{student.parentName || 'Chưa cập nhật'}</Text>
            </Grid.Col>
            <Grid.Col span={6}>
              <Text style={{ fontWeight: 'bold' }}>Số điện thoại liên lạc:</Text>
              <Text>{student.parentPhoneNumber || 'Chưa cập nhật'}</Text>
            </Grid.Col>
          </Grid>
        </Stack>
      )}
    </Card>
  );
};