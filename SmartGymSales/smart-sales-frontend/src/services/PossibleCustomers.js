import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    GetPossibleCustomerrs() {
        const GetPossibleCustomerrs = new API("PossibleCustomers/GetPossibleCustomerrs");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        return GetPossibleCustomerrs.get(requestBody);
      },
    }