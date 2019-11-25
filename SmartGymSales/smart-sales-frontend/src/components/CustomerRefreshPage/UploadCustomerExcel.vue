<template>
  <b-card header="Upload Customers from File" header-tag="h4">
    <b-row>
      <b-col md="4">
        <b-form-group label="Default:" label-for="file-default" label-cols-sm="2">
          <b-form-file
            id="file-default"
            v-model="file"
            accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
            :state="Boolean(file)"
            placeholder="Choose a file or drop it here..."
            drop-placeholder="Drop file here..."
          ></b-form-file>
        </b-form-group>
      </b-col>
      <b-col>
        <b-button-group>
          <b-button variant="success" @click="submitFile()">Submit File</b-button>
          <b-button @click="file = null" variant="danger">Reset File</b-button>
        </b-button-group>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
import CustomersServices from "../../services/Customers";
export default {
  data() {
    return {
      file: null
    };
  },
  methods: {
    submitFile: function() {
      this.loadingCount++;
      CustomersServices.updateCustomersFromSheet(this.file)
        .then(res => {}) // eslint-disable-line no-unused-vars
        .catch(error => { // eslint-disable-line no-unused-vars
                      this.$bvToast.toast('error in uploading customers sheet',this.failToastConfig);
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