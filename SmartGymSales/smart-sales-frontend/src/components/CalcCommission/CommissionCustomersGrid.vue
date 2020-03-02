<template>
  <b-card :header="title" header-tag="h4">
    <b-table
      striped
      hover
      :items="sourceData"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
      <template
        v-slot:cell(subscription_start_date)="row"
      >{{row.item.subscription_start_date | DD_MMM_YYYY}}</template>
      <template
        v-slot:cell(subscription_end_date)="row"
      >{{row.item.subscription_end_date | DD_MMM_YYYY}}</template>
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
  props: ["sourceData", "title"],
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
          label: "Name",
          class: "text-center"
        },
        {
          key: "mobile",
          label: "Mobile",
          class: "text-center"
        },
        {
          key: "calles_count",
          label: "calles Count",
          sortable: true,
          class: "text-center"
        },
        {
          key: "subscription_start_date",
          label: "Subscription Start Date",
          sortable: true,
          class: "text-center"
        },
        {
          key: "subscription_end_date",
          label: "Subscription End Date",
          sortable: true,
          class: "text-center"
        },
        {
          key: "subscription_paid_money",
          label: "Money Paid",
          sortable: true,
          class: "text-center"
        },
        { key: "is_called_by_name", label: "Called By" }
      ]
    };
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    }
  },
  watch: {
    commissions: function(newVal) {
      if (newVal) {
        this.totalRows = newVal.length;
      }
    }
  }
};
</script>

<style>
</style>