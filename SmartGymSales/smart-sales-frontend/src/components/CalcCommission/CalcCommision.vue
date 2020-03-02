<template>
  <b-card header="Calculate Commission" header-tag="h4">
    <div>
      <b-form>
        <b-row>
          <b-col>
            <b-form-group label="Date Range" label-for="Date-input">
              <date-picker id="Date-input" type="date" v-model="dates" range></date-picker>
            </b-form-group>
          </b-col>

          <b-col>
            <b-form-group label="Sales employee" label-for="sales-input">
              <multiselect
                id="sales-input"
                v-model="salesEmployee"
                :options="salesEmployeeOptions"
                label="name"
                track-by="id"
              ></multiselect>
            </b-form-group>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <div class="button-group actions-buttons pull-right">
              <b-button
                @click="calcCommission"
                :disabled="salesEmployee==null||dates.length<2"
                variant="primary"
              >Get Commission</b-button>
            </div>
          </b-col>
        </b-row>
      </b-form>
    </div>
  </b-card>
</template>

<script>
import UsersService from "../../services/Users";
export default {
  data() {
    return {
      salesEmployee: null,
      dates: [],
      salesEmployeeOptions: []
    };
  },
  methods: {
    getSalesUsers: function() {
      this.loadingCount++;
      UsersService.getSalesUsers()
        .then(res => {
          this.salesEmployeeOptions = res.data;
        })
        .catch(error => { // eslint-disable-line no-unused-vars
          this.$bvToast.toast("error in getting users ", this.failToastConfig);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    calcCommission: function() {
      let data = {
        from: this.dates[0],
        to: this.dates[1],
        employee: this.salesEmployee
      };
      this.$emit("calcCommission", data);
      this.onReset();
    },
    onReset: function() {
      this.dates = [];
      this.salesEmployee = null;
    }
  },
  created: function() {
    console.log(
      "asdasdasdasdasdasd"
    ); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
    this.getSalesUsers();
  }
};
</script>
<style scoped>
</style>