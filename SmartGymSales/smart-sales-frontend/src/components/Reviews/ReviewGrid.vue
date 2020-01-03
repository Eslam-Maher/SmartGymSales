<template>
  <b-card header="Commissions Grid" header-tag="h4">
   
   <b-row>
      <b-col md="6">
        <b-form-group
          label="Filter"
          label-cols-sm="1"
          label-align-sm="left"
          label-for="filterInput"
        >
          <b-input-group>
            <b-form-input
              v-model="filter"
              type="search"
              id="filterInput"
              placeholder="Type to Search"
            ></b-form-input>
            <b-input-group-append>
              <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
            </b-input-group-append>
          </b-input-group>
        </b-form-group>
      </b-col>
    </b-row>
   
    <b-table
      striped
      hover
      :items="reviews"
      :fields="fields"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      @filtered="onFiltered"
    >
       <template v-slot:cell(creation_date)="row">
        {{row.item.creation_date | DD_MMM_YYYY}}
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
props: ["reviews"],
  data() {
    return {
      totalRows: 0,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 15],
      filter: null,
      fields: [
        {
          key: "parent_name",
          label: "Customer Name",
          class: "text-center"
        },
        {
          key: "parent_type",
          label: "Customer Type",
          class: "text-center"
        },
        {
          key: "training",
          label: "Training Rate",
          sortable: true,
          class: "text-center"
        },
         {
          key: "reciption",
          label: "Reciption Rate",
          sortable: true,
          class: "text-center"
        },
         {
          key: "general",
          label: "General Rate",
          sortable: true,
          class: "text-center"
        },
             {
          key: "comment",
          label: "Comment",
          sortable: true,
          class: "text-center"
        },
            {
          key: "creation_date",
          label: "Creation Date",
          sortable: true,
          class: "text-center"
        },
      ]
    };
  },
  methods: {
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
  },
  watch: {
    reviews: function(newVal) {
      if (newVal) {
        this.totalRows = newVal.length;
      }
    }
  }
}
</script>

<style>

</style>