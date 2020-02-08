<template>
  <div>
    <add-commission @refreshGrid="refreshGrid"></add-commission>
    <br />
    <commission-grid :commissions="commissions" @refreshGrid="refreshGrid"></commission-grid>
  </div>
</template>

<script>
import addCommission from "../components/Commissions/AddCommission";
import commissionGrid from "../components/Commissions/CommissionsGrid";
import CommissionService from "../services/Commission";
export default {
  name: "Commissions",
  components: { addCommission, commissionGrid },
  data() {
    return {
      commissions: []
    };
  },
  created: function() {
    if (!this.isManger) {
      this.$router.push({ name: "home" });
      return;
    }
    this.getAllCommissions();
  },
  methods: {
    refreshGrid: function() {
      this.getAllCommissions();
    },
    getAllCommissions: function() {
      this.loadingCount++;
      CommissionService.GetCommissions()
        .then(res => {
          this.commissions = res.data;
        })
        .catch(error => {
          this.$bvToast.toast(error.message, this.failToastConfig);
        })
        .finally(() => {
          this.loadingCount--;
        });
    }
  }
};
</script>

<style>

</style>