// QUAN-20260604-153038
import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Title, Text, Card, Group, Button, LoadingOverlay, Box } from '@mantine/core';
import { notifications } from '@mantine/notifications';
import { IconArrowLeft } from '@tabler/icons-react';
import { hocSinhService } from '../../api/hocSinhService';
import { HocSinh } from '../../types/hocSinh';
import { DateOnlyFormatter, DateTimeFormatter } from '../../utils/formatters';

const HocSinhDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [hocSinh, setHocSinh] = useState<HocSinh | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchHocSinhDetail = async () => {
      if (!id) {
        notifications.show({
          title: 'Lỗi',
          message: 'Không tìm thấy ID học sinh.',
          color: 'red',
        });
        navigate('/hoc-sinh');
        return;
      }
      setLoading(true);
      try {
        const response = await hocSinhService.getHocSinhById(id);
        if (response.success && response.data) {
          setHocSinh(response.data);
        } else {
          notifications.show({
            title: 'Lỗi',
            message: response.message || 'Không thể tải chi tiết học sinh.',
            color: 'red',
          });
          navigate('/hoc-sinh'); // Redirect if not found
        }
      } catch (error) {
        console.error('Failed to fetch student details:', error);
        notifications.show({
          title: 'Lỗi',
          message: 'Đã xảy ra lỗi khi tải chi tiết học sinh.',
          color: 'red',
        });
        navigate('/hoc-sinh');
      } finally {
        setLoading(false);
      }
    };

    fetchHocSinhDetail();
  }, [id, navigate]);

  if (!hocSinh && !loading) {
    return <Text>Học sinh không tìm thấy.</Text>;
  }

  return (
    <Box pos="relative">
      <LoadingOverlay visible={loading} zIndex={1000} overlayProps={{ radius: 'sm', blur: 2 }} />

      <Group justify="space-between" mb="md">
        <Title order={2}>Chi tiết Học sinh: {hocSinh?.hoTen}</Title>
        <Button variant="default" leftSection={<IconArrowLeft size="1rem" />} onClick={() => navigate('/hoc-sinh')}>
          Quay lại danh sách
        </Button>
      </Group>

      {hocSinh && (
        <Card withBorder radius="md" p="xl" shadow="md">
          <Text size="lg" mb="sm"><Text span fw={700}>Mã Học sinh:</Text> {hocSinh.maHocSinh}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Họ và Tên:</Text> {hocSinh.hoTen}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Ngày Sinh:</Text> {DateOnlyFormatter(hocSinh.ngaySinh)}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Giới Tính:</Text> {hocSinh.gioiTinh}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Địa Chỉ:</Text> {hocSinh.diaChi || 'N/A'}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Số Điện Thoại Phụ Huynh:</Text> {hocSinh.soDienThoaiPH || 'N/A'}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Email Phụ Huynh:</Text> {hocSinh.emailPH || 'N/A'}</Text>
          <Text size="lg" mb="sm"><Text span fw={700}>Lớp Học:</Text> {hocSinh.tenLop}</Text>
          <Text size="lg" mb="xl"><Text span fw={700}>Trạng Thái:</Text> {hocSinh.trangThai}</Text>

          <Text size="sm" c="dimmed" mt="lg">
            <Text span fw={700}>Ngày tạo:</Text> {DateTimeFormatter(hocSinh.createdAt)} bởi {hocSinh.createdBy}
          </Text>
          {hocSinh.updatedAt && (
            <Text size="sm" c="dimmed">
              <Text span fw={700}>Cập nhật lần cuối:</Text> {DateTimeFormatter(hocSinh.updatedAt)} bởi {hocSinh.updatedBy}
            </Text>
          )}
        </Card>
      )}
    </Box>
  );
};

export default HocSinhDetailPage;