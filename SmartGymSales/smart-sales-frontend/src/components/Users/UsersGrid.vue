<template>
  <b-card header="Users Grid" header-tag="h4">
      <b-table striped hover :items="users"> </b-table>
    </b-card>
</template>

<script>
export default {
 data: function() {
    return {
      users: [
        { id: 1, name: "ess" },
        { id: 2, name: "mera" }
      ]
    };
  }
}
</script>

<style scoped>
.card-body {
  text-align: center;
}
</style>