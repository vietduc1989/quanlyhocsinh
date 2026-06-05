// QUAN-20260604-153038
import axios from 'axios';
import { notifications } from '@mantine/notifications';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api';

const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to add JWT token
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwt_token'); // Get token from local storage
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor to handle common errors and API response structure
axiosInstance.interceptors.response.use(
  (response) => {
    if (response.data && response.data.success) {
      // Optional: show success notification for non-GET requests if desired
      // if (response.config.method !== 'get') {
      //   notifications.show({
      //     title: 'Thành công',
      //     message: response.data.message || 'Thao tác thành công',
      //     color: 'green',
      //   });
      // }
    }
    return response;
  },
  (error) => {
    const originalRequest = error.config;
    if (error.response) {
      const { status, data } = error.response;
      let errorMessage = data?.message || 'Có lỗi xảy ra.';
      if (data && data.errors && data.errors.length > 0) {
        errorMessage = data.errors.join(' | ');
      }

      switch (status) {
        case 400: // Bad Request (e.g., validation errors)
          notifications.show({
            title: 'Lỗi xác thực',
            message: errorMessage,
            color: 'red',
          });
          break;
        case 401: // Unauthorized
          notifications.show({
            title: 'Chưa xác thực',
            message: 'Phiên đăng nhập đã hết hạn hoặc bạn chưa đăng nhập. Vui lòng đăng nhập lại.',
            color: 'red',
          });
          // Redirect to login page
          if (!originalRequest._retry) {
            originalRequest._retry = true;
            // Optionally, refresh token here or redirect to login
            // For now, just redirect to login
            localStorage.removeItem('jwt_token');
            window.location.href = '/login';
          }
          break;
        case 403: // Forbidden
          notifications.show({
            title: 'Không có quyền',
            message: 'Bạn không có quyền thực hiện thao tác này.',
            color: 'red',
          });
          break;
        case 404: // Not Found
          notifications.show({
            title: 'Không tìm thấy',
            message: errorMessage,
            color: 'orange',
          });
          break;
        case 500: // Internal Server Error
          notifications.show({
            title: 'Lỗi máy chủ',
            message: errorMessage,
            color: 'red',
          });
          break;
        default:
          notifications.show({
            title: `Lỗi ${status}`,
            message: errorMessage,
            color: 'red',
          });
          break;
      }
    } else if (error.request) {
      // The request was made but no response was received
      notifications.show({
        title: 'Lỗi mạng',
        message: 'Không thể kết nối đến máy chủ. Vui lòng kiểm tra kết nối internet hoặc thử lại sau.',
        color: 'red',
      });
    } else {
      // Something happened in setting up the request that triggered an Error
      notifications.show({
        title: 'Lỗi không xác định',
        message: error.message || 'Một lỗi không mong muốn đã xảy ra.',
        color: 'red',
      });
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;