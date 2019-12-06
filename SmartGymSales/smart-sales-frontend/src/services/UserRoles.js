import { API } from "./../models/API";
import { store } from "./../store/index";
export default {
    getAllUserRoles() {
      const getAllUserRoles = new API("UserRoles/getAllUserRoles");
      const requestBody = {
        headers: {
          userName: store.getters.getUser.user_name,
          Password: store.getters.getUser.password
        },
      };
      return getAllUserRoles.get(requestBody);
    },
    insertUserRole(User){
      const insertUserRole= new API("UserRoles/insertUserRole");
      const requestBody = {
        headers: {
          userName: store.getters.getUser.user_name,
          Password: store.getters.getUser.password
        },
      };
  
      return insertUserRole.post(requestBody,User);
    },
    deleteUserRole(UserId){
      const deleteUserRole= new API("UserRoles/deleteUserRole/"+UserId);
      const requestBody = {
        headers: {
          userName: store.getters.getUser.user_name,
          Password: store.getters.getUser.password
        },
      };
  
      return deleteUserRole.delete(requestBody);
    }


}