// QUAN-20260530-0942
import apiClient from './apiClient';
import { LopHoc } from '../types/lopHoc'; // Assuming a LopHoc type exists

// This client interfaces with the external LopHoc module.
// The actual base URL would be configured for the LopHoc microservice.
const LOP_HOC_API_PREFIX = '/lop-hoc'; // Assuming LopHoc module has /api/lop-hoc prefix

export const getLopHocList = async (): Promise<LopHoc[]> => {
  try {
    // In a real scenario, this would fetch the actual list of classes.
    // For now, a mock list is used.
    const mockLopHoc: LopHoc[] = [
      { maLopHoc: '10A1', tenLop: 'Lớp 10A1', khoiHoc: '10' },
      { maLopHoc: '11B2', tenLop: 'Lớp 11B2', khoiHoc: '11' },
      { maLopHoc: '12C3', tenLop: 'Lớp 12C3', khoiHoc: '12' },
      { maLopHoc: '9D4', tenLop: 'Lớp 9D4', khoiHoc: '9' },
    ];
    // Simulating API delay
    await new Promise(resolve => setTimeout(resolve, 500));
    return mockLopHoc;

    // Example if calling an actual backend:
    // const response = await apiClient.get<LopHoc[]>(`${LOP_HOC_API_PREFIX}`);
    // return response.data;
  } catch (error) {
    console.error('Error fetching LopHoc list:', error);
    throw error;
  }
};

export const checkLopHocExists = async (maLopHoc: string): Promise<boolean> => {
    try {
        const mockLopHocCodes = ['10A1', '11B2', '12C3', '9D4'];
        await new Promise(resolve => setTimeout(resolve, 200));
        return mockLopHocCodes.includes(maLopHoc);

        // Example if calling an actual backend:
        // const response = await apiClient.get<boolean>(`${LOP_HOC_API_PREFIX}/exists/${maLopHoc}`);
        // return response.data;
    } catch (error) {
        console.error(`Error checking LopHoc existence for ${maLopHoc}:`, error);
        // Depending on the exact API implementation, a 404 from the external service
        // might correctly mean "not found", or any error could imply it doesn't exist/is unreachable.
        // For validation, we typically want to return false on error.
        return false;
    }
};

export const getLopHocByMa = async (maLopHoc: string): Promise<LopHoc | null> => {
    try {
        const mockLopHoc: LopHoc[] = [
            { maLopHoc: '10A1', tenLop: 'Lớp 10A1', khoiHoc: '10' },
            { maLopHoc: '11B2', tenLop: 'Lớp 11B2', khoiHoc: '11' },
            { maLopHoc: '12C3', tenLop: 'Lớp 12C3', khoiHoc: '12' },
            { maLopHoc: '9D4', tenLop: 'Lớp 9D4', khoiHoc: '9' },
        ];
        await new Promise(resolve => setTimeout(resolve, 200));
        return mockLopHoc.find(lop => lop.maLopHoc === maLopHoc) || null;
    } catch (error) {
        console.error(`Error fetching LopHoc by Ma for ${maLopHoc}:`, error);
        return null;
    }
}