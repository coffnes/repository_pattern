<template>
    <div>
        <h1>It's weather api page</h1>
        <div class="request_options">
            <v-select v-if="sortOptions"
                :items="sortOptions"
                :item-title="'name'"
                :item-value="'value'"
                v-model="selectedSort"
                label="Sort options">
            </v-select>
            <h3>Filter by city:</h3>
            <v-select
                label="Choose city"
                :items="['None',
                  'Moscow',
                  'Saint-Petesburg',
                  'Yekaterinburg',
                  'Kazan',
                  'Nizhny Novgorod']"
                v-model="selectedCity">
            </v-select>
            <h3>Filter by date</h3>
            <p>Select DateFrom and DateTo</p>
            <date-picker v-model="selectedDate" range
              :partial-range="false"
              :enable-time-picker="false">
            </date-picker>
            <h3>Get only zeroes</h3>
            <v-btn @click="fetchOnlyZeroes()">Get only zeroes</v-btn>
            <v-divider
                :thickness="3"
                class="border-opacity-50">
            </v-divider>
        </div>
        <add-dialog @create="addWeather"></add-dialog>
        <weather-list :weathers="sortedWeather"/>
    </div>
</template>

<script>
import axios from 'axios';
import WeatherList from '@/components/WeatherList.vue';
import AddDialog from '@/components/UI/AddDialog.vue';

export default {
  components: {
    'weather-list': WeatherList,
    'add-dialog': AddDialog,
  },
  data() {
    return {
      weathers: [],
      sortOptions: [
        { value: '', name: 'None' },
        { value: 'date', name: 'Date' },
        { value: 'city', name: 'City' },
        { value: 'temperatureC', name: 'Temperature' },
      ],
      selectedSort: '',
      selectedCity: '',
      selectedDate: '',
    };
  },
  methods: {
    async addWeather(weather) {
      await axios.post('/weatherforecast', { ...weather })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async fetchWeathers() {
      try {
        const response = await axios.get('/weatherforecast');
        this.weathers = response.data;
      } catch (e) {
        console.log(e);
      }
    },
    async fetchWeathersByCity(city) {
      try {
        const response = await axios.get(`/weatherforecast/city/${city}`);
        this.weathers = response.data;
      } catch (e) {
        console.log(e);
      }
    },
    async fetchWeatherByDate(dateFrom, dateTo) {
      try {
        const response = await axios.get(`/weatherforecast/date/${dateFrom}/${dateTo}`);
        this.weathers = response.data;
      } catch (e) {
        console.log(e);
      }
    },
    async fetchOnlyZeroes() {
      try {
        const response = await axios.get('/weatherforecast/zero');
        this.weathers = response.data;
      } catch (e) {
        console.log(e);
      }
    },
  },
  mounted() {
    this.fetchWeathers();
  },
  computed: {
    sortedWeather() {
      return [...this.weathers].sort((w1, w2) => {
        if (w1[this.selectedSort] < w2[this.selectedSort]) {
          return -1;
        }
        if (w1[this.selectedSort] > w2[this.selectedSort]) {
          return 1;
        }
        return 0;
      });
    },
  },
  watch: {
    selectedCity(city) {
      if (city !== 'None') {
        this.fetchWeathersByCity(city);
      } else {
        this.fetchWeathers();
      }
    },
    selectedDate(date) {
      const dateFrom = Math.round(date[0].getTime() / 1000);
      const dateTo = Math.round(date[1].getTime() / 1000);
      this.fetchWeatherByDate(dateFrom, dateTo);
    },
  },
};
</script>

<style scoped>
    .request_options {
        margin-bottom: 20px;
    }
</style>
