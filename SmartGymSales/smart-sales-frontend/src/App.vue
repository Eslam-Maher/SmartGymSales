<template>
  <div id="app">
    <div class='wrap-whole'>
      <SideBar></SideBar>
      <div class="context-wrapper">
        <header>
          <span>Vue.js PWA</span>
        </header>
        <main class="context">
          <img src="./assets/logo.png" alt="Vue.js PWA" />
          <router-view></router-view>
        </main>
      </div>
    </div>
  </div>
</template>

<script>
import 'material-icons/iconfont/material-icons.css'
import SideBar from './SideBar.vue'
export default {
  components: { SideBar },
  name: 'app'
}
</script>

<style scoped>
body {
  margin: 0;
}

#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50
}

main {
  text-align: center;
  margin-top: 40px;
}

header {
  margin: 0;
  height: 56px;
  padding: 0 16px 0 24px;
  background-color: #35495e;
  color: #ffffff;
}

header span {
  display: block;
  position: relative;
  font-size: 20px;
  line-height: 1;
  letter-spacing: 0.02em;
  font-weight: 400;
  box-sizing: border-box;
  padding-top: 16px;
}

.wrap-whole{
display: flex;
flex-flow: row nowrap;
justify-content: space-between;
height: 100vh;
}

.context-wrapper{
flex: 1 1 300px;
overflow-y: auto;
overflow-x: hidden;
}

.context{
  padding-left: 1rem;
  padding-right: 1rem;
}
</style>
