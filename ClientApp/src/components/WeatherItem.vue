<template>
    <tr>
        <td>{{ weather.date }}</td>
        <td>{{ weather.city }}</td>
        <td>{{ weather.temperatureC }}</td>
    </tr>
</template>

<script>
    export default {
        props: {
            weather: {
                type: Object,
                required: true
            }
        }
    }
</script>

<style scoped>

</style>