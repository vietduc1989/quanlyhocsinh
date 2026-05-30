// QUAN-20260530-0942
import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Card, Spin, notification, Button, Flex } from 'antd';
import { ArrowLeftOutlined } from '@ant-design/icons';
import StudentForm from '../components/StudentForm';
import { Student, StudentRequest } from '../types/student';
import { getStudentById, createStudent, updateStudent } from '../api/studentApi';

const StudentUpsertPage: React.FC = () => {
  const { maHocSinh } = useParams<{ maHocSinh: string }>(); // Will be undefined for new student
  const navigate = useNavigate();
  const isEdit = !!maHocSinh;

  const [initialStudentData, setInitialStudentData] = useState<Student | undefined>(undefined);
  const [loadingForm, setLoadingForm] = useState<boolean>(isEdit);
  const [submitting, setSubmitting] = useState<boolean>(false);

  useEffect(() => {
    if (isEdit && maHocSinh) {
      const fetchStudent = async () => {
        setLoadingForm(true);
        try {
          const data = await getStudentById(maHocSinh);
          setInitialStudentData(data);
        } catch (error: any) {
          notification.error({
            message: 'Lỗi tải dữ liệu',
            description: error.response?.data?.message || 'Không thể tải thông tin học sinh để chỉnh sửa.',
          });
          navigate('/'); // Redirect if student not found or error
        } finally {
          setLoadingForm(false);
        }
      };
      fetchStudent();
    }
  }, [isEdit, maHocSinh, navigate]);

  const handleSubmit = async (values: StudentRequest) => {
    setSubmitting(true);
    try {
      if (isEdit && maHocSinh) {
        await updateStudent(maHocSinh, values);
        notification.success({
          message: 'Cập nhật thành công',
          description: `Thông tin học sinh ${values.hoTen} đã được cập nhật.`,
        });
      } else {
        await createStudent(values);
        notification.success({
          message: 'Thêm mới thành công',
          description: `Học sinh ${values.hoTen} đã được thêm vào hệ thống.`,
        });
      }
      navigate('/'); // Go back to list page
    } catch (error: any) {
      // Error handling is mostly done by apiClient interceptors.
      // Specific form errors might be handled here if needed.
    } finally {
      setSubmitting(false);
    }
  };

  const handleCancel = () => {
    navigate('/'); // Go back to list page
  };

  return (
    <Card
      title={isEdit ? `Chỉnh sửa Học sinh: ${maHocSinh}` : 'Thêm mới Học sinh'}
      extra={
        <Button icon={<ArrowLeftOutlined />} onClick={handleCancel}>
          Quay lại
        </Button>
      }
      style={{ maxWidth: 700, margin: 'auto' }}
    >
      {loadingForm ? (
        <Flex justify="center" align="center" style={{ height: 300 }}>
          <Spin size="large" tip="Đang tải dữ liệu..." />
        </Flex>
      ) : (
        <StudentForm
          initialValues={initialStudentData}
          onSubmit={handleSubmit}
          onCancel={handleCancel}
          isEdit={isEdit}
          loading={submitting}
        />
      )}
    </Card>
  );
};

export default StudentUpsertPage;