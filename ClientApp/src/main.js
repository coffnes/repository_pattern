import { createApp } from 'vue';
import './style.css';

// Vuetify
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as VueComponents from 'vuetify/components';
import * as directives from 'vuetify/directives';

// Date Picker
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

import router from '@/router/router';
import App from './App.vue';

const vuetify = createVuetify({
  components: VueComponents,
  directives,
});

createApp(App).use(router).use(vuetify).component('date-picker', VueDatePicker)
  .mount('#app');
