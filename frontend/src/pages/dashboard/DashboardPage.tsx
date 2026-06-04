import { HomeModernIcon, ChartPieIcon, ShieldCheckIcon } from '@heroicons/react/24/outline';

const DashboardPage = () => {
  return (
    <div className="p-6 md:p-10 space-y-8 bg-gray-50 min-h-full">
      <h1 className="text-4xl font-extrabold text-gray-900 mb-6">Welcome to Your Dashboard!</h1>
      <p className="text-lg text-gray-700">
        This is a beautifully designed, functional dashboard page. Explore the features and start building.
      </p>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {/* Feature Card 1 */}
        <div className="bg-white rounded-lg shadow-xl p-6 transition-transform transform hover:scale-105 duration-300 ease-in-out">
          <div className="flex items-center justify-center w-12 h-12 rounded-full bg-primary-100 text-primary-600 mb-4">
            <HomeModernIcon className="h-6 w-6" />
          </div>
          <h3 className="text-xl font-semibold text-gray-900 mb-2">Clean Architecture</h3>
          <p className="text-gray-600">
            Our backend follows a robust Clean Architecture pattern for scalability and maintainability.
          </p>
        </div>

        {/* Feature Card 2 */}
        <div className="bg-white rounded-lg shadow-xl p-6 transition-transform transform hover:scale-105 duration-300 ease-in-out">
          <div className="flex items-center justify-center w-12 h-12 rounded-full bg-green-100 text-green-600 mb-4">
            <ChartPieIcon className="h-6 w-6" />
          </div>
          <h3 className="text-xl font-semibold text-gray-900 mb-2">Modern Frontend Stack</h3>
          <p className="text-gray-600">
            Built with Vite, React, TypeScript, and Tailwind CSS for a premium, performant user experience.
          </p>
        </div>

        {/* Feature Card 3 */}
        <div className="bg-white rounded-lg shadow-xl p-6 transition-transform transform hover:scale-105 duration-300 ease-in-out">
          <div className="flex items-center justify-center w-12 h-12 rounded-full bg-purple-100 text-purple-600 mb-4">
            <ShieldCheckIcon className="h-6 w-6" />
          </div>
          <h3 className="text-xl font-semibold text-gray-900 mb-2">Ready to Scale</h3>
          <p className="text-gray-600">
            Designed with future growth in mind, from both code quality and deployment standpoints.
          </p>
        </div>
      </div>

      <div className="mt-10 p-8 bg-gradient-to-r from-primary-500 to-primary-700 text-white rounded-lg shadow-lg">
        <h2 className="text-3xl font-bold mb-4">Next Steps:</h2>
        <ul className="list-disc list-inside space-y-2 text-lg">
          <li>Integrate actual API calls for data fetching.</li>
          <li>Implement state management solutions (e.g., React Query, Zustand).</li>
          <li>Expand the UI components library as needed.</li>
          <li>Add more specific business logic to the backend.</li>
        </ul>
      </div>
    </div>
  );
};

export default DashboardPage;