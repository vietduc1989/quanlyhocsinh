// QUAN-20260604-153038
import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
  Title,
  Button,
  Group,
  TextInput,
  Select,
  Textarea,
  LoadingOverlay,
  Box,
  Flex,
} from '@mantine/core';
import { DateInput } from '@mantine/dates';
import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import { IconArrowLeft, IconDeviceFloppy } from '@tabler/icons-react';
import { HocSinhFormValues, GioiTinh, TrangThaiHocSinh, LopHocLookup } from '../../types/hocSinh';
import { hocSinhService } from '../../api/hocSinhService';
import dayjs from 'dayjs';

const HocSinhFormPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const isEditing = !!id;
  const [loading, setLoading] = useState(false);
  const [lopHocLookup, setLopHocLookup] = useState<LopHocLookup[]>([]);

  const form = useForm<HocSinhFormValues>({
    initialValues: {
      hoTen: '',
      ngaySinh: '',
      gioiTinh: 'Nam',
      diaChi: '',
      soDienThoaiPH: '',
      emailPH: '',
      lopHocId: '',
      trangThai: 'DangHoc',
    },
    validate: {
      hoTen: (value) => (value ? null : 'Họ và Tên không được để trống.'),
      ngaySinh: (value) => {
        if (!value) return 'Ngày Sinh không được để trống.';
        const date = dayjs(value);
        if (!date.isValid()) return 'Ngày Sinh không hợp lệ.';
        if (date.isAfter(dayjs())) return 'Ngày Sinh không được lớn hơn hoặc bằng ngày hiện tại.';
        return null;
      },
      gioiTinh: (value) => (value ? null : 'Giới Tính không được để trống.'),
      lopHocId: (value) => (value ? null : 'Lớp Học không được để trống.'),
      trangThai: (value) => (value ? null : 'Trạng Thái không được để trống.'),
      soDienThoaiPH: (value) => {
        if (!value) return null;
        // Basic Vietnamese phone number regex (10 digits, starts with 0)
        return /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(value)
          ? null
          : 'Số Điện Thoại Phụ Huynh không đúng định dạng.';
      },
      emailPH: (value) => {
        if (!value) return null;
        return /^\S+@\S+\.\S+$/.test(value) ? null : 'Email Phụ Huynh không đúng định dạng.';
      },
    },
  });

  const trangThaiOptions: { value: TrangThaiHocSinh; label: string }[] = [
    { value: 'DangHoc', label: 'Đang học' },
    { value: 'DaTotNghiep', label: 'Đã tốt nghiệp' },
    { value: 'DaChuyenTruong', label: 'Đã chuyển trường' },
    { value: 'TamDung', label: 'Tạm dừng' },
  ];

  const gioiTinhOptions: { value: GioiTinh; label: string }[] = [
    { value: 'Nam', label: 'Nam' },
    { value: 'Nu', label: 'Nữ' },
    { value: 'Khac', label: 'Khác' },
  ];

  useEffect(() => {
    const fetchFormData = async () => {
      setLoading(true);
      try {
        const lopHocsResponse = await hocSinhService.getLopHocsForLookup();
        if (lopHocsResponse.success) {
          setLopHocLookup(lopHocsResponse.data);
        } else {
          notifications.show({
            title: 'Lỗi',
            message: lopHocsResponse.message || 'Không thể tải danh sách lớp học.',
            color: 'red',
          });
        }

        if (isEditing && id) {
          const hocSinhResponse = await hocSinhService.getHocSinhById(id);
          if (hocSinhResponse.success && hocSinhResponse.data) {
            const hocSinhData = hocSinhResponse.data;
            form.setValues({
              id: hocSinhData.id,
              hoTen: hocSinhData.hoTen,
              ngaySinh: hocSinhData.ngaySinh,
              gioiTinh: hocSinhData.gioiTinh,
              diaChi: hocSinhData.diaChi,
              soDienThoaiPH: hocSinhData.soDienThoaiPH,
              emailPH: hocSinhData.emailPH,
              lopHocId: hocSinhData.lopHocId,
              trangThai: hocSinhData.trangThai,
            });
          } else {
            notifications.show({
              title: 'Lỗi',
              message: hocSinhResponse.message || 'Không thể tải thông tin học sinh.',
              color: 'red',
            });
            navigate('/hoc-sinh');
          }
        }
      } catch (error) {
        console.error('Failed to fetch form data:', error);
        notifications.show({
          title: 'Lỗi',
          message: 'Đã xảy ra lỗi khi tải dữ liệu. Vui lòng thử lại.',
          color: 'red',
        });
        navigate('/hoc-sinh');
      } finally {
        setLoading(false);
      }
    };
    fetchFormData();
  }, [id, isEditing, navigate]);

  const handleSubmit = async (values: HocSinhFormValues) => {
    setLoading(true);
    try {
      let response;
      if (isEditing && id) {
        response = await hocSinhService.updateHocSinh(id, values);
      } else {
        response = await hocSinhService.createHocSinh(values);
      }

      if (response.success) {
        notifications.show({
          title: 'Thành công',
          message: response.message || (isEditing ? 'Cập nhật học sinh thành công.' : 'Thêm học sinh thành công.'),
          color: 'green',
        });
        navigate('/hoc-sinh');
      } else {
        notifications.show({
          title: 'Lỗi',
          message: response.message || 'Thao tác thất bại.',
          color: 'red',
        });
      }
    } catch (error) {
      console.error('Failed to submit form:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box pos="relative">
      <LoadingOverlay visible={loading} zIndex={1000} overlayProps={{ radius: 'sm', blur: 2 }} />

      <Group justify="space-between" mb="md">
        <Title order={2}>{isEditing ? 'Chỉnh sửa Học sinh' : 'Thêm mới Học sinh'}</Title>
        <Button variant="default" leftSection={<IconArrowLeft size="1rem" />} onClick={() => navigate('/hoc-sinh')}>
          Quay lại danh sách
        </Button>
      </Group>

      <form onSubmit={form.onSubmit(handleSubmit)}>
        <Flex direction="column" gap="md">
          {isEditing && (
            <TextInput
              label="Mã Học sinh"
              placeholder="Mã Học sinh"
              value={id} // Display existing ID but not editable
              disabled
              readOnly
            />
          )}
          <TextInput
            label="Họ và Tên"
            placeholder="Nhập họ và tên học sinh"
            required
            {...form.getInputProps('hoTen')}
          />
          <DateInput
            label="Ngày Sinh"
            placeholder="Chọn ngày sinh"
            value={form.values.ngaySinh ? dayjs(form.values.ngaySinh).toDate() : null}
            onChange={(date) => form.setFieldValue('ngaySinh', date ? dayjs(date).format('YYYY-MM-DD') : '')}
            error={form.errors.ngaySinh}
            clearable
            maxDate={new Date()} // BR03: Not in future
            required
          />
          <Select
            label="Giới Tính"
            placeholder="Chọn giới tính"
            data={gioiTinhOptions}
            required
            {...form.getInputProps('gioiTinh')}
          />
          <Textarea
            label="Địa Chỉ"
            placeholder="Nhập địa chỉ"
            {...form.getInputProps('diaChi')}
          />
          <TextInput
            label="Số Điện Thoại Phụ Huynh"
            placeholder="Nhập số điện thoại phụ huynh"
            {...form.getInputProps('soDienThoaiPH')}
          />
          <TextInput
            label="Email Phụ Huynh"
            placeholder="Nhập email phụ huynh"
            {...form.getInputProps('emailPH')}
          />
          <Select
            label="Lớp Học"
            placeholder="Chọn lớp học"
            data={lopHocLookup.map((l) => ({ value: l.id, label: l.tenLop }))}
            required
            {...form.getInputProps('lopHocId')}
          />
          <Select
            label="Trạng Thái"
            placeholder="Chọn trạng thái"
            data={trangThaiOptions}
            required
            {...form.getInputProps('trangThai')}
          />

          <Group justify="flex-end" mt="xl">
            <Button variant="default" onClick={() => navigate('/hoc-sinh')}>
              Hủy
            </Button>
            <Button type="submit" leftSection={<IconDeviceFloppy size="1rem" />}>
              {isEditing ? 'Cập nhật' : 'Thêm mới'}
            </Button>
          </Group>
        </Flex>
      </form>
    </Box>
  );
};

export default HocSinhFormPage;