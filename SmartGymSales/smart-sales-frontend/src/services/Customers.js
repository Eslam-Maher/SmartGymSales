import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    updateCustomersFromSheet(inputFile){
        const updateCustomersFromSheet= new API("Customers/updateCustomersFromSheet");
        let formData = new FormData();
        formData.append('file', inputFile);

        const requestBody = {
          headers: {
            'Content-Type': 'multipart/form-data',
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
    
        return updateCustomersFromSheet.post(requestBody,formData);
      },
      downloadCustomersExcelSheet(){
          const downloadCustomersExcelSheet= new API("Customers/downloadCustomersFile");
          const headers= {
              userName: store.getters.getUser.user_name,
              Password: store.getters.getUser.password
            };
          console.log(headers); /*eslint no-console: ["error", { allow: ["warn", "error","log"] }] */
          return downloadCustomersExcelSheet.getExcel(headers);

      },
      getAllCustomers() {
        const getAllCustomers = new API("Customers/getAllCustomers");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
        return getAllCustomers.get(requestBody);
      },

}