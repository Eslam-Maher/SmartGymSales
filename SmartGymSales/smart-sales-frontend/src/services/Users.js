import { API } from "./../models/API";
// import { store } from "./../store";
export default {
    getAllUsers() {
      const getAllUsers = new API("Users");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return getAllUsers.get(requestBody);
    }
}