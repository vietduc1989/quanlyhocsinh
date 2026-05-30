// QUAN-20260530-0942
import React, { useState, useEffect, useCallback } from 'react';
import { Button, Input, Select, Space, Typography, notification, Flex } from 'antd';
import { PlusOutlined, SearchOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import StudentTable from '../components/StudentTable';
import ConfirmationModal from '../components/ConfirmationModal';
import { Student } from '../types/student';
import { getStudents, deleteStudent } from '../api/studentApi';
import { useAuth } from '../utils/auth';
import { LopHoc } from '../types/lopHoc';
import { getLopHocList } from '../api/lopHocApi';

const { Title } = Typography;
const { Search } = Input;
const { Option } = Select;

const StudentListPage: React.FC = () => {
  const navigate = useNavigate();
  const { hasRole } = useAuth();

  const [students, setStudents] = useState<Student[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [pagination, setPagination] = useState({
    current: 1,
    pageSize: 10,
    total: 0,
    pageSizeOptions: ['10', '25', '50', '100'],
    showSizeChanger: true,
    showTotal: (total: number, range: [number, number]) => `Hiển thị ${range[0]}-${range[1]} trên tổng số ${total} học sinh`,
  });
  const [sortBy, setSortBy] = useState<string>('maHocSinh');
  const [sortDir, setSortDir] = useState<'asc' | 'desc'>('asc');
  const [keyword, setKeyword] = useState<string>('');
  const [searchMaLopHoc, setSearchMaLopHoc] = useState<string>('');
  const [lopHocFilterOptions, setLopHocFilterOptions] = useState<LopHoc[]>([]);

  // State for delete confirmation modal
  const [isDeleteModalVisible, setIsDeleteModalVisible] = useState(false);
  const [studentToDelete, setStudentToDelete] = useState<{ maHocSinh: string; hoTen: string } | null>(null);
  const [isDeleting, setIsDeleting] = useState(false);

  const fetchStudents = useCallback(async () => {
    setLoading(true);
    try {
      const response = await getStudents(
        pagination.current - 1,
        pagination.pageSize,
        sortBy,
        sortDir,
        keyword,
        searchMaLopHoc
      );
      setStudents(response.content);
      setPagination((prev) => ({
        ...prev,
        total: response.totalElements,
        current: response.pageNo + 1,
        pageSize: response.pageSize,
      }));
    } catch (error: any) {
      notification.error({
        message: 'Lỗi tải danh sách học sinh',
        description: error.message || 'Không thể tải dữ liệu học sinh.',
      });
    } finally {
      setLoading(false);
    }
  }, [pagination.current, pagination.pageSize, sortBy, sortDir, keyword, searchMaLopHoc]);

  useEffect(() => {
    fetchStudents();
  }, [fetchStudents]);

  useEffect(() => {
    const fetchLopHocOptions = async () => {
      try {
        const classes = await getLopHocList();
        setLopHocFilterOptions(classes);
      } catch (error) {
        notification.error({
          message: 'Lỗi tải danh sách lớp học',
          description: 'Không thể tải danh sách lớp học cho bộ lọc.',
        });
      }
    };
    fetchLopHocOptions();
  }, []);

  const handleTableChange = (pag: any, filters: any, sorter: any) => {
    // Handle pagination change
    setPagination((prev) => ({
      ...prev,
      current: pag.current,
      pageSize: pag.pageSize,
    }));

    // Handle sorting change
    if (sorter.field && sorter.order) {
      setSortBy(sorter.field as string);
      setSortDir(sorter.order === 'ascend' ? 'asc' : 'desc');
    } else {
      setSortBy('maHocSinh'); // Default sort
      setSortDir('asc');
    }
  };

  const handleSearch = (value: string) => {
    setKeyword(value);
    setPagination((prev) => ({ ...prev, current: 1 })); // Reset to first page on new search
  };

  const handleLopHocFilterChange = (value: string) => {
    setSearchMaLopHoc(value);
    setPagination((prev) => ({ ...prev, current: 1 }));
  };

  const handleViewStudent = (maHocSinh: string) => {
    navigate(`/students/${maHocSinh}`);
  };

  const handleEditStudent = (maHocSinh: string) => {
    navigate(`/students/${maHocSinh}/edit`);
  };

  const handleDeleteClick = (maHocSinh: string, hoTen: string) => {
    setStudentToDelete({ maHocSinh, hoTen });
    setIsDeleteModalVisible(true);
  };

  const handleConfirmDelete = async () => {
    if (studentToDelete) {
      setIsDeleting(true);
      try {
        await deleteStudent(studentToDelete.maHocSinh);
        notification.success({
          message: 'Xóa thành công',
          description: `Học sinh ${studentToDelete.hoTen} đã được xóa mềm.`,
        });
        fetchStudents(); // Refresh the list
        setIsDeleteModalVisible(false);
        setStudentToDelete(null);
      } catch (error: any) {
        notification.error({
          message: 'Lỗi xóa học sinh',
          description: error.message || 'Không thể xóa học sinh.',
        });
      } finally {
        setIsDeleting(false);
      }
    }
  };

  const handleCancelDelete = () => {
    setIsDeleteModalVisible(false);
    setStudentToDelete(null);
  };

  return (
    <div>
      <Flex justify="space-between" align="center" style={{ marginBottom: 20 }}>
        <Title level={3} style={{ margin: 0 }}>Danh sách Học sinh</Title>
        {hasRole('ADMIN') && (
          <Button type="primary" icon={<PlusOutlined />} onClick={() => navigate('/students/new')}>
            Thêm mới học sinh
          </Button>
        )}
      </Flex>

      <Space style={{ marginBottom: 16, width: '100%', flexWrap: 'wrap' }}>
        <Search
          placeholder="Tìm kiếm theo mã, tên, lớp học"
          onSearch={handleSearch}
          enterButton={<SearchOutlined />}
          style={{ width: 300 }}
          allowClear
        />
        <Select
          placeholder="Lọc theo lớp học"
          style={{ width: 200 }}
          onChange={handleLopHocFilterChange}
          allowClear
          showSearch
          optionFilterProp="children"
          filterOption={(input, option) =>
            (option?.children as string)?.toLowerCase().includes(input.toLowerCase()) ||
            (option?.value as string)?.toLowerCase().includes(input.toLowerCase())
          }
        >
          {lopHocFilterOptions.map(lop => (
            <Option key={lop.maLopHoc} value={lop.maLopHoc}>{lop.tenLop} ({lop.maLopHoc})</Option>
          ))}
        </Select>
      </Space>

      <StudentTable
        students={students}
        loading={loading}
        pagination={{ ...pagination, onChange: handleTableChange }}
        onView={handleViewStudent}
        onEdit={handleEditStudent}
        onDelete={handleDeleteClick}
      />

      <ConfirmationModal
        visible={isDeleteModalVisible}
        onConfirm={handleConfirmDelete}
        onCancel={handleCancelDelete}
        title="Xác nhận xóa học sinh"
        content={`Bạn có chắc chắn muốn xóa học sinh "${studentToDelete?.hoTen}" (Mã: ${studentToDelete?.maHocSinh}) không? Học sinh sẽ được chuyển sang trạng thái "Đã xóa".`}
        okText="Xóa"
        cancelText="Hủy"
        loading={isDeleting}
      />
    </div>
  );
};

export default StudentListPage;