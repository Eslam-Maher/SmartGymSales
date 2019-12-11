<template>
  <b-card header="Customers Grid" header-tag="h4">
    <b-table
      striped
      hover
      :items="customers"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
      <template v-slot:cell(is_called)="row">
        <label> {{row.item.is_called?'Yes':'No'}}</label>
      </template>
      <template v-slot:cell(is_active)="row">
        <label> {{row.item.is_active?'Yes':'No'}}</label>
      </template>
      <template v-slot:cell(subscription_start_date)="row">
          <label>{{row.item.subscription_start_date||DD_MMM_YYYY}} </label>
      </template>
      <template v-slot:cell(subscription_end_date)="row">
          <label>{{row.item.subscription_end_date||DD_MMM_YYYY}} </label>
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
export default {
  props: ["customers"],
  data() {
    return {
      totalRows: 0,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15],
      filter: null,
      fields: [
        {
          key: "name",
          label: "Customer Name",
          sortable: true,
          class: "text-center"
        },
        {
          key: "mobile",
          label: "Mobile",
          sortable: true,
          class: "text-center"
        },
        {
          key: "email",
          label: "Email",
          sortable: true,
          class: "text-center"
        },
        {
            key:"additionLookup.addition_type",
            label:"Source",sortable: true,
          class: "text-center"
        },  {
            key:"is_called",
            label:"Called before",sortable: true,
          class: "text-center"
        },
        {
            key:"calles_count",
            label:"Calles Count",sortable: true,
          class: "text-center"
        },
        {
            key:"subscription_start_date",
            label:"Subscription Start Date",sortable: true,
          class: "text-center"
        },
        {
            key:"subscription_end_date",
            label:"Subscription End Date",sortable: true,
          class: "text-center"
        },
        {
            key:"is_active",
            label:"Already Member",sortable: true,
          class: "text-center"
        },
      ]
    };
  },
  watch: {
    customers: function(newVal) {
      if (newVal) {
        console.log(
          newVal
        ); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
        this.totalRows = newVal.length;
      }
    }
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    }
  }
};
</script>

<style>
</style>