import { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Header from './Header';
import Sidebar from './Sidebar';

const AppLayout = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  return (
    <div className="flex h-screen bg-gray-100">
      {/* Sidebar */}
      <Sidebar sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />

      {/* Content Area */}
      <div className="flex flex-1 flex-col overflow-hidden">
        {/* Header */}
        <Header setSidebarOpen={setSidebarOpen} />

        {/* Main Content */}
        <main className="flex-1 overflow-y-auto focus:outline-none">
          <div className="py-6">
            <Outlet /> {/* Renders the child route component */}
          </div>
        </main>
      </div>
    </div>
  );
};

export default AppLayout;