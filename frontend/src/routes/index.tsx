import { Routes, Route } from 'react-router-dom';
import AppLayout from '@/components/AppShell/Layout';
import DashboardPage from '@/pages/dashboard/DashboardPage';
import NotFoundPage from '@/pages/NotFoundPage';
import LoginPage from '@/pages/auth/LoginPage'; // Example Auth page

const AppRoutes = () => {
  return (
    <Routes>
      {/* Public Routes */}
      <Route path="/login" element={<LoginPage />} />

      {/* Authenticated Routes with AppShell Layout */}
      <Route path="/" element={<AppLayout />}>
        <Route index element={<DashboardPage />} />
        {/* Add more authenticated routes here */}
        {/* <Route path="settings" element={<SettingsPage />} /> */}
      </Route>

      {/* Catch all - 404 Not Found */}
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
  );
};

export default AppRoutes;