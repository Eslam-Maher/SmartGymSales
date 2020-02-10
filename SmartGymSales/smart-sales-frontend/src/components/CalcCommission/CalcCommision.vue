<template>
  <b-card header="Calculate Commission" header-tag="h4">
    <div>
      <b-form>
        <b-row>
          <b-col>
            <b-form-group label="From" label-for="fromDate-input">
               <date-pick v-model="fromDate"  id="fromDate-input" ></date-pick>
            </b-form-group>
          </b-col>
<!-- class="form-control" -->
          <b-col>
            <b-form-group label="To" label-for="toDate-input">
            <date-pick v-model="toDate" id="toDate-input" ></date-pick>

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
                   <b-button @click="getCommission" variant="primary">Get Commission</b-button>
            </div>
          </b-col>
         </b-row>     
      </b-form>
      <b-row>

      </b-row>
    </div>
  </b-card>
</template>

<script>
import UsersService from "../../services/Users";

// import "vue-date-pick/dist/vueDatePick.css";

export default {
  data() {
    return {
      salesEmployee: null,
      toDate: null,
      fromDate: null,
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
        .catch(error => {// eslint-disable-line no-unused-vars
          this.$bvToast.toast("error in getting users ", this.failToastConfig);
        })
        .finally(() => {
          this.loadingCount--;
        });
    },
    getCommission:function(){}
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

.vdp-datepicker__calendar {
  width: 100% !important;
  border-radius: 1rem !important;
}

.vdp-datepicker__calendar > header {
  font-size: 0.9rem;
}

.vdp-datepicker__calendar .cell {
  font-size: 0.8rem;
  height: 2rem;
}
.vdp-datepicker .input-group .form-control[readonly] {
  background-color: white !important;
}
</style>