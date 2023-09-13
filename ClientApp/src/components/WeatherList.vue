<template>
    <v-table>
        <thead>
            <tr>
                <th class="text-left">Date</th>
                <th class="text-left">City</th>
                <th class="text-left">Temperature</th>
            </tr>
        </thead>
        <tbody>
            <weather-item v-for="weather in weathers" :weather="weather" :key="weather.id"/>
        </tbody>
    </v-table>
</template>

<script>
    import WeatherItem from '@/components/WeatherItem.vue'
    export default {
        components: {'weather-item': WeatherItem},
        props: {
            weathers: {
                type: Array,
                required: true
            }
        }
    }
</script>

<style scoped>

</style>