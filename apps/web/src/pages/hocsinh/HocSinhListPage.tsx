// QUAN-20260604-153038
import React, { useState, useEffect } from 'react';
import {
  Title,
  Button,
  Table,
  Group,
  TextInput,
  Select,
  Pagination,
  Box,
  LoadingOverlay,
  Flex,
  Tooltip,
  ActionIcon,
  Text,
} from '@mantine/core';
import { notifications } from '@mantine/notifications';
import { Link, useNavigate } from 'react-router-dom';
import { IconSearch, IconFilter, IconEye, IconEdit, IconTrash, IconPlus, IconRefresh } from '@tabler/icons-react';
import { HocSinh, HocSinhSearchParams, LopHocLookup, TrangThaiHocSinh } from '../../types/hocSinh';
import { hocSinhService } from '../../api/hocSinhService';
import ConfirmationDialog from '../../components/UI/ConfirmationDialog';
import { DateOnlyFormatter } from '../../utils/formatters';

const HocSinhListPage: React.FC = () => {
  const navigate = useNavigate();
  const [hocSinhs, setHocSinhs] = useState<HocSinh[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchParams, setSearchParams] = useState<HocSinhSearchParams>({
    pageNumber: 1,
    pageSize: 10,
  });
  const [totalCount, setTotalCount] = useState(0);
  const [totalPages, setTotalPages] = useState(1);
  const [lopHocLookup, setLopHocLookup] = useState<LopHocLookup[]>([]);
  const [confirmDeleteOpened, setConfirmDeleteOpened] = useState(false);
  const [hocSinhToDelete, setHocSinhToDelete] = useState<HocSinh | null>(null);

  const trangThaiOptions: { value: TrangThaiHocSinh; label: string }[] = [
    { value: 'DangHoc', label: 'Đang học' },
    { value: 'DaTotNghiep', label: 'Đã tốt nghiệp' },
    { value: 'DaChuyenTruong', label: 'Đã chuyển trường' },
    { value: 'TamDung', label: 'Tạm dừng' },
  ];

  const fetchHocSinhs = async () => {
    setLoading(true);
    try {
      const response = await hocSinhService.getHocSinhs(searchParams);
      if (response.success) {
        setHocSinhs(response.data.items);
        setTotalCount(response.data.totalCount);
        setTotalPages(response.data.totalPages);
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Không thể tải danh sách học sinh.',
          color: 'red',
        });
      }
    } catch (error) {
      console.error('Failed to fetch students:', error);
    } finally {
      setLoading(false);
    }
  };

  const fetchLopHocLookup = async () => {
    try {
      const response = await hocSinhService.getLopHocsForLookup();
      if (response.success) {
        setLopHocLookup(response.data);
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Không thể tải danh sách lớp học.',
          color: 'red',
        });
      }
    } catch (error) {
      console.error('Failed to fetch class lookup:', error);
    }
  };

  useEffect(() => {
    fetchHocSinhs();
    fetchLopHocLookup();
  }, [searchParams]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchParams((prev) => ({ ...prev, searchTerm: event.target.value, pageNumber: 1 }));
  };

  const handleLopHocFilterChange = (value: string | null) => {
    setSearchParams((prev) => ({ ...prev, lopHocId: value || undefined, pageNumber: 1 }));
  };

  const handleTrangThaiFilterChange = (value: string | null) => {
    setSearchParams((prev) => ({ ...prev, trangThai: (value as TrangThaiHocSinh) || undefined, pageNumber: 1 }));
  };

  const handlePageChange = (page: number) => {
    setSearchParams((prev) => ({ ...prev, pageNumber: page }));
  };

  const handlePageSizeChange = (value: string | null) => {
    if (value) {
      setSearchParams((prev) => ({ ...prev, pageSize: parseInt(value, 10), pageNumber: 1 }));
    }
  };

  const handleClearFilters = () => {
    setSearchParams({ pageNumber: 1, pageSize: 10 });
  };

  const handleDeleteClick = (hocSinh: HocSinh) => {
    setHocSinhToDelete(hocSinh);
    setConfirmDeleteOpened(true);
  };

  const handleConfirmDelete = async () => {
    if (hocSinhToDelete) {
      setLoading(true);
      try {
        const response = await hocSinhService.deleteHocSinh(hocSinhToDelete.id);
        if (response.success) {
          notifications.show({
            title: 'Thành công',
            message: response.message || 'Xóa học sinh thành công.',
            color: 'green',
          });
          fetchHocSinhs(); // Refresh list
        } else {
          notifications.show({
            title: 'Lỗi',
            message: response.message || 'Xóa học sinh thất bại.',
            color: 'red',
          });
        }
      } catch (error) {
        console.error('Failed to delete student:', error);
      } finally {
        setLoading(false);
        setConfirmDeleteOpened(false);
        setHocSinhToDelete(null);
      }
    }
  };

  const rows = hocSinhs.map((hocSinh) => (
    <Table.Tr key={hocSinh.id}>
      <Table.Td>{hocSinh.maHocSinh}</Table.Td>
      <Table.Td>{hocSinh.hoTen}</Table.Td>
      <Table.Td>{DateOnlyFormatter(hocSinh.ngaySinh)}</Table.Td>
      <Table.Td>{hocSinh.gioiTinh}</Table.Td>
      <Table.Td>{hocSinh.tenLop}</Table.Td>
      <Table.Td>{hocSinh.trangThai}</Table.Td>
      <Table.Td>
        <Group gap="xs" justify="center" wrap="nowrap">
          <Tooltip label="Xem chi tiết">
            <ActionIcon variant="subtle" color="blue" onClick={() => navigate(`/hoc-sinh/${hocSinh.id}`)}>
              <IconEye size="1rem" />
            </ActionIcon>
          </Tooltip>
          <Tooltip label="Chỉnh sửa">
            <ActionIcon variant="subtle" color="yellow" onClick={() => navigate(`/hoc-sinh/edit/${hocSinh.id}`)}>
              <IconEdit size="1rem" />
            </ActionIcon>
          </Tooltip>
          <Tooltip label="Xóa">
            <ActionIcon variant="subtle" color="red" onClick={() => handleDeleteClick(hocSinh)}>
              <IconTrash size="1rem" />
            </ActionIcon>
          </Tooltip>
        </Group>
      </Table.Td>
    </Table.Tr>
  ));

  return (
    <Box pos="relative">
      <LoadingOverlay visible={loading} zIndex={1000} overlayProps={{ radius: 'sm', blur: 2 }} />
      <Group justify="space-between" mb="md">
        <Title order={2}>Danh sách Học sinh</Title>
        <Button component={Link} to="/hoc-sinh/add" leftSection={<IconPlus size="1rem" />}>
          Thêm mới Học sinh
        </Button>
      </Group>

      <Flex direction={{ base: 'column', sm: 'row' }} gap="md" align="flex-end" mb="md">
        <TextInput
          placeholder="Tìm kiếm theo Mã/Họ tên"
          value={searchParams.searchTerm || ''}
          onChange={handleSearchChange}
          leftSection={<IconSearch size="1rem" />}
          style={{ flex: 1 }}
        />
        <Select
          placeholder="Lọc theo Lớp học"
          data={lopHocLookup.map((l) => ({ value: l.id, label: l.tenLop }))}
          value={searchParams.lopHocId || null}
          onChange={handleLopHocFilterChange}
          leftSection={<IconFilter size="1rem" />}
          clearable
          style={{ flex: 1 }}
        />
        <Select
          placeholder="Lọc theo Trạng thái"
          data={trangThaiOptions}
          value={searchParams.trangThai || null}
          onChange={handleTrangThaiFilterChange}
          leftSection={<IconFilter size="1rem" />}
          clearable
          style={{ flex: 1 }}
        />
        <Button onClick={handleClearFilters} variant="outline" leftSection={<IconRefresh size="1rem" />}>
          Xóa bộ lọc
        </Button>
      </Flex>

      <Table highlightOnHover withTableBorder withColumnBorders mb="md">
        <Table.Thead>
          <Table.Tr>
            <Table.Th>Mã Học sinh</Table.Th>
            <Table.Th>Họ và Tên</Table.Th>
            <Table.Th>Ngày Sinh</Table.Th>
            <Table.Th>Giới Tính</Table.Th>
            <Table.Th>Lớp Học</Table.Th>
            <Table.Th>Trạng Thái</Table.Th>
            <Table.Th style={{ width: 150, textAlign: 'center' }}>Hành động</Table.Th>
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows.length > 0 ? rows : <Table.Tr><Table.Td colSpan={7} style={{ textAlign: 'center' }}>Không có dữ liệu</Table.Td></Table.Tr>}</Table.Tbody>
      </Table>

      <Group justify="space-between">
        <Select
          label="Học sinh mỗi trang"
          data={['10', '20', '50', '100']}
          value={searchParams.pageSize?.toString()}
          onChange={handlePageSizeChange}
          allowDeselect={false}
          style={{ width: 150 }}
        />
        <Pagination
          total={totalPages}
          value={searchParams.pageNumber}
          onChange={handlePageChange}
          siblings={1}
          boundaries={1}
        />
        <Text size="sm">
          Hiển thị {Math.min(((searchParams.pageNumber ?? 1) - 1) * (searchParams.pageSize ?? 10) + 1, totalCount)} -{' '}
          {Math.min((searchParams.pageNumber ?? 1) * (searchParams.pageSize ?? 10), totalCount)} trên {totalCount} học sinh
        </Text>
      </Group>

      <ConfirmationDialog
        opened={confirmDeleteOpened}
        onClose={() => setConfirmDeleteOpened(false)}
        onConfirm={handleConfirmDelete}
        title="Xác nhận xóa học sinh"
        message={`Bạn có chắc chắn muốn xóa học sinh "${hocSinhToDelete?.hoTen}" (Mã: ${hocSinhToDelete?.maHocSinh})? Thao tác này sẽ đánh dấu học sinh là đã xóa mềm.`}
        confirmLabel="Xác nhận xóa"
        cancelLabel="Hủy"
      />
    </Box>
  );
};

export default HocSinhListPage;