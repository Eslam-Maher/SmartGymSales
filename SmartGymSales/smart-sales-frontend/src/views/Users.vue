<template>
  <div>
    <add-users></add-users>
    <br />
    <users-grid :users="users"></users-grid>
  </div>
</template>

<script>
import addUsers from "../components/Users/AddUsers";
import usersGrid from "../components/Users/UsersGrid";
import UsersService from "../services/Users";
export default {
  name: "Users",
  components: { usersGrid, addUsers },
  data() {
    return {
      users: []
    };
  },
  created: function() {
    this.getAllUsers();
  },
  methods: {
    getAllUsers: function() {
      UsersService.getAllUsers()
        .then(res => {
          this.users = res.data;
        
        })
        // .catch(error => {
        //   console.log(error.message);
        // })
        .finally(() => {});
    }
  }
};
</script>

<style scoped></style>
