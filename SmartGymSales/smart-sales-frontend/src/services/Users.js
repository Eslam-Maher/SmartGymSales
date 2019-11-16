import { API } from "./../models/API";
// import { store } from "./../store";
export default {
    getAllUsers() {
      const getAllUsers = new API("Users/getUsers");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return getAllUsers.get(requestBody);
    },
    insertUser(User){
      const insertUser= new API("Users/insertUsers");
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return insertUser.post(requestBody,User);
    },
    login(user_name,password){

      const login= new API("Users/login");
      const requestBody={
        user_name:user_name,
        password:password
      }
      const config = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
      return login.post(config,requestBody);
    },
    deleteUser(UserId){
      const deleteUser= new API("Users/deleteUser/"+UserId);
      const requestBody = {
        headers: {
          // userName: store.getters.getUser.eMail,
        //   Password: store.getters.getToken
        },
      };
  
      return deleteUser.delete(requestBody);
    }
}