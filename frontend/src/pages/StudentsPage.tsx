import React, { useState } from 'react';
import {
  Table,
  Button,
  TextInput,
  Select,
  Pagination,
  Group,
  Stack,
  Title,
  Card,
  ActionIcon,
  Modal,
  Text,
  Badge,
  Grid
} from '@mantine/core';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import { studentApi, Student } from '../api/studentApi';

export const StudentsPage: React.FC = () => {
  const queryClient = useQueryClient();
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedClass, setSelectedClass] = useState<string | null>(null);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(10);
  
  // State quản lý Modal thêm/sửa học sinh
  const [opened, setOpened] = useState(false);
  const [selectedStudentId, setSelectedStudentId] = useState<string | null>(null);

  // Lấy dữ liệu danh sách học sinh
  const { data, isLoading } = useQuery({
    queryKey: ['students', searchTerm, selectedClass, page, pageSize],
    queryFn: () => studentApi.getStudents({
      searchTerm,
      classId: selectedClass || undefined,
      pageNumber: page,
      pageSize
    })
  });

  // Mock data danh sách lớp học
  const classOptions = [
    { value: '7c8b0561-bd3e-4861-ba0f-07e11942fc0c', label: 'Lớp 10A' },
    { value: '5a8b0561-bd3e-4861-ba0f-07e11942fc1a', label: 'Lớp 11B' },
  ];

  const form = useForm<Student>({
    initialValues: {
      studentCode: '',
      fullName: '',
      dateOfBirth: '',
      gender: 'Nam',
      address: '',
      phoneNumber: '',
      email: '',
      classId: '',
      parentName: '',
      parentPhoneNumber: ''
    },
    validate: {
      studentCode: (value) => (value ? null : 'Mã học sinh không được để trống'),
      fullName: (value) => (value ? null : 'Họ tên không được để trống'),
      dateOfBirth: (value) => (value ? null : 'Vui lòng chọn ngày sinh'),
      classId: (value) => (value ? null : 'Vui lòng chọn lớp học'),
      email: (value) => (!value || /^\S+@\S+$/.test(value) ? null : 'Địa chỉ email không đúng định dạng')
    }
  });

  // Mutation thêm mới hoặc cập nhật học sinh
  const saveMutation = useMutation({
    mutationFn: (values: Student) => {
      if (selectedStudentId) {
        return studentApi.updateStudent(selectedStudentId, values);
      }
      return studentApi.createStudent(values);
    },
    onSuccess: () => {
      notifications.show({
        title: 'Thành công',
        message: selectedStudentId ? 'Cập nhật thông tin thành công!' : 'Thêm mới học sinh thành công!',
        color: 'green'
      });
      queryClient.invalidateQueries({ queryKey: ['students'] });
      handleCloseModal();
    },
    onError: (error: any) => {
      notifications.show({
        title: 'Thất bại',
        message: error.response?.data?.message || 'Có lỗi xảy ra',
        color: 'red'
      });
    }
  });

  const deleteMutation = useMutation({
    mutationFn: (id: string) => studentApi.deleteStudent(id),
    onSuccess: () => {
      notifications.show({
        title: 'Xóa thành công',
        message: 'Thông tin học sinh đã được dọn dẹp mềm khỏi hệ thống.',
        color: 'teal'
      });
      queryClient.invalidateQueries({ queryKey: ['students'] });
    }
  });

  const handleOpenAdd = () => {
    setSelectedStudentId(null);
    form.reset();
    setOpened(true);
  };

  const handleOpenEdit = (student: Student) => {
    setSelectedStudentId(student.id || null);
    form.setValues({
      ...student,
      dateOfBirth: student.dateOfBirth.split('T')[0] // format date string
    });
    setOpened(true);
  };

  const handleCloseModal = () => {
    setOpened(false);
    setSelectedStudentId(null);
  };

  const handleDelete = (id: string) => {
    if (confirm('Bạn có chắc chắn muốn vô hiệu hóa học sinh này khỏi hệ thống?')) {
      deleteMutation.mutate(id);
    }
  };

  return (
    <Stack gap="lg" p="md">
      <Group justify="space-between">
        <Title order={2}>Quản lý học sinh - ONENET Phase 2</Title>
        <Button onClick={handleOpenAdd} color="blue">
          Thêm học sinh mới
        </Button>
      </Group>

      <Card withBorder shadow="xs" radius="md">
        <Grid align="end" mb="md">
          <Grid.Col span={{ base: 12, sm: 6, md: 4 }}>
            <TextInput
              placeholder="Nhập tên hoặc mã học sinh..."
              label="Tìm kiếm nhanh"
              value={searchTerm}
              onChange={(e) => {
                setSearchTerm(e.currentTarget.value);
                setPage(1);
              }}
            />
          </Grid.Col>
          <Grid.Col span={{ base: 12, sm: 6, md: 4 }}>
            <Select
              label="Lọc theo Lớp học"
              placeholder="Chọn lớp học"
              data={classOptions}
              value={selectedClass}
              onChange={(val) => {
                setSelectedClass(val);
                setPage(1);
              }}
              clearable
            />
          </Grid.Col>
        </Grid>

        <Table striped highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Mã học sinh</Table.Th>
              <Table.Th>Họ và tên</Table.Th>
              <Table.Th>Ngày sinh</Table.Th>
              <Table.Th>Giới tính</Table.Th>
              <Table.Th>Lớp học</Table.Th>
              <Table.Th>Trạng thái</Table.Th>
              <Table.Th style={{ width: '120px' }}>Hành động</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {isLoading ? (
              <Table.Tr>
                <Table.Td colSpan={7}>
                  <Text align="center">Đang tải dữ liệu học sinh...</Text>
                </Table.Td>
              </Table.Tr>
            ) : data?.items.length === 0 ? (
              <Table.Tr>
                <Table.Td colSpan={7}>
                  <Text align="center">Không tìm thấy học sinh nào phù hợp.</Text>
                </Table.Td>
              </Table.Tr>
            ) : (
              data?.items.map((student) => (
                <Table.Tr key={student.id}>
                  <Table.Td>{student.studentCode}</Table.Td>
                  <Table.Td style={{ fontWeight: 500 }}>{student.fullName}</Table.Td>
                  <Table.Td>{new Date(student.dateOfBirth).toLocaleDateString('vi-VN')}</Table.Td>
                  <Table.Td>{student.gender}</Table.Td>
                  <Table.Td>{student.className}</Table.Td>
                  <Table.Td>
                    <Badge color={student.status === 1 ? 'green' : 'gray'}>
                      {student.statusText || 'Không xác định'}
                    </Badge>
                  </Table.Td>
                  <Table.Td>
                    <Group gap="xs">
                      <Button variant="subtle" size="xs" onClick={() => handleOpenEdit(student)}>
                        Sửa
                      </Button>
                      <Button variant="subtle" color="red" size="xs" onClick={() => handleDelete(student.id!)}>
                        Xóa
                      </Button>
                    </Group>
                  </Table.Td>
                </Table.Tr>
              ))
            )}
          </Table.Tbody>
        </Table>

        {data && (
          <Group justify="space-between" mt="md">
            <Text size="sm">
              Hiển thị {data.items.length} trên tổng số {data.totalCount} học sinh
            </Text>
            <Pagination total={data.totalPages} value={page} onChange={setPage} />
          </Group>
        )}
      </Card>

      {/* Modal CRUD Form */}
      <Modal
        opened={opened}
        onClose={handleCloseModal}
        title={selectedStudentId ? 'Cập nhật thông tin học sinh' : 'Thêm mới học sinh'}
        size="lg"
      >
        <form onSubmit={form.onSubmit((values) => saveMutation.mutate(values))}>
          <Stack gap="sm">
            <Grid>
              <Grid.Col span={6}>
                <TextInput
                  label="Mã học sinh"
                  placeholder="Ví dụ: HS001"
                  required
                  {...form.getInputProps('studentCode')}
                />
              </Grid.Col>
              <Grid.Col span={6}>
                <TextInput
                  label="Họ và tên"
                  placeholder="Nguyễn Văn A"
                  required
                  {...form.getInputProps('fullName')}
                />
              </Grid.Col>
            </Grid>

            <Grid>
              <Grid.Col span={6}>
                <TextInput
                  type="date"
                  label="Ngày sinh"
                  required
                  {...form.getInputProps('dateOfBirth')}
                />
              </Grid.Col>
              <Grid.Col span={6}>
                <Select
                  label="Giới tính"
                  placeholder="Chọn giới tính"
                  data={['Nam', 'Nữ', 'Khác']}
                  required
                  {...form.getInputProps('gender')}
                />
              </Grid.Col>
            </Grid>

            <Grid>
              <Grid.Col span={6}>
                <Select
                  label="Lớp học"
                  placeholder="Chọn lớp"
                  data={classOptions}
                  required
                  {...form.getInputProps('classId')}
                />
              </Grid.Col>
              <Grid.Col span={6}>
                <TextInput
                  label="Email liên lạc"
                  placeholder="example@onenet.vn"
                  {...form.getInputProps('email')}
                />
              </Grid.Col>
            </Grid>

            <TextInput
              label="Địa chỉ liên lạc"
              placeholder="Số 1, Đường Trần Hưng Đạo, Hà Nội"
              {...form.getInputProps('address')}
            />

            <Grid>
              <Grid.Col span={6}>
                <TextInput
                  label="Họ tên phụ huynh"
                  placeholder="Nguyễn Văn B"
                  {...form.getInputProps('parentName')}
                />
              </Grid.Col>
              <Grid.Col span={6}>
                <TextInput
                  label="SĐT phụ huynh"
                  placeholder="0987654321"
                  {...form.getInputProps('parentPhoneNumber')}
                />
              </Grid.Col>
            </Grid>

            <Group justify="end" mt="md">
              <Button variant="outline" onClick={handleCloseModal}>
                Hủy
              </Button>
              <Button type="submit" loading={saveMutation.isPending} color="blue">
                Lưu lại
              </Button>
            </Group>
          </Stack>
        </form>
      </Modal>
    </Stack>
  );
};