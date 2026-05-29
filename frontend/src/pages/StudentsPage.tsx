/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Frontend (Main Pages)
 */

import React, { useState, useEffect } from 'react';
import { Container, Title, Button, Group, Table, Pagination, TextInput, ActionIcon, FileButton, Anchor } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { notifications } from '@mantine/notifications';
import { studentApi } from '../api/studentApi';
import { Student } from '../types/student';
import { StudentModal } from '../components/StudentModal';

export const StudentsPage: React.FC = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [search, setSearch] = useState('');
  const [activePage, setActivePage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [modalOpened, { open, close }] = useDisclosure(false);

  const fetchStudents = async () => {
    try {
      const result = await studentApi.getStudents(search, activePage);
      if (result.success) {
        setStudents(result.data);
        setTotalPages(result.pagination.totalPages || 1);
      }
    } catch (error) {
      notifications.show({ title: 'Lỗi', message: 'Không thể tải danh sách học sinh', color: 'red' });
    }
  };

  useEffect(() => {
    fetchStudents();
  }, [activePage]);

  const handleSearch = () => {
    setActivePage(1);
    fetchStudents();
  };

  const handleClear = () => {
    setSearch('');
    setActivePage(1);
    setTimeout(() => fetchStudents(), 0);
  };

  const handleSaveStudent = async (values: any) => {
    try {
      if (selectedStudent) {
        await studentApi.updateStudent(selectedStudent.id, values);
        notifications.show({ title: 'Thành công', message: 'Cập nhật học sinh thành công', color: 'green' });
      } else {
        await studentApi.createStudent(values);
        notifications.show({ title: 'Thành công', message: 'Thêm mới học sinh thành công', color: 'green' });
      }
      fetchStudents();
    } catch (err: any) {
      notifications.show({ title: 'Thất bại', message: err.response?.data?.message || 'Có lỗi xảy ra', color: 'red' });
    }
  };

  const handleDelete = async (student: Student) => {
    if (confirm(`Bạn có chắc chắn muốn xóa học sinh [${student.fullName}] ra khỏi hệ thống không?`)) {
      try {
        await studentApi.deleteStudent(student.id);
        notifications.show({ title: 'Thành công', message: 'Xóa học sinh thành công', color: 'green' });
        fetchStudents();
      } catch {
        notifications.show({ title: 'Lỗi', message: 'Không thể xóa học sinh này', color: 'red' });
      }
    }
  };

  const handleImportExcel = async (file: File | null) => {
    if (!file) return;
    try {
      const response = await studentApi.importStudents(file);
      
      // Nếu có blob Excel báo lỗi trả về
      if (response.data instanceof Blob && response.data.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
        const blobUrl = window.URL.createObjectURL(response.data);
        const link = document.createElement('a');
        link.href = blobUrl;
        link.setAttribute('download', 'Bao_Cao_Loi_Import.xlsx');
        document.body.appendChild(link);
        link.click();
        link.remove();
        notifications.show({ title: 'Thất bại', message: 'Import có dòng bị lỗi dữ liệu. Đã tải file lỗi chi tiết.', color: 'orange' });
      } else {
        notifications.show({ title: 'Thành công', message: 'Import dữ liệu thành công', color: 'green' });
      }
      fetchStudents();
    } catch (err) {
      notifications.show({ title: 'Lỗi', message: 'Import file lỗi hoặc file sai cấu trúc', color: 'red' });
    }
  };

  const rows = students.map((element, index) => (
    <Table.Tr key={element.id}>
      <Table.Td>{(activePage - 1) * 20 + index + 1}</Table.Td>
      <Table.Td>{element.studentCode}</Table.Td>
      <Table.Td style={{ fontWeight: 500 }}>{element.fullName}</Table.Td>
      <Table.Td>{new Date(element.dateOfBirth).toLocaleDateString('vi-VN')}</Table.Td>
      <Table.Td>{element.gender}</Table.Td>
      <Table.Td>{element.parentPhone}</Table.Td>
      <Table.Td>{element.email || '-'}</Table.Td>
      <Table.Td>
        <Group gap="xs">
          <Button size="xs" variant="light" onClick={() => { setSelectedStudent(element); open(); }}>Sửa</Button>
          <Button size="xs" variant="light" color="red" onClick={() => handleDelete(element)}>Xóa</Button>
        </Group>
      </Table.Td>
    </Table.Tr>
  ));

  return (
    <Container size="xl" py="md">
      <Group justify="space-between" mb="lg">
        <Title order={2}>Quản lý Hồ sơ Học sinh</Title>
        <Group>
          <Button color="blue" onClick={() => { setSelectedStudent(null); open(); }}>Thêm mới học sinh</Button>
          <FileButton onChange={handleImportExcel} accept=".xlsx">
            {(props) => <Button variant="outline" {...props}>Import Excel</Button>}
          </FileButton>
          <Anchor href={studentApi.exportStudentsUrl(search)} target="_blank">
            <Button variant="outline" color="green">Export Excel</Button>
          </Anchor>
        </Group>
      </Group>

      <Group mb="md">
        <TextInput
          placeholder="Tìm kiếm theo Mã HS hoặc Họ tên..."
          style={{ flexGrow: 1 }}
          value={search}
          onChange={(e) => setSearch(e.currentTarget.value)}
        />
        <Button onClick={handleSearch}>Tìm kiếm</Button>
        <Button variant="subtle" color="gray" onClick={handleClear}>Làm mới</Button>
      </Group>

      <Table.ScrollContainer minWidth={800}>
        <Table striped highlightOnHover withBorder withColumnBorders>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>STT</Table.Th>
              <Table.Th>Mã học sinh</Table.Th>
              <Table.Th>Họ và tên</Table.Th>
              <Table.Th>Ngày sinh</Table.Th>
              <Table.Th>Giới tính</Table.Th>
              <Table.Th>SĐT Phụ huynh</Table.Th>
              <Table.Th>Email</Table.Th>
              <Table.Th>Hành động</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>{rows.length > 0 ? rows : <Table.Tr><Table.Td colSpan={8} style={{ textAlign: 'center' }}>Không có dữ liệu học sinh</Table.Td></Table.Tr>}</Table.Tbody>
        </Table>
      </Table.ScrollContainer>

      <Group justify="flex-end" mt="md">
        <Pagination total={totalPages} value={activePage} onChange={setActivePage} />
      </Group>

      <StudentModal
        opened={modalOpened}
        onClose={close}
        student={selectedStudent}
        onSave={handleSaveStudent}
      />
    </Container>
  );
};