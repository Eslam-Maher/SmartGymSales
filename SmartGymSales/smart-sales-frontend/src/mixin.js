import moment from "moment";
import {ROLES_ENUM} from "./models/enums/roles";
export default {
  methods: {
    invalidFilter(val) {
      return val.match(/[^a-zA-Z0-9,.'/?-\s]|(\s\s+)/g) ? true : false;
    }
  },
  computed: {
    user:{
      get:function(){
        return this.$store.getters.getUser? this.$store.getters.getUser:null
      },
      set:function(value){
        this.$store.commit("setUser",value)
      }
    },
    loadingCount: {
      get: function() {
        return this.$store.getters.getLoadingCount;
      },
      set: function(value) {
        this.$store.commit("setLoadingCount", value);
      }
    },
    sucessToastConfig:function(){
      return  {
        title: `Success`,
        variant: "success",
        solid: true
        };
    },
    failToastConfig:function(){
      return {
        title: "Failure",/*eslint no-undef: "error"*/

        variant: "danger", /*eslint no-undef: "error"*/

        solid: true
        };
    },
    isAdmin:function(){
        return this.user&&this.user.userRoles && this.user.userRoles.some(element => {
         return element.role_id==ROLES_ENUM.ADMIN;
        }); 
    },
    isSales:function(){},
    isManger:function(){},

  },
  filters: {
    DD_MMM_YYYY: function(date) {
      return moment(date).format("DD-MMM-YYYY");
    }
  }
}
