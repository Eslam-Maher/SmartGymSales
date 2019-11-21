import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import axios from "axios";
import VueAxios from "vue-axios";
import Multiselect from "vue-multiselect";
import Datepicker from "vuejs-datepicker";
import mixin from "./mixin.js";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faUserSecret } from '@fortawesome/free-solid-svg-icons'
import { faTrashAlt } from '@fortawesome/free-solid-svg-icons'
import { faFontAwesome } from '@fortawesome/free-brands-svg-icons'
library.add(faTrashAlt)
library.add(faUserSecret)
library.add(faFontAwesome)

Vue.config.productionTip = false;
Vue.mixin(mixin);
Vue.use(BootstrapVue);
Vue.use(VueAxios, axios);
Vue.use(require("vue-moment"));
Vue.component("multiselect", Multiselect);
Vue.component("Datepicker", Datepicker);
Vue.component("font-awesome-icon", FontAwesomeIcon);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
