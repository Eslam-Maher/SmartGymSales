<template>
  <div>
    <add-users @refreshGrid="refreshGrid"></add-users>
    <br />
    <users-grid :users="users" @refreshGrid="refreshGrid"></users-grid>
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
    refreshGrid:function(){
      this.getAllUsers();
    },
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
