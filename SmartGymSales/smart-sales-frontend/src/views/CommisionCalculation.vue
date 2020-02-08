<template>
<div>
<calc-commision></calc-commision>
</div>
</template>
<script>
import calcCommision from "../components/CalcCommission/CalcCommision";
export default {
name: "CommissionCalculation",
components:{calcCommision},
created:function(){
     if (!this.isManger) {
      this.$router.push({ name: "home" });
      return;
    }
}

}
</script>