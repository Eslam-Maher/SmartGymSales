<template>
  <div>
    <b-navbar toggleable="lg" type="dark" style="background-color:rgb(255,133,0)">
      <b-navbar-brand to="/">Smart Gym Sales</b-navbar-brand>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <!-- <b-nav-item href="/">Link</b-nav-item> -->
          <b-nav-item-dropdown v-if="isManger||isSales" text="Customers">
             <b-dropdown-item v-if="isSales" to="/Customers">View Customers</b-dropdown-item>
            <b-dropdown-item v-if="isManger" to="/CustomerRefreshPage">Refresh Customers</b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown v-if="isSales" text="Possible Customers">
             <b-dropdown-item v-if="isSales" to="/PossibleCustomersView">View Possible Customers</b-dropdown-item>
             <b-dropdown-item v-if="isSales" to="/AddPossibleCustomers">Add Possible Customers</b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item v-if="isManger" to="/CommissionView">Commission Setup</b-nav-item>
                 <b-nav-item v-if="isManger" to="/ReviewsView">Reviews</b-nav-item>
        </b-navbar-nav>
    <div class="ml-auto" style="display: inline-flex" right>
        <!-- Right aligned nav items -->
        <b-navbar-nav  v-if="isAdmin">
          <b-nav-item-dropdown text="Setup" >
            <b-dropdown-item to="/Users">Users</b-dropdown-item>
            <b-dropdown-item to="/UserRoles">Roles</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>

        <b-navbar-nav  >
        <b-nav-item v-if="user.user_name!=null" @click="user=null" href="/">Sign Out</b-nav-item>
        </b-navbar-nav>
        </div>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
export default {};
</script>

<style></style>
