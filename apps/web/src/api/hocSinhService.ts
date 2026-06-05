// QUAN-20260604-153038
import axiosInstance from './axiosInstance';
import { HocSinh, HocSinhSearchParams, PaginatedList, HocSinhFormValues, LopHocLookup } from '../types/hocSinh';
import { ApiResponse } from '../types/api';

const BASE_URL = '/hocsinh';

export const hocSinhService = {
  // FR01, FR07, FR08
  getHocSinhs: async (params?: HocSinhSearchParams): Promise<ApiResponse<PaginatedList<HocSinh>>> => {
    const response = await axiosInstance.get<ApiResponse<PaginatedList<HocSinh>>>(BASE_URL, { params });
    return response.data;
  },

  // FR02
  getHocSinhById: async (id: string): Promise<ApiResponse<HocSinh>> => {
    const response = await axiosInstance.get<ApiResponse<HocSinh>>(`${BASE_URL}/${id}`);
    return response.data;
  },

  // FR03
  createHocSinh: async (data: HocSinhFormValues): Promise<ApiResponse<HocSinh>> => {
    const response = await axiosInstance.post<ApiResponse<HocSinh>>(BASE_URL, data);
    return response.data;
  },

  // FR04
  updateHocSinh: async (id: string, data: HocSinhFormValues): Promise<ApiResponse<HocSinh>> => {
    const response = await axiosInstance.put<ApiResponse<HocSinh>>(`${BASE_URL}/${id}`, { id, ...data });
    return response.data;
  },

  // FR05
  deleteHocSinh: async (id: string): Promise<ApiResponse<null>> => {
    const response = await axiosInstance.delete<ApiResponse<null>>(`${BASE_URL}/${id}`);
    return response.data;
  },

  // FR07: Get lop hocs for filter/dropdown
  getLopHocsForLookup: async (): Promise<ApiResponse<LopHocLookup[]>> => {
    const response = await axiosInstance.get<ApiResponse<LopHocLookup[]>>(`${BASE_URL}/lop-hocs-lookup`);
    return response.data;
  },
};