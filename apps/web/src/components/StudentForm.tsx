// QUAN-20260530-0942
import React, { useEffect, useState } from 'react';
import { Form, Input, Select, DatePicker, Button, Space, notification } from 'antd';
import dayjs from 'dayjs';
import { Student, StudentRequest } from '../types/student';
import { LopHoc } from '../types/lopHoc';
import { getLopHocList, checkLopHocExists } from '../api/lopHocApi'; // Import for LopHoc dropdown
import { checkStudentAge } from '../utils/validation';

const { Option } = Select;

interface StudentFormProps {
  initialValues?: Student;
  onSubmit: (values: StudentRequest) => void;
  onCancel: () => void;
  isEdit?: boolean;
  loading?: boolean;
}

const StudentForm: React.FC<StudentFormProps> = ({ initialValues, onSubmit, onCancel, isEdit = false, loading = false }) => {
  const [form] = Form.useForm();
  const [lopHocOptions, setLopHocOptions] = useState<LopHoc[]>([]);

  useEffect(() => {
    form.setFieldsValue({
      ...initialValues,
      ngaySinh: initialValues?.ngaySinh ? dayjs(initialValues.ngaySinh) : undefined,
    });
  }, [initialValues, form]);

  useEffect(() => {
    const fetchLopHoc = async () => {
      try {
        const classes = await getLopHocList();
        setLopHocOptions(classes);
      } catch (error) {
        notification.error({
          message: 'Lỗi tải danh sách lớp học',
          description: 'Không thể tải danh sách lớp học từ hệ thống.',
        });
      }
    };
    fetchLopHoc();
  }, []);

  const handleFormSubmit = (values: any) => {
    const formattedValues: StudentRequest = {
      ...values,
      ngaySinh: values.ngaySinh ? values.ngaySinh.format('YYYY-MM-DD') : undefined,
    };
    onSubmit(formattedValues);
  };

  return (
    <Form
      form={form}
      layout="vertical"
      onFinish={handleFormSubmit}
      initialValues={{ trangThai: 'Đang học', ...initialValues, ngaySinh: initialValues?.ngaySinh ? dayjs(initialValues.ngaySinh) : undefined }}
    >
      <Form.Item
        name="maHocSinh"
        label="Mã học sinh"
        rules={[
          { required: true, message: 'Vui lòng nhập mã học sinh!' },
          { max: 20, message: 'Mã học sinh không được vượt quá 20 ký tự.' },
          { pattern: /^[a-zA-Z0-9_-]*$/, message: 'Mã học sinh chỉ chứa chữ cái, số, dấu gạch ngang/dưới.' },
        ]}
      >
        <Input placeholder="Ví dụ: HS001" disabled={isEdit} />
      </Form.Item>

      <Form.Item
        name="hoTen"
        label="Họ và tên"
        rules={[
          { required: true, message: 'Vui lòng nhập họ và tên!' },
          { max: 100, message: 'Họ và tên không được vượt quá 100 ký tự.' },
        ]}
      >
        <Input placeholder="Ví dụ: Nguyễn Văn A" />
      </Form.Item>

      <Form.Item
        name="ngaySinh"
        label="Ngày sinh"
        rules={[
          { required: true, message: 'Vui lòng chọn ngày sinh!' },
          ({ getFieldValue }) => ({
            validator(_, value) {
              if (!value) return Promise.resolve();
              const dateOfBirth = value.toDate();
              const today = new Date();
              if (dateOfBirth > today) {
                return Promise.reject(new Error('Ngày sinh không được là ngày trong tương lai!'));
              }
              if (!checkStudentAge(dateOfBirth)) {
                return Promise.reject(new Error('Tuổi học sinh phải nằm trong khoảng từ 5 đến 20 tuổi!'));
              }
              return Promise.resolve();
            },
          }),
        ]}
      >
        <DatePicker style={{ width: '100%' }} format="YYYY-MM-DD" placeholder="Chọn ngày sinh" />
      </Form.Item>

      <Form.Item
        name="gioiTinh"
        label="Giới tính"
        rules={[{ required: true, message: 'Vui lòng chọn giới tính!' }]}
      >
        <Select placeholder="Chọn giới tính">
          <Option value="Nam">Nam</Option>
          <Option value="Nữ">Nữ</Option>
          <Option value="Khác">Khác</Option>
        </Select>
      </Form.Item>

      <Form.Item
        name="diaChi"
        label="Địa chỉ"
        rules={[{ max: 255, message: 'Địa chỉ không được vượt quá 255 ký tự.' }]}
      >
        <Input.TextArea rows={2} placeholder="Nhập địa chỉ" />
      </Form.Item>

      <Form.Item
        name="soDienThoaiPH"
        label="SĐT Phụ huynh"
        rules={[
          {
            pattern: /^(0|\+84)([3|5|7|8|9])+([0-9]{8})$|^$/,
            message: 'Số điện thoại không hợp lệ (ví dụ: 0XXXXXXXXX hoặc +84XXXXXXXXX).',
          },
        ]}
      >
        <Input placeholder="Ví dụ: 09xxxxxxxx" />
      </Form.Item>

      <Form.Item
        name="emailPH"
        label="Email Phụ huynh"
        rules={[
          { type: 'email', message: 'Email không hợp lệ!' },
          { max: 100, message: 'Email không được vượt quá 100 ký tự.' },
        ]}
      >
        <Input placeholder="Ví dụ: phuhuynh@example.com" />
      </Form.Item>

      <Form.Item
        name="maLopHoc"
        label="Mã lớp học"
        rules={[
          { required: true, message: 'Vui lòng nhập mã lớp học!' },
          {
            validator: async (_, value) => {
              if (!value) return Promise.resolve();
              const exists = await checkLopHocExists(value);
              if (!exists) {
                return Promise.reject(new Error('Mã lớp học không tồn tại!'));
              }
              return Promise.resolve();
            },
          },
        ]}
      >
        {/* Using a Select with dynamic options from LopHoc module */}
        <Select
          showSearch
          placeholder="Chọn hoặc nhập mã lớp học"
          optionFilterProp="children"
          filterOption={(input, option) =>
            (option?.children as string)?.toLowerCase().indexOf(input.toLowerCase()) >= 0 ||
            (option?.value as string)?.toLowerCase().indexOf(input.toLowerCase()) >= 0
          }
        >
          {lopHocOptions.map((lop) => (
            <Option key={lop.maLopHoc} value={lop.maLopHoc}>
              {lop.tenLop} ({lop.maLopHoc})
            </Option>
          ))}
        </Select>
      </Form.Item>

      <Form.Item
        name="trangThai"
        label="Trạng thái"
        rules={[{ required: true, message: 'Vui lòng chọn trạng thái!' }]}
      >
        <Select placeholder="Chọn trạng thái">
          <Option value="Đang học">Đang học</Option>
          <Option value="Thôi học">Thôi học</Option>
          <Option value="Tạm nghỉ">Tạm nghỉ</Option>
        </Select>
      </Form.Item>

      <Form.Item>
        <Space>
          <Button type="primary" htmlType="submit" loading={loading}>
            {isEdit ? 'Lưu thay đổi' : 'Thêm mới'}
          </Button>
          <Button htmlType="button" onClick={onCancel} disabled={loading}>
            Hủy
          </Button>
        </Space>
      </Form.Item>
    </Form>
  );
};

export default StudentForm;