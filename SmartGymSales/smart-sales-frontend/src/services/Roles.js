import { API } from "./../models/API";
// import { store } from "./../store";
export default {
    getAllRoles() {
      const getAllRoles = new API("Roles/getAllRoles");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
      return getAllRoles.get(requestBody);
    },
}