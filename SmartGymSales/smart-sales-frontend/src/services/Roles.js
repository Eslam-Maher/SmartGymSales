import { API } from "./../models/API";
import { store } from "./../store/index";
export default {
    getAllRoles() {
      const getAllRoles = new API("Roles/getAllRoles");
      const requestBody = {
        headers: {
          userName: store.getters.getUser.user_name,
          Password: store.getters.getUser.password
        },
      };
      return getAllRoles.get(requestBody);
    },
}