// QUAN-20260530-2301
import { Table, Group, Button, Text, ActionIcon, Menu, ScrollArea } from '@mantine/core';
import { IconEdit, IconTrash, IconDotsVertical, IconChevronUp, IconChevronDown } from '@tabler/icons-react';
import type { StudentListDto, StudentListFilter } from '../types/student';
import { Link } from 'react-router-dom';

interface StudentTableProps {
  data: StudentListDto[];
  onDelete: (id: string) => void;
  onSortChange: (sortBy: string | null, sortOrder: 'asc' | 'desc') => void;
  currentSortBy?: string;
  currentSortOrder?: 'asc' | 'desc';
}

export function StudentTable({ data, onDelete, onSortChange, currentSortBy, currentSortOrder }: StudentTableProps) {
  const getSortIcon = (columnName: string) => {
    if (currentSortBy === columnName) {
      return currentSortOrder === 'asc' ? <IconChevronUp size="0.9rem" /> : <IconChevronDown size="0.9rem" />;
    }
    return null;
  };

  const handleSortClick = (columnName: string) => {
    let newSortOrder: 'asc' | 'desc' = 'asc';
    if (currentSortBy === columnName) {
      newSortOrder = currentSortOrder === 'asc' ? 'desc' : 'asc';
    }
    onSortChange(columnName, newSortOrder);
  };

  const rows = data.map((item) => (
    <tr key={item.id}>
      <td>{item.maHocSinh}</td>
      <td>
        <Link to={`/students/${item.id}`} style={{ textDecoration: 'none' }}>
          <Text component="span" variant="link">{item.hoVaTen}</Text>
        </Link>
      </td>
      <td>{item.tenLop}</td>
      <td>{item.tenTrangThai}</td>
      <td>
        <Group spacing={4} position="right">
          <ActionIcon component={Link} to={`/students/edit/${item.id}`} variant="light" color="blue" size="md">
            <IconEdit size="1rem" />
          </ActionIcon>
          <ActionIcon variant="light" color="red" size="md" onClick={() => onDelete(item.id)}>
            <IconTrash size="1rem" />
          </ActionIcon>
        </Group>
      </td>
    </tr>
  ));

  return (
    <ScrollArea>
      <Table miw={800} verticalSpacing="sm" highlightOnHover>
        <thead>
          <tr>
            <th>
              <Group spacing="xs" onClick={() => handleSortClick('maHocSinh')} style={{ cursor: 'pointer' }}>
                <Text fw={500}>Mã Học Sinh</Text>
                {getSortIcon('maHocSinh')}
              </Group>
            </th>
            <th>
              <Group spacing="xs" onClick={() => handleSortClick('hoVaTen')} style={{ cursor: 'pointer' }}>
                <Text fw={500}>Họ và Tên</Text>
                {getSortIcon('hoVaTen')}
              </Group>
            </th>
            <th>
              <Group spacing="xs" onClick={() => handleSortClick('lopHoc')} style={{ cursor: 'pointer' }}>
                <Text fw={500}>Lớp Học</Text>
                {getSortIcon('lopHoc')}
              </Group>
            </th>
            <th>
              <Group spacing="xs" onClick={() => handleSortClick('trangThai')} style={{ cursor: 'pointer' }}>
                <Text fw={500}>Trạng Thái</Text>
                {getSortIcon('trangThai')}
              </Group>
            </th>
            <th style={{ width: 100, textAlign: 'right' }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {rows.length > 0 ? (
            rows
          ) : (
            <tr>
              <td colSpan={5}>
                <Text weight={500} align="center">
                  Không tìm thấy học sinh nào.
                </Text>
              </td>
            </tr>
          )}
        </tbody>
      </Table>
    </ScrollArea>
  );
}