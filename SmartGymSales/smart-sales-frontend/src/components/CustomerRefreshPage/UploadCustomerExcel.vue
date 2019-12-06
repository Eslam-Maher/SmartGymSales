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
      <b-col>
        <b-button variant="primary" @click="downloadFile()">download Template File</b-button>
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
    downloadFile: function() {
      this.loadingCount++;
      CustomersServices.downloadCustomersExcelSheet()
        .then(res => {
          var fileURL = window.URL.createObjectURL(new Blob([res.data]));
          var fileLink = document.createElement("a");

          fileLink.href = fileURL;
          fileLink.setAttribute("download", "Customers Sheet.xlsx");
          document.body.appendChild(fileLink);

          fileLink.click();
        }) // eslint-disable-line no-unused-vars

        .catch(() => {
          this.$bvToast.toast(
            "error in downloading customers sheet",
            this.failToastConfig
          );
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    submitFile: function() {
      this.loadingCount++;
      CustomersServices.updateCustomersFromSheet(this.file)
        .then(res => {
          if (res.data.length > 0) {
            res.data.forEach(element => {
              this.$bvToast.toast(element, this.failToastConfig);
            });
          }
          else{
              this.$bvToast.toast("File uploaded successfully", this.sucessToastConfig);
          }
        }) // eslint-disable-line no-unused-vars
        .catch(() => {
          // eslint-disable-line no-unused-vars
          this.$bvToast.toast(
            "error in uploading customers sheet",
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

<style>
</style>