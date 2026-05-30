// QUAN-20260530-0942
import apiClient from './apiClient';
import { AuthCredentials } from '../types/auth';

// This is a placeholder for ONENET's centralized authentication service.
// In a real ONENET setup, this might communicate with an OAuth2 provider
// or a specific authentication microservice.
const AUTH_API_PREFIX = '/auth'; // Example prefix if auth endpoints are separate

export const login = async (credentials: AuthCredentials): Promise<{ token: string, roles: string[] }> => {
  try {
    // This is a simplified example. Replace with actual ONENET authentication logic.
    // For now, it will return a mock token and roles based on username.
    // In a real system, you'd send credentials to ONENET's AuthN service.

    if (credentials.username === 'admin' && credentials.password === 'adminpass') {
      return { token: 'mock_admin_jwt_token', roles: ['ADMIN', 'TEACHER'] };
    } else if (credentials.username === 'teacher' && credentials.password === 'teacherpass') {
      return { token: 'mock_teacher_jwt_token', roles: ['TEACHER'] };
    } else {
      throw new Error('Tên đăng nhập hoặc mật khẩu không đúng.');
    }

    // Example if calling an actual backend:
    // const response = await apiClient.post(`${AUTH_API_PREFIX}/login`, credentials);
    // return response.data; // Should contain token and roles
  } catch (error: any) {
    console.error('Login failed:', error);
    throw new Error(error.response?.data?.message || error.message || 'Đăng nhập thất bại.');
  }
};

export const validateToken = async (token: string): Promise<{ isValid: boolean, roles: string[] }> => {
  try {
    // In a real ONENET scenario, this would involve calling an endpoint
    // to validate the JWT token and retrieve user roles.
    // For this mock, we'll decode the mock token to infer roles.
    if (token === 'mock_admin_jwt_token') {
      return { isValid: true, roles: ['ADMIN', 'TEACHER'] };
    } else if (token === 'mock_teacher_jwt_token') {
      return { isValid: true, roles: ['TEACHER'] };
    }
    return { isValid: false, roles: [] };

    // Example if calling an actual backend:
    // const response = await apiClient.get(`${AUTH_API_PREFIX}/validate-token`, {
    //   headers: { Authorization: `Bearer ${token}` }
    // });
    // return response.data; // Should contain isValid and roles
  } catch (error) {
    console.error('Token validation failed:', error);
    return { isValid: false, roles: [] };
  }
};