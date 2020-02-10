<template>
  <b-card header="Create new User" header-tag="h4">
    <div>
      <b-form @submit="onSubmit" @reset="onReset">
        <b-row>
          <b-col>
            <b-form-group id="input-group-1" label="New Member Percentatge:" label-for="input-1">
              <b-form-input
                id="input-1"
                v-model="InputCommission.new_customer_percentatge"
                required
                type="number"
                min="0"
                max="100"
                oninput="validity.valid || (value='')"
                placeholder="Enter Percentatge"
              ></b-form-input>
              <b-form-invalid-feedback>Percentatge must be between 0 and 100</b-form-invalid-feedback>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group id="input-group-2" label="Old Member Percentatge:" label-for="input-2">
              <b-form-input
                id="input-2"
                v-model="InputCommission.old_customer_percentatge"
                required
                type="number"
                min="0"
                max="100"
                oninput="validity.valid || (value='')"
                placeholder="Enter Percentatge"
              ></b-form-input>
              <b-form-invalid-feedback>Percentatge must be between 0 and 100</b-form-invalid-feedback>
            </b-form-group>
          </b-col>
          <b-col>
            <b-form-group id="input-group-3" label="Target:" label-for="input-3">
              <b-form-input
                id="input-3"
                required
                type="number"
                min="0"
                oninput="validity.valid || (value='')"
                v-model="InputCommission.target"
                placeholder="Enter Target"
              ></b-form-input>
              <b-form-invalid-feedback>Target must be more than 0</b-form-invalid-feedback>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <div class="button-group actions-buttons">
              <b-button type="submit" variant="primary">Submit</b-button>
              <b-button type="reset" variant="secondary">Reset</b-button>
            </div>
          </b-col>
        </b-row>
      </b-form>
    </div>
  </b-card>
</template>

<script>
import CommissionService from "../../services/Commission";

export default {
  name: "addCommission",
  data() {
    return {
      InputCommission: {
        new_customer_percentatge: null,
        old_customer_percentatge: null,
        target: null
      }
    };
  },

  methods: {
    onSubmit: function() {
      this.loadingCount++;
      CommissionService.insertCommission(this.InputCommission)
        .then(res => {
          if (res.data.length > 0) {
            res.data.forEach(element => {
              this.$bvToast.toast(element, this.failToastConfig);
            });
          } else {
            this.$bvToast.toast(
              "Commission added Successfully",
              this.sucessToastConfig
            );
            this.$emit("refreshGrid");
            this.onReset();
          }
          // eslint-disable-line no-unused-vars
        })
        .catch(error => {
          this.$bvToast.toast("Commission addtion Failed", error.message);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    onReset: function() {
      this.InputCommission.new_customer_percentatge = null;
      this.InputCommission.old_customer_percentatge = null;
      this.InputCommission.target = null;
    }
  }
};
</script>

<style scoped>
.card-body {
  text-align: "center";
}
.actions-buttons {
  float: right !important;
}
</style>