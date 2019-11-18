import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: {},
    loadingCount:0
  },
  getters: {
    getUser: function(state) {
      return state.user;
    },
    getLoadingCount:function(state){
      return state.loadingCount;
    }
  },
  mutations: {
    setUser: function(state, theUser) {
      state.user = theUser;
    },
    setLoadingCount:function(state,count){
      state.loadingCount=count;
    }
  },
  actions: {},
  modules: {}
});
