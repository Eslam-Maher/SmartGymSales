import { API } from "./../models/API";
// import { store } from "./../store";
export default {
    getAllUserRoles() {
      const getAllUserRoles = new API("UserRoles/getAllUserRoles");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
      return getAllUserRoles.get(requestBody);
    },
    insertUserRole(User){
      const insertUserRole= new API("UserRoles/insertUserRole");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return insertUserRole.post(requestBody,User);
    },
    deleteUserRole(UserId){
      const deleteUserRole= new API("UserRoles/deleteUserRole/"+UserId);
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return deleteUserRole.delete(requestBody);
    }


}