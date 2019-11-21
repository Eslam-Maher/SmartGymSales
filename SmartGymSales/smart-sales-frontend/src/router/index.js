import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Users from "../views/Users.vue";
import Login from "../views/Login.vue";
import UserRoles from "../views/UserRoles.vue";
Vue.use(VueRouter);

const router = new VueRouter({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/about",
      name: "about",
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () =>
        import(/* webpackChunkName: "about" */ "../views/About.vue")
    },
    {
      path: "/Users",
      name: "Users",
      component: Users
    },
    {
      path: "/Login",
      name: "Login",
      component: Login
    },
    { path: "/UserRoles", name: "UserRoles", component: UserRoles }
  ]
});

export default router;
