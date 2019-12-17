<template>
  <b-card header="Add Role to User" header-tag="h4">
    <div>
      <b-form @submit="onSubmit" @reset="onReset">
        <b-row>
          <b-col>
            <multiselect v-model="roleUser.user" :options="userOptions" label="name" track-by="id"></multiselect>
          </b-col>
          <b-col>
            <multiselect v-model="roleUser.role" :options="rolesOptions" label="name" track-by="id"></multiselect>
          </b-col>
          <b-col>
            <b-col>
              <div class="button-group actions-buttons">
                <b-button type="submit" variant="primary">Submit</b-button>
                <b-button type="reset" variant="secondary">Reset</b-button>
              </div>
            </b-col>
          </b-col>
        </b-row>
      </b-form>
    </div>
  </b-card>
</template>
<script>
import RolesService from "../../services/Roles";
import UserRolesService from "../../services/UserRoles";
export default {
  props: ["userOptions"],
  data() {
    return {
      rolesOptions: [],
      roleUser: {
        user: null,
        role: null
      }
    };
  },
  created: function() {
    this.getAllRoles();
  },

  methods: {
    onSubmit: function() {
      if (
        this.roleUser == null ||
        this.roleUser.user == null ||
        this.roleUser.role == null
      ) {
        this.$bvToast.toast(
          "please choose the user and his role first",
          this.failToastConfig
        );
        return;
      }
      console.log(
        this.roleUser
      ); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
      this.loadingCount++;
      const userRole={
        user_id:this.roleUser.user.id,
        role_id:this.roleUser.role.id
      }
      UserRolesService.insertUserRole(userRole)
        .then(res => {
          if (res.data) {
            this.$bvToast.toast(
              "User Role added Successfully",
              this.sucessToastConfig
            );
          }else{
            this.$bvToast.toast("User Role addtion Failed", this.failToastConfig);
          }
          // eslint-disable-line no-unused-vars
          this.$emit("refreshGrid");
          this.onReset();
        })
        .catch(error => {
          this.$bvToast.toast(error.message, this.failToastConfig);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    getAllRoles: function() {
      this.loadingCount++;

      RolesService.getAllRoles()
        .then(res => {
          this.rolesOptions = res.data;
        })
        .catch(error => {
          this.$bvToast.toast(error.message, this.failToastConfig);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    onReset: function() {
      this.roleUser.user = null;
      this.roleUser.role = null;
    }
  }
};
</script>
