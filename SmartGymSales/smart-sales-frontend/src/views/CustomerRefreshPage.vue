<template>
<div>
<upload-customerExcel></upload-customerExcel>
<br/>
<update-customerDB></update-customerDB>
</div>
</template>

<script>
import uploadCustomerExcel from "../components/CustomerRefreshPage/UploadCustomerExcel"
import updateCustomerDB from "../components/CustomerRefreshPage/UpdateCustomerFromDb"
export default {
components:{uploadCustomerExcel,updateCustomerDB},
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