<template>
  <div>
    <b-navbar toggleable="lg" type="dark" style="background-color:rgb(255,133,0)">
      <b-navbar-brand to="/">Smart Gym Sales</b-navbar-brand>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item href="/">Link</b-nav-item>
          <b-nav-item-dropdown text="Customers">
            <b-dropdown-item to="/CustomerRefreshPage">Refresh Customers</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item-dropdown text="Setup" right>
            <b-dropdown-item to="/Users">Users</b-dropdown-item>
            <b-dropdown-item to="/UserRoles">Roles</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>

        <b-navbar-nav>
        <b-nav-item @click="this.user=null" href="/">Sign Out</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
export default {};
</script>

<style></style>
