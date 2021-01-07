<template>
  <div class="home">
    <img alt="Smart Body Gym logo" src="../assets/home.jpg" />

  </div>
</template>

<script>
// @ is an alias to /src

export default {
  name: "home",

  created:function() {
    if(!this.user ||!this.user.userRoles){
    this.$router.push({ name: "Login" });
    }
  }
};
</script>
