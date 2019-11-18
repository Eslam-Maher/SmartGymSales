<template>
  <div class="spinner-wrapper">
    <b-spinner class="spinner"
               variant="success"
               label="Loading..."></b-spinner>
  </div>
</template>

<script>
export default {
  name: "loader"
};
</script>

<style scoped>
.spinner-wrapper {
  width: 100vw;
  height: 100vh;
  display: flex;
  position: fixed;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  background-color: rgba(255, 255, 255, 0.5);
}

.spinner {
  width: 3rem;
  height: 3rem;
}
</style>
