<template>
  <b-card header="Create new User" header-tag="h4">
    <div>
      <b-form @submit="onSubmit" @reset="onReset">
        <b-row>
          <b-col>
            <b-form-group id="input-group-1" label="Name:" label-for="input-1">
              <b-form-input
                id="input-1"
                v-model="user.name"
                required
                placeholder="Enter name"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              id="input-group-2"
              label="User Name:"
              label-for="input-2"
            >
              <b-form-input
                id="input-2"
                v-model="user.user_name"
                required
                placeholder="Enter user name"
                :state="userNameValidation"
              ></b-form-input>
              <b-form-invalid-feedback :state="userNameValidation">
               Your password must be 4-10 characters long, 
               contain letters and numbers, and must not
                contain spaces, special characters
              </b-form-invalid-feedback>
              <b-form-valid-feedback :state="userNameValidation">
                Looks Good.
              </b-form-valid-feedback>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              id="input-group-3"
              label="Password:"
              label-for="input-3"
            >
              <b-form-input
                type="password"
                id="text-password"
                required
                v-model="user.password"
                :state="PasswordValidation"
                placeholder="Enter Password"
              ></b-form-input>
              <b-form-invalid-feedback :state="PasswordValidation">
                Your password must be 5-20 characters long, contain letters and
                numbers, and must not contain spaces, special characters, or
                emoji.
              </b-form-invalid-feedback>
              <b-form-valid-feedback :state="PasswordValidation">
                Looks Good.
              </b-form-valid-feedback>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <div class="button-group actions-buttons">
              <b-button type="submit" variant="primary">Submit</b-button>
              <b-button type="reset" variant="secondary">Reset</b-button>
            </div>
          </b-col>
        </b-row>
      </b-form>
    </div>
  </b-card>
</template>

<script>
import UsersService from "../../services/Users";

export default {
  name: "addUsers",
  data() {
    return {
      user: {
        user_name: null,
        name: null,
        password: null
      }
    };
  },
  computed: {
    PasswordValidation() {
      if (this.user.password) {
        return (
          this.user.password.length >= 5 &&
          this.user.password.length < 20 &&
          !this.invalidFilter(this.user.password)
        );
      } else {
        return null;
      }
    },
    userNameValidation() {
      if (this.user.user_name) {
        return (
          this.user.user_name.length <= 10 && this.user.user_name.length >= 4&&
          !this.invalidFilter(this.user.user_name)
        );
      } else {
        return null;
      }
    }
  },
  methods: {
    onSubmit: function() {
      UsersService.insertUser(this.user)
        .then(res => {          // eslint-disable-line no-unused-vars
        this.$emit("refreshGrid")
        this.onReset();
          /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
          console.log("test");
        })
        .finally(() => {});
    },
    onReset: function() {
      this.user.user_name = null;
      this.user.name = null;
      this.user.password = null;
    }
  }
};
</script>

<style scoped>
.card-body {
  text-align: "center";
}
.actions-buttons {
  float: right !important;
}
</style>
