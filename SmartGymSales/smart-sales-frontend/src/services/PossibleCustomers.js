import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    GetPossibleCustomers() {
        const GetPossibleCustomers = new API("PossibleCustomers/GetPossibleCustomers");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        return GetPossibleCustomers.get(requestBody);
      },

      insertPossibleCustomers(PossibleCustomer){
        const insertUserRole= new API("PossibleCustomers/InsertPossibleCustomer");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
    
        return insertUserRole.post(requestBody,PossibleCustomer);
      },
    }