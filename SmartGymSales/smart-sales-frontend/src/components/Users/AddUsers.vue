<template>
  <b-card header="Create new User" header-tag="h4">
    <div>
      <b-form @submit="onSubmit" @reset="onReset">
        <b-row>
          <b-col>
            <b-form-group id="input-group-1" label="Name:" label-for="input-1">
              <b-form-input
                id="input-1"
                v-model="InputUser.name"
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
                v-model="InputUser.user_name"
                required
                placeholder="Enter user name"
                :state="userNameValidation"
              ></b-form-input>
              <b-form-invalid-feedback :state="userNameValidation">
                Your password must be 4-10 characters long, contain letters and
                numbers, and must not contain spaces, special characters
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
                v-model="InputUser.password"
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
      InputUser: {
        user_name: null,
        name: null,
        password: null
      }
    };
  },
  computed: {
    PasswordValidation() {
      if (this.InputUser.password) {
        return (
          this.InputUser.password.length >= 5 &&
          this.InputUser.password.length < 20 &&
          !this.invalidFilter(this.InputUser.password)
        );
      } else {
        return null;
      }
    },
    userNameValidation() {
      if (this.InputUser.user_name) {
        return (
          this.InputUser.user_name.length <= 10 &&
          this.InputUser.user_name.length >= 4 &&
          !this.invalidFilter(this.InputUser.user_name)
        );
      } else {
        return null;
      }
    }
  },
  methods: {
    onSubmit: function() {
      this.loadingCount++;
      UsersService.insertUser(this.InputUser)
        .then(res => {
          if (res.data) {
            this.$bvToast.toast(
              "User added Successfully",
              this.sucessToastConfig
            );
          }
          // eslint-disable-line no-unused-vars
          this.$emit("refreshGrid");
          this.onReset();
        })
        .catch(error => {
          this.$bvToast.toast("User addtion Failed", error.message);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    onReset: function() {
      this.InputUser.user_name = null;
      this.InputUser.name = null;
      this.InputUser.password = null;
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
