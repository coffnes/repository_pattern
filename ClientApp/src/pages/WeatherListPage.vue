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
            <v-autocomplete
                label="Choose city"
                :items="['Moscow', 'Saint-Petesburg', 'Novosibirsk', 'Yekaterinburg', 'Kazan', 'Nizhny Novgorod']"
                v-model="selectedCity">
            </v-autocomplete>
            <h3>Filter by date</h3>
            <p>Select DateFrom and DateTo</p>
            <date-picker v-model="selectedDate" range :partial-range="false" :enable-time-picker="false"></date-picker>
            <h3>Get only zeroes</h3>
            <v-btn @click="fetchOnlyZeroes()">Get only zeroes</v-btn>
            <v-divider
                :thickness="3"
                class="border-opacity-50">
            </v-divider>
        </div>
        <v-btn color="success">Add measurement</v-btn>
        <weather-list :weathers="sortedWeather"/>
    </div>
</template>

<script>
    import axios from 'axios';
    import WeatherList from '@/components/WeatherList.vue';
    export default {
        components: {'weather-list': WeatherList},
        data() {
            return {
                weathers: [],
                sortOptions: [
                    {value: "", name: "None"},
                    {value: "date", name: "Date"},
                    {value: "city", name: "City"},
                    {value: "temperatureC", name: "Temperature"},
                ],
                selectedSort: '',
                selectedCity: '',
                selectedDate: '',
            }
        },
        methods: {
            async fetchWeathers() {
                try {
                    const response = await axios.get('/weatherforecast');
                    this.weathers = response.data;
                } catch(e) {
                    console.log(e);
                }
            },
            async fetchWeathersByCity(city) {
                try {
                    const response = await axios.get('/weatherforecast/city/' + city);
                    this.weathers = response.data;
                } catch(e) {
                    console.log(e);
                }
            },
            async fetchWeatherByDate(dateFrom, dateTo) {
                try {
                    const response = await axios.get(`/weatherforecast/date/${dateFrom}-${dateTo}`);
                    this.weathers = response.data;
                } catch(e) {
                    console.log(e)
                }
            },
            async fetchOnlyZeroes() {
                try {
                    const response = await axios.get('/weatherforecast/zero');
                    this.weathers = response.data;
                } catch(e) {
                    console.log(e);
                }
            }
        },
        mounted() {
            this.fetchWeathers();
        },
        computed: {
            sortedWeather() {
                return [...this.weathers].sort((w1, w2) => {
                    if(w1[this.selectedSort] < w2[this.selectedSort]) {
                        return -1;
                    } else if(w1[this.selectedSort] > w2[this.selectedSort]) {
                        return 1;
                    }
                    return 0;
                });
            },
        },
        watch: {
            selectedCity(city) {
                if(city !== null) {
                    this.fetchWeathersByCity(city);
                } else {
                    this.fetchWeathers();
                }
            },
            selectedDate(date) {
                const dateFrom = `${date[0].getMonth()+1}.${date[0].getDate()}.${date[0].getFullYear()}`;
                const dateTo = `${date[1].getMonth()+1}.${date[1].getDate()}.${date[1].getFullYear()}`;
                this.fetchWeatherByDate(dateFrom, dateTo);
            }
        }
    }
</script>

<style scoped>
    .request_options {
        margin-bottom: 20px;
    }
</style>