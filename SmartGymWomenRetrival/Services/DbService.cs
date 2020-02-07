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