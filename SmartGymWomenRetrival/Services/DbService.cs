using SmartGymWomenRetrival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymWomenRetrival.Services
{
    public class DbService
    {
        public List<Customer> GetAllWomenCustomers()
        {
            SmatGymWomenEntities db = new SmatGymWomenEntities();
            return db.Customers.ToList();
        }

        public List<Customer> getAllActiveWomenCustomers(DateTime? from ,DateTime? to) {

            SmatGymWomenEntities db = new SmatGymWomenEntities();
                List<Membership> membershipList = GetAllWomenMembership().Where(x => x.S_Date > from && x.S_Date < to).ToList();

                return db.Customers.Where(x => !String.IsNullOrEmpty(x.Phone_mobile)
                && membershipList.Where(y => y.Customer_ID == x.Customer_ID).Any()).ToList();
        }


        public List<T_session_subscriber> GetAllWomenT_session_subscriber()
        {
            SmatGymWomenEntities db = new SmatGymWomenEntities();
            return db.T_session_subscriber.ToList();
        }

        public List<Membership> GetAllWomenMembership()
        {
            SmatGymWomenEntities db = new SmatGymWomenEntities();
            return db.Memberships.ToList();
        }
    }
}