<template>
  <customers-grid :customers="customers"></customers-grid>
</template>

<script>
import customersGrid from "../components/Customers/CustomersGrid";
import CustomersServices from "../services/Customers";
export default {
  components: { customersGrid },
  data() {
    return {
      customers: []
    };
  },
  methods: {
    getAllCustomers: function() {
      this.loadingCount++;
      CustomersServices.getAllCustomers()
        .then(res => {
          this.customers = res.data;
        })
        .catch(() => {
          // eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in getting customers ",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  created: function() {
    if (!this.isManger && !this.isSales) {
      this.$router.push({ name: "home" });
      return;
    }
    this.getAllCustomers();
  }
};
</script>

<style>
</style>