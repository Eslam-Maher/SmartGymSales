<template>
  <div>
    <add-users></add-users>
    <br/>
    <users-grid></users-grid>
  </div>
</template>

<script>
import addUsers from "../components/Users/AddUsers";
import usersGrid from "../components/Users/UsersGrid";
export default {
  name: "Users",
  components: { usersGrid, addUsers }
};
</script>

<style scoped></style>
