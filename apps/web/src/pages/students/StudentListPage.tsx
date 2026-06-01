import { useState } from 'react';
import {
  Table,
  Button,
  TextInput,
  Group,
  Pagination,
  Loader,
  Container,
  Title,
  Center,
  Select,
  ActionIcon,
  Modal,
  Text,
  Badge,
  Flex,
  Tooltip,
} from '@mantine/core';
import { IconSearch, IconFilter, IconEdit, IconTrash, IconEye, IconRefresh } from '@tabler/icons-react';
import { useStudents } from '../../hooks/useStudents';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createStudent, updateStudent, deleteStudent, getLops, getTrangThais, getStudentById } from '../../api/studentApi';
import { useForm } from '@mantine/form';
import { useDisclosure } from '@mantine/hooks';
import { StudentFormModal } from './StudentFormModal';
import { Link, useNavigate } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import type { Student, StudentDetail, LookupItem, ApiError } from '../../types/common';
import { notifications } from '@mantine/notifications'; // Assuming Mantine Notifications is set up

export function StudentListPage() {
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const [page, setPage] = useState(1);
  const [search, setSearch] = useState('');
  const [filterLopId, setFilterLopId] = useState<string | null>(null);
  const [filterTrangThaiId, setFilterTrangThaiId] = useState<string | null>(null);
  const [sortBy, setSortBy] = useState<string>('hoVaTen');
  const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');
  const [pageSize, setPageSize] = useState(10);

  const [openedModal, { open: openModal, close: closeModal }] = useDisclosure(false);
  const [openedDeleteConfirm, { open: openDeleteConfirm, close: closeDeleteConfirm }] = useDisclosure(false);
  const [selectedStudent, setSelectedStudent] = useState<StudentDetail | null>(null);
  const [studentToDeleteId, setStudentToDeleteId] = useState<string | null>(null);

  // QUAN-20260530-2301-SRS01, QUAN-20260530-2301-SRS07, QUAN-20260530-2301-SRS08
  const { data: pagedStudents, isLoading, isFetching, isError, error, refetch } = useStudents({
    pageNumber: page,
    pageSize,
    searchQuery: search,
    lopId: filterLopId || undefined,
    trangThaiId: filterTrangThaiId || undefined,
    sortBy,
    sortOrder,
  });

  const { data: lopsData } = useQuery<LookupItem[], Error>({
    queryKey: ['lopsLookup'],
    queryFn: async () => {
      const response = await getLops();
      if (!response.success || !response.data) {
        throw new Error(response.message || 'Failed to fetch classes.');
      }
      return response.data;
    },
    staleTime: 1000 * 60 * 60, // 1 hour
  });

  const { data: trangThaisData } = useQuery<LookupItem[], Error>({
    queryKey: ['trangThaisLookup'],
    queryFn: async () => {
      const response = await getTrangThais();
      if (!response.success || !response.data) {
        throw new Error(response.message || 'Failed to fetch statuses.');
      }
      return response.data;
    },
    staleTime: 1000 * 60 * 60, // 1 hour
  });

  const lops = lopsData || [];
  const trangThais = trangThaisData || [];

  const createStudentMutation = useMutation({
    mutationFn: createStudent,
    onSuccess: (response) => {
      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || 'Thêm học sinh thành công',
          color: 'green',
        });
        queryClient.invalidateQueries({ queryKey: ['students'] });
        closeModal();
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Đã xảy ra lỗi khi thêm học sinh.',
          color: 'red',
        });
      }
    },
    onError: (err: any) => {
      handleApiError(err, 'Thêm học sinh thất bại');
    },
  });

  const updateStudentMutation = useMutation({
    mutationFn: ({ id, payload }: { id: string; payload: UpdateStudentPayload }) => updateStudent(id, payload),
    onSuccess: (response) => {
      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || 'Cập nhật học sinh thành công',
          color: 'green',
        });
        queryClient.invalidateQueries({ queryKey: ['students'] });
        queryClient.invalidateQueries({ queryKey: ['studentDetail', selectedStudent?.id] });
        closeModal();
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Đã xảy ra lỗi khi cập nhật học sinh.',
          color: 'red',
        });
      }
    },
    onError: (err: any) => {
      handleApiError(err, 'Cập nhật học sinh thất bại');
    },
  });

  const deleteStudentMutation = useMutation({
    mutationFn: deleteStudent,
    onSuccess: (response) => {
      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || 'Xóa học sinh thành công',
          color: 'green',
        });
        queryClient.invalidateQueries({ queryKey: ['students'] });
        closeDeleteConfirm();
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Đã xảy ra lỗi khi xóa học sinh.',
          color: 'red',
        });
      }
    },
    onError: (err: any) => {
      handleApiError(err, 'Xóa học sinh thất bại');
      closeDeleteConfirm();
    },
  });

  const handleApiError = (err: any, defaultMessage: string) => {
    let errorMessage = defaultMessage;
    const errors: ApiError[] = err.response?.data?.errors || [];

    if (errors.length > 0) {
      errorMessage = errors.map(e => e.message).join('; ');
    } else if (err.response?.data?.message) {
      errorMessage = err.response.data.message;
    } else if (err.message) {
      errorMessage = err.message;
    }

    notifications.show({
      title: 'Lỗi',
      message: errorMessage,
      color: 'red',
    });
  };

  const handleAddClick = () => {
    setSelectedStudent(null);
    form.reset();
    openModal();
  };

  const handleEditClick = async (studentId: string) => {
    try {
      const response = await getStudentById(studentId);
      if (response.success && response.data) {
        setSelectedStudent(response.data);
        openModal();
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Không tìm thấy chi tiết học sinh để chỉnh sửa.',
          color: 'red',
        });
      }
    } catch (err: any) {
      handleApiError(err, 'Không thể tải chi tiết học sinh.');
    }
  };

  const handleDeleteClick = (studentId: string) => {
    setStudentToDeleteId(studentId);
    openDeleteConfirm();
  };

  const confirmDelete = () => {
    if (studentToDeleteId) {
      deleteStudentMutation.mutate(studentToDeleteId);
    }
  };

  const handleFormSubmit = async (values: CreateStudentPayload | UpdateStudentPayload) => {
    if (selectedStudent) {
      await updateStudentMutation.mutateAsync({ id: selectedStudent.id, payload: values as UpdateStudentPayload });
    } else {
      await createStudentMutation.mutateAsync(values as CreateStudentPayload);
    }
  };

  // Mantine Form instance for the modal
  const form = useForm({
    initialValues: {
      maHocSinh: '',
      hoVaTen: '',
      ngaySinh: null as Date | null,
      gioiTinh: 'Nam' as 'Nam' | 'Nu' | 'Khac',
      diaChi: '',
      sdtPhuHuynh: '',
      emailPhuHuynh: '',
      lopId: '',
      ngayNhapHoc: null as Date | null,
      trangThaiId: '',
    },
    // No direct validation here, handled by ZodResolver in StudentFormModal
  });


  const rows = pagedStudents?.items.map((student) => (
    <Table.Tr key={student.id}>
      <Table.Td>{student.maHocSinh}</Table.Td>
      <Table.Td>{student.hoVaTen}</Table.Td>
      <Table.Td>{student.lopHoc}</Table.Td>
      <Table.Td><Badge variant="light" color="blue">{student.trangThai}</Badge></Table.Td>
      <Table.Td>
        <Group gap="xs" wrap="nowrap">
          <Tooltip label="Xem chi tiết">
            <ActionIcon variant="subtle" color="gray" size="sm" onClick={() => navigate(`/students/${student.id}`)}>
              <IconEye style={{ width: '70%', height: '70%' }} stroke={1.5} />
            </ActionIcon>
          </Tooltip>
          <Tooltip label="Sửa">
            <ActionIcon variant="subtle" color="blue" size="sm" onClick={() => handleEditClick(student.id)}>
              <IconEdit style={{ width: '70%', height: '70%' }} stroke={1.5} />
            </ActionIcon>
          </Tooltip>
          <Tooltip label="Xóa">
            <ActionIcon variant="subtle" color="red" size="sm" onClick={() => handleDeleteClick(student.id)}>
              <IconTrash style={{ width: '70%', height: '70%' }} stroke={1.5} />
            </ActionIcon>
          </Tooltip>
        </Group>
      </Table.Td>
    </Table.Tr>
  ));

  if (isError) {
    return (
      <Container>
        <Title order={3} color="red">Lỗi tải dữ liệu</Title>
        <Text color="red">{error?.message || 'Không thể tải danh sách học sinh.'}</Text>
      </Container>
    );
  }

  return (
    <Container size="xl" my="md">
      <Title order={2} mb="md">Quản lý Học sinh</Title>

      <Flex
        direction={{ base: 'column', sm: 'row' }}
        justify={{ sm: 'space-between' }}
        align={{ sm: 'flex-end' }}
        gap="md"
        mb="md"
      >
        <Group wrap="nowrap">
          <TextInput
            placeholder="Tìm kiếm theo Mã HS / Tên"
            leftSection={<IconSearch size={16} />}
            value={search}
            onChange={(event) => setSearch(event.currentTarget.value)}
            style={{ flex: 1 }}
          />
          <Select
            placeholder="Lọc theo Lớp"
            leftSection={<IconFilter size={16} />}
            data={lops.map((lop) => ({ value: lop.id, label: lop.name }))}
            value={filterLopId}
            onChange={setFilterLopId}
            clearable
          />
          <Select
            placeholder="Lọc theo Trạng thái"
            leftSection={<IconFilter size={16} />}
            data={trangThais.map((tt) => ({ value: tt.id, label: tt.name }))}
            value={filterTrangThaiId}
            onChange={setFilterTrangThaiId}
            clearable
          />
        </Group>
        <Group>
            <Button onClick={handleAddClick}>Thêm Học sinh</Button>
            <Tooltip label="Làm mới dữ liệu">
              <ActionIcon variant="light" size="lg" onClick={() => refetch()} loading={isFetching}>
                <IconRefresh style={{ width: '70%', height: '70%' }} stroke={1.5} />
              </ActionIcon>
            </Tooltip>
        </Group>
      </Flex>


      {(isLoading || isFetching) && pagedStudents === undefined ? (
        <Center style={{ height: '200px' }}>
          <Loader size="lg" />
        </Center>
      ) : (
        <>
          <Table stickyHeader striped highlightOnHover withTableBorder withColumnBorders>
            <Table.Thead>
              <Table.Tr>
                <Table.Th>Mã Học sinh</Table.Th>
                <Table.Th>Họ và Tên</Table.Th>
                <Table.Th>Lớp Học</Table.Th>
                <Table.Th>Trạng thái</Table.Th>
                <Table.Th>Hành động</Table.Th>
              </Table.Tr>
            </Table.Thead>
            <Table.Tbody>
              {rows && rows.length > 0 ? (
                rows
              ) : (
                <Table.Tr>
                  <Table.Td colSpan={5}>
                    <Text fw={500} ta="center">Không có dữ liệu</Text>
                  </Table.Td>
                </Table.Tr>
              )}
            </Table.Tbody>
          </Table>

          <Group justify="space-between" mt="md">
            <Select
              data={['10', '20', '50']}
              value={pageSize.toString()}
              onChange={(value) => setPageSize(Number(value))}
              label="Số lượng/trang"
              allowDeselect={false}
              size="xs"
            />
            {pagedStudents && pagedStudents.totalPages > 0 && (
              <Pagination
                total={pagedStudents?.totalPages || 1}
                value={pagedStudents?.pageNumber || 1}
                onChange={setPage}
                siblings={1}
                boundaries={1}
              />
            )}
            <Text size="sm">Tổng cộng: {pagedStudents?.totalCount || 0} học sinh</Text>
          </Group>
        </>
      )}

      <StudentFormModal
        opened={openedModal}
        onClose={closeModal}
        onSubmit={handleFormSubmit}
        initialData={selectedStudent}
        isSubmitting={createStudentMutation.isPending || updateStudentMutation.isPending}
        lops={lops}
        trangThais={trangThais}
      />

      <Modal
        opened={openedDeleteConfirm}
        onClose={closeDeleteConfirm}
        title="Xác nhận xóa"
        centered
      >
        <Text>Bạn có chắc chắn muốn xóa học sinh này? Hành động này không thể hoàn tác.</Text>
        <Group justify="flex-end" mt="md">
          <Button variant="default" onClick={closeDeleteConfirm}>Hủy</Button>
          <Button color="red" onClick={confirmDelete} loading={deleteStudentMutation.isPending}>
            Xóa
          </Button>
        </Group>
      </Modal>
    </Container>
  );
}
