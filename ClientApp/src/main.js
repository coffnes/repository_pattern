import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from '@/router/router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as vue_components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
    components: vue_components,
    directives,
  });

createApp(App).use(router).use(vuetify).mount('#app')
