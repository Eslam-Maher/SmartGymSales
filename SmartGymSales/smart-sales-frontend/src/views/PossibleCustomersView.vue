<template>
    <possible-customersGrid :possibleCustomers="possibleCustomers"></possible-CustomersGrid>

</template>

<script>
import possibleCustomersService from "../services/PossibleCustomers"
import possibleCustomersGrid from "../components/PossibleCustomers/PossibleCustomersGrid"
export default {
  components: { possibleCustomersGrid },
  data() {
    return {
      possibleCustomers: []
    };
  },
  methods: {
    GetPossibleCustomerrs: function() {
      this.loadingCount++;
      possibleCustomersService.GetPossibleCustomerrs()
        .then(res => {
          this.possibleCustomers = res.data;
        //   this.customers.forEach(element => {
        //     element. _rowVariant= element.is_active?'success': 'danger'
        //   });
        })
        .catch(error => { // eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in getting possible customers ",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  },
  created: function() {
    if (!this.isSales) {
      this.$router.push({ name: "home" });
      return;
    }
    this.GetPossibleCustomerrs();
  }
}
</script>

<style>

</style>