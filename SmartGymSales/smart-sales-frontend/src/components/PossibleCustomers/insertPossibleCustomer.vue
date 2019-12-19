<template>
  <b-card header="Insert Possible Customer" header-tag="h4">
    <form ref="form" @submit.stop.prevent="handleP_CustomerSubmit">
      <b-row>
        <b-col>
          <b-form-group
            :state="possibleCustomerState.name"
            label="Name"
            label-for="name-input"
            invalid-feedback="Name is required"
          >
            <b-form-input
              id="name-input"
              v-model="possibleCustomer.name"
              :state="possibleCustomerState.name"
              required
              type="text"
            ></b-form-input>
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group
            :state="possibleCustomerState.mobile"
            label="Mobile"
            label-for="Mobile-input"
            invalid-feedback="Mobile is required and must be valid "
          >
            <b-form-input
              id="Mobile-input"
              v-model="possibleCustomer.mobile"
              :state="possibleCustomerState.mobile"
              required
              type="text"
            ></b-form-input>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-form-group
            :state="possibleCustomerState.email"
            label="Email"
            label-for="email-input"
            invalid-feedback="Email must be valid "
          >
            <b-form-input
              id="email-input"
              v-model="possibleCustomer.email"
              :state="possibleCustomerState.email"
              type="text"
            ></b-form-input>
          </b-form-group>
        </b-col>
        <b-col>
          <b-form-group
            :state="possibleCustomerState.discont_percentage"
            label="Discont%"
            label-for="discont_percentage-input"
            invalid-feedback="Email must be valid "
          >
            <b-form-input
              id="discont_percentage-input"
              v-model="possibleCustomer.discont_percentage"
              :state="possibleCustomerState.discont_percentage"
              oninput="validity.valid || (value='')"
              type="number"
              min="0"
              max="100"
            ></b-form-input>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col md="6">  <b-form-group
            label="Knowledge"
            label-for="Knowledge-input"
            :state="possibleCustomerState.knowledge_id"
            invalid-feedback="You must select knowledge option"
          >
          <b-form-select v-model="possibleCustomer.knowledge_id" value-field="id" id="Knowledge-input"
          text-field="type" :options="Knowledgeoptions"></b-form-select>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button-group>
          <b-button variant="primary" type="submit">Insert Possible Customer</b-button>
          <b-button @click="close">close</b-button>
          </b-button-group>
        </b-col>
      </b-row>
    </form>
  </b-card>
</template>

<script>
import knowledgeLookUpService from "../../services/knowledgeLookUp";
export default {
  props: { customer_id: Number },
  data() {
    return {
      Knowledgeoptions:[],
      possibleCustomer: {
        name: "",
        mobile: "",
        email: "",
        customer_id: this.customer_id,
        discont_percentage: 0,
        knowledge_id: null, //forNow
        addition_type_id: 0 //fornow
      }
    };
  },
  created:function(){
     if (!this.isSales) {
      this.$router.push({ name: "home" });
      return;
    }
    this.getKnowledgeLookup();
  },
  computed: {
    possibleCustomerState() {
      return {
        name: this.possibleCustomer.name.length > 0,
        mobile: this.possibleCustomer.mobile.length >= 10,
        email:
          this.possibleCustomer.email.length == 0 ||
          this.possibleCustomer.email.length > 0,
        discont_percentage:
          this.possibleCustomer.discont_percentage >= 0 &&
          this.possibleCustomer.discont_percentage <= 100,
        knowledge_id: this.possibleCustomer.knowledge_id != null
      };
    }
  },
  methods: {
    handleP_CustomerSubmit: function() {},
    resetP_CustomerModal: function() {
      this.possibleCustomer= {
        name: "",
        mobile: "",
        email: "",
        customer_id: this.customer_id,
        discont_percentage: 0,
        knowledge_id: null, //forNow
        addition_type_id: 0 //fornow
      };
    },
    getKnowledgeLookup:function(){

        this.loadingCount++;
      knowledgeLookUpService.GetknowledgeLookups()
        .then(res => {
          this.Knowledgeoptions = res.data;
        })
        .catch(error => { // eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in getting knowledge options ",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    close: function() {
      this.$emit("closePopUp");
    }
  }
};
</script>

<style></style>
