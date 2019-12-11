<template>
<upload-customerExcel></upload-customerExcel>

</template>

<script>
import uploadCustomerExcel from "../components/CustomerRefreshPage/UploadCustomerExcel"
export default {
components:{uploadCustomerExcel},
created: function() {
    if (!this.isManger) {
      this.$router.push({ name: "home" });
      return;
    }
}
}
</script>

<style>

</style>