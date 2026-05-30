// QUAN-20260530-0942
import React from 'react';
import { Table, Button, Space, PaginationProps } from 'antd';
import { EditOutlined, EyeOutlined, DeleteOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { Student } from '../types/student';
import { useAuth } from '../utils/auth';

interface StudentTableProps {
  students: Student[];
  loading: boolean;
  pagination: PaginationProps;
  onView: (maHocSinh: string) => void;
  onEdit: (maHocSinh: string) => void;
  onDelete: (maHocSinh: string, hoTen: string) => void;
}

const StudentTable: React.FC<StudentTableProps> = ({ students, loading, pagination, onView, onEdit, onDelete }) => {
  const { hasRole } = useAuth();

  const columns = [
    {
      title: 'Mã học sinh',
      dataIndex: 'maHocSinh',
      key: 'maHocSinh',
      sorter: true,
      render: (text: string, record: Student) => (
        <a onClick={() => onView(record.maHocSinh)}>{text}</a>
      ),
    },
    {
      title: 'Họ và tên',
      dataIndex: 'hoTen',
      key: 'hoTen',
      sorter: true,
    },
    {
      title: 'Ngày sinh',
      dataIndex: 'ngaySinh',
      key: 'ngaySinh',
      render: (date: string) => dayjs(date).format('DD-MM-YYYY'),
      sorter: true,
    },
    {
      title: 'Giới tính',
      dataIndex: 'gioiTinh',
      key: 'gioiTinh',
      filters: [
        { text: 'Nam', value: 'Nam' },
        { text: 'Nữ', value: 'Nữ' },
        { text: 'Khác', value: 'Khác' },
      ],
      onFilter: (value: string | number | boolean, record: Student) => record.gioiTinh === value,
    },
    {
      title: 'Lớp học',
      dataIndex: 'maLopHoc',
      key: 'maLopHoc',
      sorter: true,
    },
    {
      title: 'Trạng thái',
      dataIndex: 'trangThai',
      key: 'trangThai',
      filters: [
        { text: 'Đang học', value: 'Đang học' },
        { text: 'Thôi học', value: 'Thôi học' },
        { text: 'Tạm nghỉ', value: 'Tạm nghỉ' },
      ],
      onFilter: (value: string | number | boolean, record: Student) => record.trangThai === value,
    },
    {
      title: 'Hành động',
      key: 'actions',
      render: (_: any, record: Student) => (
        <Space size="middle">
          <Button icon={<EyeOutlined />} onClick={() => onView(record.maHocSinh)}>
            Xem
          </Button>
          {hasRole('ADMIN') && (
            <>
              <Button icon={<EditOutlined />} onClick={() => onEdit(record.maHocSinh)}>
                Sửa
              </Button>
              <Button danger icon={<DeleteOutlined />} onClick={() => onDelete(record.maHocSinh, record.hoTen)}>
                Xóa
              </Button>
            </>
          )}
        </Space>
      ),
    },
  ];

  return (
    <Table
      columns={columns}
      dataSource={students}
      rowKey="maHocSinh"
      loading={loading}
      pagination={pagination}
      onChange={(pagination, filters, sorter, extra) => {
        // Handle sorting and filtering if not already managed by pagination object
        // For simplicity, pagination object is expected to handle change internally.
      }}
    />
  );
};

export default StudentTable;