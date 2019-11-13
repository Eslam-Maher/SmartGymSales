// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Vuex from 'vuex'
import mixin from "./mixin.js";
import { store } from "./store";
import BootstrapVue from "bootstrap-vue";
import Multiselect from "vue-multiselect"
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import vueDatepicker from "vuejs-datepicker";


Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.mixin(mixin);
Vue.use(BootstrapVue);
Vue.use(require('vue-moment'))
Vue.component("multiselect", Multiselect);
Vue.component("vue-datepicker", vueDatepicker);
// Vue.component("page-header", () =>
//   import("@/components/shared/PageHeader.vue")
// );//to be handled later
Vue.use(Vuex)
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App }
})
