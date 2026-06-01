import { useEffect } from 'react';
import { Modal, TextInput, Select, Button, Group, Box, Text } from '@mantine/core';
import { DateInput } from '@mantine/dates';
import { useForm, zodResolver } from '@mantine/form';
import { z } from 'zod';
import dayjs from 'dayjs';
import type { CreateStudentPayload, UpdateStudentPayload, StudentDetail, LookupItem } from '../../types/common';

// Define schema for validation
const studentSchema = z.object({
  maHocSinh: z.string().min(1, 'Mã Học sinh không được để trống.').max(20, 'Mã Học sinh không được vượt quá 20 ký tự.'),
  hoVaTen: z.string().min(3, 'Họ và Tên phải có ít nhất 3 ký tự.').max(100, 'Họ và Tên không được vượt quá 100 ký tự.'),
  ngaySinh: z.date().refine((date) => date < new Date(dayjs().format('YYYY-MM-DD')), 'Ngày Sinh không thể lớn hơn hoặc bằng ngày hiện tại.'),
  gioiTinh: z.enum(['Nam', 'Nu', 'Khac'], { message: 'Giới Tính không hợp lệ.' }),
  diaChi: z.string().max(255, 'Địa chỉ không được vượt quá 255 ký tự.').optional().or(z.literal('')),
  sdtPhuHuynh: z.string().regex(/^\+?[0-9]{7,15}$/, 'Số Điện Thoại Phụ Huynh không đúng định dạng.').optional().or(z.literal('')),
  emailPhuHuynh: z.string().email('Email Phụ Huynh không đúng định dạng.').optional().or(z.literal('')),
  lopId: z.string().uuid('Lớp Học không hợp lệ.').min(1, 'Lớp Học không được để trống.'),
  ngayNhapHoc: z.date().refine((date) => date <= new Date(dayjs().format('YYYY-MM-DD')), 'Ngày Nhập Học không thể lớn hơn ngày hiện tại.'),
  trangThaiId: z.string().uuid('Trạng Thái không hợp lệ.').min(1, 'Trạng Thái không được để trống.'),
});

interface StudentFormModalProps {
  opened: boolean;
  onClose: () => void;
  onSubmit: (values: CreateStudentPayload | UpdateStudentPayload) => Promise<void>;
  initialData?: StudentDetail | null;
  isSubmitting: boolean;
  lops: LookupItem[];
  trangThais: LookupItem[];
}

export function StudentFormModal({
  opened,
  onClose,
  onSubmit,
  initialData,
  isSubmitting,
  lops,
  trangThais,
}: StudentFormModalProps) {
  const form = useForm({
    initialValues: {
      maHocSinh: '',
      hoVaTen: '',
      ngaySinh: null as Date | null,
      gioiTinh: 'Nam' as 'Nam' | 'Nu' | 'Khac',
      diaChi: '',
      sdtPhuHuynh: '',
      emailPhuHuynh: '',
      lopId: '',
      ngayNhapHoc: null as Date | null,
      trangThaiId: '',
    },
    validate: zodResolver(studentSchema),
  });

  useEffect(() => {
    if (initialData) {
      form.setValues({
        maHocSinh: initialData.maHocSinh,
        hoVaTen: initialData.hoVaTen,
        ngaySinh: dayjs(initialData.ngaySinh).toDate(),
        // Convert string back to enum value
        gioiTinh: initialData.gioiTinh as 'Nam' | 'Nu' | 'Khac',
        diaChi: initialData.diaChi || '',
        sdtPhuHuynh: initialData.sdtPhuHuynh || '',
        emailPhuHuynh: initialData.emailPhuHuynh || '',
        lopId: initialData.lopId,
        ngayNhapHoc: dayjs(initialData.ngayNhapHoc).toDate(),
        trangThaiId: initialData.trangThaiId,
      });
    } else {
      form.reset();
    }
  }, [initialData, opened]);

  const handleSubmit = async (values: typeof form.values) => {
    // Convert Date objects to ISO string for API
    const payload = {
      ...values,
      ngaySinh: values.ngaySinh?.toISOString(),
      ngayNhapHoc: values.ngayNhapHoc?.toISOString(),
      // Ensure empty strings for optional fields that are null in API
      diaChi: values.diaChi === '' ? null : values.diaChi,
      sdtPhuHuynh: values.sdtPhuHuynh === '' ? null : values.sdtPhuHuynh,
      emailPhuHuynh: values.emailPhuHuynh === '' ? null : values.emailPhuHuynh,
    };
    await onSubmit(payload as CreateStudentPayload | UpdateStudentPayload);
    if (!isSubmitting) { // Only reset if submission was successful
      onClose();
    }
  };

  return (
    <Modal opened={opened} onClose={onClose} title={initialData ? 'Cập nhật Học sinh' : 'Thêm mới Học sinh'} centered>
      <Box component="form" onSubmit={form.onSubmit(handleSubmit)}>
        <TextInput
          label="Mã Học sinh"
          placeholder="VD: HS2024001"
          {...form.getInputProps('maHocSinh')}
          required
          readOnly={!!initialData} // Mã Học sinh is generally not editable after creation
        />
        <TextInput
          mt="md"
          label="Họ và Tên"
          placeholder="VD: Nguyễn Văn A"
          {...form.getInputProps('hoVaTen')}
          required
        />
        <DateInput
          mt="md"
          label="Ngày Sinh"
          placeholder="Chọn ngày sinh"
          valueFormat="DD/MM/YYYY"
          {...form.getInputProps('ngaySinh')}
          required
          maxDate={dayjs().subtract(1, 'day').toDate()} // Must be < current date
        />
        <Select
          mt="md"
          label="Giới Tính"
          placeholder="Chọn giới tính"
          data={['Nam', 'Nu', 'Khac']}
          {...form.getInputProps('gioiTinh')}
          required
        />
        <TextInput
          mt="md"
          label="Địa Chỉ"
          placeholder="VD: 123 Đường ABC"
          {...form.getInputProps('diaChi')}
        />
        <TextInput
          mt="md"
          label="SĐT Phụ Huynh"
          placeholder="VD: 0901234567"
          {...form.getInputProps('sdtPhuHuynh')}
        />
        <TextInput
          mt="md"
          label="Email Phụ Huynh"
          placeholder="VD: phuhuynh@example.com"
          {...form.getInputProps('emailPhuHuynh')}
        />
        <Select
          mt="md"
          label="Lớp Học"
          placeholder="Chọn lớp học"
          data={lops.map((lop) => ({ value: lop.id, label: lop.name }))}
          {...form.getInputProps('lopId')}
          required
        />
        <DateInput
          mt="md"
          label="Ngày Nhập Học"
          placeholder="Chọn ngày nhập học"
          valueFormat="DD/MM/YYYY"
          {...form.getInputProps('ngayNhapHoc')}
          required
          maxDate={new Date()} // Must be <= current date
        />
        <Select
          mt="md"
          label="Trạng Thái"
          placeholder="Chọn trạng thái"
          data={trangThais.map((tt) => ({ value: tt.id, label: tt.name }))}
          {...form.getInputProps('trangThaiId')}
          required
        />

        {form.errors.root && (
          <Text color="red" size="sm" mt="md">
            {form.errors.root}
          </Text>
        )}

        <Group justify="flex-end" mt="xl">
          <Button variant="default" onClick={onClose}>Hủy</Button>
          <Button type="submit" loading={isSubmitting}>
            {initialData ? 'Cập nhật' : 'Thêm mới'}
          </Button>
        </Group>
      </Box>
    </Modal>
  );
}
