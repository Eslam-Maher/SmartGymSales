<template>
  <b-card header="Customers Grid" header-tag="h4">
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
        {{additionLookup.Sheet}}
      </b-col>
    </b-row>
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
        <label>{{ row.item.is_called ? "Yes" : "No" }}</label>
      </template>
      <template v-slot:cell(is_active)="row">
        <label>{{ row.item.is_active ? "Yes" : "No" }}</label>
      </template>
      <template v-slot:cell(subscription_start_date)="row">
        <label>{{row.item.subscription_start_date? row.item.subscription_start_date||DD_MMM_YYYY :'-'}}</label>
      </template>
      <template v-slot:cell(subscription_end_date)="row">
        <label>{{ row.item.subscription_end_date ? row.item.subscription_end_date||DD_MMM_YYYY:'-' }}</label>
      </template>
      <template v-slot:cell(addition_type_id)=row> 
        <label>
        {{row.item.addition_type_id==additionLookup.Sheet?"Sheet":
          row.item.addition_type_id==additionLookup.Sync?"Sync": 
            row.item.addition_type_id==additionLookup.Manual?"Manual":"-" }}
        </label>
      </template>
      <template v-slot:cell(call)="row">
        <b-button variant="success" @click="openReview(row)">Call</b-button>
      </template>
      <template v-slot:cell(insertPossibleCustomer)="row">
        <b-button variant="primary" @click="openInsertPossibleCustomers(row)">Insert Referral</b-button>
      </template>
    </b-table>
    <b-row v-if="customers.length>0" align-h="between">
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
    <b-modal
      ref="modal-review"
      id="modal-review"
      centered
      title="Call Review"
      @show="resetReviewModal"
      @hidden="resetReviewModal"
      @ok="handleReviewOk"  >
      <form ref="form" @submit.stop.prevent="handleReviewSubmit">
        <b-row>
          <b-col>
            <b-form-group
              :state="reviewState.training"
              label="Training"
              label-for="training-input"
              invalid-feedback="Training is required and between 1 to 5 "
            >
              <b-form-input
                id="training-input"
                v-model="review.training"
                :state="reviewState.training"
                required
                oninput="validity.valid || (value='')"
                type="number"
                min="1"
                max="5"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              :state="reviewState.reciption"
              label="Reciption"
              label-for="Reciption-input"
              invalid-feedback="Reciption is required and between 1 to 5 "
            >
              <b-form-input
                id="Reciption-input"
                v-model="review.reciption"
                :state="reviewState.reciption"
                required
                oninput="validity.valid || (value='')"
                type="number"
                min="1"
                max="5"
              ></b-form-input>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group
              :state="reviewState.general"
              label="General"
              label-for="General-input"
              invalid-feedback="General is required and between 1 to 5 "
            >
              <b-form-input
                id="General-input"
                v-model="review.general"
                :state="reviewState.general"
                required
                oninput="validity.valid || (value='')"
                type="number"
                min="1"
                max="5"
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-form-group
              :state="reviewState.comment"
              label="Comment"
              label-for="Comment-input"
              invalid-feedback="Comment is required"
            >
              <b-form-input
                id="Comment-input"
                v-model="review.comment"
                :state="reviewState.comment"
                required
              ></b-form-input>
            </b-form-group>
          </b-col>
        </b-row>
      </form>
    </b-modal>

    <b-modal
      ref="modal-possibleCustomers"
      id="modal-possibleCustomers"
      centered
      :visible="modalShow"
      @change="prevent"
      hide-footer
      hide-header
    >
      <insert-possibleCustomer :customer_id="customer_id" @closePopUp="hidePopUp()"></insert-possibleCustomer>
    </b-modal>
  </b-card>
</template>

<script>
import insertPossibleCustomer from "../shared/insertPossibleCustomer";
import {Additional_ENUM} from "../../models/enums/AdditionalLookUp.js";
export default {
  components: { insertPossibleCustomer },
  props: ["customers"],
  data() {
    return {
      additionLookup:Additional_ENUM,
      modalShow: false,
      // customers: [
      //   {
      //     id: 1,
      //     name: "eslam",
      //     mobile: "0151164185",
      //     email: "test@test",
      //     additionLookup: { addition_type: "asdasdasd" },
      //     is_called: false,
      //     calles_count: 0,
      //     is_active: false
      //   }
      // ], //for testing
      review: {
        training: 0,
        reciption: 0,
        general: 0,
        comment: "",
        parent_id: null,
        parent_id_type: "customer"
      },

      totalRows: 0,
      currentPage: 1,
      perPage: 10,
      pageOptions: [ 10, 50,100,200],
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
          key: "addition_type_id",
          label: "Source",
          sortable: true,
          class: "text-center"
        },
        {
          key: "is_called",
          label: "Called before",
          sortable: true,
          class: "text-center"
        },
        {
          key: "calles_count",
          label: "Calles Count",
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
          key: "is_active",
          label: "Already Member",
          sortable: true,
          class: "text-center"
        },
        {
          key: "call",
          label: "Call",
          class: "text-center"
        },
        {
          key: "insertPossibleCustomer",
          label: "Insert Possible Customers",
          class: "text-center"
        }
      ],
      customer_id: null
    };
  },
  computed: {
    reviewState() {
      return {
        training: this.review.training >= 1 && this.review.training <= 5,
        reciption: this.review.reciption >= 1 && this.review.reciption <= 5,
        general: this.review.general >= 1 && this.review.general <= 5,
        comment: this.review.comment.length > 0
      };
    }
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
    handleReviewOk: function(bvModalEvt) {
      bvModalEvt.preventDefault();
      this.handleReviewSubmit();
    },

    resetReviewModal: function() {
      this.review = {
        training: 0,
        reciption: 0,
        general: 0,
        comment: "",
        parent_id: null,
        parent_id_type: "customer"
      };
    },
    checkReviewValidity() {
      const valid = this.$refs.form.checkValidity();
      return valid;
    },
    handleReviewSubmit: function() {
      if (!this.checkReviewValidity()) {
        return;
      }

      //call submit review Api

      this.$nextTick(() => {
        this.$refs["modal-review"].hide();
      });
    },
    openReview: function(row) {
      this.review.parent_id = row.item.id;
      this.$refs["modal-review"].show();
    },

    prevent: function() {
      this.$nextTick(() => {
        this.modalShow = false;
      });
    },
    hidePopUp: function() {
      this.$nextTick(() => {
        this.$refs["modal-possibleCustomers"].hide();
      });
    },
    handleP_CustomerSubmit: function() {},
    openInsertPossibleCustomers: function(row) {
      this.customer_id = row.item.id;
      this.$refs["modal-possibleCustomers"].show();
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    }
  }
};
</script>

<style></style>
