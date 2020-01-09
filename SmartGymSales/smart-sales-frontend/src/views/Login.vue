<template>
  <div  :class="{darkMood: blackMode}">
    <div class="login-header">
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
          type="password"
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
          <span>Sign in</span>
        </b-button>
        <b-button
          pill
          block
          size="lg"
          class="login-form-signin"
          variant="light"
          @click="blackMode=!blackMode"
        >
          <span>Switch Mode</span>
        </b-button>
        <label v-if="error" style="color:red">
          please double check your user name and password
        </label>
      </div>
      <!-- :src="blackMode?blackMoodImg:whiteMoodImg" -->
      <!-- :src="'./../assets/'+blackMode?blackMoodImg:whiteMoodImg" -->
      <!-- :src="blackMoodFullImg" -->
      <img class="login-background" alt="smart gym image" :src="blackMode?blackMoodFullImg:whiteMoodFullImg" />
      <!-- {{imagePath}} -->
    </div>
  </div>
</template>

<script>
import UsersService from "../services/Users";
import whiteMoodFullImg from "../assets/smart body gym.png"
import blackMoodFullImg from "../assets/smart body gymBlack.png"
export default {
  name: "login",
  data() {
    return {
      error: false,
      user_name: null,
      password: null,
      blackMode:false,
            whiteMoodFullImg:whiteMoodFullImg,
            blackMoodFullImg:blackMoodFullImg

    };
  },
  methods: {
    Login: function() {
      this.loadingCount++
      UsersService.login(this.user_name, this.password)
        .then(res => {
          // eslint-disable-line no-unused-vars
          console.log(
            res
          ); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
          if (res.data) {
            this.user=res.data
            this.$bvToast.toast('Login Done Successfully',this.sucessToastConfig);
            this.$router.push({ name: "home" });
          } else {
          this.$bvToast.toast('Login Failed',this.failToastConfig);
            this.error = true;
          }
        })
        .catch(error => {

          this.$bvToast.toast('Error '+error.message, this.failToastConfig);

        }) // eslint-disable-line no-unused-vars
        .finally(() => {
          this.loadingCount--
        });
    }
  }
};
</script>

<style scoped>

.darkMood{
background-color: black;
    color: white;
}

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
  position: relative;
    top: -30px;
    right: 80px;
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
