import { API } from "./../models/API";
import { store }  from "./../store/index.js";

export default {
    
    insertReview(review){
        const insertReview= new API("Reviews/insertReview");
        const requestBody = {
          headers: {
            userName: store.getters.getUser.user_name,
            Password: store.getters.getUser.password
          },
        };
    
        return insertReview.post(requestBody,review);
      },
}