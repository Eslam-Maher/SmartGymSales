<template>
 <div>
  <add-role :userOptions="users" @refreshGrid="refreshGrid"></add-role>
<br/>
<user-grid :usersRoles="userRoles" @refreshGrid="refreshGrid"> </user-grid>
 </div>
</template>

<script>
import addRole from "../components/UserRoles/AddUserRole";
import userGrid from "../components/UserRoles/UsersRolesGrid";
import UsersService from "../services/Users";
import UserRolesService from "../services/UserRoles";
export default {
components:{addRole,userGrid},
data(){
  return{
      users:[],
      userRoles:[]
  }
},
methods:{
  refreshGrid:function() {
    this.getAllUsersRoles();
  },
   getAllUsers: function() {
            this.loadingCount++
      UsersService.getAllUsers()
        .then(res => {
          this.users = res.data;
        })
        .catch(error => {    // eslint-disable-line no-unused-vars
          this.$bvToast.toast('error in getting users ',this.failToastConfig);
        })
        .finally(() => {
                this.loadingCount--

        });
    },
    getAllUsersRoles: function() {
            this.loadingCount++
      UserRolesService.getAllUserRoles()
        .then(res => {
          this.userRoles = res.data;
          /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
        })
        .catch(error => {    // eslint-disable-line no-unused-vars
          this.$bvToast.toast('error in getting users roles ',this.failToastConfig);
        })
        .finally(() => {
                this.loadingCount--

        });
    }
},
created:function(){
  if (!this.isAdmin){
    this.$router.push({ name: "home" });
    return;
  }
  this.getAllUsers();
  this.getAllUsersRoles();
}
}
</script>
<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>

<style>
</style>