<template>
  <b-card header="Update Customers from DB" header-tag="h4">
    <b-row>
      <b-col md="3">
        <b-button variant="primary" @click="UpdateCustomers('Men')">Update Men Customers</b-button>
      </b-col>
      <b-col md="3">
        <b-button
          variant="primary"
          @click="UpdatePossibleCustomers('Men')"
        >Update Men Possible Customers</b-button>
      </b-col>
      <b-col md="3">
        <b-button class="women-btn" @click="UpdateCustomers('Women')">Update Women Customers</b-button>
      </b-col>
      <b-col md="3">
        <b-button
          class="women-btn"
          @click="UpdatePossibleCustomers('Women')"
        >Update Women Possible Customers</b-button>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
import CustomersServices from "../../services/Customers";
export default {
  name: "updateCustomersFromDb",
  methods: {
    UpdateCustomers: function(dbType) {
      this.loadingCount++;
      CustomersServices.updateCustomersFromDb(dbType)
        .then(res => {
          if (res.data.length > 0) {
            res.data.forEach(element => {
              this.$bvToast.toast(element, this.failToastConfig);
            });
          } else {
            this.$bvToast.toast(
              "Customers Updated Successfully",
              this.sucessToastConfig
            );
          }
        })
        .catch(() => {
          this.$bvToast.toast(
            "update customers failed ,please try again later",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    updatePossibleCustomers: function(dbType) {
      this.loadingCount++;
      CustomersServices.updatePossibleCustomersFromDb(dbType)
        .then(res => {
          if (res.data.length > 0) {
            res.data.forEach(element => {
              this.$bvToast.toast(element, this.failToastConfig);
            });
          } else {
            this.$bvToast.toast(
              "Possible Customers Updated Successfully",
              this.sucessToastConfig
            );
          }
        })
        .catch(() => {
          this.$bvToast.toast(
            "update possible customers failed ,please try again later",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  }
};
</script>

<style scoped>
.women-btn {
  background-color: rgb(186, 3, 99);
}
.women-btn:hover {
  background-color: rgb(189, 0, 80);
}
</style>