import { createRouter, createWebHistory } from 'vue-router';
import WeatherListPage from '@/pages/WeatherListPage.vue';
import MainPage from '@/pages/MainPage.vue';
import WeatherChart from '@/pages/WeatherChart.vue';

const routes = [
  {
    path: '/',
    component: MainPage,
  },
  {
    path: '/weather',
    component: WeatherListPage,
  },
  {
    path: '/chart',
    component: WeatherChart,
  },
];

const router = createRouter({
  routes,
  history: createWebHistory(),
});

export default router;
