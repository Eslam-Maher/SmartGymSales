<template>
  <div id="app">
    <loader v-if="loadingCount >0" />
    <nav-bar></nav-bar>
    <div class="context-wrapper">
      <router-view class="context" />
    </div>
  </div>
</template>
<script>
import navBar from "../src/components/navBar.vue";
import loader from  "../src/components/Loader.vue";
export default {
  name: "App",
  components: { navBar,loader }
};
</script>
<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}
.context-wrapper {
  /* flex: 1 1 300px; */
  overflow-y: auto;
  overflow-x: hidden;
}

.context {
  padding-top: 2rem;
  padding-left: 1rem;
  padding-right: 1rem;
  padding-bottom: 1rem;
}
#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
