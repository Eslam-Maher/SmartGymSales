import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    GetknowledgeLookups() {
        const GetknowledgeLookups = new API("knowledgeLookups/GetknowledgeLookups");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        return GetknowledgeLookups.get(requestBody);
      },
    }