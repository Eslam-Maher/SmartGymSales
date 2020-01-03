import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Users from "../views/Users.vue";
import Login from "../views/Login.vue";
import CustomerRefreshPage from "../views/CustomerRefreshPage.vue";
import CustomersView from "../views/CustomersView.vue";
import UserRoles from "../views/UserRoles.vue";
import AddPossibleCustomers from "../components/PossibleCustomers/insertPossibleCustomer.vue"
import PossibleCustomersView from "../views/PossibleCustomersView.vue";
import CommissionView from "../views/CommissionView";
import ReviewsView from "../views/ReviewsView";
Vue.use(VueRouter);

const router = new VueRouter({
  routes: [
    {
      path: "/home",
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
      path: "/",
      name: "Login",
      component: Login
    },
    {
      path:"/CustomerRefreshPage",
      name:"CustomerRefresh",
      component:CustomerRefreshPage
    },
    {
      path:"/Customers",
      name:"Customers",
      component:CustomersView
    },
    {
      path:"/AddPossibleCustomers",
      name:"AddPossibleCustomers",
      component:AddPossibleCustomers
    },{
      path:"/PossibleCustomersView",
      name:"PossibleCustomersView",
      component:PossibleCustomersView
    },
    {
      path:"/CommissionView",
      name:"CommissionView",
      component:CommissionView
    },
    {
      path:"/ReviewsView",
      name:"ReviewsView",
      component:ReviewsView
    },
    { path: "/UserRoles", name: "UserRoles", component: UserRoles }
  ]
});

export default router;
