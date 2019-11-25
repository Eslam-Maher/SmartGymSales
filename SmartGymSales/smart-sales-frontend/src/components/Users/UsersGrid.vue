<template>
  <b-card header="Users Grid" header-tag="h4">
    <b-table
      striped
      hover
      :items="users"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
      <template v-slot:cell(delete)="row">
        <b-button variant="danger" size="sm" class="mr-1" @click="deleteUser(row.item)">
          <font-awesome-icon icon="trash-alt" />
        </b-button>
      </template>
      <template v-slot:cell(userRoles)="row">
        <div>
          <p v-for="(value, key) in row.item.userRoles" :key="key">{{ value.role.name }}</p>
        </div>
      </template>
    </b-table>
    <b-row align-h="between">
      <b-col cols="3">
        <b-form-group
          label="Per page"
          label-cols-sm="6"
          label-align-sm="right"
          label-size="sm"
          label-for="perPageSelect"
        >
          <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
        </b-form-group>
      </b-col>

      <b-col cols="3">
        <b-pagination
          v-model="currentPage"
          :total-rows="totalRows"
          :per-page="perPage"
          align="fill"
          size="sm"
        ></b-pagination>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
import UsersService from "../../services/Users";
export default {
  props: ["users"],
  data() {
    return {
      totalRows: 0,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15],
      filter: null,
      fields: [
        { key: "name", label: "Name", sortable: true, class: "text-center" },
        {
          key: "user_name",
          label: "User Name",
          sortable: true,
          class: "text-center"
        },
        {
          key: "password",
          label: "Password",
          class: "text-center"
        },
        {
          key: "userRoles",
          label: "Roles",
          sortable: true,
          class: "text-center"
        },
        { key: "delete", label: "Delete" }
      ]
    };
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    deleteUser: function(user) {
      this.loadingCount++;
      UsersService.deleteUser(user.id)
        .then(res => {
          // eslint-disable-line no-unused-vars
          if (res.data) {
            this.$emit("refreshGrid");
            this.$bvToast.toast(
              "User deleted Successfully",
              this.sucessToastConfig
            );
          } else {
            this.$bvToast.toast("User deletion Failed", "User is not found");
          }
        })
        .catch(error => {
          this.$bvToast.toast("User deletion Failed", error.message);
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  watch: {
    users: function(newVal) {
      if (newVal) {
        this.totalRows = newVal.length;
      }
    }
  }
};
</script>

<style scoped>
.card-body {
  text-align: center;
}
</style>
