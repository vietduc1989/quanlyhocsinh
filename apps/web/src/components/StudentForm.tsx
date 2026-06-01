// QUAN-20260530-2301
import { useEffect } from 'react';
import { useForm, hasLength, isNotEmpty, isEmail, matches, isNotEqualTo } from '@mantine/form';
import { TextInput, Button, Group, Select, Textarea, Box, Text, NativeSelect } from '@mantine/core';
import { DateInput } from '@mantine/dates';
import dayjs from 'dayjs';
import { useLopsAndTrangThai } from '../hooks/useLopsAndTrangThai';
import type { CreateStudentCommand, UpdateStudentCommand, StudentDetailDto } from '../types/student';
import { notifications } from '@mantine/notifications';

interface StudentFormProps {
  initialValues?: StudentDetailDto;
  onSubmit: (values: CreateStudentCommand | UpdateStudentCommand) => Promise<void>;
  isSubmitting: boolean;
  isEditMode?: boolean;
}

const PHONE_REGEX = /^\+?[0-9\s-]{7,15}$/;
const GENDERS = ['Nam', 'Nữ', 'Khác'];

export function StudentForm({ initialValues, onSubmit, isSubmitting, isEditMode = false }: StudentFormProps) {
  const { lops, trangThais, isLoadingLops, isLoadingTrangThais } = useLopsAndTrangThai();

  const form = useForm<CreateStudentCommand | UpdateStudentCommand>({
    initialValues: {
      maHocSinh: initialValues?.maHocSinh || '',
      hoVaTen: initialValues?.hoVaTen || '',
      ngaySinh: initialValues?.ngaySinh ? dayjs(initialValues.ngaySinh).format('YYYY-MM-DD') : '',
      gioiTinh: initialValues?.gioiTinh || '',
      diaChi: initialValues?.diaChi || '',
      sdtPhuHuynh: initialValues?.sdtPhuHuynh || '',
      emailPhuHuynh: initialValues?.emailPhuHuynh || '',
      lopId: initialValues?.lopId || '',
      ngayNhapHoc: initialValues?.ngayNhapHoc ? dayjs(initialValues.ngayNhapHoc).format('YYYY-MM-DD') : '',
      trangThaiId: initialValues?.trangThaiId || '',
      ...(isEditMode && { id: initialValues?.id || '' }), // Only add ID for update
    },
    validate: {
      maHocSinh: (value) => {
        if (!isEditMode) return isNotEmpty('Mã Học sinh không được để trống.')(value);
        if (value && !hasLength({ min: 1, max: 20 }, 'Mã Học sinh không được vượt quá 20 ký tự.')(value)) return 'Mã Học sinh không được vượt quá 20 ký tự.';
        return null;
      },
      hoVaTen: (value) => {
        if (!isEditMode) return isNotEmpty('Họ và Tên không được để trống.')(value);
        if (value && (!hasLength({ min: 3, max: 100 }, 'Họ và Tên phải có ít nhất 3 ký tự và không vượt quá 100 ký tự.')(value))) return 'Họ và Tên phải có ít nhất 3 ký tự và không vượt quá 100 ký tự.';
        return null;
      },
      ngaySinh: (value) => {
        if (!isEditMode) return isNotEmpty('Ngày Sinh không được để trống.')(value);
        if (value && dayjs(value).isSameOrAfter(dayjs(), 'day')) return 'Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.';
        return null;
      },
      gioiTinh: (value) => {
        if (!isEditMode) return isNotEmpty('Giới Tính không được để trống.')(value);
        if (value && !GENDERS.includes(value)) return `Giới Tính phải là một trong các giá trị: ${GENDERS.join(', ')}.`;
        return null;
      },
      lopId: (value) => {
        if (!isEditMode) return isNotEmpty('Lớp Học không được để trống.')(value);
        return null;
      },
      ngayNhapHoc: (value) => {
        if (!isEditMode) return isNotEmpty('Ngày Nhập Học không được để trống.')(value);
        if (value && dayjs(value).isAfter(dayjs(), 'day')) return 'Ngày Nhập Học không thể lớn hơn ngày hiện tại.';
        return null;
      },
      trangThaiId: (value) => {
        if (!isEditMode) return isNotEmpty('Trạng Thái không được để trống.')(value);
        return null;
      },
      sdtPhuHuynh: (value) => {
        if (value && !matches(PHONE_REGEX, 'Số Điện Thoại Phụ Huynh không đúng định dạng.')(value)) return 'Số Điện Thoại Phụ Huynh không đúng định dạng.';
        return null;
      },
      emailPhuHuynh: (value) => {
        if (value && !isEmail('Email Phụ Huynh không đúng định dạng.')(value)) return 'Email Phụ Huynh không đúng định dạng.';
        return null;
      },
    },
    validateInputOnBlur: true,
    validateInputOnChange: true,
  });

  useEffect(() => {
    if (initialValues) {
      form.setValues({
        maHocSinh: initialValues.maHocSinh || '',
        hoVaTen: initialValues.hoVaTen || '',
        ngaySinh: initialValues.ngaySinh ? dayjs(initialValues.ngaySinh).format('YYYY-MM-DD') : '',
        gioiTinh: initialValues.gioiTinh || '',
        diaChi: initialValues.diaChi || '',
        sdtPhuHuynh: initialValues.sdtPhuHuynh || '',
        emailPhuHuynh: initialValues.emailPhuHuynh || '',
        lopId: initialValues.lopId || '',
        ngayNhapHoc: initialValues.ngayNhapHoc ? dayjs(initialValues.ngayNhapHoc).format('YYYY-MM-DD') : '',
        trangThaiId: initialValues.trangThaiId || '',
        ...(isEditMode && { id: initialValues.id || '' }),
      });
    }
  }, [initialValues, isEditMode]);


  const handleSubmit = form.onSubmit(async (values) => {
    await onSubmit(values);
  }, (validationErrors) => {
    notifications.show({
      title: 'Lỗi xác thực',
      message: 'Vui lòng kiểm tra lại các trường thông tin.',
      color: 'red',
    });
    console.error('Validation errors:', validationErrors);
  });

  return (
    <Box maw={500} mx="auto">
      <form onSubmit={handleSubmit}>
        <TextInput
          label="Mã Học sinh"
          placeholder="VD: HS2024001"
          required={!isEditMode}
          readOnly={isEditMode} // Usually mã học sinh is immutable after creation
          {...form.getInputProps('maHocSinh')}
          mb="sm"
        />
        <TextInput
          label="Họ và Tên"
          placeholder="VD: Nguyễn Văn A"
          required={!isEditMode}
          {...form.getInputProps('hoVaTen')}
          mb="sm"
        />
        <DateInput
          label="Ngày Sinh"
          placeholder="Chọn ngày sinh"
          value={form.values.ngaySinh ? dayjs(form.values.ngaySinh).toDate() : null}
          onChange={(date) => form.setFieldValue('ngaySinh', date ? dayjs(date).format('YYYY-MM-DD') : '')}
          required={!isEditMode}
          maxDate={new Date()} // QUAN-20260530-2301-BR03
          valueFormat="DD/MM/YYYY"
          error={form.errors.ngaySinh}
          mb="sm"
        />
        <NativeSelect
          label="Giới Tính"
          data={['', ...GENDERS]}
          value={form.values.gioiTinh}
          onChange={(event) => form.setFieldValue('gioiTinh', event.currentTarget.value)}
          required={!isEditMode}
          error={form.errors.gioiTinh}
          mb="sm"
        />
        <Textarea
          label="Địa chỉ"
          placeholder="VD: 123 Đường ABC, Quận 1, TP.HCM"
          {...form.getInputProps('diaChi')}
          mb="sm"
        />
        <TextInput
          label="Số Điện Thoại Phụ Huynh"
          placeholder="VD: 0901234567"
          {...form.getInputProps('sdtPhuHuynh')}
          mb="sm"
        />
        <TextInput
          label="Email Phụ Huynh"
          placeholder="VD: phuhuynh.a@example.com"
          {...form.getInputProps('emailPhuHuynh')}
          mb="sm"
        />
        <Select
          label="Lớp Học"
          placeholder="Chọn lớp học"
          data={lops.map((lop) => ({ value: lop.id, label: lop.tenLop }))}
          value={form.values.lopId}
          onChange={(value) => form.setFieldValue('lopId', value || '')}
          required={!isEditMode}
          searchable
          clearable
          disabled={isLoadingLops}
          loading={isLoadingLops}
          error={form.errors.lopId}
          mb="sm"
        />
        <DateInput
          label="Ngày Nhập Học"
          placeholder="Chọn ngày nhập học"
          value={form.values.ngayNhapHoc ? dayjs(form.values.ngayNhapHoc).toDate() : null}
          onChange={(date) => form.setFieldValue('ngayNhapHoc', date ? dayjs(date).format('YYYY-MM-DD') : '')}
          required={!isEditMode}
          maxDate={new Date()}
          valueFormat="DD/MM/YYYY"
          error={form.errors.ngayNhapHoc}
          mb="sm"
        />
        <Select
          label="Trạng Thái Học Sinh"
          placeholder="Chọn trạng thái"
          data={trangThais.map((tt) => ({ value: tt.id, label: tt.tenTrangThai }))}
          value={form.values.trangThaiId}
          onChange={(value) => form.setFieldValue('trangThaiId', value || '')}
          required={!isEditMode}
          searchable
          clearable
          disabled={isLoadingTrangThais}
          loading={isLoadingTrangThais}
          error={form.errors.trangThaiId}
          mb="lg"
        />
        <Group position="right" mt="md">
          <Button type="submit" loading={isSubmitting}>
            {isEditMode ? 'Cập nhật' : 'Thêm mới'}
          </Button>
        </Group>
      </form>
    </Box>
  );
}