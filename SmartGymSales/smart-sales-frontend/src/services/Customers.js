import { API } from "./../models/API";
export default {
    updateCustomersFromSheet(inputFile){
        const updateCustomersFromSheet= new API("Customers/updateCustomersFromSheet");
        let formData = new FormData();
        formData.append('file', inputFile);

        const requestBody = {
          headers: {
            'Content-Type': 'multipart/form-data'
            // userName: store.getters.getUser.eMail,
          //   Password: store.getters.getToken
          },
        };
    
        return updateCustomersFromSheet.post(requestBody,formData);
      },

}