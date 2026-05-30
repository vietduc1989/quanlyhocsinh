// QUAN-20260530-0942
import React, { createContext, useContext, useState, useEffect, useCallback } from 'react';
import { login as apiLogin, validateToken } from '../api/authClient';
import { AuthContextType } from '../types/auth';

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const AUTH_TOKEN_KEY = 'onenet_auth_token';
const USER_ROLES_KEY = 'onenet_user_roles';

export const getAuthToken = (): string | null => localStorage.getItem(AUTH_TOKEN_KEY);
export const setAuthToken = (token: string) => localStorage.setItem(AUTH_TOKEN_KEY, token);
export const removeAuthToken = () => localStorage.removeItem(AUTH_TOKEN_KEY);

export const getUserRoles = (): string[] => {
  const roles = localStorage.getItem(USER_ROLES_KEY);
  return roles ? JSON.parse(roles) : [];
};
export const setUserRoles = (roles: string[]) => localStorage.setItem(USER_ROLES_KEY, JSON.stringify(roles));
export const removeUserRoles = () => localStorage.removeItem(USER_ROLES_KEY);


export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [userRoles, setUserRolesState] = useState<string[]>([]);
  const [loading, setLoading] = useState<boolean>(true); // To indicate initial auth check is in progress

  const checkAuthStatus = useCallback(async () => {
    setLoading(true);
    const token = getAuthToken();
    if (token) {
      try {
        const { isValid, roles } = await validateToken(token);
        if (isValid) {
          setIsAuthenticated(true);
          setUserRolesState(roles);
          setUserRoles(roles); // Persist roles
        } else {
          logout(); // Invalid token, log out
        }
      } catch (error) {
        console.error('Auth token validation failed:', error);
        logout(); // Error during validation, log out
      }
    } else {
      setIsAuthenticated(false);
      setUserRolesState([]);
      removeUserRoles();
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    checkAuthStatus();
  }, [checkAuthStatus]);

  const login = async (username: string, password: string) => {
    const { token, roles } = await apiLogin({ username, password });
    setAuthToken(token);
    setUserRoles(roles);
    setIsAuthenticated(true);
    setUserRolesState(roles);
  };

  const logout = () => {
    removeAuthToken();
    removeUserRoles();
    setIsAuthenticated(false);
    setUserRolesState([]);
    window.location.href = '/login'; // Redirect to login page
  };

  const hasRole = (role: string) => userRoles.includes(role);

  const contextValue: AuthContextType = {
    isAuthenticated,
    userRoles,
    login,
    logout,
    hasRole,
    loading,
  };

  return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};