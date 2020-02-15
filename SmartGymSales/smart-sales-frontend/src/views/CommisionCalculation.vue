<template>
  <div>
    <calc-commision @calcCommission="calcCommission"></calc-commision>
    <commision-data :commissionOutput="commissionOutput"></commision-data>
  </div>
</template>
<script>
import calcCommision from "../components/CalcCommission/CalcCommision";
import commisionData from "../components/CalcCommission/CommissionData";
import CommissionService from "../services/Commission";// eslint-disable-line no-unused-vars
import moment from "moment";// eslint-disable-line no-unused-vars

export default {
  name: "CommissionCalculation",
  components: { calcCommision, commisionData },
  created: function() {
    if (!this.isManger) {
      this.$router.push({ name: "home" });
      return;
    }
  },
  data() {
    return {
      commissionOutput: null
    };
  },
  methods: {
    calcCommission: function(data) {// eslint-disable-line no-unused-vars
    //   this.commissionOutput = {
    //     inputIncome: 1000,
    //     totalRequiredIncome: 5000,
    //     totalCountCustomersCalled: 2,
    //     totalCountCustomerSubscriped: 1,
    //     commission: 10
    //   };
        this.loadingCount++;
        CommissionService.calcCommission(
          moment(data.from),
          moment(data.to),
          data.employee
        )
          .then(res => {
           this.commissionOutput=res.data;
          })
          .catch(error => {
            this.$bvToast.toast("Commission Calculation Failed", error.message);
          })
          .finally(() => {
            this.loadingCount--;
          });
    }
  }
};
</script>