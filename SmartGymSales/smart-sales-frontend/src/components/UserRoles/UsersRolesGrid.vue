<template>
  <b-card header="Users Roles Grid" header-tag="h4">
    <b-table
      striped
      hover
      :items="usersRoles"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
      <template v-slot:cell(delete)="row">
        <b-button
          variant="danger"
          size="sm"
          class="mr-1"
          @click="deleteUserRole(row.item)"
        >
          <font-awesome-icon icon="trash-alt" />
        </b-button>
      </template>
    </b-table>
  </b-card>
</template>

<script>
import UserRolesService from "../../services/UserRoles"
export default {

 props: ["usersRoles"],
  data() {
    return {
      totalRows: 0,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15],
      filter: null,
      fields: [
        { key: "user.name", label: "User Name", sortable: true, class: "text-center" },
        {
          key: "role.name",
          label: "Role",
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
    deleteUserRole: function(user) {
      this.loadingCount++;
      UserRolesService.deleteUserRole(user.id)
        .then(res => {
          // eslint-disable-line no-unused-vars
          if (res.data){
          this.$emit("refreshGrid");
          this.$bvToast.toast(
            "User Role deleted Successfully",
            this.sucessToastConfig
          );}
          else{
             this.$bvToast.toast("User Role deletion Failed","User is not found");
          }
        })
        .catch(error => {
          this.$bvToast.toast("User Role deletion Failed", error.message);
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  watch: {
    usersRoles: function(newVal) {
      if (newVal) {
        this.totalRows = newVal.length;
      }
    }
  }
};
</script>

<style>

</style>