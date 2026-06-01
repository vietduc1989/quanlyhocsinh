// QUAN-20260530-2301
import { useState, useEffect } from 'react';
import { notifications } from '@mantine/notifications';
import { getLops, getTrangThaiHocSinhs } from '../api/studentApi';
import type { LopDto, TrangThaiHocSinhDto } from '../types/student';

export function useLopsAndTrangThai() {
  const [lops, setLops] = useState<LopDto[]>([]);
  const [trangThais, setTrangThais] = useState<TrangThaiHocSinhDto[]>([]);
  const [isLoadingLops, setIsLoadingLops] = useState(false);
  const [isLoadingTrangThais, setIsLoadingTrangThais] = useState(false);
  const [errorLops, setErrorLops] = useState<string | null>(null);
  const [errorTrangThais, setErrorTrangThais] = useState<string | null>(null);

  useEffect(() => {
    const fetchLops = async () => {
      setIsLoadingLops(true);
      try {
        const response = await getLops();
        if (response.success && response.data) {
          setLops(response.data);
        } else {
          notifications.show({
            title: 'Lỗi tải danh sách lớp',
            message: response.message || 'Có lỗi xảy ra khi tải dữ liệu lớp.',
            color: 'red',
          });
          setErrorLops(response.message || 'Failed to fetch lops.');
        }
      } catch (err: any) {
        notifications.show({
          title: 'Lỗi API',
          message: err.response?.data?.message || err.message || 'Không thể kết nối tới máy chủ.',
          color: 'red',
        });
        setErrorLops(err.response?.data?.message || err.message || 'An unexpected error occurred.');
      } finally {
        setIsLoadingLops(false);
      }
    };

    const fetchTrangThais = async () => {
      setIsLoadingTrangThais(true);
      try {
        const response = await getTrangThaiHocSinhs();
        if (response.success && response.data) {
          setTrangThais(response.data);
        } else {
          notifications.show({
            title: 'Lỗi tải danh sách trạng thái',
            message: response.message || 'Có lỗi xảy ra khi tải dữ liệu trạng thái.',
            color: 'red',
          });
          setErrorTrangThais(response.message || 'Failed to fetch trang thais.');
        }
      } catch (err: any) {
        notifications.show({
          title: 'Lỗi API',
          message: err.response?.data?.message || err.message || 'Không thể kết nối tới máy chủ.',
          color: 'red',
        });
        setErrorTrangThais(err.response?.data?.message || err.message || 'An unexpected error occurred.');
      } finally {
        setIsLoadingTrangThais(false);
      }
    };

    fetchLops();
    fetchTrangThais();
  }, []);

  return { lops, trangThais, isLoadingLops, isLoadingTrangThais, errorLops, errorTrangThais };
}