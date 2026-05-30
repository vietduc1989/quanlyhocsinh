// QUAN-20260530-0942
import dayjs from 'dayjs';

/**
 * Checks if a student's age falls within a reasonable range (e.g., 5 to 20 years old).
 * @param dateOfBirth The student's date of birth as a Date object or dayjs object.
 * @returns True if the age is within the valid range, false otherwise.
 */
export const checkStudentAge = (dateOfBirth: Date | dayjs.Dayjs): boolean => {
  const dob = dayjs(dateOfBirth);
  const today = dayjs();
  const age = today.diff(dob, 'year');
  return age >= 5 && age <= 20;
};

// Add other client-side validation utilities here if needed