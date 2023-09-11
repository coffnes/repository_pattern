<template>
    <div>
        <h1>It's weather api page</h1>
        <v-select v-if="sortOptions"
            :items="sortOptions"
            :item-title="'name'"
            :item-value="'value'"
            v-model="selectedSort"
            label="Sort options">
        </v-select>
        <p>Filter by city:</p>
        <v-autocomplete
            label="Choose city"
            :items="['Moscow', 'Saint-Petesburg', 'Novosibirsk', 'Yekaterinburg', 'Kazan', 'Nizhny Novgorod']"
            v-model="selectedCity">
        </v-autocomplete>
        <v-btn>Add measurement</v-btn>
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
                    {value: "date", name: "Date"},
                    {value: "city", name: "City"},
                    {value: "temperatureC", name: "Temperature"},
                ],
                selectedSort: '',
                selectedCity: '',
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
            selectedCity(newValue) {
                this.fetchWeathersByCity(newValue);
            }
        }
    }
</script>

<style scoped>

</style>