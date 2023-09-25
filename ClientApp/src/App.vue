<template>
  <div class="main">
    <v-app>
      <v-app-bar scroll-behavior="elevate" clipped-left>
        <v-btn stacked variant="plain" href="/app">Main</v-btn>
        <v-btn stacked variant="plain" href="/app/weather">Weather API</v-btn>
        <v-btn stacked variant="plain" href="/app/weather/filter_client">Filter by Client</v-btn>
        <v-btn stacked variant="plain"
          href="/app/weather/filter_query_post">
          Filter with query post request
        </v-btn>
        <v-btn stacked variant="plain"
          href="/app/weather/filter_query_get">
          Filter with query get request
        </v-btn>
        <v-btn stacked variant="plain"
          href="/app/weather/filter_graphql">
          Filter by GraphQL
        </v-btn>
        <v-btn stacked variant="plain" href="/app/chart">Chart</v-btn>
      </v-app-bar>
      <v-main>
        <router-view></router-view>
      </v-main>
  </v-app>
  </div>
</template>

<script>
export default {
};
</script>

<style scoped>
  .main {
    margin: 20px;
  }
</style>
