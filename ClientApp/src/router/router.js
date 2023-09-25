import { createRouter, createWebHistory } from 'vue-router';
import WeatherListPage from '@/pages/WeatherListPage.vue';
import MainPage from '@/pages/MainPage.vue';
import WeatherChart from '@/pages/WeatherChart.vue';
import WeatherListClient from '@/pages/WeatherListClient.vue';
import FilterQueryPost from '@/pages/FilterQueryPost.vue';
import FilterQueryGet from '@/pages/FilterQueryGet.vue';
import FilterGraphQL from '@/pages/FilterGraphQL.vue';

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
  {
    path: '/app/weather/filter_client',
    component: WeatherListClient,
  },
  {
    path: '/app/weather/filter_query_post',
    component: FilterQueryPost,
  },
  {
    path: '/app/weather/filter_query_get',
    component: FilterQueryGet,
  },
  {
    path: '/app/weather/filter_graphql',
    component: FilterGraphQL,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
