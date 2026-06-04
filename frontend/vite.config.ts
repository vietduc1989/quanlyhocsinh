import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 5173,
    // If your backend is on a different port/domain, configure proxy here
    proxy: {
      '/api': {
        target: 'http://localhost:5001', // Your .NET backend HTTP URL
        changeOrigin: true,
        secure: false, // Set to true for HTTPS
        rewrite: (path) => path.replace(/^\/api/, '/api'), // Adjust if your backend has a different base path
      },
    },
  },
});