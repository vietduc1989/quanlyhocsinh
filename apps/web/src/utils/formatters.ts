// QUAN-20260604-153038
import dayjs from 'dayjs';

export const DateOnlyFormatter = (dateString: string | Date): string => {
  if (!dateString) return '';
  return dayjs(dateString).format('DD/MM/YYYY');
};

export const DateTimeFormatter = (dateString: string | Date): string => {
  if (!dateString) return '';
  return dayjs(dateString).format('DD/MM/YYYY HH:mm:ss');
};