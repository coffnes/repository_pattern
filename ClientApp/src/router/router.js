import { createRouter, createWebHistory } from 'vue-router';
import WeatherListPage from '@/pages/WeatherListPage.vue';
import MainPage from '@/pages/MainPage.vue';
import WeatherChart from '@/pages/WeatherChart.vue';

const routes = [
  {
    path: '/app',
    component: MainPage,
  },
  {
    path: '/app/weather',
    component: WeatherListPage,
  },
  {
    path: '/app/chart',
    component: WeatherChart,
  },
];

const router = createRouter({
  routes,
  history: createWebHistory(),
});

export default router;
