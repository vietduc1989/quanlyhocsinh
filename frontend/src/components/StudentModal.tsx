/*
 * FEATURE CODE: QUAN-20260529-0943
 * Project: ONENET Student Management System
 * Layer: Frontend (UI Modal Form)
 */

import React, { useEffect } from 'react';
import { Modal, TextInput, Select, Textarea, Button, Group } from '@mantine/core';
import { useForm } from '@mantine/form';
import { Student } from '../types/student';

interface StudentModalProps {
  opened: boolean;
  onClose: () => void;
  student: Student | null;
  onSave: (values: any) => Promise<void>;
}

export const StudentModal: React.FC<StudentModalProps> = ({ opened, onClose, student, onSave }) => {
  const form = useForm({
    initialValues: {
      fullName: '',
      dateOfBirth: '',
      gender: 'Nam',
      address: '',
      parentPhone: '',
      email: '',
    },
    validate: {
      fullName: (value) => {
        if (!value.trim()) return 'Họ tên là trường bắt buộc';
        if (!/^[a-zA-Z\s\p{L}]+$/u.test(value)) return 'Họ tên không được chứa ký tự số hoặc đặc biệt';
        return null;
      },
      dateOfBirth: (value) => {
        if (!value) return 'Ngày sinh là trường bắt buộc';
        const dob = new Date(value);
        if (dob >= new Date()) return 'Ngày sinh phải nhỏ hơn ngày hiện tại';
        return null;
      },
      parentPhone: (value) => {
        if (!value) return 'SĐT Phụ huynh là bắt buộc';
        if (!/^(03|05|07|08|09)\d{8}$/.test(value)) return 'SĐT bắt đầu bằng 03,05,07,08,09 gồm 10 chữ số';
        return null;
      },
      email: (value) => {
        if (value && !/^\S+@\S+\.\S+$/.test(value)) return 'Email không đúng định dạng';
        return null;
      },
    },
  });

  useEffect(() => {
    if (student) {
      form.setValues({
        fullName: student.fullName,
        dateOfBirth: student.dateOfBirth.split('T')[0],
        gender: student.gender,
        address: student.address || '',
        parentPhone: student.parentPhone,
        email: student.email || '',
      });
    } else {
      form.reset();
    }
  }, [student, opened]);

  const handleSubmit = async (values: typeof form.values) => {
    await onSave(values);
    onClose();
  };

  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title={student ? `Cập nhật thông tin học sinh: ${student.studentCode}` : 'Thêm mới học sinh'}
      size="lg"
      centered
    >
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <TextInput
          withAsterisk
          label="Họ và tên"
          placeholder="Nhập họ và tên học sinh"
          mb="sm"
          {...form.getInputProps('fullName')}
        />

        <Group grow mb="sm">
          <TextInput
            withAsterisk
            type="date"
            label="Ngày sinh"
            {...form.getInputProps('dateOfBirth')}
          />

          <Select
            withAsterisk
            label="Giới tính"
            data={['Nam', 'Nữ', 'Khác']}
            {...form.getInputProps('gender')}
          />
        </Group>

        <TextInput
          withAsterisk
          label="SĐT phụ huynh"
          placeholder="Nhập 10 chữ số"
          mb="sm"
          {...form.getInputProps('parentPhone')}
        />

        <TextInput
          label="Email phụ huynh"
          placeholder="Ví dụ: nguyenvana@gmail.com"
          mb="sm"
          {...form.getInputProps('email')}
        />

        <Textarea
          label="Địa chỉ"
          placeholder="Nhập địa chỉ của học sinh"
          mb="lg"
          {...form.getInputProps('address')}
        />

        <Group justify="flex-end">
          <Button variant="outline" color="gray" onClick={onClose}>
            Hủy bỏ
          </Button>
          <Button type="submit" color="blue">
            Lưu thông tin
          </Button>
        </Group>
      </form>
    </Modal>
  );
};