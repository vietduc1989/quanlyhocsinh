import { ExampleItemDto } from '@/types/example';

const BASE_URL = import.meta.env.VITE_API_BASE_URL || '/api';

export const getExampleItems = async (): Promise<ExampleItemDto[]> => {
  const response = await fetch(`${BASE_URL}/ExampleItems`);
  if (!response.ok) {
    throw new Error('Failed to fetch example items');
  }
  return response.json();
};

export const createExampleItem = async (data: { name: string; description: string }): Promise<string> => {
  const response = await fetch(`${BASE_URL}/ExampleItems`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  });
  if (!response.ok) {
    throw new Error('Failed to create example item');
  }
  return response.json(); // Assuming it returns the ID
};