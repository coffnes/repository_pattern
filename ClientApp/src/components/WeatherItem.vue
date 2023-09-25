<template>
    <tr>
      <td>{{ convertDate(weather.date) }}</td>
      <td>{{ weather.city }}</td>
      <td>{{ weather.temperatureC }}</td>
    </tr>
</template>

<script>
export default {
  props: {
    weather: {
      type: Object,
      required: true,
    },
  },
  methods: {
    convertDate(timestamp) {
      const date = new Date(timestamp * 1000);
      const day = (`0${date.getDate()}`).slice(-2);
      const month = (`0${date.getMonth()}`).slice(-2);
      const year = date.getFullYear();
      const strDate = `${day}-${month}-${year}`;
      return strDate;
    },
  },
};
</script>

<style scoped>

</style>
