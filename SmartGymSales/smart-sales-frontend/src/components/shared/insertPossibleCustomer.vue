<template>
  <b-card header="Insert Possible Customer" header-tag="h4">
    <form ref="form" @submit.stop.prevent="handleP_CustomerSubmit">
      <b-row>
        <b-col>
          <b-button type="submit">Insert Possible Customer</b-button>
          <b-button @click="close">close</b-button>

        </b-col>
      </b-row>
    </form>
  </b-card>
</template>

<script>
export default {
  props: { customer_id: Number },
  data() {
    return {
      possibleCustomer: {
        name: "",
        mobile: "",
        email: "",
        customer_id: this.customer_id,
        discont_percentage: 0,
        knowledge_id: 0, //forNow
        addition_type_id: 0 //fornow
      }
    };
  },
  methods: {
    handleP_CustomerSubmit: function() {
    },
    resetP_CustomerModal:function(){},
close:function(){
          this.$emit("preventPopUp");

}
  }
};
</script>

<style></style>
