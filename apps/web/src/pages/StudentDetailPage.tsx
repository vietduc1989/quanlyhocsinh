// QUAN-20260530-0942
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Card, Descriptions, Spin, Button, Space, Result, notification } from 'antd';
import { ArrowLeftOutlined, EditOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { Student } from '../types/student';
import { getStudentById } from '../api/studentApi';
import { useAuth } from '../utils/auth';

const StudentDetailPage: React.FC = () => {
  const { maHocSinh } = useParams<{ maHocSinh: string }>();
  const navigate = useNavigate();
  const [student, setStudent] = useState<Student | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const { hasRole } = useAuth();

  useEffect(() => {
    const fetchStudent = async () => {
      if (!maHocSinh) {
        setError("Mã học sinh không hợp lệ.");
        setLoading(false);
        return;
      }
      setLoading(true);
      try {
        const data = await getStudentById(maHocSinh);
        setStudent(data);
        setError(null);
      } catch (err: any) {
        console.error('Failed to fetch student details:', err);
        setError(err.response?.data?.message || err.message || 'Không thể tải thông tin học sinh.');
        notification.error({
          message: 'Lỗi',
          description: `Không thể tải thông tin học sinh: ${err.response?.data?.message || err.message}`,
        });
      } finally {
        setLoading(false);
      }
    };

    fetchStudent();
  }, [maHocSinh]);

  if (loading) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
        <Spin size="large" tip="Đang tải thông tin học sinh..." />
      </div>
    );
  }

  if (error) {
    return (
      <Result
        status="error"
        title="Không thể tải thông tin học sinh"
        subTitle={error}
        extra={
          <Button type="primary" onClick={() => navigate('/')}>
            Quay lại danh sách
          </Button>
        }
      />
    );
  }

  if (!student) {
    return (
      <Result
        status="404"
        title="Không tìm thấy học sinh"
        subTitle="Học sinh bạn đang tìm kiếm không tồn tại."
        extra={
          <Button type="primary" onClick={() => navigate('/')}>
            Quay lại danh sách
          </Button>
        }
      />
    );
  }

  return (
    <Card
      title={`Thông tin chi tiết học sinh: ${student.hoTen}`}
      extra={
        <Space>
          {hasRole('ADMIN') && (
            <Button type="primary" icon={<EditOutlined />} onClick={() => navigate(`/students/${student.maHocSinh}/edit`)}>
              Chỉnh sửa
            </Button>
          )}
          <Button icon={<ArrowLeftOutlined />} onClick={() => navigate('/')}>
            Quay lại
          </Button>
        </Space>
      }
      style={{ maxWidth: 900, margin: 'auto' }}
    >
      <Descriptions bordered column={{ xs: 1, sm: 2, md: 2, lg: 2 }}>
        <Descriptions.Item label="Mã học sinh">{student.maHocSinh}</Descriptions.Item>
        <Descriptions.Item label="Họ và tên">{student.hoTen}</Descriptions.Item>
        <Descriptions.Item label="Ngày sinh">{dayjs(student.ngaySinh).format('DD-MM-YYYY')}</Descriptions.Item>
        <Descriptions.Item label="Giới tính">{student.gioiTinh}</Descriptions.Item>
        <Descriptions.Item label="Địa chỉ" span={2}>{student.diaChi || 'N/A'}</Descriptions.Item>
        <Descriptions.Item label="SĐT Phụ huynh">{student.soDienThoaiPH || 'N/A'}</Descriptions.Item>
        <Descriptions.Item label="Email Phụ huynh">{student.emailPH || 'N/A'}</Descriptions.Item>
        <Descriptions.Item label="Mã lớp học">{student.maLopHoc}</Descriptions.Item>
        <Descriptions.Item label="Trạng thái">{student.trangThai}</Descriptions.Item>
        <Descriptions.Item label="Ngày tạo">{dayjs(student.ngayTao).format('DD-MM-YYYY HH:mm:ss')}</Descriptions.Item>
        <Descriptions.Item label="Người tạo">{student.nguoiTao}</Descriptions.Item>
        {student.ngayCapNhatCuoi && (
          <Descriptions.Item label="Cập nhật cuối">{dayjs(student.ngayCapNhatCuoi).format('DD-MM-YYYY HH:mm:ss')}</Descriptions.Item>
        )}
        {student.nguoiCapNhatCuoi && (
          <Descriptions.Item label="Người cập nhật">{student.nguoiCapNhatCuoi}</Descriptions.Item>
        )}
      </Descriptions>
    </Card>
  );
};

export default StudentDetailPage;