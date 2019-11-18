<template>
  <div>
    <div class="login-header">
      <!-- <img src="./../assets/valeo-logo.svg" /> -->
    </div>
    <div class="login-container">
      <div class="login-form">
        <h2 class="login-form-header">Welcome to Smart Gym</h2>
        <p class="login-form-label">
          Welcome to sales system, please login to your account!
        </p>
        <b-input
          pill
          block
          size="lg"
          class="login-form-signin"
          variant="light"
          v-model="user_name"
          placeholder="Enter User Name"
        ></b-input>
        <b-input
          pill
          block
          size="lg"
          class="login-form-signin"
          variant="light"
          v-model="password"
          placeholder="Enter Password"
        ></b-input>
        <b-button
          pill
          block
          size="lg"
          class="login-form-signin"
          variant="light"
          @click="Login"
        >
          <!-- <img src="./../assets/search.png"> -->
          <span>Sign in</span>
        </b-button>
        <label v-if="error" style="color:red">
          please double check your user name and password
        </label>
      </div>
      <img class="login-background" src="./../assets/gymBackground.jpg" />
    </div>
  </div>
</template>

<script>
import UsersService from "../services/Users";
export default {
  name: "login",
  data() {
    return {
      error: false,
      user_name: null,
      password: null
    };
  },
  methods: {
    Login: function() {
      UsersService.login(this.user_name, this.password)
        .then(res => {
          // eslint-disable-line no-unused-vars
          console.log(
            res
          ); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
          if (res.data == true) {
            this.$bvToast.toast('Login Done Successfully', {
          title: `Success`,
          variant: "success",
          solid: true
          });
            this.$router.push({ name: "home" });
          } else {
          this.$bvToast.toast('Login Failed', {
          title: `Failure`,
          variant: "danger",
          solid: true
          });
            this.error = true;
          }
        })
        .catch(error => {

          this.$bvToast.toast('Error'+error.message, {
          title: "Failure",/*eslint no-undef: "error"*/

          variant: "danger", /*eslint no-undef: "error"*/

          solid: true
          });

        }) // eslint-disable-line no-unused-vars
        .finally(() => {});
    }
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  flex-flow: row nowrap;
  justify-content: space-between;
  padding-left: 4%;
  padding-right: 4%;
  padding-top: 4%;
}

.login-header {
  padding-top: 2%;
}

.login-header > img {
  padding-left: 4%;
  padding-right: 4%;
  width: 10;
}

.login-background {
  width: 40%;
  height: 5%;
}

.login-form-header {
  font-weight: bold;
}

.login-form-label {
  font-size: 1rem;
  color: #999999;
}

.login-form-signin {
  margin-top: 8%;
}

.login-form-signin > img {
  width: 5%;
  display: inline-block;
  margin-right: 1rem;
}
.login-form-signin > span {
  font-size: medium;
  font-family: "Open Sans", sans-serif;
  color: #949fa5;
}
</style>
