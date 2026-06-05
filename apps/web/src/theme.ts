// QUAN-20260604-153038
import { createTheme } from '@mantine/core';

export const theme = createTheme({
  /** Put your mantine theme override here */
  fontFamily: 'Inter, sans-serif', // Using Google Fonts: Inter
  headings: {
    fontFamily: 'Inter, sans-serif',
    sizes: {
      h1: { fontSize: '2.2rem' },
      h2: { fontSize: '1.8rem' },
      h3: { fontSize: '1.4rem' },
    },
  },
  colors: {
    // Custom colors can be defined here for premium aesthetics
    'deep-blue': ['#EAF2F8', '#D3E0E9', '#A7C0D9', '#7AA0C2', '#507FA8', '#386693', '#2B5580', '#1F4060', '#142B40', '#0A1A2B'],
  },
  primaryColor: 'deep-blue',
  primaryShade: { light: 6, dark: 8 },
  shadows: {
    md: '1px 1px 3px rgba(0, 0, 0, .25)',
    xl: '5px 5px 20px rgba(0, 0, 0, .25)',
  },
  // Add other global styles or components overrides for Glassmorphism, etc.
  // Example for Glassmorphism effect (requires custom styles in App.tsx or global CSS):
  // components: {
  //   Card: {
  //     styles: (theme) => ({
  //       root: {
  //         backgroundColor: theme.colorScheme === 'dark' ? 'rgba(0,0,0,0.3)' : 'rgba(255,255,255,0.3)',
  //         backdropFilter: 'blur(10px)',
  //         border: '1px solid rgba(255, 255, 255, 0.18)',
  //         boxShadow: theme.shadows.xl,
  //       },
  //     }),
  //   },
  // },
});