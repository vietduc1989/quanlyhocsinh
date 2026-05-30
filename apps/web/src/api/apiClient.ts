// QUAN-20260530-0942
import axios, { AxiosInstance, AxiosError } from 'axios';
import { notification } from 'antd';
import { getAuthToken, removeAuthToken } from '../utils/auth';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'http://localhost:8080/api', // Backend API base URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to add the auth token
apiClient.interceptors.request.use(
  (config) => {
    const token = getAuthToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor to handle errors globally
apiClient.interceptors.response.use(
  (response) => {
    return response;
  },
  (error: AxiosError) => {
    if (error.response) {
      const { status, data } = error.response;
      let errorMessage = 'Đã xảy ra lỗi. Vui lòng thử lại.';

      if (typeof data === 'object' && data !== null && 'message' in data && typeof data.message === 'string') {
        errorMessage = (data as any).message;
      } else if (typeof data === 'string') {
        errorMessage = data;
      }

      if (status === 401 || status === 403) {
        notification.error({
          message: 'Lỗi xác thực',
          description: 'Phiên đăng nhập đã hết hạn hoặc bạn không có quyền truy cập. Vui lòng đăng nhập lại.',
        });
        removeAuthToken(); // Clear token on unauthorized/forbidden
        window.location.href = '/login'; // Redirect to login page
      } else if (status === 404) {
        notification.warning({
          message: 'Không tìm thấy tài nguyên',
          description: errorMessage,
        });
      } else if (status === 400 || status === 409) {
        // Bad request or conflict, show detailed error
        if (typeof data === 'object' && data !== null && 'validationErrors' in data) {
          const validationErrors = (data as any).validationErrors;
          const errorList = Object.keys(validationErrors).map(field => `${field}: ${validationErrors[field]}`).join('\n');
          notification.error({
            message: 'Lỗi dữ liệu nhập vào',
            description: `${errorMessage}\n${errorList}`,
            duration: 5
          });
        } else {
          notification.error({
            message: `Lỗi ${status}`,
            description: errorMessage,
          });
        }
      } else {
        notification.error({
          message: `Lỗi ${status}`,
          description: errorMessage,
        });
      }
    } else if (error.request) {
      notification.error({
        message: 'Lỗi kết nối',
        description: 'Không thể kết nối đến máy chủ. Vui lòng kiểm tra kết nối mạng của bạn.',
      });
    } else {
      notification.error({
        message: 'Lỗi không xác định',
        description: error.message,
      });
    }
    return Promise.reject(error);
  }
);

export default apiClient;