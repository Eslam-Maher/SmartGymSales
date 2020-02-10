import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    GetCommissions() {
        const GetCommissions = new API("Commissions/getAllCommissions");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        return GetCommissions.get(requestBody);
      },
      calcCommission(dateFrom,dateTo,user){
        const calcCommission= new API("Commissions/calcCommission");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        const requestDate={
          dateFrom:dateFrom,
          dateTo:dateTo,
          user:user
        }
        return calcCommission.post(requestBody,requestDate);
      },

      insertCommission(Commission){
        const insertCommission= new API("Commissions/insertCommission");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
    
        return insertCommission.post(requestBody,Commission);
      },
      deleteCommission(CommissionId){
        const deleteCommission= new API("Commissions/deleteCommission/"+CommissionId);
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
    
        return deleteCommission.delete(requestBody);
      }
    }