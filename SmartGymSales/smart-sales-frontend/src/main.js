// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Vuex from 'vuex'
import Vuesax from 'vuesax'
import 'vuesax/dist/vuesax.css'

Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.use(require('vue-moment'))
Vue.use(Vuex)
Vue.use(Vuesax)
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})
