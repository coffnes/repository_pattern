import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from '@/router/router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as vue_components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import VueDatePicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'

const vuetify = createVuetify({
    components: vue_components,
    directives,
  });

createApp(App).use(router).use(vuetify).component('date-picker', VueDatePicker).mount('#app')
