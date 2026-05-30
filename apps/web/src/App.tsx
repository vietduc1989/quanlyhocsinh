// QUAN-20260530-0942
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { Layout, Menu, theme } from 'antd';
import { UserOutlined, BookOutlined } from '@ant-design/icons';
import StudentListPage from './pages/StudentListPage';
import StudentDetailPage from './pages/StudentDetailPage';
import StudentUpsertPage from './pages/StudentUpsertPage';
import LoginPage from './pages/LoginPage';
import { useAuth } from './utils/auth';

const { Header, Content, Footer, Sider } = Layout;

// A simple PrivateRoute component for demonstration
const PrivateRoute: React.FC<{ children: React.ReactNode, roles?: string[] }> = ({ children, roles }) => {
  const { isAuthenticated, userRoles } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (roles && roles.length > 0 && !roles.some(role => userRoles.includes(role))) {
    // Optionally show an access denied page or redirect to home
    return <Navigate to="/" replace />; // Redirect to home if not authorized
  }

  return <>{children}</>;
};

const App: React.FC = () => {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const { isAuthenticated, logout } = useAuth();

  return (
    <Router>
      <Layout style={{ minHeight: '100vh' }}>
        <Header style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', padding: '0 20px' }}>
          <div style={{ color: 'white', fontSize: '20px' }}>ONENET - Quản lý Học sinh</div>
          {isAuthenticated && (
            <Menu theme="dark" mode="horizontal" selectable={false} style={{ flex: 1, minWidth: 0, justifyContent: 'flex-end' }}>
              <Menu.Item key="logout" onClick={logout}>Đăng xuất</Menu.Item>
            </Menu>
          )}
        </Header>
        <Layout>
          {isAuthenticated && (
            <Sider width={200} style={{ background: colorBgContainer }}>
              <Menu
                mode="inline"
                defaultSelectedKeys={['1']}
                style={{ height: '100%', borderRight: 0 }}
                items={[
                  { key: '1', icon: <UserOutlined />, label: 'Quản lý Học sinh', onClick: () => window.location.href = '/' },
                  { key: '2', icon: <BookOutlined />, label: 'Quản lý Lớp học (Placeholder)', disabled: true },
                ]}
              />
            </Sider>
          )}
          <Layout style={{ padding: '0 24px 24px' }}>
            <Content
              style={{
                padding: 24,
                margin: 0,
                minHeight: 280,
                background: colorBgContainer,
                borderRadius: borderRadiusLG,
              }}
            >
              <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/" element={<PrivateRoute><StudentListPage /></PrivateRoute>} />
                <Route path="/students/new" element={<PrivateRoute roles={['ADMIN']}><StudentUpsertPage /></PrivateRoute>} />
                <Route path="/students/:maHocSinh" element={<PrivateRoute><StudentDetailPage /></PrivateRoute>} />
                <Route path="/students/:maHocSinh/edit" element={<PrivateRoute roles={['ADMIN']}><StudentUpsertPage /></PrivateRoute>} />
                <Route path="*" element={<Navigate to="/" replace />} /> {/* Redirect unknown routes to home */}
              </Routes>
            </Content>
          </Layout>
        </Layout>
        <Footer style={{ textAlign: 'center' }}>ONENET ©2024 Created by ONENET Team</Footer>
      </Layout>
    </Router>
  );
};

export default App;