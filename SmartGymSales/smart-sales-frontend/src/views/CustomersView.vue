<template>
  <div>
    <search-customers @refreshgrid="refreshGrid"></search-customers>
    <customers-grid :customers="customers" @refreshGrid="refreshGrid"></customers-grid>
  </div>
</template>

<script>
import customersGrid from "../components/Customers/CustomersGrid";
import searchCustomers from "../components/Customers/SearchCustomers";
import CustomersServices from "../services/Customers";
export default {
  components: { customersGrid, searchCustomers },
  data() {
    return {
      customers: [],
      searchCriteria:{
            name:"",
            mobile:"",
            email:"",
            source:null,
            isCalled:null,
            isSubscriped:null
        }    };
  },
  methods: {
    getAllCustomers: function() {
      this.loadingCount++;
      const params ={
            name: this.searchCriteria.name,
            mobile: this.searchCriteria.mobile,
            email: this.searchCriteria.email,
            source: this.searchCriteria.source,
            isCalled: this.searchCriteria.isCalled,
            isSubscriped: this.searchCriteria.isSubscriped
      }
      CustomersServices.getAllCustomers(params)
        .then(res => {
          this.customers = res.data;
          this.customers.forEach(element => {
            element._rowVariant = element.is_active ? "success" : "danger";
          });
        })
        .catch(error => { // eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in getting customers ",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    refreshGrid: function(Creatria) {
      console.log(Creatria); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
      if (Creatria){
      this.searchCriteria.name=Creatria.name;
            this.searchCriteria.mobile=Creatria.mobile;
            this.searchCriteria.email=Creatria.email;
            this.searchCriteria.source=Creatria.source?Creatria.source.id:null;
            this.searchCriteria.isCalled=Creatria.isCalled?Creatria.isCalled.id:null;
            this.searchCriteria.isSubscriped=Creatria.isSubscriped?Creatria.isSubscriped.id:null;
            }
      this.getAllCustomers();
    }
  },
  created: function() {
    if (!this.isSales) {
      this.$router.push({ name: "home" });
      return;
    }
    this.getAllCustomers();
  }
};
</script>

<style>
</style>