// QUAN-20260604-153038
import { AppShell, Burger, Group, Skeleton, Text, NavLink } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { Route, Routes, Link } from 'react-router-dom';
import HocSinhListPage from './pages/hocsinh/HocSinhListPage';
import HocSinhDetailPage from './pages/hocsinh/HocSinhDetailPage';
import HocSinhFormPage from './pages/hocsinh/HocSinhFormPage';
import {
  IconGauge,
  IconFingerprint,
  IconChevronRight,
  IconUsersGroup,
  IconSchool,
} from '@tabler/icons-react';

function App() {
  const [opened, { toggle }] = useDisclosure();

  return (
    <AppShell
      header={{ height: 60 }}
      navbar={{ width: 300, breakpoint: 'sm', collapsed: { mobile: !opened } }}
      padding="md"
    >
      <AppShell.Header>
        <Group h="100%" px="md">
          <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
          <Text size="xl" fw={700}>ONENET Student Management</Text>
        </Group>
      </AppShell.Header>

      <AppShell.Navbar p="md">
        <NavLink
          component={Link}
          to="/"
          label="Dashboard"
          leftSection={<IconGauge size="1rem" stroke={1.5} />}
          active
        />
        <NavLink
          label="Quản lý Học sinh"
          leftSection={<IconUsersGroup size="1rem" stroke={1.5} />}
          childrenOffset={28}
          defaultOpened
        >
          <NavLink
            component={Link}
            to="/hoc-sinh"
            label="Danh sách Học sinh"
            leftSection={<IconSchool size="1rem" stroke={1.5} />}
          />
          <NavLink
            component={Link}
            to="/hoc-sinh/add"
            label="Thêm mới Học sinh"
            leftSection={<IconChevronRight size="1rem" stroke={1.5} />}
          />
        </NavLink>
        <NavLink
          label="Authentication"
          leftSection={<IconFingerprint size="1rem" stroke={1.5} />}
          childrenOffset={28}
        >
          <NavLink label="Login" />
          <NavLink label="Register" />
          <NavLink label="Forgot password" />
        </NavLink>
        {/* Skeleton for other menu items as per guideline */}
        {Array(5).fill(0).map((_, index) => (
          <Skeleton key={index} h={28} mt="sm" animate={false} />
        ))}
      </AppShell.Navbar>

      <AppShell.Main>
        <Routes>
          <Route path="/" element={<Text>Welcome to ONENET Student Management Dashboard!</Text>} />
          <Route path="/hoc-sinh" element={<HocSinhListPage />} />
          <Route path="/hoc-sinh/add" element={<HocSinhFormPage />} />
          <Route path="/hoc-sinh/edit/:id" element={<HocSinhFormPage />} />
          <Route path="/hoc-sinh/:id" element={<HocSinhDetailPage />} />
        </Routes>
      </AppShell.Main>
      <AppShell.Footer p="md">
        <Text size="sm" c="dimmed">© 2026 ONENET. All rights reserved.</Text>
      </AppShell.Footer>
    </AppShell>
  );
}

export default App;