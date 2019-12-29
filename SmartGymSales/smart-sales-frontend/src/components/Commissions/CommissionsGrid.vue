<template>
  <b-card header="Commissions Grid" header-tag="h4">
    <b-table
      striped
      hover
      :items="commissions"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
       <template v-slot:cell(creation_date)="row">
        {{row.item.creation_date | DD_MMM_YYYY}}
      </template>
      <template v-slot:cell(delete)="row">
        <b-button variant="danger" size="sm" class="mr-1" @click="deleteCommission(row.item)">
          <font-awesome-icon icon="trash-alt" />
        </b-button>
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
import CommissionService from "../../services/Commission";
export default {
 props: ["commissions"],
  data() {
    return {
      totalRows: 0,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15],
      filter: null,
      fields: [
        {
          key: "new_customer_percentatge",
          label: "New Member Percentatge",
          class: "text-center"
        },
        {
          key: "old_customer_percentatge",
          label: "Old Member Percentatge",
          class: "text-center"
        },
        {
          key: "target",
          label: "Target",
          sortable: true,
          class: "text-center"
        },
            {
          key: "creation_date",
          label: "Creation Date",
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
    deleteCommission: function(commission) {
      this.loadingCount++;
      CommissionService.deleteCommission(commission.id)
        .then(res => {
           if (res.data.length > 0) {
            res.data.forEach(element => {
              this.$bvToast.toast(element, this.failToastConfig);
            });
          } else {
            this.$bvToast.toast(
              "Commission Deleted Successfully",
              this.sucessToastConfig
            );
                        this.$emit("refreshGrid");
            }
        })
        .catch(error => {
          this.$bvToast.toast("commission deletion Failed", error.message);
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  watch: {
    commissions: function(newVal) {
      if (newVal) {
        this.totalRows = newVal.length;
      }
    }
  }
}
</script>

<style>

</style>