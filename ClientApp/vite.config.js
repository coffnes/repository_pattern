import { defineConfig } from 'vite';
import { fileURLToPath, URL } from 'node:url';
import vue from '@vitejs/plugin-vue';
import vuetify from 'vite-plugin-vuetify';

export default defineConfig({
  plugins: [
    vue(),
    vuetify(),
  ],
  resolve: {
    alias: [
      { find: '@', replacement: fileURLToPath(new URL('./src', import.meta.url)) },
    ],
  },
  base: '/app',
  server: {
    proxy: {
      '^/weatherforecast.*': {
        target: 'http://localhost:5154',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
