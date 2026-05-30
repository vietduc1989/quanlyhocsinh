// QUAN-20260530-0942
export interface AuthCredentials {
    username: string;
    password: string;
}

export interface AuthContextType {
    isAuthenticated: boolean;
    userRoles: string[];
    login: (username: string, password: string) => Promise<void>;
    logout: () => void;
    hasRole: (role: string) => boolean;
    loading: boolean;
}